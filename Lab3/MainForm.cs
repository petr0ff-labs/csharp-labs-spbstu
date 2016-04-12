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
        private int iPointToDrag = -1;
        private List<Point> arPoints = new List<Point>();        
        private static bool bAddPoints, bExit, bMove, bDrag;
        private static int pointSize = 5;
        private static Color pointColor = Color.Blue;
        private static int lineSize = 1;
        private static Color lineColor = Color.Black;
        private static string movementType = "С сохранением";
        private static int startingOffsetX = 15;
        private static int startingOffsetY = 15;
        private RectangleF drawRect;
        private StringFormat sf;
        private Graphics g;
        private enum eLineType { None, Curved, Filled, Polygone, Bezier };        
        private eLineType lineType;
        private Timer moveTimer;
        private int interval = 50;
        private int offsetX = 15;
        private int offsetY = 15;
        private int[] randX, randY;

        public MainForm() {
            sf = new StringFormat();
            rnd = new Random();
            moveTimer = new Timer();
            InitializeComponent();
            StartPosition = FormStartPosition.CenterScreen;
            this.drawPanel.Paint += DrawPanelPaint;
            this.pointsButton.Click += PointsDrawClick;
            this.paramsButton.Click += ParamsClick;
            this.curveLineButton.Click += CurveLineDrawClick;
            this.polyLineButton.Click += PolyLineDrawClick;
            this.bezLineButton.Click += BezLineDrawClick;
            this.clearFromButton.Click += ClearFormClick;
            this.fillPointsButton.Click += FillDrawClick;
            this.moveButton.Click += MoveClick;            
            this.exitButton.Click += ExitButton_Click;
            this.drawPanel.MouseClick += DrawPanelClick;
            this.drawPanel.MouseDown += DrawPanelMouseDown;
            this.drawPanel.MouseMove += DrawPanelMouseMove;
            this.drawPanel.MouseUp += DrawPanelMouseUp;
            FormClosing += MainFormClose;
            KeyPreview = true;
            KeyDown += new KeyEventHandler(MainFormKeyDown);
            moveTimer.Interval = interval;
            moveTimer.Tick += TimerTickHandler;
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
            bDrag = false;
            iPointToDrag = -1;
            moveTimer.Stop();
            offsetX = startingOffsetX;
            offsetY = startingOffsetY;
            var paramsForm = new ParamsForm();
            paramsForm.Show();            
        }

        private void DrawPanelClick(object sender, MouseEventArgs e) {
            if (bAddPoints && !bDrag) {
                arPoints.Add(new Point(e.Location.X, e.Location.Y));
                Refresh();
            }
        }

        private void DrawPanelMouseUp(object sender, MouseEventArgs e) {
            bDrag = false;
            iPointToDrag = -1;
        }

        private void DrawPanelMouseMove(object sender, MouseEventArgs e) {
            if (bDrag) {
                arPoints[iPointToDrag] = new Point(e.Location.X, e.Location.Y);
                Refresh();
            }
        }

        private void DrawPanelMouseDown(object sender, MouseEventArgs e) {
            if (arPoints.Count > 0) {
                for (int i = 0; i < arPoints.Count; i++) {
                if (arPoints[i].X <= (e.Location.X + pointSize) && arPoints[i].X >= (e.Location.X - pointSize) && arPoints[i].Y <= (e.Location.Y + pointSize) && arPoints[i].Y >= (e.Location.Y - pointSize))
                    this.iPointToDrag = i;
                }
            
                if (this.iPointToDrag >= 0)
                    bDrag = true;
            }
        }

        private void MoveClick(object sender, EventArgs e) {
            bAddPoints = false;
            if (arPoints.Count == 0) {
                ShowAlert("Сначала добавьте точек!");
                return;
            }
            if (movementType.Equals("Случайный")) {
                randX = new int[arPoints.Count];
                randY = new int[arPoints.Count];
                for (int i=0; i < arPoints.Count; i++) {
                    randX[i] = rnd.Next(-pointSize * 2, pointSize * 2);
                    randY[i] = rnd.Next(-pointSize * 2, pointSize * 2);
                }
            }                          
            moveTimer.Start();            
        }

        private void ChangeCoordinates(int offsetx, int offsety) {            
            for (int i = 0; i < arPoints.Count; i++) {
                arPoints[i] = new Point(arPoints[i].X + offsetx, arPoints[i].Y + offsety);
                if (arPoints[i].X > this.drawPanel.Size.Width - (pointSize + 5)) {
                    offsetX = -offsetx;
                }
                else if (arPoints[i].X < pointSize) {
                    offsetX = -offsetx;
                }
                if (arPoints[i].Y > this.drawPanel.Size.Height - (pointSize + 5)) {
                    offsetY = -offsety;
                }
                else if (arPoints[i].Y < pointSize) {
                    offsetY = -offsety;
                }
            }
        }
        

        private void TimerTickHandler(object sender, EventArgs e) {
            bMove = true;
            if (movementType.Equals("С сохранением"))
                ChangeCoordinates(offsetX, offsetY);
            else {
                for (int i = 0; i < arPoints.Count; i++) {
                    arPoints[i] = new Point(arPoints[i].X + randX[i], arPoints[i].Y + randY[i]);
                    if (arPoints[i].X > this.drawPanel.Size.Width - (pointSize + 5)) {
                        randX[i] = -randX[i];
                    }
                    else if (arPoints[i].X < pointSize) {
                        randX[i] = -randX[i];
                    }
                    if (arPoints[i].Y > this.drawPanel.Size.Height - (pointSize + 5)) {
                        randY[i] = -randY[i];
                    }
                    else if (arPoints[i].Y < pointSize) {
                        randY[i] = -randY[i];
                    }
                }
            }
                
            Refresh();
        }

        private void InitPointsError(string text) {
            g.DrawString(text, this.Font, Brushes.Black, drawRect, sf);
        }

        private void DrawPanelPaint(object sender, PaintEventArgs e) {
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
            bMove = false;
            moveTimer.Stop();
            offsetX = startingOffsetX;
            offsetY = startingOffsetY;
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
            bMove = false;
            moveTimer.Stop();
            offsetX = startingOffsetX;
            offsetY = startingOffsetY;
            if (ShowConfirmation("Вы уверены что хотите очистить форму?", "Очистить") == DialogResult.Yes) {                
                lineType = eLineType.None;
                bAddPoints = false;
                arPoints.Clear();
                Refresh();
            }
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData) {
            switch (keyData) {
                case Keys.Up:
                    if (!bMove) {
                        ChangeCoordinates(0, -15);
                        Refresh();
                    }
                    else {
                        if (offsetX >= 0)
                            offsetX += 10;
                        else
                            offsetX -= 10;
                    }
                    return true;
                case Keys.Down:
                    if (!bMove) {
                        ChangeCoordinates(0, 15);
                        Refresh();
                    }
                    else {
                        if (offsetX >= 0)
                            offsetX += 10;
                        else
                            offsetX -= 10;
                    }
                    return true;
                case Keys.Right:
                    if (!bMove) {
                        ChangeCoordinates(15, 0);
                        Refresh();
                    }
                    else {
                        if (offsetY >= 0)
                            offsetY += 10;
                        else
                            offsetY -= 10;
                    }
                    return true;
                case Keys.Left:
                    if (!bMove) {
                        ChangeCoordinates(-15, 0);
                        Refresh();
                    }
                    else {
                        if (offsetY >= 0)
                            offsetY += 10;
                        else
                            offsetY -= 10;
                    }
                    return true;
                default:
                    return base.ProcessCmdKey(ref msg, keyData);
            }
        }

        private void MainFormKeyDown(object sender, KeyEventArgs e) {
            switch (e.KeyCode) {
                case (Keys.Escape):
                    ClearFormClick(sender, e);
                    break;
                case (Keys.Space):
                    if (!bMove)
                        MoveClick(sender, e);
                    else {
                        bMove = false;
                        moveTimer.Stop();
                    }
                        
                    break;
                case (Keys.Add):
                    if (offsetX >= 0)
                        this.offsetX += 10;
                    else
                        this.offsetX -= 10;
                    if (offsetY >= 0)
                        this.offsetY += 10;
                    else
                        this.offsetY -= 10;
                    break;
                case (Keys.Subtract):
                    if (offsetX > 0)
                        this.offsetX -= 10;
                    else
                        this.offsetX += 10;
                    if (offsetY > 0)
                        this.offsetY -= 10;
                    else
                        this.offsetY += 10;
                    break;
                default:
                    break;
            }
            e.Handled = true;
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
