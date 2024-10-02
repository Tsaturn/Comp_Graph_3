using System;
using System.Drawing;
using System.Windows.Forms;
using System.Threading;
using System.Drawing.Imaging;
using static System.Net.Mime.MediaTypeNames;
using System.Collections.Generic;
using static System.Windows.Forms.AxHost;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;


namespace Lab3
{
    enum Type
    {
        Pen, Fill, FillImage, Hightlight,
    }

    public partial class Task1 : Form
    {
        private Type type; // Метка что мы делаем: заливаем, рисуем, выделяем итд
        private bool isDrawing = false; // Флаг рисования
        private Bitmap bitmap; // Это наш холст
        private Point lastPoint; // Последняя точка рисования
        private Graphics g;
        private Pen pen = new Pen(Color.SlateBlue, 1); // Карандаш
        private Color fillColor;
        private Bitmap originalImage; // это фотка, которую мы загружаем

        public Task1()
        {
            InitializeComponent();
            type = Type.Pen;
            bitmap = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            pictureBox1.Image = bitmap;
            g = Graphics.FromImage(bitmap);
            g.Clear(Color.White);
        }

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            lastPoint = e.Location;
            var x = lastPoint.X;
            var y = lastPoint.Y;
            switch (type)
            {
                case Type.Fill:
                    Color targetColor = bitmap.GetPixel(x, y);
                    FloodFill(x, y, bitmap, targetColor, fillColor);
                    pictureBox1.Invalidate();
                    break;
                case Type.Pen:
                    isDrawing = true;
                    break;
                case Type.FillImage:
                    targetColor = bitmap.GetPixel(x, y);
                    FloodFillWithImage(x, y, bitmap, originalImage, targetColor);
                    pictureBox1.Invalidate();
                    break;
                case Type.Hightlight:
                    Highlight(x, y, bitmap);
                    break;
            }

        }

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            if (isDrawing)
            {
                g.DrawLine(pen, lastPoint, e.Location);
                lastPoint = e.Location;
                pictureBox1.Invalidate();
            }
        }

        private void pictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                isDrawing = false;
            }
        }

        private void clear_button(object sender, EventArgs e)
        {
            g.Clear(Color.White);
            pictureBox1.Invalidate();
        }

        private void buttonSelectColor(object sender, EventArgs e)
        {
            if (colorDialog1.ShowDialog() == DialogResult.OK)
            {
                fillColor = colorDialog1.Color;
            }
        }

        private void pen_button(object sender, EventArgs e)
        {
            type = Type.Pen;
        }

        private void fill_button(object sender, EventArgs e)
        {
            type = Type.Fill;
        }

        private void fillImage_button(object sender, EventArgs e)
        {
            type = Type.FillImage;
        }

        private void hightlight_button(object sender, EventArgs e)
        {
            type = Type.Hightlight;
            pictureBox1.Cursor = Cursors.Hand;
        }

        private void thicknessChange(object sender, EventArgs e)
        {
            pen = new Pen(Color.SlateBlue, trackBar1.Value);
        }

        private void FloodFill(int x, int y, Bitmap b, Color targetColor, Color fillColor)
        {
            if (x < 0 || x >= b.Width || y < 0 || y >= b.Height)
                return;

            if (b.GetPixel(x, y) != targetColor || b.GetPixel(x, y) == fillColor)
                return;

            int x_left = x;
            int x_right = x;

            // Найти крайние левые и правые границы интервала
            while (x_left > 0 && b.GetPixel(x_left - 1, y) == targetColor)
            {
                x_left--;
            }

            while (x_right < b.Width - 1 && b.GetPixel(x_right + 1, y) == targetColor)
            {
                x_right++;
            }

            // Закрасить интервал
            for (int i = x_left; i <= x_right; i++)
            {
                b.SetPixel(i, y, fillColor);
            }

            // Рекурсивно проверить строки выше и ниже
            for (int i = x_left; i <= x_right; i++)
            {
                if (y > 0 && b.GetPixel(i, y - 1) == targetColor)
                {
                    FloodFill(i, y - 1, b, targetColor, fillColor);
                }
                if (y < b.Height - 1 && b.GetPixel(i, y + 1) == targetColor)
                {
                    FloodFill(i, y + 1, b, targetColor, fillColor);
                }
            }
        }

        private void FloodFillWithImage(int x, int y, Bitmap b, Bitmap pattern, Color targetColor)
        {
            if (x < 0 || x >= b.Width || y < 0 || y >= b.Height)
                return;

            if (b.GetPixel(x, y) != targetColor)
                return;

            int x_left = x;
            int x_right = x;

            // Найти крайние левые и правые границы интервала
            while (x_left > 0 && b.GetPixel(x_left - 1, y) == targetColor)
            {
                x_left--;
            }

            while (x_right < b.Width - 1 && b.GetPixel(x_right + 1, y) == targetColor)
            {
                x_right++;
            }

            // Закрасить интервал рисунком
            for (int i = x_left; i <= x_right; i++)
            {
                Color fillColor = pattern.GetPixel(i % pattern.Width, y % pattern.Height);
                b.SetPixel(i, y, fillColor);
            }

            // Рекурсивно проверить строки выше и ниже
            for (int i = x_left; i <= x_right; i++)
            {
                if (y > 0 && b.GetPixel(i, y - 1) == targetColor)
                {
                    FloodFillWithImage(i, y - 1, b, pattern, targetColor);
                }
                if (y < b.Height - 1 && b.GetPixel(i, y + 1) == targetColor)
                {
                    FloodFillWithImage(i, y + 1, b, pattern, targetColor);
                }
            }

        }

        private bool IsSimilarColor(Color c1, Color c2, int threshold = 50)
        {
            int rDiff = Math.Abs(c1.R - c2.R);
            int gDiff = Math.Abs(c1.G - c2.G);
            int bDiff = Math.Abs(c1.B - c2.B);
            return rDiff <= threshold && gDiff <= threshold && bDiff <= threshold;
        }

        private List<Point> GetBorderPoints(int x, int y, Bitmap b)
        {
            int x_left = x;
            while (x_left > 0 && !IsSimilarColor(b.GetPixel(x_left, y), Color.SlateBlue))
                x_left--;

            List<List<int>> directions = new List<List<int>>
            {
                new List<int>() { 1, 0 },
                new List<int>() { 1, 1 },
                new List<int>() { 0, 1 },
                new List<int>() { -1, 1 },
                new List<int>() { -1, 0 },
                new List<int>() { -1, -1 },
                new List<int>() { 0, -1 },
                new List<int>() { 1, -1 },
            };

            Stack<Point> stack = new Stack<Point>();
            HashSet<Point> labeled = new HashSet<Point>();
            List<Point> result = new List<Point>();

            stack.Push(new Point(x_left, y));

            while (stack.Count > 0)
            {
                var point = stack.Pop();
                x = point.X;
                y = point.Y;
                if (!labeled.Contains(point) && x >= 0 && x < b.Width && y >= 0 && y < b.Height && IsSimilarColor(b.GetPixel(x, y), Color.SlateBlue))
                {
                    result.Add(point);
                    labeled.Add(point);
                    foreach (var direction in directions)
                        stack.Push(new Point(x + direction[0], y + direction[1]));
                }
            }
            return result;
        }

        public void Highlight(int x, int y, Bitmap b)
        {
            Color targetColor = bitmap.GetPixel(x, y);
            List<Point> borderPoints = GetBorderPoints(x, y, b);

            foreach (Point point in borderPoints)
            {
                bitmap.SetPixel(point.X, point.Y, Color.Fuchsia);
            }
            pictureBox1.Invalidate();
        }


        private void UploadImageButton(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.bmp";

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                originalImage = new Bitmap(openFileDialog.FileName);
                Bitmap resizedImage = new Bitmap(pictureBox2.Width, pictureBox2.Height);
                using (Graphics g = Graphics.FromImage(resizedImage))
                {
                    g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBilinear;
                    g.DrawImage(originalImage, 0, 0, pictureBox2.Width, pictureBox2.Height);
                }

                pictureBox2.Image = resizedImage;
                if (pictureBox2.Image != null)
                {
                    button5.Enabled = true;
                }

                pictureBox2.Invalidate();
            }
        }


    }
}
