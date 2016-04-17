using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab4.Dictionary {
    abstract class Word {
        protected string m_value;

        public Word(string w) {
            this.m_value = w;
        }

        public string Value {
            get { return this.m_value; }
            set { this.m_value = value; }
        }
    }
}
