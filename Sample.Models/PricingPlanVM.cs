using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using iPractice.Models.DTO;

namespace iPractice.Models
{
    public class PricingPlanVM
    {
        public PricingPlanDto PricingPlan { get; set; }
        public List<PricingPlanDto> ListPricingPlan { get; set; }

        public PricingPlanVM ()
        {
            this.ListPricingPlan = new List<PricingPlanDto>();
        }
    }
}
