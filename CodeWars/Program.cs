using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace CodeWars
{
    class Program
    {
        static void Main(string[] args)
        {
            //Console.WriteLine(Order(""));
            //Console.WriteLine(Order("is2 Th1is wa4tch 3my"));
            //Console.WriteLine(Order("this website is for losers LOL!"));

            //var result = sqInRect(3, 5);
            //foreach (var item in result)
            //{
            //    Console.WriteLine(item);
            //}

            //int[] arr = new int[] { 2, 3, -1, 5 };
            //Console.WriteLine(arr.Take(0).Sum());
            //Console.WriteLine(arr.Skip(0 + 1).Take(arr.Length - 1).Sum());
            //Stack<int> stack = new Stack<int>();

            string input = "123 45 6";
            Console.WriteLine(input);
            Console.WriteLine(Justify(input, 8));
            Console.WriteLine("*********************");
            string input2 = @"Hello everyone! I'm so happy to come here! Please let'm introduce myself. I'm ZhouPeng, From China, i wish i can make more money and make more friends in the future.";
            Console.WriteLine(input2);
            Console.WriteLine(Justify(input2, 30));
            Console.WriteLine(formatDuration(31537894));
            Console.ReadLine();

        }

        public static int sumTwoSmallestNumbers(int[] numbers)
        {
            return numbers.Where(n => n > 0).OrderBy(n => n).Take(2).Sum();
        }

        public static string Order(string words)
        {
            //return String.Join(" ", words.Split().OrderBy(w => w.SingleOrDefault(char.IsDigit)));


            return System.Threading.Thread.CurrentThread.CurrentCulture.TextInfo.ToTitleCase(words);
        }

        public static string Justify(string str, int length)
        {
            StringBuilder sb = new StringBuilder();
            int index = 0;
            int pos = index + length;
            while (pos < str.Length)
            {
                while (str[pos] != ' ')
                { pos--; }
                string[] temp = str.Substring(index, pos - index).Split(' ');
                int spacesNum = length - temp.Sum(s => s.Length);
                int gapsNum = temp.Length - 1;
                if (gapsNum == 0)
                {
                    for (int i = 0; i < spacesNum; i++)
                        sb.Append(' ');
                    sb.Append(temp[0]);
                }
                else
                {
                    int n = spacesNum / gapsNum;
                    int m = spacesNum % gapsNum;
                    for (int i = 0; i < temp.Length; i++)
                    {
                        sb.Append(temp[i]);
                        if (i == temp.Length - 1)
                            break;
                        for (int j = 0; j < n; j++)
                            sb.Append(' ');
                        if (i < m)
                            sb.Append(' ');
                    }
                }
                sb.Append('\n');
                index = pos + 1;
                pos = index + length;
            }
            if (index < str.Length)
                sb.Append(str.Substring(index, str.Length - index));

            return sb.ToString();
        }

        private static string[] marks = new string[] { "years", "year", "days", "day", "hours", "hour", "minutes", "minute", "seconds", "second" };

        public static string formatDuration(int seconds)
        {
            Dictionary<int, int> times = new Dictionary<int, int>();
            times[0] = seconds / 31536000;
            times[1] = seconds / 86400 % 365;
            times[2] = seconds / 3600 % 24;
            times[3] = seconds / 60 % 60;
            times[4] = seconds % 60;
            var valid = times.Where(t => t.Value > 0).Select(t => t.Key).ToArray();
            int length = valid.Length;
            StringBuilder sb = new StringBuilder();
            if (length == 0)
                sb.Append("now");
            else
            {
                for (int i = 0; i < length; i++)
                {
                    sb.Append(times[valid[i]]);
                    sb.Append(' ');
                    sb.Append(times[valid[i]] > 1 ? marks[2 * valid[i]] : marks[2 * valid[i] + 1]);
                    if (i == length - 2)
                        sb.Append(" and ");
                    else if (i < length - 2)
                        sb.Append(", ");
                }
            }
            return sb.ToString();
        }
    }
}
