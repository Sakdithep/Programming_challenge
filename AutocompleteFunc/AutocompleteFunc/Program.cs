using System;

namespace AutocompleteFunc
{
    public static class AutocompleteHelper
    {
        private enum MatchPriority
        {
            StartsWith = 0,  // คำที่ขึ้นต้นด้วยคำค้นหา - สำคัญที่สุด
            Contains = 1,     // คำที่มีคำค้นหาอยู่ตรงกลาง - สำคัญรองลงมา
            EndsWith = 2      // คำที่ลงท้ายด้วยคำค้นหา - สำคัญน้อยที่สุด
        }
        private class SearchMatch
        {
            public string OriginalValue { get; set; } = null!;
            public string NormalizedValue { get; set; } = null!;
            public MatchPriority Priority { get; set; }
        }
        public static string[] Autocomplete(string search, string[] items, int maxResult)
        {
            if (maxResult <= 0)
            {
                throw new ArgumentOutOfRangeException(
                    nameof(maxResult),
                    "The value of maxResult must be greater than zero.");
            }

            if (string.IsNullOrWhiteSpace(search) || items == null || items.Length == 0)
            {
                return Array.Empty<string>();
            }

            string normalizedSearch = NormalizeString(search);

            var matches = FindMatches(items, normalizedSearch);

            if (matches.Count == 0)
            {
                return Array.Empty<string>();
            }

            var sortedMatches = SortMatches(matches);

            return sortedMatches
                .Take(maxResult)
                .Select(m => m.OriginalValue)
                .ToArray();
        }
        private static string NormalizeString(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                return string.Empty;
            }

            return value.Trim().ToLowerInvariant();
        }
        private static List<SearchMatch> FindMatches(string[] items, string normalizedSearch)
        {
            var matches = new List<SearchMatch>();

            foreach (var item in items)
            {
                if (string.IsNullOrWhiteSpace(item))
                {
                    continue;
                }

                string normalizedItem = NormalizeString(item);

                if (!normalizedItem.Contains(normalizedSearch))
                {
                    continue;
                }

                MatchPriority priority = DeterminePriority(normalizedItem, normalizedSearch);

                matches.Add(new SearchMatch
                {
                    OriginalValue = item.Trim(),
                    NormalizedValue = normalizedItem,
                    Priority = priority
                });
            }

            return matches;
        }
        private static MatchPriority DeterminePriority(string normalizedItem, string normalizedSearch)
        {
            if (normalizedItem.StartsWith(normalizedSearch))
            {
                return MatchPriority.StartsWith;
            }

            if (normalizedItem.EndsWith(normalizedSearch))
            {
                return MatchPriority.EndsWith;
            }

            return MatchPriority.Contains;
        }
        private static List<SearchMatch> SortMatches(List<SearchMatch> matches)
        {
            return matches
                .OrderBy(m => m.Priority)
                .ThenBy(m => m.NormalizedValue)
                .ThenBy(m => m.OriginalValue)
                .ToList();
        }
    }

    public class Program
    {
        static void Main(string[] args)
        {
            string[] items = { "Dog", "DOOR", "dome" };
            string[] result = AutocompleteHelper.Autocomplete("do", items, 3);

            Console.WriteLine("Results:");
            foreach (var r in result)
                Console.WriteLine($"- {r}");
        }
    }
}
