using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Sample.Models;
using Sample.Models.DTO;

namespace Sample.Service
{
    public static class AutoMapperConfigurator
    {
        public static void Configure()
        {
            Mapper.Configuration.AllowNullDestinationValues = true;
            Mapper.AllowNullDestinationValues = true;
            Mapper.CreateMap<Topic, TopicDto>();
           
            
            Mapper.CreateMap<Grade, GradeDto>().ReverseMap();
            Mapper.CreateMap<GradeSubTopic, GradeSubTopicDto>();
            
            Mapper.CreateMap<SubTopicDto, SubTopic>();
            Mapper.CreateMap<SubTopicDetailDto, SubTopic>();

            
            
        }
    }
}
