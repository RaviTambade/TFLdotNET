using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using PaymentGatewayApi.Models;

namespace PaymentGatewayApi.Services;

public sealed class PaymentService
{
    private readonly JsonPaymentStore _store;
    private readonly ILogger<PaymentService> _logger;
    private readonly decimal _maxAmount;

    public PaymentService(JsonPaymentStore store, IConfiguration config, ILogger<PaymentService> logger)
    {
        _store = store;
        _logger = logger;
        _maxAmount = config.GetValue<decimal>("PaymentGateway:MaxAmount", 50000m);
    }

    public async Task<(PaymentRecord? Payment, bool Replay, bool Conflict, string? Error)> CreateAsync(
        CreatePaymentRequest request, string idempotencyKey, CancellationToken ct)
    {
        if (string.IsNullOrWhiteSpace(request.MerchantOrderId))
            return (null, false, false, "MerchantOrderId is required.");
        if (request.Amount <= 0 || request.Amount > _maxAmount)
            return (null, false, false, $"Amount must be greater than zero and at most {_maxAmount}.");
        if (request.Currency != "INR")
            return (null, false, false, "This demo supports INR only.");
        if (!new[] { "UPI", "CARD", "WALLET", "NETBANKING" }.Contains(request.Method.ToUpperInvariant()))
            return (null, false, false, "Method must be UPI, CARD, WALLET, or NETBANKING.");
        if (string.IsNullOrWhiteSpace(request.PaymentToken))
            return (null, false, false, "PaymentToken is required (demo token only).");
        if (string.IsNullOrWhiteSpace(idempotencyKey) || idempotencyKey.Length > 100)
            return (null, false, false, "A valid Idempotency-Key header (1-100 characters) is required.");

        // Simple educational fraud rule; not a real fraud engine.
        if (request.Amount >= 40000m)
            return (null, false, false, "Payment flagged by demo fraud rule.");

        var normalized = JsonSerializer.Serialize(new
        {
            request.MerchantOrderId, request.Amount, request.Currency,
            Method = request.Method.ToUpperInvariant(), request.PaymentToken
        });
        var hash = Convert.ToHexString(SHA256.HashData(Encoding.UTF8.GetBytes(normalized)));

        var payment = new PaymentRecord
        {
            MerchantOrderId = request.MerchantOrderId,
            Amount = request.Amount,
            Currency = request.Currency,
            Method = request.Method.ToUpperInvariant(),
            IdempotencyKey = idempotencyKey,
            Status = nameof(PaymentStatus.Pending)
        };

        var created = await _store.CreateAsync(payment, hash, ct);
        if (created.Conflict)
            return (created.Payment, true, true, "Idempotency key was already used with a different request.");
        if (created.IsReplay)
            return (created.Payment, true, false, null);

        try
        {
            payment.Status = nameof(PaymentStatus.Processing);
            await _store.UpdatePaymentAsync(payment, ct);

            var providerStatus = await _store.AuthorizeProviderAsync(payment.Id, payment.Amount, ct);
            payment.ProviderReference = await _store.GetProviderReferenceAsync(payment.Id, ct) ?? "";
            payment.Status = providerStatus switch
            {
                "Authorized" => nameof(PaymentStatus.Authorized),
                "Declined" => nameof(PaymentStatus.Declined),
                _ => nameof(PaymentStatus.Unknown)
            };
            payment.FailureReason = payment.Status == nameof(PaymentStatus.Declined)
                ? "Simulated provider decline." : "";
            await _store.UpdatePaymentAsync(payment, ct);
            await _store.AddOutboxEventAsync(payment.Id, ct);
            _logger.LogInformation("Payment {PaymentId} transitioned to {Status}", payment.Id, payment.Status);
            return (payment, false, false, null);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Payment {PaymentId} outcome could not be confirmed", payment.Id);
            payment.Status = nameof(PaymentStatus.Unknown);
            payment.FailureReason = "Outcome uncertain; reconcile before attempting another charge.";
            try { await _store.UpdatePaymentAsync(payment, ct); } catch { /* preserve original error */ }
            return (payment, false, false, null);
        }
    }

    public async Task<(PaymentRecord? Payment, string? Error)> ReconcileAsync(string id, CancellationToken ct)
    {
        var payment = await _store.GetPaymentAsync(id, ct);
        if (payment is null) return (null, "Payment not found.");

        var providerStatus = await _store.GetProviderStatusAsync(id, ct);
        if (providerStatus is null)
        {
            payment.Status = nameof(PaymentStatus.Unknown);
            payment.FailureReason = "Provider has no recorded result; manual/provider-side investigation may be required.";
        }
        else
        {
            payment.Status = providerStatus switch
            {
                "Authorized" => nameof(PaymentStatus.Authorized),
                "Declined" => nameof(PaymentStatus.Declined),
                _ => nameof(PaymentStatus.Unknown)
            };
            payment.FailureReason = "";
            payment.ProviderReference = await _store.GetProviderReferenceAsync(id, ct) ?? "";
        }

        await _store.UpdatePaymentAsync(payment, ct);
        await _store.AddOutboxEventAsync(payment.Id, ct);
        return (payment, null);
    }

    public Task<PaymentRecord?> GetAsync(string id, CancellationToken ct) => _store.GetPaymentAsync(id, ct);
    public Task<List<PaymentRecord>> ListAsync(int take, CancellationToken ct) => _store.ListPaymentsAsync(take, ct);
}
