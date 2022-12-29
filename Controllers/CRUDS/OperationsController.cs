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
    public class OperationsController : Controller
    {
        private readonly TagContext _context;

        public OperationsController(TagContext context)
        {
            _context = context;
        }

        // GET: Operations

        public async Task<IActionResult> Index(string columnSetsName = "Default")
        {
            Dictionary<string, ColParams> colIndex = new Dictionary<string, ColParams>();
            var col_customisations = _context.ColumnSets
                                        .Where(x => x.ColumnSetsEntity == "Operation")
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
                           .Where(x => x.EntityName == "Operation")
                           .Include(x => x.EntityAttributeRequirements);

            ViewData["SapValidationJson"] = JsonConvert.SerializeObject(EAId);

            // ViewBag required to update GridView properties and maintain state for selected view
            ViewBag.colIndex = colIndex;
            ViewBag.columnSetsName = columnSetsName;

            return View();
        }

        private void PopulateModel(Operation model, IDictionary values)
        {
            string Operation_ID = nameof(Operation.OperationId);
            string Operation_NAME = nameof(Operation.OperationName);
            string Operation_Notes = nameof(Operation.OperationNotes);

            if (values.Contains(Operation_ID))
            {
                model.OperationId = Convert.ToInt32(values[Operation_ID]);
            }

            if (values.Contains(Operation_NAME))
            {
                model.OperationName = Convert.ToString(values[Operation_NAME]);
            }

            if (values.Contains(Operation_Notes))
            {
                model.OperationNotes = Convert.ToString(values[Operation_Notes]);
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

        public Object Operations_GetData(DataSourceLoadOptions loadOptions)
        {
            // Oh well, this causes a loop, so will need to find waht field is causing an objhect depth to exceed 32 or be cyclic. 
            // var dataSet = _context.Tag.AsQueryable();

            var dataSet = from rec in _context.Operation
                              //select new { rec.TagId, rec.TagNumber, rec.TagService, rec.TagFloc, rec.TagFlocNotes, rec.EngDiscId, rec.SubSystem.SubSystemNum };
                          select new { rec.OperationId, rec.OperationName, rec.OperationNotes };

            return DataSourceLoader.Load(dataSet, loadOptions);
        }

        [HttpPost]
        public IActionResult Operations_Insert(string values)
        {
            var newOperation = new Operation();
            JsonConvert.PopulateObject(values, newOperation);

            if (!TryValidateModel(newOperation))
                return BadRequest();

            _context.Operation.Add(newOperation);
            _context.SaveChanges();

            return Ok(newOperation);
        }


        [HttpPut]
        public IActionResult Operations_Update(int key, string values)
        {
            // TODO override to update tag state. 
            var order = _context.Operation.First(o => o.OperationId == key);
            JsonConvert.PopulateObject(values, order);

            if (!TryValidateModel(order))
                return BadRequest();

            _context.SaveChanges();

            return Ok(order);
        }

        [HttpDelete]
        public void Operations_Delete(int key)
        {
            var order = _context.Operation.First(o => o.OperationId == key);
            _context.Operation.Remove(order);
            _context.SaveChanges();
        }

        [HttpPost]
        public object Operations_Batch(List<DataChange> changes)
        {
            foreach (var change in changes)
            {
                Operation order;

                if (change.Type == "update" || change.Type == "remove")
                {
                    var key = Convert.ToInt32(change.Key);
                    order = _context.Operation.First(o => o.OperationId == key);
                }
                else
                {
                    order = new Operation();
                }

                if (change.Type == "insert" || change.Type == "update")
                {
                    JsonConvert.PopulateObject(change.Data.ToString(), order);

                    if (!TryValidateModel(order))
                        return BadRequest();

                    if (change.Type == "insert")
                    {
                        _context.Operation.Add(order);
                    }
                    change.Data = order;
                }
                else if (change.Type == "remove")
                {
                    _context.Operation.Remove(order);
                }
            }

            _context.SaveChanges();

            return Ok(changes);
        }
        public ActionResult ExportToExcel()
        {
            return View();
        }

        public Boolean ValidateName(Operation operation)
        {
            var valid = _context.Operation.Any(x => x.OperationName == operation.OperationName);
            return !valid;
        }
    }
}
