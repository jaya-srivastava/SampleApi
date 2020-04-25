using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using Sample.Models;
using Sample.Core.Caching;
using Sample.Core.Caching.Caching;
using System.Threading.Tasks;
using Sample.Repository.Helpers;
using System.Text;
using System.Data.Entity.Core.Objects;


namespace Sample.Repository
{
    public partial class MainRepository : BaseRepository
    {
      
        #region Grade
        public List<Grade> GetGrades()
        {
            Func<List<Grade>> func = () =>
            {
                var gList = db.Grades != null ? db.Grades.Include("GradeSubTopics").ToList() : null; //.Where(g=> g.GradeSubTopics.Select(gs=>gs.SubTopic.IsInactive.ToList() : null;
                foreach (var grade in gList)
                {
                    grade.GradeSubTopics.ToList().ForEach(m => { m.SubTopic.SubTopicDescSEO = m.SubTopic.SubTopicDesc.ToStringURLBuilder(); });
                }
                return gList;
            };
            //List<Grade> gradeList =  CacheServiceHelper.Get<List<Grade>>("GetGradesWithSubTopic", func);
            //gradeList.ForEach(m => m.GradeSubTopics.ToList().ForEach(gs => gs.SubTopic = GetSubTopicBySubTopicIdWithoutFilter(gs.SubTopicId)));
            return func();
        }
        public List<GradeSubTopic> GetSubTopicsByGradeId(int gradeId)
        {
            List<GradeSubTopic> gradeSubtopic = new List<GradeSubTopic>();

            gradeSubtopic = db.GradeSubTopics.Include("SubTopic").Where(m => m.GradeId == gradeId && (m.IsInactive == false || m.IsInactive == null)).ToList();
            gradeSubtopic.ForEach(m => { m.SubTopic.SubTopicDescSEO = m.SubTopic.SubTopicDesc.ToStringURLBuilder(); });
            return gradeSubtopic;
        }
        public Grade GetGradebyId(int id)
        {
            Func<Grade> func = () => db.Grades.Where(m => m.GradeId == id).FirstOrDefault();
            //return CacheServiceHelper.Get<List<Grade>>("GetGradebyGradeId_" + id, func);
            return func();
        }

        public List<GradeSubTopic> GetSubTopicsByGradeIdUserName(int gradeId, string UserName)
        {
            List<GradeSubTopic> gradeSubtopic = new List<GradeSubTopic>();
           
            gradeSubtopic = db.GradeSubTopics.Include("SubTopic").Where(m => m.GradeId == gradeId && m.SubTopic.IsSignInRequired == false && (m.IsInactive == false || m.IsInactive == null)).ToList();
            
            gradeSubtopic.ForEach(m => { m.SubTopic.SubTopicDescSEO = m.SubTopic.SubTopicDesc.ToStringURLBuilder(); });
            return gradeSubtopic;
        }

        public int AddGrade(Grade grade)
        {
            db.Grades.Add(grade);
            db.SaveChanges();
            return grade.GradeId;
        }

        #endregion



        public void Save()
        {
            db.SaveChanges();
        }

        #region IDisposable Members

        public override void Dispose()
        {
            db.Dispose();
        }

        #endregion


     
      
    }
}

