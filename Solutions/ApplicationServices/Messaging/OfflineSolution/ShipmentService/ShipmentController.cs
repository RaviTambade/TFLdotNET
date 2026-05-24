using Microsoft.AspNetCore.Mvc;

namespace ShipmentService
{


    [ApiController]
    [Route("[controller]")]
    public class ShipmentController : ControllerBase
    {
        [HttpPost("track")]
        public IActionResult TrackShipment([FromBody] Shipment shipment)
        {
            // Simulate tracking logic
            var trackingInfo = new
            {
                ShipmentId = shipment.ShipmentId,
                Status = "In Transit",
                EstimatedDelivery = DateTime.Now.AddDays(3)
            };
            return Ok(trackingInfo);
        }
    }
    public class Shipment
    {
        public int ShipmentId { get; set; }
        public string Destination { get; set; }
    }

}