using Microsoft.AspNetCore.Mvc;
using PaymentGatewayApi.Models;
using PaymentGatewayApi.Services;

namespace PaymentGatewayApi.Controllers;

[ApiController]
[Route("api/payments")]
public sealed class PaymentsController : ControllerBase
{
    private readonly PaymentService _service;
    public PaymentsController(PaymentService service) => _service = service;

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreatePaymentRequest request, CancellationToken ct)
    {
        var key = Request.Headers["Idempotency-Key"].FirstOrDefault() ?? "";
        var result = await _service.CreateAsync(request, key, ct);
        if (result.Error is not null)
            return result.Conflict
                ? Conflict(new { error = result.Error })
                : BadRequest(new { error = result.Error });

        if (result.Payment is null) return StatusCode(500, new { error = "Payment could not be created." });
        if (result.Replay) Response.Headers["Idempotency-Replayed"] = "true";
        var response = PaymentResponse.From(result.Payment);
        return result.Replay ? Ok(response) : AcceptedAtAction(nameof(GetById), new { id = result.Payment.Id }, response);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(string id, CancellationToken ct)
    {
        var payment = await _service.GetAsync(id, ct);
        return payment is null ? NotFound(new { error = "Payment not found." }) : Ok(PaymentResponse.From(payment));
    }

    [HttpGet]
    public async Task<IActionResult> List([FromQuery] int take = 50, CancellationToken ct = default)
    {
        var items = await _service.ListAsync(take, ct);
        return Ok(items.Select(PaymentResponse.From));
    }

    [HttpPost("{id}/reconcile")]
    public async Task<IActionResult> Reconcile(string id, CancellationToken ct)
    {
        var result = await _service.ReconcileAsync(id, ct);
        if (result.Error is not null) return NotFound(new { error = result.Error });
        return Ok(PaymentResponse.From(result.Payment!));
    }

    [HttpPost("{id}/retry")]
    public async Task<IActionResult> Retry(string id, CancellationToken ct)
    {
        var payment = await _service.GetAsync(id, ct);
        if (payment is null) return NotFound(new { error = "Payment not found." });
        if (payment.Status != nameof(PaymentStatus.Unknown) &&
            payment.Status != nameof(PaymentStatus.Processing))
            return Conflict(new { error = "Only Unknown or Processing payments can be reconciled. Do not blindly retry a charge." });

        // Provider-side idempotency is keyed by the existing payment ID in this simulator.
        var result = await _service.ReconcileAsync(id, ct);
        if (result.Payment is null) return NotFound(new { error = result.Error });
        return Ok(PaymentResponse.From(result.Payment));
    }
}
