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
                      
        public ParamsForm() {
            try {
                colors.Add("Синий", Color.Blue);
                colors.Add("Чёрный", Color.Black);
                colors.Add("Красный", Color.Red);
                colors.Add("Жёлтый", Color.Yellow);
            } catch(ArgumentException) { }
                        
            InitializeComponent();
            this.button1.Click += Button1_Click;
            this.button2.Click += Button2_Click;
        }        

        private void Button1_Click(object sender, EventArgs e) {
            string pointColorSelected = this.comboBox1.SelectedItem.ToString();
            string pointSizeSelected = this.comboBox2.SelectedItem.ToString();
            string lineColorSelected = this.comboBox3.SelectedItem.ToString();
            string lineSizeSelected = this.comboBox4.SelectedItem.ToString();
            MainForm.PointColor = colors[pointColorSelected];
            MainForm.PointSize = Int32.Parse(pointSizeSelected);
            MainForm.LineColor = colors[lineColorSelected];
            MainForm.LineSize = Int32.Parse(lineSizeSelected);
            this.Close();
        }

        private void Button2_Click(object sender, EventArgs e) {
            this.Close();
        }        
        
    }
}
