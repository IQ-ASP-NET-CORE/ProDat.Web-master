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
    public class ExMethodsController : Controller
    {
        private readonly TagContext _context;

        public ExMethodsController(TagContext context)
        {
            _context = context;
        }

        // GET: ExMethods

        public IActionResult Index(string columnSetsName = "Default")
        {
            Dictionary<string, ColParams> colIndex = new Dictionary<string, ColParams>();
            var col_customisations = _context.ColumnSets
                                        .Where(x => x.ColumnSetsEntity == "ExMethod")
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
                           .Where(x => x.EntityName == "ExMethod")
                           .Include(x => x.EntityAttributeRequirements);

            ViewData["SapValidationJson"] = JsonConvert.SerializeObject(EAId);

            // ViewBag required to update GridView properties and maintain state for selected view
            ViewBag.colIndex = colIndex;
            ViewBag.columnSetsName = columnSetsName;

            return View();
        }

        private void PopulateModel(ExMethod model, IDictionary values)
        {
            string ExMethod_ID = nameof(ExMethod.ExMethodId);
            string ExMethod_NAME = nameof(ExMethod.ExMethodName);
            string ExMethod_DESC = nameof(ExMethod.ExMethodDesc);

            if (values.Contains(ExMethod_ID))
            {
                model.ExMethodId = Convert.ToInt32(values[ExMethod_ID]);
            }

            if (values.Contains(ExMethod_NAME))
            {
                model.ExMethodName = Convert.ToString(values[ExMethod_NAME]);
            }

            if (values.Contains(ExMethod_DESC))
            {
                model.ExMethodDesc = Convert.ToString(values[ExMethod_DESC]);
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

        public Object ExMethods_GetData(DataSourceLoadOptions loadOptions)
        {
            // Oh well, this causes a loop, so will need to find waht field is causing an objhect depth to exceed 32 or be cyclic. 
            // var dataSet = _context.Tag.AsQueryable();

            var dataSet = from rec in _context.ExMethod
                              //select new { rec.TagId, rec.TagNumber, rec.TagService, rec.TagFloc, rec.TagFlocDesc, rec.EngDiscId, rec.SubSystem.SubSystemNum };
                          select new { rec.ExMethodId, rec.ExMethodName, rec.ExMethodDesc };

            return DataSourceLoader.Load(dataSet, loadOptions);
        }

        [HttpPost]
        public IActionResult ExMethods_Insert(string values)
        {
            var newExMethod = new ExMethod();
            JsonConvert.PopulateObject(values, newExMethod);

            if (!TryValidateModel(newExMethod))
                return BadRequest();

            _context.ExMethod.Add(newExMethod);
            _context.SaveChanges();

            return Ok(newExMethod);
        }


        [HttpPut]
        public IActionResult ExMethods_Update(int key, string values)
        {
            // TODO override to update tag state. 
            var order = _context.ExMethod.First(o => o.ExMethodId == key);
            JsonConvert.PopulateObject(values, order);

            if (!TryValidateModel(order))
                return BadRequest();

            _context.SaveChanges();

            return Ok(order);
        }

        [HttpDelete]
        public void ExMethods_Delete(int key)
        {
            var order = _context.ExMethod.First(o => o.ExMethodId == key);
            _context.ExMethod.Remove(order);
            _context.SaveChanges();
        }

        [HttpPost]
        public object ExMethods_Batch(List<DataChange> changes)
        {
            foreach (var change in changes)
            {
                ExMethod order;

                if (change.Type == "update" || change.Type == "remove")
                {
                    var key = Convert.ToInt32(change.Key);
                    order = _context.ExMethod.First(o => o.ExMethodId == key);
                }
                else
                {
                    order = new ExMethod();
                }

                if (change.Type == "insert" || change.Type == "update")
                {
                    JsonConvert.PopulateObject(change.Data.ToString(), order);

                    if (!TryValidateModel(order))
                        return BadRequest();

                    if (change.Type == "insert")
                    {
                        _context.ExMethod.Add(order);
                    }
                    change.Data = order;
                }
                else if (change.Type == "remove")
                {
                    _context.ExMethod.Remove(order);
                }
            }

            _context.SaveChanges();

            return Ok(changes);
        }
        public ActionResult ExportToExcel()
        {
            return View();
        }
        public Boolean ValidateName(ExMethod exmethod)
        {
            var valid = _context.ExMethod.Any(x => x.ExMethodName == exmethod.ExMethodName);
            return !valid;
        }
    }
}
