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
    public class SubSystemsController : Controller
    {
        private readonly TagContext _context;

        public SubSystemsController(TagContext context)
        {
            _context = context;
        }

        // GET: SubSystems

        public async Task<IActionResult> Index(string columnSetsName = "Default")
        {
            Dictionary<string, ColParams> colIndex = new Dictionary<string, ColParams>();
            var col_customisations = _context.ColumnSets
                                        .Where(x => x.ColumnSetsEntity == "SubSystem")
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
                           .Where(x => x.EntityName == "SubSystem")
                           .Include(x => x.EntityAttributeRequirements);

            ViewData["SapValidationJson"] = JsonConvert.SerializeObject(EAId);

            // ViewBag required to update GridView properties and maintain state for selected view
            ViewBag.colIndex = colIndex;
            ViewBag.columnSetsName = columnSetsName;

            return View();
        }

        private void PopulateModel(SubSystem model, IDictionary values)
        {
            string SubSystem_ID = nameof(SubSystem.SubSystemId);
            string Systems_ID = nameof(SubSystem.SystemsId);
            string SubSystem_NAME = nameof(SubSystem.SubSystemName);
            string SubSystem_NUM = nameof(SubSystem.SubSystemNum);

            if (values.Contains(SubSystem_ID))
            {
                model.SubSystemId = Convert.ToInt32(values[SubSystem_ID]);
            }

            if (values.Contains(Systems_ID))
            {
                model.SystemsId = Convert.ToInt32(values[Systems_ID]);
            }

            if (values.Contains(SubSystem_NAME))
            {
                model.SubSystemName = Convert.ToString(values[SubSystem_NAME]);
            }

            if (values.Contains(SubSystem_NUM))
            {
                model.SubSystemNum = Convert.ToString(values[SubSystem_NUM]);
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

        public Object SubSystems_GetData(DataSourceLoadOptions loadOptions)
        {
            // Oh well, this causes a loop, so will need to find waht field is causing an objhect depth to exceed 32 or be cyclic.
            // var dataSet = _context.Tag.AsQueryable();

            var dataSet = from rec in _context.SubSystem
                              //select new { rec.TagId, rec.TagNumber, rec.TagService, rec.TagFloc, rec.TagFlocDesc, rec.EngDiscId, rec.SubSystem.SubSystemNum };
                          select new { rec.SubSystemId, rec.SystemsId, rec.SubSystemName, rec.SubSystemNum };

            return DataSourceLoader.Load(dataSet, loadOptions);
        }

        [HttpPost]
        public IActionResult SubSystems_Insert(string values)
        {
            var newSubSystem = new SubSystem();
            JsonConvert.PopulateObject(values, newSubSystem);

            if (!TryValidateModel(newSubSystem))
                return BadRequest();

            _context.SubSystem.Add(newSubSystem);
            _context.SaveChanges();

            return Ok(newSubSystem);
        }


        [HttpPut]
        public IActionResult SubSystems_Update(int key, string values)
        {
            // TODO override to update tag state.
            var order = _context.SubSystem.First(o => o.SubSystemId == key);
            JsonConvert.PopulateObject(values, order);

            if (!TryValidateModel(order))
                return BadRequest();

            _context.SaveChanges();

            return Ok(order);
        }

        [HttpDelete]
        public void SubSystems_Delete(int key)
        {
            var order = _context.SubSystem.First(o => o.SubSystemId == key);
            _context.SubSystem.Remove(order);
            _context.SaveChanges();
        }

        [HttpPost]
        public object SubSystems_Batch(List<DataChange> changes)
        {
            foreach (var change in changes)
            {
                SubSystem order;

                if (change.Type == "update" || change.Type == "remove")
                {
                    var key = Convert.ToInt32(change.Key);
                    order = _context.SubSystem.First(o => o.SubSystemId == key);
                }
                else
                {
                    order = new SubSystem();
                }

                if (change.Type == "insert" || change.Type == "update")
                {
                    JsonConvert.PopulateObject(change.Data.ToString(), order);

                    if (!TryValidateModel(order))
                        return BadRequest();

                    if (change.Type == "insert")
                    {
                        _context.SubSystem.Add(order);
                    }
                    change.Data = order;
                }
                else if (change.Type == "remove")
                {
                    _context.SubSystem.Remove(order);
                }
            }

            _context.SaveChanges();

            return Ok(changes);
        }
        public ActionResult ExportToExcel()
        {
            return View();
        }

        public Boolean ValidateNum(SubSystem subsystem)
        {
            var valid = _context.SubSystem.Any(x => x.SubSystemNum == subsystem.SubSystemNum);
            return !valid;
        }
    }
}