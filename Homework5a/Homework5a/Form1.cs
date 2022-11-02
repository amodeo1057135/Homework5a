using System.CodeDom;
using System.Data;
using System.Runtime.CompilerServices;

namespace Homework5a
{
    public partial class Form1 : Form
    {
        private Bitmap b;
        private Graphics g;
        private int[] dist;
        private Pen histoPen = new Pen(Color.Red, 50);

        public Form1()
        {
            InitializeComponent();
            dist = new int[] { 30, 58, 47, 88, 99, 32, 23, 1, 7, 89, 54 };
            b = new Bitmap(pictureBox1.Width,pictureBox1.Height);
            g = Graphics.FromImage(b);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            g.Clear(Color.White);
            pictureBox1.Image = b;
        }

        /*private void numericUpDown2_ValueChanged(object sender, EventArgs e)
        {
            this.sequencesCount = Convert.ToInt32(Math.Floor(((NumericUpDown)sender).Value));
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            this.trialsCount = Convert.ToInt32(Math.Floor(((NumericUpDown)sender).Value));
        }*/

        private void button1_Click(object sender, EventArgs e)
        {
            g.Clear(Color.White);
            Point minValue = new Point(0, 0);
            Point maxValue = new Point(dist.Length, dist.Sum());
            Rectangle plottableWindow = new Rectangle(50, 50, b.Width - 50, b.Height - 50);

            List<PointF> histogram = new List<PointF>();
           
            for (int x = 0; x < dist.Length; x++)
            {
                var pointF = fromRealToVirtual(new PointF(x, 0), minValue, maxValue, plottableWindow);
                var pointH = fromRealToVirtual(new PointF(x, dist[x]), minValue, maxValue, plottableWindow);
                histogram.Add(pointF);
                histogram.Add(pointH);   
            }
            var histoArray = histogram.ToArray();
            for (int i= 0; i < histoArray.Length-1; i++)
            {
                g.DrawLine(histoPen, histoArray[i], histoArray[i+1]);
                i++;
            }
            pictureBox1.Image = b;
        }
        private void button2_Click(object sender, EventArgs e)
        {
            g.Clear(Color.White);
            Point minValue = new Point(0, 0);
            Point maxValue = new Point(dist.Sum(), dist.Length);
            Rectangle plottableWindow = new Rectangle(50, 50, b.Width - 50, b.Height - 50);

            List<PointF> histogram = new List<PointF>();

            for (int y = 0; y < dist.Length; y++)
            {
                var pointF = fromRealToVirtual(new PointF(0, y), minValue, maxValue, plottableWindow);
                var pointH = fromRealToVirtual(new PointF(dist[y], y), minValue, maxValue, plottableWindow);
                histogram.Add(pointF);
                histogram.Add(pointH);
            }
            var histoArray = histogram.ToArray();
            for (int i = 0; i < histoArray.Length - 1; i++)
            {
                g.DrawLine(histoPen, histoArray[i], histoArray[i + 1]);
                i++;
            }
            pictureBox1.Image = b;
        }

        private PointF fromRealToVirtual(PointF point, Point minValue, Point maxValue, Rectangle rect)
        {
            float newX = maxValue.X - minValue.X == 0 ? 0 : (rect.Left + rect.Width * (point.X - minValue.X) / (maxValue.X - minValue.X));
            float newY = maxValue.Y - minValue.Y == 0 ? 0 : (rect.Top + rect.Height - rect.Height * (point.Y - minValue.Y) / (maxValue.Y - minValue.Y));
            return new PointF(newX, newY);
        }

        
    }
}