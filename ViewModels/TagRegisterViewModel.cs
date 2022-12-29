using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ProDat.Web2.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ProDat.Web2.ViewModels
{
    public class TagRegisterViewModel
    {
        public int TagId { get; set; }

        public SelectList EngDiscList { get; set; }

        public SelectList MaintCriticalityList { get; set; }

        public SelectList MaintLocationList { get; set; }

        public SelectList MaintObjectTypeList { get; set; }

        public SelectList SubSystemList { get; set; }

        [Required]
        [Display(Name = "Tag Number")]
        public string TagNumber { get; set; }
        public string TagService { get; set; }

        [Display(Name = "FLOC"), MaxLength(15, ErrorMessage = "Max Length is 15")]
        public string TagFloc { get; set; }

        [Display(Name = "Sub Sys.")]
        public int? SubSystemId { get; set; }
        public int? EngClassId { get; set; }

        [Display(Name = "Eng. Parent")]
        public int? EngParentId { get; set; }
        public int? MaintParentId { get; set; }

        [Display(Name = "Disc")]
        public int? EngDiscId { get; set; }

        [Display(Name = "Maint. Loc.")]
        public int? MaintLocationId { get; set; }
        public int? LocationId { get; set; }
        public int? MaintTypeId { get; set; }
        public int? MaintStatusId { get; set; }
        public int? EngStatusId { get; set; }
        public int? MaintWorkCentreId { get; set; }
        public int? EdcId { get; set; }
        public int? MaintStructureIndicatorId { get; set; }
        public int? CommissioningSubsystemId { get; set; }
        public int? CommClassId { get; set; }
        public int? ComZoneId { get; set; }
        public int? MaintPlannerGroupId { get; set; }
        public int? MaintenanceplanId { get; set; }

        [Display(Name = "Maint. Crit.")]
        public int? MaintCriticalityId { get; set; }
        public int? PerformanceStandardId { get; set; }
        public int? MaintClassId { get; set; }
        public int? KeyDocId { get; set; }
        public int? PoId { get; set; }
        public string TagSource { get; set; }
        public bool TagDeleted { get; set; }

        [Display(Name = "FLOC Description")]
        public string TagFlocDesc { get; set; }

        [Display(Name = "Open Query")]
        public bool TagMaintQuery { get; set; }
        public string TagComment { get; set; }
        public int? ModelId { get; set; }
        public int? VibId { get; set; }
        public bool Tagnoneng { get; set; }
        public string TagVendorTag { get; set; }

        [Display(Name = "Maint. Obj Type")]
        public int? MaintObjectTypeId { get; set; }
        public int? RbiSilId { get; set; }
        public int? IpfId { get; set; }
        public int? RcmId { get; set; }
        public string TagRawNumber { get; set; }
        public string TagRawDesc { get; set; }
        public int? MaintScePsReviewTeamId { get; set; }
        public string MaintScePsJustification { get; set; }
        public string TagMaintCritComments { get; set; }
        public int? RbmId { get; set; }
        public int? ManufacturerId { get; set; }
        public int? ExMethodId { get; set; }
        public string TagRbmMethod { get; set; }
        public string TagVib { get; set; }
        public string TagSrcKeyList { get; set; }
        public string TagBomReq { get; set; }
        public string TagSpNo { get; set; }
        public int? MaintSortProcessId { get; set; }
        public string TagCharacteristic { get; set; }
        public string TagCharValue { get; set; }
        public string TagCharDesc { get; set; }

        public virtual CommZone ComZone { get; set; }
        public virtual CommClass CommClass { get; set; }
        public virtual CommSubSystem CommissioningSubsystem { get; set; }
        public virtual MaintEdcCode Edc { get; set; }
        public virtual EngClass EngClass { get; set; }
        public virtual EngDisc EngDisc { get; set; }

        [ForeignKey("EngParentID")]
        public virtual Tag EngParent { get; set; }

        public virtual EngStatus EngStatus { get; set; }
        public virtual ExMethod ExMethod { get; set; }
        public virtual Ipf Ipf { get; set; }
        public virtual Doc KeyDoc { get; set; }
        public virtual Location Location { get; set; }
        public virtual MaintClass MaintClass { get; set; }
        public virtual MaintCriticality MaintCriticality { get; set; }
        public virtual MaintLocation MaintLocation { get; set; }
        public virtual MaintObjectType MaintObjectType { get; set; }
        public virtual Tag MaintParent { get; set; }
        public virtual MaintPlannerGroup MaintPlannerGroup { get; set; }
        public virtual MaintScePsReviewTeam MaintScePsReviewTeam { get; set; }
        public virtual MaintSortProcess MaintSortProcess { get; set; }
        public virtual MaintStructureIndicator MaintStructureIndicator { get; set; }
        public virtual MaintType MaintType { get; set; }
        public virtual MaintWorkCentre MaintWorkCentre { get; set; }
        public virtual MaintPlan Maintenanceplan { get; set; }
        public virtual Manufacturer Manufacturer { get; set; }
        public virtual Model Model { get; set; }
        public virtual PerformanceStandard PerformanceStandard { get; set; }
        public virtual RbiSil RbiSil { get; set; }
        public virtual Rbm Rbm { get; set; }
        public virtual Rcm Rcm { get; set; }
        public virtual SubSystem SubSystem { get; set; }
        public virtual Vib Vib { get; set; }
            
    }
}