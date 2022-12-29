using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ProDat.Web2.Models
{
    public class MaintPlan
    {
        public MaintPlan()
        {
            Tags = new HashSet<Tag>();
        }

        public int MaintPlanId { get; set; }
        [Required]
        //[Remote("NameAvailable", "MaintPlans", ErrorMessage = "Name already exists.")]
        public string MaintPlanName { get; set; }
        [Required]
        public string ShortText { get; set; }
        public int? MaintStrategyId { get; set; }

        [Required]
        public int MaintSortProcessId { get; set; }
        public string Sort { get; set; }
        public double? CycleModFactor { get; set; }
        public string StartDate { get; set; }
        public int? MeasPointId { get; set; }
        public string ChangeStatus { get; set; }
        public string StartingInstructions { get; set; }
        public string CallHorizon { get; set; }
        public int? SchedulingPeriodValue { get; set; }

        [Display(Name = "Scheduling Period UOM")]
        public int? SchedulingPeriodUomId { get; set; }
        //public string SchedulingPeriodUom { get; set; }

        public virtual MaintSortProcess MaintSortProcess { get; set; }
        public virtual MaintStrategy MaintStrategy { get; set; }
        public virtual MeasPoint MeasPoint { get; set; }
        public virtual ICollection<Tag> Tags { get; set; }
    }
}
