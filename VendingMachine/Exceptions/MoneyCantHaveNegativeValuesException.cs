using System;

namespace VendingMachine.Exceptions
{
    public class MoneyCantHaveNegativeValuesException : Exception
    {
        public MoneyCantHaveNegativeValuesException(): base($"Vending machine don't accept negative value") { }
    }
}
