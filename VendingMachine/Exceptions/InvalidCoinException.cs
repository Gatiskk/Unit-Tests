using System;

namespace VendingMachine.Exceptions
{
    public class InvalidCoinException : Exception
    {
        public InvalidCoinException(): base ($"Cant insert this coin") {}
    }
}