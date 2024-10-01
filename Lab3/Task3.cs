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

            // Сортировка точек по координате Y, чтобы в они были по возрастанию 
            if (p1.Item1.Y > p2.Item1.Y) { (p1, p2) = (p2, p1); }
            if (p1.Item1.Y > p3.Item1.Y) { (p1, p3) = (p3, p1); }
            if (p2.Item1.Y > p3.Item1.Y) { (p2, p3) = (p3, p2); }

            int top = p1.Item1.Y;
            int cross_x1, cross_x2;
            int dx1 = p2.Item1.X - p1.Item1.X;
            int dy1 = p2.Item1.Y - p1.Item1.Y;
            int dx2 = p3.Item1.X - p1.Item1.X;
            int dy2 = p3.Item1.Y - p1.Item1.Y;

            while (top < p2.Item1.Y)
            {
                cross_x1 = p1.Item1.X + dx1 * (top - p1.Item1.Y) / dy1;
                cross_x2 = p1.Item1.X + dx2 * (top - p1.Item1.Y) / dy2;

                if (cross_x1 > cross_x2)
                    //g.DrawRectangle(new Pen(Color.Aqua, 1), new Rectangle(cross_x2, top, cross_x1 - cross_x2, 1));
                    //g.DrawRectangle(new Pen(Color.Bisque, 1), new Rectangle(cross_x1, top, cross_x2 - cross_x1, 1));
                    while (cross_x2 < cross_x1)
                    {
                        Color c = CalculatePixelColor(cross_x2, top, p1.Item2, p2.Item2, p3.Item2, p1, p2, p3);
                        g.DrawRectangle(new Pen(c, 1), new Rectangle(cross_x2, top, 1, 1));
                        cross_x2++;
                        pictureBox1.Refresh();
                    }
                else
                    while (cross_x1 < cross_x2)
                    {
                        Color c = CalculatePixelColor(cross_x1, top, p1.Item2, p2.Item2, p3.Item2, p1, p2, p3);
                        g.DrawRectangle(new Pen(c, 1), new Rectangle(cross_x1, top, 1, 1));
                        cross_x1++;
                        pictureBox1.Refresh();
                    }


                top++;
            }
            pictureBox1.Refresh();

            dx1 = p3.Item1.X - p2.Item1.X;
            dy1 = p3.Item1.Y - p2.Item1.Y;

            while (top < p3.Item1.Y)
            {
                cross_x1 = p2.Item1.X + dx1 * (top - p2.Item1.Y) / dy1;
                cross_x2 = p1.Item1.X + dx2 * (top - p1.Item1.Y) / dy2;

                if (cross_x1 > cross_x2)
                    while (cross_x2 < cross_x1)
                    {
                        Color c = CalculatePixelColor(cross_x2, top, p1.Item2, p2.Item2, p3.Item2, p1, p2, p3);
                        g.DrawRectangle(new Pen(c, 1), new Rectangle(cross_x2, top, 1, 1));
                        cross_x2++;
                        pictureBox1.Refresh();
                    }
                else
                    while (cross_x1 < cross_x2)
                    {
                        Color c = CalculatePixelColor(cross_x1, top, p1.Item2, p2.Item2, p3.Item2, p1, p2, p3);
                        g.DrawRectangle(new Pen(c, 1), new Rectangle(cross_x1, top, 1, 1));
                        cross_x1++;
                        pictureBox1.Refresh();
                    }
                top++;
            }
            pictureBox1.Refresh();

        }
        private Color CalculatePixelColor(int x, int y, Color c1, Color c2, Color c3, (Point, Color) p1, (Point, Color) p2, (Point, Color) p3)
        {
            // 1. Находим барицентрические координаты точки (x, y) относительно треугольника
            //    (a, b, c) - барицентрические координаты, где a + b + c = 1
            //    a, b, c - доли влияния цветов вершин треугольника на итоговый цвет
            double a, b, c;
            CalculateBarycentricCoordinates(x, y, p1.Item1.X, p1.Item1.Y, p2.Item1.X, p2.Item1.Y, p3.Item1.X, p3.Item1.Y, out a, out b, out c);

            // 2. Интерполируем цвет пикселя с помощью барицентрических координат
            //    r, g, b - компоненты итогового цвета пикселя
            int r = (int)(c1.R * a + c2.R * b + c3.R * c);
            if (r > 255) r = 255;
            if (r < 0) r = 0;
            int g = (int)(c1.G * a + c2.G * b + c3.G * c);
            if (g > 255) g = 255;
            if (g < 0) g = 0;
            int blue = (int)(c1.B * a + c2.B * b + c3.B * c);
            if (blue > 255) blue = 255;
            if (blue < 0)   blue = 0;

            // 3. Возвращаем интерполированный цвет
            return Color.FromArgb(r, g, blue);
        }

        private void CalculateBarycentricCoordinates(int x, int y, int x1, int y1, int x2, int y2, int x3, int y3, out double a, out double b, out double c)
        {
            // Формула для вычисления барицентрических координат
            double denominator = (x2 - x1) * (y3 - y1) - (x3 - x1) * (y2 - y1);
            a = ((x2 * y3 - x3 * y2) + (y2 - y3) * x + (x3 - x2) * y) / denominator;
            b = ((x3 * y1 - x1 * y3) + (y3 - y1) * x + (x1 - x3) * y) / denominator;
            c = 1 - a - b;
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
