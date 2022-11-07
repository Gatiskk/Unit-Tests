using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VendingMachine.Exceptions
{
    public class ProductWithThisNameAlreadyExistsException : Exception

    {
        public ProductWithThisNameAlreadyExistsException() : base($"Product already exists")
        {
        }
    }
}