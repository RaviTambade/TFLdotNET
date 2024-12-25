namespace Catalog
{
    public class ProductManager
    {
        public List<Product> Products { get; set; }

        public ProductManager()
        {
            Products = new List<Product>();
        }

        public void AddProduct(Product product)
        {
            Products.Add(product);
        }

        public void RemoveProduct(Product product)
        {
            Products.Remove(product);
        }

        public void UpdateProduct(Product product)
        {
            int i=45;
            bool b = true;
            string s = "Hello";
            var result="Welcome";
            //anonymous type
            var obj= new {
                Name="John",
                Age=25  
            };

            var productToUpdate = Products.FirstOrDefault(p => p.Id == product.Id);
            if (productToUpdate != null)
            {
                productToUpdate.Name = product.Name;
                productToUpdate.Price = product.Price;
                productToUpdate.Description = product.Description;
                productToUpdate.ImageUrl = product.ImageUrl;
                productToUpdate.Category = product.Category;
            }
        }

        public Product GetProductById(string id)
        {
            return Products.FirstOrDefault(p => p.Id == id);
        }

        public List<Product> GetProductsByCategory(string category)
        {
            return Products.Where(p => p.Category == category).ToList();
        }

        public List<Product> GetProductsByPrice(decimal minPrice, decimal maxPrice)
        {
            //C# 3: LInq to Objects
            //LINQ: Language Integrated Query (C# Feature)
            return Products.Where(p => p.Price >= minPrice && p.Price <= maxPrice).ToList();
        }
    }
}