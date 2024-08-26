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
                    currentTetromino.Rotate(false);

                    // undo lol
                    if (!IsValidRotation(currentTetromino.GetPoints())) currentTetromino.Rotate(true);

                    PaintCurrentTetromino();

                    return;
                default:
                    return;
            }

            if (CanCurrentMove(direction)) currentTetromino.Move(direction);
            else MessageBox.Show("DEBUG: cant move further"); 

            PaintCurrentTetromino();
        }

        private void SpawnTetromino()
        {
            int index = random.Next(0, 7);

            currentTetromino = allTetrominos[index];

            mainCheckeredGrid.pointsToClear.AddRange(currentTetromino.FormAt(spawnPoint));

            isTetrominoPresent = true;
        }

        private void PaintCurrentTetromino()
        {
            mainCheckeredGrid.ClearPoints();

            mainCheckeredGrid.TryFillFormation(currentTetromino.GetPoints(), new SolidBrush(currentTetromino.color));
        }

        private bool IsValidRotation(IEnumerable<Point> rotated)
        {
            return rotated.All(p => p.X >= 0 && p.X < mainCheckeredGrid.GetWidthInTiles() 
                && p.Y >= 0 && p.Y < mainCheckeredGrid.GetHeightInTiles());
        }

        private bool CanCurrentMove(Direction direction)
        {
            List<Point> currentPoints = currentTetromino.GetPoints();

            switch (direction)
            {
                case Direction.Down:
                    return currentPoints.Max(p => p.Y) < mainCheckeredGrid.GetHeightInTiles() - 1;
                case Direction.Left:
                    return currentPoints.Min(p => p.X) > 0;
                case Direction.Right:
                    return currentPoints.Max(p => p.X) < mainCheckeredGrid.GetWidthInTiles() - 1;
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

            PaintCurrentTetromino();

            mainCheckeredGrid.pointsToClear.AddRange(currentTetromino.GetPoints());

            if(CanCurrentMove(Direction.Down)) currentTetromino.Move(Direction.Down);
            //else assimilate
        }
    }
}
