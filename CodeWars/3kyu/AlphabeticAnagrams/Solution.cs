using System;
using System.Collections.Generic;
using System.Linq;

namespace CodeWars._3kyu.AlphabeticAnagrams
{
    public class Solution
    {
        public static long ListPosition(string value)
        {
            if (value.Length <= 1)
                return 1;
            long result = 0;
            char[] temp = value.ToCharArray();
            Dictionary<char, int> dic = temp.Distinct().ToDictionary(c => c, c => temp.Count(a => a == c));
            char[] arr = temp.Distinct().OrderBy(t => t).ToArray();
            int length = value.Length;
            var more = temp.Where(t => t < temp[0]).Distinct();
            if (more != null && more.Count() > 0)
            {
                foreach (var item in more)
                {
                    var tt = fact(length - 1);
                    foreach (var key in dic.Keys)
                    {
                        if (key == item)
                            tt = tt / fact(dic[key] - 1);
                        else
                            tt = tt / fact(dic[key]);
                    }
                    result += tt;
                }
            }

            result += ListPosition(value.Substring(1, value.Length - 1));
            return result;
        }

        public static long fact(long n)
        {
            if (n <= 1)
                return 1;
            else
                return n * fact(n - 1);
        }
    }
}
