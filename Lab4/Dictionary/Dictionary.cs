using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab4.Dictionary {
    abstract class ADictionary {
        private Dictionary<Word, IEnumerable<Word>> dict;

        public ADictionary() {}

        public ADictionary(String path) {}

        abstract public IEnumerable<Word> this[Word w] { get; set; }

        abstract public Word[] Keys { get; }

        abstract public void addToDict(Word w, IEnumerable<Word> v);

        abstract public void addToDict(Word w, Word v);

        abstract public void removeFromDict(Word w);

        abstract public string printDict();

        abstract public string getValues(IEnumerable<Word> w);
    }
}
