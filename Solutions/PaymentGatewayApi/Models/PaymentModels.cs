namespace PaymentGatewayApi.Models;

public enum PaymentStatus
{
    Pending,
    Processing,
    Authorized,
    Failed,
    Declined,
    Unknown
}

public sealed class CreatePaymentRequest
{
    public string MerchantOrderId { get; set; } = "";
    public decimal Amount { get; set; }
    public string Currency { get; set; } = "INR";
    public string Method { get; set; } = "UPI";
    // Demo token only. Never accept/store raw card numbers, CVV, UPI PIN, or banking passwords.
    public string PaymentToken { get; set; } = "";
}

public sealed class PaymentRecord
{
    public string Id { get; set; } = Guid.NewGuid().ToString("N");
    public string MerchantOrderId { get; set; } = "";
    public decimal Amount { get; set; }
    public string Currency { get; set; } = "INR";
    public string Method { get; set; } = "UPI";
    public string Status { get; set; } = nameof(PaymentStatus.Pending);
    public string ProviderReference { get; set; } = "";
    public string IdempotencyKey { get; set; } = "";
    public string FailureReason { get; set; } = "";
    public DateTimeOffset CreatedAt { get; set; } = DateTimeOffset.UtcNow;
    public DateTimeOffset UpdatedAt { get; set; } = DateTimeOffset.UtcNow;
}

public sealed class IdempotencyRecord
{
    public string Key { get; set; } = "";
    public string RequestHash { get; set; } = "";
    public string PaymentId { get; set; } = "";
    public DateTimeOffset CreatedAt { get; set; } = DateTimeOffset.UtcNow;
}

public sealed class OutboxEvent
{
    public string Id { get; set; } = Guid.NewGuid().ToString("N");
    public string PaymentId { get; set; } = "";
    public string EventType { get; set; } = "payment.updated";
    public string Status { get; set; } = "Pending";
    public int Attempts { get; set; }
    public DateTimeOffset CreatedAt { get; set; } = DateTimeOffset.UtcNow;
    public DateTimeOffset? DeliveredAt { get; set; }
    public string LastError { get; set; } = "";
}

public sealed record PaymentResponse(
    string Id,
    string MerchantOrderId,
    decimal Amount,
    string Currency,
    string Method,
    string Status,
    string ProviderReference,
    string FailureReason,
    DateTimeOffset CreatedAt,
    DateTimeOffset UpdatedAt)
{
    public static PaymentResponse From(PaymentRecord p) => new(
        p.Id, p.MerchantOrderId, p.Amount, p.Currency, p.Method,
        p.Status, p.ProviderReference, p.FailureReason, p.CreatedAt, p.UpdatedAt);
}
