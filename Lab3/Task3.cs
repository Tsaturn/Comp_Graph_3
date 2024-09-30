using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lab3
{
    public partial class Task3 : Form
    {
        private Color PointColor = Color.Black;
        private Graphics g;
        private Queue<(Point, Color)> points = new Queue<(Point, Color)>();
        private int count = 0;

        public Task3()
        {
            InitializeComponent();
            pictureBox1.Image = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            g = Graphics.FromImage(pictureBox1.Image);
            g.Clear(Color.White);
        }

        private void Task3_Load(object sender, EventArgs e)
        {

        }



        private void button1_Click(object sender, EventArgs e)
        {
                if (colorDialog1.ShowDialog() == DialogResult.Cancel)
                    return;
                PointColor = colorDialog1.Color;
        }

        private void pictureBox1_MouseClick(object sender, MouseEventArgs e)
        {
            Point point = e.Location;
            count++;

            float x = e.Location.X;
            float y = e.Location.Y;
            g.FillRectangle(new SolidBrush(PointColor), e.Location.X, e.Location.Y, 2, 2);
            pictureBox1.Refresh();

            points.Enqueue((point, PointColor));
            if (count % 3 == 0)
                DrawTriangle();
        }

        private void DrawTriangle()
        {
            (Point, Color) p1 = points.Dequeue();
            (Point, Color) p2 = points.Dequeue();
            (Point, Color) p3 = points.Dequeue();


        }

        private void button2_Click(object sender, EventArgs e)
        {
            g.Clear(Color.White);
            pictureBox1.Refresh();
            count = 0;
            points.Clear();
        }
    }
}
