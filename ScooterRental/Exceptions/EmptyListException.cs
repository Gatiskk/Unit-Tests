using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScooterRental.Exceptions
{
    public class EmptyListException : Exception
    {
        public EmptyListException() :
            base($"The list can not be null or empty") { }
    }
}
