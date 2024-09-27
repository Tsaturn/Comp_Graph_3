using System;
using System.Drawing;
using System.Windows.Forms;

namespace Lab3
{
    public partial class Task2 : Form
    {
        public Task2()
        {
            InitializeComponent();
        }

        private void Task2_Load(object sender, EventArgs e)
        {
        }

        private void BresenhamLine(int x0, int y0, int x1, int y1)
        {
            int dx = Math.Abs(x1 - x0);
            int dy = Math.Abs(y1 - y0);
            int sx = x0 < x1 ? 1 : -1;
            int sy = y0 < y1 ? 1 : -1;
            int err = dx - dy;

            while (true)
            {
                DrawPoint(x0, y0); 

                if (x0 == x1 && y0 == y1) break;

                int e2 = 2 * err;
                if (e2 > -dy)
                {
                    err -= dy;
                    x0 += sx;
                }

                if (e2 < dx)
                {
                    err += dx;
                    y0 += sy;
                }
            }
        }

        private void WuLine(int x0, int y0, int x1, int y1)
        {
            float dx = x1 - x0;
            float dy = y1 - y0;
            float gradient = dy / dx;

            if (x0 > x1)
            {
                int tempX = x0;
                int tempY = y0;
                x0 = x1;
                y0 = y1;
                x1 = tempX;
                y1 = tempY;
            }

            float y = y0 + gradient;

            for (var x = x0 + 1; x <= x1 - 1; x++)
            {
                DrawPoint(x, (int)y, 1 - (y - (int)y)); 
                DrawPoint(x, (int)y + 1, y - (int)y);   
                y += gradient;
            }
        }


        private void DrawPoint(int x, int y, float intensity = 1)
        {
            if (intensity == 1)
            {
                using (Graphics g = pictureBox1.CreateGraphics())
                {
                    g.FillRectangle(Brushes.Black, x, y, 2, 2);
                }
            }
            else
            {
                using (Graphics g = pictureBox1.CreateGraphics())
                {
                    Color color = Color.FromArgb((int)(intensity * 255), Color.Black);
                    Brush brush = new SolidBrush(color);
                    g.FillRectangle(brush, x, y, 2, 2);
                }
            }
        }

        private void pictureBox1_MouseClick(object sender, MouseEventArgs e)
        {
            int x0 = 150;  
            int y0 = 100;
            int x1 = e.X;
            int y1 = e.Y;

            if (checkBoxBresenham.Checked)
            {
                BresenhamLine(x0, y0, x1, y1);
            }
            else if (checkBoxWu.Checked)
            {
                WuLine(x0, y0, x1, y1);
            }
        }

        private void clearButton_Click(object sender, EventArgs e)
        {
            pictureBox1.Image = null;
            pictureBox1.Invalidate(); 
        }

        private void checkBoxBresenham_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxBresenham.Checked)
            {
                checkBoxWu.Checked = false; 
            }
        }

        private void checkBoxWu_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxWu.Checked)
            {
                checkBoxBresenham.Checked = false; 
            }
        }
    }
}
