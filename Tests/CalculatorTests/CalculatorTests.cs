using Xunit;
using Web;

namespace Web.Tests
{
    public class CalculatorTests
    {
        [Fact]
        public void Add_ReturnsSum()
        {
            var calc = new Calculator();
            Assert.Equal(5, calc.Add(2, 3));
        }

        [Fact]
        public void Add_Overflow_ReturnsIntMax()
        {
            var calc = new Calculator();
            Assert.Equal(int.MaxValue, calc.Add(int.MaxValue, 1));
        }

        [Fact]
        public void Subtract_ReturnsDifference()
        {
            var calc = new Calculator();
            Assert.Equal(1, calc.Subtract(3, 2));
        }

        [Fact]
        public void Subtract_Underflow_ReturnsIntMin()
        {
            var calc = new Calculator();
            Assert.Equal(int.MinValue, calc.Subtract(int.MinValue, 1));
        }

        [Fact]
        public void Multiply_ReturnsProduct()
        {
            var calc = new Calculator();
            Assert.Equal(6, calc.Multiply(2, 3));
        }

        [Fact]
        public void Multiply_Overflow_ReturnsIntMax()
        {
            var calc = new Calculator();
            Assert.Equal(int.MaxValue, calc.Multiply(int.MaxValue, 2));
        }

        [Fact]
        public void Multiply_NegativeOverflow_ReturnsIntMin()
        {
            var calc = new Calculator();
            Assert.Equal(int.MinValue, calc.Multiply(int.MinValue, 2));
        }
    }
}
