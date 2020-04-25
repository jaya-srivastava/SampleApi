using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace iPractice.Helpers
{
    public class Enums
    {
        public enum CommonReportType
        {
            Daily,
            Weekly,
            Monthly
        }

        public enum CertificateType
        {
            Weekly,
            Monthly,
            COfParticipation,
            COfAwAttitude,
            COfExcellence,
            COfShStar
        }

        public enum NotificationType
        {
            Feedback,
            SchoolAdminRegistration,
            DisttAdminRegistration,
            QuestionReport,
            Grade_1,
            Grade_2,
            Grade_3,
            Grade_4,
            Grade_5,
            Grade_6,
            Grade_7,
            Grade_8,
            Grade_9,
            Grade_10,
            
        }

        public enum Role
        {
            Student,
            Parent,
            Teacher,
            HsTeacher,
            SchoolAdmin,
            DisttAdmin,
            SA
        }


        public enum BillingPeriodType
        {
            Day,
            Week,
            SemiMonth,
            Month,
            Year
        }


        public enum QuestionCount
        {
            QuestionAttempted
        }

        public enum AssignWorksheetType
        {
            SystemAssigned,
            TeacherAssigned
        }
    }
}