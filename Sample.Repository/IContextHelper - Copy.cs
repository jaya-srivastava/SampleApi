using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using iPractice.Models;

namespace iPractice.Repository
{
    public interface IContextHelper
    {
        void UpdateContext(PracticeContext context);
    }


    public class ContextHelperToSeed  : IContextHelper
    {
        public List<Question> QuestionList { get; set; }
        public List<QuestionDetail> QuestionDetailList { get; set; }
        public List<AnswerOption> AnswerOptionList { get; set; }

        public List<Sheet> SheetList { get; set; }
        public List<QuestionSheet> QuestionSheetList { get; set; }
        public List<Grade> GradeList { get; set; }

        public List<GradeSubTopic> GradeSubTopicList { get; set; }


        public ContextHelperToSeed()
        {

        }

        public ContextHelperToSeed(List<CompleteQuestion> completeQuestionList)
        {
            UpdateCompleteQuestionDetail(completeQuestionList);
        }


        public void UpdateCompleteQuestionDetail(List<CompleteQuestion> completeQuestionList)
        {
            int count =1;
            foreach (var item in completeQuestionList)
            {
                item.Question.QuestionId = count;
                item.Option.AnswerOptionId = count;
                item.Option.QuestionId = count;
                item.QuestionDetail.QuestionDetailId = count;
                item.QuestionDetail.QuestionId = count;
                item.Question.CorrectAnswer = item.Option.CorrectAnswer;

                count++;
            }

            this.QuestionList = completeQuestionList.Select(m => m.Question).ToList();
            this.QuestionDetailList = completeQuestionList.Select(m => m.QuestionDetail).ToList();
            this.AnswerOptionList = completeQuestionList.Select(m => m.Option).ToList();


        }

        public void UpdateContext(PracticeContext context)
        {
            //Membership.CreateUser(“testuser”, “test123″);
            //Roles.CreateRole(“Administrator”);
            //Roles.AddUsersToRole(new[] {“testuser”}, “Administrator”);

            var topicList = new List<Topic>                                                            
            {
                new Topic { TopicId="TAdd", TopicDesc="Adddition" },
                new Topic { TopicId="TSub", TopicDesc="Subtraction" },
                new Topic { TopicId="TMul", TopicDesc="Multipication" },
                new Topic { TopicId="TDiv", TopicDesc="Division" },  
              
                 new Topic { TopicId="TAlg", TopicDesc="Algebra" },
                new Topic { TopicId="TCom", TopicDesc="Compare" },
             
                new Topic { TopicId="TFra", TopicDesc="Fraction" },
                new Topic { TopicId="TDec", TopicDesc="Decimal" }, 
                new Topic { TopicId="TPAl", TopicDesc="Pre-Algebra" },                   
                new Topic { TopicId="TWor", TopicDesc="WordProblem" },  
             };

            topicList.ForEach(s => context.Topics.Add(s));
            context.SaveChanges();

            var subTopicList = new List<SubTopic>
            {
                //Add
                new SubTopic {  SubTopicId="1001", SubTopicDesc ="Additions 1 to 9", TopicId="TAdd" },
                new SubTopic {  SubTopicId="1002", SubTopicDesc ="Additions 1 to 100", TopicId="TAdd" },

                new SubTopic {  SubTopicId="1003", SubTopicDesc ="Two Digit Additions 11-20 with 11-20", TopicId="TAdd" },
                new SubTopic {  SubTopicId="1004", SubTopicDesc ="Two Digit Additions with Regrouping", TopicId="TAdd" },
              
                //Sub               
                new SubTopic {  SubTopicId="1101", SubTopicDesc ="Subtraction 1 to 9", TopicId="TSub" },
                new SubTopic {  SubTopicId="1102", SubTopicDesc ="Subtraction 1 to 100", TopicId="TSub" },   
            
                new SubTopic {  SubTopicId="1103", SubTopicDesc ="Two Digit Subtraction 11-20 with 11-20", TopicId="TSub" },
                new SubTopic {  SubTopicId="1104", SubTopicDesc ="Two Digit Subtraction with Regrouping", TopicId="TSub" },

                //Mul                     
                new SubTopic {  SubTopicId="1201", SubTopicDesc ="Multiplication by 1", TopicId="TMul" },
                new SubTopic {  SubTopicId="1202", SubTopicDesc ="Multiplication by 2", TopicId="TMul" },  
                new SubTopic {  SubTopicId="1203", SubTopicDesc ="Multiplication by 3", TopicId="TMul" },
                new SubTopic {  SubTopicId="1204", SubTopicDesc ="Multiplication by 4", TopicId="TMul" },

                new SubTopic {  SubTopicId="1205", SubTopicDesc ="Multiplication by 5", TopicId="TMul" },
                new SubTopic {  SubTopicId="1206", SubTopicDesc ="Multiplication by 6", TopicId="TMul" },   
                new SubTopic {  SubTopicId="1207", SubTopicDesc ="Multiplication by 7", TopicId="TMul" },
                new SubTopic {  SubTopicId="1208", SubTopicDesc ="Multiplication by 8", TopicId="TMul" },

                new SubTopic {  SubTopicId="1209", SubTopicDesc ="Multiplication by 9", TopicId="TMul" },
                new SubTopic {  SubTopicId="1210", SubTopicDesc ="Multiplication by 10", TopicId="TMul" },   
                new SubTopic {  SubTopicId="1211", SubTopicDesc ="Multiplication by 11", TopicId="TMul" },
                new SubTopic {  SubTopicId="1212", SubTopicDesc ="Multiplication by 12", TopicId="TMul" },

                //new SubTopic {  SubTopicId="1213", SubTopicDesc ="Multiplication by 8", TopicId="TMul" },

                //Division                                   
                new SubTopic {  SubTopicId="1301", SubTopicDesc ="Division 1 to 9", TopicId="TDiv" },
                new SubTopic {  SubTopicId="1302", SubTopicDesc ="Division 1 to 100", TopicId="TDiv" },   
            
                new SubTopic {  SubTopicId="1303", SubTopicDesc ="Two Digit Division 11-20 with 11-20", TopicId="TDiv" },
                new SubTopic {  SubTopicId="1304", SubTopicDesc ="Two Digit Division with Regrouping", TopicId="TDiv" },

                //Algebra
                                              
                new SubTopic {  SubTopicId="2001", SubTopicDesc ="Algebra for Addition",   TopicId="TAlg" },
                new SubTopic {  SubTopicId="2101", SubTopicDesc ="Algebra for Subraction", TopicId="TAlg" },   
            
                new SubTopic {  SubTopicId="2201", SubTopicDesc ="Algebra involving Mulitplication", TopicId="TAlg" },
                new SubTopic {  SubTopicId="2301", SubTopicDesc ="Algebra involving Division", TopicId="TAlg" },


             };
            subTopicList.ForEach(s => context.SubTopics.Add(s));
            context.SaveChanges();

            //add the Grades Table
           // var gradeList = GradeList ?? SimpleInit.GetHardCodedGrades();
            var gradeList = new List<Grade>
            {
                //Add
                new Grade {GradeId="-1", GradeDesc ="Pre-K", GradeDesc2="PreSchool" },
                new Grade {GradeId="0", GradeDesc ="KinderGarten", GradeDesc2="KinderGarten" },
                new Grade {GradeId="1", GradeDesc ="Grade1", GradeDesc2="Grade1" },
                new Grade {GradeId="2", GradeDesc ="Grade2", GradeDesc2="Grade2" },
                new Grade {GradeId="3", GradeDesc ="Grade3", GradeDesc2="Grade3" },
                new Grade {GradeId="4", GradeDesc ="Grade4", GradeDesc2="Grade4" },
                new Grade {GradeId="5", GradeDesc ="Grade5", GradeDesc2="Grade5" },
                new Grade {GradeId="6", GradeDesc ="Grade6", GradeDesc2="Grade6" },
                new Grade {GradeId="7", GradeDesc ="Grade7", GradeDesc2="Grade7" },
                new Grade {GradeId="8", GradeDesc ="Grade8", GradeDesc2="Grade8" },
            };
            gradeList.ForEach(s => context.Grades.Add(s));
            context.SaveChanges();

            //var gradeSubTopicList = GradeSubTopicList ?? SimpleInit.GetHardCodedGrade_Subtopic();

             var gradeSubTopicList = new List<GradeSubTopic>
             {
                 new GradeSubTopic { GradeId ="-1", SubTopicId = "1001"},
                 new GradeSubTopic { GradeId ="-1", SubTopicId = "1002"},

                 new GradeSubTopic { GradeId ="0", SubTopicId = "1001"},
                 new GradeSubTopic { GradeId ="0", SubTopicId = "1002"},
                 new GradeSubTopic { GradeId ="0", SubTopicId = "1101"},
                 new GradeSubTopic { GradeId ="0", SubTopicId = "1102"},

                new GradeSubTopic { GradeId ="1", SubTopicId = "1001"},
                new GradeSubTopic { GradeId ="1", SubTopicId = "1002"},
                new GradeSubTopic { GradeId ="1", SubTopicId = "1101"},
                new GradeSubTopic { GradeId ="1", SubTopicId = "1102"},
                new GradeSubTopic { GradeId ="1", SubTopicId = "1003"},
                new GradeSubTopic { GradeId ="1", SubTopicId = "1004"},
                new GradeSubTopic { GradeId ="1", SubTopicId = "1103"},
                new GradeSubTopic { GradeId ="1", SubTopicId = "1104"},

                new GradeSubTopic { GradeId ="2", SubTopicId = "1001"},
                new GradeSubTopic { GradeId ="2", SubTopicId = "1002"},
                new GradeSubTopic { GradeId ="2", SubTopicId = "1101"},
                new GradeSubTopic { GradeId ="2", SubTopicId = "1102"},


                 new GradeSubTopic { GradeId ="3", SubTopicId = "1201"},
                 new GradeSubTopic { GradeId ="3", SubTopicId = "1202"},
                 new GradeSubTopic { GradeId ="3", SubTopicId = "1203"},
                 new GradeSubTopic { GradeId ="3", SubTopicId = "1204"},
                 new GradeSubTopic { GradeId ="3", SubTopicId = "1205"},
                 new GradeSubTopic { GradeId ="3", SubTopicId = "1206"},
                 new GradeSubTopic { GradeId ="3", SubTopicId = "1207"},
                 new GradeSubTopic { GradeId ="3", SubTopicId = "1208"},
                 new GradeSubTopic { GradeId ="3", SubTopicId = "1209"},
                 new GradeSubTopic { GradeId ="3", SubTopicId = "1210"},
                 new GradeSubTopic { GradeId ="3", SubTopicId = "1211"},
                 new GradeSubTopic { GradeId ="3", SubTopicId = "1212"},

                 new GradeSubTopic { GradeId ="4", SubTopicId = "1201"},
                 new GradeSubTopic { GradeId ="4", SubTopicId = "1202"},
                 new GradeSubTopic { GradeId ="4", SubTopicId = "1203"},
                 new GradeSubTopic { GradeId ="4", SubTopicId = "1204"},
                 new GradeSubTopic { GradeId ="4", SubTopicId = "1205"},

                 new GradeSubTopic { GradeId ="5", SubTopicId = "1201"},
                 new GradeSubTopic { GradeId ="5", SubTopicId = "1202"},
                 new GradeSubTopic { GradeId ="5", SubTopicId = "1203"},
                 new GradeSubTopic { GradeId ="5", SubTopicId = "1204"},
                 new GradeSubTopic { GradeId ="5", SubTopicId = "1205"},
                 
                 new GradeSubTopic { GradeId ="6", SubTopicId = "1201"},
                 new GradeSubTopic { GradeId ="6", SubTopicId = "1202"},
                 new GradeSubTopic { GradeId ="6", SubTopicId = "1203"},
                 new GradeSubTopic { GradeId ="6", SubTopicId = "1204"},
                 new GradeSubTopic { GradeId ="6", SubTopicId = "1205"},

                 new GradeSubTopic { GradeId ="7", SubTopicId = "1201"},
                 new GradeSubTopic { GradeId ="7", SubTopicId = "1202"},
                 new GradeSubTopic { GradeId ="7", SubTopicId = "1203"},
                 new GradeSubTopic { GradeId ="7", SubTopicId = "1204"},
                 new GradeSubTopic { GradeId ="7", SubTopicId = "1205"},

                 new GradeSubTopic { GradeId ="8", SubTopicId = "1201"},
                 new GradeSubTopic { GradeId ="8", SubTopicId = "1202"},
                 new GradeSubTopic { GradeId ="8", SubTopicId = "1203"},
                 new GradeSubTopic { GradeId ="8", SubTopicId = "1204"},
                 new GradeSubTopic { GradeId ="8", SubTopicId = "1205"},
             };

            gradeSubTopicList.ForEach(s => context.GradeSubTopics.Add(s));
            context.SaveChanges();

            var questionList = QuestionList ?? SimpleInit.GetHardCodedQuestions();
            questionList.ForEach(s => context.Questions.Add(s));
            context.SaveChanges();

            var answerOptionsList = AnswerOptionList ?? SimpleInit.GetHardCodedAnsweOptions();
            answerOptionsList.ForEach(s => context.AnswerOptions.Add(s));
            context.SaveChanges();

            var questionDetailList = QuestionDetailList ?? SimpleInit.GetHardCodedQuestionDetails();
            questionDetailList.ForEach(s => context.QuestionDetails.Add(s));
            context.SaveChanges();

            //add the Sheet Tables
            var sheetList = SheetList ?? SimpleInit.GenerateSheetList();
            sheetList.ForEach(s => context.Sheet.Add(s));
            context.SaveChanges();

            //add the Question Sheet details
            var questionSheetList = QuestionSheetList ?? SimpleInit.GenerateQuestionSheetList();
            questionSheetList.ForEach(s => context.QuestionSheet.Add(s));
            context.SaveChanges();

        }

      
    }




}
