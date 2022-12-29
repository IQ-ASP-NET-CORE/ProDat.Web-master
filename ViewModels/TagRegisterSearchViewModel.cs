using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ProDat.Web2.Models;

namespace ProDat.Web2.ViewModels
{
    public class TagRegisterSearchViewModel
    {
        //PK
        public int? TagId { get; set; }

        //Application logic
        public bool? Posted { get; set; }

        public int? TagViewId { get; set; }

        public bool? ExportAllColumns { get; set; }

        public int? PageRecordsId { get; set; }

        // Tag Register text values
        public string TagNumber { get; set; }

        public string TagService { get; set; }

        public string TagFloc { get; set; }

        public string TagFlocDesc { get; set; }

        public string TagSource { get; set; }

        public string TagComment { get; set; }

        public string TagVendorTag { get; set; }

        public string TagRawNumber { get; set; }

        public string TagRawDesc { get; set; }

        public string MaintScePsJustification { get; set; }

        public string TagMaintCritComments { get; set; }

        public string TagRbmMethod { get; set; }

        public string TagVib { get; set; }

        public string TagSrcKeyList { get; set; }

        public string TagBomReq { get; set; }

        public string TagSpNo { get; set; }

        public string TagCharacteristic { get; set; }

        public string TagCharValue { get; set; }

        public string TagCharDesc { get; set; }

        // Bit search values
        public bool? TagDeleted { get; set; }

        public bool? TagMaintQuery { get; set; }

        public bool? Tagnoneng { get; set; }

        // Star search values
        public string MaintSortProcessId { get; set; }

        public string SubSystemId { get; set; }

        public string SystemId { get; set; }

        public string EngClassId { get; set; }

        public string EngParentId { get; set; }

        public string MaintParentId { get; set; }

        public string EngDiscId { get; set; }

        public string MaintLocationId { get; set; }

        public string LocationId { get; set; }

        public string MaintTypeId { get; set; }

        public string MaintStatusId { get; set; }

        public string EngStatusId { get; set; }

        public string MaintWorkCentreId { get; set; }

        public string MaintEdcCodeId { get; set; }

        public string MaintStructureIndicatorId { get; set; }

        public string CommissioningSubsystemId { get; set; }

        public string CommClassId { get; set; }

        public string CommZoneId { get; set; }

        public string MaintPlannerGroupId { get; set; }

        public string MaintenanceplanId { get; set; }

        public string MaintCriticalityId { get; set; }

        public string PerformanceStandardId { get; set; }

        public string MaintClassId { get; set; }

        public string KeyDocId { get; set; }

        public string PoId { get; set; }        

        public string ModelId { get; set; }

        public string VibId { get; set; }
        
        public string MaintObjectTypeId { get; set; }

        public string RbiSilId { get; set; }

        public string IpfId { get; set; }

        public string RcmId { get; set; }

        public string MaintScePsReviewTeamId { get; set; }

        public string RbmId { get; set; }

        public string ManufacturerId { get; set; }

        public string ExMethodId { get; set; }

    }
}
