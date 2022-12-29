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
    public class MaintenancePlantsController : Controller
    {
        private readonly TagContext _context;

        public MaintenancePlantsController(TagContext context)
        {
            _context = context;
        }

        // GET: MaintenancePlants

        public async Task<IActionResult> Index(string columnSetsName = "Default")
        {
            Dictionary<string, ColParams> colIndex = new Dictionary<string, ColParams>();
            var col_customisations = _context.ColumnSets
                                        .Where(x => x.ColumnSetsEntity == "MaintenancePlant")
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
                           .Where(x => x.EntityName == "MaintenancePlant")
                           .Include(x => x.EntityAttributeRequirements);

            ViewData["SapValidationJson"] = JsonConvert.SerializeObject(EAId);

            // ViewBag required to update GridView properties and maintain state for selected view
            ViewBag.colIndex = colIndex;
            ViewBag.columnSetsName = columnSetsName;

            return View();
        }

        private void PopulateModel(MaintenancePlant model, IDictionary values)
        {
            string MaintenancePlant_ID = nameof(MaintenancePlant.MaintenancePlantId);
            string MaintenancePlant_NUMBER = nameof(MaintenancePlant.MaintenancePlantNum);
            string MaintenancePlant_DESC = nameof(MaintenancePlant.MaintenancePlantDesc);

            if (values.Contains(MaintenancePlant_ID))
            {
                model.MaintenancePlantId = Convert.ToInt32(values[MaintenancePlant_ID]);
            }

            if (values.Contains(MaintenancePlant_NUMBER))
            {
                model.MaintenancePlantNum = Convert.ToString(values[MaintenancePlant_NUMBER]);
            }

            if (values.Contains(MaintenancePlant_DESC))
            {
                model.MaintenancePlantDesc = Convert.ToString(values[MaintenancePlant_DESC]);
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

        public Object MaintenancePlants_GetData(DataSourceLoadOptions loadOptions)
        {
            // Oh well, this causes a loop, so will need to find waht field is causing an objhect depth to exceed 32 or be cyclic. 
            // var dataSet = _context.Tag.AsQueryable();

            var dataSet = from rec in _context.MaintenancePlant
                              //select new { rec.TagId, rec.TagNumber, rec.TagService, rec.TagFloc, rec.TagFlocDesc, rec.EngDiscId, rec.SubSystem.SubSystemNum };
                          select new { rec.MaintenancePlantId, rec.MaintenancePlantNum, rec.MaintenancePlantDesc };

            return DataSourceLoader.Load(dataSet, loadOptions);
        }

        [HttpPost]
        public IActionResult MaintenancePlants_Insert(string values)
        {
            var newMaintenancePlant = new MaintenancePlant();
            JsonConvert.PopulateObject(values, newMaintenancePlant);

            if (!TryValidateModel(newMaintenancePlant))
                return BadRequest();

            _context.MaintenancePlant.Add(newMaintenancePlant);
            _context.SaveChanges();

            return Ok(newMaintenancePlant);
        }


        [HttpPut]
        public IActionResult MaintenancePlants_Update(int key, string values)
        {
            // TODO override to update tag state. 
            var order = _context.MaintenancePlant.First(o => o.MaintenancePlantId == key);
            JsonConvert.PopulateObject(values, order);

            if (!TryValidateModel(order))
                return BadRequest();

            _context.SaveChanges();

            return Ok(order);
        }

        [HttpDelete]
        public void MaintenancePlants_Delete(int key)
        {
            var order = _context.MaintenancePlant.First(o => o.MaintenancePlantId == key);
            _context.MaintenancePlant.Remove(order);
            _context.SaveChanges();
        }

        [HttpPost]
        public object MaintenancePlants_Batch(List<DataChange> changes)
        {
            foreach (var change in changes)
            {
                MaintenancePlant order;

                if (change.Type == "update" || change.Type == "remove")
                {
                    var key = Convert.ToInt32(change.Key);
                    order = _context.MaintenancePlant.First(o => o.MaintenancePlantId == key);
                }
                else
                {
                    order = new MaintenancePlant();
                }

                if (change.Type == "insert" || change.Type == "update")
                {
                    JsonConvert.PopulateObject(change.Data.ToString(), order);

                    if (!TryValidateModel(order))
                        return BadRequest();

                    if (change.Type == "insert")
                    {
                        _context.MaintenancePlant.Add(order);
                    }
                    change.Data = order;
                }
                else if (change.Type == "remove")
                {
                    _context.MaintenancePlant.Remove(order);
                }
            }

            _context.SaveChanges();

            return Ok(changes);
        }
        public ActionResult ExportToExcel()
        {
            return View();
        }
    }
}