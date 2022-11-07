using System;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FluentAssertions;
using Hierarchy.Exceptions;

namespace Hierarchy.Tests
{
    [TestClass]
    public class MouseTest
    {
        private Mouse _mouse;
        private StringWriter _stringWriter;

        [TestInitialize]
        public void Setup()
        {
            _mouse = new Mouse("Jerry", "Forest", 0.6, "America");
            _stringWriter = new StringWriter();
            Console.SetOut(_stringWriter);
        }

        [TestMethod]
        public void MouseMakeSound()
        {
            
            string expectedSound = "Squeek";

            
            _mouse.MakeSound();

            
            var expectedString = _stringWriter.ToString().Trim();
            expectedSound.Should().Be(expectedString);
        }

        [TestMethod]
        public void MouseWeightShouldNotBeNegativeNumberException()
        {
            Action act = () => new Mouse("Jerry", "Forest", -0.6, "America"); ;
            act.Should().Throw<NegativeWeightException>().
                WithMessage($"Can not input negative or zero weight -0.6");
        }

        [TestMethod]
        public void MouseWeightShouldNotBeNullException()
        {
            Action act = () => new Zebra("Jerry", "Forest", 0, "America"); ;
            act.Should().Throw<NegativeWeightException>().
                WithMessage($"Can not input negative or zero weight 0");
        }

        [TestMethod]
        public void MouseFoodShouldNotBeNegativeException()
        {
            Action act = () => _mouse.Eat(new Vegetable(-2));
            act.Should().Throw<NegativeFoodException>().
                WithMessage($"Can not input negative food");
        }

        [TestMethod]
        public void MouseNotFoodEaten()
        {
            
            _mouse.Eat(new Meat(5));

            
            _mouse.ToString().Should().Be("Forest [Jerry, 0.6, America, 0]");
        }

        [TestMethod]
        public void MouseFoodEaten()
        {
            
            _mouse.Eat(new Vegetable(5));

            
            _mouse.ToString().Should().Be("Forest [Jerry, 0.6, America, 5]");
        }
    }
}