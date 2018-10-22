using System;
using MySql.Data.MySqlClient;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using System.IO;

namespace ConnectingToDB
{
    class Program
    {
        static void Main(string[] args)
        {
#if DEBUG
            string jsonText = File.ReadAllText("appsetting.development.json");
#else
            string jsonText = File.ReadAllText("appsetting.release.json");
#endif
            string connStr = JObject.Parse(jsonText)["ConnectionStrings"]["DefaultConnection"].ToString();
            DataBaseManager dbm = new DataBaseManager();
            var prodRep = new ProductRepository(connStr);
            bool mainMenuTrigger = true;
            while (mainMenuTrigger)
            {
                Console.Clear();
                Console.WriteLine("Would you like to \n1. View all products in the product table? \n2. Create an entry in the product table \n3. Update an entry in the product table \n4. Delete an entry in the product table");
                int userChoice = int.Parse(Console.ReadLine());
                Console.Clear();

                switch (userChoice)
                {
                    case 1:
                        Console.Clear();
                        List<Product> products = prodRep.GetAllProducts();
                        foreach (Product product in products)
                        {
                            Console.WriteLine($"Product ID:{product.Id}, Name:{product.Name}, Price: ${product.Price}, Category ID: {product.CategoryId}.");
                        }
                        mainMenuTrigger = dbm.MainMenuRequest();
                        break;


                    case 2:
                        Console.Clear();
                        Product createProduct = dbm.AskUserToDefineProduct("create");
                        Console.Clear();
                        if (dbm.GetUserConfirmation("create entry")) { prodRep.CreateProduct(createProduct); }
                        else { break; }
                        dbm.SucessfulFeedback("created");
                        mainMenuTrigger = dbm.MainMenuRequest();
                        break;

                    case 3:
                        Console.Clear();
                        if (dbm.ToSeeProductList())
                        {
                            List<Product> updateProducts = prodRep.GetAllProducts();
                            foreach (Product product in updateProducts)
                            {
                                Console.WriteLine($"Product ID:{product.Id}, Name:{product.Name}, Price: ${product.Price}, Category ID: {product.CategoryId}.");
                            }

                        }
                        Product updateProduct = dbm.AskUserToDefineProduct("update");
                        Console.Clear();
                        if (dbm.GetUserConfirmation("update entry"))
                        {
                            prodRep.UpdateProduct(updateProduct);
                            Console.Clear();
                            dbm.SucessfulFeedback("updated");
                        }
                        mainMenuTrigger = dbm.MainMenuRequest();
                        break;

                    case 4:
                        Console.Clear();
                        if (dbm.ToSeeProductList())
                        {
                            List<Product> deleteProducts = prodRep.GetAllProducts();
                            foreach (Product product in deleteProducts)
                            {
                                Console.WriteLine($"Product ID:{product.Id}, Name:{product.Name}, Price: ${product.Price}, Category ID: {product.CategoryId}.");
                            }

                        }
                        int deleteId = dbm.ProductIdSelection("delete");
                        if (dbm.GetUserConfirmation("delete"))
                        {
                            prodRep.DeleteProduct(deleteId);
                            Console.Clear();
                            dbm.SucessfulFeedback("deleted");
                        }
                        mainMenuTrigger = dbm.MainMenuRequest();
                        break;




                    default:
                        break;
                }
            }


            Console.ReadLine();
        }
    }
}
