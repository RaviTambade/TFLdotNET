using MySql.Data.MySqlClient;
using ProductCatalog.Models;
using ProductCatalog.Repositories.Interfaces;
using System.Data;

namespace ProductCatalog.Repositories
{
    public class ProductRepositoryMySql : IProductRepository
    {
        private IConfiguration _configuration;
        private string _conString;
        public ProductRepositoryMySql(IConfiguration configuration)
        {
            _configuration = configuration;
            _conString = this._configuration.GetConnectionString("ECommerceConnection");
        }
        public async Task<IEnumerable<Product>> GetAll()
        {
            await Task.Delay(3000);
            List<Product> products = new List<Product>();
            MySqlConnection connection = new MySqlConnection(_conString);
            try
            {
                string query = "SELECT * FROM products";
                MySqlCommand command = new MySqlCommand(query, connection);
                await connection.OpenAsync();
                MySqlDataReader reader = command.ExecuteReader();
                while (await reader.ReadAsync())
                {
                    int id = Int32.Parse(reader["product_id"].ToString());
                    string? title = reader["product_title"].ToString();
                    string? description = reader["description"].ToString();
                    int stockAvailable = Int32.Parse(reader["stock_available"].ToString());
                    double price = double.Parse(reader["unit_price"].ToString());
                    string? imgUrl = reader["image"].ToString();
                    int categoryId = Int32.Parse(reader["category_id"].ToString());
                    int supplierId = Int32.Parse(reader["supplier_id"].ToString());


                    Product product = new Product
                    {
                        ProductId = id,
                        ProductTitle = title,
                        Description = description,
                        StockAvailable = stockAvailable,
                        UnitPrice = price,
                        ImageUrl = imgUrl,
                        CategoryId = categoryId,
                        SupplierId = supplierId
                    };

                    products.Add(product);
                }
                reader.Close();
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                connection.Close();
            }
            return products;
        }
        public async Task<Product> GetById(int productId)
        {
            Product product = new Product();
            MySqlConnection connection = new MySqlConnection(_conString);
            try
            {
                string query = "SELECT * FROM products where product_id=@productId";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@productId", productId);
                await connection.OpenAsync();
                MySqlDataReader reader = command.ExecuteReader();
                while (await reader.ReadAsync())
                {
                    string? name = reader["product_title"].ToString();
                    string? description = reader["description"].ToString();
                    int stockAvailable = Int32.Parse(reader["stock_available"].ToString());
                    double price = double.Parse(reader["unit_price"].ToString());
                    string? imgUrl = reader["image"].ToString();
                    int categoryId = Int32.Parse(reader["category_id"].ToString());
                    int supplierId = Int32.Parse(reader["supplier_id"].ToString());



                    product = new Product
                    {
                        ProductId = productId,
                        ProductTitle = name,
                        StockAvailable = stockAvailable,
                        Description = description,
                        UnitPrice = price,
                        ImageUrl = imgUrl,
                        CategoryId = categoryId,
                        SupplierId = supplierId

                    };

                }
                reader.Close();
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                connection.Close();
            }

            return product;
        }
        public async Task<bool> Insert(Product product)
        {
            bool status = false;
            MySqlConnection connection = new MySqlConnection(_conString);
            try
            {
                string query = "INSERT INTO products(product_title,description,stock_available,unit_price,image,category_id,supplier_id)VALUES(@productTitle,@description,@stockAvailable,@unitPrice,@image,@categoryId,@supplierId)";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@productTitle", product.ProductTitle);
                command.Parameters.AddWithValue("@description", product.Description);
                command.Parameters.AddWithValue("@stockAvailable", product.StockAvailable);
                command.Parameters.AddWithValue("@unitPrice", product.UnitPrice);
                command.Parameters.AddWithValue("@image", product.ImageUrl);
                command.Parameters.AddWithValue("@categoryId", product.CategoryId);
                command.Parameters.AddWithValue("@supplierId", product.SupplierId);
                await connection.OpenAsync();
                int rowsAffected = await command.ExecuteNonQueryAsync();
                if (rowsAffected > 0)
                {
                    status = true;
                }
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                connection.Close();
            }
            return status;
        }
        public async Task<bool> Update(Product product)
        {
            bool status = false;
            MySqlConnection connection = new MySqlConnection(_conString);
            try
            {
                string query = "UPDATE products SET product_title=@productTitle , description=@description , stock_available=@stockAvailable , unit_price=@unitPrice , image=@imageUrl, category_id=@categoryId, supplier_id=@supplierId  WHERE product_id=@productId";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@productId", product.ProductId);
                command.Parameters.AddWithValue("@productTitle", product.ProductTitle);
                command.Parameters.AddWithValue("@description", product.Description);
                command.Parameters.AddWithValue("@stockAvailable", product.StockAvailable);
                command.Parameters.AddWithValue("@unitPrice", product.UnitPrice);
                command.Parameters.AddWithValue("@imageUrl", product.ImageUrl);
                command.Parameters.AddWithValue("@categoryId", product.CategoryId);
                command.Parameters.AddWithValue("@supplierId", product.SupplierId);
                Console.WriteLine(query);
                Console.WriteLine(product.ProductId);
                Console.WriteLine(product.ProductTitle);
                await connection.OpenAsync();
                int rowsAffected = await command.ExecuteNonQueryAsync();
                if (rowsAffected > 0)
                {
                    status = true;
                }
            }
            catch (Exception e)
            {
                throw e;

            }
            finally
            {
                connection.Close();
            }
            return status;
        }
        public async Task<bool> Delete(int productId)
        {
            bool status = false;
            MySqlConnection connection = new MySqlConnection(_conString);
            try
            {
                string query = "DELETE FROM products WHERE product_id=@productId";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@productId", productId);
                await connection.OpenAsync();
                int rowsAffected = await command.ExecuteNonQueryAsync();
                if (rowsAffected > 0)
                {
                    status = true;
                }
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                connection.Close();
            }
            return status;
        }

        public async Task<bool> HikePrice(double percentage)
        {
            bool status = false;
            MySqlConnection con = new MySqlConnection();
            con.ConnectionString = _conString;
            try
            {
                MySqlCommand command = new MySqlCommand("hike_price", con);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@percentage", percentage);
                con.Open();
                command.ExecuteNonQuery();
                status = true;
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                con.Close();
            }
            return status;
        }

    }
}
