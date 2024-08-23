﻿using System;
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
        public volatile List<Point> previousPoints;

        public Point currentStartingPoint;

        public Color color;

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

        public List<Point> GetPrevPoints() 
        {
            return previousPoints;
        }

        public void RotateRight()
        {
            previousPoints = new List<Point>(points);

            if (points == null || points.Count == 0) return;

            Point reference = points[1];

            int refX = reference.X;
            int refY = reference.Y;

            for (int i = 0; i < points.Count; i++)
            {
                Point p = points[i];

                if (p == reference) continue;

                int deltaX = refX - p.X;
                int deltaY = refY - p.Y;

                deltaX *= -1;

                int x = refX + deltaY;
                int y = refY + deltaX;

                points[i] = new Point(x, y);
            }
        }

        public void Move(Direction direction)
        {
            previousPoints = new List<Point>(points);

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
