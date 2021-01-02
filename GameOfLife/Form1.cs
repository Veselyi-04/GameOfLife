using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GameOfLife
{
    public partial class Form1 : Form
    {

        private Graphics graphics;
        private int resolution;
        private GameEngine gameEngine;

        public Form1()
        {
            InitializeComponent();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            DrawNexGenerations();
        }

        private void DrawNexGenerations()
        {
            graphics.Clear(Color.Black);

            var field = gameEngine.GetCurrentGenerations();

            for (int x = 0; x < field.GetLength(0); x++)
            {
                for (int y = 0; y < field.GetLength(1); y++)
                {
                    if (field[x, y])
                        graphics.FillRectangle(Brushes.Green, x * resolution, y * resolution, resolution - 1, resolution - 1);
                }
            }
        
            Text = $"Generations {gameEngine.CurrentGenerations}";
            pictureBox1.Refresh();
            gameEngine.nexGenerations();
        }

        private void startGame()
        {
            if (timer1.Enabled)
                return;

            nudResilution.Enabled = false;
            nudDensity.Enabled = false;
            bStart.Enabled = false;

            resolution = (int)nudResilution.Value;

            gameEngine = new GameEngine(
                rows: pictureBox1.Height / resolution,
                cols: pictureBox1.Width / resolution,
                density: (int)nudDensity.Maximum + (int)nudDensity.Minimum - (int)nudDensity.Value
            );

            Text = $"Generations {gameEngine.CurrentGenerations}";

            pictureBox1.Image = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            graphics = Graphics.FromImage(pictureBox1.Image);
            timer1.Start();
        }
        private void bStart_Click(object sender, EventArgs e)
        {
            startGame();
        }

        private void stopGame()
        {
            if (!timer1.Enabled)
                return;

            timer1.Stop();
            nudResilution.Enabled = true;
            nudDensity.Enabled = true;
            bStart.Enabled = true;
        }

        private void bStop_Click(object sender, EventArgs e)
        {
            stopGame();
        }

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
           if (!timer1.Enabled)
                return;

            if (e.Button == MouseButtons.Left)
            {
                int x = e.Location.X / resolution;
                int y = e.Location.Y / resolution;
                gameEngine.AddCell(x, y);
            }
            if (e.Button == MouseButtons.Right)
            {
                int x = e.Location.X / resolution;
                int y = e.Location.Y / resolution;
                gameEngine.RemoveCell(x, y);
            }

        }

        private void pictureBox1_MouseClick(object sender, MouseEventArgs e)
        {
            if (!timer1.Enabled)
                return;

            if (e.Button == MouseButtons.Left)
            {
                int x = e.Location.X / resolution;
                int y = e.Location.Y / resolution;
                gameEngine.AddCell(x, y);
            }
            if (e.Button == MouseButtons.Right)
            {
                int x = e.Location.X / resolution;
                int y = e.Location.Y / resolution;
                gameEngine.RemoveCell(x, y);
            }
        }
    }
}
