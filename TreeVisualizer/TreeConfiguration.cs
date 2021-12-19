namespace TreeVisualizer
{
    public class TreeConfiguration
    {
        Node nodeForSearch = null;

        public TreeConfiguration(int circleDiameter)
        {   // диаметр окружности узла в отобржаемом дереве
            CircleDiameter = circleDiameter;
        }

        public int CircleDiameter { get; private set; }
    }
}