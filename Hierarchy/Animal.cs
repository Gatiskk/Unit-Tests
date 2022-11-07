using System;
using System.Collections.Generic;
using System.Text;

namespace Hierarchy
{
    public abstract class Animal
    {
        protected string AnimalName;
        protected string AnimalType;
        protected double AnimalWeight;
        protected int FoodEaten;

        protected Animal(string name, string type, double weight)
        {
            AnimalName = name;
            AnimalType = type;
            AnimalWeight = weight;
        }

        public abstract void MakeSound();

        public virtual void Eat(Food food)
        {
            FoodEaten += food.Quantity;
        }
    }
}