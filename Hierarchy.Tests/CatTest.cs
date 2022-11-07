using System;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FluentAssertions;
using Hierarchy.Exceptions;

namespace Hierarchy.Tests
{
    [TestClass]
    public class CatTest
    {
        private Cat _cat;
        private StringWriter _stringWriter;

        [TestInitialize]
        public void Setup()
        {
            _cat = new Cat("Pablo", "Persian", 4, "Europe","Domestic");
            _stringWriter = new StringWriter();
            Console.SetOut(_stringWriter);
        }

        [TestMethod]
        public void CatMakeSound()
        {
            
            string expectedSound = "Meowwww";

           
            _cat.MakeSound();

            
            var expectedString = _stringWriter.ToString().Trim();
            expectedSound.Should().Be(expectedString);
        }

        [TestMethod]
        public void CatWeightShouldNotBeNegativeNumber()
        {
            Action act = () => new Cat("Pablo", "Persian", -55, "Europe", "Domestic");
            act.Should().Throw<NegativeWeightException>().
                WithMessage($"Can not input negative or zero weight -55");
        }

        [TestMethod]
        public void CatWeightShouldNotBeNull()
        {
            Action act = () => new Cat("Pablo", "Persian", 0, "Europe", "Domestic");
            act.Should().Throw<NegativeWeightException>().
                WithMessage($"Can not input negative or zero weight 0");
        }

        [TestMethod]
        public void CatFoodShouldNotBeNegative()
        {
            Action act = () => _cat.Eat(new Vegetable(-2));
            act.Should().Throw<NegativeFoodException>().
                WithMessage($"Can not input negative food");
        }

        [TestMethod]
        public void CatFoodNotEaten()
        {
            
            _cat.Eat(new Meat(5));

            
            _cat.ToString().Should().Be("Persian [Pablo, Domestic, 4, Europe, 5]");
        }

        [TestMethod]
        public void CatFoodEaten()
        {
            
            _cat.Eat(new Vegetable(4));

            
            _cat.ToString().Should().Be("Persian [Pablo, Domestic, 4, Europe, 4]");
        }

        [TestMethod]
        public void CatMustHaveBreed()
        {
            Action act = () => new Cat("Pablo", "Persian", 4, "Europe", "");
            act.Should().Throw<CatMustHaveBreedException>().
                WithMessage($"Cat must have breed");
        }
    }
}