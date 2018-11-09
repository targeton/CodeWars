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
        //Pawn, King, Queen, Rook, Knight, Bishop
        // the possible distances from target to attack
        private static int[][][] _attackTable = new int[6][][] 
        {
            new int[2][] { new int[]{ -17 }, new int[]{ -15 } },
            new int[8][] { new int[]{ -17 }, new int[]{ -16 }, new int[]{ -15 }, new int[]{ 1 }, new int[]{ 17 }, new int[]{ 16 }, new int[]{ 15 }, new int[]{ -1 } }, 
            new int[8][] { new int[]{ -119, -102, -85, -68, -51, -34, -17 }, new int[]{ -112, -96, -80, -64, -48, -32, -16 }, new int[]{ -105, -80, -75, -60, -45, -30, -15 }, new int[]{ 7, 6, 5, 4, 3, 2, 1 }, new int[]{ 119, 102, 85, 68, 51, 34, 17}, new int[]{ 112, 96, 80, 64, 48, 32, 16, }, new int[]{ 105, 80, 75, 60, 45, 30, 15}, new int[]{ -7, -6, -5, -4, -3, -2, -1} },
            new int[4][] { new int[]{ -112, -96, -80, -64, -48, -32, -16 }, new int[]{ 7, 6, 5, 4, 3, 2, 1 }, new int[]{ 112, 96, 80, 64, 48, 32, 16, }, new int[]{ -7, -6, -5, -4, -3, -2, -1} },
            new int[8][] { new int[]{ -33 }, new int[]{ -31 }, new int[]{ -14 }, new int[]{ 18 }, new int[]{ 33 }, new int[]{ 31 } , new int[]{ 14 }, new int[]{ -18 }},
            new int[4][] { new int[]{ -119, -102, -85, -68, -51, -34, -17 }, new int[]{ -105, -80, -75, -60, -45, -30, -15 }, new int[]{ 119, 102, 85, 68, 51, 34, 17}, new int[]{ 105, 80, 75, 60, 45, 30, 15} }
        };

        // Returns an array of threats if the arrangement of 
        // the pieces is a check, otherwise null
        public static List<Figure> isCheck(IList<Figure> pieces, int player)
        {
            var result = new List<Figure>();
            var kingFigure = pieces.FirstOrDefault(f => f.Owner == player && f.Type == FigureType.King);
            if (kingFigure == null)
                return result;
            var otherFigures = pieces.Where(f => f.Owner != player);
            int flag = player == 0 ? -1 : 1;
            int kingPos = GetPos(kingFigure);
            int[] distances = pieces.Select(p => (kingPos - GetPos(p)) * flag).ToArray();
            foreach (var f in otherFigures)
            {
                int[] gaps = GetAttackLine(f, kingPos, flag);
                if (gaps == null)
                    continue;
                else if (gaps.Length <= 1)
                    result.Add(f);
                else
                {
                    int index = 1;
                    for (index = 1; index < gaps.Length; index++)
                    {
                        if (distances.Contains(gaps[index]))
                            break;
                    }
                    if (index == gaps.Length)
                        result.Add(f);
                }
            }
            return result;
        }

        private static int GetPos(Figure f)
        {
            return f.Cell.Y * 16 + f.Cell.X;
        }

        private static Pos Resolve(int pos)
        {
            return new Pos(pos / 16, pos % 16);
        }

        private static int[] GetAttackLine(Figure f, int kingPos, int flag)
        {
            int pos = (kingPos - GetPos(f)) * flag;
            int[][] table = _attackTable[(int)f.Type];
            int[] gaps = null;
            bool loopFlag = true;
            for (int i = 0; i < table.Length && loopFlag; i++)
            {
                for (int j = 0; j < table[i].Length && loopFlag; j++)
                {
                    if (table[i][j] == pos)
                    {
                        gaps = table[i].Skip(j).Take(table[i].Length - j).ToArray();
                        loopFlag = false;
                    }
                }
            }
            return gaps;
        }
        //从左上开始，以顺时针旋转，跳到相邻八个格子的偏移量
        private static int[] _delta = new int[] { -17, -16, -15, 1, 17, 16, 15, -1 };
        private static int[] _queenDirection = new int[] { 0, 1, 2, 3, 4, 5, 6, 7 };
        private static int[] _rookDirection = new int[] { 1, 3, 5, 7 };
        private static int[] _bishopDirection = new int[] { 0, 2, 4, 6, };

        // Returns true if the arrangement of the
        // pieces is a check mate, otherwise false
        public static bool isMate(IList<Figure> pieces, int player)
        {
            var kingFigure = pieces.FirstOrDefault(f => f.Owner == player && f.Type == FigureType.King);
            if (kingFigure == null)
                return false;
            var result = isCheck(pieces, player);
            if (result == null || result.Count <= 0)
                return false;
            var friendPieces = pieces.Where(p => p.Owner == player);
            var friendPos = friendPieces.Select(p => GetPos(p));
            var allPos = pieces.Select(p => GetPos(p));
            var otherPos = allPos.Except(friendPos);

            foreach (var f in friendPieces)
            {
                int fPos = GetPos(f);
                switch (f.Type)
                {
                    case FigureType.Pawn:
                        {
                            //前进一步；初始位置可以前进两步；斜前方有子可以斜吃；水平方向有对方兵，可以吃过路兵
                            var canMove = new List<int>();
                            //前进一步或前进两步
                            for (int i = 1; i < 3; i++)
                            {
                                if (i == 2 && f.Cell.Y != (player == 0 ? 6 : 1))
                                    break;
                                var move = fPos + (player == 0 ? -16 : 16) * i;
                                if ((move & 0x88) != 0 || allPos.Contains(move))
                                    break;
                                canMove.Add(move);
                            }
                            //斜前方是否有子(斜前方有没有对方棋子)
                            var delta = _attackTable[(int)f.Type].SelectMany(a => a);
                            foreach (var item in delta)
                            {
                                var move = fPos + item * (player == 0 ? 1 : -1);
                                if (otherPos.Contains(move))
                                    canMove.Add(move);
                            }
                            //遍历前进或斜前方吃子的位置集合
                            foreach (var m in canMove)
                            {
                                var newPieces = CreateNewPieces(pieces, f, m);
                                var checkResult = isCheck(newPieces, player);
                                if (checkResult == null || checkResult.Count <= 0)
                                    return false;
                            }
                            //过路兵
                            var passent = pieces.Where(p => p.Cell.Y == f.Cell.Y && p.Owner != player && p.Type == FigureType.Pawn && p.Cell.Y == (player == 0 ? 3 : 4) && p.PrevCell.HasValue && p.PrevCell.Value.Y == (player == 0 ? 1 : 6));
                            foreach (var item in passent)
                            {
                                var newPieces = pieces.Select(p => new Figure(p.Type, p.Owner, p.Cell)).ToList();
                                var itemPiece = newPieces.First(p => p.Cell.X == item.Cell.X && p.Cell.Y == item.Cell.Y);
                                newPieces.Remove(itemPiece);
                                var old = newPieces.First(p => p.Cell.X == f.Cell.X && p.Cell.Y == f.Cell.Y);
                                var newPos = Resolve(GetPos(item) + (player == 0 ? (item.Cell.X - f.Cell.X > 0 ? -15 : -17) : (item.Cell.X - f.Cell.X > 0 ? 17 : 15)));
                                old.Cell = newPos;
                                var checkResult = isCheck(newPieces, player);
                                if (checkResult == null || checkResult.Count <= 0)
                                    return false;
                            }
                        }
                        break;
                    case FigureType.King:
                    case FigureType.Knight:
                        {
                            var move = _attackTable[(int)f.Type].SelectMany(a => a).Select(a => fPos + a).Where(a => (a & 0x88) == 0);
                            foreach (var m in move)
                            {
                                if (friendPos.Contains(m))
                                    continue;
                                var newPieces = CreateNewPieces(pieces, f, m);
                                var checkResult = isCheck(newPieces, player);
                                if (checkResult == null || checkResult.Count <= 0)
                                    return false;
                            }
                        }
                        break;
                    case FigureType.Queen:
                    case FigureType.Rook:
                    case FigureType.Bishop:
                        {
                            int[] direction = f.Type == FigureType.Queen ? _queenDirection : (f.Type == FigureType.Rook ? _rookDirection : _bishopDirection);
                            for (int j = 0; j < direction.Length; j++)
                                for (int i = 1; i < 8; i++)
                                {
                                    var move = fPos + _delta[direction[j]] * i;
                                    if ((move & 0x88) != 0)
                                        break;
                                    if (friendPos.Contains(move))
                                        break;
                                    var newPieces = CreateNewPieces(pieces, f, move);
                                    var checkResult = isCheck(newPieces, player);
                                    if (checkResult == null || checkResult.Count <= 0)
                                        return false;
                                    if (newPieces.Count < pieces.Count)
                                        break;
                                }
                        }
                        break;
                    default:
                        break;
                }
            }
            return true;
        }

        private static List<Figure> CreateNewPieces(IList<Figure> pieces, Figure friend, int pos)
        {
            var newPieces = pieces.Select(p => new Figure(p.Type, p.Owner, p.Cell)).ToList();
            var f = newPieces.First(p => p.Cell.X == friend.Cell.X && p.Cell.Y == friend.Cell.Y);
            var newPos = Resolve(pos);
            var newP = newPieces.FirstOrDefault(p => p.Cell.Equals(newPos));
            if (newP != null)
                newPieces.Remove(newP);
            f.Cell = newPos;
            return newPieces;
        }

    }
}
