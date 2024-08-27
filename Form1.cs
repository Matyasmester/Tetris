using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Tetris
{
    public partial class MainForm : Form
    {
        private const int squareUnit = 25;

        private Tetromino currentTetromino;

        private bool isTetrominoPresent = false;

        private Random random = new Random();

        private Point spawnPoint;

        private readonly Tetromino[] allTetrominos = 
        { 
            new IShape(Color.LightSkyBlue), 
            new TShape(Color.Purple), 
            new SShape(Color.Green), 
            new ZShape(Color.Red), 
            new OShape(Color.Yellow), 
            new JShape(Color.DarkBlue), 
            new LShape(Color.Orange)
        };

        public MainForm()
        {
            InitializeComponent();

            mainCheckeredGrid.SetSquareUnit(squareUnit);

            mainCheckeredGrid.InitGraphics();

            mainCheckeredGrid.InitializeGrid();

            this.KeyDown += MainForm_KeyDown;

            spawnPoint = new Point(mainCheckeredGrid.GetWidthInTiles() / 2, 0);

            MainTimer.Start();
        }

        private void MainForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (!isTetrominoPresent) return;

            mainCheckeredGrid.pointsToClear.AddRange(currentTetromino.GetPoints());

            Direction direction;

            switch (e.KeyCode)
            {
                case Keys.A:
                    direction = Direction.Left;
                    break;
                case Keys.D:
                    direction = Direction.Right;
                    break;
                case Keys.S:
                    direction = Direction.Down;
                    break;
                case Keys.W:
                    if (IsValidRotation(currentTetromino.SimulateRotation(false))) currentTetromino.Rotate(false);

                    PaintOccupiedTiles();

                    return;
                default:
                    return;
            }

            if (CanCurrentMove(direction)) currentTetromino.Move(direction);
            else
            {
                if(direction == Direction.Down)
                {
                    mainCheckeredGrid.SaveTetromino(currentTetromino);

                    isTetrominoPresent = false;
                }
            }

            PaintOccupiedTiles();
        }

        private void SpawnTetromino()
        {
            int index = random.Next(0, 7);

            currentTetromino = allTetrominos[index];

            mainCheckeredGrid.pointsToClear.AddRange(currentTetromino.FormAt(spawnPoint));

            isTetrominoPresent = true;
        }

        private void PaintOccupiedTiles()
        {
            mainCheckeredGrid.ClearPoints();

            mainCheckeredGrid.FillSavedTetrominos();

            mainCheckeredGrid.FillTetromino(currentTetromino);
        }

        private bool IsValidRotation(List<Point> rotated)
        {
            return rotated.All(p => p.X >= 0 && p.X < mainCheckeredGrid.GetWidthInTiles()
                && p.Y >= 0 && p.Y < mainCheckeredGrid.GetHeightInTiles() 
                && !mainCheckeredGrid.savedTetrominoPoints.Any(kwp => kwp.Key == p));
        }

        private bool CanCurrentMove(Direction direction)
        {
            List<Point> currentPoints = currentTetromino.GetPoints();

            switch (direction)
            {
                case Direction.Down:
                    int maxY = currentPoints.Max(p => p.Y);

                    return maxY < mainCheckeredGrid.GetHeightInTiles() - 1
                        && currentPoints.All(p => !mainCheckeredGrid.savedTetrominoPoints.Any(kwp => kwp.Key == new Point(p.X, p.Y + 1)));

                case Direction.Left:
                    int minX = currentPoints.Min(p => p.X);

                    return minX > 0 
                        && currentPoints.All(p => !mainCheckeredGrid.savedTetrominoPoints.Any(kwp => kwp.Key == new Point(p.X - 1, p.Y)));

                case Direction.Right:
                    int maxX = currentPoints.Max(p => p.X);

                    return maxX < mainCheckeredGrid.GetWidthInTiles() - 1
                        && currentPoints.All(p => !mainCheckeredGrid.savedTetrominoPoints.Any(kwp => kwp.Key == new Point(p.X + 1, p.Y)));

                default:
                    return false;
            }
        }

        private void MainForm_Load(object sender, EventArgs e)
        {

        }

        private void checkeredGrid1_Load(object sender, EventArgs e)
        {
            
        }

        protected override void OnPaint(PaintEventArgs e)
        {

        }

        private void MainTimer_Tick(object sender, EventArgs e)
        {
            if(!isTetrominoPresent) SpawnTetromino();

            List<Point> currentPoints = currentTetromino.GetPoints();

            PaintOccupiedTiles();

            mainCheckeredGrid.pointsToClear.AddRange(currentPoints);

            if(CanCurrentMove(Direction.Down)) currentTetromino.Move(Direction.Down);
            else
            {
                mainCheckeredGrid.SaveTetromino(currentTetromino);

                isTetrominoPresent = false;
            }
        }
    }
}
