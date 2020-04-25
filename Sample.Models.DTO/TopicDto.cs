using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sample.Models.DTO
{
    //used in Topic/Index
    public class TopicDto 
    {
        public string TopicId { get; set; }
        public string TopicDesc { get; set; }
        public ICollection<SubTopicDto> SubTopic { get; set; }
        public bool? IsActive { get; set; }

        public DateTime? DateAdded { get; set; }
    }

    //used in Topic/Index
    public class SubTopicDto 
    {
        public int SubTopicId { get; set; }
        public string TopicId { get; set; }
        public string SubTopicDesc { get; set; }
        public string TopicTopicDesc { get; set; }
        public string Hint { get; set; }
        public string SubTopicDescSEO { get; set; }

        public bool? IsSEOUrlActive { get; set; }
        public bool? IsInactive { get; set; }
        public bool? IsActive { get; set; }

        public bool? IsCombinedSubTopic { get; set; }
        public string CombineSubTopicIdList { get; set; }

        public int? HTMLMetaDataId { get; set; }

        public bool? IsToolbar { get; set; }

        public string DisplayFormat { get; set; }

        public string PrefixText { get; set; }

        public string SuffixText { get; set; }

        public string QuestionTextFormat { get; set; }

        public int? SubTopicDiffLevel { get; set; }

        public bool? IsQuestionTextFormat { get; set; }

        public bool? IsPrefixShown { get; set; }

        public bool? IsSuffixShown { get; set; }

        public string WorksheetText { get; set; }

        public bool IsSignInRequired { get; set; }

        public bool IsPaidRequired { get; set; }

        //public virtual TopicDto Topic { get; set; }
        //public List<UserProgressDTO> UserProgress { get; set; }

       

    }

    //used in Topic/Details
    public class SubTopicDetailDto
    {
        public int SubTopicId { get; set; }
        public string SubTopicDesc { get; set; }

        public string Hint { get; set; }

        public string SubTopicDescSEO { get; set; }

        public bool? IsSEOUrlActive { get; set; }
        public bool? IsInactive { get; set; }

        public bool? IsCombinedSubTopic { get; set; }
        public string CombineSubTopicIdList { get; set; }

        public string TopicTopicId { get; set; }
        public string TopicTopicDesc { get; set; }
        public int? HTMLMetaDataId { get; set; }

        public bool? IsToolbar { get; set; }

        public string DisplayFormat { get; set; }

        public string QuestionTextFormat { get; set; }

        public string WorksheetText { get; set; }

        public bool IsSignInRequired { get; set; }

        public bool IsPaidRequired { get; set; }

        //public TopicDto Topic { get; set; }

        //public List<UserProgressDTO> UserProgress { get; set; }

        //public string CategoryDescription { get; set; }
        //public DisplayFormatJsonDto DisplayFormatJson { get; set; }
    }

    //used in Topic/Details
    public class SubTopicGroupDto
    {
        public int SubTopicGroupId { get; set; }
        //public int SubTopicId { get; set; }
        public int GroupId { get; set; }
        public bool? IsInActive { get; set; }
        public bool IsWorksheetExists { get; set; }
        public string GroupDescription { get; set; }
        public List<SubTopicDetailDto> SubTopics { get; set; }
        
    }

    public class SubTopicGroupDetailDTO 
    {
        public List<SubTopicGroupDto> SubTopicGroup { get; set; }

    } 
   

}
