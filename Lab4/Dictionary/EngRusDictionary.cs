using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab4.Dictionary {
    class EngRusDictionary : ADictionary {
        private Dictionary<EnglishWord, IEnumerable<RussianWord>> dict;

        public EngRusDictionary() {
            dict = new Dictionary<EnglishWord, IEnumerable<RussianWord>>();
            addToDict(new EnglishWord("Cat"), new RussianWord("Кошка"));
            addToDict(new EnglishWord("Dog"), new RussianWord("Собака"));
            addToDict(new EnglishWord("Bag"), new List<RussianWord>() { new RussianWord("Сумка"), new RussianWord("Пакет") });
        }

        private EngRusDictionary(Dictionary<EnglishWord, IEnumerable<RussianWord>> d) {
            this.dict = d;
        }

        private Dictionary<EnglishWord, IEnumerable<RussianWord>> Dict {
            get { return this.dict; }
            set { this.dict = value; }
        }

        public override IEnumerable<Word> this[Word w] {
            get { return this.dict[(EnglishWord)w]; }
            set { this.dict[(EnglishWord)w] = (IEnumerable<RussianWord>)value; }
        }

        public override Word[] Keys {
            get { return this.dict.Keys.ToArray(); }
        }

        public override void addToDict(Word w, IEnumerable<Word> v) {
            this.Dict.Add((EnglishWord) w, (List<RussianWord>) v);
        }

        public override void addToDict(Word w, Word v) {
            List<RussianWord> l = new List<RussianWord>();
            l.Add((RussianWord)v);
            this.Dict.Add((EnglishWord)w, l);
        }

        public override void removeFromDict(Word w) {
            this.Dict.Remove((EnglishWord) w);
        }        
        
        public override string printDict() {
            string s = "";
            foreach (var w in this.Dict.Keys) {
                s += "Key=" + w.Value + ", Val=" + getValues(this.Dict[w]) + "\n";
            }
            return s;
        }

        public override string getValues(IEnumerable<Word> w) {
            string res = "";
            if (w.ToArray().Length == 1)
                res = w.First().Value;
            else {
                foreach (var v in w)
                    res += v.Value + "\n";
                res = res.Remove(res.Length - 1);
            }
            return res;
        }
    }
}
