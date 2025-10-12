using System;
using System.Collections.Generic;

namespace BalancedParentheses
{
    public static class ParenthesesValidator
    {
        public static bool IsBalanced(string input)
        {
            if (input == null || input == string.Empty)
                return false;

            Stack<char> stack = new Stack<char>();

            foreach (char c in input)
            {
                switch (c)
                {
                    case '(':
                    case '{':
                    case '[':
                        stack.Push(c);
                        break;

                    case ')':
                        if (stack.Count == 0 || stack.Pop() != '(') return false;
                        break;

                    case '}':
                        if (stack.Count == 0 || stack.Pop() != '{') return false;
                        break;

                    case ']':
                        if (stack.Count == 0 || stack.Pop() != '[') return false;
                        break;

                    default:
                        return false;
                }
            }

            return stack.Count == 0;
        }
    }

    public class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                Console.Write("Input value: ");
                string input = Console.ReadLine();

                if (string.IsNullOrEmpty(input))
                    break;

                bool result = ParenthesesValidator.IsBalanced(input);
                Console.WriteLine(result);

                Console.WriteLine("Press any key to exit program or Enter to continue...");
                var key = Console.ReadKey(intercept: true);

                if (key.Key != ConsoleKey.Enter)
                    break;

                Console.WriteLine();
            }

            Console.WriteLine("\nProgram exited.");
        }
    }
}
