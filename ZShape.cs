using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tetris
{
    public class ZShape : Tetromino
    {
        public ZShape(Color color)
        {
            this.color = color;
        }

        public override List<Point> FormAt(Point startingPoint)
        {
            base.FormAt(startingPoint);

            int startX = startingPoint.X;
            int startY = startingPoint.Y;

            points = new List<Point>
            {
                startingPoint,
                new Point(startX + 1, startY),
                new Point(startX + 1, startY + 1),
                new Point(startX + 2, startY + 1)
            };

            base.points = new List<Point>(points);

            return points;
        }
    }
}
