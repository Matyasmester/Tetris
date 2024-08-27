namespace Tetris
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.mainCheckeredGrid = new Tetris.CheckeredGrid();
            this.MainTimer = new System.Windows.Forms.Timer(this.components);
            this.SuspendLayout();
            // 
            // mainCheckeredGrid
            // 
            this.mainCheckeredGrid.BackColor = System.Drawing.Color.Black;
            this.mainCheckeredGrid.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.mainCheckeredGrid.Location = new System.Drawing.Point(91, 11);
            this.mainCheckeredGrid.Margin = new System.Windows.Forms.Padding(2);
            this.mainCheckeredGrid.Name = "mainCheckeredGrid";
            this.mainCheckeredGrid.Size = new System.Drawing.Size(500, 500);
            this.mainCheckeredGrid.TabIndex = 0;
            // 
            // MainTimer
            // 
            this.MainTimer.Interval = 1000;
            this.MainTimer.Tick += new System.EventHandler(this.MainTimer_Tick);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.DimGray;
            this.ClientSize = new System.Drawing.Size(684, 611);
            this.Controls.Add(this.mainCheckeredGrid);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.KeyPreview = true;
            this.Margin = new System.Windows.Forms.Padding(2);
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.Text = "Tetris";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private CheckeredGrid mainCheckeredGrid;
        private System.Windows.Forms.Timer MainTimer;
    }
}

