using System;

namespace VendingMachine.Exceptions
{
    public class MoneyCentsHaveToBeLimitException : Exception
    {
        public MoneyCentsHaveToBeLimitException(): base($"Cents must be from 0 - 99") { }
    }
}
