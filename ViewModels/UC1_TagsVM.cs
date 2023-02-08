using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ProDat.Web2.ViewModels
{
    public class UC1_TagsVM
    {
        public UC1_TagsVM(ProDat.Web2.Models.Tag rec )
        {
            TagId = rec.TagId;
            TagDeleted = rec.TagDeleted;
            TagMaintQuery = rec.TagMaintQuery;
            Tagnoneng = rec.Tagnoneng;
            CommClassId = rec.CommClassId;
            CommissioningSubsystemId = rec.CommissioningSubsystemId;
            CommZoneId = rec.CommZoneId;
            ComnpanyCodeId = rec.ComnpanyCodeId;
            EngClassId = rec.EngClassId;
            EngDiscId = rec.EngDiscId;
            EngParentId = rec.EngParentId;
            EngStatusId = rec.EngStatusId;
            EnvZoneId = rec.EnvZoneId;
            ExMethodId = rec.ExMethodId;
            FlocTypeId = rec.FlocTypeId;
            IpfId = rec.IpfId;
            KeyDocId = rec.KeyDocId;
            LocationId = rec.LocationID;
            //MaintClassId  = //rec.MaintClassId;
            MaintCriticalityId = rec.MaintCriticalityId;
            MaintEdcCodeId = rec.MaintEdcCodeId;
            MaintenanceplanId = rec.MaintenanceplanId;
            MaintenancePlantId = rec.MaintenancePlantId;
            MaintLocationId = rec.MaintLocationId;
            MaintObjectTypeId = rec.MaintObjectTypeId;
            MaintParentId = rec.MaintParentId;
            MaintPlannerGroupId = rec.MaintPlannerGroupId;
            MaintScePsJustification = rec.MaintScePsJustification;
            MaintScePsReviewTeamId = rec.MaintScePsReviewTeamId;
            MaintSortProcessId = rec.MaintSortProcessId;
            MaintStatusId = rec.MaintStatusId;
            MaintStructureIndicatorId = rec.MaintStructureIndicatorId;
            MaintTypeId = rec.MaintTypeId;
            MaintWorkCentreId = rec.MaintWorkCentreId;
            ManufacturerId = rec.ManufacturerId;
            MEXEquipList = rec.MEXEquipList;
            MEXParentEquip = rec.MEXParentEquip;
            ModelId = rec.ModelId;
            ModelDescription = rec.ModelDescription;
            PbsId = rec.PbsId;
            PerformanceStandardId = rec.PerformanceStandardId;
            PlannerPlantdId = rec.PlannerPlantdId;
            PoId = rec.PoId;
            RbiSilId = rec.RbiSilId;
            RbmId = rec.RbmId;
            RcmId = rec.RcmId;
            SAPStatusId = rec.SAPStatusId;
            SerialNumber = rec.SerialNumber;
            SortFieldId = rec.SortFieldId;
            SubSystemId = rec.SubSystemId;
            SupFunctLoc = rec.SupFunctLoc;
            TagBomReq = rec.TagBomReq;
            TagCharacteristic = rec.TagCharacteristic;
            TagCharDesc = rec.TagCharDesc;
            TagCharValue = rec.TagCharValue;
            TagComment = rec.TagComment;
            TagFloc = rec.TagFloc;
            TagFlocDesc = rec.TagFlocDesc;
            TagMaintCritComments = rec.TagMaintCritComments;
            TagNumber = rec.TagNumber;
            TagRawDesc = rec.TagRawDesc;
            TagRawNumber = rec.TagRawNumber;
            TagRbmMethod = rec.TagRbmMethod;
            TagService = rec.TagService;
            TagSource = rec.TagSource;
            TagSpNo = rec.TagSpNo;
            TagSrcKeyList = rec.TagSrcKeyList;
            TagVendorTag = rec.TagVendorTag;
            TagVib = rec.TagVib;
            VibId = rec.VibId;
            WBSElementId = rec.WBSElementId;
        }

        public int TagId { get; set; }

        [Display(Name = "RTF")]
        public bool RTF { get; set; }

        [Required]
        [Display(Name = "Tag Number")]
        public string TagNumber { get; set; }

        [Display(Name = "Tag Service")]
        public string TagService { get; set; }

        [Display(Name = "FLOC"), MaxLength(25, ErrorMessage = "Max Length is 25")]
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
        public int? LocationId { get; set; }

        [Display(Name = "Maint Type")]
        public int? MaintTypeId { get; set; }

        [Display(Name = "Maint Status")]
        public int? MaintStatusId { get; set; }

        [Display(Name = "Eng Status")]
        public int? EngStatusId { get; set; }

        [Display(Name = "Work Centre")]
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

        [Display(Name = "Maintenance Plant")]
        public int? MaintenancePlantId { get; set; }

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

        [Display(Name = "FLOC Description"), MaxLength(50, ErrorMessage = "Max Length is 50")]
        public string TagFlocDesc { get; set; }

        public int? FlocTypeId { get; set; }

        [Display(Name = "Open Query")]
        public bool TagMaintQuery { get; set; }

        [Display(Name = "Comment")]
        public string TagComment { get; set; }

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


    }
}
