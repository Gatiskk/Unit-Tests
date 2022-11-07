using System;
using Hierarchy.Exceptions;

namespace Hierarchy
{
    public class Cat : Feline
    {
        private readonly string _breed;

        public Cat(string name, string type, double weight, string livingRegion, string breed)
            : base(name, type, weight, livingRegion)
        {
            _breed = breed;

            if (weight <= 0)
            {
                throw new NegativeWeightException(weight);
            }
            
            if (breed == "")
            {
                throw new CatMustHaveBreedException();
            }
        }
        public override void MakeSound()
        {
            Console.WriteLine("Meowwww");
        }

        public override string ToString()
        {
            return $"{AnimalType} [{AnimalName}, {_breed}, {AnimalWeight}, {LivingRegion}, {FoodEaten}]";
        }
    }
}