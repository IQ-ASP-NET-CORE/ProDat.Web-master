using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ClosedXML.Excel;
using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using ProDat.Web2.Data;
using ProDat.Web2.Models;
using ProDat.Web2.Models.DataGrid;
using ProDat.Web2.TagLibrary;
using ProDat.Web2.ViewModels;

namespace ProDat.Web2.Controllers
{
    /*
     * Provides functionality to manage Tag Register. 
     * Note: Star lookup data sources are defined in Lookups Controller.
     */

    public class UC1Controller : Controller
    {
        private readonly TagContext _context;
        private readonly ILogger<TagContext> _logger;
        private readonly IModelMetadataProvider _provider;
        private IConfiguration _configuration;

        public UC1Controller(TagContext context, ILogger<TagContext> logger, IModelMetadataProvider provider, IConfiguration Configuration)
        {
            _provider = provider;
            _context = context;
            _logger = logger;
            _configuration = Configuration;
        }

        public async Task<IActionResult> Index(string columnSetsName="Default")
        {

            // to update. Add check for MFA not set and redirect to 2FA Management page. 
            // confirm user has mfa, else redirect to MFA setup.
            var claimTwoFactorEnabled =
               User.Claims.FirstOrDefault(t => t.Type == "amr");


            if (claimTwoFactorEnabled != null && "mfa".Equals(claimTwoFactorEnabled.Value))
            {
                // continue
            }
            else
            {
                return Redirect(
                    // Modified by MWM 
                    "/Identity/Account/Login");
                    //"/Identity/Account/Manage/TwoFactorAuthentication");
            }

            //TODO:  Update Dictionary<str, int> to <str, obj> so we can capture default width as well. 
            Dictionary<string, ColParams> colIndex = new Dictionary<string, ColParams>();
            var col_customisations =  _context.ColumnSets
                                        .Where(x => x.ColumnSetsEntity == "Tag")
                                        .Where(x => x.ColumnSetsName == columnSetsName)
                                        .Select(x => new { x.ColumnName
                                                            , x.ColumnOrder
                                                            , x.ColumnWidth
                                                            , x.ColumnVisible
                                                         }
                                               );
            foreach (var cust in col_customisations)
            {
                colIndex.Add(cust.ColumnName, new ColParams(cust.ColumnOrder, cust.ColumnWidth) );
            }

            // SAP Validation 
            var EAId = _context.EntityAttribute
                           .Where(x => x.EntityName == "Tag")
                           .Include(x => x.EntityAttributeRequirements);

            ViewData["SapValidationJson"] = JsonConvert.SerializeObject(EAId);

            // ViewBag required to update GridView properties and maintain state for selected view
            ViewBag.colIndex = colIndex;
            ViewBag.columnSetsName = columnSetsName;
            ViewBag.GlobalProjectDescription = _context.Project.First().ProjectName;

            return View();
        }



        #region Create new Tag with a Form View. 

        [Authorize(Roles = "User")]
        [HttpGet]
        public IActionResult Create()
        {
            // Load up Create Form without data. 
            ViewBag.GlobalProjectDescription = _context.Project.First().ProjectName;
            return View();
        }

        [Authorize(Roles = "User")]
        [HttpPost]
        public async Task<IActionResult> Create(string values)
        {
            // Post from Create Form. 
            var model = new Tag();
            var valuesDict = JsonConvert.DeserializeObject<IDictionary>(values);
            PopulateModel(model, valuesDict);

            if (!TryValidateModel(model))
                return BadRequest(GetFullErrorMessage(ModelState));

            var result = _context.Tag.Add(model);
            await _context.SaveChangesAsync();

            return Json(new { result.Entity.TagId });
        }


        private void PopulateModel(Tag model, IDictionary values)
        {
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

            if (values.Contains(TAG_ID))
            {
                model.TagId = Convert.ToInt32(values[TAG_ID]);
            }

            if (values.Contains(TAG_NUMBER))
            {
                model.TagNumber = Convert.ToString(values[TAG_NUMBER]);
            }

            if (values.Contains(TAG_SERVICE))
            {
                model.TagService = Convert.ToString(values[TAG_SERVICE]);
            }

            if (values.Contains(TAG_FLOC))
            {
                model.TagFloc = Convert.ToString(values[TAG_FLOC]);
            }

            if (values.Contains(SUB_SYSTEM_ID))
            {
                model.SubSystemId = values[SUB_SYSTEM_ID] != null ? Convert.ToInt32(values[SUB_SYSTEM_ID]) : (int?)null;
            }

            if (values.Contains(ENG_CLASS_ID))
            {
                model.EngClassId = values[ENG_CLASS_ID] != null ? Convert.ToInt32(values[ENG_CLASS_ID]) : (int?)null;
            }

            if (values.Contains(ENG_PARENT_ID))
            {
                model.EngParentId = values[ENG_PARENT_ID] != null ? Convert.ToInt32(values[ENG_PARENT_ID]) : (int?)null;
            }

            if (values.Contains(MAINT_PARENT_ID))
            {
                model.MaintParentId = values[MAINT_PARENT_ID] != null ? Convert.ToInt32(values[MAINT_PARENT_ID]) : (int?)null;
            }

            if (values.Contains(ENG_DISC_ID))
            {
                model.EngDiscId = values[ENG_DISC_ID] != null ? Convert.ToInt32(values[ENG_DISC_ID]) : (int?)null;
            }

            if (values.Contains(MAINT_LOCATION_ID))
            {
                model.MaintLocationId = values[MAINT_LOCATION_ID] != null ? Convert.ToInt32(values[MAINT_LOCATION_ID]) : (int?)null;
            }

            if (values.Contains(LOCATION_ID))
            {
                model.LocationID = values[LOCATION_ID] != null ? Convert.ToInt32(values[LOCATION_ID]) : (int?)null;
            }

            if (values.Contains(MAINT_TYPE_ID))
            {
                model.MaintTypeId = values[MAINT_TYPE_ID] != null ? Convert.ToInt32(values[MAINT_TYPE_ID]) : (int?)null;
            }

            if (values.Contains(MAINT_STATUS_ID))
            {
                model.MaintStatusId = values[MAINT_STATUS_ID] != null ? Convert.ToInt32(values[MAINT_STATUS_ID]) : (int?)null;
            }

            if (values.Contains(ENG_STATUS_ID))
            {
                model.EngStatusId = values[ENG_STATUS_ID] != null ? Convert.ToInt32(values[ENG_STATUS_ID]) : (int?)null;
            }

            if (values.Contains(MAINT_WORK_CENTRE_ID))
            {
                model.MaintWorkCentreId = values[MAINT_WORK_CENTRE_ID] != null ? Convert.ToInt32(values[MAINT_WORK_CENTRE_ID]) : (int?)null;
            }

            if (values.Contains(MAINT_EDC_CODE_ID))
            {
                model.MaintEdcCodeId = values[MAINT_EDC_CODE_ID] != null ? Convert.ToInt32(values[MAINT_EDC_CODE_ID]) : (int?)null;
            }

            if (values.Contains(MAINT_STRUCTURE_INDICATOR_ID))
            {
                model.MaintStructureIndicatorId = values[MAINT_STRUCTURE_INDICATOR_ID] != null ? Convert.ToInt32(values[MAINT_STRUCTURE_INDICATOR_ID]) : (int?)null;
            }

            if (values.Contains(COMMISSIONING_SUBSYSTEM_ID))
            {
                model.CommissioningSubsystemId = values[COMMISSIONING_SUBSYSTEM_ID] != null ? Convert.ToInt32(values[COMMISSIONING_SUBSYSTEM_ID]) : (int?)null;
            }

            if (values.Contains(COMM_CLASS_ID))
            {
                model.CommClassId = values[COMM_CLASS_ID] != null ? Convert.ToInt32(values[COMM_CLASS_ID]) : (int?)null;
            }

            if (values.Contains(COMM_ZONE_ID))
            {
                model.CommZoneId = values[COMM_ZONE_ID] != null ? Convert.ToInt32(values[COMM_ZONE_ID]) : (int?)null;
            }

            if (values.Contains(MAINT_PLANNER_GROUP_ID))
            {
                model.MaintPlannerGroupId = values[MAINT_PLANNER_GROUP_ID] != null ? Convert.ToInt32(values[MAINT_PLANNER_GROUP_ID]) : (int?)null;
            }

            if (values.Contains(MAINTENANCEPLAN_ID))
            {
                model.MaintenanceplanId = values[MAINTENANCEPLAN_ID] != null ? Convert.ToInt32(values[MAINTENANCEPLAN_ID]) : (int?)null;
            }

            if (values.Contains(MAINT_CRITICALITY_ID))
            {
                model.MaintCriticalityId = values[MAINT_CRITICALITY_ID] != null ? Convert.ToInt32(values[MAINT_CRITICALITY_ID]) : (int?)null;
            }

            if (values.Contains(PERFORMANCE_STANDARD_ID))
            {
                model.PerformanceStandardId = values[PERFORMANCE_STANDARD_ID] != null ? Convert.ToInt32(values[PERFORMANCE_STANDARD_ID]) : (int?)null;
            }

            if (values.Contains(KEY_DOC_ID))
            {
                model.KeyDocId = values[KEY_DOC_ID] != null ? Convert.ToInt32(values[KEY_DOC_ID]) : (int?)null;
            }

            if (values.Contains(PO_ID))
            {
                model.PoId = values[PO_ID] != null ? Convert.ToInt32(values[PO_ID]) : (int?)null;
            }

            if (values.Contains(TAG_SOURCE))
            {
                model.TagSource = Convert.ToString(values[TAG_SOURCE]);
            }

            if (values.Contains(TAG_DELETED))
            {
                model.TagDeleted = Convert.ToBoolean(values[TAG_DELETED]);
            }

            if (values.Contains(RTF))
            {
                model.RTF = Convert.ToBoolean(values[RTF]);
            }

            if (values.Contains(TAG_FLOC_DESC))
            {
                model.TagFlocDesc = Convert.ToString(values[TAG_FLOC_DESC]);
            }

            if (values.Contains(FLOC_TYPE_ID))
            {
                model.FlocTypeId = values[FLOC_TYPE_ID] != null ? Convert.ToInt32(values[FLOC_TYPE_ID]) : (int?)null;
            }

            if (values.Contains(TAG_MAINT_QUERY))
            {
                model.TagMaintQuery = Convert.ToBoolean(values[TAG_MAINT_QUERY]);
            }

            if (values.Contains(TAG_COMMENT))
            {
                model.TagComment = Convert.ToString(values[TAG_COMMENT]);
            }

            if (values.Contains(MODEL_ID))
            {
                model.ModelId = values[MODEL_ID] != null ? Convert.ToInt32(values[MODEL_ID]) : (int?)null;
            }

            if (values.Contains(VIB_ID))
            {
                model.VibId = values[VIB_ID] != null ? Convert.ToInt32(values[VIB_ID]) : (int?)null;
            }

            if (values.Contains(TAGNONENG))
            {
                model.Tagnoneng = Convert.ToBoolean(values[TAGNONENG]);
            }

            if (values.Contains(TAG_VENDOR_TAG))
            {
                model.TagVendorTag = Convert.ToString(values[TAG_VENDOR_TAG]);
            }

            if (values.Contains(MAINT_OBJECT_TYPE_ID))
            {
                model.MaintObjectTypeId = values[MAINT_OBJECT_TYPE_ID] != null ? Convert.ToInt32(values[MAINT_OBJECT_TYPE_ID]) : (int?)null;
            }

            if (values.Contains(RBI_SIL_ID))
            {
                model.RbiSilId = values[RBI_SIL_ID] != null ? Convert.ToInt32(values[RBI_SIL_ID]) : (int?)null;
            }

            if (values.Contains(IPF_ID))
            {
                model.IpfId = values[IPF_ID] != null ? Convert.ToInt32(values[IPF_ID]) : (int?)null;
            }

            if (values.Contains(RCM_ID))
            {
                model.RcmId = values[RCM_ID] != null ? Convert.ToInt32(values[RCM_ID]) : (int?)null;
            }

            if (values.Contains(TAG_RAW_NUMBER))
            {
                model.TagRawNumber = Convert.ToString(values[TAG_RAW_NUMBER]);
            }

            if (values.Contains(TAG_RAW_DESC))
            {
                model.TagRawDesc = Convert.ToString(values[TAG_RAW_DESC]);
            }

            if (values.Contains(MAINT_SCE_PS_REVIEW_TEAM_ID))
            {
                model.MaintScePsReviewTeamId = values[MAINT_SCE_PS_REVIEW_TEAM_ID] != null ? Convert.ToInt32(values[MAINT_SCE_PS_REVIEW_TEAM_ID]) : (int?)null;
            }

            if (values.Contains(MAINT_SCE_PS_JUSTIFICATION))
            {
                model.MaintScePsJustification = Convert.ToString(values[MAINT_SCE_PS_JUSTIFICATION]);
            }

            if (values.Contains(TAG_MAINT_CRIT_COMMENTS))
            {
                model.TagMaintCritComments = Convert.ToString(values[TAG_MAINT_CRIT_COMMENTS]);
            }

            if (values.Contains(RBM_ID))
            {
                model.RbmId = values[RBM_ID] != null ? Convert.ToInt32(values[RBM_ID]) : (int?)null;
            }

            if (values.Contains(MANUFACTURER_ID))
            {
                model.ManufacturerId = values[MANUFACTURER_ID] != null ? Convert.ToInt32(values[MANUFACTURER_ID]) : (int?)null;
            }

            if (values.Contains(EX_METHOD_ID))
            {
                model.ExMethodId = values[EX_METHOD_ID] != null ? Convert.ToInt32(values[EX_METHOD_ID]) : (int?)null;
            }

            if (values.Contains(TAG_RBM_METHOD))
            {
                model.TagRbmMethod = Convert.ToString(values[TAG_RBM_METHOD]);
            }

            if (values.Contains(TAG_VIB))
            {
                model.TagVib = Convert.ToString(values[TAG_VIB]);
            }

            if (values.Contains(TAG_SRC_KEY_LIST))
            {
                model.TagSrcKeyList = Convert.ToString(values[TAG_SRC_KEY_LIST]);
            }

            if (values.Contains(TAG_BOM_REQ))
            {
                model.TagBomReq = Convert.ToString(values[TAG_BOM_REQ]);
            }

            if (values.Contains(TAG_SP_NO))
            {
                model.TagSpNo = Convert.ToString(values[TAG_SP_NO]);
            }

            if (values.Contains(MAINT_SORT_PROCESS_ID))
            {
                model.MaintSortProcessId = values[MAINT_SORT_PROCESS_ID] != null ? Convert.ToInt32(values[MAINT_SORT_PROCESS_ID]) : (int?)null;
            }

            if (values.Contains(TAG_CHARACTERISTIC))
            {
                model.TagCharacteristic = Convert.ToString(values[TAG_CHARACTERISTIC]);
            }

            if (values.Contains(TAG_CHAR_VALUE))
            {
                model.TagCharValue = Convert.ToString(values[TAG_CHAR_VALUE]);
            }

            if (values.Contains(TAG_CHAR_DESC))
            {
                model.TagCharDesc = Convert.ToString(values[TAG_CHAR_DESC]);
            }

            if (values.Contains(SAPSTATUS_ID))
            {
                model.SAPStatusId = values[SAPSTATUS_ID] != null ? Convert.ToInt32(values[SAPSTATUS_ID]) : (int?)null;
            }

            if (values.Contains(SERIAL_NUMBER))
            {
                model.SerialNumber = Convert.ToString(values[SERIAL_NUMBER]);
            }

            if (values.Contains(MEXEQUIP_LIST))
            {
                model.MEXEquipList = Convert.ToString(values[MEXEQUIP_LIST]);
            }

            if (values.Contains(MEXPARENT_EQUIP))
            {
                model.MEXParentEquip = Convert.ToString(values[MEXPARENT_EQUIP]);
            }

            if (values.Contains(SUP_FUNCT_LOC))
            {
                model.SupFunctLoc = Convert.ToString(values[SUP_FUNCT_LOC]);
            }

            if (values.Contains(SORT_FIELD_ID))
            {
                model.SortFieldId = values[SORT_FIELD_ID] != null ? Convert.ToInt32(values[SORT_FIELD_ID]) : (int?)null;
            }

            if (values.Contains(PLANNER_PLANTD_ID))
            {
                model.PlannerPlantdId = values[PLANNER_PLANTD_ID] != null ? Convert.ToInt32(values[PLANNER_PLANTD_ID]) : (int?)null;
            }

            if (values.Contains(COMNPANY_CODE_ID))
            {
                model.ComnpanyCodeId = values[COMNPANY_CODE_ID] != null ? Convert.ToInt32(values[COMNPANY_CODE_ID]) : (int?)null;
            }

            if (values.Contains(WBSELEMENT_ID))
            {
                model.WBSElementId = values[WBSELEMENT_ID] != null ? Convert.ToInt32(values[WBSELEMENT_ID]) : (int?)null;
            }

            if (values.Contains(PBS_ID))
            {
                model.PbsId = Convert.ToInt32(values[PBS_ID]);
            }

            if (values.Contains(ENV_ZONE_ID))
            {
                model.EnvZoneId = Convert.ToInt32(values[ENV_ZONE_ID]);
            }
        }

        private string GetFullErrorMessage(ModelStateDictionary modelState)
        {
            var messages = new List<string>();

            foreach (var entry in modelState)
            {
                foreach (var error in entry.Value.Errors)
                    messages.Add(error.ErrorMessage);
            }

            return String.Join(" ", messages);
        }


        #endregion


        #region data mangement for grid view.

        public Object TagRegister_GetData(DataSourceLoadOptions loadOptions)
        {
            // Oh well, this causes a loop, so will need to find waht field is causing an objhect depth to exceed 32 or be cyclic. 
            // var dataSet = _context.Tag.AsQueryable();

            var dataSet = from rec in _context.Tag
                              //select new { rec.TagId, rec.TagNumber, rec.TagService, rec.TagFloc, rec.TagFlocDesc, rec.EngDiscId, rec.SubSystem.SubSystemNum };
                          select new UC1_TagsVM(rec);

            return DataSourceLoader.Load(dataSet, loadOptions);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public IActionResult TagRegister_Insert(string values)
        {
            var newTag = new Tag();
            JsonConvert.PopulateObject(values, newTag);

            if (!TryValidateModel(newTag))
                return BadRequest();

            _context.Tag.Add(newTag);
            _context.SaveChanges();

            return Ok(newTag);
        }

        [Authorize(Roles = "User")]
        [HttpPut]
        public IActionResult TagRegister_Update(int key, string values)
        {
            // TODO override to update tag state. 
            var order = _context.Tag.First(o => o.TagId == key);
            JsonConvert.PopulateObject(values, order);

            if (!TryValidateModel(order))
                return BadRequest();

            _context.SaveChanges();

            return Ok(order);
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete]
        public void TagRegister_Delete(int key)
        {
            var order = _context.Tag.First(o => o.TagId == key);
            _context.Tag.Remove(order);
            _context.SaveChanges();
        }

        // Functionality not used.
        [Authorize(Roles = "User")]
        [HttpPost]
        public object TagRegister_Batch(List<DataChange> changes)
        {
            foreach (var change in changes)
            {
                Tag order;

                if (change.Type == "update" || change.Type == "remove")
                {
                    var key = Convert.ToInt32(change.Key);
                    order = _context.Tag.First(o => o.TagId == key);
                }
                else
                {
                    order = new Tag();
                }

                if (change.Type == "insert" || change.Type == "update")
                {
                    JsonConvert.PopulateObject(change.Data.ToString(), order);

                    if (!TryValidateModel(order))
                        return BadRequest();

                    if (change.Type == "insert")
                    {
                        _context.Tag.Add(order);
                    }
                    change.Data = order;
                }
                else if (change.Type == "remove")
                {
                    _context.Tag.Remove(order);
                }
            }

            _context.SaveChanges();

            return Ok(changes);
        }

        #endregion

        // Return Excel document to User. 
        // Takes current TagRegisterSearchViewModel to filter.
        public IActionResult Excel(string currentFilter)
        {
            // Retrieve all Tags into recordset. State which star models to include.
            var tagModel = (from t in _context.Tag
                                //.Include(t => t.EngParent)
                                .Include(t => t.EngDisc)
                                .Include(t => t.MaintCriticality)
                                .Include(t => t.MaintLocation)
                                .Include(t => t.MaintObjectType)
                                .Include(t => t.SubSystem)
                                .Include(t => t.SubSystem.Systems)
                                .Include(t => t.MaintWorkCentre)
                                // additional stars
                                .Include(t => t.CommClass)
                                .Include(t => t.MaintEdcCode)
                                .Include(t => t.EngClass)
                                .Include(t => t.Ipf)
                                .Include(t => t.Location)
                                .Include(t => t.MaintPlannerGroup)
                                .Include(t => t.MaintScePsReviewTeam)
                                .Include(t => t.MaintSortProcess)
                                .Include(t => t.MaintType)
                                .Include(t => t.Manufacturer)
                                .Include(t => t.RbiSil)
                                .Include(t => t.Rbm)
                                .Include(t => t.Rcm)
                                .Include(t => t.Vib)
                                // costly? to test and confirm.
                                .Include(t => t.MaintParent)
                                .Include(t => t.MaintStructureIndicator)
                                .Include(t => t.PerformanceStandard)
                                .Include(t => t.ExMethod)
                            select t).AsQueryable();

            var TagMeta = _provider.GetMetadataForType(typeof(Models.Tag));

            using (var workbook = new XLWorkbook())
            {
                IXLWorksheet worksheet = workbook.Worksheets.Add("Tags");
                int currentRow = 1;

                // fieldNames to ignore.
                ICollection<string> ignoreFields = new System.Collections.ObjectModel.Collection<string>();

                //InverseMaintParents takes long time to query as each Tag hashes whole table to get parent.
                ignoreFields.Add("InverseMaintParents");

                // Fields to export = distinct set of fieldnames in ColumnSets, so is controlled by Admin table. 
                List<string> fieldsToExport = new List<string> { };
                var tmp = _context.ColumnSets
                                  .Where(x => x.ColumnSetsEntity == "Tag")
                                  .Select(x => x.ColumnName);

                fieldsToExport = (List<string>)tmp.Distinct().ToList();

                // strip Id from FieldName. We'll dig into these entities to retrieve Num or Name attribute.
                for (int j = 0; j < fieldsToExport.Count(); j++)
                    if (fieldsToExport[j].EndsWith("Id"))
                        fieldsToExport[j] = fieldsToExport[j].Substring(0, fieldsToExport[j].Length - 2);

                // build header from metadata.
                int i = 1;
                ICollection<string> fields = new System.Collections.ObjectModel.Collection<string>();
                foreach (var property in TagMeta.Properties)
                {
                    if (ignoreFields.Contains(property.Name) || property.Name.EndsWith("Id"))
                        continue;

                    // If not basic attribute, it is an entity. 
                    // Drill into entity and retrieve property ending with Num, else Name
                    if (property.ModelType != typeof(string) && property.ModelType != typeof(int) && property.ModelType != typeof(DateTime) && property.ModelType != typeof(bool))
                    {
                        string entityName = property.Name;
                        var ChildMeta = _provider.GetMetadataForType(property.ModelType);
                        foreach (var childProperty in ChildMeta.Properties)
                        {
                            // Stop outputting Name fields for Subsystem/system
                            if (childProperty.Name.EndsWith("SubSystemName") || childProperty.Name.EndsWith("SystemName"))
                                continue;

                            // need to test if Num exists, and take that as priority. Dont dig further if Num found.
                            if (childProperty.Name == entityName + "Num" || childProperty.Name == entityName + "Name")
                            {
                                if (fieldsToExport.Contains(entityName))
                                {
                                    fields.Add(entityName + "." + childProperty.Name);
                                    worksheet.Cell(currentRow, i++).Value = childProperty.Name;
                                    continue;
                                }
                            }
                            // dig dig... go to next level and get System Num/Name
                            if (childProperty.ModelType == typeof(Systems))
                            {
                                //string entityName2 = childProperty.Name;
                                string entityName2 = "System";
                                var ChildMeta2 = _provider.GetMetadataForType(childProperty.ModelType);
                                foreach (var childProperty2 in ChildMeta2.Properties)
                                {
                                    if (childProperty2.Name == entityName2 + "Num" || childProperty2.Name == entityName2 + "Name")
                                    {
                                        if (fieldsToExport.Contains(entityName))
                                        {
                                            fields.Add(entityName + "." + childProperty.Name + "." + childProperty2.Name);
                                            worksheet.Cell(currentRow, i++).Value = childProperty2.Name;
                                            // num comes before name on child entries.
                                            break;
                                        }
                                    }
                                }
                            }
                        }
                    }
                    else
                    {
                        if (fieldsToExport.Contains(property.Name))
                        {
                            // Add fieldname, and index.
                            fields.Add(property.Name);
                            worksheet.Cell(currentRow, i++).Value = property.Name;
                        }
                    }
                }

                worksheet.Cell(currentRow, i).Value = "NEW_TagNumber";

                // #####################
                // ##  Export Values  ##
                // #####################
                ExpressionGet t = new ExpressionGet();
                foreach (Models.Tag tag in tagModel)
                {
                    currentRow++;
                    i = 1;
                    foreach (string field in fields)
                    {
                        //if (field.Contains("Systems") && tag.SubSystem != null)
                        //    Debug.Print("hold.");

                        var RetVal = t.getChildPropertyValue(field, tag);
                        if (RetVal == null)
                            RetVal = "";

                        if (RetVal.GetType() == typeof(bool))
                        {
                            RetVal = RetVal.ToString();
                        }
                        worksheet.Cell(currentRow, i++).SetValue(RetVal);
                    }
                }

                worksheet.Columns("A:AZ").AdjustToContents();

                using (var stream = new System.IO.MemoryStream())
                {
                    workbook.SaveAs(stream);
                    var content = stream.ToArray();

                    return File(
                        content,
                        "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                        "TagRegister.xlsx");
                }
            }
        }

        public ActionResult ExportToExcel()
        {
            return View();
        }

    } //UC1 Controller

} // Namespace

