using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab4.Test {
    public class Question {
        private Object testQuestion;
        private Object answer;

        public Question (Object q, Object a) {
            this.testQuestion = q;
            this.answer = a;
        }

        public string TestQuestion {
            get { return testQuestion.ToString(); }
        }

        public string Answer {
            get { return answer.ToString(); }
        }
    }
}
