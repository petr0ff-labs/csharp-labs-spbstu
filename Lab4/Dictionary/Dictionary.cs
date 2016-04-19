using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Lab4.Dictionary {
    abstract public class ADictionary {
        private Dictionary<Word, IEnumerable<Word>> dict;

        public enum WordType { Noun, Adjective, Adverb, Verb, Expression };

        public ADictionary() { }

        /*public abstract Dictionary<Word, IEnumerable<Word>> Dict {
            get;
        }*/

        abstract public IEnumerable<Word> this[Word w] { get; set; }

        abstract public Word[] Keys { get; }

        public Dictionary<Word, IEnumerable<Word>> Dict {
            get;
        }

        abstract public void addToDict(Word w, IEnumerable<Word> v);

        abstract public void addToDict(Word w, Word v);

        abstract public void removeFromDict(Word w);

        abstract public string printDict();

        public Word getWord(string s) {
            foreach (var w in Keys) {
                if (w.Value.Equals(s))
                    return w;
            }
            return null;
        }

        public string getValues(IEnumerable<Word> w) {
            string res = "";
            if (w.ToArray().Length == 1)
                res = "- " + w.First().Value;
            else {
                foreach (var v in w)
                    res += "- " + v.Value + "\n";
                res = res.Remove(res.Length - 1);
            }
            return res;
        }
    }

    public class item {
        [XmlElement]
        public Word word;
        [XmlElement]
        public IEnumerable<Word> value;
    }
}