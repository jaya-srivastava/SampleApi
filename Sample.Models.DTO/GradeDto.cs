using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sample.Models.DTO
{

    //Grade/Index
    public class GradeDto 
    {
        public int GradeId { get; set; }
        public string GradeDesc { get; set; } 
        public string GradeDesc2 { get; set; }

        public ICollection<GradeSubTopicDto> GradeSubTopics { get; set; }
    }

    //Grade/Index
    public class GradeSubTopicDto
    {
        public int GradeId { get; set; }
        //public int SubTopicId { get; set; }
        public bool? IsInactive { get; set; }
        public SubTopicDetailDto SubTopic { get; set; }
        //public virtual GradeDto Grade { get; set; } 

    }

	//Grade/Details
    public class GradeDetailDto
    {
        public int GradeId { get; set; }
        public string GradeDesc { get; set; }
        public string GradeDesc2 { get; set; }
        public string Email { get; set; }

        public List<GradeTopicDetailDto> Topics { get; set; }
        //public List<QuestionDto> Questions { get; set; }

    }
    public class GradeTopicDetailDto
    {
        public string TopicId { get; set; }
        public string TopicDesc { get; set; }

        public List<GradeSubTopicDto> GradeSubTopics { get; set; }

    }




}
