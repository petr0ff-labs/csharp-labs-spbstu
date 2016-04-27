﻿namespace Lab5.Teams {
    public class Player {
        private string lastName;
        private double height;
        private double weight;
        private int caps;
        private int goals;
        private string country;

        public Player() { }

        public Player(string ln, double h, double w, int c, int g, string ctry) {
            this.lastName = ln;
            this.height = h;
            this.weight = w;
            this.caps = c;
            this.goals = g;
            this.country = ctry;
        }

        public string LastName {
            get {
                return lastName;
            }

            set {
                lastName = value;
            }
        }

        public double Height {
            get {
                return height;
            }

            set {
                height = value;
            }
        }

        public double Weight {
            get {
                return weight;
            }

            set {
                weight = value;
            }
        }

        public int Caps {
            get {
                return caps;
            }

            set {
                caps = value;
            }
        }

        public int Goals {
            get {
                return goals;
            }

            set {
                goals = value;
            }
        }

        public string Country {
            get {
                return country;
            }

            set {
                country = value;
            }
        }
    }
}
