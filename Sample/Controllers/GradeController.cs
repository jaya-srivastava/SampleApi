using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using Sample.Models.DTO;
using System.Threading.Tasks;
using iPractice.RestServices;

namespace iPractice.Controllers
{
    public class GradeController : AppController
    {
        private GradeService gradeService = new GradeService();
        private string QuestionCount = System.Configuration.ConfigurationManager.AppSettings["QuestionsCount"].ToString() ?? "4";

        public ActionResult Index()
        {
            //var grades = await gradeService.GetGradesAsync();
            return View();
        }
    

        [Route("Grade/Grade-{id}-skills")]
        public async Task<ActionResult> Details(int id)
        {
            string username = "";
            if (Request.IsAuthenticated)
            {
                username = User.Identity.Name;
            }
            var subTopics =await gradeService.GetSubTopicsByGradeIdUserNameAsync(id,username);
            ViewBag.GradeId = id;
            if (subTopics != null)
            {
                subTopics.GradeId = id;
                
            }
            return View(subTopics);
        }

        [Route("Grade/Grade-1-5-skills")]
        public ActionResult GradeDetails()
        {
            return View("Grade1-5");
        }

        [Route("Grade/AddGrade")]
        public ActionResult AddAdministrators(GradeDto grade)
        {
            grade.GradeDesc = "test";
            grade.GradeDesc2 = "test";
            grade.GradeId = 1;

            var item = gradeService.AddGradeAsync(grade);
            return View(item);
        }
    }
}
