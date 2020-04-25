using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sample.Models
{
    public class HtmlMetaData
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int HtmlMetaDataId { get; set; }

        public string Title { get; set; }

        public string MetaDesc { get; set; }

        public string Keywords { get; set; }

        public string H1Tag { get; set; }

        //public List<string> H2Tags { get; set; }

        public string RouteUrl { get; set; }

        public string PageType { get; set; }//worksheet, math-problem, LearnPage etc

        public bool IsActive { get; set; }
    }
    public class HtmlMetaRelation
    {
        public int HtmlMetaRelationId { get; set; }
        public int SubTopicId { get; set; }
        public int WorkSheetId { get; set; }
        public string RouteUrl { get; set; }
        public int HtmlMetaDataId { get; set; }

    }
}
