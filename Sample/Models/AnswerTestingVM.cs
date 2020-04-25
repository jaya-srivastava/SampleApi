using Sample.Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sample.Models
{
    public class AnswerTestingVM
    {

        public IEnumerable<SubTopicDto> SubTopics { get; set; }

        public IEnumerable<TopicDto> Topics { get; set; }

       // public IEnumerable<QuestionDto> Questions { get; set; }

    }
}