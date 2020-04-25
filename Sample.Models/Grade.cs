using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sample.Models
{
   public class Grade
    {
        public int GradeId { get; set; }

        public string GradeDesc { get; set; }

        public string GradeDesc2 { get; set; }

        public virtual ICollection<GradeSubTopic> GradeSubTopics { get; set; }

        //public virtual ICollection<User> User { get; set; }
        //public virtual ICollection<SubTopic> SubTopics { get; set; }

    }
}
