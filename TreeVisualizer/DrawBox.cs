using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Windows.Forms;

namespace TreeVisualizer
{
    public class DrawBox : PictureBox
    {
        // набор вершин в дереве
        private IEnumerable<NodeInfo> _treeNodes;
        // содержит размер вершины в отображаемом дереве
        private TreeConfiguration _configuration;

        public DrawBox()
        {

        }

        public void Print(AVLTree tree)
        {
            _treeNodes = tree.GetAllNodes();
            _configuration = tree.GetConfiguration();

            // очищает поле отрисовки и вызывает OnPaint
            Invalidate();
        }

        protected override void OnPaint(PaintEventArgs pe)
        {
            // если в дереве нет вершин
            if (_treeNodes == null)
            {
                return;
            }

            // сглаживание отрисовки
            pe.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
            // для вывода отображаемого дерева по центру формы
            int baseOffset = Width / 2 - _configuration.CircleDiameter / 2 - _treeNodes.FirstOrDefault()?.Position.X ?? default;
            // отрисовка всех вершин и соединяющих линий
            foreach (var node in _treeNodes)
            {
                if (node.LeftChildPosition != null)
                    DrawConnectionLine(node.Position, node.LeftChildPosition, baseOffset, pe.Graphics);
                if (node.RightChildPosition != null)
                    DrawConnectionLine(node.Position, node.RightChildPosition, baseOffset, pe.Graphics);

                DrawNode(node, baseOffset, pe.Graphics);
            }
        }

        private void DrawConnectionLine(Position fromNodePosition, Position toNodePosition, int offset, Graphics grapics)
        {
            // перо отрисовывающее линию
            Pen linePen = new Pen(Color.Black, 1);
            // центр вершины, от которой нужно провести линию
            var startPoint = new Point
            {
                X = fromNodePosition.X + _configuration.CircleDiameter / 2 + offset,
                Y = fromNodePosition.Y + _configuration.CircleDiameter / 2
            };
            // центр вершины, до которой нужно провести линию
            var endPoint = new Point
            {
                X = toNodePosition.X + _configuration.CircleDiameter / 2 + offset,
                Y = toNodePosition.Y + _configuration.CircleDiameter / 2
            };
            // отрисовка линии
            grapics.DrawLine(
                linePen,
                startPoint,
                endPoint
            );
        }

        private void DrawNode(NodeInfo node, int offset, Graphics grapics)
        {
            // заполнение круга
            grapics.FillEllipse(
                new SolidBrush(Color.White),
                node.Position.X + offset,
                node.Position.Y,
                _configuration.CircleDiameter,
                _configuration.CircleDiameter
                );

            Pen pen;

            if (_configuration.NodeForSearch != null && int.Parse(node.Value) == _configuration.NodeForSearch.Value)
            {
                pen = new Pen(Color.Red, 1);
                _configuration.NodeForSearch = null;
            }
            else
            {
                pen = new Pen(Color.Black, 1);    
            }

            // отрисовка круга
            grapics.DrawEllipse(
                pen,
                node.Position.X + offset,
                node.Position.Y,
                _configuration.CircleDiameter,
                _configuration.CircleDiameter
                );

            // вычисление размера текста
            var stringSize = grapics.MeasureString(node.Value, DefaultFont);
            // запись текста в круг
            grapics.DrawString(
                node.Value,
                DefaultFont,
                new SolidBrush(Color.Black),
                node.Position.X + (_configuration.CircleDiameter / 2) - (stringSize.Width / 2) + 1 + offset,
                node.Position.Y + (_configuration.CircleDiameter / 2) - (stringSize.Height / 2) + 1
                );
        }
    }
}
 