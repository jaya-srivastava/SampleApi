using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace iPractice.ViewModel
{
    //LeaderBoard will be shown based on this
    //todo : may be need to change VM name in future
    public class CommonReportVM
    {
        //UserId
        public string UI { get; set; }

        //Email
        public string E { get; set; }

        //UserName
        public string UN { get; set; }

        //Question Attempted
        public int QA { get; set; }

        //Correct Number
        public int CN { get; set; }

        //Correct Percentage
        public decimal CP { get; set; }

        //Is Register user
        public bool IR { get; set; }

    }


    public class CertificateVM
    {
        public string TopicDesc { get; set; }

        public string SubTopicDesc { get; set; }

        public string UserFirstName { get; set; }

        public string UserLastName { get; set; }

        public int Attempted { get; set; }

        public int Correct { get; set; }

        public string UserName { get; set; }

        public string Gender { get; set; }

        public int? CertificateID { get; set; }

        public string TemplateName { get; set; }

        public string DateOfCompletion { get; set; }

    }
}