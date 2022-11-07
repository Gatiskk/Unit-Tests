using System;
using System.Collections.Generic;
using System.Text;

namespace Hierarchy
{
    public abstract class Feline : Mammal
    {
        protected Feline(string name, string type, double weight, string livingRegion)
            : base(name, type, weight, livingRegion)
        {

        }
    }
}