using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sample.Models
{
    public class GradeSubTopic
    {
        public int GradeSubTopicId { get; set; }

        public int GradeId { get; set; }

        public int SubTopicId { get; set; }

        public bool? IsInactive { get; set; }

        #region Navigation Properties

        public virtual Grade Grade { get; set; } 
        public virtual SubTopic SubTopic { get; set; }

        #endregion


    }
}
