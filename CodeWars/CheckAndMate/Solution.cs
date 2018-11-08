using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CodeWars.CheckAndMate
{
    public enum FigureType { Pawn, King, Queen, Rook, Knight, Bishop }

    //struct to make it convenient to work with cells
    public struct Pos
    {
        public readonly sbyte X;
        public readonly sbyte Y;

        public Pos(sbyte y, sbyte x)
        {
            X = x;
            Y = y;
        }
        public Pos(int y, int x)
        {
            X = (sbyte)x;
            Y = (sbyte)y;
        }

        public override bool Equals(object obj)
        {
            var pos = (Pos)obj;
            return pos.X == this.X && pos.Y == this.Y;
        }
        public override int GetHashCode()
        {
            return this.GetHashCode();
        }
    }

    public class Figure
    {
        private FigureType _type;
        public FigureType Type { get { return _type; } }

        private byte _owner;
        public byte Owner { get { return _owner; } }

        private Pos _cell;
        public Pos Cell { get { return _cell; } set { _cell = value; } }

        private Pos? _prevCell;
        public Pos? PrevCell { get { return _prevCell; } }

        public Figure(FigureType type, byte owner, Pos cell, Pos? prevCell = null)
        {
            _type = type;
            _owner = owner;
            _cell = cell;
            _prevCell = prevCell;
        }
    }

    public class Figures
    {
        public static void OutputBoard(Figure[] figures)
        {
            //TODO:展现棋盘布局
        }
    }

    public class Solution
    {
        // Returns an array of threats if the arrangement of 
        // the pieces is a check, otherwise null
        public static List<Figure> isCheck(IList<Figure> pieces, int player)
        {
            var result = new List<Figure>();
            var kingFigure = pieces.FirstOrDefault(f => f.Owner == player && f.Type == FigureType.King);
            if (kingFigure == null)
                return result;
            var otherFigures = pieces.Where(f => f.Owner != player);
            foreach (var f in otherFigures)
            {
                switch (f.Type)
                {
                    case FigureType.Pawn:
                        if (Math.Abs(f.Cell.X - kingFigure.Cell.X) == 1)
                        {
                            if (player == 0 && f.Cell.Y - kingFigure.Cell.Y == -1)
                                result.Add(f);
                            else if (player == 1 && f.Cell.Y - kingFigure.Cell.Y == 1)
                                result.Add(f);
                        }
                        break;
                    case FigureType.King:

                        break;
                    case FigureType.Queen:
                        if (f.Cell.X == kingFigure.Cell.X && pieces.Where(p => p.Cell.X == f.Cell.X && (f.Cell.Y - p.Cell.Y) * (kingFigure.Cell.Y - p.Cell.Y) < 0).Count() <= 0)
                            result.Add(f);
                        else if (f.Cell.Y == kingFigure.Cell.Y && pieces.Where(p => p.Cell.Y == f.Cell.Y && (f.Cell.X - p.Cell.X) * (kingFigure.Cell.X - p.Cell.X) < 0).Count() <= 0)
                            result.Add(f);
                        else if (Math.Abs(f.Cell.X - kingFigure.Cell.X) == Math.Abs(f.Cell.Y - kingFigure.Cell.Y) && pieces.Where(p => Math.Abs(p.Cell.X - f.Cell.X) == Math.Abs(p.Cell.Y - f.Cell.Y) && (p.Cell.X - f.Cell.X) * (p.Cell.X - kingFigure.Cell.X) < 0).Count() <= 0)
                            result.Add(f);
                        break;
                    case FigureType.Rook:
                        if (f.Cell.X == kingFigure.Cell.X && pieces.Where(p => p.Cell.X == f.Cell.X && (f.Cell.Y - p.Cell.Y) * (kingFigure.Cell.Y - p.Cell.Y) < 0).Count() <= 0)
                            result.Add(f);
                        else if (f.Cell.Y == kingFigure.Cell.Y && pieces.Where(p => p.Cell.Y == f.Cell.Y && (f.Cell.X - p.Cell.X) * (kingFigure.Cell.X - p.Cell.X) < 0).Count() <= 0)
                            result.Add(f);
                        break;
                    case FigureType.Knight:
                        if ((Math.Abs(f.Cell.X - kingFigure.Cell.X) == 1 && Math.Abs(f.Cell.Y - kingFigure.Cell.Y) == 2) || (Math.Abs(f.Cell.X - kingFigure.Cell.X) == 2 && Math.Abs(f.Cell.Y - kingFigure.Cell.Y) == 1))
                            result.Add(f);
                        break;
                    case FigureType.Bishop:
                        if (Math.Abs(f.Cell.X - kingFigure.Cell.X) == Math.Abs(f.Cell.Y - kingFigure.Cell.Y) && pieces.Where(p => Math.Abs(p.Cell.X - f.Cell.X) == Math.Abs(p.Cell.Y - f.Cell.Y) && (p.Cell.X - f.Cell.X) * (p.Cell.X - kingFigure.Cell.X) < 0).Count() <= 0)
                            result.Add(f);
                        break;
                    default:
                        break;
                }
            }
            return result;
        }

        private static sbyte[] xShift = new sbyte[] { -1, -1, -1, 0, 1, 1, 1, 0 };
        private static sbyte[] yShift = new sbyte[] { -1, 0, 1, 1, 1, 0, -1, -1 };
        // Returns true if the arrangement of the
        // pieces is a check mate, otherwise false
        public static bool isMate(IList<Figure> pieces, int player)
        {
            var kingFigure = pieces.FirstOrDefault(f => f.Owner == player && f.Type == FigureType.King);
            if (kingFigure == null)
                return false;
            var result = isCheck(pieces, player);
            if (result.Count <= 0)
                return false;
            //王被将军，通过移动本方棋子，测试能否摆脱被将军
            var figures = pieces.Where(p => p.Owner == player);
            foreach (var item in figures)
            {
                switch (item.Type)
                {
                    case FigureType.Pawn:
                        {
                            //移动兵（有特殊情况）
                            var readyPos = new List<Pos>();
                        }
                        break;
                    case FigureType.King:
                        {
                            //移动王
                            var readyPos = new List<Pos>();
                            for (int i = 0; i < 8; i++)
                            {
                                sbyte newX = (sbyte)(kingFigure.Cell.X + xShift[i]);
                                sbyte newY = (sbyte)(kingFigure.Cell.Y + yShift[i]);
                                if (newX < 0 || newX > 7 || newY < 0 || newY > 7)
                                    continue;
                                var f = pieces.FirstOrDefault(p => p.Cell.X == newX && p.Cell.Y == newY);
                                if (f == null)
                                    readyPos.Add(new Pos(newY, newX));
                            }

                            foreach (var pos in readyPos)
                            {
                                kingFigure.Cell = pos;
                                var r = isCheck(pieces, player);
                                if (r.Count <= 0)
                                    return false;
                            }
                        }
                        break;
                    case FigureType.Queen:
                        {
                            //移动皇后
                        }
                        break;
                    case FigureType.Rook:
                        {
                            //移动车
                        }
                        break;
                    case FigureType.Knight:
                        {
                            //移动马

                        }
                        break;
                    case FigureType.Bishop:
                        {
                            //移动象

                        }
                        break;
                    default:
                        break;
                }
            }
            
            return true;
        }
    }
}
