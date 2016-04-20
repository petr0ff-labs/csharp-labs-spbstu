using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Lab4.Dictionary {    
    [XmlInclude(typeof(RussianWord))]
    [XmlInclude(typeof(EnglishWord))]
    [Serializable]
    abstract public class Word {
        protected string m_value;
        protected string m_type;

        public Word() { }

        public Word(string w, string t) {
            this.m_value = w.Trim();
            this.m_type = t;
        }

        public string Value {
            get {
                return char.ToUpper(this.m_value[0]) + this.m_value.Substring(1);
            }
            set { this.m_value = value; }
        }

        public string Type {
            get { return this.m_type; }
            set { this.m_type = value; }
        }

        public override string ToString() {
            return this.m_value;
        }
    }
}
