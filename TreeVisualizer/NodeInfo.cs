namespace TreeVisualizer
{
    // используется для отрисовки дерева на форме
    public class NodeInfo
    {
        public string Value { get; set; }

        public Position Position { get; set; }

        public Position LeftChildPosition { get; set; }

        public Position RightChildPosition { get; set; }
    }
}
