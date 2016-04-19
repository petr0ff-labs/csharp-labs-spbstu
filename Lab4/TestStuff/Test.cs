using Lab4.Dictionary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab4.Test {
    public class RunTest {
        private List<Question> questions;
        private Object subject;
        public int correctAnswers;

        public RunTest(ADictionary s) {
            this.subject = s;
            this.correctAnswers = 0;
        }

        public List<Question> Questions {
            get { return questions; }
            set { this.questions = value; }
        }

        public int CorrectAnswers {
            get { return correctAnswers; }
            set { correctAnswers = value; }
        }
    }
}
