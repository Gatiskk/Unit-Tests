using System;
using System.Collections.Generic;
using System.Linq;
using VendingMachine.Exceptions;
using VendingMachine.Interfaces;

namespace VendingMachine
{
    public class VendingMachine : IVendingMachine
    {
        private readonly int _capacity;
        private int _emptySpots;
        private readonly List<string> _validCoins = new() { "0.10", "0.20", "0.50", "1.00", "2.00" };

        private List<Product> _products { get; set; }
        public Product[] Products
        {
            get { return _products.ToArray(); }
        }

        public string Manufacturer { get; }

        public string ValidCoins => String.Join(" | ", _validCoins);
        public int Capacity { get; }
        public int EmptySpots { get; }
        public bool HasProducts => _products.Count > 0;
        public Money Amount { get; private set; }

        public VendingMachine(string manufacturer, int capacity, List<string>_validCoins)
        {
            Manufacturer = manufacturer;
            _products = new List<Product>();
            _capacity = capacity;
            _emptySpots = capacity;

            if (manufacturer == null)
            {
                throw new NameIsNullException();
            }

            if (_capacity <= 0)
            {
                throw new NegativeOrZeroAmountException();
            }
        }

        private Money SumInsertedCoins(Money amount)
        {
            var euros = Amount.Euros + amount.Euros;
            var cents = Amount.Cents + amount.Cents;
            if (cents >= 100)
            {
                euros++;
                cents = cents % 100;
            }

            return new Money(euros, cents);
        }

        private Money CalculateChange(Money amount)
        {
            var euros = Amount.Euros - amount.Euros;
            var cents = Amount.Cents - amount.Cents;
            if (cents < 0)
            {
                euros--;
                cents = 100 + cents;
            }

            return new Money(euros, cents);
        }

        public Money InsertCoin(Money amount)
        {
            if (!_validCoins.Contains(amount.ToString()))
            {
                throw new InvalidCoinException();
            }

            Amount = SumInsertedCoins(amount);
            return Amount;
        }

        public bool BuyProduct(int productNumber)
        {
            if (productNumber < 1 || productNumber > _products.Count)
            {
                Console.WriteLine($"Invalid product number. Valid numbers are between 1 and {_products.Count}");
                return false;
            }
            else if (_products[productNumber - 1].Available < 1)
            {
                Console.WriteLine("This product is not available. Try another one.");
                return false;
            }
            else if (_products[productNumber - 1].Price.GetValue() > Amount.GetValue())
            {
                Console.WriteLine("Your funds are not sufficient. Please insert more coins.");
                return false;
            }
            else
            {
                _emptySpots++;
                Product boughtProduct = _products[productNumber - 1];
                boughtProduct.Available--;
                _products[productNumber - 1] = boughtProduct;
                Amount = CalculateChange(boughtProduct.Price);
                Console.WriteLine($"Enjoy your {boughtProduct.Name}!");
                Console.WriteLine($"Your amount is: {Amount}");
                return true;
            }
        }

        public Money ReturnMoney()
        {
            if (Amount.GetValue() == 0)
            {
                Console.WriteLine("No change!");
                Console.WriteLine("Thank you! See you next time!");
                return Amount;
            }
            else
            {
                Console.WriteLine($"Here is your change: {Amount}");
                Console.WriteLine("Thank you! See you next time!");
                Amount = new Money();
                return Amount;
            }
        }

        public bool AddProduct(string name, Money price, int amount)
        {
            if (_products.Exists(p => p.Name == name))
            {
                throw new InvalidProductNumberException();
            }
            else if (_emptySpots - amount < 0)
            {
                throw new NotEnoughEmptySlotsException();
            }
            else
            {
                _products.Add(new Product(name, price, amount));
                _emptySpots -= amount;
                return true;
            }
        }

        public bool UpdateProduct(int productNumber, string name, Money? price, int amount)
        {
            int amountDiff = Math.Abs(_products[productNumber - 1].Available - amount);

            if (productNumber < 1 && productNumber > _products.Count)
            {
                throw new InvalidProductNumberException();
            }
            else if (_products.Select(p => p.Name).ToString() == name)
            {
                GetAllProducts();
                return false;
            }
            else if (amount > _products[productNumber - 1].Available && amountDiff > EmptySpots)
            {
                throw new ProductWithThisNameAlreadyExistsException();
            }
            else
            {
                Product productToUpdate = _products[productNumber - 1];
                productToUpdate.Name = name;
                productToUpdate.Price = (Money)price;
                productToUpdate.Available = amount;
                _products[productNumber - 1] = productToUpdate;

                _emptySpots = _products[productNumber - 1].Available > amount ? _emptySpots - amountDiff : _emptySpots + amountDiff;

                return true;
            }
        }

        public void GetAllProducts()
        {
            if (HasProducts)
            {
                int num = 1;
                foreach (var p in _products)
                {
                    Console.WriteLine("Our product list:\n");
                    Console.WriteLine(String.Format("{0, 3} | {1, 9} | {2, 5} | {3, 3}\n", "N", "Name", "Price", "Amount"));
                    Console.WriteLine(String.Format("{0, 3} | {1, 9} | {2, 5} | {3, 3}", num, p.Name, p.Price, p.Available));
                    num++;
                }
            }
            else
            {
                throw new MachineIsEmptyException();
            }
        }

        public void GetAvailableProducts()
        {
            if (HasProducts)
            {
                int num = 1;
                Console.WriteLine("\nWe currently offer:\n");
                Console.WriteLine(String.Format("{0, 3} | {1, 9} | {2, 5} | {3, 3}\n", "N", "Name", "Price", "Amount"));
                foreach (var p in _products)
                {
                    if (p.Available > 0)
                    {
                        Console.WriteLine(String.Format("{0, 3} | {1, 9} | {2, 5} | {3, 3}", num, p.Name, p.Price, p.Available));
                    }

                    num++;
                }
            }
            else
            {
                throw new MachineIsEmptyException();
            }
        }
    }
}