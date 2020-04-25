using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sample.Models.DTO;
using Sample.Models;

namespace Sample.Service
{
    public interface IGradeService
    {
        List<GradeDto> GetGrades();
       
        //for all Subtopics - applicable to Paid Users 
        GradeDetailDto GetSubTopicsByGradeId(int gradeId);
        int AddGrade(GradeDto grade);
        GradeDetailDto GetSubTopicsByGradeIdUserName(int gradeId,string UserName);
    }
}
