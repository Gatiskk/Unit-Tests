using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VendingMachine.Exceptions
{
    public class NotEnoughEmptySpotsException : Exception
    {
        public NotEnoughEmptySpotsException(): base($"Not enough slots in machine"){}
    }
}
