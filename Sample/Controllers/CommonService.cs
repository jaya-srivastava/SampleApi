using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.Web.Mvc;
using System.Threading.Tasks;
using System.Web.Security;
using iPractice.RestServices;

using iPractice.Helpers;
using iPractice.ViewModel;

namespace iPractice.Controllers
{
    public class CommonService
    {
        //AccountService accountService = new AccountService();
        //private MainRepository rp = new MainRepository();
        //private ReportRepository reportService = new ReportRepository();
        GradeService _gradesService = new GradeService();

        
        public async Task<SelectList> GetGrades()
        {
            List<SelectListItem> GradeList = new List<SelectListItem>();
            GradeList.Add(new SelectListItem() { Text = " --- Select Grade --- ", Value = "-100" });
            var lstGrades = await _gradesService.GetGradesAsync();
            foreach (var item in lstGrades)
            {
                GradeList.Add(new SelectListItem() { Text = item.GradeDesc2, Value = item.GradeId.ToString() });
            }
            SelectList lists = new SelectList(GradeList, "Value", "Text", "-100");
            return lists;
        }
    }
}