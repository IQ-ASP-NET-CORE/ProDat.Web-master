using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Mvc;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using ProDat.Web2.Data;
using ProDat.Web2.Models;
using ProDat.Web2.Models.DataGrid;
using ProDat.Web2.ViewModels;

namespace ProDat.Web2.Controllers.CRUDS
{
    public class PerformanceStandardsController : Controller
    {
        private readonly TagContext _context;

        public PerformanceStandardsController(TagContext context)
        {
            _context = context;
        }

        // GET: PerformanceStandards

        public async Task<IActionResult> Index(string columnSetsName = "Default")
        {
            Dictionary<string, ColParams> colIndex = new Dictionary<string, ColParams>();
            var col_customisations = _context.ColumnSets
                                        .Where(x => x.ColumnSetsEntity == "PerformanceStandard")
                                        .Where(x => x.ColumnSetsName == columnSetsName)
                                        .Select(x => new
                                        {
                                            x.ColumnName
                                                            ,
                                            x.ColumnOrder
                                                            ,
                                            x.ColumnWidth
                                                            ,
                                            x.ColumnVisible
                                        }
                                               );
            foreach (var cust in col_customisations)
            {
                colIndex.Add(cust.ColumnName, new ColParams(cust.ColumnOrder, cust.ColumnWidth));
            }

            // SAP Validation 
            var EAId = _context.EntityAttribute
                           .Where(x => x.EntityName == "PerformanceStandard")
                           .Include(x => x.EntityAttributeRequirements);

            ViewData["SapValidationJson"] = JsonConvert.SerializeObject(EAId);

            // ViewBag required to update GridView properties and maintain state for selected view
            ViewBag.colIndex = colIndex;
            ViewBag.columnSetsName = columnSetsName;

            return View();
        }

        private void PopulateModel(PerformanceStandard model, IDictionary values)
        {
            string PerformanceStandard_ID = nameof(PerformanceStandard.PerformanceStandardId);
            string PerformanceStandard_NAME = nameof(PerformanceStandard.PerformanceStandardName);
            string PerformanceStandard_DESC = nameof(PerformanceStandard.PerformanceStandardDesc);

            if (values.Contains(PerformanceStandard_ID))
            {
                model.PerformanceStandardId = Convert.ToInt32(values[PerformanceStandard_ID]);
            }

            if (values.Contains(PerformanceStandard_NAME))
            {
                model.PerformanceStandardName = Convert.ToString(values[PerformanceStandard_NAME]);
            }

            if (values.Contains(PerformanceStandard_DESC))
            {
                model.PerformanceStandardDesc = Convert.ToString(values[PerformanceStandard_DESC]);
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

        public Object PerformanceStandards_GetData(DataSourceLoadOptions loadOptions)
        {
            // Oh well, this causes a loop, so will need to find waht field is causing an objhect depth to exceed 32 or be cyclic. 
            // var dataSet = _context.Tag.AsQueryable();

            var dataSet = from rec in _context.PerformanceStandard
                              //select new { rec.TagId, rec.TagNumber, rec.TagService, rec.TagFloc, rec.TagFlocDesc, rec.EngDiscId, rec.SubSystem.SubSystemNum };
                          select new { rec.PerformanceStandardId, rec.PerformanceStandardName, rec.PerformanceStandardDesc };

            return DataSourceLoader.Load(dataSet, loadOptions);
        }

        [HttpPost]
        public IActionResult PerformanceStandards_Insert(string values)
        {
            var newPerformanceStandard = new PerformanceStandard();
            JsonConvert.PopulateObject(values, newPerformanceStandard);

            if (!TryValidateModel(newPerformanceStandard))
                return BadRequest();

            _context.PerformanceStandard.Add(newPerformanceStandard);
            _context.SaveChanges();

            return Ok(newPerformanceStandard);
        }


        [HttpPut]
        public IActionResult PerformanceStandards_Update(int key, string values)
        {
            // TODO override to update tag state. 
            var order = _context.PerformanceStandard.First(o => o.PerformanceStandardId == key);
            JsonConvert.PopulateObject(values, order);

            if (!TryValidateModel(order))
                return BadRequest();

            _context.SaveChanges();

            return Ok(order);
        }

        [HttpDelete]
        public void PerformanceStandards_Delete(int key)
        {
            var order = _context.PerformanceStandard.First(o => o.PerformanceStandardId == key);
            _context.PerformanceStandard.Remove(order);
            _context.SaveChanges();
        }

        [HttpPost]
        public object PerformanceStandards_Batch(List<DataChange> changes)
        {
            foreach (var change in changes)
            {
                PerformanceStandard order;

                if (change.Type == "update" || change.Type == "remove")
                {
                    var key = Convert.ToInt32(change.Key);
                    order = _context.PerformanceStandard.First(o => o.PerformanceStandardId == key);
                }
                else
                {
                    order = new PerformanceStandard();
                }

                if (change.Type == "insert" || change.Type == "update")
                {
                    JsonConvert.PopulateObject(change.Data.ToString(), order);

                    if (!TryValidateModel(order))
                        return BadRequest();

                    if (change.Type == "insert")
                    {
                        _context.PerformanceStandard.Add(order);
                    }
                    change.Data = order;
                }
                else if (change.Type == "remove")
                {
                    _context.PerformanceStandard.Remove(order);
                }
            }

            _context.SaveChanges();

            return Ok(changes);
        }
        public ActionResult ExportToExcel()
        {
            return View();
        }

        public Boolean ValidateName(PerformanceStandard performancestandard)
        {
            var valid = _context.PerformanceStandard.Any(x => x.PerformanceStandardName == performancestandard.PerformanceStandardName);
            return !valid;
        }
    }
}
