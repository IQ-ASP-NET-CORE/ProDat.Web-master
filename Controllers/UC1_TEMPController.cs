using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Mvc;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Newtonsoft.Json;
using ProDat.Web2.Data;
using ProDat.Web2.Models;
using ProDat.Web2.ViewModels;
using Microsoft.EntityFrameworkCore;


namespace ProDat.Web2.Controllers
{
    //[Route("api/[controller]/[action]")]
    public class UC1_TEMPController : Controller
    {
        private TagContext _context;

        public UC1_TEMPController(TagContext context) {
            _context = context;
        }


        [HttpGet]
        public async Task<IActionResult> Get(DataSourceLoadOptions loadOptions) {
            var tag = _context.Tag.Select(i => new {
                i.TagId,
                i.TagNumber,
                i.TagService,
                i.TagFloc,
                i.SubSystemId,
                i.EngClassId,
                i.EngParentId,
                i.MaintParentId,
                i.EngDiscId,
                i.MaintLocationId,
                i.LocationID,
                i.MaintTypeId,
                i.MaintStatusId,
                i.EngStatusId,
                i.MaintWorkCentreId,
                i.MaintEdcCodeId,
                i.MaintStructureIndicatorId,
                i.CommissioningSubsystemId,
                i.CommClassId,
                i.CommZoneId,
                i.MaintPlannerGroupId,
                i.MaintenanceplanId,
                i.MaintCriticalityId,
                i.PerformanceStandardId,
                i.KeyDocId,
                i.PoId,
                i.TagSource,
                i.TagDeleted,
                i.RTF,
                i.TagFlocDesc,
                i.FlocTypeId,
                i.TagMaintQuery,
                i.TagComment,
                i.ModelId,
                i.VibId,
                i.Tagnoneng,
                i.TagVendorTag,
                i.MaintObjectTypeId,
                i.RbiSilId,
                i.IpfId,
                i.RcmId,
                i.TagRawNumber,
                i.TagRawDesc,
                i.MaintScePsReviewTeamId,
                i.MaintScePsJustification,
                i.TagMaintCritComments,
                i.RbmId,
                i.ManufacturerId,
                i.ExMethodId,
                i.TagRbmMethod,
                i.TagVib,
                i.TagSrcKeyList,
                i.TagBomReq,
                i.TagSpNo,
                i.MaintSortProcessId,
                i.TagCharacteristic,
                i.TagCharValue,
                i.TagCharDesc,
                i.SAPStatusId,
                i.SerialNumber,
                i.MEXEquipList,
                i.MEXParentEquip,
                i.SupFunctLoc,
                i.SortFieldId,
                i.PlannerPlantdId,
                i.ComnpanyCodeId,
                i.WBSElementId,
                i.PbsId,
                i.EnvZoneId
            });

            // If you work with a large amount of data, consider specifying the PaginateViaPrimaryKey and PrimaryKey properties.
            // In this case, keys and data are loaded in separate queries. This can make the SQL execution plan more efficient.
            // Refer to the topic https://github.com/DevExpress/DevExtreme.AspNet.Data/issues/336.
            // loadOptions.PrimaryKey = new[] { "TagId" };
            // loadOptions.PaginateViaPrimaryKey = true;

            return Json(await DataSourceLoader.LoadAsync(tag, loadOptions));
        }

        public IActionResult Create()
        {
            //UC1_TagsVM x=null;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Tag tag) {

            //var result = _context.Tag.Add(model);
            //await _context.SaveChangesAsync();

            //return Json(new { result.Entity.TagId });
            if (ModelState.IsValid)
            {
                var result = _context.Tag.Add(tag);
                _context.SaveChangesAsync();
                return View("Index");
            }

            return View(tag);

        }

        [HttpPut]
        public async Task<IActionResult> Put(int key, string values) {
            var model = await _context.Tag.FirstOrDefaultAsync(item => item.TagId == key);
            if(model == null)
                return StatusCode(409, "Object not found");

            var valuesDict = JsonConvert.DeserializeObject<IDictionary>(values);
            PopulateModel(model, valuesDict);

            if(!TryValidateModel(model))
                return BadRequest(GetFullErrorMessage(ModelState));

            await _context.SaveChangesAsync();
            return Ok();
        }

        [HttpDelete]
        public async Task Delete(int key) {
            var model = await _context.Tag.FirstOrDefaultAsync(item => item.TagId == key);

            _context.Tag.Remove(model);
            await _context.SaveChangesAsync();
        }

        #region Lookup Functions

        [HttpGet]
        public async Task<IActionResult> EnvZoneLookup(DataSourceLoadOptions loadOptions) {
            var lookup = from i in _context.EnvZone
                         orderby i.EnvZoneName
                         select new {
                             Value = i.EnvZoneId,
                             Text = i.EnvZoneName
                         };
            return Json(await DataSourceLoader.LoadAsync(lookup, loadOptions));
        }

        [HttpGet]
        public async Task<IActionResult> PbsLookup(DataSourceLoadOptions loadOptions) {
            var lookup = from i in _context.Pbs
                         orderby i.PbsName
                         select new {
                             Value = i.PbsId,
                             Text = i.PbsName
                         };
            return Json(await DataSourceLoader.LoadAsync(lookup, loadOptions));
        }

        [HttpGet]
        public async Task<IActionResult> CommZoneLookup(DataSourceLoadOptions loadOptions) {
            var lookup = from i in _context.CommZone
                         orderby i.CommZoneName
                         select new {
                             Value = i.CommZoneId,
                             Text = i.CommZoneName
                         };
            return Json(await DataSourceLoader.LoadAsync(lookup, loadOptions));
        }

        [HttpGet]
        public async Task<IActionResult> CommClassLookup(DataSourceLoadOptions loadOptions) {
            var lookup = from i in _context.CommClass
                         orderby i.CommClassName
                         select new {
                             Value = i.CommClassId,
                             Text = i.CommClassName
                         };
            return Json(await DataSourceLoader.LoadAsync(lookup, loadOptions));
        }

        [HttpGet]
        public async Task<IActionResult> MaintStatusLookup(DataSourceLoadOptions loadOptions) {
            var lookup = from i in _context.MaintStatus
                         orderby i.MaintStatusName
                         select new {
                             Value = i.MaintStatusId,
                             Text = i.MaintStatusName
                         };
            return Json(await DataSourceLoader.LoadAsync(lookup, loadOptions));
        }

        [HttpGet]
        public async Task<IActionResult> CommSubSystemLookup(DataSourceLoadOptions loadOptions) {
            var lookup = from i in _context.CommSubSystem
                         orderby i.CommSubSystemNo
                         select new {
                             Value = i.CommSubSystemId,
                             Text = i.CommSubSystemNo
                         };
            return Json(await DataSourceLoader.LoadAsync(lookup, loadOptions));
        }

        [HttpGet]
        public async Task<IActionResult> MaintEdcCodeLookup(DataSourceLoadOptions loadOptions) {
            var lookup = from i in _context.MaintEdcCode
                         orderby i.MaintEdcCodeName
                         select new {
                             Value = i.MaintEdcCodeId,
                             Text = i.MaintEdcCodeName
                         };
            return Json(await DataSourceLoader.LoadAsync(lookup, loadOptions));
        }

        [HttpGet]
        public async Task<IActionResult> EngClassLookup(DataSourceLoadOptions loadOptions) {
            var lookup = from i in _context.EngClass
                         orderby i.EngClassName
                         select new {
                             Value = i.EngClassId,
                             Text = i.EngClassName
                         };
            return Json(await DataSourceLoader.LoadAsync(lookup, loadOptions));
        }

        [HttpGet]
        public async Task<IActionResult> EngDiscLookup(DataSourceLoadOptions loadOptions) {
            var lookup = from i in _context.EngDisc
                         orderby i.EngDiscName
                         select new {
                             Value = i.EngDiscId,
                             Text = i.EngDiscName
                         };
            return Json(await DataSourceLoader.LoadAsync(lookup, loadOptions));
        }

        [HttpGet]
        public async Task<IActionResult> TagLookup(DataSourceLoadOptions loadOptions) {
            var lookup = from i in _context.Tag
                         orderby i.TagNumber
                         select new {
                             Value = i.TagId,
                             Text = i.TagNumber
                         };
            return Json(await DataSourceLoader.LoadAsync(lookup, loadOptions));
        }

        [HttpGet]
        public async Task<IActionResult> EngStatusLookup(DataSourceLoadOptions loadOptions) {
            var lookup = from i in _context.EngStatus
                         orderby i.EngStatusName
                         select new {
                             Value = i.EngStatusId,
                             Text = i.EngStatusName
                         };
            return Json(await DataSourceLoader.LoadAsync(lookup, loadOptions));
        }

        [HttpGet]
        public async Task<IActionResult> ExMethodLookup(DataSourceLoadOptions loadOptions) {
            var lookup = from i in _context.ExMethod
                         orderby i.ExMethodName
                         select new {
                             Value = i.ExMethodId,
                             Text = i.ExMethodName
                         };
            return Json(await DataSourceLoader.LoadAsync(lookup, loadOptions));
        }

        [HttpGet]
        public async Task<IActionResult> IpfLookup(DataSourceLoadOptions loadOptions) {
            var lookup = from i in _context.Ipf
                         orderby i.IpfName
                         select new {
                             Value = i.IpfId,
                             Text = i.IpfName
                         };
            return Json(await DataSourceLoader.LoadAsync(lookup, loadOptions));
        }

        [HttpGet]
        public async Task<IActionResult> DocLookup(DataSourceLoadOptions loadOptions) {
            var lookup = from i in _context.Doc
                         orderby i.DocNum
                         select new {
                             Value = i.DocId,
                             Text = i.DocNum
                         };
            return Json(await DataSourceLoader.LoadAsync(lookup, loadOptions));
        }

        [HttpGet]
        public async Task<IActionResult> LocationLookup(DataSourceLoadOptions loadOptions) {
            var lookup = from i in _context.Location
                         orderby i.LocationName
                         select new {
                             Value = i.LocationID,
                             Text = i.LocationName
                         };
            return Json(await DataSourceLoader.LoadAsync(lookup, loadOptions));
        }

        [HttpGet]
        public async Task<IActionResult> MaintCriticalityLookup(DataSourceLoadOptions loadOptions) {
            var lookup = from i in _context.MaintCriticality
                         orderby i.MaintCriticalityName
                         select new {
                             Value = i.MaintCriticalityId,
                             Text = i.MaintCriticalityName
                         };
            return Json(await DataSourceLoader.LoadAsync(lookup, loadOptions));
        }

        [HttpGet]
        public async Task<IActionResult> MaintLocationLookup(DataSourceLoadOptions loadOptions) {
            var lookup = from i in _context.MaintLocation
                         orderby i.MaintLocationName
                         select new {
                             Value = i.MaintLocationId,
                             Text = i.MaintLocationName
                         };
            return Json(await DataSourceLoader.LoadAsync(lookup, loadOptions));
        }

        [HttpGet]
        public async Task<IActionResult> MaintObjectTypeLookup(DataSourceLoadOptions loadOptions) {
            var lookup = from i in _context.MaintObjectType
                         orderby i.MaintObjectTypeName
                         select new {
                             Value = i.MaintObjectTypeId,
                             Text = i.MaintObjectTypeName
                         };
            return Json(await DataSourceLoader.LoadAsync(lookup, loadOptions));
        }

        [HttpGet]
        public async Task<IActionResult> MaintPlannerGroupLookup(DataSourceLoadOptions loadOptions) {
            var lookup = from i in _context.MaintPlannerGroup
                         orderby i.MaintPlannerGroupName
                         select new {
                             Value = i.MaintPlannerGroupId,
                             Text = i.MaintPlannerGroupName
                         };
            return Json(await DataSourceLoader.LoadAsync(lookup, loadOptions));
        }

        [HttpGet]
        public async Task<IActionResult> MaintScePsReviewTeamLookup(DataSourceLoadOptions loadOptions) {
            var lookup = from i in _context.MaintScePsReviewTeam
                         orderby i.MaintScePsReviewTeamName
                         select new {
                             Value = i.MaintScePsReviewTeamId,
                             Text = i.MaintScePsReviewTeamName
                         };
            return Json(await DataSourceLoader.LoadAsync(lookup, loadOptions));
        }

        [HttpGet]
        public async Task<IActionResult> MaintSortProcessLookup(DataSourceLoadOptions loadOptions) {
            var lookup = from i in _context.MaintSortProcess
                         orderby i.MaintSortProcessName
                         select new {
                             Value = i.MaintSortProcessId,
                             Text = i.MaintSortProcessName
                         };
            return Json(await DataSourceLoader.LoadAsync(lookup, loadOptions));
        }

        [HttpGet]
        public async Task<IActionResult> MaintStructureIndicatorLookup(DataSourceLoadOptions loadOptions) {
            var lookup = from i in _context.MaintStructureIndicator
                         orderby i.MaintStructureIndicatorName
                         select new {
                             Value = i.MaintStructureIndicatorId,
                             Text = i.MaintStructureIndicatorName
                         };
            return Json(await DataSourceLoader.LoadAsync(lookup, loadOptions));
        }

        [HttpGet]
        public async Task<IActionResult> MaintTypeLookup(DataSourceLoadOptions loadOptions) {
            var lookup = from i in _context.MaintType
                         orderby i.MaintTypeName
                         select new {
                             Value = i.MaintTypeId,
                             Text = i.MaintTypeName
                         };
            return Json(await DataSourceLoader.LoadAsync(lookup, loadOptions));
        }

        [HttpGet]
        public async Task<IActionResult> MaintWorkCentreLookup(DataSourceLoadOptions loadOptions) {
            var lookup = from i in _context.MaintWorkCentre
                         orderby i.MaintWorkCentreName
                         select new {
                             Value = i.MaintWorkCentreId,
                             Text = i.MaintWorkCentreName
                         };
            return Json(await DataSourceLoader.LoadAsync(lookup, loadOptions));
        }

        [HttpGet]
        public async Task<IActionResult> MaintPlanLookup(DataSourceLoadOptions loadOptions) {
            var lookup = from i in _context.MaintPlan
                         orderby i.MaintPlanName
                         select new {
                             Value = i.MaintPlanId,
                             Text = i.MaintPlanName
                         };
            return Json(await DataSourceLoader.LoadAsync(lookup, loadOptions));
        }

        [HttpGet]
        public async Task<IActionResult> ManufacturerLookup(DataSourceLoadOptions loadOptions) {
            var lookup = from i in _context.Manufacturer
                         orderby i.ManufacturerName
                         select new {
                             Value = i.ManufacturerId,
                             Text = i.ManufacturerName
                         };
            return Json(await DataSourceLoader.LoadAsync(lookup, loadOptions));
        }

        [HttpGet]
        public async Task<IActionResult> ModelsLookup(DataSourceLoadOptions loadOptions) {
            var lookup = from i in _context.Models
                         orderby i.ModelName
                         select new {
                             Value = i.ModelId,
                             Text = i.ModelName
                         };
            return Json(await DataSourceLoader.LoadAsync(lookup, loadOptions));
        }

        [HttpGet]
        public async Task<IActionResult> PerformanceStandardLookup(DataSourceLoadOptions loadOptions) {
            var lookup = from i in _context.PerformanceStandard
                         orderby i.PerformanceStandardName
                         select new {
                             Value = i.PerformanceStandardId,
                             Text = i.PerformanceStandardName
                         };
            return Json(await DataSourceLoader.LoadAsync(lookup, loadOptions));
        }

        [HttpGet]
        public async Task<IActionResult> RbiSilLookup(DataSourceLoadOptions loadOptions) {
            var lookup = from i in _context.RbiSil
                         orderby i.RbiSilName
                         select new {
                             Value = i.RbiSilId,
                             Text = i.RbiSilName
                         };
            return Json(await DataSourceLoader.LoadAsync(lookup, loadOptions));
        }

        [HttpGet]
        public async Task<IActionResult> RbmLookup(DataSourceLoadOptions loadOptions) {
            var lookup = from i in _context.Rbm
                         orderby i.RbmName
                         select new {
                             Value = i.RbmId,
                             Text = i.RbmName
                         };
            return Json(await DataSourceLoader.LoadAsync(lookup, loadOptions));
        }

        [HttpGet]
        public async Task<IActionResult> RcmLookup(DataSourceLoadOptions loadOptions) {
            var lookup = from i in _context.Rcm
                         orderby i.RcmName
                         select new {
                             Value = i.RcmId,
                             Text = i.RcmName
                         };
            return Json(await DataSourceLoader.LoadAsync(lookup, loadOptions));
        }

        [HttpGet]
        public async Task<IActionResult> SubSystemLookup(DataSourceLoadOptions loadOptions) {
            var lookup = from i in _context.SubSystem
                         orderby i.SubSystemNum
                         select new {
                             Value = i.SubSystemId,
                             Text = i.SubSystemNum
                         };
            return Json(await DataSourceLoader.LoadAsync(lookup, loadOptions));
        }

        [HttpGet]
        public async Task<IActionResult> VibLookup(DataSourceLoadOptions loadOptions) {
            var lookup = from i in _context.Vib
                         orderby i.VibName
                         select new {
                             Value = i.VibId,
                             Text = i.VibName
                         };
            return Json(await DataSourceLoader.LoadAsync(lookup, loadOptions));
        }

        [HttpGet]
        public async Task<IActionResult> SAPStatusLookup(DataSourceLoadOptions loadOptions) {
            var lookup = from i in _context.SAPStatus
                         orderby i.Description
                         select new {
                             Value = i.SAPStatusId,
                             Text = i.Description
                         };
            return Json(await DataSourceLoader.LoadAsync(lookup, loadOptions));
        }

        [HttpGet]
        public async Task<IActionResult> SortFieldLookup(DataSourceLoadOptions loadOptions) {
            var lookup = from i in _context.SortField
                         orderby i.SortFieldName
                         select new {
                             Value = i.SortFieldId,
                             Text = i.SortFieldName
                         };
            return Json(await DataSourceLoader.LoadAsync(lookup, loadOptions));
        }

        [HttpGet]
        public async Task<IActionResult> PlannerPlantLookup(DataSourceLoadOptions loadOptions) {
            var lookup = from i in _context.PlannerPlant
                         orderby i.PlannerPlantName
                         select new {
                             Value = i.PlannerPlantId,
                             Text = i.PlannerPlantName
                         };
            return Json(await DataSourceLoader.LoadAsync(lookup, loadOptions));
        }

        [HttpGet]
        public async Task<IActionResult> CompanyCodeLookup(DataSourceLoadOptions loadOptions) {
            var lookup = from i in _context.CompanyCode
                         orderby i.CompanyCodeName
                         select new {
                             Value = i.CompanyCodeId,
                             Text = i.CompanyCodeName
                         };
            return Json(await DataSourceLoader.LoadAsync(lookup, loadOptions));
        }

        [HttpGet]
        public async Task<IActionResult> WBSElementLookup(DataSourceLoadOptions loadOptions) {
            var lookup = from i in _context.WBSElement
                         orderby i.WBSElementName
                         select new {
                             Value = i.WBSElementId,
                             Text = i.WBSElementName
                         };
            return Json(await DataSourceLoader.LoadAsync(lookup, loadOptions));
        }

        #endregion

        private void PopulateModel(Tag model, IDictionary values) {
            string TAG_ID = nameof(Tag.TagId);
            string TAG_NUMBER = nameof(Tag.TagNumber);
            string TAG_SERVICE = nameof(Tag.TagService);
            string TAG_FLOC = nameof(Tag.TagFloc);
            string SUB_SYSTEM_ID = nameof(Tag.SubSystemId);
            string ENG_CLASS_ID = nameof(Tag.EngClassId);
            string ENG_PARENT_ID = nameof(Tag.EngParentId);
            string MAINT_PARENT_ID = nameof(Tag.MaintParentId);
            string ENG_DISC_ID = nameof(Tag.EngDiscId);
            string MAINT_LOCATION_ID = nameof(Tag.MaintLocationId);
            string LOCATION_ID = nameof(Tag.LocationID);
            string MAINT_TYPE_ID = nameof(Tag.MaintTypeId);
            string MAINT_STATUS_ID = nameof(Tag.MaintStatusId);
            string ENG_STATUS_ID = nameof(Tag.EngStatusId);
            string MAINT_WORK_CENTRE_ID = nameof(Tag.MaintWorkCentreId);
            string MAINT_EDC_CODE_ID = nameof(Tag.MaintEdcCodeId);
            string MAINT_STRUCTURE_INDICATOR_ID = nameof(Tag.MaintStructureIndicatorId);
            string COMMISSIONING_SUBSYSTEM_ID = nameof(Tag.CommissioningSubsystemId);
            string COMM_CLASS_ID = nameof(Tag.CommClassId);
            string COMM_ZONE_ID = nameof(Tag.CommZoneId);
            string MAINT_PLANNER_GROUP_ID = nameof(Tag.MaintPlannerGroupId);
            string MAINTENANCEPLAN_ID = nameof(Tag.MaintenanceplanId);
            string MAINT_CRITICALITY_ID = nameof(Tag.MaintCriticalityId);
            string PERFORMANCE_STANDARD_ID = nameof(Tag.PerformanceStandardId);
            string KEY_DOC_ID = nameof(Tag.KeyDocId);
            string PO_ID = nameof(Tag.PoId);
            string TAG_SOURCE = nameof(Tag.TagSource);
            string TAG_DELETED = nameof(Tag.TagDeleted);
            string RTF = nameof(Tag.RTF);
            string TAG_FLOC_DESC = nameof(Tag.TagFlocDesc);
            string FLOC_TYPE_ID = nameof(Tag.FlocTypeId);
            string TAG_MAINT_QUERY = nameof(Tag.TagMaintQuery);
            string TAG_COMMENT = nameof(Tag.TagComment);
            string MODEL_ID = nameof(Tag.ModelId);
            string VIB_ID = nameof(Tag.VibId);
            string TAGNONENG = nameof(Tag.Tagnoneng);
            string TAG_VENDOR_TAG = nameof(Tag.TagVendorTag);
            string MAINT_OBJECT_TYPE_ID = nameof(Tag.MaintObjectTypeId);
            string RBI_SIL_ID = nameof(Tag.RbiSilId);
            string IPF_ID = nameof(Tag.IpfId);
            string RCM_ID = nameof(Tag.RcmId);
            string TAG_RAW_NUMBER = nameof(Tag.TagRawNumber);
            string TAG_RAW_DESC = nameof(Tag.TagRawDesc);
            string MAINT_SCE_PS_REVIEW_TEAM_ID = nameof(Tag.MaintScePsReviewTeamId);
            string MAINT_SCE_PS_JUSTIFICATION = nameof(Tag.MaintScePsJustification);
            string TAG_MAINT_CRIT_COMMENTS = nameof(Tag.TagMaintCritComments);
            string RBM_ID = nameof(Tag.RbmId);
            string MANUFACTURER_ID = nameof(Tag.ManufacturerId);
            string EX_METHOD_ID = nameof(Tag.ExMethodId);
            string TAG_RBM_METHOD = nameof(Tag.TagRbmMethod);
            string TAG_VIB = nameof(Tag.TagVib);
            string TAG_SRC_KEY_LIST = nameof(Tag.TagSrcKeyList);
            string TAG_BOM_REQ = nameof(Tag.TagBomReq);
            string TAG_SP_NO = nameof(Tag.TagSpNo);
            string MAINT_SORT_PROCESS_ID = nameof(Tag.MaintSortProcessId);
            string TAG_CHARACTERISTIC = nameof(Tag.TagCharacteristic);
            string TAG_CHAR_VALUE = nameof(Tag.TagCharValue);
            string TAG_CHAR_DESC = nameof(Tag.TagCharDesc);
            string SAPSTATUS_ID = nameof(Tag.SAPStatusId);
            string SERIAL_NUMBER = nameof(Tag.SerialNumber);
            string MEXEQUIP_LIST = nameof(Tag.MEXEquipList);
            string MEXPARENT_EQUIP = nameof(Tag.MEXParentEquip);
            string SUP_FUNCT_LOC = nameof(Tag.SupFunctLoc);
            string SORT_FIELD_ID = nameof(Tag.SortFieldId);
            string PLANNER_PLANTD_ID = nameof(Tag.PlannerPlantdId);
            string COMNPANY_CODE_ID = nameof(Tag.ComnpanyCodeId);
            string WBSELEMENT_ID = nameof(Tag.WBSElementId);
            string PBS_ID = nameof(Tag.PbsId);
            string ENV_ZONE_ID = nameof(Tag.EnvZoneId);

            if(values.Contains(TAG_ID)) {
                model.TagId = Convert.ToInt32(values[TAG_ID]);
            }

            if(values.Contains(TAG_NUMBER)) {
                model.TagNumber = Convert.ToString(values[TAG_NUMBER]);
            }

            if(values.Contains(TAG_SERVICE)) {
                model.TagService = Convert.ToString(values[TAG_SERVICE]);
            }

            if(values.Contains(TAG_FLOC)) {
                model.TagFloc = Convert.ToString(values[TAG_FLOC]);
            }

            if(values.Contains(SUB_SYSTEM_ID)) {
                model.SubSystemId = values[SUB_SYSTEM_ID] != null ? Convert.ToInt32(values[SUB_SYSTEM_ID]) : (int?)null;
            }

            if(values.Contains(ENG_CLASS_ID)) {
                model.EngClassId = values[ENG_CLASS_ID] != null ? Convert.ToInt32(values[ENG_CLASS_ID]) : (int?)null;
            }

            if(values.Contains(ENG_PARENT_ID)) {
                model.EngParentId = values[ENG_PARENT_ID] != null ? Convert.ToInt32(values[ENG_PARENT_ID]) : (int?)null;
            }

            if(values.Contains(MAINT_PARENT_ID)) {
                model.MaintParentId = values[MAINT_PARENT_ID] != null ? Convert.ToInt32(values[MAINT_PARENT_ID]) : (int?)null;
            }

            if(values.Contains(ENG_DISC_ID)) {
                model.EngDiscId = values[ENG_DISC_ID] != null ? Convert.ToInt32(values[ENG_DISC_ID]) : (int?)null;
            }

            if(values.Contains(MAINT_LOCATION_ID)) {
                model.MaintLocationId = values[MAINT_LOCATION_ID] != null ? Convert.ToInt32(values[MAINT_LOCATION_ID]) : (int?)null;
            }

            if(values.Contains(LOCATION_ID)) {
                model.LocationID = values[LOCATION_ID] != null ? Convert.ToInt32(values[LOCATION_ID]) : (int?)null;
            }

            if(values.Contains(MAINT_TYPE_ID)) {
                model.MaintTypeId = values[MAINT_TYPE_ID] != null ? Convert.ToInt32(values[MAINT_TYPE_ID]) : (int?)null;
            }

            if(values.Contains(MAINT_STATUS_ID)) {
                model.MaintStatusId = values[MAINT_STATUS_ID] != null ? Convert.ToInt32(values[MAINT_STATUS_ID]) : (int?)null;
            }

            if(values.Contains(ENG_STATUS_ID)) {
                model.EngStatusId = values[ENG_STATUS_ID] != null ? Convert.ToInt32(values[ENG_STATUS_ID]) : (int?)null;
            }

            if(values.Contains(MAINT_WORK_CENTRE_ID)) {
                model.MaintWorkCentreId = values[MAINT_WORK_CENTRE_ID] != null ? Convert.ToInt32(values[MAINT_WORK_CENTRE_ID]) : (int?)null;
            }

            if(values.Contains(MAINT_EDC_CODE_ID)) {
                model.MaintEdcCodeId = values[MAINT_EDC_CODE_ID] != null ? Convert.ToInt32(values[MAINT_EDC_CODE_ID]) : (int?)null;
            }

            if(values.Contains(MAINT_STRUCTURE_INDICATOR_ID)) {
                model.MaintStructureIndicatorId = values[MAINT_STRUCTURE_INDICATOR_ID] != null ? Convert.ToInt32(values[MAINT_STRUCTURE_INDICATOR_ID]) : (int?)null;
            }

            if(values.Contains(COMMISSIONING_SUBSYSTEM_ID)) {
                model.CommissioningSubsystemId = values[COMMISSIONING_SUBSYSTEM_ID] != null ? Convert.ToInt32(values[COMMISSIONING_SUBSYSTEM_ID]) : (int?)null;
            }

            if(values.Contains(COMM_CLASS_ID)) {
                model.CommClassId = values[COMM_CLASS_ID] != null ? Convert.ToInt32(values[COMM_CLASS_ID]) : (int?)null;
            }

            if(values.Contains(COMM_ZONE_ID)) {
                model.CommZoneId = values[COMM_ZONE_ID] != null ? Convert.ToInt32(values[COMM_ZONE_ID]) : (int?)null;
            }

            if(values.Contains(MAINT_PLANNER_GROUP_ID)) {
                model.MaintPlannerGroupId = values[MAINT_PLANNER_GROUP_ID] != null ? Convert.ToInt32(values[MAINT_PLANNER_GROUP_ID]) : (int?)null;
            }

            if(values.Contains(MAINTENANCEPLAN_ID)) {
                model.MaintenanceplanId = values[MAINTENANCEPLAN_ID] != null ? Convert.ToInt32(values[MAINTENANCEPLAN_ID]) : (int?)null;
            }

            if(values.Contains(MAINT_CRITICALITY_ID)) {
                model.MaintCriticalityId = values[MAINT_CRITICALITY_ID] != null ? Convert.ToInt32(values[MAINT_CRITICALITY_ID]) : (int?)null;
            }

            if(values.Contains(PERFORMANCE_STANDARD_ID)) {
                model.PerformanceStandardId = values[PERFORMANCE_STANDARD_ID] != null ? Convert.ToInt32(values[PERFORMANCE_STANDARD_ID]) : (int?)null;
            }

            if(values.Contains(KEY_DOC_ID)) {
                model.KeyDocId = values[KEY_DOC_ID] != null ? Convert.ToInt32(values[KEY_DOC_ID]) : (int?)null;
            }

            if(values.Contains(PO_ID)) {
                model.PoId = values[PO_ID] != null ? Convert.ToInt32(values[PO_ID]) : (int?)null;
            }

            if(values.Contains(TAG_SOURCE)) {
                model.TagSource = Convert.ToString(values[TAG_SOURCE]);
            }

            if(values.Contains(TAG_DELETED)) {
                model.TagDeleted = Convert.ToBoolean(values[TAG_DELETED]);
            }

            if(values.Contains(RTF)) {
                model.RTF = Convert.ToBoolean(values[RTF]);
            }

            if(values.Contains(TAG_FLOC_DESC)) {
                model.TagFlocDesc = Convert.ToString(values[TAG_FLOC_DESC]);
            }

            if(values.Contains(FLOC_TYPE_ID)) {
                model.FlocTypeId = values[FLOC_TYPE_ID] != null ? Convert.ToInt32(values[FLOC_TYPE_ID]) : (int?)null;
            }

            if(values.Contains(TAG_MAINT_QUERY)) {
                model.TagMaintQuery = Convert.ToBoolean(values[TAG_MAINT_QUERY]);
            }

            if(values.Contains(TAG_COMMENT)) {
                model.TagComment = Convert.ToString(values[TAG_COMMENT]);
            }

            if(values.Contains(MODEL_ID)) {
                model.ModelId = values[MODEL_ID] != null ? Convert.ToInt32(values[MODEL_ID]) : (int?)null;
            }

            if(values.Contains(VIB_ID)) {
                model.VibId = values[VIB_ID] != null ? Convert.ToInt32(values[VIB_ID]) : (int?)null;
            }

            if(values.Contains(TAGNONENG)) {
                model.Tagnoneng = Convert.ToBoolean(values[TAGNONENG]);
            }

            if(values.Contains(TAG_VENDOR_TAG)) {
                model.TagVendorTag = Convert.ToString(values[TAG_VENDOR_TAG]);
            }

            if(values.Contains(MAINT_OBJECT_TYPE_ID)) {
                model.MaintObjectTypeId = values[MAINT_OBJECT_TYPE_ID] != null ? Convert.ToInt32(values[MAINT_OBJECT_TYPE_ID]) : (int?)null;
            }

            if(values.Contains(RBI_SIL_ID)) {
                model.RbiSilId = values[RBI_SIL_ID] != null ? Convert.ToInt32(values[RBI_SIL_ID]) : (int?)null;
            }

            if(values.Contains(IPF_ID)) {
                model.IpfId = values[IPF_ID] != null ? Convert.ToInt32(values[IPF_ID]) : (int?)null;
            }

            if(values.Contains(RCM_ID)) {
                model.RcmId = values[RCM_ID] != null ? Convert.ToInt32(values[RCM_ID]) : (int?)null;
            }

            if(values.Contains(TAG_RAW_NUMBER)) {
                model.TagRawNumber = Convert.ToString(values[TAG_RAW_NUMBER]);
            }

            if(values.Contains(TAG_RAW_DESC)) {
                model.TagRawDesc = Convert.ToString(values[TAG_RAW_DESC]);
            }

            if(values.Contains(MAINT_SCE_PS_REVIEW_TEAM_ID)) {
                model.MaintScePsReviewTeamId = values[MAINT_SCE_PS_REVIEW_TEAM_ID] != null ? Convert.ToInt32(values[MAINT_SCE_PS_REVIEW_TEAM_ID]) : (int?)null;
            }

            if(values.Contains(MAINT_SCE_PS_JUSTIFICATION)) {
                model.MaintScePsJustification = Convert.ToString(values[MAINT_SCE_PS_JUSTIFICATION]);
            }

            if(values.Contains(TAG_MAINT_CRIT_COMMENTS)) {
                model.TagMaintCritComments = Convert.ToString(values[TAG_MAINT_CRIT_COMMENTS]);
            }

            if(values.Contains(RBM_ID)) {
                model.RbmId = values[RBM_ID] != null ? Convert.ToInt32(values[RBM_ID]) : (int?)null;
            }

            if(values.Contains(MANUFACTURER_ID)) {
                model.ManufacturerId = values[MANUFACTURER_ID] != null ? Convert.ToInt32(values[MANUFACTURER_ID]) : (int?)null;
            }

            if(values.Contains(EX_METHOD_ID)) {
                model.ExMethodId = values[EX_METHOD_ID] != null ? Convert.ToInt32(values[EX_METHOD_ID]) : (int?)null;
            }

            if(values.Contains(TAG_RBM_METHOD)) {
                model.TagRbmMethod = Convert.ToString(values[TAG_RBM_METHOD]);
            }

            if(values.Contains(TAG_VIB)) {
                model.TagVib = Convert.ToString(values[TAG_VIB]);
            }

            if(values.Contains(TAG_SRC_KEY_LIST)) {
                model.TagSrcKeyList = Convert.ToString(values[TAG_SRC_KEY_LIST]);
            }

            if(values.Contains(TAG_BOM_REQ)) {
                model.TagBomReq = Convert.ToString(values[TAG_BOM_REQ]);
            }

            if(values.Contains(TAG_SP_NO)) {
                model.TagSpNo = Convert.ToString(values[TAG_SP_NO]);
            }

            if(values.Contains(MAINT_SORT_PROCESS_ID)) {
                model.MaintSortProcessId = values[MAINT_SORT_PROCESS_ID] != null ? Convert.ToInt32(values[MAINT_SORT_PROCESS_ID]) : (int?)null;
            }

            if(values.Contains(TAG_CHARACTERISTIC)) {
                model.TagCharacteristic = Convert.ToString(values[TAG_CHARACTERISTIC]);
            }

            if(values.Contains(TAG_CHAR_VALUE)) {
                model.TagCharValue = Convert.ToString(values[TAG_CHAR_VALUE]);
            }

            if(values.Contains(TAG_CHAR_DESC)) {
                model.TagCharDesc = Convert.ToString(values[TAG_CHAR_DESC]);
            }

            if(values.Contains(SAPSTATUS_ID)) {
                model.SAPStatusId = values[SAPSTATUS_ID] != null ? Convert.ToInt32(values[SAPSTATUS_ID]) : (int?)null;
            }

            if(values.Contains(SERIAL_NUMBER)) {
                model.SerialNumber = Convert.ToString(values[SERIAL_NUMBER]);
            }

            if(values.Contains(MEXEQUIP_LIST)) {
                model.MEXEquipList = Convert.ToString(values[MEXEQUIP_LIST]);
            }

            if(values.Contains(MEXPARENT_EQUIP)) {
                model.MEXParentEquip = Convert.ToString(values[MEXPARENT_EQUIP]);
            }

            if(values.Contains(SUP_FUNCT_LOC)) {
                model.SupFunctLoc = Convert.ToString(values[SUP_FUNCT_LOC]);
            }

            if(values.Contains(SORT_FIELD_ID)) {
                model.SortFieldId = values[SORT_FIELD_ID] != null ? Convert.ToInt32(values[SORT_FIELD_ID]) : (int?)null;
            }

            if(values.Contains(PLANNER_PLANTD_ID)) {
                model.PlannerPlantdId = values[PLANNER_PLANTD_ID] != null ? Convert.ToInt32(values[PLANNER_PLANTD_ID]) : (int?)null;
            }

            if(values.Contains(COMNPANY_CODE_ID)) {
                model.ComnpanyCodeId = values[COMNPANY_CODE_ID] != null ? Convert.ToInt32(values[COMNPANY_CODE_ID]) : (int?)null;
            }

            if(values.Contains(WBSELEMENT_ID)) {
                model.WBSElementId = values[WBSELEMENT_ID] != null ? Convert.ToInt32(values[WBSELEMENT_ID]) : (int?)null;
            }

            if(values.Contains(PBS_ID)) {
                model.PbsId = Convert.ToInt32(values[PBS_ID]);
            }

            if(values.Contains(ENV_ZONE_ID)) {
                model.EnvZoneId = Convert.ToInt32(values[ENV_ZONE_ID]);
            }
        }

        private string GetFullErrorMessage(ModelStateDictionary modelState) {
            var messages = new List<string>();

            foreach(var entry in modelState) {
                foreach(var error in entry.Value.Errors)
                    messages.Add(error.ErrorMessage);
            }

            return String.Join(" ", messages);
        }
    }
}