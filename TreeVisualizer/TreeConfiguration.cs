namespace TreeVisualizer
{
    public class TreeConfiguration
    {
        Node nodeForSearch = null;

        public TreeConfiguration(int circleDiameter)
        {   // диаметр окружности узла в отображаемом дереве
            CircleDiameter = circleDiameter;
        }

        public int CircleDiameter { get; private set; }
        public Node NodeForSearch { get => nodeForSearch; set => nodeForSearch = value; }
    }
}