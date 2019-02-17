using System;
using System.Collections.Generic;
using System.Linq;

namespace CodeWars._3kyu.AlphabeticAnagrams
{
    public class Solution
    {
        public static long ListPosition(string value)
        {
            long rank = 1;
            long suffixPermCount = 1;

            var charCounts = new Dictionary<char, int>();

            for (var i = value.Length - 1; i > -1; i--)
            {
                var current = value.ElementAt(i);
                var currentCount = charCounts.ContainsKey(current) ? charCounts[current] + 1 : 1;

                if (charCounts.ContainsKey(current))
                {
                    charCounts[current] = currentCount;
                }
                else
                {
                    charCounts.Add(current, currentCount);
                }
                rank += charCounts.Where(charCount => charCount.Key < current).Sum(charCount => suffixPermCount * charCount.Value / currentCount);
                suffixPermCount *= value.Length - i;
                suffixPermCount /= currentCount;
            }
            return rank;
        }

    }
}
