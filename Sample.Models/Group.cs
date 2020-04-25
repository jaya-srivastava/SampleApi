using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sample.Models
{
    public class Group
    {
        #region Primitive Properties

        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int GroupId { get; set; }

        public string Description { get; set; }

        public bool? IsInActive { get; set; }

        #endregion
    }
    public class SubTopicGroup
    {
        #region Primitive Properties

        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int SubTopicGroupId { get; set; }

        public int SubTopicId { get; set; }
        public int GroupId { get; set; }
        public bool? IsInActive { get; set; }
        #endregion


        public virtual Group Group { get; set; }
        public virtual SubTopic SubTopic { get; set; }
    }
}
