using System;

namespace TreeVisualizer
{
    public class Node
    {
        public Node(int value)
        {
            Value = value;
        }

        public Node Left { get; set; }

        public Node Right { get; set; }
        // значение вершины
        public int Value { get; set; }
    }
}
