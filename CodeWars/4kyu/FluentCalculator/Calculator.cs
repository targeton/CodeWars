using System;
namespace CodeWars._4kyu.FluentCalculator
{
    using System.Collections.Generic;
    using System.Data;
    public class Calculator
    {
        private string str = "";
        public Calculator()
        {
            // Start the magic
        }

        #region 属性
        public Calculator Zero { get { str += "0"; return this; } }
        public Calculator One { get { str += "1"; return this; } }
        public Calculator Two { get { str += "2"; return this; } }
        public Calculator Three { get { str += "3"; return this; } }
        public Calculator Four { get { str += "4"; return this; } }
        public Calculator Five { get { str += "5"; return this; } }
        public Calculator Six { get { str += "6"; return this; } }
        public Calculator Seven { get { str += "7"; return this; } }
        public Calculator Eight { get { str += "8"; return this; } }
        public Calculator Nine { get { str += "9"; return this; } }
        public Calculator Ten { get { str += "10"; return this; } }
        public Calculator Plus { get { str += ".0+"; return this; } }
        public Calculator Minus { get { str += ".0-"; return this; } }
        public Calculator Times { get { str += ".0*"; return this; } }
        public Calculator DividedBy { get { str += ".0/"; return this; } }
        #endregion

        public double Result()
        {
            string r = str += ".0";
            str = "";
            return Convert.ToDouble(new DataTable().Compute(r, null)); ;
        }

        public static implicit operator double(Calculator calc)
        {
            return calc.Result();
        }
    }
}
