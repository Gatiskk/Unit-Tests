using System;
using System.Collections.Generic;
using System.Text;

namespace Hierarchy
{
    public abstract class Mammal : Animal
    {
        protected string LivingRegion;

        protected Mammal(string name, string type, double weight, string livingRegion)
            : base(name, type, weight)
        {
            LivingRegion = livingRegion;
        }

        public override string ToString()
        {
            return $"{AnimalType} [{AnimalName}, {AnimalWeight}, {LivingRegion}, {FoodEaten}]";
        }
    }
}