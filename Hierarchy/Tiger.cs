using System;
using System.Collections.Generic;
using System.Text;
using Hierarchy.Exceptions;

namespace Hierarchy
{
    public class Tiger : Feline
    {
        public Tiger(string name, string type, double weight, string livingRegion)
            : base(name, type, weight, livingRegion)
        {
            if (weight <= 0)
            {
                throw new NegativeWeightException(weight);
            }
        }

        public override void MakeSound()
        {
            Console.WriteLine("ROAAR!!!");
        }

        public override void Eat(Food food)
        {
            if (food is Meat)
            {
                this.FoodEaten += food.Quantity;
            }
            else
            {
                Console.WriteLine($"{this.GetType().Name} does not eat that type of food!");
            }
        }
    }
}