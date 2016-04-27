using Lab5.Source;
using Lab5.Teams;
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
        League rpl = new League();
        public Leagues() {
            //this.Font = new Font("HP Simplified", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            InitializeComponent();
            this.Load += Leagues_Load;            
            bs.CurrentChanged += (o, e) => this.teamsChart.DataBind();
        }

        private void Leagues_Load(object sender, EventArgs e) {
            List<Player> zenit_players = new List<Player>();
            List<Player> cska_players = new List<Player>();
            zenit_players.Add(new Player("Халк", 180, 90, 100, 50, "Бразилия"));
            cska_players.Add(new Player("Акинфеев", 185, 80, 100, -50, "Россия"));
            rpl.Teams.Add(new Team("Зенит", "СПБ", 1905, "А. Виллаш-Боаш", Config.logosPath + "zenit.jpg", zenit_players, 100000000));
            rpl.Teams.Add(new Team("ЦСКА", "Москва", 1905, "Л. Слуцкий", Config.logosPath + "cska_m-pr.jpg", cska_players, 80000000));
            bs.DataSource = rpl.Teams;
            this.pictureBoxTeamLogo.SizeMode = PictureBoxSizeMode.StretchImage;
            this.teamsDataGridView.DataSource = bs;
            this.pictureBoxTeamLogo.DataBindings.Add("Image", bs, "Logo", true);
            this.propertyGridTeamDetails.DataBindings.Add("SelectedObject", bs, "");
            this.teamsChart.DataSource = bs;
            this.teamsChart.Series[0].XValueMember = "ClubName";
            this.teamsChart.Series[0].YValueMembers = "Budget";
            this.teamsChart.Legends.Clear();
            this.teamsChart.Titles.Add("Бюджет (доллары США)");
        }

        private List<Player> addPlayers(string cname) {
            List <Player> pl = new List<Player>();
            return pl;
        }
    }
}
