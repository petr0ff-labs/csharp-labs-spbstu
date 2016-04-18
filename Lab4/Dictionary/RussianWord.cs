using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab4.Dictionary {
    class RussianWord : Word {
        public RussianWord(string w, string t) : base(w, t) { }
        public RussianWord(string w) : this(w, "") { }
    }
}
