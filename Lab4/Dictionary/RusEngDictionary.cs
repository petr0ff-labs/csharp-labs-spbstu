using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Lab4.Dictionary {
    class RusEngDictionary : ADictionary {
        private Dictionary<RussianWord, IEnumerable<EnglishWord>> dict;

        public RusEngDictionary() {
            dict = new Dictionary<RussianWord, IEnumerable<EnglishWord>>();
            addToDict(new RussianWord("Кошка", "noun"), new EnglishWord("Cat"));
            addToDict(new RussianWord("Собака", "noun"), new EnglishWord("Dog"));
            addToDict(new RussianWord("Сумка", "noun"), new List<EnglishWord>() { new EnglishWord("Bag") });
        }

        private RusEngDictionary(Dictionary<RussianWord, IEnumerable<EnglishWord>> d) {
            this.dict = d;
        }

        public RusEngDictionary(string path) {
            this.dict = new Dictionary<RussianWord, IEnumerable<EnglishWord>>();
            XDocument database = XDocument.Load(path);
            List<XElement> entries = database.Descendants("Row").ToList();
            foreach (var e in entries) {                
                List<XElement> cells = e.Elements().ToList();                
                string rusWord = e.Element("Word").Element("Value").Value;                
                string wordType = e.Element("Word").Element("Type").Value;
                List<XElement> vals = e.Element("Values").Elements().ToList();
                List<EnglishWord> wList = new List<EnglishWord>();
                foreach (var w in vals)
                    wList.Add(new EnglishWord(w.Element("Value").Value, w.Element("Type").Value));
                addToDict(new RussianWord(rusWord, wordType), wList);
                
            }
        }

        public Dictionary<RussianWord, IEnumerable<EnglishWord>> Dict {
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
            if (!this.Dict.ContainsKey((RussianWord)w))
                this.Dict.Add((RussianWord) w, (List<EnglishWord>) v);
        }

        public override void addToDict(Word w, Word v) {
            if (!this.Dict.ContainsKey((RussianWord)w)) {
                List<EnglishWord> l = new List<EnglishWord>();
                l.Add((EnglishWord)v);
                this.Dict.Add((RussianWord)w, l);
            }
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
    }
}
