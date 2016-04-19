﻿using Lab4.Dictionary;
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
    public partial class AddNodeForm : Form {
        private bool bExit, exists;
        private string word, values;

        public AddNodeForm() {
            InitializeComponent();
            StartPosition = FormStartPosition.CenterScreen;
            this.addNodeButton.Click += AddNodeButton_Click;
            this.cancelButton.Click += CancelButton_Click;
            FormClosing += AddNodeForm_Close;
        }        

        private void AddNodeButton_Click(object sender, EventArgs e) {
            word = this.textBox1.Text;
            foreach (var w in DictionaryForm.CurrentD.Keys)
                if (w.Value.Equals(word))
                    exists = true;
            if (!exists) {
                values = this.richTextBox1.Text;
                List<String> words = values.Split(';').ToList();
                Console.WriteLine(words.Count);
                if (DictionaryForm.CurrentDictionary == DictionaryForm.Edictionary.EngRus) {
                    List<RussianWord> dictValues = new List<RussianWord>();
                    EnglishWord w = new EnglishWord(word);
                    foreach (var r in words)
                        dictValues.Add(new RussianWord(r));
                    Console.WriteLine(dictValues.Count);
                    DictionaryForm.CurrentD.addToDict(w, dictValues);
                }
                else if (DictionaryForm.CurrentDictionary == DictionaryForm.Edictionary.RusEng) {
                    List<EnglishWord> dictValues = new List<EnglishWord>();
                    RussianWord w = new RussianWord(word);
                    foreach (var r in words)
                        dictValues.Add(new EnglishWord(r));
                    DictionaryForm.CurrentD.addToDict(w, dictValues);
                }
                DictionaryForm.ShowAlert("Добавлено!");
            }
            else {
                DictionaryForm.ShowAlert("Такое слово уже есть!");
                exists = false;
            }
        }

        private void CancelButton_Click(object sender, EventArgs e) {
            bExit = true;
            this.Close();
            //if (DictionaryForm.ShowConfirmation("Вы уверены что хотите выйти?", "Выход") == DialogResult.Yes)
            //    this.Close();
        }

        private void AddNodeForm_Close(object sender, FormClosingEventArgs e) {
            if (!bExit) {
                e.Cancel = (DictionaryForm.ShowConfirmation("Вы уверены что хотите выйти?", "Выход") == DialogResult.No);
            }
        }
    }
}
