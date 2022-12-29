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

    public class MPRegisterController : Controller
    {
        private readonly TagContext _context;
        private readonly ILogger<TagContext> _logger;
        private readonly IModelMetadataProvider _provider;
        private IConfiguration _configuration;

        public MPRegisterController(TagContext context, ILogger<TagContext> logger, IModelMetadataProvider provider, IConfiguration Configuration)
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
                                        .Where(x=> x.ColumnSetsEntity == "MaintPlan")
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
                           .Where(x => x.EntityName == "MaintPlan")
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

        [HttpPost]
        public async Task<IActionResult> Create(string values)
        {
            // Post from Create Form. 
            var model = new MaintPlan();
            var valuesDict = JsonConvert.DeserializeObject<IDictionary>(values);
            PopulateModel(model, valuesDict);

            if (!TryValidateModel(model))
                return BadRequest(GetFullErrorMessage(ModelState));

            var result = _context.MaintPlan.Add(model);
            await _context.SaveChangesAsync();

            return Json(new { result.Entity.MaintPlanId });
        }


        private void PopulateModel(MaintPlan model, IDictionary values)
        {
            string ID = nameof(MaintPlan.MaintPlanId);
            string MAINT_PLAN_NAME = nameof(MaintPlan.MaintPlanName);
            string SHORT_TEXT = nameof(MaintPlan.ShortText);
            string MAINT_STRATEGY_ID = nameof(MaintPlan.MaintStrategyId);
            string SORT = nameof(MaintPlan.Sort);
            string SORT_PROCESS_ID = nameof(MaintPlan.MaintSortProcessId);
            string CYCLE_MOD_FACTOR = nameof(MaintPlan.CycleModFactor);
            string START_DATE = nameof(MaintPlan.StartDate);
            string MEAS_POINT_ID = nameof(MaintPlan.MeasPointId);
            string CHANGE_STATUS = nameof(MaintPlan.ChangeStatus);
            string STARTING_INSTRUCTIONS = nameof(MaintPlan.StartingInstructions);
            string CALL_HORIZON = nameof(MaintPlan.CallHorizon);
            string SCHED_PERIOD = nameof(MaintPlan.SchedulingPeriodValue);
            string SCHED_UOM = nameof(MaintPlan.SchedulingPeriodUomId);


            if (values.Contains(ID))
            {
                model.MaintPlanId = Convert.ToInt32(values[ID]);
            }

            if (values.Contains(MAINT_PLAN_NAME))
            {
                model.MaintPlanName = Convert.ToString(values[MAINT_PLAN_NAME]);
            }

            if (values.Contains(SHORT_TEXT))
            {
                model.ShortText = Convert.ToString(values[SHORT_TEXT]);
            }

            if (values.Contains(SORT_PROCESS_ID))
            {
                model.MaintSortProcessId = Convert.ToInt32(values[SORT_PROCESS_ID]);
            }

            if (values.Contains(MAINT_STRATEGY_ID))
            {
                model.MaintStrategyId = values[MAINT_STRATEGY_ID] != null ? Convert.ToInt32(values[MAINT_STRATEGY_ID]) : (int?)null;
            }

            if (values.Contains(SORT))
            {
                model.Sort = Convert.ToString(values[SORT]);
            }

            if (values.Contains(CYCLE_MOD_FACTOR))
            {
                model.CycleModFactor = values[CYCLE_MOD_FACTOR] != null ? Convert.ToDouble(values[CYCLE_MOD_FACTOR]) : (double?)null;
            }

            if (values.Contains(START_DATE))
            {
                model.StartDate = Convert.ToString(values[START_DATE]);
            }

            if (values.Contains(MEAS_POINT_ID))
            {
                model.MeasPointId = values[MEAS_POINT_ID] != null ? Convert.ToInt32(values[MEAS_POINT_ID]) : (int?)null;
            }

            if (values.Contains(CHANGE_STATUS))
            {
                model.ChangeStatus = Convert.ToString(values[CHANGE_STATUS]);
            }

            if (values.Contains(STARTING_INSTRUCTIONS))
            {
                model.StartingInstructions = Convert.ToString(values[CHANGE_STATUS]);
            }

            if (values.Contains(CALL_HORIZON))
            {
                model.CallHorizon = Convert.ToString(values[CHANGE_STATUS]);
            }

            if (values.Contains(SCHED_PERIOD))
            {
                model.SchedulingPeriodValue = values[SCHED_PERIOD] != null ? Convert.ToInt32(values[SCHED_PERIOD]) : (int?)null;
            }

            if (values.Contains(SCHED_UOM))
            {
                model.SchedulingPeriodUomId = values[SCHED_UOM] != null ? Convert.ToInt32(values[SCHED_UOM]) : (int?)null;
                //model.SchedulingPeriodUom = Convert.ToString(values[SCHED_UOM]);
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

        public Object MPRegister_GetData(DataSourceLoadOptions loadOptions)
        {
            // Oh well, this causes a loop, so will need to find waht field is causing an objhect depth to exceed 32 or be cyclic. 
            // var dataSet = _context.Tag.AsQueryable();

            var dataSet = _context.MaintPlan;
                              //select new { rec.TagId, rec.TagNumber, rec.TagService, rec.TagFloc, rec.TagFlocDesc, rec.EngDiscId, rec.SubSystem.SubSystemNum };
                          

            return DataSourceLoader.Load(dataSet, loadOptions);
        }


        [Authorize(Roles = "User")]
        [HttpPost]
        public IActionResult MPRegister_Insert(string values)
        {
            var newObj = new MaintPlan();

            // added below to remove 'MaintPlanId=\"xx\",' as ID will cause error in MaintPlan on add.
            // this shouldnt happen... Add should ignore PK field(?) to investigate. 
            if (values.Contains("MaintPlanId"))
            {
                var startpoint = values.IndexOf(",") + 1;
                values = "{" + values.Substring(startpoint);
            }

            JsonConvert.PopulateObject(values, newObj);

            // unique constraint on MaintPlanName. Append CLONE x to keep unique. 
            var count = _context.MaintPlan
                        .Where(x => x.MaintPlanName.StartsWith(newObj.MaintPlanName)).Count();

            newObj.MaintPlanName = (newObj.MaintPlanName + " CLONE " + count.ToString("00") ).Trim();

            if (!TryValidateModel(newObj))
                return BadRequest();

            _context.MaintPlan.Add(newObj);
            _context.SaveChanges();

            return Ok(newObj);
        }

        [Authorize(Roles = "User")]
        [HttpPut]
        public IActionResult MPRegister_Update(int key, string values)
        {
            // TODO override to update tag state. 
            var order = _context.MaintPlan.First(o => o.MaintPlanId == key);
            JsonConvert.PopulateObject(values, order);

            if (!TryValidateModel(order))
                return BadRequest();

            _context.SaveChanges();

            return Ok(order);
        }

        [Authorize(Roles = "User")]
        [HttpDelete]
        public void MPRegister_Delete(int key)
        {
            var order = _context.MaintPlan.First(o => o.MaintPlanId == key);

            //Dont remove it if has children.
            var children = _context.MaintItem
                .Where(x => x.MaintPlanId == order.MaintPlanId).Count();

            if (children < 1) { 
                _context.MaintPlan.Remove(order);
                _context.SaveChanges();
            }
        }

        [Authorize(Roles = "User")]
        [HttpPost]
        public object MPRegister_Batch(List<DataChange> changes)
        {
            foreach (var change in changes)
            {
                MaintPlan order;

                if (change.Type == "update" || change.Type == "remove")
                {
                    var key = Convert.ToInt32(change.Key);
                    order = _context.MaintPlan.First(o => o.MaintPlanId == key);
                }
                else
                {
                    order = new MaintPlan();
                }

                if (change.Type == "insert" || change.Type == "update")
                {
                    JsonConvert.PopulateObject(change.Data.ToString(), order);

                    if (!TryValidateModel(order))
                        return BadRequest();

                    if (change.Type == "insert")
                    {
                        _context.MaintPlan.Add(order);
                    }
                    change.Data = order;
                }
                else if (change.Type == "remove")
                {
                    _context.MaintPlan.Remove(order);
                }
            }

            _context.SaveChanges();

            return Ok(changes);
        }

        #endregion

        public IActionResult Excel(string currentFilter)
        {
            // Retrieve all MaintPlans into recordset. State which star models to include.
            var mpModel = (from t in _context.MaintPlan
                                .Include(t => t.MaintSortProcess)
                                .Include(t => t.MaintStrategy)
                                .Include(t => t.MeasPoint)

                           select t).AsQueryable();

            var MIMeta = _provider.GetMetadataForType(typeof(Models.MaintPlan));

            using (var workbook = new XLWorkbook())
            {
                IXLWorksheet worksheet = workbook.Worksheets.Add("MaintPlans");
                int currentRow = 1;

                // fieldNames to ignore.
                ICollection<string> ignoreFields = new System.Collections.ObjectModel.Collection<string>();

                //InverseMaintParents takes long time to query as each Tag hashes whole table to get parent.
                ignoreFields.Add("InverseMaintParents");

                // Fields to export = distinct set of fieldnames in ColumnSets, so is controlled by Admin table. 
                List<string> fieldsToExport = new List<string> { };
                var tmp = _context.ColumnSets
                                  .Where(x => x.ColumnSetsEntity == "MaintPlan")
                                  .Select(x => x.ColumnName);

                fieldsToExport = (List<string>)tmp.Distinct().ToList();

                // strip Id from FieldName. We'll dig into these entities to retrieve Num or Name attribute.
                for (int j = 0; j < fieldsToExport.Count(); j++)
                    if (fieldsToExport[j].EndsWith("Id"))
                        fieldsToExport[j] = fieldsToExport[j].Substring(0, fieldsToExport[j].Length - 2);

                // build header from metadata.
                int i = 1;
                ICollection<string> fields = new System.Collections.ObjectModel.Collection<string>();
                foreach (var property in MIMeta.Properties)
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

                worksheet.Cell(currentRow, i).Value = "MaintPlanName_NEW";

                // #####################
                // ##  Export Values  ##
                // #####################
                ExpressionGet t = new ExpressionGet();
                foreach (Models.MaintPlan maintPlan in mpModel)
                {
                    currentRow++;
                    i = 1;
                    foreach (string field in fields)
                    {
                        //if (field.Contains("Systems") && tag.SubSystem != null)
                        //    Debug.Print("hold.");

                        var RetVal = t.getChildPropertyValue(field, maintPlan);
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
                        "MaintPlanRegister.xlsx");
                }
            }
        }

        public ActionResult ExportToExcel()
        {
            return View();
        }

    }

}

