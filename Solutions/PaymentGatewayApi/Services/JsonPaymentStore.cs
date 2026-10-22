using System.Text.Json;
using PaymentGatewayApi.Models;

namespace PaymentGatewayApi.Services;

// Single-process teaching repository. The semaphore protects only this process.
// It is NOT a substitute for a transactional database or multi-instance coordination.
public sealed class JsonPaymentStore
{
    private readonly SemaphoreSlim _gate = new(1, 1);
    private readonly string _dataDirectory;
    private readonly JsonSerializerOptions _jsonOptions = new(JsonSerializerDefaults.Web)
    {
        WriteIndented = true
    };

    public JsonPaymentStore(IWebHostEnvironment env)
    {
        _dataDirectory = Path.Combine(env.ContentRootPath, "Data");
        Directory.CreateDirectory(_dataDirectory);
        EnsureFile("payments.json", "[]");
        EnsureFile("idempotency.json", "[]");
        EnsureFile("outbox.json", "[]");
        EnsureFile("provider-transactions.json", "[]");
    }

    private void EnsureFile(string name, string content)
    {
        var path = Path.Combine(_dataDirectory, name);
        if (!File.Exists(path)) File.WriteAllText(path, content);
    }

    private string PathFor(string name) => Path.Combine(_dataDirectory, name);

    private async Task<List<T>> ReadAsync<T>(string name, CancellationToken ct)
    {
        await using var stream = new FileStream(PathFor(name), FileMode.Open, FileAccess.Read, FileShare.Read);
        return await JsonSerializer.DeserializeAsync<List<T>>(stream, _jsonOptions, ct) ?? new List<T>();
    }

    private async Task WriteAtomicAsync<T>(string name, List<T> data, CancellationToken ct)
    {
        var path = PathFor(name);
        var temp = path + ".tmp";
        await using (var stream = new FileStream(temp, FileMode.Create, FileAccess.Write, FileShare.None))
        {
            await JsonSerializer.SerializeAsync(stream, data, _jsonOptions, ct);
            await stream.FlushAsync(ct);
        }
        File.Move(temp, path, true);
    }

    public async Task<(PaymentRecord? Payment, bool IsReplay, bool Conflict)> CreateAsync(
        PaymentRecord payment, string requestHash, CancellationToken ct)
    {
        await _gate.WaitAsync(ct);
        try
        {
            var keys = await ReadAsync<IdempotencyRecord>("idempotency.json", ct);
            var existingKey = keys.FirstOrDefault(x => x.Key == payment.IdempotencyKey);
            if (existingKey is not null)
            {
                var existingPayment = (await ReadAsync<PaymentRecord>("payments.json", ct))
                    .FirstOrDefault(x => x.Id == existingKey.PaymentId);
                return (existingPayment, true, existingKey.RequestHash != requestHash);
            }

            var payments = await ReadAsync<PaymentRecord>("payments.json", ct);
            payments.Add(payment);
            keys.Add(new IdempotencyRecord
            {
                Key = payment.IdempotencyKey,
                RequestHash = requestHash,
                PaymentId = payment.Id
            });

            // Sequential atomic file replacements. A crash between replacements can still
            // leave inconsistent files; production requires a database transaction.
            await WriteAtomicAsync("payments.json", payments, ct);
            await WriteAtomicAsync("idempotency.json", keys, ct);
            return (payment, false, false);
        }
        finally { _gate.Release(); }
    }

    public async Task<PaymentRecord?> GetPaymentAsync(string id, CancellationToken ct)
    {
        await _gate.WaitAsync(ct);
        try { return (await ReadAsync<PaymentRecord>("payments.json", ct)).FirstOrDefault(x => x.Id == id); }
        finally { _gate.Release(); }
    }

    public async Task<List<PaymentRecord>> ListPaymentsAsync(int take, CancellationToken ct)
    {
        await _gate.WaitAsync(ct);
        try { return (await ReadAsync<PaymentRecord>("payments.json", ct))
            .OrderByDescending(x => x.CreatedAt).Take(Math.Clamp(take, 1, 200)).ToList(); }
        finally { _gate.Release(); }
    }

    public async Task UpdatePaymentAsync(PaymentRecord payment, CancellationToken ct)
    {
        await _gate.WaitAsync(ct);
        try
        {
            var list = await ReadAsync<PaymentRecord>("payments.json", ct);
            var index = list.FindIndex(x => x.Id == payment.Id);
            if (index < 0) throw new InvalidOperationException("Payment not found.");
            payment.UpdatedAt = DateTimeOffset.UtcNow;
            list[index] = payment;
            await WriteAtomicAsync("payments.json", list, ct);
        }
        finally { _gate.Release(); }
    }

    public async Task AddOutboxEventAsync(string paymentId, CancellationToken ct)
    {
        await _gate.WaitAsync(ct);
        try
        {
            var list = await ReadAsync<OutboxEvent>("outbox.json", ct);
            list.Add(new OutboxEvent { PaymentId = paymentId });
            await WriteAtomicAsync("outbox.json", list, ct);
        }
        finally { _gate.Release(); }
    }

    public async Task<List<OutboxEvent>> GetPendingEventsAsync(CancellationToken ct)
    {
        await _gate.WaitAsync(ct);
        try { return (await ReadAsync<OutboxEvent>("outbox.json", ct))
            .Where(x => x.Status == "Pending" && x.Attempts < 5).ToList(); }
        finally { _gate.Release(); }
    }

    public async Task UpdateOutboxEventAsync(OutboxEvent item, CancellationToken ct)
    {
        await _gate.WaitAsync(ct);
        try
        {
            var list = await ReadAsync<OutboxEvent>("outbox.json", ct);
            var index = list.FindIndex(x => x.Id == item.Id);
            if (index >= 0) { list[index] = item; await WriteAtomicAsync("outbox.json", list, ct); }
        }
        finally { _gate.Release(); }
    }

    public async Task<string?> GetProviderStatusAsync(string paymentId, CancellationToken ct)
    {
        await _gate.WaitAsync(ct);
        try
        {
            var list = await ReadAsync<ProviderTransaction>("provider-transactions.json", ct);
            return list.FirstOrDefault(x => x.PaymentId == paymentId)?.Status;
        }
        finally { _gate.Release(); }
    }

    public async Task<string> AuthorizeProviderAsync(string paymentId, decimal amount, CancellationToken ct)
    {
        await _gate.WaitAsync(ct);
        try
        {
            var list = await ReadAsync<ProviderTransaction>("provider-transactions.json", ct);
            var existing = list.FirstOrDefault(x => x.PaymentId == paymentId);
            if (existing is not null) return existing.Status; // provider-side idempotency simulation

            // Deterministic demo rules:
            // > 50,000 is rejected by API; amount ending .99 is declined; amount ending .77 is unknown.
            var status = amount % 100 == 99 ? "Declined"
                : amount % 100 == 77 ? "Unknown"
                : "Authorized";

            list.Add(new ProviderTransaction
            {
                PaymentId = paymentId,
                Status = status,
                ProviderReference = "SIM-" + Guid.NewGuid().ToString("N")[..12].ToUpperInvariant(),
                UpdatedAt = DateTimeOffset.UtcNow
            });
            await WriteAtomicAsync("provider-transactions.json", list, ct);
            return status;
        }
        finally { _gate.Release(); }
    }

    public async Task<string?> GetProviderReferenceAsync(string paymentId, CancellationToken ct)
    {
        await _gate.WaitAsync(ct);
        try
        {
            var list = await ReadAsync<ProviderTransaction>("provider-transactions.json", ct);
            return list.FirstOrDefault(x => x.PaymentId == paymentId)?.ProviderReference;
        }
        finally { _gate.Release(); }
    }

    private sealed class ProviderTransaction
    {
        public string PaymentId { get; set; } = "";
        public string Status { get; set; } = "";
        public string ProviderReference { get; set; } = "";
        public DateTimeOffset UpdatedAt { get; set; }
    }
}
