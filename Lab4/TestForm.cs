using Lab4.Test;
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
    public partial class TestForm : Form {
        private int qCount = 0;
        private RunTest t;

        public TestForm() {
            List<Question> qs = new List<Question>();
            int count;
            t = new RunTest(DictionaryForm.CurrentD);

            if (DictionaryForm.CurrentD.Keys.Length < 5)
                count = DictionaryForm.CurrentD.Keys.Length;
            else
                count = DictionaryForm.CurrentD.Keys.Length - (int)(DictionaryForm.CurrentD.Keys.Length * 0.75);
            Console.WriteLine(count);
            for (int i = 0; i < count; i++) {
                var w = DictionaryForm.CurrentD.Keys[i];
                qs.Add(new Question(w, DictionaryForm.CurrentD.getValues(DictionaryForm.CurrentD[w])));
            }
            t.Questions = qs;            
            InitializeComponent();
            StartPosition = FormStartPosition.CenterScreen;
            this.answerButton.Click += AnswerButton_Click;     
        }

        private void AnswerButton_Click(object sender, EventArgs e) {
            if (this.answerTextBox.Text.Equals(""))
                DictionaryForm.ShowAlert("Вы не ответили!");
            else {
                if (isCorrectAnswer(this.answerTextBox.Text))
                    t.CorrectAnswers += 1;
                else
                    Console.WriteLine(this.answerTextBox.Text + " " + t.Questions[qCount].Answer);
                qCount++;
                ShowQuestion();
            }
        }

        private bool isCorrectAnswer(string a) {
            return (t.Questions[qCount].Answer.Contains(char.ToUpper(a[0]) + a.Substring(1)));
        }

        protected override void OnLoad(EventArgs e) {
            base.OnLoad(e);
            ShowQuestion();
        }

        private void ShowQuestion() {
            if (qCount >= t.Questions.Count) {
                DialogResult res = DictionaryForm.ShowConfirmation("Вы ответили на все вопросы! Правильных ответов: " + t.CorrectAnswers + ". Пройти тест еще раз?", "Результаты");
                if (res == DialogResult.Yes) {
                    qCount = 0;
                    ShowQuestion();
                } else {
                    this.Close();
                }
            }
            else {
                this.countLabel.Text = "Вопрос " + (qCount + 1) + " из " + t.Questions.Count;
                this.questionLabel.Text = t.Questions[qCount].TestQuestion;
                this.answerTextBox.Text = "";
            }
        }
    }
}
