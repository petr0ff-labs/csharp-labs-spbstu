using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Lab4.Dictionary {
    public class EnglishWord : Word {
        private string transcription;

        public EnglishWord(string w, string t) : base(w, t) { }

        public EnglishWord(string w) : this(w, "") { }

        public EnglishWord() { }

        public EnglishWord(string w, string t, string tr) : this(w, t) {
            this.transcription = tr;
        }

        [XmlIgnore]
        public string Transcription {
            get { return this.transcription; }
        }
    }
}
