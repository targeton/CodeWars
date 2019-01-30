using System;
using System.Linq;
using System.Collections.Generic;

namespace CodeWars._4kyu.SudokuValidate
{
    public class Sudoku
    {
        int[][] _data;

        public Sudoku(int[][] sudokuData)
        {
            //Constructor here
            _data = sudokuData;
        }

        public bool IsValid()
        {
            int n = _data.Length;
            int m = (int)Math.Sqrt(n);
            int e = m * m;
            int sum = (e * e + e) / 2;
            return Enumerable.Range(0, n)
                .SelectMany(i => new[] 
                { 
                    _data[i].Sum(),
                    _data.Sum(d => d[i]),
                    _data.Skip(m*(i/m)).Take(m).SelectMany(r=>r.Skip(m*(i%m)).Take(m)).Sum()
                }).All(i => i == sum);
        }
    }
}
