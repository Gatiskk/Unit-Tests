using System;

namespace Hierarchy.Exceptions
{
    public class NegativeFoodException : Exception
    {
        public NegativeFoodException() :
                base($"Can not input negative food") { }
        }
    }