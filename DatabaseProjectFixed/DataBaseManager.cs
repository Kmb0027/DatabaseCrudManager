using System;
namespace ConnectingToDB
{
    public class DataBaseManager
    {
        public Product AskUserToDefineProduct(string crudOperationToPerform)
        {

            Console.WriteLine($"What product name would you like to {crudOperationToPerform}?");
            string name = Console.ReadLine();

            Console.WriteLine("For what price will it sell?");
            if(!decimal.TryParse(Console.ReadLine(), out decimal price))
            {
                Console.WriteLine("The price needs to be a decimal value.");
            }
            Console.WriteLine("What is the category id?");
            if(!int.TryParse(Console.ReadLine(), out int catId))
            {
                Console.WriteLine("The price needs to be a number.");
            }
            Product userProduct = new Product() { Name = name, Price = price, CategoryId = catId };
            return userProduct;
        }
        public Product AskUserToDefineProductUpdate()
        {
            Console.WriteLine("What is this products new name?");
            string name = Console.ReadLine();
            Console.WriteLine("For what price will it sell?");
            if (!decimal.TryParse(Console.ReadLine(), out decimal price))
            {
                Console.WriteLine("The price needs to be a decimal value.");
            }
            Console.WriteLine("What is the category id?");
            if (!int.TryParse(Console.ReadLine(), out int catId))
            {
                Console.WriteLine("The price needs to be a number.");
            }
            Product userProduct = new Product() { Name = name, Price = price, CategoryId = catId };
            return userProduct;
        }

        public bool GetUserConfirmation(string crudOperation)
        {
            Console.WriteLine($"Are you sure you want to {crudOperation}?");
            string userAnswer = Console.ReadLine();
            return userAnswer.ToUpper() == "YES" || userAnswer.ToUpper() == "YEAH" || userAnswer.ToUpper() == "YEP" || userAnswer.ToUpper() == "Y";
        }

        public void SucessfulFeedback(string crudOperationPastTense)
        {
            Console.WriteLine($"You sucessfully {crudOperationPastTense} an entry");
        }

        public bool ToSeeProductList()
        {
            Console.WriteLine("Do you want to see the list of products?");
            string userAnswer = Console.ReadLine();
            return userAnswer.ToUpper() == "YES" || userAnswer.ToUpper() == "YEAH" || userAnswer.ToUpper() == "YEP" || userAnswer.ToUpper() == "Y";
        }

        public int ProductIdSelection(string getOrDelete)
        {
            Console.WriteLine($"Which product according to product ID would you like to {getOrDelete}?");
            if(!int.TryParse(Console.ReadLine(), out int productId))
            {
                Console.WriteLine("The product id must be a number.");
            }
            return productId;
        }
       
       public bool MainMenuRequest()
        {
            Console.WriteLine("Would you like to go to the main menu?");
            string userAnswer = Console.ReadLine();
            return userAnswer.ToUpper() == "YES" || userAnswer.ToUpper() == "YEAH" || userAnswer.ToUpper() == "YEP" || userAnswer.ToUpper() == "Y";
        }
        public int ProductIdToUpdate()
        {
            Console.WriteLine("Which product ID would you like to update?");
            if (!int.TryParse(Console.ReadLine(), out int productId))
            {
                Console.WriteLine("The product id must be a number.");
            }
            return productId;
        }

    }
}
