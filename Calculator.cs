namespace Web
{
    public class Calculator
    {
        public int Add(int a, int b)
        {
            long result = (long)a + b;
            if (result > int.MaxValue) return int.MaxValue;
            if (result < int.MinValue) return int.MinValue;
            return (int)result;
        }

        public int Subtract(int a, int b)
        {
            long result = (long)a - b;
            if (result > int.MaxValue) return int.MaxValue;
            if (result < int.MinValue) return int.MinValue;
            return (int)result;
        }

        public int Multiply(int a, int b)
        {
            long result = (long)a * b;
            if (result > int.MaxValue) return int.MaxValue;
            if (result < int.MinValue) return int.MinValue;
            return (int)result;
        }
    }
}