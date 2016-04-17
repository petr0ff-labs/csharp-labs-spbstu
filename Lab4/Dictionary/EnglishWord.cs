using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab4.Dictionary {    
    class EnglishWord : Word {
        private string transcription;

        public EnglishWord(string w) : base(w) {
            this.transcription = "/" + w + "/";
        }        

        public string Transcription {
            get { return this.transcription; }
        }
    }
}
