namespace ProductCatalog.Models
{
    public class Product
    {
        public int ProductId { get; set; }
        public string? ProductTitle { get; set; }
        public string? Description { get; set; }
        public int StockAvailable { get; set; }
        public double UnitPrice { get; set; }
        public string? ImageUrl { get; set; }
        public int CategoryId { get; set; }
        public int SupplierId { get; set; }

    }
}
