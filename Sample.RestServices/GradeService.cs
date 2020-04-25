using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Sample.Models.DTO;
using System.Threading.Tasks;
using System.Net.Http;
using Newtonsoft.Json;
using iPractice.RestServices.Helper;

namespace iPractice.RestServices
{
    public class GradeService
    {
        public async Task<List<GradeDto>> GetGradesAsync()
        {
            using (var httpClient = new RestClient(CommonHelper.BaseUrl))
            {
                return await httpClient.GetManyAsync<GradeDto>("grade/GetGrades");
            }
        }

        public List<GradeDto> GetGrades()
        {
            using (var httpClient = new RestClient(CommonHelper.BaseUrl))
            {
                return httpClient.GetManyAsync<GradeDto>("grade/GetGrades").Result;
            }
        }

        public async Task<GradeDetailDto> GetSubTopicsByGradeIdAsync(int gradeId)
        {
            using (var httpClient = new RestClient(CommonHelper.BaseUrl))
            {
                return await httpClient.GetAsync<GradeDetailDto>("grade/GetSubTopicsByGradeId?gradeId=" + gradeId);
            }
        }


        public async Task<GradeDetailDto> GetSubTopicsByGradeIdUserNameAsync(int gradeId, string UserName)
        {
            using (var httpClient = new RestClient(CommonHelper.BaseUrl))
            {
                return await httpClient.GetAsync<GradeDetailDto>("grade/GetSubTopicsByGradeIdUserName?gradeId=" + gradeId + "&UserName=" + UserName);
            }
        }

        public async Task<int> AddGradeAsync(GradeDto grade)
        {
            using (var httpClient = new RestClient(CommonHelper.BaseUrl))
            {
                return await httpClient.PostRequestGenericAsync<GradeDto, int>("grade/AddGrade?gradeId" ,grade);
            }
        }


    }
}