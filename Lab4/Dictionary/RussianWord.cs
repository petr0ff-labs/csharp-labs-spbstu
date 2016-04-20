using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Lab4.Dictionary {
    public class RussianWord : Word {
        public RussianWord(string w, string t) : base(w, t) { }
        public RussianWord(string w) : this(w, "") { }
        public RussianWord() { }
    }
}
