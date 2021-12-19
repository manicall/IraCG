namespace TreeVisualizer
{
    public class TreeConfiguration
    {
        public TreeConfiguration(int circleDiameter)
        {
            CircleDiameter = circleDiameter;
        }

        public int CircleDiameter { get; private set; }
    }
}