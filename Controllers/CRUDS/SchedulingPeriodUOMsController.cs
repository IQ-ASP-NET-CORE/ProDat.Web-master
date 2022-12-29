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
    public class SchedulingPeriodUOMsController : Controller
    {
        private readonly TagContext _context;

        public SchedulingPeriodUOMsController(TagContext context)
        {
            _context = context;
        }

        // GET: SchedulingPeriodUOMs

        public async Task<IActionResult> Index(string columnSetsName = "Default")
        {
            Dictionary<string, ColParams> colIndex = new Dictionary<string, ColParams>();
            var col_customisations = _context.ColumnSets
                                        .Where(x => x.ColumnSetsEntity == "SchedulingPeriodUOM")
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
                           .Where(x => x.EntityName == "SchedulingPeriodUOM")
                           .Include(x => x.EntityAttributeRequirements);

            ViewData["SapValidationJson"] = JsonConvert.SerializeObject(EAId);

            // ViewBag required to update GridView properties and maintain state for selected view
            ViewBag.colIndex = colIndex;
            ViewBag.columnSetsName = columnSetsName;

            return View();
        }

        private void PopulateModel(SchedulingPeriodUOM model, IDictionary values)
        {
            string SchedulingPeriodUOM_ID = nameof(SchedulingPeriodUOM.SchedulingPeriodUOMId);
            string SchedulingPeriodUOM_NAME = nameof(SchedulingPeriodUOM.SchedulingPeriodUOMName);
            string SchedulingPeriodUOM_DESC = nameof(SchedulingPeriodUOM.SchedulingPeriodUOMDesc);

            if (values.Contains(SchedulingPeriodUOM_ID))
            {
                model.SchedulingPeriodUOMId = Convert.ToInt32(values[SchedulingPeriodUOM_ID]);
            }

            if (values.Contains(SchedulingPeriodUOM_NAME))
            {
                model.SchedulingPeriodUOMName = Convert.ToString(values[SchedulingPeriodUOM_NAME]);
            }

            if (values.Contains(SchedulingPeriodUOM_DESC))
            {
                model.SchedulingPeriodUOMDesc = Convert.ToString(values[SchedulingPeriodUOM_DESC]);
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

        public Object SchedulingPeriodUOMs_GetData(DataSourceLoadOptions loadOptions)
        {
            // Oh well, this causes a loop, so will need to find waht field is causing an objhect depth to exceed 32 or be cyclic. 
            // var dataSet = _context.Tag.AsQueryable();

            var dataSet = from rec in _context.SchedulingPeriodUOM
                              //select new { rec.TagId, rec.TagNumber, rec.TagService, rec.TagFloc, rec.TagFlocDesc, rec.EngDiscId, rec.SubSystem.SubSystemNum };
                          select new { rec.SchedulingPeriodUOMId, rec.SchedulingPeriodUOMName, rec.SchedulingPeriodUOMDesc };

            return DataSourceLoader.Load(dataSet, loadOptions);
        }

        [HttpPost]
        public IActionResult SchedulingPeriodUOMs_Insert(string values)
        {
            var newSchedulingPeriodUOM = new SchedulingPeriodUOM();
            JsonConvert.PopulateObject(values, newSchedulingPeriodUOM);

            if (!TryValidateModel(newSchedulingPeriodUOM))
                return BadRequest();

            _context.SchedulingPeriodUOM.Add(newSchedulingPeriodUOM);
            _context.SaveChanges();

            return Ok(newSchedulingPeriodUOM);
        }


        [HttpPut]
        public IActionResult SchedulingPeriodUOMs_Update(int key, string values)
        {
            // TODO override to update tag state. 
            var order = _context.SchedulingPeriodUOM.First(o => o.SchedulingPeriodUOMId == key);
            JsonConvert.PopulateObject(values, order);

            if (!TryValidateModel(order))
                return BadRequest();

            _context.SaveChanges();

            return Ok(order);
        }

        [HttpDelete]
        public void SchedulingPeriodUOMs_Delete(int key)
        {
            var order = _context.SchedulingPeriodUOM.First(o => o.SchedulingPeriodUOMId == key);
            _context.SchedulingPeriodUOM.Remove(order);
            _context.SaveChanges();
        }

        [HttpPost]
        public object SchedulingPeriodUOMs_Batch(List<DataChange> changes)
        {
            foreach (var change in changes)
            {
                SchedulingPeriodUOM order;

                if (change.Type == "update" || change.Type == "remove")
                {
                    var key = Convert.ToInt32(change.Key);
                    order = _context.SchedulingPeriodUOM.First(o => o.SchedulingPeriodUOMId == key);
                }
                else
                {
                    order = new SchedulingPeriodUOM();
                }

                if (change.Type == "insert" || change.Type == "update")
                {
                    JsonConvert.PopulateObject(change.Data.ToString(), order);

                    if (!TryValidateModel(order))
                        return BadRequest();

                    if (change.Type == "insert")
                    {
                        _context.SchedulingPeriodUOM.Add(order);
                    }
                    change.Data = order;
                }
                else if (change.Type == "remove")
                {
                    _context.SchedulingPeriodUOM.Remove(order);
                }
            }

            _context.SaveChanges();

            return Ok(changes);
        }
        public ActionResult ExportToExcel()
        {
            return View();
        }
        
        public Boolean ValidateName(SchedulingPeriodUOM schedperiod)
        {
            var valid = _context.SchedulingPeriodUOM.Any(x => x.SchedulingPeriodUOMName == schedperiod.SchedulingPeriodUOMName);
            return !valid;
        }
    }
}
