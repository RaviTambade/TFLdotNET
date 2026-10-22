# PaymentGatewayApi — JSON-file learning implementation

A .NET 10 ASP.NET Core Web API that demonstrates a payment lifecycle without a database.
It uses JSON files under `Data/` and a simulated provider. It does **not** move real money.

## Requirements
- .NET 10 SDK
- PowerShell, Command Prompt, Bash, or terminal

## Run
```bash
dotnet restore
dotnet run --urls http://localhost:5080
```

Health check:
```bash
curl http://localhost:5080/api/health
```

Development API key is configured in `appsettings.json`:
`dev-merchant-key-change-me`

For any protected API, send `X-Api-Key` with that value.

## Create a payment (Bash / macOS / Linux)
```bash
curl -i -X POST http://localhost:5080/api/payments \
  -H "Content-Type: application/json" \
  -H "X-Api-Key: dev-merchant-key-change-me" \
  -H "Idempotency-Key: order-1001-attempt-1" \
  -d '{
    "merchantOrderId": "ORDER-1001",
    "amount": 1250.00,
    "currency": "INR",
    "method": "UPI",
    "paymentToken": "demo-token-001"
  }'
```

PowerShell:
```powershell
$headers = @{
  "X-Api-Key" = "dev-merchant-key-change-me"
  "Idempotency-Key" = "order-1001-attempt-1"
}
$body = @{
  merchantOrderId = "ORDER-1001"
  amount = 1250
  currency = "INR"
  method = "UPI"
  paymentToken = "demo-token-001"
} | ConvertTo-Json
Invoke-RestMethod -Method Post -Uri "http://localhost:5080/api/payments" -Headers $headers -ContentType "application/json" -Body $body
```

## Try the demo rules
- Amount ending in `.99` (e.g. `1250.99`) is declined by the simulated provider.
- Amount ending in `.77` (e.g. `1250.77`) produces `Unknown` to model an uncertain outcome.
- Amount `>= 40000` is rejected by a demo fraud rule.
- Amount must be positive and no more than `50000`.
- Only INR is supported in this demo.

## Endpoints
| Method | Path | Purpose |
|---|---|---|
| GET | `/api/health` | Public health check |
| POST | `/api/payments` | Create / process payment |
| GET | `/api/payments` | List latest payments (`?take=50`, max 200) |
| GET | `/api/payments/{id}` | Get a payment |
| POST | `/api/payments/{id}/reconcile` | Query simulated provider and update status |
| POST | `/api/payments/{id}/retry` | Safe reconciliation path for uncertain/processing transactions |

## JSON files
- `Data/payments.json`: payment records
- `Data/idempotency.json`: idempotency key to request hash/payment mapping
- `Data/outbox.json`: notification events
- `Data/provider-transactions.json`: simulated provider's idempotent transaction ledger

The application creates these files on first run. Do not edit them while the application is running.

## Webhooks / outbox
With no `PaymentGateway:WebhookUrl`, the worker marks events `SimulatedDelivered`.
Set a reachable HTTPS endpoint in `appsettings.json` to exercise real HTTP POST delivery. Delivery retries at most five times.
This is a simple JSON outbox demonstration, not a durable broker. A crash between updating the payment file and the outbox file can lose an event.

## Idempotency
Send `Idempotency-Key` for every create request. Repeating the same key with the same request returns the existing payment. Reusing the same key with a different request returns HTTP 409.
The in-process semaphore only coordinates requests in one application process. Multiple replicas/processes can race; a production database unique constraint and transaction are required.

## Important production limitations
This project is a learning model, not PCI DSS compliant and not suitable for real money:
1. JSON files are not a transactional database. No cross-file ACID guarantees, robust multi-process locking, backups, or high-volume performance.
2. Replace the simulated provider with a certified payment-provider SDK/API. Use provider idempotency keys and verify signed provider webhooks.
3. Never collect or store raw card numbers, CVV, UPI PINs, passwords, or payment secrets. Use provider-hosted checkout/tokenization.
4. Replace the demo API key with proper merchant identity, key rotation, HTTPS, secret storage, and scoped authorization.
5. Replace the basic fraud rule with a risk engine and operational review.
6. Add durable queue/broker, dead-letter queue, webhook signatures, exponential backoff, metrics, alerts, and audit retention.
7. Reconciliation must use provider reports/status APIs and settlement records; this demo only reconciles its simulated provider ledger.
8. A production system must treat unknown/time-out outcomes as uncertain and query the provider before retrying to avoid duplicate charges.

## Suggested learning extensions
- Split `PaymentService` into validation, fraud, provider, repository and notification interfaces.
- Add xUnit tests for idempotency, amount validation, decline, and unknown outcomes.
- Add a local webhook receiver to inspect delivered event payloads.
- Replace JSON persistence with SQL transactions and a unique index on `IdempotencyKey`.
