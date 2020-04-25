using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using WebMatrix.WebData;
using System.Threading.Tasks;
using iPractice.RestServices;
using iPractice.ViewModel;
using Sample.Models.DTO;
using System.Text.RegularExpressions;


namespace iPractice.Helpers
{
    public class CommonHelper
    {        
        GradeService _gradesService = new GradeService();
        //AccountService _accountService = new AccountService();
        public static int PaidMinDate = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["ReportsPaidMinDate"].ToString());
        public static int NotPaidMinDate = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["ReportsNotPaidMinDate"].ToString());
        public static string pattern = @"^(-)?\\frac(([{])(-)?(\d+)([}]))(([{])(\d+)([}]))$";
        Regex objFractionRegex = new Regex(pattern);

        public bool CompareAnswers(string yourAnswer, string CorrectAnswer)
        {   
            if(CorrectAnswer.Contains("+") && !yourAnswer.Contains("+"))
            {
                return false;
            }
            var userAnswer = clearAnswer(yourAnswer);
            var correctAnswer = clearAnswer(CorrectAnswer);
            userAnswer = isContainsMinus(userAnswer);
            correctAnswer = isContainsMinus(correctAnswer);

            if (userAnswer == correctAnswer) {
                return true;
            }

            if (IsNumeric(userAnswer) && IsNumeric(correctAnswer)) {
                return IsNumericCorrect(userAnswer, correctAnswer);
            }

            if(isCdotOrTimes(userAnswer,correctAnswer)) 
            {
                return true;
            }

            if (isContainsComma(userAnswer,correctAnswer))
            {
                return true;
            }

            if(isBothNotInFracForm(userAnswer,correctAnswer))
            {
                return true;
            }

            if (isBothInFracForm(userAnswer,correctAnswer))
            {
                return true;
            }
            if(isOneInFracForm(userAnswer,correctAnswer))
            {
                return true;
            }                
            return false;
        }
       
        public string clearAnswer(string Answer) // remove unusal strings from correctanswer and AnswerGiven
        {  
            string CorrectAnswer=Answer;             
            //Regex rgxSpace = new Regex(@"\s+");
            //Regex rgx = new Regex(@"$$");
            Regex rgx = new Regex(@"([$$\s+])");
            Regex logRgx = new Regex(@"\\log");
            Regex lnRgx = new Regex(@"\\ln");
            Regex leftRgx = new Regex(@"\\left");
            Regex rightRgx = new Regex(@"\\right");

            //CorrectAnswer = rgxSpace.Replace(CorrectAnswer, String.Empty).Trim().ToLower();
            CorrectAnswer = rgx.Replace(CorrectAnswer, String.Empty).Trim().ToLower();
            CorrectAnswer = logRgx.Replace(CorrectAnswer, "log");
            CorrectAnswer = lnRgx.Replace(CorrectAnswer, "ln");
            CorrectAnswer = leftRgx.Replace(CorrectAnswer, String.Empty);
            CorrectAnswer = rightRgx.Replace(CorrectAnswer, String.Empty);

            return CorrectAnswer;
        }

        public bool IsNumericCorrect(string userAnswer, string correctAnswer)// to match numeric values
        {
            bool result = false;
            decimal selectedAns;
            decimal CorrectAns;
            bool isSelectedAnsNumeric = decimal.TryParse(correctAnswer, out CorrectAns);
            bool isCorrectAnsNUmeric = decimal.TryParse(userAnswer, out selectedAns);
            //Check if GivenAnswer and DB Correct answer is numeric/decimal or Not
            if (correctAnswer.Contains("+") && !userAnswer.Contains("+"))  //  check if correct answer contains + sign then check in answer given for + sign also
            {
                result = false;
            }
            if (isCorrectAnsNUmeric && isSelectedAnsNumeric)  // match the numeric value 
            {
                selectedAns = Decimal.Parse(selectedAns.ToString("0.0000"));
                CorrectAns = Decimal.Parse(CorrectAns.ToString("0.0000"));
                result = selectedAns.Equals(CorrectAns);
            }
            return result;
        }

        
        public bool isOneInFracForm(string userAnswer,string correctAnswer) {
            var result = false;
            if ((!objFractionRegex.IsMatch(userAnswer) && objFractionRegex.IsMatch(correctAnswer)) || (objFractionRegex.IsMatch(userAnswer) && !objFractionRegex.IsMatch(correctAnswer))) {
                var answerGiven = isNotInFracForm(userAnswer);
                var CAnswer = isNotInFracForm(correctAnswer);;
                answerGiven = isContainsMinus(answerGiven);
                CAnswer = isContainsMinus(CAnswer);

                if (answerGiven == CAnswer) {
                    result = true;
                }
            }
            return result;
        }

        public string isNotInFracForm(string answer)
        {
            var answerGiven=answer;
            if (!objFractionRegex.IsMatch(answer)) {
                if (answer.Contains("/") ) {
                    answerGiven = SimplifyFraction(answer);
                }
                else
                {   
                    answerGiven = "\\frac{" + answer + "}{" + 1 + "}";
                }                    
            }
            return answerGiven;
        }

        public bool isBothInFracForm(string userAnswer,string correctAnswer) {
            var result = false;
            if (objFractionRegex.IsMatch(userAnswer) && objFractionRegex.IsMatch(correctAnswer)) {
                if (userAnswer == correctAnswer) {
                    result = true;
                }
            }
            return result;
        }

        public bool isBothNotInFracForm(string userAnswer,string correctAnswer)
        {
            var result = false;
             if (!objFractionRegex.IsMatch(userAnswer) && !objFractionRegex.IsMatch(correctAnswer))
            {
                 var answerGiven = isContainsMinus(userAnswer);
                 var CAnswer = isContainsMinus(correctAnswer);            
                 if (answerGiven == CAnswer)
                 {                
                     result = true;                
                 }

                 if (isBothContainsSlash(answerGiven,CAnswer))
                 {
                     result= true;
                 }
             
                 if(isOneOfTwoContainsSlash(answerGiven,CAnswer))
                 {
                     result= true;
                 }
             }
             return result;
        }

        public bool isOneOfTwoContainsSlash(string answerGiven, string CAnswer)
        {
             var result = false;
             if ((answerGiven.Contains("/") && CAnswer.Contains("/") ) || (answerGiven.Contains("/")  && CAnswer.Contains("/") ))
             {
                 var yAnswer = IsContainsSlash(answerGiven);
                 var cAnswer = IsContainsSlash(CAnswer);
                 yAnswer = isContainsMinus(yAnswer);
                 cAnswer = isContainsMinus(cAnswer);
                 if(yAnswer==cAnswer)
                 {
                     result = true;
                 }
             }
             return result;
        }

        public string IsContainsSlash(string answer)
        {
            var answerGiven = "";
            if (answer.Contains("/"))
            {
                answerGiven = SimplifyFraction(answer);
            }
            else {
                answerGiven = "\\frac{" + answer + "}{" + 1 + "}";
            }
            return answerGiven;
        }

        public bool isBothContainsSlash(string answerGiven,string CAnswer)
        {
            var result = false;
            if ((answerGiven.Contains("/") && CAnswer.Contains("/") )) {
                if (answerGiven == CAnswer) {
                    result = true;
                }
            }
            return result;
        }

        public bool isCdotOrTimes(string userAnswer,string correctAnswer) //check if one contains /cdot and other /times
        {
            var result = false;
            if ((userAnswer.Contains("\\cdot") && correctAnswer.Contains("\\times") ) || (userAnswer.Contains("\\times") && correctAnswer.Contains("\\cdot") )) {
                var answerGiven = userAnswer;
                var CAnswer = correctAnswer;
                if (userAnswer.Contains(".") && correctAnswer.Contains("\\cdot") ) {
                    answerGiven = userAnswer.Replace(".", "\\cdot");
                }
                if (userAnswer.Contains("\\cdot") && correctAnswer.Contains(".") ) {
                    CAnswer = CAnswer.Replace(".", "\\times");
                }
                if (userAnswer.Contains("\\cdot") && correctAnswer.Contains("\\times") ) {
                    answerGiven = userAnswer.Replace("\\cdot", "\\times");
                }
                if (userAnswer.Contains("\\times") && correctAnswer.Contains("\\cdot") ) {
                    answerGiven = userAnswer.Replace("\\times", "\\cdot");
                }
                if (answerGiven == CAnswer) {
                    result = true;
                }
            }
            return result;
        }

        public bool isContainsComma (string userAnswer,string correctAnswer)
        {
            var result = false;
            if ((userAnswer.Contains(",") && correctAnswer.Contains(",") ) || (userAnswer.Contains(",")  && correctAnswer.Contains(",") )) {
                var answerGiven = isContainsMinus(userAnswer);
                var CAnswer = isContainsMinus(correctAnswer);           
            
                if (answerGiven == CAnswer) {
                    result = true;
                }
            }
            return result;
        }

        public string isContainsMinus(string answer)
        {
            var answerGiven = "";
            if (answer.Contains(",") ) {
                var ag = answer.Split(',');
                for (var i = 0; i < ag.Length; i++) {
                    if (ag[i].Contains("-")) {
                        answerGiven = answerGiven + "-" + ag[i].Replace("-", "");
                    }
                    else {
                        answerGiven = answerGiven + ag[i];
                    }
                }
            }
            else {
                if (answer.Contains("-"))
                {
                    answerGiven ="-"+ answer.Replace("-","");
                }
                else
                {
                    answerGiven = answer;
                }
            }
            return answerGiven;
        }

        public static bool IsNumeric(string text)
        {
            double test;
            return double.TryParse(text, out test);
        }

        public string SimplifyFraction(string Answer)  // convert answer to \frac form
        {
            var value = "";
            if(Answer.Contains("/"))
            {
                var ansStr = Answer.Split('/');
                value = "\\frac{" + ansStr[0] + "}{" + ansStr[1] + "}";
            }
            return value;
        }

        //   Called when a specific LogIn URL is given to students through School
        public string GetSchoolSignInUrlBySuffix(string suffix)
        {

            string url = "http://localhost:18359/Membership/StudentLogin/" + suffix; //"http://www." + Request.Url.Host + "/School/StudentLogin/" + suffix;
            return url;
        }

       
        public string GetRoleOfUserByAdminRole(string Role)
        {
            string roletype = "";
            if (Role == Enums.Role.Teacher.ToString())
            {
                roletype = Enums.Role.Student.ToString();
            }
            else if (Role == Enums.Role.SchoolAdmin.ToString())
            {
                roletype = Enums.Role.Teacher.ToString();
            }
            else if (Role == Enums.Role.Parent.ToString())
            {
                roletype = Enums.Role.Student.ToString();
            }
            else if (Role == Enums.Role.DisttAdmin.ToString())
            {
                roletype = Enums.Role.SchoolAdmin.ToString();
            }
            else if (Role == Enums.Role.HsTeacher.ToString())
            {
                roletype = Enums.Role.Student.ToString();
            }
            else if (Role == Enums.Role.SA.ToString())
            {
                roletype = Enums.Role.Student.ToString();
            }
            return roletype;
        }

        public async Task<SelectList> GetGrades()
        {
            List<SelectListItem> GradeList = new List<SelectListItem>();
            GradeList.Add(new SelectListItem() { Text = " --- Select Grade --- ", Value = "-100" });
            var gradesList =await _gradesService.GetGradesAsync();
            foreach (var item in gradesList)
            {
                GradeList.Add(new SelectListItem() { Text = item.GradeDesc2, Value = item.GradeId.ToString() });
            }
            SelectList lists = new SelectList(GradeList, "Value", "Text", "-100");
            return lists;
        }

        public SelectList GetGradesSync()
        {
            List<SelectListItem> GradeList = new List<SelectListItem>();
            GradeList.Add(new SelectListItem() { Text = " --- Select Grade --- ", Value = "-100" });
            var gradesList = _gradesService.GetGrades();
            foreach (var item in gradesList)
            {
                GradeList.Add(new SelectListItem() { Text = item.GradeDesc2, Value = item.GradeId.ToString() });
            }
            SelectList lists = new SelectList(GradeList, "Value", "Text", "-100");
            return lists;
        }

        //public string getUserAgent()

        public enum ManageMessageId
        {
            ChangePasswordSuccess,
            SetPasswordSuccess,
            RemoveLoginSuccess,
        }

        public string ErrorCodeToString(MembershipCreateStatus createStatus)
        {
            // See http://go.microsoft.com/fwlink/?LinkID=177550 for
            // a full list of status codes.
            switch (createStatus)
            {
                case MembershipCreateStatus.DuplicateUserName:
                    return "User name already exists. Please enter a different user name.";

                case MembershipCreateStatus.DuplicateEmail:
                    return "A user name for that e-mail address already exists. Please enter a different e-mail address.";

                case MembershipCreateStatus.InvalidPassword:
                    return "The password provided is invalid. Please enter a valid password value.";

                case MembershipCreateStatus.InvalidEmail:
                    return "The e-mail address provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.InvalidAnswer:
                    return "The password retrieval answer provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.InvalidQuestion:
                    return "The password retrieval question provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.InvalidUserName:
                    return "The user name provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.ProviderError:
                    return "The authentication provider returned an error. Please verify your entry and try again. If the problem persists, please contact your system administrator.";

                case MembershipCreateStatus.UserRejected:
                    return "The user creation request has been canceled. Please verify your entry and try again. If the problem persists, please contact your system administrator.";

                default:
                    return "An unknown error occurred. Please verify your entry and try again. If the problem persists, please contact your system administrator.";
            }
        }

        //function to convert enum to string list
        public static List<string> GetEnumValues(Type enumType)
        {
            List<string> itemList = new List<string>();

            var listOfValues = Enum.GetValues(enumType);
            foreach (var value in listOfValues)
            {
                itemList.Add(value.ToString());
            }

            return itemList;
        }

        public static DateTime GetUTCTime()
        {
            return DateTime.Now.ToUniversalTime();
        }      
    }
}