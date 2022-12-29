using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using ProDat.Web2.Models;

namespace ProDat.Web2.ViewModels
{
    public class TagPropertiesStandardViewModel
    {
        public class SelectBoxViewModel
        {
            public IEnumerable<string> Items { get; set; }
        }

        public virtual Tag Tag {get; set;}

        #region old code commented out

        //public int TagId { get; set; }

        //[Display(Name = "Tag Number")]
        //public string TagNumber { get; set; }

        //[Display(Name = "FLOC"), MaxLength(25, ErrorMessage = "Max Length is 25")]
        //public string TagFloc { get; set; }

        //[Display(Name = "FLOC Description"), MaxLength(50, ErrorMessage = "Max Length is 50")]
        //public string TagFlocDesc { get; set; }

        //public string TagService { get; set; }

        //// Whats this?
        //public string SAPDescription { get; set; }

        //// whats this?
        //public string Section { get; set; }

        //[Display(Name = "Location")]
        //public int? LocationId { get; set; }

        //[Display(Name = "Comm Zone")]
        //public int? CommZoneId { get; set; }

        //[Display(Name = "Parent")]
        //public int? MaintParentId { get; set; }

        //public int? EngStatusId { get; set; }

        //[Display(Name = "Maint Status")]
        //public int? MaintStatusId { get; set; }

        //public int? MaintTypeId { get; set; }

        //[Display(Name = "SAP Status")]
        //public int? SAPStatusId { get; set; }

        //[Display(Name = "Manufacturer")]
        //public int? ManufacturerId { get; set; }

        //[Display(Name = "Model")]
        //public int? ModelId { get; set; }

        //[Display(Name = "Serial Number")]
        //public string SerialNumber { get; set; }

        //public Boolean TagDeleted { get; set; }

        //public Boolean TagMaintQuery { get; set; }

        //public string SAPStatus { get; set; }

        //public string TagComment { get; set; }

        //[Display(Name = "Maint. Plant")]
        //public string MaintenancePlant { get; set; }

        //[Display(Name = "Maint. Area")]
        //public string MaintenanceArea { get; set; }

        //[Display(Name = "Sup. Function Location")]
        //public string SuperiorFunctionLocationId { get; set; }

        //[Display(Name = "MEX Equip. List")]
        //public string MEXEquipList { get; set; }

        //[Display(Name = "MEX Parent Equip.")]
        //public string MEXParentEquip { get; set; }

        //[Display(Name = "Maint. Location")]
        //public int? MaintLocationId { get; set; }

        //[Display(Name = "Maint. Criticality Ind.")]
        //public int? MaintCriticalityId { get; set; }

        //[Display(Name = "Maint. Structure Ind.")]
        //public int? MaintStructureIndicatorId { get; set; }

        //[Display(Name = "Maint. Work Centre")]
        //public int? MaintWorkCentreId { get; set; }

        //[Display(Name = "Maint. Planner Group")]
        //public int? MaintPlannerGroupId { get; set; }

        //[Display(Name = "WBS Element")]
        //public int? WBSElementId { get; set; }

        //[Display(Name = "Company Code")]
        //public int? CompanyCodeId { get; set; }

        //[Display(Name = "Planner Plant")]
        //public int? PlannerPlantId { get; set; }

        //[Display(Name = "Sort Field")]
        //public int? SortFieldId { get; set; }

        #endregion

        [Display(Name ="Maintenance Area")]
        public int? MaintAreaId { get; set; }

        [Display(Name ="Plant Section")]
        public int? PlantSectionId { get; set; }

        [Display(Name = "Maint Parent Floc")]
        public string MaintParent_TagFloc { get; set; }

        public string bgcolour { get; set; }

        public int SAPId { get; set; }

        public int width { get; set; }

        public int height { get; set; }

    }
}

