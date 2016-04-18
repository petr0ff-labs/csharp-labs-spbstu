using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using System.Xml.XPath;

namespace Lab4.Dictionary {
    class EngRusDictionary : ADictionary {
        private Dictionary<EnglishWord, IEnumerable<RussianWord>> dict;

        public EngRusDictionary() {
            this.dict = new Dictionary<EnglishWord, IEnumerable<RussianWord>>();
            addToDict(new EnglishWord("Cat", "noun"), new RussianWord("Кошка"));
            addToDict(new EnglishWord("Dog", "noun"), new RussianWord("Собака"));
            addToDict(new EnglishWord("Bag", "noun"), new List<RussianWord>() { new RussianWord("Сумка"), new RussianWord("Пакет") });
        }

        public EngRusDictionary(string path) {
            this.dict = new Dictionary<EnglishWord, IEnumerable<RussianWord>>();
            XDocument database = XDocument.Load(path);
            List<XElement> entries = database.Descendants("Row").ToList();
            foreach (var e in entries) {
                List<XElement> cells = e.Elements().ToList();
                string engWord = cells[0].Value;
                string rusWord = cells[1].Value;
                string wordType = cells[2].Value;
                try {
                    if (rusWord.Contains(';')) {
                        List <String> rusWords = rusWord.Split(';').ToList();
                        List<RussianWord> rusList = new List<RussianWord>();
                        foreach (var r in rusWords)
                            rusList.Add(new RussianWord(r));
                        addToDict(new EnglishWord(engWord, wordType), rusList);
                    }
                    else
                        addToDict(new EnglishWord(engWord, wordType), new RussianWord(rusWord));
                }
                catch (Exception) { }
            }
                
            /*FileStream sr = new FileStream(path, FileMode.Open);
            XmlReader xmlReader1 = XmlReader.Create(sr);
            xmlReader1.MoveToContent();
            Console.WriteLine(xmlReader1.ReadInnerXml());
            sr.Close();*/
        }

        private EngRusDictionary(Dictionary<EnglishWord, IEnumerable<RussianWord>> d) {
            this.dict = d;
        }

        public Dictionary<EnglishWord, IEnumerable<RussianWord>> Dict {
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
            if (!this.Dict.ContainsKey((EnglishWord)w))
                this.Dict.Add((EnglishWord) w, (List<RussianWord>) v);
        }

        public override void addToDict(Word w, Word v) {
            if (!this.Dict.ContainsKey((EnglishWord)w)) {
                List<RussianWord> l = new List<RussianWord>();
                l.Add((RussianWord)v);
                this.Dict.Add((EnglishWord)w, l);
            }
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
    }
}
