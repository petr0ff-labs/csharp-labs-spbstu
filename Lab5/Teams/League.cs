using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Lab5.Teams {
    [Serializable()]
    public class League {
        private string leagueName = "Российская Премьер Лига";
        [XmlArray]
        private List<Team> teams;

        public League() {
            this.teams = new List<Team>();
        }

        public string LeagueName {
            get {
                return leagueName;
            }

            set {
                leagueName = value;
            }
        }

        public List<Team> Teams {
            get {
                return teams;
            }

            set {
                teams = value;
            }
        }
    }
}
