using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Sample.Models.DTO;
using Sample.Repository;
using Sample.Core.Caching.Caching;
using Sample.Models;

namespace Sample.Service
{
    public class GradeService : IGradeService
    {
        MainRepository mainRepo = new MainRepository();

        public List<GradeDto> GetGrades()
        {
            Func<List<GradeDto>> func = () => GetGradesInternal();
            var dtos = CacheServiceHelper.Get<List<GradeDto>>("GetGrades", func);
            return dtos;
        }

        public List<GradeDto> GetGradesInternal()
        {
            var grades = mainRepo.GetGrades();
            var dtos = Mapper.Map<List<GradeDto>>(grades);
            return dtos;
        }

        public GradeDetailDto GetSubTopicsByGradeIdInteral(int gradeId)
        {
            var gradeSubTopics = mainRepo.GetSubTopicsByGradeId(gradeId);
            if (gradeSubTopics == null ) return null;
            var grade = mainRepo.GetGradebyId(gradeId);
            if (grade == null) return null;

            return FindGradeDetails(gradeId, gradeSubTopics, grade);
        }

        private static GradeDetailDto FindGradeDetails(int gradeId, List<Models.GradeSubTopic> gradeSubTopics, Models.Grade grade)
        {
            var Topicgroups = gradeSubTopics.Select(x => x.SubTopic.TopicId).Distinct().ToList();
            //var groups = gradeSubTopics.GroupBy(m => m.SubTopic.TopicId);

            List<GradeTopicDetailDto> gradeTopicDetails = new List<GradeTopicDetailDto>();
            foreach (var g in Topicgroups)
            {
                var grps = gradeSubTopics.Where(x => x.SubTopic.TopicId == g).ToList();

                var gradeSubTopicDto = Mapper.Map<List<GradeSubTopicDto>>(grps);
                gradeTopicDetails.Add(new GradeTopicDetailDto
                {
                    TopicId = g,
                    TopicDesc = grps.FirstOrDefault().SubTopic.Topic.TopicDesc,
                    GradeSubTopics = gradeSubTopicDto ?? null,
                });
            }

            var gradeDetailDto = new GradeDetailDto
            {
                GradeId = gradeId,
                GradeDesc = grade.GradeDesc,
                GradeDesc2 = grade.GradeDesc2,
                Topics = gradeTopicDetails,
            };


            return gradeDetailDto;
        }

        public GradeDetailDto GetSubTopicsByGradeIdUserName(int gradeId, string UserName)
        {
            Func<GradeDetailDto> func = () => GetSubTopicsByGradeIdUserNameInteral(gradeId, UserName);
            var cacheKey = String.Format("GetSubTopicsByGradeId_{0}", gradeId);
            var dtos = CacheServiceHelper.Get<GradeDetailDto>(cacheKey, func);
            return dtos;
        }

        private GradeDetailDto GetSubTopicsByGradeIdUserNameInteral(int gradeId, string UserName)
        {
            var gradeSubTopics = mainRepo.GetSubTopicsByGradeIdUserName(gradeId, UserName);
            if (gradeSubTopics == null ) return null;

            var grade = mainRepo.GetGradebyId(gradeId);
            if (grade == null) return null;

            return FindGradeDetails(gradeId, gradeSubTopics, grade);
        }

        public GradeDetailDto GetSubTopicsByGradeId(int gradeId)
        {
            Func<GradeDetailDto> func = () => GetSubTopicsByGradeIdInteral(gradeId);
            var cacheKey = String.Format("GetSubTopicsByGradeId_{0}", gradeId);
            var dtos = CacheServiceHelper.Get<GradeDetailDto>(cacheKey, func);
            return dtos;
        }

        public int AddGrade(GradeDto gradeDto)
        {
            Grade grade = Mapper.Map<Grade>(gradeDto);
            int res = mainRepo.AddGrade(grade);
            return res;

        }


    }
}
