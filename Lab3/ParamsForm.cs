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
    public partial class ParamsForm : Form {
        public static Dictionary<string, Color> colors = new Dictionary<string, Color>();
        private string  pointColorSelected,
                        pointSizeSelected,
                        lineColorSelected,
                        lineSizeSelected,
                        movementSelected;
        private static bool isChanged;
        private bool bExit;

        public static bool IsChanged{
            get { return isChanged; }
        }

        //public static bool isShown = true;

        public ParamsForm() {
            isChanged = false;
            try {
                colors.Add("Синий", Color.Blue);
                colors.Add("Чёрный", Color.Black);
                colors.Add("Красный", Color.Red);
                colors.Add("Жёлтый", Color.Yellow);
            } catch(ArgumentException) { }
                        
            InitializeComponent();
            this.saveButton.Click += SaveClick;
            this.cancelButton.Click += CancelClick;
            FormClosing += ParamsFormClose;
        }        

        private void SaveClick(object sender, EventArgs e) {
            pointColorSelected = (this.comboBox1.SelectedItem == null) ? "Синий" : this.comboBox1.SelectedItem.ToString();
            pointSizeSelected = (this.comboBox2.SelectedItem == null) ? "5" : this.comboBox2.SelectedItem.ToString();
            lineColorSelected = (this.comboBox3.SelectedItem == null) ? "Чёрный" : this.comboBox3.SelectedItem.ToString();
            lineSizeSelected = (this.comboBox4.SelectedItem == null) ? "1" : this.comboBox4.SelectedItem.ToString();
            movementSelected = (this.comboBox5.SelectedItem == null) ? "С сохранением" : this.comboBox5.SelectedItem.ToString();
            MainForm.PointColor = colors[pointColorSelected];
            MainForm.PointSize = Int32.Parse(pointSizeSelected);
            MainForm.LineColor = colors[lineColorSelected];
            MainForm.LineSize = Int32.Parse(lineSizeSelected);
            MainForm.MovementType = movementSelected;
            bExit = true;
            isChanged = true;
            this.Close();            
        }

        private void CancelClick(object sender, EventArgs e) {
            bExit = true;
            if (MainForm.ShowConfirmation("Вы уверены что хотите выйти из настроек?", "Выход") == DialogResult.Yes)
                this.Close();
        }
        private void ParamsFormClose(object sender, FormClosingEventArgs e) {
            if (!bExit) {
                e.Cancel = (MainForm.ShowConfirmation("Вы уверены что хотите выйти из настроек?", "Выход") == DialogResult.No);
            }
        }
    }
}
