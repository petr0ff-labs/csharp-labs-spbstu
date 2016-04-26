using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lab5 {
    public partial class Leagues : Form {
        private BindingSource bs = new BindingSource();
        public Leagues() {
            InitializeComponent();
            this.Load += Leagues_Load;
        }

        private void Leagues_Load(object sender, EventArgs e) {
            this.teamsDataGridView.DataSource = bs;
        }
    }
}
