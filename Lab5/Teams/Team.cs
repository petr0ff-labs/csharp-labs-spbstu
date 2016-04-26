using System.Collections.Generic;

namespace Lab5.Teams {
    public class Team {
        private List<Player> players;
        private string clubName;
        private string city;
        private int foundation;
        private int cupWinner;

        public Team () { }
        public Team (string path) {

        }

        public List<Player> Players {
            get {
                return players;
            }

            set {
                players = value;
            }
        }

        public string ClubName {
            get {
                return clubName;
            }

            set {
                clubName = value;
            }
        }

        public int Foundation {
            get {
                return foundation;
            }

            set {
                foundation = value;
            }
        }

        public int CupWinner {
            get {
                return cupWinner;
            }

            set {
                cupWinner = value;
            }
        }
    }
}
