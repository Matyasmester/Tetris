using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Windows.Forms;

namespace Tetris
{
    public class Tetromino
    {
        public List<Point> points;

        public Point currentStartingPoint;

        private protected Color color;

        public Tetromino()
        {
            
        }

        public virtual List<Point> FormAt(Point startingPoint) 
        {
            currentStartingPoint = startingPoint;

            return points;
        }

        public List<Point> GetPoints()
        {
            return points;
        }

        public Color GetColor()
        {
            return color;
        }

        private protected List<Point> InnerRotate(bool isLeft)
        {
            List<Point> retval = new List<Point>();

            if (points == null || points.Count == 0) return retval;

            Point reference = points[1];

            int refX = reference.X;
            int refY = reference.Y;

            for (int i = 0; i < points.Count; i++)
            {
                Point p = points[i];

                int deltaX = refX - p.X;
                int deltaY = refY - p.Y;

                if (isLeft) deltaY *= -1;
                else deltaX *= -1;

                int x = refX + deltaY;
                int y = refY + deltaX;

                retval.Add(new Point(x, y));
            }

            return retval;
        }

        public void Rotate(bool isLeft)
        {
            this.points = new List<Point>(this.InnerRotate(isLeft));
        }

        public List<Point> SimulateRotation(bool isLeft)
        {
            return this.InnerRotate(isLeft);
        }

        public void Move(Direction direction)
        {
            for (int i = 0; i < points.Count; i++)
            {
                Point p = points[i];

                int x = p.X;
                int y = p.Y;

                if (direction == Direction.Down) y++;
                else x += (int)direction;

                points[i] = new Point(x, y);
            }
        }
    }
}
