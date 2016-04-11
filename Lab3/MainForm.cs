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
        private static bool bAddPoints, bExit;
        private static int pointSize = 5;
        private static Color pointColor = Color.Blue;
        private static int lineSize = 1;
        private static Color lineColor = Color.Black;
        private RectangleF drawRect;
        private StringFormat sf;
        private Graphics g;
        private enum eLineType { None, Curved, Filled, Polygone, Bezier };
        private eLineType lineType;
        private Timer moveTimer;
        private static int interval = 200;
        private int offsetX = 60;
        private int offsetY = 60;

        public MainForm() {
            sf = new StringFormat();
            moveTimer = new Timer();
            InitializeComponent();
            StartPosition = FormStartPosition.CenterScreen;
            this.drawPanel.Paint += MainFormPaint;
            this.pointsButton.Click += PointsDrawClick;
            this.paramsButton.Click += ParamsClick;
            this.curveLineButton.Click += CurveLineDrawClick;
            this.polyLineButton.Click += PolyLineDrawClick;
            this.bezLineButton.Click += BezLineDrawClick;
            this.clearFromButton.Click += ClearFormClick;
            this.fillPointsButton.Click += FillDrawClick;
            this.moveButton.Click += MoveClick;            
            this.exitButton.Click += ExitButton_Click;
            this.drawPanel.MouseClick += OnFormClick;
            FormClosing += MainFormClose;
            KeyPreview = true;
            KeyDown += new KeyEventHandler(MainFormKeyDown);
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

        private void InitRect() {            
            sf.LineAlignment = StringAlignment.Center;
            sf.Alignment = StringAlignment.Center;
            float x = (this.drawPanel.Location.X / 2) - 50;
            float y = this.drawPanel.Location.Y / 2;
            float width = 200.0F;
            float height = 50.0F;
            drawRect = new RectangleF(x, y, width, height);
        }
        
        private void ParamsClick(object sender, EventArgs e) {
            moveTimer.Stop();
            var paramsForm = new ParamsForm();
            paramsForm.Show();
        }

        private void OnFormClick(object sender, MouseEventArgs e) {
            if (bAddPoints) {
                arPoints.Add(new Point(e.Location.X, e.Location.Y));
                Refresh();
            }
        }

        private void MoveClick(object sender, EventArgs e) {
            bAddPoints = false;
            if (arPoints.Count == 0) {
                ShowAlert("Сначала добавьте точек!");
                return;
            }                   
            moveTimer.Interval = interval;
            moveTimer.Tick += TimerTickHandler;
            moveTimer.Start();            
        }

        private void TimerTickHandler(object sender, EventArgs e) {
            for (int i = 0; i < arPoints.Count; i++) {
                arPoints[i] = new Point(arPoints[i].X + offsetX, arPoints[i].Y + offsetY);
                if ((this.drawPanel.Size.Width) < (arPoints[i].X + 5)) {
                    offsetX = -offsetX;
                }
                if (arPoints[i].X < 20) {
                    offsetX = -offsetX;
                }
                if ((this.drawPanel.Size.Height) < (arPoints[i].Y + 10)) {
                    offsetY = -offsetY;
                }
                if (arPoints[i].Y < 40) {
                    offsetY = -offsetY;
                }
            }
            Refresh();
        }

        private void InitPointsError(string text) {
            g.DrawString(text, this.Font, Brushes.Black, drawRect, sf);
        }

        private void MainFormPaint(object sender, PaintEventArgs e) {
            g = e.Graphics;
            Pen pointPen = new Pen(PointColor, PointSize);
            Pen linePen = new Pen(lineColor, lineSize);
            SolidBrush brush = new SolidBrush(lineColor);
            InitRect();
            if (arPoints.Count > 0) {
                foreach (var p in arPoints) {
                    g.DrawLine(pointPen, p.X, p.Y, p.X + PointSize, p.Y);
                }
                if (lineType == eLineType.Bezier) {
                    try {
                        g.DrawBeziers(linePen, arPoints.ToArray());
                    }
                    catch (ArgumentException) {
                        InitPointsError("Недостаточно точек для кривой Безье! Количество точек должно быть кратным трём плюс один");
                    }
                } 
                else if (lineType == eLineType.Curved) {
                    try {
                        g.DrawClosedCurve(linePen, arPoints.ToArray());
                    }
                    catch (ArgumentException) {
                        InitPointsError("Недостаточно точек для кривой!");
                    }
                }
                else if (lineType == eLineType.Filled) {
                    try {
                        g.FillClosedCurve(brush, arPoints.ToArray());
                    }
                    catch (ArgumentException) {
                        InitPointsError("Недостаточно точек для заполнения!");
                    }
                }
                else if (lineType == eLineType.Polygone) {
                    try {
                        g.DrawPolygon(linePen, arPoints.ToArray());
                    }
                    catch (ArgumentException) {
                        InitPointsError("Недостаточно точек для ломанной!");
                    }
                }
            } else {
                InitPointsError("Нажмите кнопку \"Точки\" и добавьте точек.");
            }
        }

        private void PointsDrawClick(object sender, EventArgs e) {
            moveTimer.Stop();
            bAddPoints = true;
        }

        private void CurveLineDrawClick(object sender, EventArgs e) {
            lineType = eLineType.Curved;
            Refresh();
        }

        private void PolyLineDrawClick(object sender, EventArgs e) {
            lineType = eLineType.Polygone;
            Refresh();
        }

        private void FillDrawClick(object sender, EventArgs e) {
            lineType = eLineType.Filled;
            Refresh();
        }

        private void BezLineDrawClick(object sender, EventArgs e) {
            lineType = eLineType.Bezier;
            Refresh();
        }

        private void ClearFormClick(object sender, EventArgs e) {
            if (arPoints.Count == 0) {
                ShowAlert("Форма чиста");
                return;
            }
            moveTimer.Stop();
            if (ShowConfirmation("Вы уверены что хотите очистить форму?", "Очистить") == DialogResult.Yes) {
                lineType = eLineType.None;
                bAddPoints = false;
                arPoints.Clear();
                Refresh();
            }
        }

        private void MainFormKeyDown(object sender, KeyEventArgs e) {
            switch (e.KeyCode) {
                case (Keys.Escape):
                    ClearFormClick(sender, e);
                    break;
                case (Keys.Space):
                    MoveClick(sender, e);
                    break;
                case (Keys.A):
                    interval -= 10;
                    TimerTickHandler(sender, e);
                    break;
                case (Keys.S):
                    interval += 20;
                    TimerTickHandler(sender, e);
                    break;
                default:
                    break;
            }
        }

        public static DialogResult ShowConfirmation(string text, string caption) {
            return MessageBox.Show(text, caption,
                             MessageBoxButtons.YesNo,
                             MessageBoxIcon.Question);
        }

        public static void ShowAlert(string text) {
            MessageBox.Show(text);
        }

        private void ExitButton_Click(object sender, EventArgs e) {
            if (ShowConfirmation("Вы уверены что хотите выйти?", "Выход") == DialogResult.Yes) {
                bExit = true;
                this.Close();
            }
        }

        private void MainFormClose(object sender, FormClosingEventArgs e) {
            if (!bExit) {
                e.Cancel = (ShowConfirmation("Вы уверены что хотите выйти?", "Выход") == DialogResult.No);
            }
        }
    }
}
