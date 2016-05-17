using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Xml.Serialization;

namespace Lab5.Teams {
    public class Team {
        [XmlArray]
        private List<Player> players;
        private string clubName;
        private string city;
        private int foundation;
        private string headCoach;
        private string logoPath;
        private string stadium;
        private int stadCapacity;

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

        public Team(string name, string c, int year, string coach, string logo, List<Player> p, string s, int b) : this(name, c, year, coach, logo, p) {
            this.stadium = s;
            this.stadCapacity = b;
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

        [DisplayName("Стадион")]
        public string Stadium {
            get {
                return stadium;
            }

            set {
                stadium = value;
            }
        }

        [DisplayName("Вместимость")]
        public int StadCapacity {
            get {
                return stadCapacity;
            }

            set {
                stadCapacity = value;
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
                
        [Bindable(true)]
        [Browsable(true)]
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
        /*public Bitmap Logo {
            get {
                return new Bitmap(logoPath);
            }
            set {
                Logo = value;
            }
        }*/

        [DisplayName("Состав клуба"), Category("Состав")]
        public List<Player> Players {
            get {
                return players;
            }

            set {
                players = value;
            }
        }        
    }
}
