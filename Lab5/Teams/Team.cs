using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;

namespace Lab5.Teams {
    public class Team {
        private List<Player> players;
        private string clubName;
        private string city;
        private int foundation;
        private string headCoach;
        private string logoPath;
        private long budget;

        public Team () { }
        public Team (string path) {

        }

        public Team (string name, string c, int year, string coach, string logo) {
            this.clubName = name;
            this.city = c;
            this.foundation = year;
            this.headCoach = coach;
            this.logoPath = logo;
            this.players = new List<Player>();            
        }

        public Team(string name, string c, int year, string coach, string logo, List<Player> p) : this(name, c, year, coach, logo) {            
            this.players = p;
        }

        public Team(string name, string c, int year, string coach, string logo, List<Player> p, long b) : this(name, c, year, coach, logo, p) {
            this.budget = b;
        }

        [DisplayName("Состав клуба"), Category("Состав")]
        public List<Player> Players {
            get {
                return players;
            }

            set {
                players = value;
            }
        }

        [DisplayName("Имя клуба")]
        public string ClubName {
            get {
                return clubName;
            }

            set {
                clubName = value;
            }
        }

        [DisplayName("Основан"), Category("Основная информация")]
        public int Foundation {
            get {
                return foundation;
            }

            set {
                foundation = value;
            }
        }

        [DisplayName("Город"), Category("Основная информация")]
        public string City {
            get {
                return city;
            }

            set {
                city = value;
            }
        }

        [DisplayName("Бюджет")]
        public long Budget {
            get {
                return budget;
            }

            set {
                budget = value;
            }
        }

        [DisplayName("Гл. Тренер")]
        public string HeadCoach {
            get {
                return headCoach;
            }

            set {
                headCoach = value;
            }
        }

        [Browsable(false)]
        //[Bindable(true)]
        public string LogoPath {
            get {
                return logoPath;
            }

            set {
                logoPath = value;
            }
        }

        //[Browsable(false)]
        //[Bindable(true)]
        public Bitmap Logo {
            get {
                return new Bitmap(logoPath);
            }
        }
    }
}
