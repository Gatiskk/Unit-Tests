using System;

namespace VendingMachine.Exceptions
{
    public class NegativeOrZeroAmountException : Exception
    {
        public NegativeOrZeroAmountException(): base ($"Amount can not be zero or negative") {}
    }
}