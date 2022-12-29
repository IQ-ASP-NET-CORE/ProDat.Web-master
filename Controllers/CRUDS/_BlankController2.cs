//using System;
//using System.Collections;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;
//using DevExtreme.AspNet.Data;
//using DevExtreme.AspNet.Mvc;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.AspNetCore.Mvc.ModelBinding;
//using Microsoft.EntityFrameworkCore;
//using Microsoft.Extensions.Configuration;
//using Microsoft.Extensions.Logging;
//using Newtonsoft.Json;
//using ProDat.Web2.Data;
//using ProDat.Web2.Models;
//using ProDat.Web2.Models.DataGrid;
//using ProDat.Web2.ViewModels;

//namespace ProDat.Web2.Controllers.CRUDS
//{
//    public class BlanksController : Controller
//    {
//        private readonly TagContext _context;

//        public BlanksController(TagContext context)
//        {
//            _context = context;
//        }

//        // GET: Blanks

//        public async Task<IActionResult> Index(string columnSetsName = "Default")
//        {
//            Dictionary<string, ColParams> colIndex = new Dictionary<string, ColParams>();
//            var col_customisations = _context.ColumnSets
//                                        .Where(x => x.ColumnSetsEntity == "Blank")
//                                        .Where(x => x.ColumnSetsName == columnSetsName)
//                                        .Select(x => new
//                                        {
//                                            x.ColumnName
//                                                            ,
//                                            x.ColumnOrder
//                                                            ,
//                                            x.ColumnWidth
//                                                            ,
//                                            x.ColumnVisible
//                                        }
//                                               );
//            foreach (var cust in col_customisations)
//            {
//                colIndex.Add(cust.ColumnName, new ColParams(cust.ColumnOrder, cust.ColumnWidth));
//            }

//            // SAP Validation 
//            var EAId = _context.EntityAttribute
//                           .Where(x => x.EntityName == "Blank")
//                           .Include(x => x.EntityAttributeRequirements);

//            ViewData["SapValidationJson"] = JsonConvert.SerializeObject(EAId);

//            // ViewBag required to update GridView properties and maintain state for selected view
//            ViewBag.colIndex = colIndex;
//            ViewBag.columnSetsName = columnSetsName;

//            return View();
//        }

//        private void PopulateModel(Blank model, IDictionary values)
//        {
//            string BLANK_ID = nameof(Blank.BlankId);
//            string ITEM2_ID = nameof(Blank.ITEM2Id);
//            string BLANK_NAME = nameof(Blank.BlankName);
//            string BLANK_DESC = nameof(Blank.BlankDesc);

//            if (values.Contains(Blank_ID))
//            {
//                model.BlankId = Convert.ToInt32(values[BLANK_ID]);
//            }

//            if (values.Contains(ITEM2_ID))
//            {
//                model.BlankId = Convert.ToInt32(values[ITEM2_ID]);
//            }

//            if (values.Contains(Blank_NAME))
//            {
//                model.BlankName = Convert.ToString(values[BLANK_NAME]);
//            }

//            if (values.Contains(Blank_DESC))
//            {
//                model.BlankDesc = Convert.ToString(values[BLANK_DESC]);
//            }

//        }

//        private string GetFullErrorMessage(ModelStateDictionary modelState)
//        {
//            var messages = new List<string>();

//            foreach (var entry in modelState)
//            {
//                foreach (var error in entry.Value.Errors)
//                    messages.Add(error.ErrorMessage);
//            }

//            return String.Join(" ", messages);
//        }

//        public Object Blanks_GetData(DataSourceLoadOptions loadOptions)
//        {
//            // Oh well, this causes a loop, so will need to find waht field is causing an objhect depth to exceed 32 or be cyclic. 
//            // var dataSet = _context.Tag.AsQueryable();

//            var dataSet = from rec in _context.Blank
//                              //select new { rec.TagId, rec.TagNumber, rec.TagService, rec.TagFloc, rec.TagFlocDesc, rec.EngDiscId, rec.SubSystem.SubSystemNum };
//                          select new BlankVM(rec);

//            return DataSourceLoader.Load(dataSet, loadOptions);
//        }

//        [HttpPost]
//        public IActionResult Blanks_Insert(string values)
//        {
//            var newBlank = new Blank();
//            JsonConvert.PopulateObject(values, newBlank);

//            if (!TryValidateModel(newBlank))
//                return BadRequest();

//            _context.Blank.Add(newBlank);
//            _context.SaveChanges();

//            return Ok(newBlank);
//        }


//        [HttpPut]
//        public IActionResult Blanks_Update(int key, string values)
//        {
//            // TODO override to update tag state. 
//            var order = _context.Blank.First(o => o.BlankId == key);
//            JsonConvert.PopulateObject(values, order);

//            if (!TryValidateModel(order))
//                return BadRequest();

//            _context.SaveChanges();

//            return Ok(order);
//        }

//        [HttpDelete]
//        public void Blanks_Delete(int key)
//        {
//            var order = _context.Blank.First(o => o.BlankId == key);
//            _context.Blank.Remove(order);
//            _context.SaveChanges();
//        }

//        [HttpPost]
//        public object Blanks_Batch(List<DataChange> changes)
//        {
//            foreach (var change in changes)
//            {
//                Blank order;

//                if (change.Type == "update" || change.Type == "remove")
//                {
//                    var key = Convert.ToInt32(change.Key);
//                    order = _context.Blank.First(o => o.BlankId == key);
//                }
//                else
//                {
//                    order = new Blank();
//                }

//                if (change.Type == "insert" || change.Type == "update")
//                {
//                    JsonConvert.PopulateObject(change.Data.ToString(), order);

//                    if (!TryValidateModel(order))
//                        return BadRequest();

//                    if (change.Type == "insert")
//                    {
//                        _context.Blank.Add(order);
//                    }
//                    change.Data = order;
//                }
//                else if (change.Type == "remove")
//                {
//                    _context.Blank.Remove(order);
//                }
//            }

//            _context.SaveChanges();

//            return Ok(changes);
//        }
//        public ActionResult ExportToExcel()
//        {
//            return View();
//        }
//    }
//}