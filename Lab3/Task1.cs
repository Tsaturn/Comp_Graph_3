using System;
using System.Drawing;
using System.Windows.Forms;

namespace Lab3
{
    public partial class Task1 : Form
    {
        private Graphics g;
        private Image im;
        private bool isDrawing = false; 
        private Point lastPoint;
        Pen pen = new Pen(Color.SlateBlue, 1);

        public Task1()
        {
            InitializeComponent();
            pictureBox2.Image = new Bitmap(pictureBox2.Width, pictureBox2.Height);
            g = Graphics.FromImage(pictureBox2.Image);
            g.Clear(Color.White);
        }

        private void pictureBox2_MouseDown_1(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                isDrawing = true;
                lastPoint = e.Location; 
            }
        }

        private void pictureBox2_MouseMove_1(object sender, MouseEventArgs e)
        {
            if (isDrawing)
            { 
                g.DrawLine(pen, lastPoint, e.Location);
                lastPoint = e.Location; 
                pictureBox2.Invalidate(); 
            }
        }

        private void pictureBox2_MouseUp_1(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                isDrawing = false; 
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (colorDialog1.ShowDialog() == DialogResult.Cancel)
                return;
            this.BackColor = colorDialog1.Color;
        }
    }
}
