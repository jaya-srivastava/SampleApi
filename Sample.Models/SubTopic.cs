using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sample.Models
{
    public partial class SubTopic
    {
        #region Primitive Properties	
		
		[DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int SubTopicId { get; set; }

        public string SubTopicDesc { get; set; }

        public string TopicId { get; set; }

        public bool? IsInactive { get; set; }

        public string Hint { get; set; }

		public string SubTopicDescSEO { get; set; }

		public bool? IsSEOUrlActive { get; set; }

        public string PrefixText { get; set; }

        public string SuffixText { get; set; }

		public string QuestionTextFormat { get; set; }

        public int? SubTopicDiffLevel { get; set; }

		public bool? IsQuestionTextFormat { get; set; }

        public bool? IsPrefixShown { get; set; }

        public bool? IsSuffixShown { get; set; }

        public int? HTMLMetaDataId { get; set; }

        public bool? IsToolbar { get; set; }

        public string WorksheetText { get; set; }

        public string DisplayFormat { get; set; }

        public bool IsSignInRequired { get; set; }

        public bool IsPaidRequired { get; set; }
        
        #endregion
      
        #region Navigation Properties

        
        public virtual ICollection<GradeSubTopic> GradeSubTopics { get; set; }

        public virtual Topic Topic { get; set; }

        public virtual ICollection<SubTopicGroup> SubTopicGroups { get; set; }
        #endregion

        public bool? IsCombinedSubTopic { get; set; }
        public string CombineSubTopicIdList { get; set; }
       
    
    }
    
}
