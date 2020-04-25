using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;

namespace Sample.Models
{
    public partial class Topic
    {
        #region Primitive Properties

        public  string TopicId { get; set; }
      

        public  string TopicDesc  { get; set; }
       

        #endregion
        #region Navigation Properties

        //public virtual ICollection<QuestionDetail> QuestionDetail { get; set; }


        public virtual ICollection<SubTopic> SubTopic { get; set; }
       

        #endregion

        public bool? IsActive { get; set;  }
        public DateTime? DateAdded { get; set; }
       
    }
  
}
