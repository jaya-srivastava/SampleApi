using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Sample.Service;
using Sample.Models;
using Sample.Models.DTO;
using System.Web.Http.Cors;

namespace IPracticeApi.Controllers
{
    [RoutePrefix("api/v1/grade")]
    public class GradeController : ApiController
    {
        private IGradeService _gradeService = new GradeService();
      
        // GET api/grade/GetGrades        
        [Route("GetGrades")]
        public IHttpActionResult GetGrades()
        {
            var item = _gradeService.GetGrades();            
            return Ok(item);
            
        }

       

        // GET api/grade/GetSubTopicsByGradeId        
        [Route("GetSubTopicsByGradeIdUserName")]
        public IHttpActionResult GetSubTopicsByGradeIdUserName(int gradeId, string UserName)
        {
            GradeDetailDto gradeDetailDto = _gradeService.GetSubTopicsByGradeId(gradeId);
           
            return Ok(gradeDetailDto);
        }

        [Route("AddGrade")]
        public IHttpActionResult AddGrade([FromBody] GradeDto grade)
        {
            var item = _gradeService.AddGrade(grade);
            return Ok(item);
        }
        private GradeDetailDto GetFreeAnnonymousSubTopicsByGradeId(GradeDetailDto fullList)
        {
            GradeDetailDto lstSubtopicGroupDto = new GradeDetailDto();
            List<GradeTopicDetailDto> lstGradeTopic = new List<GradeTopicDetailDto>();
            foreach (var lstSubtopic in fullList.Topics)
            {
                var GradeSubTopics = lstSubtopic.GradeSubTopics.Where(x => x.SubTopic.IsSignInRequired == false && x.SubTopic.IsPaidRequired == false).ToList();
                if (GradeSubTopics.Count > 0)
                {
                    lstSubtopic.GradeSubTopics = GradeSubTopics;
                    lstGradeTopic.Add(lstSubtopic);
                }
            }
            lstSubtopicGroupDto.Topics = lstGradeTopic;
            return lstSubtopicGroupDto;
        }

        private GradeDetailDto GetFreeSubTopicsbyTopicId(GradeDetailDto fullList)
        {
            GradeDetailDto lstSubtopicGroupDto = new GradeDetailDto();
            List<GradeTopicDetailDto> lstGradeTopic = new List<GradeTopicDetailDto>();
            foreach (var lstSubtopic in fullList.Topics)
            {
                var GradeSubTopics = lstSubtopic.GradeSubTopics.Where(x=>x.SubTopic.IsPaidRequired == false).ToList();
                if (GradeSubTopics.Count > 0)
                {
                    lstSubtopic.GradeSubTopics = GradeSubTopics;
                    lstGradeTopic.Add(lstSubtopic);
                }
            }
            lstSubtopicGroupDto.Topics = lstGradeTopic;

            return lstSubtopicGroupDto;
        }

      
    }
}
