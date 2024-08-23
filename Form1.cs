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

        private Brush redBrush = new SolidBrush(Color.Red);

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
            if (currentTetromino == null) return;

            switch (e.KeyCode)
            {
                case Keys.A:
                    currentTetromino.Move(Direction.Left);
                    break;
                case Keys.D:
                    currentTetromino.Move(Direction.Right);
                    break;
                case Keys.S:
                    currentTetromino.Move(Direction.Down);
                    break;
                case Keys.W:
                    currentTetromino.RotateRight();
                    break;
                default:
                    break;
            }

            PaintTetromino();
        }

        private void SpawnTetromino()
        {
            int index = random.Next(0, 7);

            currentTetromino = allTetrominos[index];

            currentTetromino.previousPoints = currentTetromino.FormAt(spawnPoint);

            isTetrominoPresent = true;
        }

        private void PaintTetromino()
        {
            mainCheckeredGrid.ClearAreaAround(currentTetromino.GetPrevPoints()[2], 3);

            mainCheckeredGrid.TryFillFormation(currentTetromino.GetPoints(), new SolidBrush(currentTetromino.color));
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

            PaintTetromino();

            currentTetromino.Move(Direction.Down);
        }
    }
}
