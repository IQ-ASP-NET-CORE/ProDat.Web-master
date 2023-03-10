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
    public class EngStatusesController : Controller
    {
        private readonly TagContext _context;

        public EngStatusesController(TagContext context)
        {
            _context = context;
        }

        // GET: EngStatuses

        public IActionResult Index(string columnSetsName = "Default")
        {
            Dictionary<string, ColParams> colIndex = new Dictionary<string, ColParams>();
            var col_customisations = _context.ColumnSets
                                        .Where(x => x.ColumnSetsEntity == "EngStatus")
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
                           .Where(x => x.EntityName == "EngStatus")
                           .Include(x => x.EntityAttributeRequirements);

            ViewData["SapValidationJson"] = JsonConvert.SerializeObject(EAId);

            // ViewBag required to update GridView properties and maintain state for selected view
            ViewBag.colIndex = colIndex;
            ViewBag.columnSetsName = columnSetsName;

            return View();
        }

        private void PopulateModel(EngStatus model, IDictionary values)
        {
            string EngStatus_ID = nameof(EngStatus.EngStatusId);
            string EngStatus_NAME = nameof(EngStatus.EngStatusName);

            if (values.Contains(EngStatus_ID))
            {
                model.EngStatusId = Convert.ToInt32(values[EngStatus_ID]);
            }

            if (values.Contains(EngStatus_NAME))
            {
                model.EngStatusName = Convert.ToString(values[EngStatus_NAME]);
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

        public Object EngStatuses_GetData(DataSourceLoadOptions loadOptions)
        {
            // Oh well, this causes a loop, so will need to find waht field is causing an objhect depth to exceed 32 or be cyclic. 
            // var dataSet = _context.Tag.AsQueryable();

            var dataSet = from rec in _context.EngStatus
                              //select new { rec.TagId, rec.TagNumber, rec.TagService, rec.TagFloc, rec.TagFlocDesc, rec.EngDiscId, rec.SubSystem.SubSystemNum };
                          select new { rec.EngStatusId, rec.EngStatusName };

            return DataSourceLoader.Load(dataSet, loadOptions);
        }

        [HttpPost]
        public IActionResult EngStatuses_Insert(string values)
        {
            var newEngStatus = new EngStatus();
            JsonConvert.PopulateObject(values, newEngStatus);

            if (!TryValidateModel(newEngStatus))
                return BadRequest();

            _context.EngStatus.Add(newEngStatus);
            _context.SaveChanges();

            return Ok(newEngStatus);
        }


        [HttpPut]
        public IActionResult EngStatuses_Update(int key, string values)
        {
            // TODO override to update tag state. 
            var order = _context.EngStatus.First(o => o.EngStatusId == key);
            JsonConvert.PopulateObject(values, order);

            if (!TryValidateModel(order))
                return BadRequest();

            _context.SaveChanges();

            return Ok(order);
        }

        [HttpDelete]
        public void EngStatuses_Delete(int key)
        {
            var order = _context.EngStatus.First(o => o.EngStatusId == key);
            _context.EngStatus.Remove(order);
            _context.SaveChanges();
        }

        [HttpPost]
        public object EngStatuses_Batch(List<DataChange> changes)
        {
            foreach (var change in changes)
            {
                EngStatus order;

                if (change.Type == "update" || change.Type == "remove")
                {
                    var key = Convert.ToInt32(change.Key);
                    order = _context.EngStatus.First(o => o.EngStatusId == key);
                }
                else
                {
                    order = new EngStatus();
                }

                if (change.Type == "insert" || change.Type == "update")
                {
                    JsonConvert.PopulateObject(change.Data.ToString(), order);

                    if (!TryValidateModel(order))
                        return BadRequest();

                    if (change.Type == "insert")
                    {
                        _context.EngStatus.Add(order);
                    }
                    change.Data = order;
                }
                else if (change.Type == "remove")
                {
                    _context.EngStatus.Remove(order);
                }
            }

            _context.SaveChanges();

            return Ok(changes);
        }
        public ActionResult ExportToExcel()
        {
            return View();
        }
        public Boolean ValidateName(EngStatus engstatus)
        {
            var valid = _context.EngStatus.Any(x => x.EngStatusName == engstatus.EngStatusName);
            return !valid;
        }
    }
}
