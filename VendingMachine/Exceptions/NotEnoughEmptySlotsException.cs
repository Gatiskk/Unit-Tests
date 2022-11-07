using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VendingMachine.Exceptions
{
    public class NotEnoughEmptySlotsException : Exception
    {
        public NotEnoughEmptySlotsException() : base($"Machine is already full!") { }
    }
}