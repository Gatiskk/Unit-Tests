using System;
using System.Collections.Generic;
using System.Linq;

namespace VendingMachine
{
    internal class Program
    {
        private static readonly VendingMachine vendingMachine = new("Frozen Treats", 30, new List<string>());

        static void Main()
        {
            vendingMachine.AddProduct("Magnum", new Money(0, 70), 5);
            vendingMachine.AddProduct("Polo", new Money(0, 50), 6);
            vendingMachine.AddProduct("BenJerry", new Money(0, 60), 7);
            Console.WriteLine(@"
      (**)
     (____)
    (______)
   (________)
  (__________)
   \/\/\/\/\/
    \/\/\/\/
     \/\/\/
      \/\/
       \/ ");

            while (true)
            {

                Console.WriteLine(new String('*', 50));
                Console.WriteLine($"Vending machine {vendingMachine.Manufacturer}. What would you like to do?\n");
                Console.WriteLine("Type 0 to exit");
                Console.WriteLine("Type 1 to check the available ice-creams");
                Console.WriteLine("Type 2 to insert coins");
                Console.WriteLine("Type 3 to buy a product");
                Console.WriteLine("Type 4 to receive change");
                Console.WriteLine("Type 5 to add a product");
                Console.WriteLine("Type 6 to update a product");
                Console.WriteLine(new String('*', 50));

                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "0":
                        return;
                    case "1":
                        ShowProducts();
                        break;
                    case "2":
                        InsertCoins();
                        break;
                    case "3":
                        BuyProduct();
                        break;
                    case "4":
                        ReturnChange();
                        break;
                    case "5":
                        AddProduct();
                        break;
                    case "6":
                        UpdateProduct();
                        break;
                    default:
                        break;
                }
            }
        }

        private static void ShowProducts()
        {
            Console.Clear();
            vendingMachine.GetAvailableProducts();
        }

        private static void AddProduct()
        {
            Console.Clear();

            Console.WriteLine("Enter product name:");
            string name = Console.ReadLine();

            Console.WriteLine("Enter price in the following format (euros cents): 0 00");
            string[] coins = Console.ReadLine().Split();
            int euros = int.Parse(coins[0]);
            int cents = int.Parse(coins[1]);

            Console.WriteLine("Enter amount:");
            int amount = int.Parse(Console.ReadLine());
            vendingMachine.AddProduct(name, new Money(euros, cents), amount);
        }

        private static void UpdateProduct()
        {
            Console.Clear();

            Console.WriteLine($"Enter product number 1 - {vendingMachine.Products.Count()}");
            int prodNum = int.Parse(Console.ReadLine());

            Console.WriteLine("Enter product name:");
            string name = Console.ReadLine();

            Console.WriteLine("Enter price in the following format (euros cents): 0 00");
            string[] coins = Console.ReadLine().Split();
            int euros = int.Parse(coins[0]);
            int cents = int.Parse(coins[1]);

            Console.WriteLine("Enter amount:");
            int amount = int.Parse(Console.ReadLine());

            vendingMachine.UpdateProduct(prodNum, name, new Money(euros, cents), amount);
        }

        private static void InsertCoins()
        {
            Console.Clear();

            Console.WriteLine($"\nValid coins are: {vendingMachine.ValidCoins}");
            Console.WriteLine("Insert coins in the following format (euros cents): 0 00");
            string[] coins = Console.ReadLine().Split();
            int euros = int.Parse(coins[0]);
            int cents = int.Parse(coins[1]);
            vendingMachine.InsertCoin(new Money(euros, cents));
            Console.Write($"Your amount is {vendingMachine.Amount}\n");
        }

        private static void BuyProduct()
        {
            Console.Clear();

            vendingMachine.GetAvailableProducts();
            Console.WriteLine($"\nEnter product number 1 - {vendingMachine.Products.Length}:");
            int prodNum = int.Parse(Console.ReadLine());
            vendingMachine.BuyProduct(prodNum);
        }

        private static void ReturnChange()
        {
            Console.Clear();
            vendingMachine.ReturnMoney();
        }
    }
}