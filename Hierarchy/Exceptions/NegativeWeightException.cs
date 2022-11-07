using System;
namespace Hierarchy.Exceptions
{
    public class NegativeWeightException : Exception
    {
        public NegativeWeightException(double weight) :
                base($"Can not input negative or zero weight {weight}") { }
        }
    }