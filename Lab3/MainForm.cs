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
        private Random rnd;
        private List<Point> arPoints = new List<Point>();        
        private static bool bAddPoints, bExit, moving;
        private static int pointSize = 5;
        private static Color pointColor = Color.Blue;
        private static int lineSize = 1;
        private static Color lineColor = Color.Black;
        private static string movementType = "С сохранением";
        private RectangleF drawRect;
        private StringFormat sf;
        private Graphics g;
        private enum eLineType { None, Curved, Filled, Polygone, Bezier };        
        private eLineType lineType;
        private Timer moveTimer;
        private int interval = 50;
        private static int startingOffset = 15;
        private static int startingInterval = 50;
        private int offsetX = 15;
        private int offsetY = 15;

        public MainForm() {
            sf = new StringFormat();
            rnd = new Random();
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

        public static string MovementType {
            get { return movementType; }
            set { movementType = value; }
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
            moving = false;
            offsetX = startingOffset;
            offsetY = startingOffset;
            interval = startingInterval;
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
            if (!moving) {
                moveTimer.Interval = interval;
                moveTimer.Tick += TimerTickHandler;
                moveTimer.Start();
            }
        }

        private void ChangeCoordinates(int offsetx, int offsety) {            
            for (int i = 0; i < arPoints.Count; i++) {                
                arPoints[i] = new Point(arPoints[i].X + offsetx, arPoints[i].Y + offsety);
                if (arPoints[i].X > this.drawPanel.Size.Width - pointSize) {
                    offsetX = -offsetx;
                }
                else if (arPoints[i].X < pointSize) {
                    offsetX = -offsetx;
                }
                if (arPoints[i].Y > this.drawPanel.Size.Height - pointSize) {
                    offsetY = -offsety;
                }
                else if (arPoints[i].Y < pointSize) {
                    offsetY = -offsety;
                }
            }
        }

        private void RandomMovement(int offsetx, int offsety) {
            for (int i = 0; i < arPoints.Count; i++) {
                arPoints[i] = new Point(arPoints[i].X + offsetx, arPoints[i].Y + offsety);
                if (arPoints[i].X > this.drawPanel.Size.Width - pointSize) {
                    offsetx = -offsetx;
                }
                else if (arPoints[i].X < pointSize) {
                    offsetx = -offsetx;
                }
                if (arPoints[i].Y > this.drawPanel.Size.Height - pointSize) {
                    offsety = -offsety;
                }
                else if (arPoints[i].Y < pointSize) {
                    offsety = -offsety;
                }
            }
        }

        private void TimerTickHandler(object sender, EventArgs e) {
            moving = true;
            if (movementType.Equals("С сохранением"))
                ChangeCoordinates(offsetX, offsetY);
            else
                RandomMovement(rnd.Next(pointSize, this.drawPanel.Width), rnd.Next(pointSize, this.drawPanel.Height));
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
                InitPointsError("Нажмите кнопку \"Точки\" и добавьте точек на панель.");
            }
        }

        private void PointsDrawClick(object sender, EventArgs e) {
            moving = false;
            moveTimer.Stop();
            offsetX = startingOffset;
            offsetY = startingOffset;
            interval = startingInterval;
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
            moving = false;
            moveTimer.Stop();            
            if (ShowConfirmation("Вы уверены что хотите очистить форму?", "Очистить") == DialogResult.Yes) {
                offsetX = startingOffset;
                offsetY = startingOffset;
                interval = startingInterval;
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
                    this.offsetX -= 10;
                    this.offsetY -= 10;
                    break;
                case (Keys.S):
                    this.offsetX += 10;
                    this.offsetY += 10;
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
