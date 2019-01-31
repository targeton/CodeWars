using System.Collections.Generic;

namespace CodeWars._4kyu.SortBinaryTreeByLevels
{
    public class Node
    {
        public Node Left;
        public Node Right;
        public int Value;

        public Node(Node l, Node r, int v)
        {
            Left = l;
            Right = r;
            Value = v;
        }
    }
    public class Solution
    {
        public static List<int> TreeByLevels(Node node)
        {
            var result = new List<int>();
            Queue<Node> nodes = new Queue<Node>();
            nodes.Enqueue(node);
            while (nodes.Count > 0)
            {
                var n = nodes.Dequeue();
                if (n == null)
                    continue;
                result.Add(n.Value);
                nodes.Enqueue(n.Left);
                nodes.Enqueue(n.Right);
            }
            return result;
        }
    }
}
