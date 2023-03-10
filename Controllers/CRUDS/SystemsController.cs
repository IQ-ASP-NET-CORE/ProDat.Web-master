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
    public class SystemsController : Controller
    {
        private readonly TagContext _context;

        public SystemsController(TagContext context)
        {
            _context = context;
        }

        // GET: Systems

        public IActionResult Index(string columnSetsName = "Default")
        {
            Dictionary<string, ColParams> colIndex = new Dictionary<string, ColParams>();
            var col_customisations = _context.ColumnSets
                                        .Where(x => x.ColumnSetsEntity == "Systems")
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
                           .Where(x => x.EntityName == "Systems")
                           .Include(x => x.EntityAttributeRequirements);

            ViewData["SapValidationJson"] = JsonConvert.SerializeObject(EAId);

            // ViewBag required to update GridView properties and maintain state for selected view
            ViewBag.colIndex = colIndex;
            ViewBag.columnSetsName = columnSetsName;

            return View();
        }

        private void PopulateModel(Systems model, IDictionary values)
        {
            string Systems_ID = nameof(Systems.SystemsId);
            string Systems_NAME = nameof(Systems.SystemName);
            string Systems_NUM = nameof(Systems.SystemNum);

            if (values.Contains(Systems_ID))
            {
                model.SystemsId = Convert.ToInt32(values[Systems_ID]);
            }

            if (values.Contains(Systems_NAME))
            {
                model.SystemName = Convert.ToString(values[Systems_NAME]);
            }

            if (values.Contains(Systems_NUM))
            {
                model.SystemNum = Convert.ToString(values[Systems_NUM]);
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

        public Object Systems_GetData(DataSourceLoadOptions loadOptions)
        {
            // Oh well, this causes a loop, so will need to find waht field is causing an objhect depth to exceed 32 or be cyclic. 
            // var dataSet = _context.Tag.AsQueryable();

            var dataSet = from rec in _context.System
                              //select new { rec.TagId, rec.TagNumber, rec.TagService, rec.TagFloc, rec.TagFlocDesc, rec.EngDiscId, rec.SubSystem.SubSystemNum };
                          select new { rec.SystemsId, rec.SystemName, rec.SystemNum };

            return DataSourceLoader.Load(dataSet, loadOptions);
        }

        [HttpPost]
        public IActionResult Systems_Insert(string values)
        {
            var newSystems = new Systems();
            JsonConvert.PopulateObject(values, newSystems);

            if (!TryValidateModel(newSystems))
                return BadRequest();

            _context.System.Add(newSystems);
            _context.SaveChanges();

            return Ok(newSystems);
        }


        [HttpPut]
        public IActionResult Systems_Update(int key, string values)
        {
            // TODO override to update tag state. 
            var order = _context.System.First(o => o.SystemsId == key);
            JsonConvert.PopulateObject(values, order);

            if (!TryValidateModel(order))
                return BadRequest();

            _context.SaveChanges();

            return Ok(order);
        }

        [HttpDelete]
        public void Systems_Delete(int key)
        {
            var order = _context.System.First(o => o.SystemsId == key);
            _context.System.Remove(order);
            _context.SaveChanges();
        }

        [HttpPost]
        public object Systems_Batch(List<DataChange> changes)
        {
            foreach (var change in changes)
            {
                Systems order;

                if (change.Type == "update" || change.Type == "remove")
                {
                    var key = Convert.ToInt32(change.Key);
                    order = _context.System.First(o => o.SystemsId == key);
                }
                else
                {
                    order = new Systems();
                }

                if (change.Type == "insert" || change.Type == "update")
                {
                    JsonConvert.PopulateObject(change.Data.ToString(), order);

                    if (!TryValidateModel(order))
                        return BadRequest();

                    if (change.Type == "insert")
                    {
                        _context.System.Add(order);
                    }
                    change.Data = order;
                }
                else if (change.Type == "remove")
                {
                    _context.System.Remove(order);
                }
            }

            _context.SaveChanges();

            return Ok(changes);
        }
        public ActionResult ExportToExcel()
        {
            return View();
        }

        public Boolean ValidateNum(Systems system)
        {
            var valid = _context.System.Any(x => x.SystemNum == system.SystemNum);
            return !valid;
        }
    }
}
