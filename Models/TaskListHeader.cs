using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ProDat.Web2.Models
{
    public class TaskListHeader
    {
        public TaskListHeader()
        {
            TaskListXscePsreviews = new HashSet<TaskListXscePsreview>();
        }

        public int TaskListHeaderId { get; set; }

        [Display(Name = "Group")]
        public int TaskListGroupId { get; set; }

        [Display(Name = "Counter")]
        public int Counter { get; set; }

        [Display(Name = "Short Text")]
        public string TaskListShortText { get; set; }

        [Display(Name = "Work Centre")]
        public int? MaintWorkCentreId { get; set; }

        [Display(Name = "Plant")]
        public int? MaintenancePlantId { get; set; }

        [Display(Name = "Sys Cond")]
        public int? SysCondId { get; set; }

        [Display(Name = "Strategy")]
        public int? MaintStrategyId { get; set; }

        [Display(Name = "Package")]
        public int? MaintPackageId { get; set; }

        [Display(Name = "PM Assembly")]
        public int? PmassemblyId { get; set; }

        [Display(Name = "TL Cat")]
        public int? TasklistCatId { get; set; }

        [Display(Name = "Perf STD")]
        public int? PerformanceStandardId { get; set; }

        [Display(Name = "Perf STD App Del")]
        public string PerfStdAppDel { get; set; }

        [Display(Name = "SCE PS")]
        public int? ScePsReviewId { get; set; }

        [Display(Name = "Reg Body")]
        public int? RegulatoryBodyId { get; set; }

        [Display(Name = "Reg Body App Del")]
        public string RegBodyAppDel { get; set; }

        [Display(Name = "Change Req?")]
        public string ChangeRequired { get; set; }

        [Display(Name = "TL Class")]
        public int? TaskListClassId { get; set; }

        [Display(Name = "Planner Grp")]
        public int? MaintPlannerGroupId { get; set; }

        [Display(Name = "Status")]
        public int? StatusId { get; set; }



        // entity relationships
        public virtual MaintPackage MaintPackage { get; set; }
        public virtual MaintStrategy MaintStrategy { get; set; }
        public virtual MaintWorkCentre MaintWorkCentre { get; set; }
        public virtual MaintenancePlant MaintenancePlant { get; set; }
        public virtual PerformanceStandard PerformanceStandard { get; set; }
        public virtual Pmassembly Pmassembly { get; set; }
        public virtual RegulatoryBody RegulatoryBody { get; set; }
        public virtual SysCond SysCond { get; set; }
        public virtual TaskListGroup TaskListGroup { get; set; }
        public virtual TaskListCat TasklistCat { get; set; }
        public virtual MaintPlannerGroup MaintPlannerGroup { get; set; }
        public virtual SAPStatus Status { get; set; }

        public virtual ICollection<TaskListXscePsreview> TaskListXscePsreviews { get; set; }
        public virtual ICollection<MaintItemXmaintTaskListHeader> MaintItemXmaintTaskListHeaders { get; set; }
    }
}
