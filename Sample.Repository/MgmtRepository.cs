using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using iPractice.Models;
using System.Data.Objects;
using System.Data;
using System.Data.Entity.Infrastructure;
using System.Data.Entity;
using System.Data.SqlClient;
using iPractice.Models.DTO;

namespace iPractice.Repository
{
    public class MgmtRepository : BaseRepository
    {
        public bool UpdateUser(User user, Administrator admin)
        {
            var User = db.Users.Where(x => x.UserId == user.UserId).FirstOrDefault();
            user.RoleId = user.RoleId;
            if (admin != null)
            {
                Administrator Administrator = db.Administrator.Where(x => x.AdministratorID == admin.AdministratorID).FirstOrDefault();
                Administrator.SecurityCode = admin.SecurityCode;
                Administrator.IsActive = true;
                Administrator.MaxStudentCount = admin.MaxStudentCount;

            }
            db.SaveChanges();
            return true;
        }
        public IList<Administrator> GetTeachers()
        {
            return db.Administrator.ToList();
        }
        public IList<Feedback> GetFeedbacks()
        {
            return db.Feedback.ToList();
        }
        public IList<User> GetUsersUnderAdministrator(List<int> userId)
        {
            return db.Users.Where(x => userId.Contains(x.UserId)).ToList();
        }
        public void UpdateAdminstrator(string SecurityCode, int AdministratorId, int maxStudentCount)
        {
            if (!string.IsNullOrEmpty(SecurityCode))
            {
                Administrator admin = db.Administrator.Where(x => x.AdministratorID == AdministratorId).FirstOrDefault();
                admin.SecurityCode = SecurityCode;
                admin.IsActive = true;
                admin.MaxStudentCount = maxStudentCount;
                db.SaveChanges();
            }
        }
        #region SUBTOPIC

        public List<SubTopic> GetSubTopicsForAdmin()
        {
            var subTopics = db.SubTopics != null ? db.SubTopics.ToList() : null;
            subTopics.ForEach(m => { m.SubTopicDescSEO = m.SubTopicDesc.ToStringURLBuilder(); });
            return subTopics;
        }

        public List<SubTopic> GetSubTopicsbyTopicIdForAdmin(string topicId)
        {
            var subTopics = db.SubTopics.Where(m => m.TopicId == topicId).ToList();
            subTopics.ForEach(m => { m.SubTopicDescSEO = m.SubTopicDesc.ToStringURLBuilder(); });
            return subTopics;
        }

        public bool AddSubTopic(SubTopic objSubTopic)
        {
            try
            {
                db.SubTopics.Add(objSubTopic);
                db.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool UpdateSubTopic(SubTopic objSubTopic)
        {
            try
            {
                SubTopic subTopic = db.SubTopics.Where(x => x.SubTopicId == objSubTopic.SubTopicId).FirstOrDefault();
                subTopic.SubTopicDesc = objSubTopic.SubTopicDesc;
                subTopic.Hint = objSubTopic.Hint;
                subTopic.IsInactive = objSubTopic.IsInactive;
                subTopic.IsPrefixShown = objSubTopic.IsPrefixShown;
                subTopic.IsSuffixShown = objSubTopic.IsSuffixShown;
                subTopic.IsSEOUrlActive = objSubTopic.IsSEOUrlActive;
                subTopic.PrefixText = objSubTopic.PrefixText;
                subTopic.SuffixText = objSubTopic.SuffixText;
                subTopic.IsQuestionTextFormat = objSubTopic.IsQuestionTextFormat;
                subTopic.QuestionTextFormat = objSubTopic.QuestionTextFormat;
                db.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        private void DeleteSubTopic(List<int> subTopicIdList)
        {
            var subTopicList = db.SubTopics.Where(m => subTopicIdList.Contains(m.SubTopicId)).ToList();
            subTopicList.ForEach(m => m.IsInactive = true);

            db.SaveChanges();
        }

        private void UpdateSubTopic(List<int> subTopicIdList, List<SubTopic> subTopics)
        {
            var subTopicList = db.SubTopics.Where(m => subTopicIdList.Contains(m.SubTopicId)).ToList();
            subTopicList.ForEach(sTopic => subTopics.Where(m => m.SubTopicId == sTopic.SubTopicId));

        }

        public void ChangeSubTopicStatus(int subTopicID, string status)
        {
            SubTopic Subtopic = db.SubTopics.Where(x => x.SubTopicId == subTopicID).FirstOrDefault();
            Subtopic.IsInactive = (string.IsNullOrEmpty(status) || status.ToLower() == "false") ? true : false;
            db.SaveChanges();
        }

        #endregion

        #region QUESTION

        public Question GetQuestionByIdForAdmin(int questionId)
        {
            if (questionId > 0)
            {
                Question q = db.Questions.Where(m => m.QuestionId == questionId).FirstOrDefault();
                return q;
            }
            return null;
        }

        private void DeleteQuestion(List<int> subTopicIdList)
        {
            if (subTopicIdList == null || subTopicIdList.Count <= 0) return;
            var qList = db.Questions.Where(m => subTopicIdList.Contains(m.QuestionDetail.FirstOrDefault().SubTopicId)).ToList();
            qList.ForEach(q => q.IsInactive = true);
            db.SaveChanges();
        }

        public List<Question> GetCompleteQuestion(int subTopicId)
        {
            if (!IsCombinedSubTopic(subTopicId))
                return db.Questions.Include("QuestionDetail").Include("AnswerOptions").Where(m => m.QuestionDetail.FirstOrDefault().SubTopicId == subTopicId).ToList();
            else
            {
                List<int> severalSubTopicIdList = CombinedSubTopicIdList(subTopicId);
                return db.Questions.Include("QuestionDetail").Include("AnswerOptions").Where(m => severalSubTopicIdList.Contains(m.QuestionDetail.FirstOrDefault().SubTopicId)).ToList();
            }
        }

        public Question AddQuestion(Question question)
        {
            db.Questions.Add(question);
            db.SaveChanges();
            return question;
        }

        public void UpdateQuestion(Question question)
        {
            Question existing = GetQuestionByIdForAdmin(question.QuestionId);
            existing.QuestionText = question.QuestionText;
            existing.QuestionFormatJson = question.QuestionFormatJson;
            existing.QuestionType = question.QuestionType;
            existing.Operand1 = question.Operand1;
            existing.Operand2 = question.Operand2;
            existing.Operator = question.Operator;
            existing.OpExpression = question.OpExpression;
            existing.CorrectAnswer = question.CorrectAnswer;
            existing.CorrectOption = question.CorrectOption;
            existing.FormatType = question.FormatType;
            existing.VariableList = question.VariableList;
            existing.IsInactive = question.IsInactive == true ? false : true;
            existing.AnswerForamtJson = question.AnswerForamtJson;
            existing.AnswerFormatType = question.AnswerFormatType;
            db.SaveChanges();
        }


        #endregion

        #region ANSWER OPTIONS

        public AnswerOption GetAnswerOptionsByQuestionId(int questionId)
        {
            return db.AnswerOptions.Where(m => m.QuestionId == questionId).First();
        }
        public void AddAnswerOption(AnswerOption answersOption)
        {
            db.AnswerOptions.Add(answersOption);
            db.SaveChanges();
        }

        public void UpdateAnswerOption(AnswerOption answersOption)
        {

            AnswerOption existing = GetAnswerOptionsByQuestionId(answersOption.QuestionId);
            existing.CorrectAnswer = answersOption.CorrectAnswer;
            existing.CorrectOption = answersOption.CorrectOption;
            existing.Opt1 = answersOption.Opt1;
            existing.Opt2 = answersOption.Opt2;
            existing.Opt3 = answersOption.Opt3;
            existing.Opt4 = answersOption.Opt4;
            db.SaveChanges();

        }

        #endregion

        #region  SHEET & WORKSHEET
        public bool AddWorkSheet(int SubTopicId, string SheetDescription)
        {
            try
            {
                int Count = db.Sheet.Where(x => x.SubTopicId == SubTopicId).Count();
                db.Sheet.Add(new Sheet
                {
                    SheetId = String.Format("{0}_A{1}", SubTopicId, Count + 1),
                    SubTopicId = SubTopicId,
                    Description = SheetDescription,
                    NumOfQuestions = 0,
                });
                db.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool ChangeWorkSheetStatus(string SheetId, bool? IsInActive)
        {
            try
            {
                Sheet objSheet = db.Sheet.Where(x => x.SheetId == SheetId).FirstOrDefault();
                objSheet.IsInactive = IsInActive;
                db.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public List<Sheet> GetSheetBySubTopicId(int SubTopicId)
        {
            return db.Sheet.Where(x => x.SubTopicId == SubTopicId).ToList();
        }

        public List<QuestionSheet> GetQuestionSheetBySheetId(string SheetId)
        {
            return db.QuestionSheet.Where(x => x.SheetId == SheetId).ToList();
        }

        public bool SaveSheetDesc(string sheetDesc, string sheetId)
        {
            try
            {
                Sheet objsheet = db.Sheet.Where(x => x.SheetId == sheetId).FirstOrDefault();
                objsheet.Description = sheetDesc;
                db.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool AddQuestionInSheet(String SheetId, int QuestionId)
        {
            try
            {
                db.QuestionSheet.Add(new QuestionSheet() { SheetId = SheetId, QuestionId = QuestionId });
                db.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool DeleteQuestionInSheet(QuestionSheet objQuestionSheet)
        {
            try
            {
                db.QuestionSheet.Remove(objQuestionSheet);
                db.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        #endregion

        #region GROUPING OF SUBTOPIC

        public List<Group> GetGroupList()
        {
            return db.Group.ToList();
        }

        public string Addgroup(int GroupId, string Description)
        {
            try
            {
                if (db.Group.Any(x => x.Description.ToLower() == Description.ToLower()))
                    return "Group Already Exists";
                db.Group.Add(new Group
                {
                    GroupId = GroupId,
                    Description = Description
                });
                db.SaveChanges();
                return "";
            }
            catch
            {
                return "Opps!!! Error Occured. Please try agian";
            }
        }

        public string DeleteGroup(int groupid)
        {
            try
            {
                var Group = db.Group.Where(x => x.GroupId == groupid).FirstOrDefault();
                if (Group != null)
                {
                    var subTopic = db.SubTopicGroup.Where(x => x.GroupId == groupid).ToList();
                    foreach (var item in subTopic)
                    {
                        db.SubTopicGroup.Remove(item);
                    }

                    db.Group.Remove(Group);
                    db.SaveChanges();
                }
                return "";

            }
            catch
            {
                return "Opps!!! Error Occured. Please try agian";
            }
        }

        public List<SubTopicGroup> GetSubTopiGroupList()
        {
            return db.SubTopicGroup.Include("Subtopic").Include("Group").ToList();
        }

        public string AddSubTopicToGroup(int SubTopicTGroupId, int SubTopicId, int GroupId)
        {
            try
            {
                if (db.SubTopicGroup.Any(x => x.SubTopicId == SubTopicId && x.GroupId == GroupId))
                    return "Group Already Assigned";
                db.SubTopicGroup.Add(new SubTopicGroup
                {
                    SubTopicGroupId = SubTopicTGroupId,
                    GroupId = GroupId,
                    SubTopicId = SubTopicId
                });
                db.SaveChanges();
                return "";
            }
            catch
            {
                return "Opps!!! Error Occured. Please try agian";
            }
        }

        public string DeleteSubTopicFromGroup(int SubTopicGroupId)
        {
            try
            {
                var SubTopicGroup = db.SubTopicGroup.Where(x => x.SubTopicGroupId == SubTopicGroupId).FirstOrDefault();
                if (SubTopicGroup != null)
                {

                    db.SubTopicGroup.Remove(SubTopicGroup);
                    db.SaveChanges();
                }
                return "";

            }
            catch
            {
                return "Opps!!! Error Occured. Please try agian";
            }
        }

        #endregion

        #region OTHER METHODS

        public void AddQuestionList(List<CompleteQuestion> qList, bool integrityVerifcation = true)
        {
            int startCountQ = db.Questions.Count() + 1;
            int startCountAnsOption = db.AnswerOptions.Count() + 1;
            int startCountQuestionDetails = db.QuestionDetails.Count() + 1;

            EstablishRelationShipAndUpdateTableIds(ref qList, startCountQ);
            //get all SubTopic 
            //var questions = 
            InsertCompleteQuestionInContext(qList);

        }

        public void InsertSubTopic(List<SubTopic> subTopics)
        {
            subTopics.ForEach(m => db.SubTopics.Add(m));
            db.SaveChanges();
        }

        public void EstablishRelationShipAndUpdateTableIds(ref List<CompleteQuestion> completeQuestionList, int startCount)
        {
            if (startCount == 0)
                throw new Exception("Wrong Arguments - provide the start Question Number");

            foreach (var item in completeQuestionList)
            {
                item.Question.QuestionId = startCount;

                //update QuestionDetail
                item.QuestionDetail.QuestionDetailId = startCount;
                item.QuestionDetail.QuestionId = startCount;

                //update Options
                if (item.Option != null)
                {
                    item.Option.AnswerOptionId = startCount;
                    item.Option.QuestionId = startCount;
                    if (item.Option.CorrectAnswer != null)
                    {
                        item.Question.CorrectAnswer = item.Option.CorrectAnswer;
                    }
                }
                startCount++;
            }
        }

        public void InsertCompleteQuestionInContext(List<CompleteQuestion> completeQuestionList)
        {

            var count = db.Questions.Count() + 1;
            EstablishRelationShipAndUpdateTableIds(ref completeQuestionList, count);

            var qList = completeQuestionList.Select(m => m.Question).ToList();
            var qDetailList = completeQuestionList.Select(m => m.QuestionDetail).ToList();
            var aOptionList = completeQuestionList.Select(m => m.Option).ToList();

            qList.ForEach(s => db.Questions.Add(s));
            db.SaveChanges();
            Console.WriteLine("updated questionList");

            if (aOptionList.Count > 0)
            {
                aOptionList.ForEach(s => db.AnswerOptions.Add(s));
                db.SaveChanges();
                Console.WriteLine("updated aOptionList");
            }

            if (qDetailList.Count > 0)
            {
                qDetailList.ForEach(s => db.QuestionDetails.Add(s));
                db.SaveChanges();
            }
            Console.WriteLine("updated qDetailList");
        }

        public List<int> GetsubTopicListFromQuestionList(List<CompleteQuestion> qList)
        {
            List<int> subTopicList = new List<int>();
            qList.ForEach(m =>
            {
                if (m.QuestionDetail != null)
                {
                    if (!subTopicList.Contains(m.QuestionDetail.SubTopicId))
                        subTopicList.Add(m.QuestionDetail.SubTopicId);
                };
            }
            );
            return subTopicList;
        }

        //Sept 12, 2014 - Creating new Worksheet
        public void AddWorksheetsForSubTopic(int subTopicId, List<QuestionSheet> QuestionSheets)
        {
            QuestionSheets.ForEach(m => db.QuestionSheet.Add(m));
            db.SaveChanges();
        }

        //todo
        #region ToBe Deleted - Duplicated in Repository
        public bool IsCombinedSubTopic(int subTopicId)
        {
            bool? isCombinedSubTopic = db.SubTopics.Where(m => m.SubTopicId == subTopicId).FirstOrDefault().IsCombinedSubTopic;
            return isCombinedSubTopic.HasValue ? isCombinedSubTopic.Value : false;
        }
        public List<int> CombinedSubTopicIdList(int subTopicId) //main SubTopicId
        {
            var idList = new List<int>();
            string s = db.SubTopics.Where(m => m.SubTopicId == subTopicId).FirstOrDefault().CombineSubTopicIdList;
            if (!String.IsNullOrWhiteSpace(s))
            {
                var subTopicIdList = s.Split(new char[] { ';' });

                subTopicIdList.ToList().ForEach(m =>
                {
                    int tempId = 0;
                    if (Int32.TryParse(m, out tempId)) idList.Add(tempId);
                });
            }
            return idList;
        }
        #endregion
        #endregion

        #region GRADE
        public List<GradeSubTopic> GetGradesBySubTopicId(int subTopicId)
        {
            return db.GradeSubTopics.Where(m => m.SubTopicId == subTopicId).ToList();
        }


        public void AddGradeSubTopic(int subTopicId, int gradeID)
        {
            db.GradeSubTopics.Add(new GradeSubTopic { GradeId = gradeID, SubTopicId = subTopicId });
            db.SaveChanges();
        }

        public void DeleteGradeSubTopic(GradeSubTopic gradeSubTopicRecord)
        {
            db.GradeSubTopics.Remove(gradeSubTopicRecord);
            db.SaveChanges();
        }


        #endregion

        #region  HTML Meta Data Listing

        public int AddMetaData(HtmlMetaData objMetaData)
        {
            try
            {
                db.HtmlMetaData.Add(objMetaData);
                db.SaveChanges();
                return objMetaData.HtmlMetaDataId;
            }
            catch
            {
                return 0;
            }
        }
        public bool AddMetaDataRelationShip(HtmlMetaRelation objHtmlMetaRelation)
        {
            try
            {
                db.HtmlMetaRelation.Add(objHtmlMetaRelation);
                db.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public void ChangeStatus(int id, bool IsDeleted)
        {
            HtmlMetaData data = db.HtmlMetaData.Where(x => x.HtmlMetaDataId == id).FirstOrDefault();
            if (IsDeleted)
            {
                HtmlMetaRelation dataRelation = db.HtmlMetaRelation.Where(x => x.HtmlMetaDataId == id).FirstOrDefault();
                db.HtmlMetaData.Remove(data);
                db.HtmlMetaRelation.Remove(dataRelation);
            }
            else
            {
                data.IsActive = data.IsActive == true ? false : true;
            }

            db.SaveChanges();
        }
        public HtmlMetaData GetMetaDataById(int id)
        {
            return db.HtmlMetaData.Where(x => x.HtmlMetaDataId == id).FirstOrDefault();
        }
        public void UpdateMetaData(HtmlMetaData htmlData)
        {
            HtmlMetaData data = db.HtmlMetaData.Where(x => x.HtmlMetaDataId == htmlData.HtmlMetaDataId).FirstOrDefault();
            data.Title = htmlData.Title;
            data.RouteUrl = htmlData.RouteUrl;
            data.PageType = htmlData.PageType;
            data.MetaDesc = htmlData.MetaDesc;
            data.Keywords = htmlData.Keywords;
            data.IsActive = htmlData.IsActive;
            data.H1Tag = htmlData.H1Tag;
            db.SaveChanges();
        }
        #endregion

        #region NOT USED METHODS
        //private bool IsDbContains(List<string> subTopicList)
        //{
        //    return true;
        //}
        #endregion

        public List<ManualQuestion> GetManualQuestionList()
        {
            return db.ManualQuestion.ToList();
        }
        public bool AddManualQuestion(ManualQuestion manual)
        {
            try
            {
                db.ManualQuestion.Add(manual);
                db.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }
        public void DeleteManualQuestion(int ManualQuestionId)
        {
            var manualQuestion = db.ManualQuestion.Where(m => m.ManualQuestionId == ManualQuestionId).FirstOrDefault();
            db.ManualQuestion.Remove(manualQuestion);
            db.SaveChanges();

        }
        public IList<UserLoginStatsVM> GetUserStats(DateTime activityStartDate, DateTime activityEndDate)
        {
            var UsersStats = db.Database.SqlQuery<UserLoginStatsVM>("sp_GetUserLoginStats @startdate,  @enddate"
                                                                , new SqlParameter("startdate", activityStartDate)
                                                                , new SqlParameter("enddate", activityEndDate)).ToList();
            return UsersStats;
        }

        public IList<UserProgressStatsVM> GetUserProgressStats(DateTime activityStartDate, DateTime activityEndDate)
        {
            var UsersProgressStats = db.Database.SqlQuery<UserProgressStatsVM>("sp_GetUserProgressStats @startdate,  @enddate"
                                                                , new SqlParameter("startdate", activityStartDate)
                                                                , new SqlParameter("enddate", activityEndDate)).ToList();
            return UsersProgressStats;
        }
    }
}
