using Lab4.Dictionary;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lab4 {    
    public partial class DictionaryForm : Form {
        private bool bExit, bLeftShow, bRightShow, bChangeDir;
        private RectangleF drawRect;
        private StringFormat sf;
        private Graphics lg, rg;
        private static int currentWord;
        private static ADictionary curD, er, re;
        private Font f;
        private Word leftWord;
        private string rightString;
        private enum Edictionary { EngRus, RusEng };
        private Edictionary curDict;

        public DictionaryForm() {
            sf = new StringFormat();
            StartPosition = FormStartPosition.CenterScreen;
            curDict = Edictionary.EngRus;
            er = new EngRusDictionary();
            re = new RusEngDictionary();
            curD = er;            
            f = new Font("HP Simplified", 16F, FontStyle.Regular, GraphicsUnit.Point, ((byte)(204)));
            currentWord = 0;
            InitializeComponent();
            this.leftPanel.Paint += LeftPanel_Paint;
            this.rightPanel.Paint += RightPanel_Paint;
            this.nextButton.Click += NextButton_Click;
            this.translateButton.Click += TranslateButton_Click;
            this.directionButton.Click += DirectionButton_Click;
            this.exitToolStripMenuItem.Click += ExitToolStripMenuItem_Click;
            FormClosing += DictionaryForm_FormClosing;
            
        }        

        private void LeftPanel_Paint(object sender, PaintEventArgs e) {
            lg = e.Graphics;
            InitRect();
            if (bLeftShow) {
                leftWord = curD.Keys[currentWord];
                lg.DrawString(leftWord.Value, f, Brushes.Black, drawRect, sf);
                currentWord += 1;
                if (currentWord >= curD.Keys.Length)
                    currentWord = 0;
            }            
        }

        private void RightPanel_Paint(object sender, PaintEventArgs e) {
            rg = e.Graphics;
            InitRect();
            if (bRightShow) {                
                rightString = curD.getValues(curD[leftWord]);
                rg.DrawString(rightString, f, Brushes.Black, drawRect, sf);
                currentWord += 1;
                if (currentWord >= curD.Keys.Length)
                    currentWord = 0;
            }
        }        

        private void NextButton_Click(object sender, EventArgs e) {
            bLeftShow = true;
            bRightShow = false;
            Refresh();
        }

        private void TranslateButton_Click(object sender, EventArgs e) {
            bRightShow = true;
            this.rightPanel.Refresh();
        }

        private void DirectionButton_Click(object sender, EventArgs e) {
            if (curDict == Edictionary.EngRus) {
                curDict = Edictionary.RusEng;
                curD = re;
            }
            else {
                curDict = Edictionary.EngRus;
                curD = er;
            }
            string leftText = this.leftPanelHeader.Text;
            this.leftPanelHeader.Text = this.rightPanelHeader.Text;
            this.rightPanelHeader.Text = leftText;
            bRightShow = false;
            bChangeDir = true;
            Refresh();
        }

        private void DictionaryForm_FormClosing(object sender, FormClosingEventArgs e) {
            if (!bExit) {
                e.Cancel = (ShowConfirmation("Вы уверены что хотите выйти?", "Выход") == DialogResult.No);
            }
        }

        private void ExitToolStripMenuItem_Click(object sender, EventArgs e) {
            if (ShowConfirmation("Вы уверены что хотите выйти?", "Выход") == DialogResult.Yes) {
                bExit = true;
                this.Close();
            }
        }


        public static DialogResult ShowConfirmation(string text, string caption) {
                return MessageBox.Show(text, caption,
                                 MessageBoxButtons.YesNo,
                                 MessageBoxIcon.Question);
        }
        private void InitRect() {
            sf.LineAlignment = StringAlignment.Center;
            sf.Alignment = StringAlignment.Center;
            float x = (this.leftPanel.Location.X / 2) - 50;
            float y = this.leftPanel.Location.Y / 2;
            float width = 200.0F;
            float height = 50.0F;
            drawRect = new RectangleF(x, y, width, height);
        }
    }
    
}
