using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ProDat.Web2.Models
{
    public class Tag
    {
        public Tag()
        {
            // might bring this back to life, useful if lots of data and only one item is required (hash lookups).
            //FlocXmaintItems = new HashSet<FlocXmaintItem>();
            //FlocXmaintLoads = new HashSet<FlocXmaintLoad>();
            //FlocXmaintQuerys = new HashSet<FlocXmaintQuery>();
            //FlocXpmassemblys = new HashSet<FlocXpmassembly>();
            //InverseEngParents = new HashSet<Tag>();
            //InverseMaintParents = new HashSet<Tag>();
            //TagEngDatas = new HashSet<TagEngData>();
            //TagPos = new HashSet<TagPo>();
            //TagXdocs = new HashSet<TagXdoc>();
        }


        // Tag Attributes. 

        public int TagId { get; set; }
		
		[Required]
        [Display(Name ="Tag Number")]
        public string TagNumber { get; set; }

        [Display(Name = "Tag Service")]
        public string TagService { get; set; }
		
		[Display(Name ="FLOC"), MaxLength(100, ErrorMessage ="Max Length is 100")]
        public string TagFloc { get; set; }
        
		[Display(Name = "Sub Sys.")]
		public int? SubSystemId { get; set; }
        
        [Display(Name = "Eng Class")]
        public int? EngClassId { get; set; }
        
		[Display(Name = "Eng. Parent")]
		public int? EngParentId { get; set; }

        [Display(Name = "Maint Parent")]
        public int? MaintParentId { get; set; }
        
		[Display(Name = "Disc")]
		public int? EngDiscId { get; set; }
        
		[Display(Name = "Maint. Loc.")]
		public int? MaintLocationId { get; set; }

        [Display(Name = "Loc.")]
        public int? LocationID { get; set; }

        [Display(Name = "Maint Type")]
        public int? MaintTypeId { get; set; }

        [Display(Name = "Maint Status")]
        public int? MaintStatusId { get; set; }

        [Display(Name = "Eng Status")]
        public int? EngStatusId { get; set; }

        [Display(Name ="Work Centre")]
        public int? MaintWorkCentreId { get; set; }

        [Display(Name = "Edc")]
        public int? MaintEdcCodeId { get; set; }
        
        [Display(Name = "struct. Indicator")] 
        public int? MaintStructureIndicatorId { get; set; }
        
        [Display(Name = "Comm. SubSys")]
        public int? CommissioningSubsystemId { get; set; }
        
        [Display(Name = "Comm. Class")]
        public int? CommClassId { get; set; }
        
        [Display(Name = "Comm. Zone")]
        public int? CommZoneId { get; set; }
        
        [Display(Name = "Plan Group")]
        public int? MaintPlannerGroupId { get; set; }
        
        [Display(Name = "Plan")]
        public int? MaintenanceplanId { get; set; }
        
		[Display(Name = "Crit.")]
		public int? MaintCriticalityId { get; set; }
        
        [Display(Name = "Perf Std")]
        public int? PerformanceStandardId { get; set; }
        
        //[Display(Name = "Maint. Class")]
        //public int? MaintClassId { get; set; }
        
        [Display(Name = "Key Doc")]
        public int? KeyDocId { get; set; }

        [Display(Name = "PO")]
        public int? PoId { get; set; }
        
        [Display(Name = "Source")]
        public string TagSource { get; set; }
        
        public bool TagDeleted { get; set; }

        [Display(Name = "Run to Fail")]
        public bool RTF { get; set; }

        [Display(Name = "FLOC Description"), MaxLength(100, ErrorMessage = "Max Length is 100")]
        public string TagFlocDesc { get; set; }
        
        public int? FlocTypeId { get; set; }

		[Display(Name = "Open Query")]
		public bool TagMaintQuery { get; set; }
        
        [Display(Name = "Comment")]
        public string TagComment { get; set; }

        [Display(Name = "Maintenance Plant")]
        public int? MaintenancePlantId { get; set; }

        [Display(Name = "Model")]
        public int? ModelId { get; set; }

        [Display(Name = "Model Description")]
        public string ModelDescription { get; set; }


        [Display(Name = "Vib")]
        public int? VibId { get; set; }
        
        [Display(Name = "Non Eng")]
        public bool Tagnoneng { get; set; }
        
        [Display(Name = "Vendor Number")]
        public string TagVendorTag { get; set; }
		
		[Display(Name = "Type")]
        public int? MaintObjectTypeId { get; set; }

        [Display(Name = "RbiSilId")]
        public int? RbiSilId { get; set; }

        [Display(Name = "IpfId")]
        public int? IpfId { get; set; }

        [Display(Name = "RcmId")]
        public int? RcmId { get; set; }
        
        [Display(Name = "Raw Number")]
        public string TagRawNumber { get; set; }
        
        [Display(Name = "Raw Desc")]
        public string TagRawDesc { get; set; }
        
        [Display(Name = "PS Review Team")]
        public int? MaintScePsReviewTeamId { get; set; }
        
        [Display(Name = "Sce PS Justification")]
        public string MaintScePsJustification { get; set; }

        [Display(Name = "Crit Comment")]
        public string TagMaintCritComments { get; set; }

        [Display(Name = "RbmId")]
        public int? RbmId { get; set; }
        
        [Display(Name = "Manufacturer")]
        public int? ManufacturerId { get; set; }
        
        [Display(Name = "Ex Method")]
        public int? ExMethodId { get; set; }

        [Display(Name = "TagRbmMethod")]
        public string TagRbmMethod { get; set; }

        [Display(Name = "TgaVib")]
        public string TagVib { get; set; }

        [Display(Name = "TagSrcKeyList")]
        public string TagSrcKeyList { get; set; }

        [Display(Name = "TagBomReq")]
        public string TagBomReq { get; set; }

        [Display(Name = "TagSpNo")]
        public string TagSpNo { get; set; }

        [Display(Name = "MaintSortProcessId")]
        public int? MaintSortProcessId { get; set; }

        [Display(Name = "TagCharacteristic")]
        public string TagCharacteristic { get; set; }

        [Display(Name = "TagCharValue")]
        public string TagCharValue { get; set; }

        [Display(Name = "TagCharDesc")]
        public string TagCharDesc { get; set; }

        [Display(Name = "SAP Status")]
        public int? SAPStatusId { get; set; }

        [Display(Name = "Serial Number")]
        public string SerialNumber { get; set; }

        [Display(Name = "MEX Equipment List")]
        public string MEXEquipList { get; set; }

        [Display(Name = "MEX Parent Equipment")]
        public string MEXParentEquip { get; set; }

        [Display(Name = "Sup Funct Location")]
        public string SupFunctLoc { get; set; }

        [Display(Name = "Sort Field Id")]
        public int? SortFieldId { get; set; }

        [Display(Name = "Planner Plant Id")]
        public int? PlannerPlantdId { get; set; }

        [Display(Name = "Company Code Id")]
        public int? ComnpanyCodeId { get; set; }

        [Display(Name = "WBS Element Id")]
        public int? WBSElementId { get; set; }


        public int? PbsId { get; set; }

        public int? EnvZoneId { get; set; }

        // FK Entities

        public virtual EnvZone EnvZone { get; set; }

        public virtual Pbs Pbs { get; set; }
        
        public virtual CommZone CommZone { get; set; }
        
        public virtual CommClass CommClass { get; set; }

        public virtual MaintStatus MaintStatus { get; set; }

        public virtual CommSubSystem CommissioningSubsystem { get; set; }
        public virtual MaintEdcCode MaintEdcCode { get; set; }
        public virtual EngClass EngClass { get; set; }
        public virtual EngDisc EngDisc { get; set; }
        
		[ForeignKey("EngParentID")]
		public virtual Tag EngParent { get; set; }
		
        public virtual EngStatus EngStatus { get; set; }
        public virtual ExMethod ExMethod { get; set; }

        public virtual FlocType FlocType { get; set; }
        public virtual Ipf Ipf { get; set; }
        public virtual Doc KeyDoc { get; set; }
        public virtual Location Location { get; set; }
        
        //public virtual MaintClass MaintClass { get; set; }
        public virtual MaintCriticality MaintCriticality { get; set; }
        public virtual MaintenancePlant MaintenancePlant { get; set; }
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
        public virtual SAPStatus SAPStatus { get; set; }
        public virtual SortField SortField { get; set; }
        public virtual PlannerPlant PlannerPlant { get; set; }
        public virtual CompanyCode CompanyCode { get; set; }
        public virtual WBSElement WBSElement { get; set; }

        public virtual ICollection<FlocXmaintItem> FlocXmaintItems { get; set; }
        public virtual ICollection<FlocXmaintLoad> FlocXmaintLoads { get; set; }
        public virtual ICollection<FlocXmaintQuery> FlocXmaintQuerys { get; set; }
        public virtual ICollection<FlocXpmassembly> FlocXpmassemblys { get; set; }
        public virtual ICollection<Tag> InverseEngParents { get; set; }

        // JsonIgnore means discarded in devexpress calls, but still usable in controllers, where you can manage infinite loops.
        [Newtonsoft.Json.JsonIgnore]
        public virtual ICollection<Tag> InverseMaintParents { get; set; }
        public virtual ICollection<TagEngData> TagEngDatas { get; set; }
        public virtual ICollection<TagPo> TagPos { get; set; }
        public virtual ICollection<TagXdoc> TagXdocs { get; set; }
    }
}
