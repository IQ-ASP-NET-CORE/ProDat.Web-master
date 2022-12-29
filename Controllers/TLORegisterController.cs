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
     * Provides functionality to manage TLORegister. 
     * Note: Star lookup data sources are defined in Lookups Controller.
     */

    public class TLORegisterController : Controller
    {
        private readonly TagContext _context;
        private readonly ILogger<TagContext> _logger;
        private readonly IModelMetadataProvider _provider;
        private IConfiguration _configuration;

        public TLORegisterController(TagContext context, ILogger<TagContext> logger, IModelMetadataProvider provider, IConfiguration Configuration)
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
                                        .Where(x=> x.ColumnSetsEntity == "TaskListOperations")
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
                           .Where(x => x.EntityName == "TaskListOperations")
                           .Include(x => x.EntityAttributeRequirements);

            ViewData["SapValidationJson"] = JsonConvert.SerializeObject(EAId);

            // ViewBag required to update GridView properties and maintain state for selected view
            ViewBag.colIndex = colIndex;
            ViewBag.columnSetsName = columnSetsName;
            ViewBag.GlobalProjectDescription = _context.Project.First().ProjectName;
            return View();
        }



        #region Create new TLO with a Form View. 

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
            var model = new TaskListOperations();
            var valuesDict = JsonConvert.DeserializeObject<IDictionary>(values);
            PopulateModel(model, valuesDict);

            if (!TryValidateModel(model))
                return BadRequest(GetFullErrorMessage(ModelState));

            var result = _context.TaskListOperations.Add(model);
            await _context.SaveChangesAsync();

            return Json(new { result.Entity.TaskListOperationId });
        }


        private void PopulateModel(TaskListOperations model, IDictionary values)
        {
            string ID = nameof(TaskListOperations.TaskListOperationId);
            string TLH_ID = nameof(TaskListOperations.TaskListHeaderId);
            string NUM = nameof(TaskListOperations.OperationNum);
            string SUB_NUM = nameof(TaskListOperations.SubOperationNum);
            string OPERATION_ID = nameof(TaskListOperations.OperationId);
            string OP_DESC = nameof(TaskListOperations.OperationDescription);
            string MAINT_WORKCENTRE_ID = nameof(TaskListOperations.MaintWorkCentreId);
            string MAINT_PLANT_ID = nameof(TaskListOperations.MaintenancePlantId);
            string CONTROLKEY_ID = nameof(TaskListOperations.ControlKeyId);
            string SYSCOND_ID = nameof(TaskListOperations.SysCondId);
            string REL_TO_OPERATION_ID = nameof(TaskListOperations.RelationshiptoOperationId);
            string OP_SHORT_TEXT = nameof(TaskListOperations.OperationShortText);
            string OP_LONG_TEXT = nameof(TaskListOperations.OperationLongText);
            string WORK_HRS = nameof(TaskListOperations.WorkHrs);
            string CAP_NO = nameof(TaskListOperations.CapNo);
            string MAINT_PACKAGE_ID = nameof(TaskListOperations.MaintPackageId);
            string DOC_ID = nameof(TaskListOperations.DocId);
            string TI = nameof(TaskListOperations.Ti);
            string OFFSITE = nameof(TaskListOperations.OffSite);
            string FIX_QTY = nameof(TaskListOperations.FixedOperQty);
            string CHANGE_REQD = nameof(TaskListOperations.ChangeRequired);
            

            if (values.Contains(ID))
            {
                model.TaskListOperationId = Convert.ToInt32(values[ID]);
            }

            if (values.Contains(TLH_ID))
            {
                model.TaskListHeaderId = values[TLH_ID] != null ? Convert.ToInt32(values[TLH_ID]) : (int?)null;
            }

            if (values.Contains(NUM))
            {
                model.OperationNum = Convert.ToInt32(values[NUM]);
            }

            if (values.Contains(SUB_NUM))
            {
                model.SubOperationNum = Convert.ToInt32(values[SUB_NUM]);
            }

            if (values.Contains(OPERATION_ID))
            {
                model.OperationId = values[OPERATION_ID] != null ? Convert.ToInt32(values[OPERATION_ID]) : (int?)null;
            }

            if (values.Contains(MAINT_WORKCENTRE_ID))
            {
                model.OperationId = values[MAINT_WORKCENTRE_ID] != null ? Convert.ToInt32(values[MAINT_WORKCENTRE_ID]) : (int?)null;
            }

            if (values.Contains(MAINT_PLANT_ID))
            {
                model.MaintenancePlantId = values[MAINT_PLANT_ID] != null ? Convert.ToInt32(values[MAINT_PLANT_ID]) : (int?)null;
            }

            if (values.Contains(CONTROLKEY_ID))
            {
                model.ControlKeyId = values[CONTROLKEY_ID] != null ? Convert.ToInt32(values[CONTROLKEY_ID]) : (int?)null;
            }

            if (values.Contains(SYSCOND_ID))
            {
                model.SysCondId = values[SYSCOND_ID] != null ? Convert.ToInt32(values[SYSCOND_ID]) : (int?)null;
            }

            if (values.Contains(REL_TO_OPERATION_ID))
            {
                model.RelationshiptoOperationId = values[REL_TO_OPERATION_ID] != null ? Convert.ToInt32(values[REL_TO_OPERATION_ID]) : (int?)null;
            }

            if (values.Contains(OP_SHORT_TEXT))
            {
                model.OperationShortText = Convert.ToString(values[OP_SHORT_TEXT]);
            }

            if (values.Contains(OP_LONG_TEXT))
            {
                model.OperationLongText = Convert.ToString(values[OP_LONG_TEXT]);
            }

            if (values.Contains(WORK_HRS))
            {
                model.OperationLongText = Convert.ToString(values[WORK_HRS]);
            }

            if (values.Contains(CAP_NO))
            {
                model.CapNo = values[CAP_NO] != null ? Convert.ToInt32(values[CAP_NO]) : (int?)null;
            }

            if (values.Contains(MAINT_PACKAGE_ID))
            {
                model.MaintPackageId = values[MAINT_PACKAGE_ID] != null ? Convert.ToInt32(values[MAINT_PACKAGE_ID]) : (int?)null;
            }

            if (values.Contains(DOC_ID))
            {
                model.DocId = values[DOC_ID] != null ? Convert.ToInt32(values[DOC_ID]) : (int?)null;
            }

            if (values.Contains(TI))
            {
                model.Ti = Convert.ToBoolean(values[TI]);
            }

            if (values.Contains(OFFSITE))
            {
                model.OffSite = Convert.ToBoolean(values[OFFSITE]);
            }

            if (values.Contains(FIX_QTY))
            {
                model.FixedOperQty = values[FIX_QTY] != null ? Convert.ToInt32(values[FIX_QTY]) : (int?)null;
            }

            if (values.Contains(CHANGE_REQD))
            {
                model.ChangeRequired = Convert.ToString(values[CHANGE_REQD]);
            }

            if (values.Contains(OP_DESC))
            {
                model.OperationDescription = Convert.ToString(values[OP_DESC]);
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

        public Object TLORegister_GetData(DataSourceLoadOptions loadOptions)
        {
            var dataSet = _context.TaskListOperations;

            return DataSourceLoader.Load(dataSet, loadOptions);
        }

        [Authorize(Roles = "User")]
        [HttpPost]
        public IActionResult TLORegister_Insert(string values)
        {
            if (values.Contains("TaskListOperationId"))
            {
                // strip out pk... TODO: Update PopulateObject to discard PK with optional flag (isClone=T|F)
                var startpoint = values.IndexOf(",") + 1;
                values = "{" + values.Substring(startpoint);
            }

            var newObj = new TaskListOperations();
            JsonConvert.PopulateObject(values, newObj);

            // unique insert? based on what? Operation Num?
            var count = _context.TaskListOperations
                        .Where(x => x.TaskListHeaderId == newObj.TaskListHeaderId)
                        .Select(x=> x.OperationNum).Max();

            // if count is null test?
            newObj.OperationNum = count + 1;

            if (!TryValidateModel(newObj))
                return BadRequest();

            _context.TaskListOperations.Add(newObj);
            _context.SaveChanges();

            return Ok(newObj);
        }

        [Authorize(Roles = "User")]
        [HttpPut]
        public IActionResult TLORegister_Update(int key, string values)
        {
            var order = _context.TaskListOperations.First(o => o.TaskListOperationId == key);
            JsonConvert.PopulateObject(values, order);

            if (!TryValidateModel(order))
                return BadRequest();

            _context.SaveChanges();

            return Ok(order);
        }

        [Authorize(Roles = "User")]
        [HttpDelete]
        public void TLORegister_Delete(int key)
        {
            var order = _context.TaskListOperations.First(o => o.TaskListOperationId == key);
            _context.TaskListOperations.Remove(order);
            _context.SaveChanges();

        }

        [Authorize(Roles = "User")]
        [HttpPost]
        public object TLORegister_Batch(List<DataChange> changes)
        {
            foreach (var change in changes)
            {
                TaskListOperations order;

                if (change.Type == "update" || change.Type == "remove")
                {
                    var key = Convert.ToInt32(change.Key);
                    order = _context.TaskListOperations.First(o => o.TaskListOperationId == key);
                }
                else
                {
                    order = new TaskListOperations();
                }

                if (change.Type == "insert" || change.Type == "update")
                {
                    JsonConvert.PopulateObject(change.Data.ToString(), order);

                    if (!TryValidateModel(order))
                        return BadRequest();

                    if (change.Type == "insert")
                    {
                        _context.TaskListOperations.Add(order);
                    }
                    change.Data = order;
                }
                else if (change.Type == "remove")
                {
                    _context.TaskListOperations.Remove(order);
                }
            }

            _context.SaveChanges();

            return Ok(changes);
        }

        #endregion



        #region TLO Component Data Management

        [HttpGet]
        public object TaskListOperationsDataGrid_GetData(DataSourceLoadOptions loadOptions, int? parent)
        {
            if (parent == null)
                parent = 0;
            var dataSet = _context.TaskListOperations
                          .Where(rec => rec.TaskListHeaderId == parent);
                          //.OrderBy(x => x.OperationShortText)
                          //.Select(rec => new { rec.TaskListHeaderId, rec.TaskListOperationId, rec.OperationShortText, rec.OperationNum, rec.DocId, rec.MaintWorkCentreId });

            return DataSourceLoader.Load(dataSet, loadOptions);
        }

        #endregion

        
        public IActionResult Excel(string currentFilter)
        {
            // Retrieve all TLH into recordset. State which star models to include.
            var tagModel = (from t in _context.TaskListOperations
                                .Include(t => t.ControlKey)
                                .Include(t => t.Doc)
                                .Include(t => t.MaintPackage)
                                .Include(t => t.MaintWorkCentre)
                                .Include(t => t.MaintenancePlant)
                                .Include(t => t.Operation)
                                .Include(t => t.RelationshiptoOperation)
                                .Include(t => t.SysCond)
                                .Include(t => t.TaskListHeader)
                                    .ThenInclude(u=> u.TaskListGroup)

                            select t).AsQueryable();

            var TagMeta = _provider.GetMetadataForType(typeof(TaskListOperations));

            using (var workbook = new XLWorkbook())
            {
                IXLWorksheet worksheet = workbook.Worksheets.Add("TLO");
                int currentRow = 1;

                // fieldNames to ignore.
                ICollection<string> ignoreFields = new System.Collections.ObjectModel.Collection<string>();

                // Fields to export = distinct set of fieldnames in ColumnSets, so is controlled by Admin table. 
                List<string> fieldsToExport = new List<string> { };
                var tmp = _context.ColumnSets
                                  .Where(x => x.ColumnSetsEntity == "TaskListOperations")
                                  .Select(x => x.ColumnName);

                fieldsToExport = (List<string>)tmp.Distinct().ToList();
                fieldsToExport.Remove("TaskListGroupName");

                // strip Id from FieldName. We'll dig into these entities to retrieve Num or Name attribute.
                for (int j = 0; j < fieldsToExport.Count(); j++)
                    if (fieldsToExport[j].EndsWith("Id"))
                        fieldsToExport[j] = fieldsToExport[j].Substring(0, fieldsToExport[j].Length - 2);

                // build header from metadata.
                int i = 1;
                ICollection<string> fields = new System.Collections.ObjectModel.Collection<string>();
                fields.Add("TaskListOperationName");
                worksheet.Cell(currentRow, i++).Value = "TaskListOperationName";


                foreach (var property in TagMeta.Properties)
                {
                    if (ignoreFields.Contains(property.Name) || property.Name.EndsWith("Id"))
                        continue;

                    // If not basic attribute, it is an entity. 
                    // Drill into entity and retrieve property ending with Num, else Name.

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

                            /* TODO: 
                                    If TLH/TLO can have their Counter or TaskListGroup altered, The below is best solution. 
                                    Name is transient for import only and pk is used by Historian.
                                    
                                    If TLO Unique names are required outside of Prodat, then once created, a TLH and TLO should not be able to have their group or counter altered! 
                                    Also, unique names become the best solution:
                                        Unique names for all entities, stored within entity, as EntityName+Name. e.g. TaskLisHeaderName
                                        This would allow for use of Name/Num as originally planned!
                                        note: this causes renaming problems if constituients that make up inteligent name can be changed. 
                            */
                           
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

                worksheet.Cell(currentRow, i).Value = "NEW_TLONumber";

                // #####################
                // ##  Export Values  ##
                // #####################
                ExpressionGet t = new ExpressionGet();
                foreach (TaskListOperations tag in tagModel)
                {
                    currentRow++;
                    i = 1;
                    foreach (string field in fields)
                    {
                        if (field == "TaskListOperationName")
                        {
                            // output TaskListName + Counter + OperationNum
                            var tlgName = ( tag.TaskListHeader != null && tag.TaskListHeader.TaskListGroup != null) ? tag.TaskListHeader.TaskListGroup.TaskListGroupName: null;
                            var tlCounter = (tag.TaskListHeader != null) ? tag.TaskListHeader.Counter.ToString().PadLeft(2, '0') : "00";
                            var tloCounter = tag.OperationNum.ToString().PadLeft(2, '0');
                            string retVal = "";
                            if (tlgName != null)
                            {
                                retVal = tlgName + "-" + tlCounter + "-" + tloCounter;
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
                        "TLORegister.xlsx");
                }
            }
        }

        
        public ActionResult ExportToExcel()
        {
            return View();
        }

    } //TLORegister Controller

} // Namespace

