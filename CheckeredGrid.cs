using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Tetris
{
    public partial class CheckeredGrid : UserControl
    {
        private int squareUnit;

        private Size squareSize;

        private Bitmap bitmap;
        private Graphics bitmapGraphics;

        private Brush bgBrush;

        private char Empty = 'E';
        private char Occupied = 'O';

        private char[,] Map;

        public List<Point> pointsToClear = new List<Point>();

        public CheckeredGrid()
        {
            InitializeComponent();

            bgBrush = new SolidBrush(this.BackColor);
        }

        private void CheckeredGrid_Load(object sender, EventArgs e)
        {
            
        }

        public void SetSquareUnit(int squareUnit)
        {
            this.squareUnit = squareUnit;

            squareSize = new Size(squareUnit, squareUnit);
        }

        public void InitGraphics()
        {
            int width = this.Width;
            int height = this.Height;

            bitmap = new Bitmap(width, height);
            bitmapGraphics = Graphics.FromImage(bitmap);

            Map = new char[width, height];
        }

        private void DrawRectAt(Point p, Brush brush)
        {
            Point transposed = new Point(p.X * squareUnit, p.Y * squareUnit);

            Rectangle rect = new Rectangle(transposed, squareSize);

            bitmapGraphics.FillRectangle(brush, rect);
        }

        private void DrawRectOutlineAt(Point p, Pen pen)
        {
            Point transposed = new Point(p.X * squareUnit, p.Y * squareUnit);

            Rectangle rect = new Rectangle(transposed, squareSize);

            bitmapGraphics.DrawRectangle(pen, rect);
        }

        public void InitializeGrid()
        {
            for (int x = 0; x < this.Width / squareUnit; x++)
            {
                for(int y = 0; y < this.Height / squareUnit; y++)
                {
                    Point p = new Point(x, y);

                    DrawRectAt(p, bgBrush);

                    SetEmpty(x, y);
                }
            }

            this.Invalidate();
        }

        public void ClearPoints()
        {
            foreach(Point p in pointsToClear)
            {
                try
                {
                    DrawRectAt(p, bgBrush);

                    SetEmpty(p.X, p.Y);
                }
                catch (IndexOutOfRangeException)
                {
                    continue;
                }
            }

            pointsToClear.Clear();
        }

        public bool TryFillFormation(List<Point> points, Brush brush)
        {
            foreach(Point p in points)
            {
                int x = p.X;
                int y = p.Y;

                try
                {
                    SetOccupied(x, y);
                } 
                catch(IndexOutOfRangeException)
                {
                    return false;
                }

                DrawRectAt(new Point(x, y), brush);
            }

            this.Invalidate();

            return true;
        }

        public char[,] GetMapState()
        {
            return Map;
        }

        public int GetWidthInTiles()
        {
            return this.Width / squareUnit;
        }

        public int GetHeightInTiles()
        {
            return this.Height / squareUnit;
        }

        public void SetEmpty(int x, int y)
        {
            Map[x * squareUnit, y * squareUnit] = Empty;
        }

        public void SetOccupied(int x, int y)
        {
            Map[x * squareUnit, y * squareUnit] = Occupied;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            e.Graphics.DrawImage(bitmap, 0, 0);
        }
    }
}
