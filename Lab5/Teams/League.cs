using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab5.Teams {
    public class League {
        private string leagueName = "Российская Премьер Лига";
        private List<Team> teams;

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
