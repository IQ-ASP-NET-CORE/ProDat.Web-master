using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ProDat.Web2.Models
{
    public class TaskListOperations
    {
        public int TaskListOperationId { get; set; }

        public int? TaskListHeaderId { get; set; }

        [Display(Name = "Num")]
        public int OperationNum { get; set; }

        [DefaultValue(0)]
        [Display(Name = "Sub. Num")]
        public int SubOperationNum { get; set; }

        [Display(Name = "Operation")]
        public int? OperationId { get; set; }

        [Display(Name = "Description")]
        public string OperationDescription {get; set;}

        [Display(Name = "Work Centre")]
        public int? MaintWorkCentreId { get; set; }

        [Display(Name = "Plant")]
        public int? MaintenancePlantId { get; set; }

        [Display(Name = "Control")]
        public int? ControlKeyId { get; set; }

        [Display(Name = "Sys Cond")]
        public int? SysCondId { get; set; }

        [Display(Name = "Rel to Op")]
        public int? RelationshiptoOperationId { get; set; }

        [Display(Name = "Short Text")]
        public string OperationShortText { get; set; }

        [Display(Name = "Long Text")]
        public string OperationLongText { get; set; }

        [Display(Name = "Work Hrs")]
        public string WorkHrs { get; set; }

        [Display(Name = "Cap No")]
        public int? CapNo { get; set; }
        
        [Display(Name = "Package")]
        public int? MaintPackageId { get; set; }

        [Display(Name = "Doc Ref")]
        public int? DocId { get; set; }

        public bool Ti { get; set; }

        [Display(Name = "Off Site")]
        public bool OffSite { get; set; }

        [Display(Name = "Fixed Oper Qty")]
        public int? FixedOperQty { get; set; }

        [Display(Name = "Change Req.")]
        public string ChangeRequired { get; set; }


        // entity relationships
        public virtual ControlKey ControlKey { get; set; }
        public virtual Doc Doc { get; set; }
        public virtual MaintPackage MaintPackage { get; set; }
        public virtual MaintWorkCentre MaintWorkCentre { get; set; }
        public virtual MaintenancePlant MaintenancePlant { get; set; }
        public virtual Operation Operation { get; set; }
        public virtual RelationshipToOperation RelationshiptoOperation { get; set; }
        public virtual SysCond SysCond { get; set; }
        public virtual TaskListHeader TaskListHeader { get; set; }
    }
}
