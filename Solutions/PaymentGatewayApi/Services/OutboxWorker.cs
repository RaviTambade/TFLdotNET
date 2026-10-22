using System.Net.Http.Json;
using System.Text.Json;
using PaymentGatewayApi.Models;

namespace PaymentGatewayApi.Services;

public sealed class OutboxWorker : BackgroundService
{
    private readonly IServiceScopeFactory _scopeFactory;
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly IConfiguration _configuration;
    private readonly ILogger<OutboxWorker> _logger;

    public OutboxWorker(IServiceScopeFactory scopeFactory, IHttpClientFactory httpClientFactory,
        IConfiguration configuration, ILogger<OutboxWorker> logger)
    {
        _scopeFactory = scopeFactory;
        _httpClientFactory = httpClientFactory;
        _configuration = configuration;
        _logger = logger;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            try { await ProcessBatch(stoppingToken); }
            catch (OperationCanceledException) when (stoppingToken.IsCancellationRequested) { break; }
            catch (Exception ex) { _logger.LogError(ex, "Outbox worker batch failed"); }
            await Task.Delay(TimeSpan.FromSeconds(5), stoppingToken);
        }
    }

    private async Task ProcessBatch(CancellationToken ct)
    {
        using var scope = _scopeFactory.CreateScope();
        var store = scope.ServiceProvider.GetRequiredService<JsonPaymentStore>();
        var url = _configuration["PaymentGateway:WebhookUrl"];
        foreach (var item in await store.GetPendingEventsAsync(ct))
        {
            item.Attempts++;
            try
            {
                if (string.IsNullOrWhiteSpace(url))
                {
                    // Local learning mode: mark as processed without making an external call.
                    item.Status = "SimulatedDelivered";
                    item.DeliveredAt = DateTimeOffset.UtcNow;
                }
                else
                {
                    var payment = await store.GetPaymentAsync(item.PaymentId, ct);
                    using var response = await _httpClientFactory.CreateClient().PostAsJsonAsync(url, new
                    {
                        id = item.Id,
                        type = item.EventType,
                        createdAt = item.CreatedAt,
                        payment = payment is null ? null : new
                        {
                            payment.Id, payment.MerchantOrderId, payment.Amount,
                            payment.Currency, payment.Status, payment.ProviderReference
                        }
                    }, ct);
                    response.EnsureSuccessStatusCode();
                    item.Status = "Delivered";
                    item.DeliveredAt = DateTimeOffset.UtcNow;
                    item.LastError = "";
                }
            }
            catch (Exception ex)
            {
                item.LastError = ex.Message;
                _logger.LogWarning(ex, "Webhook delivery failed for outbox event {EventId}", item.Id);
                // Remains Pending; next polling iteration retries until five attempts.
            }
            await store.UpdateOutboxEventAsync(item, ct);
        }
    }
}
