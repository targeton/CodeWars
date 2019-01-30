using System;
using System.Numerics;
using System.Text;
using System.Text.RegularExpressions;

namespace CodeWars.BinomalExpansion
{
    public class Solution
    {
        public static string Expand(string expr)
        {
            //Regex.IsMatch(expr, @"([0-9");
            string[] str = expr.Split('^');
            int factor = int.Parse(str[1]);
            if (factor == 0)
                return "1";
            string expression = str[0].TrimStart('(').TrimEnd(')');
            char flag = 'x';
            for (int i = 0; i < expression.Length; i++)
            {
                if (expression[i] >= 'a' && expression[i] <= 'z')
                {
                    flag = expression[i];
                    break;
                }
            }
            string[] nums = expression.Split(flag);
            int a = string.IsNullOrEmpty(nums[0]) ? 1 : nums[0] == "-" ? -1 : int.Parse(nums[0]);
            int b = int.Parse(nums[1]);
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i <= factor; i++)
            {
                long num = (long)Math.Pow(a, factor - i) * (long)Math.Pow(b, i) * GetCombinationNum(factor, i);
                if (num == 0) continue;
                string output = factor - i == 0 ? num.ToString() : factor - i == 1 ? string.Format("{0}{1}", (num == 1 || num == -1) ? (num == -1 ? "-" : "") : num.ToString(), flag) : string.Format("{0}{1}^{2}", (num == 1 || num == -1) ? (num == -1 ? "-" : "") : num.ToString(), flag, factor - i);
                if (i == 0 || num < 0)
                    sb.Append(output);
                else
                    sb.Append(string.Format("+{0}", output));
            }
            return sb.ToString();
        }

        //递推：C(n,m) = C(n-1,m-1) + C(n-1,m)
        private static long GetCombinationNum(long n, long m)
        {
            if (m > n)
                throw new ArgumentException("传入参数异常！");
            if (m == 0 || m == n)
                return 1;
            else if (m == 1 || m == n - 1)
                return n;
            else
                return GetCombinationNum(n - 1, m - 1) + GetCombinationNum(n - 1, m);
        }
    }
}
