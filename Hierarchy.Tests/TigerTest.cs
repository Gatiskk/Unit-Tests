using System;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FluentAssertions;
using Hierarchy.Exceptions;

namespace Hierarchy.Tests
{
    [TestClass]
    public class TigerTest
    {
        private Tiger _tiger;
        private StringWriter _stringWriter;

        [TestInitialize]
        public void Setup()
        {
            _tiger = new Tiger("Jumbo", "Bengal", 200, "India");
            _stringWriter = new StringWriter();
            Console.SetOut(_stringWriter);
        }

        [TestMethod]
        public void TigerMakeSound()
        {
            
            string expectedSound = "ROAAR!!!";

           
            _tiger.MakeSound();

            
            var expectedString = _stringWriter.ToString().Trim();
            expectedSound.Should().Be(expectedString);
        }

        [TestMethod]
        public void TigerWeightShouldNotBeNegativeNumberException()
        {
            Action act = () => new Mouse("Jumbo", "Bengal", -200, "India");
            act.Should().Throw<NegativeWeightException>().
                WithMessage($"Can not input negative or zero weight -200");
        }

        [TestMethod]
        public void TigerWeightShouldNotBeNullException()
        {
            Action act = () => new Zebra("Jumbo", "Bengal", 0, "India");
            act.Should().Throw<NegativeWeightException>().
                WithMessage($"Can not input negative or zero weight 0");
        }

        [TestMethod]
        public void TigerFoodShouldNotBeNegativeException()
        {
            Action act = () => _tiger.Eat(new Vegetable(-2));
            act.Should().Throw<NegativeFoodException>().
                WithMessage($"Can not input negative food");
        }

        [TestMethod]
        public void TigerNotFoodEaten()
        {
            
            _tiger.Eat(new Meat(5));

            
            _tiger.ToString().Should().Be("Bengal [Jumbo, 200, India, 5]");
        }

        [TestMethod]
        public void TigerFoodEaten()
        {
            
            _tiger.Eat(new Vegetable(5));

            
            _tiger.ToString().Should().Be("Bengal [Jumbo, 200, India, 0]");
        }
    }
}