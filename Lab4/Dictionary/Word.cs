using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab4.Dictionary {
    abstract class Word {
        protected string m_value;
        protected string m_type;

        public Word(string w, string t) {
            this.m_value = w;
            this.m_type = t;
        }

        public string Value {
            get { return this.m_value; }
            set { this.m_value = value; }
        }

        public string Type {
            get { return this.m_type; }
            set { this.m_type = value; }
        }
    }
}
