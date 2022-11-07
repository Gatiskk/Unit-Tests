using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VendingMachine.Interfaces
{
    public interface IVendingMachine
    {
        string Manufacturer { get; }

        public bool HasProducts { get; }

        public Money Amount { get; }
        Product[] Products { get; }
        Money InsertCoin(Money amount);

        Money ReturnMoney();

        bool AddProduct(string name, Money price, int count);

        bool UpdateProduct(int productNumber, string name, Money? price, int amount);
    }
}