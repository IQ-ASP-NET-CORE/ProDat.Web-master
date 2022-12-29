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

    public class TLHRegisterController : Controller
    {
        private readonly TagContext _context;
        private readonly ILogger<TagContext> _logger;
        private readonly IModelMetadataProvider _provider;
        private IConfiguration _configuration;

        public TLHRegisterController(TagContext context, ILogger<TagContext> logger, IModelMetadataProvider provider, IConfiguration Configuration)
        {
            _provider = provider;
            _context = context;
            _logger = logger;
            _configuration = Configuration;
        }

        public async Task<IActionResult> Index(string columnSetsName = "Default")
        {
            //TODO:  Update Dictionary<str, int> to <str, obj> so we can capture default width as well. 
            Dictionary<string, ColParams> colIndex = new Dictionary<string, ColParams>();
            var col_customisations = await _context.ColumnSets
                                        .Where(x => x.ColumnSetsName == columnSetsName)
                                        .Where(x=> x.ColumnSetsEntity == "TaskListHeader")
                                        .Select(x => new {
                                            x.ColumnName,
                                            x.ColumnOrder,
                                            x.ColumnWidth,
                                            x.ColumnVisible
                                        })
                                        .ToListAsync();

            foreach (var cust in col_customisations)
            {
                colIndex.Add(cust.ColumnName, new ColParams(cust.ColumnOrder, cust.ColumnWidth));
            }

            // SAP Validation 
            var EAId = _context.EntityAttribute
                           .Where(x => x.EntityName == "TaskListHeader")
                           .Include(x => x.EntityAttributeRequirements);

            ViewData["SapValidationJson"] = JsonConvert.SerializeObject(EAId);

            // ViewBag required to update GridView properties and maintain state for selected view
            ViewBag.colIndex = colIndex;
            ViewBag.columnSetsName = columnSetsName;
            ViewBag.GlobalProjectDescription = _context.Project.First().ProjectName;
            return View();
        }



        #region Create new MaintPlan with a Form View. 

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
            var model = new TaskListHeader();
            var valuesDict = JsonConvert.DeserializeObject<IDictionary>(values);
            PopulateModel(model, valuesDict);

            if (!TryValidateModel(model))
                return BadRequest(GetFullErrorMessage(ModelState));

            var result = _context.TaskListHeader.Add(model);
            await _context.SaveChangesAsync();

            return Json(new { result.Entity.TaskListHeaderId });
        }


        private void PopulateModel(TaskListHeader model, IDictionary values)
        {
            string ID = nameof(TaskListHeader.TaskListHeaderId);
            string GROUP_ID = nameof(TaskListHeader.TaskListGroupId);
            string COUNTER = nameof(TaskListHeader.Counter);
            string SHORT_TEXT = nameof(TaskListHeader.TaskListShortText);
            string WORK_CENTRE_ID = nameof(TaskListHeader.MaintWorkCentreId);
            string PLANT_ID = nameof(TaskListHeader.MaintenancePlantId);
            string SYSCOND_ID = nameof(TaskListHeader.SysCondId);
            string STRAT_ID = nameof(TaskListHeader.MaintStrategyId);
            string PACKAGE_ID = nameof(TaskListHeader.MaintPackageId);
            string PMA_ID = nameof(TaskListHeader.PmassemblyId);
            string CAT_ID = nameof(TaskListHeader.TasklistCatId);
            string PS_ID = nameof(TaskListHeader.PerformanceStandardId);
            string PS_APP_DEL = nameof(TaskListHeader.PerfStdAppDel);
            string PS_REVIEW_ID = nameof(TaskListHeader.ScePsReviewId);
            string REG_BODY_ID = nameof(TaskListHeader.RegulatoryBodyId);
            string REG_BODY_APPDEL = nameof(TaskListHeader.RegBodyAppDel);
            string CHANGE_REQ = nameof(TaskListHeader.ChangeRequired);
            string PLANNER_GROUP_ID = nameof(TaskListHeader.MaintPlannerGroupId);
            string STATUS_ID = nameof(TaskListHeader.StatusId);
            string CLASS_ID = nameof(TaskListHeader.TaskListClassId);

            if (values.Contains(ID))
            {
                model.TaskListHeaderId = Convert.ToInt32(values[ID]);
            }

            if (values.Contains(GROUP_ID))
            {
                model.MaintPlannerGroupId = values[GROUP_ID] != null ? Convert.ToInt32(values[GROUP_ID]) : (int?)null;
            }

            if (values.Contains(COUNTER))
            {
                model.Counter = Convert.ToInt32(values[COUNTER]);
            }

            if (values.Contains(SHORT_TEXT))
            {
                model.TaskListShortText = Convert.ToString(values[SHORT_TEXT]);
            }

            if (values.Contains(WORK_CENTRE_ID))
            {
                model.MaintWorkCentreId = values[GROUP_ID] != null ? Convert.ToInt32(values[WORK_CENTRE_ID]) : (int?)null;
            }

            if (values.Contains(PLANT_ID))
            {
                model.MaintenancePlantId = values[GROUP_ID] != null ? Convert.ToInt32(values[PLANT_ID]) : (int?)null;
            }

            if (values.Contains(SYSCOND_ID))
            {
                model.SysCondId = values[GROUP_ID] != null ? Convert.ToInt32(values[SYSCOND_ID]) : (int?)null;
            }

            if (values.Contains(STRAT_ID))
            {
                model.MaintStrategyId = values[STRAT_ID] != null ? Convert.ToInt32(values[STRAT_ID]) : (int?)null;
            }

            if (values.Contains(PACKAGE_ID))
            {
                model.MaintPackageId = values[GROUP_ID] != null ? Convert.ToInt32(values[PACKAGE_ID]) : (int?)null;
            }

            if (values.Contains(CAT_ID))
            {
                model.TasklistCatId = values[CAT_ID] != null ? Convert.ToInt32(values[CAT_ID]) : (int?)null;
            }

            if (values.Contains(PMA_ID))
            {
                model.PmassemblyId = values[GROUP_ID] != null ? Convert.ToInt32(values[PMA_ID]) : (int?)null;
            }

            if (values.Contains(PS_ID))
            {
                model.PerformanceStandardId = values[GROUP_ID] != null ? Convert.ToInt32(values[PS_ID]) : (int?)null;
            }

            if (values.Contains(PS_APP_DEL))
            {
                model.PerfStdAppDel = Convert.ToString(values[PS_APP_DEL]);
            }

            if (values.Contains(PS_REVIEW_ID))
            {
                model.ScePsReviewId = values[PS_REVIEW_ID] != null ? Convert.ToInt32(values[PS_REVIEW_ID]) : (int?)null;
            }

            if (values.Contains(REG_BODY_ID))
            {
                model.RegulatoryBodyId = values[REG_BODY_ID] != null ? Convert.ToInt32(values[REG_BODY_ID]) : (int?)null;
            }

            if (values.Contains(REG_BODY_APPDEL))
            {
                model.RegBodyAppDel = Convert.ToString(values[REG_BODY_APPDEL]);
            }

            if (values.Contains(CHANGE_REQ))
            {
                model.ChangeRequired = Convert.ToString(values[CHANGE_REQ]);
            }

            if (values.Contains(PLANNER_GROUP_ID))
            {
                model.MaintPlannerGroupId = values[PLANNER_GROUP_ID] != null ? Convert.ToInt32(values[PLANNER_GROUP_ID]) : (int?)null;
            }

            if (values.Contains(STATUS_ID))
            {
                model.StatusId = values[STATUS_ID] != null ? Convert.ToInt32(values[STATUS_ID]) : (int?)null;
            }

            if (values.Contains(CLASS_ID))
            {
                model.TaskListClassId = values[CLASS_ID] != null ? Convert.ToInt32(values[CLASS_ID]) : (int?)null;
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

        public Object TLHRegister_GetData(DataSourceLoadOptions loadOptions)
        {
            // Oh well, this causes a loop, so will need to find waht field is causing an objhect depth to exceed 32 or be cyclic. 
            // var dataSet = _context.Tag.AsQueryable();

            var dataSet = _context.TaskListHeader;
                              //select new { rec.TagId, rec.TagNumber, rec.TagService, rec.TagFloc, rec.TagFlocDesc, rec.EngDiscId, rec.SubSystem.SubSystemNum };
                          

            return DataSourceLoader.Load(dataSet, loadOptions);
        }

        [Authorize(Roles = "User")]
        [HttpPost]
        public IActionResult TLHRegister_Insert(string values)
        {
            // added below to remove 'MaintPlanId=\"xx\",' as ID will cause error in MaintPlan on add.
            // this shouldnt happen... Add should ignore PK field(?) to investigate. 
            var orgTaskListHeaderId = "";

            if (values.Contains("TaskListHeaderId"))
            {
                var startpoint = values.IndexOf(",") + 1;
                orgTaskListHeaderId = values.Substring(38, startpoint-38-1);
                values = "{" + values.Substring(startpoint);
            }

            var newObj = new TaskListHeader();
            JsonConvert.PopulateObject(values, newObj);

            // unique insert? get max(counter) for current TaskListGroupId, add 1.
            var count = _context.TaskListHeader
                        .Where(x => x.TaskListGroupId == newObj.TaskListGroupId)
                        .Select(x=> x.Counter).Max();

            newObj.Counter = count + 1;

            if (!TryValidateModel(newObj))
                return BadRequest();

            _context.TaskListHeader.Add(newObj);
            _context.SaveChanges(); // need to save to set TaskListHeaderId. 

            // clone old TLH's TLO's if any.
            var tlos = _context.TaskListOperations
                        .Where(x => x.TaskListHeaderId == int.Parse(orgTaskListHeaderId))
                        .AsNoTracking(); // AsNoTracking required so we dont accidently modify originals. 

            foreach (TaskListOperations tlo in tlos)
            {
                // clone item, pointing to new TaskListHeaderId.
                // What about ID? do I have to strip this?
                tlo.TaskListHeaderId = newObj.TaskListHeaderId;
                tlo.TaskListOperationId = 0;
                _context.TaskListOperations.Add(tlo);
            }

            // For TLH to appear in Maint Item view, it must be in MaintItemXmaintTaskListHeader
            //


            _context.SaveChanges();

            return Ok(newObj);
        }

        [Authorize(Roles = "User")]
        [HttpPut]
        public IActionResult TLHRegister_Update(int key, string values)
        {
            // TODO override to update tag state. 
            var order = _context.TaskListHeader.First(o => o.TaskListHeaderId == key);
            JsonConvert.PopulateObject(values, order);

            if (!TryValidateModel(order))
                return BadRequest();

            _context.SaveChanges();

            return Ok(order);
        }

        [Authorize(Roles = "User")]
        [HttpDelete]
        public void TLHRegister_Delete(int key)
        {
            var order = _context.TaskListHeader.First(o => o.TaskListHeaderId == key);

            // Test for TLO??

        }

        [Authorize(Roles = "User")]
        [HttpPost]
        public object TLHRegister_Batch(List<DataChange> changes)
        {
            foreach (var change in changes)
            {
                TaskListHeader order;

                if (change.Type == "update" || change.Type == "remove")
                {
                    var key = Convert.ToInt32(change.Key);
                    order = _context.TaskListHeader.First(o => o.TaskListHeaderId == key);
                }
                else
                {
                    order = new TaskListHeader();
                }

                if (change.Type == "insert" || change.Type == "update")
                {
                    JsonConvert.PopulateObject(change.Data.ToString(), order);

                    if (!TryValidateModel(order))
                        return BadRequest();

                    if (change.Type == "insert")
                    {
                        _context.TaskListHeader.Add(order);
                    }
                    change.Data = order;
                }
                else if (change.Type == "remove")
                {
                    _context.TaskListHeader.Remove(order);
                }
            }

            _context.SaveChanges();

            return Ok(changes);
        }

        #endregion

        #region data management for controllers

        public object TaskListHeader_GetData(DataSourceLoadOptions loadOptions)
        {
            var data = _context.MaintItemXmaintTaskListHeader
                            .Include(x => x.TaskListHeader)
                            .Include(x => x.MaintItem)
                            //.OrderBy(x=> x.TaskListHeader.TaskListGroup.TaskListGroupName)
                            .OrderBy(x => x.TaskListHeader.Counter)
                            .Select(x => new TaskListHeader_UC3v2
                            {
                                TaskListHeader = new Models.TaskListHeader
                                {
                                    TaskListHeaderId = x.TaskListHeaderId,
                                    TaskListGroupId = x.TaskListHeader.TaskListGroupId,
                                    Counter = x.TaskListHeader.Counter,
                                    TaskListShortText = x.TaskListHeader.TaskListShortText,
                                    MaintWorkCentreId = x.TaskListHeader.MaintWorkCentreId,
                                    MaintPlannerGroupId = x.TaskListHeader.MaintPlannerGroupId,
                                    MaintenancePlantId = x.TaskListHeader.MaintenancePlantId,
                                    MaintStrategyId = x.TaskListHeader.MaintStrategyId,
                                    SysCondId = x.TaskListHeader.SysCondId,
                                    MaintPackageId = x.TaskListHeader.MaintPackageId,
                                    PmassemblyId = x.TaskListHeader.PmassemblyId,
                                    TasklistCatId = x.TaskListHeader.TasklistCatId,
                                    PerformanceStandardId = x.TaskListHeader.PerformanceStandardId,
                                    PerfStdAppDel = x.TaskListHeader.PerfStdAppDel,
                                    ScePsReviewId = x.TaskListHeader.ScePsReviewId,
                                    RegulatoryBodyId = x.TaskListHeader.RegulatoryBodyId,
                                    RegBodyAppDel = x.TaskListHeader.RegBodyAppDel,
                                    ChangeRequired = x.TaskListHeader.ChangeRequired,
                                    TaskListClassId = x.TaskListHeader.TaskListClassId,
                                    StatusId = x.TaskListHeader.StatusId
                                },
                                MaintPlanId = (int)x.MaintItem.MaintPlanId,
                                MaintItemId = x.MaintItemId,
                            }
                            );

            return DataSourceLoader.Load(data, loadOptions);
        }

        #endregion 


        public IActionResult Excel(string currentFilter)
        {
            // Retrieve all TLH into recordset. State which star models to include.
            var tagModel = (from t in _context.TaskListHeader
                                
                                .Include(t => t.MaintPackage)
                                .Include(t => t.MaintStrategy)
                                .Include(t => t.MaintWorkCentre)
                                .Include(t => t.MaintenancePlant)
                                .Include(t => t.PerformanceStandard)
                                .Include(t => t.Pmassembly)
                                .Include(t => t.RegulatoryBody)
                                .Include(t => t.SysCond)
                                .Include(t => t.TaskListGroup)
                                .Include(t => t.TasklistCat)
                                .Include(t => t.MaintPlannerGroup)
                                .Include(t => t.Status)

                            select t).AsQueryable();

            var TagMeta = _provider.GetMetadataForType(typeof(Models.TaskListHeader));

            using (var workbook = new XLWorkbook())
            {
                IXLWorksheet worksheet = workbook.Worksheets.Add("TLH");
                int currentRow = 1;

                // fieldNames to ignore.
                ICollection<string> ignoreFields = new System.Collections.ObjectModel.Collection<string>();

                // Fields to export = distinct set of fieldnames in ColumnSets, so is controlled by Admin table. 
                List<string> fieldsToExport = new List<string> { };
                var tmp = _context.ColumnSets
                                  .Where(x => x.ColumnSetsEntity == "TaskListHeader")
                                  .Select(x => x.ColumnName);

                fieldsToExport = (List<string>)tmp.Distinct().ToList();

                // strip Id from FieldName. We'll dig into these entities to retrieve Num or Name attribute.
                for (int j = 0; j < fieldsToExport.Count(); j++)
                    if (fieldsToExport[j].EndsWith("Id"))
                        fieldsToExport[j] = fieldsToExport[j].Substring(0, fieldsToExport[j].Length - 2);

                // build header from metadata.
                int i = 1;
                ICollection<string> fields = new System.Collections.ObjectModel.Collection<string>();
                fields.Add("TLHNumber");
                worksheet.Cell(currentRow, i++).Value = "TLHNumber";

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

                worksheet.Cell(currentRow, i).Value = "NEW_TLHNumber";

                // #####################
                // ##  Export Values  ##
                // #####################
                ExpressionGet t = new ExpressionGet();
                foreach (TaskListHeader tag in tagModel)
                {
                    currentRow++;
                    i = 1;
                    foreach (string field in fields)
                    {
                        if (field == "TLHNumber")
                        {
                            // output TaskListName + Counter + OperationNum
                            var tlgName = (tag.TaskListGroup != null) ? tag.TaskListGroup.TaskListGroupName : null;
                            var tlhCounter = tag.Counter.ToString().PadLeft(2, '0');
                            string retVal = "";
                            if (tlgName != null)
                            {
                                retVal = tlgName + "-" + tlhCounter;
                            }
                            worksheet.Cell(currentRow, i++).SetValue(retVal);
                        }
                        else
                        {
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

                }

                worksheet.Columns("A:AZ").AdjustToContents();

                using (var stream = new System.IO.MemoryStream())
                {
                    workbook.SaveAs(stream);
                    var content = stream.ToArray();

                    return File(
                        content,
                        "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                        "TLHRegister.xlsx");
                }
            }
        }

        public ActionResult ExportToExcel()
        {
            return View();
        }

    } //UC1 Controller

} // Namespace

