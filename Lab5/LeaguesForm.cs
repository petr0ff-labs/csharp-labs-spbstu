using Lab5.Source;
using Lab5.Teams;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace Lab5 {
    public partial class LeaguesForm : Form {
        private BindingSource bs = new BindingSource();
        League rpl = new League();
        XmlSerializer xmlSer = new XmlSerializer(typeof(List<Team>));

        public LeaguesForm() {            
            InitializeComponent();
            this.bs.DataSource = rpl.Teams;            
            updateData(Config.rplXml);
            this.pictureBoxTeamLogo.SizeMode = PictureBoxSizeMode.StretchImage;
            this.teamsDataGridView.DataSource = bs;           
            this.teamsDataGridView.RowsAdded += TeamsDataGridView_RowsAdded;
            this.teamsDataGridView.AllowUserToAddRows = false;                      
            this.pictureBoxTeamLogo.DataBindings.Add("ImageLocation", bs, "LogoPath", true);
            this.propertyGridTeamDetails.DataBindings.Add("SelectedObject", bs, "");
            this.teamsChart.DataSource = bs;
            this.toolStripSearchComboBox.ComboBox.DataSource = getColumns();
            this.teamsChart.Series[0].XValueMember = "ClubName";
            this.teamsChart.Series[0].YValueMembers = "StadCapacity";
            this.teamsChart.Legends.Clear();
            this.teamsChart.Titles.Add("Вместимость стадиона");
            this.Load += Leagues_Load;
            this.toolStripSaveButton.Click += ToolStripSaveButton_Click;
            this.toolStripOpenButton.Click += ToolStripOpenButton_Click;
            this.toolStripSearchTextBox.TextChanged += ToolStripSearchTextBox_TextChanged;
            bs.CurrentChanged += (o, e) => this.teamsChart.DataBind();
        }

        private List<string> getColumns() {
            List<string> columns = new List<string>();
            for (int i = 0; i < this.teamsDataGridView.Columns.Count - 1; i++) {
                columns.Add(this.teamsDataGridView.Columns[i].HeaderCell.Value.ToString());
            }
            return columns;
        }

        private void ToolStripSearchTextBox_TextChanged(object sender, EventArgs e) {            
            int colIndex = 0;
            for (int i = 0; i < this.teamsDataGridView.Columns.Count; i++) {
                if (this.toolStripSearchComboBox.SelectedItem.ToString().Equals(this.teamsDataGridView.Columns[i].HeaderCell.Value.ToString())) {
                    colIndex = i;
                    break;
                }
            }
            teamsDataGridView.CurrentCell = null;
            for (int i = 0; i < teamsDataGridView.Rows.Count; i++)
                teamsDataGridView.Rows[i].Visible = teamsDataGridView[colIndex, i].Value.ToString().Contains(toolStripSearchTextBox.Text.Trim());

            if (toolStripSearchTextBox.Text.Equals("")) {
                for (int i = 0; i < teamsDataGridView.Rows.Count; i++)
                    teamsDataGridView.Rows[i].Visible = true;
            }
        }

        private void TeamsDataGridView_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e) {
            this.teamsChart.Update();
        }

        private void ToolStripOpenButton_Click(object sender, EventArgs e) {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            openFileDialog1.Filter = "XML файлы|*.xml|BIN файлы|*.bin";
            openFileDialog1.Title = "Выберите файл";
            if (openFileDialog1.ShowDialog() == DialogResult.OK) {
                string path = openFileDialog1.InitialDirectory + openFileDialog1.FileName;
                var extension = Path.GetExtension(path);
                switch (extension.ToLower()) {
                    case ".xml":
                        bs.Clear();
                        updateData(path);
                        break;
                    default:
                        throw new ArgumentOutOfRangeException(extension);
                }
            }
        }

        private void updateData(string path) {
            XDocument database = XDocument.Load(path);
            List<XElement> entries = database.Descendants("Team").ToList();
            foreach (var r in entries) {
                List<Player> players = new List<Player>();
                if (r.Element("Players") != null) {
                    List<XElement> vals = r.Element("Players").Elements().ToList();
                    
                    foreach (var w in vals)
                        players.Add(new Player(w.Element("LastName").Value,
                                               Double.Parse(w.Element("Height").Value),
                                               Double.Parse(w.Element("Weight").Value),
                                               Int32.Parse(w.Element("Caps").Value),
                                               Int32.Parse(w.Element("Goals").Value),
                                               w.Element("Country").Value));
                }
                bs.Add(new Team(r.Element("ClubName").Value,
                                r.Element("City").Value,
                                Int32.Parse(r.Element("Foundation").Value),
                                r.Element("HeadCoach").Value,
                                r.Element("LogoPath").Value,
                                players,
                                r.Element("Stadium").Value,
                                Int32.Parse(r.Element("StadCapacity").Value)));
            }
            this.bs.AllowNew = true;
            this.teamsDataGridView.DataSource = bs;
            this.teamsDataGridView.Columns["LogoPath"].Visible = false;
            //foreach (var p in this.propertyGridTeamDetails.DataBindings)
            //    Console.WriteLine(p.ToString());
            this.bindingNavigator1.BindingSource = bs;
        }

        private void ToolStripSaveButton_Click(object sender, EventArgs e) {
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();
            saveFileDialog1.Filter = "XML файлы|*.xml|BIN файлы|*.bin";
            if (saveFileDialog1.ShowDialog() == DialogResult.OK) {
                string fileName = saveFileDialog1.FileName;
                var extension = Path.GetExtension(fileName);
                Stream sr = new FileStream(fileName, FileMode.Create);
                switch (extension.ToLower()) {
                    case ".xml":
                        xmlSer.Serialize(sr, rpl.Teams);
                        break;
                    default:
                        throw new ArgumentOutOfRangeException(extension);
                }
                sr.Close();
            }
        }

        private void Leagues_Load(object sender, EventArgs e) {
            
        }

        private List<Player> addPlayers(string cname) {
            List <Player> pl = new List<Player>();
            return pl;
        }
    }
}
