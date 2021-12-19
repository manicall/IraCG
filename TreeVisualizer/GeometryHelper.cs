using System;
using System.Drawing;

namespace TreeVisualizer
{
    public static class GeometryHelper
    {
        public static Point CalculatePoint(Point a, Point b, double distance)
        {
            // a. вычислить вектор от o до g:
            double vectorX = b.X - a.X;
            double vectorY = b.Y - a.Y;

            // b. вычислить гипотинузу
            double factor = distance / Math.Sqrt(vectorX * vectorX + vectorY * vectorY);

            // c. учет длины
            vectorX *= factor;
            vectorY *= factor;

            // d. calculate and Draw the new vector,
            return new Point((int)(a.X + vectorX), (int)(a.Y + vectorY));
        }

        public static double GetDistance(Point a, Point b)
        {
            return Math.Sqrt(Math.Pow(b.X - a.X, 2) + Math.Pow(b.Y - a.Y, 2));
        }
    }
}
