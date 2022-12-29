using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
namespace ProDat.Web2.Models
{
    public class MaintItem
    {
        public MaintItem()
        {
            //FlocXmaintItems = new HashSet<FlocXmaintItem>();
        }

        public int MaintItemId { get; set; }

        //[Display(Name = "Maint Plan")]
        //public int MaintItemMaintPlanId { get; set; } // converted to int

        public int? MaintPlanId { get; set; } // converted to int

        [Required]
        [Display(Name = "MI Num")]
        public string MaintItemNum { get; set; }

        [Display(Name = "Short Text")]
        public string MaintItemShortText { get; set; }

        [Display(Name = "Header Floc txt")]
        public string FMaintItemHeaderFloc { get; set; } // what is this?

        [Display(Name = "Header Floc")]
        public int? HeaderFlocId { get; set; } // what is this?

        [Display(Name = "Header Equipment")]
        public string MaintItemHeaderEquipId { get; set; }

        [Display(Name = "Object List Floc")]
        public string MaintItemObjectListFloc { get; set; }
        
        [Display(Name = "Object List Equip")]
        public string MaintItemObjectListEquip { get; set; }

        [Display(Name = "Work Centre")]
        public int? MaintWorkCentreId { get; set; }

        [Display(Name = "Maint Work Centre txt")]
        public string MaintItemMainWorkCentre { get; set; }

        [Display(Name = "Maint Work Centre Plant txt")]
        public string MaintItemMainWorkCentrePlant { get; set; }

        [Display(Name = "Maint Work Centre Plant")]
        public int? MaintenancePlantId { get; set; }

        [Display(Name = "Order Type")]
        public string MaintItemOrderType { get; set; }

        [Display(Name = "Planner Group")]
        public int? MaintPlannerGroupId { get; set; }

        [Display(Name = "Activity Type")]
        public string MaintItemActivityTypeId { get; set; } // really is a string...

        [Display(Name = "RevNo")]
        public string MaintItemRevNo { get; set; }

        [Display(Name = "User Status")]
        public string MaintItemUserStatus { get; set; }

        [Display(Name = "System Condition txt")]
        public string MaintItemSystemCondition_Old { get; set; }

        [Display(Name = "System Condition")]
        public int? SysCondId { get; set; }

        [Display(Name = "Consequence Category")]
        public string MaintItemConsequenceCategory { get; set; }

        [Display(Name = "Consequence")]
        public string MaintItemConsequence { get; set; }

        [Display(Name = "Likelihood")]
        public string MaintItemLikelihood { get; set; }

        [Display(Name = "Priority txt")]
        public string MaintItemProposedPriority { get; set; }

        [Display(Name = "Priority")]
        public int? PriorityId { get; set; }

        [Display(Name = "Proposed TI")]
        public string MaintItemProposedTi { get; set; }

        [Display(Name = "Long Text")]
        public string MaintItemLongText { get; set; }

        [Display(Name = "Task List Execution Factor txt")]
        public string MaintItemTasklistExecutionFactor { get; set; }

        [Display(Name = "Task List Execution Factor")]
        public double? TaskListExecutionFactor { get; set; }

        [Display(Name = "Do not Release Immediately txt")]
        public string MaintItemDoNotRelImmed { get; set; }

        [Display(Name = "Do not Release Immediately")]
        public bool bDoNotRelImmed { get; set; }

        public virtual Tag HeaderFloc { get; set; }

        public virtual MaintPlannerGroup MaintPlannerGroup { get; set; }

        public virtual MaintWorkCentre MaintWorkCentre { get; set; }

        public virtual MaintPlan MaintPlan { get; set; }

        public virtual MaintenancePlant MaintenancePlant { get; set; }

        public virtual SysCond SysCond { get; set; }

        public virtual Priority Priority { get; set; }

        public virtual ICollection<FlocXmaintItem> FlocXmaintItems { get; set; }

        public virtual ICollection<MaintItemXmaintTaskListHeader> MaintItemXmaintTaskListHeaders { get; set; }
    }
}
