using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lab3 {
    public partial class MainForm : Form {
        private List<Point> arPoints = new List<Point>();
        private bool bAddPoints;
        private bool bDrawCurve;
        private bool bDrawPolygon;
        private static int pointSize = 5;
        private static Color pointColor = Color.Blue;
        private static int lineSize = 1;
        private static Color lineColor = Color.Black;

        public MainForm() {            
            InitializeComponent();
            Paint += Form1_Paint;
            this.button1.Click += Button1_Click;
            this.button3.Click += Button3_Click;
            this.button4.Click += Button4_Click;
            this.button5.Click += Button5_Click;
            MouseClick += Form1_MouseClick;
        }

        public static Color PointColor {
            get { return pointColor; }
            set { pointColor = value; }
        }
        
        public static int PointSize {
            get { return pointSize; }
            set { pointSize = value; }
        }

        public static Color LineColor {
            get { return lineColor; }
            set { lineColor = value; }
        }
        
        public static int LineSize {
            get { return lineSize; }
            set { lineSize = value; }
        }

        private void Button3_Click(object sender, EventArgs e) {
            bAddPoints = false;
            bDrawCurve = false;
            bDrawPolygon = false;
            var paramsForm = new ParamsForm();
            paramsForm.Show();
        }

        private void Form1_MouseClick(object sender, MouseEventArgs e) {
            if (bAddPoints) {
                arPoints.Add(new Point(e.Location.X, e.Location.Y));
                Refresh();
            }
        }

        private void Form1_Paint(object sender, PaintEventArgs e) {
            Graphics g = e.Graphics;
            Pen pointPen = new Pen(PointColor, PointSize);
            Pen linePen = new Pen(lineColor, lineSize);
            StringFormat sf = new StringFormat();
            sf.LineAlignment = StringAlignment.Center;
            sf.Alignment = StringAlignment.Center;
            if (arPoints.Count > 0) {
                foreach (var p in arPoints) {
                    g.DrawLine(pointPen, p.X, p.Y, p.X + PointSize, p.Y);
                }
            } 
            if (bDrawCurve && arPoints.Count > 1) {
                g.DrawClosedCurve(linePen, arPoints.ToArray());
            } else if (bDrawCurve && arPoints.Count < 2) {
                float x = 150.0F;
                float y = 150.0F;
                float width = 200.0F;
                float height = 50.0F;
                RectangleF drawRect = new RectangleF(x, y, width, height);
                g.DrawString("Недостаточно точек для кривой!", this.Font, Brushes.Black, drawRect, sf);
            }

            if (bDrawPolygon && arPoints.Count > 1) {
                g.DrawPolygon(linePen, arPoints.ToArray());
            }
            else if (bDrawPolygon && arPoints.Count < 2) {
                float x = 150.0F;
                float y = 150.0F;
                float width = 200.0F;
                float height = 50.0F;
                RectangleF drawRect = new RectangleF(x, y, width, height);
                g.DrawString("Недостаточно точек для ломанной!", this.Font, Brushes.Black, drawRect, sf);
            }

        }

        private void Button1_Click(object sender, EventArgs e) {
            bAddPoints = true;
            bDrawCurve = false;
            bDrawPolygon = false;
        }

        private void Button4_Click(object sender, EventArgs e) {
            bDrawCurve = true;
            bDrawPolygon = false;
            Refresh();
        }

        private void Button5_Click(object sender, EventArgs e) {
            bDrawCurve = false;
            bDrawPolygon = true;
            Refresh();
        }
    }
}
