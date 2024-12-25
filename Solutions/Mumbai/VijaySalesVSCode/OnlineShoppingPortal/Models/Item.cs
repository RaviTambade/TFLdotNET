namespace OnlineShoppingPortal.Models
{
    public class Item
    {
        public int Id { get; set; }
        public Product TheProduct { get; set; }
        public int Quantity { get; set; }
    }
}
