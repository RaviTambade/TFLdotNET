using System.Linq;
namespace Catalog;

public class ProductManager
    {
        public static List<Product> GetSoldOutProducts()
        {
            //BI bussiness intelligence
            //analytical query
            List<Product> products = GetAllProducts();
            //List<Product> products = GetAllProductsFromDatabase();

            var soldOutProducts = from prod in products
                                  where prod.Balance == 0
                                  select prod;
            return soldOutProducts as List<Product>; 
        }

        public static List<Product> GetProuductsInStockLessthan(int amount)
        {
            List<Product> products = GetAllProducts();
            var expensiveInStockProducts =
                from prod in products
                where prod.Balance > 0 && prod.UnitPrice > amount
                select prod;

            return expensiveInStockProducts as List<Product>;
        }

        public static List<string> GetProjectTitles()
        {
            List<Product> products = GetAllProducts();
            var productNames =
                from prod in products
                select prod.Title;

            return productNames as List<string>;
        }

        public static dynamic GetProductDetails()
        {
            List<Product> products = GetAllProducts();

            var productInfos =
                from prod in products
                select new { prod.Title, prod.Category, Price = prod.UnitPrice };

            return productInfos;
        }

        public static void Takethree()
        {
            int[] numbers = { 5, 4, 1, 3, 9, 8, 6, 7, 2, 0 };
            var first3Numbers = numbers.Take(3);

            Console.WriteLine("First 3 numbers:");
            foreach (var n in first3Numbers)
            {
                Console.WriteLine(n);
            }
        }

        public static void Skip()
        {
            int[] numbers = { 5, 4, 1, 3, 9, 8, 6, 7, 2, 0 };

            var allButFirst4Numbers = numbers.Skip(4);

            Console.WriteLine("All but first 4 numbers:");
            foreach (var n in allButFirst4Numbers)
            {
                Console.WriteLine(n);
            }
        }

        public static List<string> GetFruitsOrderby()
        {
            string[] words = { "cherry", "apple", "blueberry","banana", "mango" };
            var sortedWords =
                from word in words
                orderby word
                select word;
            return sortedWords as List<string>;
        }

        public static List<Product> GetProductsOrderby()
        {
            List<Product> products = GetAllProducts();
            var sortedProducts =
                from prod in products
                orderby prod.Title
                select prod;

            return sortedProducts as List<Product>;
        }   

        public static List<Product> GetProductsByDescending()
        {
            List<Product> products = GetAllProducts();
            var sortedProducts =
                from prod in products
                orderby prod.Balance descending
                select prod;
            return sortedProducts as List<Product>;
        }

        public static List<Product> GetProductsGroupByCategory()
        {
            List<Product> products = GetAllProducts();
            var orderGroups =
                from prod in products
                group prod by prod.Category into prodGroup
                select new { Category = prodGroup.Key, Products = prodGroup };

            return orderGroups as List<Product>;
        }
        
        public static List<string> GetProductsDistinct()
        {
            List<Product> products = GetAllProducts();
            var categoryNames = (    from prod in products
                                     select prod.Category
                                ).Distinct();

            return categoryNames as List<string>;
        }
        
        public static void ToDictionary()
        {
            var scoreRecords = new[] { new {Name = "Alice", Score = 50},
                                new {Name = "Bob"  , Score = 40},
                                new {Name = "Cathy", Score = 45}
                            };

            var scoreRecordsDict = scoreRecords.ToDictionary(sr => sr.Name);     
        }
        
        public static Product GetFirstProduct()
        {
            List<Product> products = GetAllProducts();
            Product product5 = products.FirstOrDefault(p => p.ProductId == 5);
            return product5;
        }
        
        public static dynamic  GetProductCount()
        {
            List<Product> products = GetAllProducts();
            var categoryCounts =
                from prod in products
                group prod by prod.Category into prodGroup
                select new { Category = prodGroup.Key, ProductCount = prodGroup.Count() };
            return categoryCounts;
        }
        
        public static dynamic GetAveragePriceOfCategory()
        {
            List<Product> products = GetAllProducts();
            var categories =
                from prod in products
                group prod by prod.Category into prodGroup
                select new { Category = prodGroup.Key, AveragePrice = prodGroup.Average(p => p.UnitPrice) };

            return categories;


        }
        
        public static List<Product> GetAllProducts()
        {
            List<Product> allProducts = new List<Product>();

            allProducts.Add(new Product { ProductId = 1, Title = "Gerbera", Description = "Wedding Flower", UnitPrice = 6, Category = "Flower", Balance = 5000 });
            allProducts.Add(new Product { ProductId = 2, Title = "Rose", Description = "Valentine Flower", UnitPrice = 15, Category = "Flower", Balance = 7000 });
            allProducts.Add(new Product { ProductId = 3, Title = "Lotus", Description = "Worship Flower", UnitPrice = 26, Category = "Flower", Balance = 3400 });
            allProducts.Add(new Product { ProductId = 4, Title = "Carnation", Description = "Pink carnations signify a mother's love, red is for admiration and white for good luck", UnitPrice = 16, Category = "Flower", Balance = 27000 });
            allProducts.Add(new Product { ProductId = 5, Title = "Lily", Description = "Lilies are among the most popular flowers in the U.S.", UnitPrice = 6, Category = "Flower", Balance = 1000 });
            allProducts.Add(new Product { ProductId = 6, Title = "Jasmine", Description = "Jasmine is a genus of shrubs and vines in the olive family", UnitPrice = 26, Category = "Flower", Balance = 2000 });
            allProducts.Add(new Product { ProductId = 7, Title = "Daisy", Description = "Give a gift of these cheerful flowers as a symbol of your loyalty and pure intentions.", UnitPrice = 36, Category = "Flower", Balance = 159 });
            allProducts.Add(new Product { ProductId = 8, Title = "Aster", Description = "Asters are the September birth flower and the the 20th wedding anniversary flower.", UnitPrice = 16, Category = "Flower", Balance = 67 });
            allProducts.Add(new Product { ProductId = 9, Title = "Daffodil", Description = "Wedding Flower", UnitPrice = 6, Category = "Flower", Balance = 5000 });
            allProducts.Add(new Product { ProductId = 10, Title = "Dahlia", Description = "Dahlias are a popular and glamorous summer flower.", UnitPrice = 7, Category = "Flower", Balance = 0 });
            allProducts.Add(new Product { ProductId = 11, Title = "Hydrangea", Description = "Hydrangea is the fourth wedding anniversary flower", UnitPrice = 12, Category = "Flower", Balance = 0 });
            allProducts.Add(new Product { ProductId = 12, Title = "Orchid", Description = "Orchids are exotic and beautiful, making a perfect bouquet for anyone in your life.", UnitPrice = 10, Category = "Flower", Balance = 700 });
            allProducts.Add(new Product { ProductId = 13, Title = "Statice", Description = "Surprise them with this fresh, fabulous array of Statice flowers", UnitPrice = 16, Category = "Flower", Balance = 1500 });
            allProducts.Add(new Product { ProductId = 14, Title = "Sunflower", Description = "Sunflowers express your pure love.", UnitPrice = 8, Category = "Flower", Balance = 2300 });
            allProducts.Add(new Product { ProductId = 15, Title = "Tulip", Description = "Tulips are the quintessential spring flower and available from January to June.", UnitPrice = 17, Category = "Flower", Balance = 10000 });
            return allProducts;
        }

        public static List<Product> GetAllProductsFromDatabase()
        {
            List<Product> allProducts = new List<Product>();
            //ProductDAL.GetAll();
            return allProducts;
        }

}