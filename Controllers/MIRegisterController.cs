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

    public class MIRegisterController : Controller
    {
        private readonly TagContext _context;
        private readonly ILogger<TagContext> _logger;
        private readonly IModelMetadataProvider _provider;
        private IConfiguration _configuration;

        public MIRegisterController(TagContext context, ILogger<TagContext> logger, IModelMetadataProvider provider, IConfiguration Configuration)
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
                                        .Where(x=> x.ColumnSetsEntity == "MaintItem")
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
                           .Where(x => x.EntityName == "MaintItem")
                           .Include(x => x.EntityAttributeRequirements);

            ViewData["SapValidationJson"] = JsonConvert.SerializeObject(EAId);

            // ViewBag required to update GridView properties and maintain state for selected view
            ViewBag.colIndex = colIndex;
            ViewBag.columnSetsName = columnSetsName;
            ViewBag.GlobalProjectDescription = _context.Project.First().ProjectName;
            return View();
        }



        #region Create new MaintItem with a Form View. 

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
            var model = new MaintItem();
            var valuesDict = JsonConvert.DeserializeObject<IDictionary>(values);
            PopulateModel(model, valuesDict);

            if (!TryValidateModel(model))
                return BadRequest(GetFullErrorMessage(ModelState));

            var result = _context.MaintItem.Add(model);
            await _context.SaveChangesAsync();

            return Json(new { result.Entity.MaintItemId });
        }


        private void PopulateModel(MaintItem model, IDictionary values)
        {
            string ID = nameof(MaintItem.MaintItemId);
            string MAINT_PLAN_ID = nameof(MaintItem.MaintPlanId);
            string NUM = nameof(MaintItem.MaintItemNum);
            string SHORT_TEXT = nameof(MaintItem.MaintItemShortText);
            string HEADER_FLOC_ID = nameof(MaintItem.HeaderFlocId);
            string OBJECT_LIST_FLOC = nameof(MaintItem.MaintItemObjectListFloc);
            string WORK_CENTRE_ID = nameof(MaintItem.MaintWorkCentreId);
            string PLANT_ID = nameof(MaintItem.MaintenancePlantId);
            string ORDER_TYPE = nameof(MaintItem.MaintItemOrderType);
            string PLANNER_GROUP_ID = nameof(MaintItem.MaintPlannerGroupId);
            string SYS_COND_ID = nameof(MaintItem.SysCondId);
            string PRIORITY_ID = nameof(MaintItem.PriorityId);
            string LONG_TEXT = nameof(MaintItem.MaintItemLongText);
            string EXECUTION_FACTOR = nameof(MaintItem.TaskListExecutionFactor);
            string DO_NOT_RELEASE = nameof(MaintItem.bDoNotRelImmed);


            if (values.Contains(ID))
            {
                model.MaintItemId = Convert.ToInt32(values[ID]);
            }

            if (values.Contains(MAINT_PLAN_ID))
            {
                model.MaintPlanId = Convert.ToInt32(values[MAINT_PLAN_ID]);
            }

            if (values.Contains(NUM))
            {
                model.MaintItemNum = Convert.ToString(values[NUM]);
            }

            if (values.Contains(SHORT_TEXT))
            {
                model.MaintItemShortText = Convert.ToString(values[SHORT_TEXT]);
            }

            if (values.Contains(HEADER_FLOC_ID))
            {
                model.HeaderFlocId = values[HEADER_FLOC_ID] != null ? Convert.ToInt32(values[HEADER_FLOC_ID]) : (int?)null;
            }

            if (values.Contains(OBJECT_LIST_FLOC))
            {
                model.MaintItemObjectListFloc = Convert.ToString(values[OBJECT_LIST_FLOC]);
            }

            if (values.Contains(WORK_CENTRE_ID))
            {
                model.MaintWorkCentreId = values[WORK_CENTRE_ID] != null ? Convert.ToInt32(values[WORK_CENTRE_ID]) : (int?)null;
            }

            if (values.Contains(PLANT_ID))
            {
                model.MaintenancePlantId = values[PLANT_ID] != null ? Convert.ToInt32(values[PLANT_ID]) : (int?)null;
            }

            if (values.Contains(ORDER_TYPE))
            {
                model.MaintItemOrderType = Convert.ToString(values[ORDER_TYPE]);
            }

            if (values.Contains(PLANNER_GROUP_ID))
            {
                model.MaintPlannerGroupId = values[PLANNER_GROUP_ID] != null ? Convert.ToInt32(values[PLANNER_GROUP_ID]) : (int?)null;
            }

            if (values.Contains(SYS_COND_ID))
            {
                model.SysCondId = values[SYS_COND_ID] != null ? Convert.ToInt32(values[SYS_COND_ID]) : (int?)null;
            }

            if (values.Contains(PRIORITY_ID))
            {
                model.PriorityId = values[PRIORITY_ID] != null ? Convert.ToInt32(values[PRIORITY_ID]) : (int?)null;
            }

            if (values.Contains(LONG_TEXT))
            {
                model.MaintItemLongText = Convert.ToString(values[LONG_TEXT]);
            }

            if (values.Contains(EXECUTION_FACTOR))
            {
                model.TaskListExecutionFactor = values[EXECUTION_FACTOR] != null ? Convert.ToInt32(values[EXECUTION_FACTOR]) : (int?)null;
            }

            if (values.Contains(DO_NOT_RELEASE))
            {
                model.MaintWorkCentreId = values[DO_NOT_RELEASE] != null ? Convert.ToInt32(values[DO_NOT_RELEASE]) : (int?)null;
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

        public Object MIRegister_GetData(DataSourceLoadOptions loadOptions)
        {
            // Oh well, this causes a loop, so will need to find waht field is causing an objhect depth to exceed 32 or be cyclic. 
            // var dataSet = _context.Tag.AsQueryable();

            var dataSet = _context.MaintItem;
                              //select new { rec.TagId, rec.TagNumber, rec.TagService, rec.TagFloc, rec.TagFlocDesc, rec.EngDiscId, rec.SubSystem.SubSystemNum };
                          

            return DataSourceLoader.Load(dataSet, loadOptions);
        }

        [Authorize(Roles = "User")]
        [HttpPost]
        public IActionResult MIRegister_Insert(string values)
        {
            // added below to remove 'MaintItemId=\"xx\",' as ID will cause error in MaintPlan on add.
            // this shouldnt happen... Add should ignore PK field(?) to investigate. 
            if (values.Contains("MaintItemId"))
            {
                var startpoint = values.IndexOf(",") + 1;
                values = "{" + values.Substring(startpoint);
            }

            var rec = new MaintItem();
            JsonConvert.PopulateObject(values, rec);

            // unique constraint on MaintItemNum. Append CLONE x to keep unique. 
            var count = _context.MaintItem
                        .Where(x => x.MaintItemNum.StartsWith(rec.MaintItemNum))
                        .Count();

            rec.MaintItemNum = (rec.MaintItemNum + " CLONE " + count.ToString("00")).Trim();

            if (!TryValidateModel(rec))
                return BadRequest();

            _context.MaintItem.Add(rec);
            _context.SaveChanges();

            return Ok(rec);
        }

        [Authorize(Roles = "User")]
        [HttpPut]
        public IActionResult MIRegister_Update(int key, string values)
        {
            // TODO override to update tag state. 
            var order = _context.MaintItem.First(o => o.MaintItemId == key);
            JsonConvert.PopulateObject(values, order);

            if (!TryValidateModel(order))
                return BadRequest();

            _context.SaveChanges();

            return Ok(order);
        }

        [Authorize(Roles = "User")]
        [HttpDelete]
        public void MIRegister_Delete(int key)
        {
            var order = _context.MaintItem.First(o => o.MaintItemId == key);

            //Dont remove it if has children.
            var children = _context.MaintItem
                .Where(x => x.MaintPlanId == order.MaintPlanId).Count();

            if (children < 1)
            {
                _context.MaintItem.Remove(order);
                _context.SaveChanges();
            }
        }

        [Authorize(Roles = "User")]
        [HttpPost]
        public object MIRegister_Batch(List<DataChange> changes)
        {
            foreach (var change in changes)
            {
                MaintItem order;

                if (change.Type == "update" || change.Type == "remove")
                {
                    var key = Convert.ToInt32(change.Key);
                    order = _context.MaintItem.First(o => o.MaintItemId == key);
                }
                else
                {
                    order = new MaintItem();
                }

                if (change.Type == "insert" || change.Type == "update")
                {
                    JsonConvert.PopulateObject(change.Data.ToString(), order);

                    if (!TryValidateModel(order))
                        return BadRequest();

                    if (change.Type == "insert")
                    {
                        _context.MaintItem.Add(order);
                    }
                    change.Data = order;
                }
                else if (change.Type == "remove")
                {
                    _context.MaintItem.Remove(order);
                }
            }

            _context.SaveChanges();

            return Ok(changes);
        }

        #endregion

        public IActionResult Excel(string currentFilter)
        {
            // Retrieve all MaintItems into recordset. State which star models to include.
            var miModel = (from t in _context.MaintItem
                                .Include(t => t.HeaderFloc)
                                .Include(t => t.MaintPlannerGroup)
                                .Include(t => t.MaintWorkCentre)
                                .Include(t => t.MaintPlan)
                                .Include(t => t.MaintenancePlant)
                                .Include(t => t.SysCond)
                                .Include(t => t.Priority)
                                .Include(t => t.FlocXmaintItems)
                                .Include(t => t.MaintItemXmaintTaskListHeaders)
                                
                            select t).AsQueryable();

            var MIMeta = _provider.GetMetadataForType(typeof(Models.MaintItem));

            using (var workbook = new XLWorkbook())
            {
                IXLWorksheet worksheet = workbook.Worksheets.Add("MaintItems");
                int currentRow = 1;

                // fieldNames to ignore.
                ICollection<string> ignoreFields = new System.Collections.ObjectModel.Collection<string>();

                //InverseMaintParents takes long time to query as each Tag hashes whole table to get parent.
                ignoreFields.Add("InverseMaintParents");

                // Fields to export = distinct set of fieldnames in ColumnSets, so is controlled by Admin table. 
                List<string> fieldsToExport = new List<string> { };
                var tmp = _context.ColumnSets
                                  .Where(x => x.ColumnSetsEntity == "MaintItem")
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

                worksheet.Cell(currentRow, i).Value = "NEW_MINumber";

                // #####################
                // ##  Export Values  ##
                // #####################
                ExpressionGet t = new ExpressionGet();
                foreach (Models.MaintItem maintItem in miModel)
                {
                    currentRow++;
                    i = 1;
                    foreach (string field in fields)
                    {
                        //if (field.Contains("Systems") && tag.SubSystem != null)
                        //    Debug.Print("hold.");

                        var RetVal = t.getChildPropertyValue(field, maintItem);
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
                        "MaintItemRegister.xlsx");
                }
            }
        }

        public ActionResult ExportToExcel()
        {
            return View();
        }

    }

}

