using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab4.Dictionary {
    class RusEngDictionary : ADictionary {
        private Dictionary<RussianWord, IEnumerable<EnglishWord>> dict;

        public RusEngDictionary() {
            dict = new Dictionary<RussianWord, IEnumerable<EnglishWord>>();
            addToDict(new RussianWord("Кошка"), new EnglishWord("Cat"));
            addToDict(new RussianWord("Собака"), new EnglishWord("Dog"));
            addToDict(new RussianWord("Сумка"), new List<EnglishWord>() { new EnglishWord("Bag") });
        }

        private RusEngDictionary(Dictionary<RussianWord, IEnumerable<EnglishWord>> d) {
            this.dict = d;
        }

        private Dictionary<RussianWord, IEnumerable<EnglishWord>> Dict {
            get { return this.dict; }
            set { this.dict = value; }
        }

        public override IEnumerable<Word> this[Word w] {
            get { return this.dict[(RussianWord)w]; }
            set { this.dict[(RussianWord)w] = (IEnumerable<EnglishWord>)value; }
        }

        public override Word[] Keys {
            get { return this.dict.Keys.ToArray(); }
        }

        public override void addToDict(Word w, IEnumerable<Word> v) {
            this.Dict.Add((RussianWord) w, (List<EnglishWord>) v);
        }

        public override void addToDict(Word w, Word v) {
            List<EnglishWord> l = new List<EnglishWord>();
            l.Add((EnglishWord)v);
            this.Dict.Add((RussianWord)w, l);
        }

        public override void removeFromDict(Word w) {
            this.Dict.Remove((RussianWord) w);
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
