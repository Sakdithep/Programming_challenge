using System;
using System.Text;
using System.Collections.Generic;

namespace NaturalSort
{
    public static class NaturalSorter
    {
        public static int NaturalCompare(string a, string b)
        {
            if (a == null && b == null) return 0;
            if (a == null) return -1;
            if (b == null) return 1;

            int ia = 0, ib = 0;

            while (ia < a.Length && ib < b.Length)
            {
                string chunkA = GetChunk(a, ref ia);
                string chunkB = GetChunk(b, ref ib);

                if (string.IsNullOrEmpty(chunkA) || string.IsNullOrEmpty(chunkB))
                    continue;

                bool numericA = char.IsDigit(chunkA[0]);
                bool numericB = char.IsDigit(chunkB[0]);

                if (numericA && numericB)
                {
                    if (!int.TryParse(chunkA, out int numA)) numA = 0;
                    if (!int.TryParse(chunkB, out int numB)) numB = 0;
                    int diff = numA.CompareTo(numB);
                    if (diff != 0) return diff;
                }
                else
                {
                    int diff = string.Compare(chunkA, chunkB, StringComparison.OrdinalIgnoreCase);
                    if (diff != 0) return diff;
                }
            }

            return a.Length.CompareTo(b.Length);
        }

        internal static string GetChunk(string s, ref int index)
        {
            if (index >= s.Length) return "";
            var sb = new StringBuilder();
            bool digit = char.IsDigit(s[index]);
            while (index < s.Length && char.IsDigit(s[index]) == digit)
                sb.Append(s[index++]);
            return sb.ToString();
        }

        public static string[] NaturalSort(string[] input)
        {
            if (input == null || input.Length == 0)
                return Array.Empty<string>();

            var list = new List<string>(input);
            list.Sort(NaturalCompare);
            return list.ToArray();
        }
    }

    public class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Enter items separated by comma:");
            string input = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(input))
            {
                Console.WriteLine("No input provided!");
                return;
            }

            string[] items = input.Split(',', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
            if (items.Length == 0)
            {
                Console.WriteLine("No valid items found!");
                return;
            }

            string[] sorted = NaturalSorter.NaturalSort(items);

            Console.WriteLine("\n Sorted Result:");
            foreach (var item in sorted)
                Console.WriteLine(item);
        }
    }
}
