using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CodeWars.BattleshipField
{
    public class Component
    {
        public int X { get; set; }
        public int Y { get; set; }
        public int Flag { get; set; }
    }
    public class Solution
    {
        public static List<Component> _linkedList = new List<Component>();
        public static int _flagIndex = 0;
        public static int[] _xDelta = new int[] { -1, 0, 1, 1, 1, 0, -1, -1 };
        public static int[] _yDelta = new int[] { -1, -1, -1, 0, 1, 1, 1, 0 };

        public static bool ValidateBattlefield(int[,] field)
        {
            if (field == null)
                return false;
            //检测矩阵中有一个四格直线、两个三格直线、三个两格直线、四个一格（不能相邻、不能相交）
            for (int i = 0; i < 10; i++)
                for (int j = 0; j < 10; j++)
                {
                    if (field[i, j] == -100)
                        continue;
                    else if (field[i, j] == 0)
                    {
                        field[i, j] = -100;
                        continue;
                    }
                    else
                    {
                        field[i, j] = -100;
                        var stack = new Stack<Component>();
                        var component = new Component() { X = i, Y = j, Flag = _flagIndex };
                        _linkedList.Add(component);
                        stack.Push(component);
                        while (stack.Count > 0)
                        {
                            var c = stack.Pop();
                            for (int k = 0; k < 8; k++)
                            {
                                int xPos = c.X + _xDelta[k];
                                int yPos = c.Y + _yDelta[k];
                                if (xPos < 0 || xPos > 9 || yPos < 0 || yPos > 9)
                                    continue;
                                else if (field[xPos, yPos] == -100)
                                    continue;
                                else if (field[xPos, yPos] == 0)
                                {
                                    field[xPos, yPos] = -100;
                                    continue;
                                }
                                else
                                {
                                    field[xPos, yPos] = -100;
                                    var nearComponent = new Component() { X = xPos, Y = yPos, Flag = _flagIndex };
                                    _linkedList.Add(nearComponent);
                                    stack.Push(nearComponent);
                                }
                            }
                        }
                        _flagIndex++;
                    }
                }
            var group = _linkedList.GroupBy(c => c.Flag);
            if (group.Count() != 10)
                return false;
            int battleshipNum = 0, cruiserNum = 0, destroyerNum = 0, submarineNum = 0;
            foreach (var item in group)
            {
                if (item.Count() == 1)
                    submarineNum++;
                else if (item.Count() > 1)
                {
                    var c = item.First();
                    var sameX = item.All(p => p.X == c.X);
                    var sameY = item.All(p => p.Y == c.Y);
                    if (sameX || sameY)
                    {
                        switch (item.Count())
                        {
                            case 2:
                                destroyerNum++;
                                break;
                            case 3:
                                cruiserNum++;
                                break;
                            case 4:
                                battleshipNum++;
                                break;
                            default:
                                break;
                        }
                    }
                }
            }
            if (battleshipNum == 1 && cruiserNum == 2 && destroyerNum == 3 && submarineNum == 4)
                return true;
            else
                return false;
        }
    }
}
