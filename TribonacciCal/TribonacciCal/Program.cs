namespace TribonacciCal
{
    public class Tribonacci
    {
        public static int[] Calculate(int[] items, int n)
        {
            if (n <= 0)
            {
                return new int[0];
            }

            var result = new List<int>();

            if (items == null)
            {
                items = new int[0];
            }

            var startValues = new List<int>(items);
            while (startValues.Count < 3)
            {
                startValues.Insert(0, 0);
            }

            if (n <= items.Length)
            {
                return items.Take(n).ToArray();
            }

            result.AddRange(items);

            for (int i = items.Length; i < n; i++)
            {
                int nextValue = startValues[0] + startValues[1] + startValues[2];
                result.Add(nextValue);

                startValues[0] = startValues[1];
                startValues[1] = startValues[2];
                startValues[2] = nextValue;
            }

            return result.ToArray();
        }
    }
    public class Program
    {
        static void Main(string[] args)
        {
            var result1 = Tribonacci.Calculate(new int[] { 1, 3, 5 }, 5);
            Console.WriteLine($"Output: [{string.Join(", ", result1)}]");

            var result2 = Tribonacci.Calculate(new int[] { 2, 2, 2 }, 3);
            Console.WriteLine($"Output: [{string.Join(", ", result2)}]");

            var result3 = Tribonacci.Calculate(new int[] { 10, 10, 10 }, 4);
            Console.WriteLine($"Output: [{string.Join(", ", result3)}]");
        }
    }
}
