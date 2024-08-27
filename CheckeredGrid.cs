using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
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

        public List<KeyValuePair<Point, Color>> savedTetrominoPoints = new List<KeyValuePair<Point, Color>>();

        public List<Point> pointsToClear = new List<Point>();

        public CheckeredGrid()
        {
            InitializeComponent();

            bgBrush = new SolidBrush(Color.Black);
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

        public void FillSavedTetrominos()
        {
            foreach(KeyValuePair<Point, Color> kwp in this.savedTetrominoPoints)
            {
                Point p = kwp.Key;

                DrawRectAt(p, new SolidBrush(kwp.Value));
            }

            this.Invalidate();
        }

        public void FillTetromino(Tetromino t)
        {
            foreach (Point p in t.GetPoints())
            {
                try
                {
                    SetOccupied(p.X, p.Y);
                }
                catch (IndexOutOfRangeException)
                {
                    return;
                }

                DrawRectAt(p, new SolidBrush(t.GetColor()));
            }

            this.Invalidate();

        }

        public void SaveTetromino(Tetromino t)
        {
            foreach(Point p in t.GetPoints())
            {
                this.savedTetrominoPoints.Add(new KeyValuePair<Point, Color>(p, t.GetColor()));
            }
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

        private void SetOccupied(int x, int y)
        {
            Map[x * squareUnit, y * squareUnit] = Occupied;
        }

        public void SetOccupied(Tetromino t)
        {
            foreach(Point p in t.GetPoints())
            {
                this.SetOccupied(p.X, p.Y);
            }
        }

        public bool IsOccupied(Point p)
        {
            try
            {
                return Map[p.X * squareUnit, p.Y * squareUnit] == Occupied;
            }
            catch (IndexOutOfRangeException) {  return true; }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            e.Graphics.DrawImage(bitmap, 0, 0);
        }
    }
}
