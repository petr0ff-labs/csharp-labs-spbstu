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
    public partial class AddNodeForm : Form {
        private bool bExit;
        private string word, values;
        private List<RussianWord> dictValues;

        public AddNodeForm() {
            InitializeComponent();
            StartPosition = FormStartPosition.CenterScreen;
            this.addNodeButton.Click += AddNodeButton_Click;
            this.cancelButton.Click += CancelButton_Click;
            FormClosing += AddNodeForm_Close;
        }
        
        private void AddNodeButton_Click(object sender, EventArgs e) {
            word = this.textBox1.Text;
            List<String> words = values.Split(';').ToList();
            dictValues = new List<RussianWord>();
            foreach (var r in words)
                dictValues.Add(new RussianWord(r));
            
        }

        private void CancelButton_Click(object sender, EventArgs e) {
            bExit = true;
            if (DictionaryForm.ShowConfirmation("Вы уверены что хотите выйти?", "Выход") == DialogResult.Yes)
                this.Close();
        }

        private void AddNodeForm_Close(object sender, FormClosingEventArgs e) {
            if (!bExit) {
                e.Cancel = (DictionaryForm.ShowConfirmation("Вы уверены что хотите выйти?", "Выход") == DialogResult.No);
            }
        }
    }
}
