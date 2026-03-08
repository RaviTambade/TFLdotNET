namespace Shared.Models
{
    public class EventMessage
    {
        public Guid EventId { get; set; }
        public string EventType { get; set; }
        public DateTime Timestamp { get; set; }

        public object Data { get; set; }
    }
}