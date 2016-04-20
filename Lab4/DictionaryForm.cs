﻿using Lab4.Dictionary;
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
using System.Xml.Serialization;

namespace Lab4 {
    public partial class DictionaryForm : Form {
        private bool bExit, bLeftShow, bRightShow;
        private RectangleF drawRect;
        private StringFormat sf;
        private Graphics lg, rg;
        private static int currentWord;
        private static ADictionary curD, er, re;
        private Font f;
        private Word leftWord;
        private string rightString;
        private static Edictionary curDict;
        public enum Edictionary { EngRus, RusEng };

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
            this.deleteCardToolStripMenuItem.DropDownOpening += DeleteCardToolStripMenuItem_DropDownOpening;
            this.deleteCardsList.SelectedIndexChanged += DeleteCardsList_SelectedIndexChanged;
            this.testButton.Click += TestButton_Click;
            FormClosing += DictionaryForm_FormClosing;
        }

        private void TestButton_Click(object sender, EventArgs e) {
            var testForm = new TestForm();
            testForm.Show(this);
        }

        private void DeleteCardsList_SelectedIndexChanged(object sender, EventArgs e) {
            string word = this.deleteCardsList.SelectedItem.ToString();
            if (ShowConfirmation("Вы уверены что хотите удалить карточку '" + word + "'?", "Удаление") == DialogResult.Yes) {
                curD.removeFromDict(curD.getWord(word));
                this.toolsToolStripMenuItem.HideDropDown();
                currentWord = 0;
                leftWord = null;
                bLeftShow = false;
                bRightShow = false;
                ShowAlert("Карточка успешно удалена!");
                Refresh();
            }
        }

        private void DeleteCardToolStripMenuItem_DropDownOpening(object sender, EventArgs e) {
            if (curD.Keys.Length <= 0)
                this.deleteCardsList.Text = "Всё уже удалено";
            else
                this.deleteCardsList.Text = "Выберите слово";
            this.deleteCardsList.Items.Clear();
            foreach (var w in curD.Keys)
                this.deleteCardsList.Items.Add(w.Value);
        }

        private void ViewCardsList_SelectedIndexChanged(object sender, EventArgs e) {
            currentWord = Array.IndexOf(curD.Keys, curD.getWord(this.viewCardsList.SelectedItem.ToString()));
            this.toolsToolStripMenuItem.HideDropDown();
            bLeftShow = true;
            bRightShow = false;
            Refresh();
        }

        private void ViewCardsToolStripMenuItem_DropDownOpening(object sender, EventArgs e) {
            if (curD.Keys.Length <= 0)
                this.viewCardsList.Text = "Словарь пуст";
            else
                this.viewCardsList.Text = "Выберите слово";
            this.viewCardsList.Items.Clear();
            foreach (var w in curD.Keys)
                this.viewCardsList.Items.Add(w.Value);
        }

        private void SaveAsToolStripMenuItem_Click(object sender, EventArgs e) {
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();
            saveFileDialog1.Filter = "XML файлы|*.xml|BIN файлы|*.bin";            
            if (saveFileDialog1.ShowDialog() == DialogResult.OK) {
                string fileName = saveFileDialog1.FileName;
                Stream sr = new FileStream(fileName, FileMode.Create);
                XmlSerializer xmlSer = new XmlSerializer(typeof(Row[]));
                Row[] items = new Row[curD.Keys.Length];
                for (int i = 0; i < curD.Keys.Length; i++)
                    items[i] =  new Row() { word = curD.Keys[i], value = curD[curD.Keys[i]].ToList() };
                xmlSer.Serialize(sr, items);
                sr.Close();
                ShowAlert("Словарь успешно сохранен!");
            }
        }

        private void OpenToolStripMenuItem_Click(object sender, EventArgs e) {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            openFileDialog1.Filter = "XML файлы|*.xml|BIN файлы|*.bin";
            openFileDialog1.Title = "Выберите файл";
            if (openFileDialog1.ShowDialog() == DialogResult.OK) {
                string path = openFileDialog1.InitialDirectory + openFileDialog1.FileName;
                XDocument database = XDocument.Load(path);
                if (getDictionaryType(database) != CurrentDictionary) {
                    ShowAlert("Вы пытаетесь загрузить не тот словарь! Нажмите кнопку '" + this.directionButton.Text + "'");
                    return;
                }
                else {
                    if (CurrentDictionary == Edictionary.EngRus) {
                        curD = new EngRusDictionary(path);
                        er = curD;
                    }
                    else if (CurrentDictionary == Edictionary.RusEng) {
                        curD = new RusEngDictionary(path);
                        re = curD;
                    }
                    bRightShow = false;
                    leftWord = null;
                    currentWord = 0;
                    ShowAlert("Словарь успешно загружен!");
                    bLeftShow = true;
                    Refresh();
                }
            }
        }

        private Edictionary getDictionaryType(XDocument d) {
            List<XElement> entries = d.Descendants("Row").ToList();
            string firstWord = entries[0].Element("Word").Element("Value").Value;
            if (Regex.IsMatch(firstWord, "^[a-zA-Z0-9]*$"))
                return Edictionary.EngRus;
            else
                return Edictionary.RusEng;
        }

        public static Edictionary CurrentDictionary {
            get { return curDict; }
        }

        public static ADictionary CurrentD {
            get { return curD; }
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
            data += "Не определенных типов: " + curD.Keys.Where(c => !WordType.KNOWN_TYPES.Contains(c.Type)).Select(x => x).ToList().Count.ToString() + "\n";
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
            }
        }

        private void NextButton_Click(object sender, EventArgs e) {
            if (curD.Keys.Length <= 0)
                ShowAlert("В словаре нет карточек! Нажмите '" + this.toolsToolStripMenuItem.Text + " > " + this.addCardToolStripMenuItem.Text + "' чтобы добавить карточки.");
            else {
                if (curD.Keys.Length <= 0)
                    bLeftShow = false;
                else
                    bLeftShow = true;
                bRightShow = false;
                currentWord += 1;
                if (currentWord >= curD.Keys.Length)
                    currentWord = 0;
                Refresh();
            }
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
                this.Text = "Русско-Английский словарь";
                curD = re;
            }
            else {
                curDict = Edictionary.EngRus;
                this.Text = "Англо-Русский словарь";
                curD = er;
            }
            string leftText = this.leftPanelHeader.Text;
            this.leftPanelHeader.Text = this.rightPanelHeader.Text;
            this.rightPanelHeader.Text = leftText;
            leftWord = null;
            currentWord = 0;
            bRightShow = false;
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