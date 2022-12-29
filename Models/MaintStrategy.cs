using System; 
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ProDat.Web2.Models
{
    public class MaintStrategy
    {
        public MaintStrategy()
        {
            MaintPlans = new HashSet<MaintPlan>();
            TaskListHeaders = new HashSet<TaskListHeader>();
        }

        public int MaintStrategyId { get; set; }

        [Display(Name = "Doc.")]
        public int DocId { get; set; }

        [Required]
        [Remote(action: "ValidateName", controller: "MaintStrategies", ErrorMessage = "Name exists.")]
        public string MaintStrategyName { get; set; }
        public string MaintStrategyDesc { get; set; }

        // removed. Not correct. pending response from Nicco for what we need here, if anything...
        //public string MaintStrategySortId { get; set; }
        //public DateTime StartDate { get; set; }
        //public string MeasPointNo { get; set; }
        //public string CallHorizon { get; set; }
        //public string SchedulingPeriod { get; set; }

        public virtual Doc Doc { get; set; }
        public virtual ICollection<MaintPlan> MaintPlans { get; set; }
        public virtual ICollection<TaskListHeader> TaskListHeaders { get; set; }
    }
}
