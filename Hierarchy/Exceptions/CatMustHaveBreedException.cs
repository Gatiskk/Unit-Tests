using System;

namespace Hierarchy.Exceptions
{
    public class CatMustHaveBreedException : Exception
    {
        public CatMustHaveBreedException() :
                base($"Cat must have breed") { }
        }
    }