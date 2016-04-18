using Lab4.Dictionary;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace Lab4 {    
    public partial class DictionaryForm : Form {
        private bool bExit, bLeftShow, bRightShow, bChangeDir, bSelected;
        private RectangleF drawRect;
        private StringFormat sf;
        private Graphics lg, rg;
        private static int currentWord;
        public static ADictionary curD, er, re;
        private Font f;
        private Word leftWord;
        private string rightString;
        public enum Edictionary { EngRus, RusEng };
        private static Edictionary curDict;

        public DictionaryForm() {
            sf = new StringFormat();
            StartPosition = FormStartPosition.CenterScreen;
            curDict = Edictionary.EngRus;
            er = new EngRusDictionary();
            re = new RusEngDictionary();
            curD = er;            
            f = new Font("HP Simplified", 12F, FontStyle.Regular, GraphicsUnit.Point, ((byte)(204)));
            currentWord = 0;
            InitializeComponent();
            this.leftPanel.Paint += LeftPanel_Paint;
            this.rightPanel.Paint += RightPanel_Paint;
            this.nextButton.Click += NextButton_Click;
            this.translateButton.Click += TranslateButton_Click;
            this.directionButton.Click += DirectionButton_Click;
            this.statsToolStripMenuItem.Click += StatsToolStripMenuItem_Click;
            this.addCardToolStripMenuItem.Click += AddCardToolStripMenuItem_Click;
            this.exitToolStripMenuItem.Click += ExitToolStripMenuItem_Click;
            this.openToolStripMenuItem.Click += OpenToolStripMenuItem_Click;
            this.saveAsToolStripMenuItem.Click += SaveAsToolStripMenuItem_Click;
            this.viewCardsToolStripMenuItem.DropDownOpening += ViewCardsToolStripMenuItem_DropDownOpening;
            this.viewCardsList.SelectedIndexChanged += ViewCardsList_SelectedIndexChanged;
            FormClosing += DictionaryForm_FormClosing;            
        }

        private void ViewCardsList_SelectedIndexChanged(object sender, EventArgs e) {
            Console.WriteLine(this.viewCardsList.SelectedItem.ToString());   
            currentWord = Array.IndexOf(curD.Keys, curD.getWord(this.viewCardsList.SelectedItem.ToString()));
            Console.WriteLine(currentWord);
            this.toolsToolStripMenuItem.HideDropDown();
            bLeftShow = true;
            bRightShow = false;
            bSelected = true;
            Refresh();
        }

        private void ViewCardsToolStripMenuItem_DropDownOpening(object sender, EventArgs e) {
            this.viewCardsList.Text = "Выберите слово";
            this.viewCardsList.Items.Clear();
            foreach (var w in curD.Keys)
                this.viewCardsList.Items.Add(w.Value);            
        }

        private void DeleteCardToolStripMenuItem_Click(object sender, EventArgs e) {
            throw new NotImplementedException();
        }

        private void SaveAsToolStripMenuItem_Click(object sender, EventArgs e) {
            throw new NotImplementedException();
        }

        private void OpenToolStripMenuItem_Click(object sender, EventArgs e) {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            openFileDialog1.Filter = "XML файлы|*.xml";
            openFileDialog1.Title = "Выберите XML файл";
            if (openFileDialog1.ShowDialog() == DialogResult.OK) {
                string path = openFileDialog1.InitialDirectory + openFileDialog1.FileName;
                XDocument database = XDocument.Load(path);
                List<XElement> entries = database.Descendants("Row").ToList();
                string firstWord = entries[0].Elements().ToList()[0].Value;
                if ((Regex.IsMatch(firstWord, "^[a-zA-Z0-9]*$") && CurrentDictionary == Edictionary.RusEng) || !Regex.IsMatch(firstWord, "^[a-zA-Z0-9]*$") && CurrentDictionary == Edictionary.EngRus) {
                    ShowAlert("Вы пытаетесь загрузить не тот словарь! Нажмите кнопку '" + this.directionButton.Text + "'");
                    return;
                } else {
                    if (CurrentDictionary == Edictionary.EngRus) {
                        curD = new EngRusDictionary(path);
                        er = curD;
                    } else if (CurrentDictionary == Edictionary.RusEng) {
                        curD = new RusEngDictionary(path);
                        re = curD;
                    }
                    bLeftShow = false;
                    bRightShow = false;
                    bSelected = false;
                    leftWord = null;
                    currentWord = 0;
                    Refresh();
                }
            }
        }

        public static Edictionary CurrentDictionary {
            get { return curDict; }
        }
        
        private void AddCardToolStripMenuItem_Click(object sender, EventArgs e) {
            var addNodeForm = new AddNodeForm();
            addNodeForm.Show(this);
        }

        private void StatsToolStripMenuItem_Click(object sender, EventArgs e) {
            string data = "Статистика:\n";
            data += "Всего слов в словаре: " + curD.Keys.Length.ToString() + "\n";
            data += "Существительных: " + curD.Keys.Where(c => c.Type.Equals(WordType.NOUN)).Select(x => x).ToList().Count.ToString() + "\n";
            data += "Прилагательных: " + curD.Keys.Where(c => c.Type.Equals(WordType.ADJECTIVE)).Select(x => x).ToList().Count.ToString() + "\n";
            data += "Глаголов: " + curD.Keys.Where(c => c.Type.Equals(WordType.VERB)).Select(x => x).ToList().Count.ToString() + "\n";
            data += "Наречий: " + curD.Keys.Where(c => c.Type.Equals(WordType.ADVERB)).Select(x => x).ToList().Count.ToString() + "\n";
            data += "Выражений: " + curD.Keys.Where(c => c.Type.Equals(WordType.EXPRESSION)).Select(x => x).ToList().Count.ToString() + "\n";
            data += "Не определенных типов: " + curD.Keys.Where(c => c.Type.Equals("")).Select(x => x).ToList().Count.ToString() + "\n";
            ShowAlert(data);
        }

        private void LeftPanel_Paint(object sender, PaintEventArgs e) {
            lg = e.Graphics;
            InitRect();
            if (bLeftShow) {
                leftWord = curD.Keys[currentWord];
                lg.DrawString(leftWord.Value, f, Brushes.Black, drawRect, sf);                                    
            }            
        }

        private void RightPanel_Paint(object sender, PaintEventArgs e) {
            rg = e.Graphics;
            InitRect();
            if (bRightShow) {                
                rightString = curD.getValues(curD[leftWord]);
                rg.DrawString(rightString, f, Brushes.Black, drawRect, sf);
                if (!bSelected)
                    currentWord += 1;
                if (currentWord >= curD.Keys.Length)
                    currentWord = 0;
            }
        }        

        private void NextButton_Click(object sender, EventArgs e) {
            bLeftShow = true;
            bRightShow = false;
            bSelected = false;
            currentWord += 1;
            if (currentWord >= curD.Keys.Length)
                currentWord = 0;
            Refresh();
        }

        private void TranslateButton_Click(object sender, EventArgs e) {
            if (leftWord != null) {
                bRightShow = true;
                this.rightPanel.Refresh();
            }
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
            leftWord = null;
            currentWord = 0;
            bSelected = false;
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

        public static void ShowAlert(string text) {
            MessageBox.Show(text);
        }

        private void InitRect() {
            sf.LineAlignment = StringAlignment.Center;
            sf.Alignment = StringAlignment.Center;
            float x = this.leftPanel.Location.X / 2;
            float y = this.leftPanel.Location.Y / 2;
            float width = 200.0F;
            float height = 150.0F;
            drawRect = new RectangleF(x, y, width, height);
        }
    }    
}
