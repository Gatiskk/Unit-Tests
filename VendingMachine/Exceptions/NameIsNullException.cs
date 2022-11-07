using System;
namespace VendingMachine.Exceptions
{
    public class NameIsNullException : Exception
    {
        public NameIsNullException() : base($"name can not be null") { }
    }
}