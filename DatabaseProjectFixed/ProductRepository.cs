using System;
using MySql.Data.MySqlClient;
using System.Collections.Generic;
namespace ConnectingToDB
{
    public class ProductRepository
    {
        private string connStr;

        public ProductRepository(string _connString)
        {
            connStr = _connString;
        }

        public List<Product> GetAllProducts()
        {
            MySqlConnection conn = new MySqlConnection(connStr);

            List<Product> products = new List<Product>();

            using (conn)
            {
                conn.Open();

                MySqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = "SELECT ProductID, Name, Price, CategoryID FROM products;";

                MySqlDataReader dataReader = cmd.ExecuteReader();

                while(dataReader.Read())
                {
                    Product product = new Product()
                    {
                        Id = (int)dataReader["ProductID"],
                        Name = dataReader["Name"].ToString(),
                        Price = (decimal)dataReader["Price"],
                        CategoryId = (int)dataReader["CategoryID"]

                    };
                    products.Add(product);
                }
                return products;
            }
        }

        public Product GetProduct(int id)
        {
            MySqlConnection conn = new MySqlConnection(connStr);

            using (conn)
            {
                conn.Open();
                MySqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = $"SELECT ProductID, Name, Price, CategoryId FROM products WHERE ProductID= @id";
                cmd.Parameters.AddWithValue("id", id);
                MySqlDataReader dataReader = cmd.ExecuteReader();
                if(dataReader.Read())
                {
                    Product product = new Product()
                    {
                        Name = dataReader["Name"].ToString(),
                        Id = (int)dataReader["ProductID"],
                        Price = (decimal)dataReader["Price"],
                        CategoryId = (int)dataReader["CategoryID"]
                    };
                    return product;
                }
                else
                {
                    return null;
                }
            }
        }

       public void CreateProduct(string name, double price, int catID)
        {
            MySqlConnection conn = new MySqlConnection(connStr);

            using (conn)
            {
                conn.Open();

                MySqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = "INSERT INTO products (Name, Price, CategoryID) VALUES (@name, @price, @catID);";
                cmd.Parameters.AddWithValue("name", name);
                cmd.Parameters.AddWithValue("price", price);
                cmd.Parameters.AddWithValue("catID", catID);
                cmd.ExecuteNonQuery();

            }

        }
        public void CreateProduct(Product product)
        {
            MySqlConnection conn = new MySqlConnection(connStr);

            using (conn)
            {
                conn.Open();

                MySqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = "INSERT INTO products (Name, Price, CategoryID) VALUES (@name, @price, @catID);";
                cmd.Parameters.AddWithValue("name", product.Name);
                cmd.Parameters.AddWithValue("price", product.Price);
                cmd.Parameters.AddWithValue("catID", product.CategoryId);
                cmd.ExecuteNonQuery();

            }

        }
        public void UpdateProduct(Product product)
        {
            var conn = new MySqlConnection(connStr);

            using (conn)
            {
                conn.Open();
                var cmd = conn.CreateCommand();
                cmd.CommandText = "UPDATE products SET Name = @n, Price = @p, CategoryId = @cID WHERE ProductId = @pID;";
                cmd.Parameters.AddWithValue("n", product.Name);
                cmd.Parameters.AddWithValue("p", product.Price);
                cmd.Parameters.AddWithValue("cID", product.CategoryId);
                cmd.Parameters.AddWithValue("pID", product.Id);
                cmd.ExecuteNonQuery();
            }

        }
        public void DeleteProduct(int id)
        {
            var conn = new MySqlConnection(connStr);

            using(conn)
            {
                conn.Open();
                var cmd = conn.CreateCommand();
                cmd.CommandText = "DELETE FROM products WHERE ProductId = @pID;";
                cmd.Parameters.AddWithValue("pID", id);
                cmd.ExecuteNonQuery();

            }


        }
        public void DeleteProduct(Product product)
        {
            var conn = new MySqlConnection(connStr);

            using (conn)
            {
                conn.Open();
                var cmd = conn.CreateCommand();
                cmd.CommandText = "DELETE FROM products WHERE ProductId = @pID;";
                cmd.Parameters.AddWithValue("pID", product.Id);
                cmd.ExecuteNonQuery();

            }


        }


        // avoid sql injection
        //concat values has like LIKE @xyz
        // cmd.Parameters.ADDWITHVALUE("xyz",$"%{Name}%")
    }
}
