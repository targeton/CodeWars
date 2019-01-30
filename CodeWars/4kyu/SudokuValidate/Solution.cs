using System;
using System.Linq;

namespace CodeWars._4kyu.SudokuValidate
{
    public class Sudoku
    {
        bool _formatFlag = false;
        int[][] _data;
        int _delta;
        public Sudoku(int[][] sudokuData)
        {
            //Constructor here
            int len = sudokuData.Length;
            int n = (int)Math.Sqrt(len);
            if (len != n * n)
                return;
            _delta = n;
            if (sudokuData.Any(s => s.Length != len))
                return;
            _formatFlag = true;
            _data = sudokuData;
        }
        //利用_rowCheck,_colCheck,_gridCheck记录每行、每列和每个片区内的是否有重复值，通过标志位来检测
        public bool IsValid()
        {
            if (!_formatFlag)
                return false;
            int[] rowCheck = new int[_data.Length];
            int[] colCheck = new int[_data.Length];
            int[] gridCheck = new int[_data.Length];
            for (int i = 0; i < _data.Length; i++)
            {
                for (int j = 0; j < _data[i].Length; j++)
                {
                    int n = _data[i][j];
                    if ((rowCheck[i] & (1 << n)) != 0)
                        return false;
                    else
                        rowCheck[i] |= (1 << n);
                    if ((colCheck[j] & (1 << n)) != 0)
                        return false;
                    else
                        colCheck[j] |= (1 << n);
                    int pos = (i / _delta) * _delta + j / _delta;
                    if ((gridCheck[pos] & (1 << n)) != 0)
                        return false;
                    else
                        gridCheck[pos] |= (1 << n);
                }
            }
            return true;
        }
    }
}
