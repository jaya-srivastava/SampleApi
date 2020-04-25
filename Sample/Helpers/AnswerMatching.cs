using Jint;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;

namespace iPractice.Helpers
{
    public static class AnswerMatching
    {
        //private static Jint.Engine engine = GetEngine();
        /// <summary>
        /// Method to read jscript file and use Mathjs to evalute correct answer and  useranswer   return bool  /// </summary>
        /// <param name="corectAnswer"></param>
        /// <param name="answerGiven"></param>
        /// <returns>bool value of answer is correct or not</returns>
        /// 
        public static bool IsAnswerCorrect(string corectAnswer, string answerGiven,Engine engine)
        {
            bool result = false;
            try
            {
                if (corectAnswer.ToLower() == answerGiven.ToLower())
                {
                    result = true;
                }
                else if(clearAnswer(corectAnswer).ToLower() == clearAnswer(answerGiven).ToLower())
                {
                    result = true;
                }
                else if (answerGiven.Contains("[x]") ==false && corectAnswer.Contains("[x]")==false){
                    Regex rgx = new Regex(@"([$$\s])");
                    string[] lstargument = new String[2];
                    var ansGiven = rgx.Replace(answerGiven.ToLower(), string.Empty);
                    var CorAns = rgx.Replace(corectAnswer.ToLower(), string.Empty);
                    lstargument[0] = RemoveDoubleParenthesis(ansGiven);
                    lstargument[1] = RemoveDoubleParenthesis(CorAns); ;
                    result = engine.Invoke("IsAnswerCorrect", lstargument).AsBoolean();
                }
            }catch(Exception ex)
            {
                throw ex;
            }
            return result;
        }

        public static bool checkAnswerForMultichoice(string corectAnswer, string answerGiven)
        {
            Regex rgxSpace = new Regex(@"\s+");  
            //var ansGiven = clearAnswer(answerGiven.ToLower());
            //var CorAns = clearAnswer(corectAnswer.ToLower());
            bool result = false;
            if (rgxSpace.Replace(answerGiven, String.Empty).Trim().ToLower() == rgxSpace.Replace(corectAnswer, String.Empty).Trim().ToLower())
            {
                result = true;
            }
            return result;
        }

        public static string clearAnswer(string Answer) // remove unusal strings from correctanswer and AnswerGiven
        {
            string CorrectAnswer = Answer;
            Regex rgxSpace = new Regex(@"\s+");           
            Regex rgx = new Regex(@"([$$\s])");
            //Regex leftRgx = new Regex(@"\\left");
            //Regex rightRgx = new Regex(@"\\right");

            CorrectAnswer = rgxSpace.Replace(CorrectAnswer, String.Empty).Trim().ToLower();
            CorrectAnswer = rgx.Replace(CorrectAnswer, String.Empty).Trim().ToLower();            
            //CorrectAnswer = leftRgx.Replace(CorrectAnswer, String.Empty);
            //CorrectAnswer = rightRgx.Replace(CorrectAnswer, String.Empty);
            return CorrectAnswer;
        }

        private static string RemoveDoubleParenthesis(string initialString)    //function to remove extra () from expr
        {
            char[] s = new char[initialString.Length];
            char toRemove = '$';
            Stack<int> stack = new Stack<int>();

            s = initialString.ToCharArray();

            for (int i = 0; i < s.Length; i++)
            {
                s[i] = initialString[i];
                if (s[i] == '(')
                {
                    stack.Push(i);
                }
                else if (s[i] == ')')
                {
                    int start = stack.Pop();
                    int prevIndex = start - 1;
                    //if (prevIndex < 0) prevIndex = 0;

                    //remove outer ( and ) -especial scenario
                    if (start == 0 && i == (s.Length - 1) )
                    {
                        s[start] = s[i] = toRemove;
                    }
                    else if ((prevIndex >= 0 && (s[start - 1] == '(' && (i + 1) <= s.Length  && s[i + 1] == ')')))
                    {
                        s[start] = s[i] = toRemove;
                    }

                }
            }

            return new string((from c in s where c != toRemove select c).ToArray());
        }
        
    }
}