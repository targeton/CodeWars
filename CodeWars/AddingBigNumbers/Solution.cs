using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Numerics;

namespace CodeWars.AddingBigNumbers
{
    public class Solution
    {
        public static string Add(string a, string b)
        {
            //int splitLength = 8;
            //string[] a_Array = GetSplit(a, splitLength);
            //string[] b_Array = GetSplit(b, splitLength);
            //int flag = 0;
            //int aLength = a_Array.Length;
            //int bLength = b_Array.Length;
            //StringBuilder sb = new StringBuilder();
            //for (int i = 0; i < (aLength < bLength ? aLength : bLength); i++)
            //{
            //    var result = (int.Parse(a_Array[i]) + int.Parse(b_Array[i]) + flag).ToString();
            //    flag = result.Length > splitLength ? 1 : 0;
            //    sb.Insert(0, result.Substring(result.Length - splitLength > 0 ? result.Length - splitLength : 0, result.Length >= splitLength ? splitLength : result.Length));
            //}
            //string[] more = (aLength > bLength ? a_Array : b_Array).Skip(aLength < bLength ? aLength : bLength).Take(Math.Abs(bLength - aLength)).ToArray();
            //for (int i = 0; i < more.Length; i++)
            //{
            //    var result = (int.Parse(more[i]) + flag).ToString();
            //    flag = result.Length > splitLength ? 1 : 0;
            //    sb.Insert(0, result.Substring(result.Length - splitLength > 0 ? result.Length - splitLength : 0, result.Length >= splitLength ? splitLength : result.Length));
            //}
            //if (flag == 1)
            //    sb.Insert(0, "1");
            //return sb.ToString();
            return (BigInteger.Parse(a) + BigInteger.Parse(b)).ToString();
        }

        private static string[] GetSplit(string input, int splitLength)
        {
            int inputLength = input.Length;
            string[] output = new string[inputLength / splitLength + (inputLength % splitLength == 0 ? 0 : 1)];
            for (int i = 0; i < output.Length; i++)
            {
                output[i] = input.Substring((i + 1) * splitLength > inputLength ? 0 : inputLength - (i + 1) * splitLength, (i == output.Length - 1 ? inputLength - i * splitLength : splitLength));
            }
            return output;
        }


    }
}
