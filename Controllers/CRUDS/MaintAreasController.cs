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
    public class MaintAreasController : Controller
    {
        private readonly TagContext _context;

        public MaintAreasController(TagContext context)
        {
            _context = context;
        }

        // GET: MaintAreas

        public async Task<IActionResult> Index(string columnSetsName = "Default")
        {
            Dictionary<string, ColParams> colIndex = new Dictionary<string, ColParams>();
            var col_customisations = _context.ColumnSets
                                        .Where(x => x.ColumnSetsEntity == "MaintArea")
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
                           .Where(x => x.EntityName == "MaintArea")
                           .Include(x => x.EntityAttributeRequirements);

            ViewData["SapValidationJson"] = JsonConvert.SerializeObject(EAId);

            // ViewBag required to update GridView properties and maintain state for selected view
            ViewBag.colIndex = colIndex;
            ViewBag.columnSetsName = columnSetsName;

            return View();
        }

        private void PopulateModel(MaintArea model, IDictionary values)
        {
            string MaintArea_ID = nameof(MaintArea.MaintAreaId);
            string PlantSection_ID = nameof(MaintArea.PlantSectionId);
            string MaintArea_NAME = nameof(MaintArea.MaintAreaName);
            string MaintArea_DESC = nameof(MaintArea.MaintAreaDesc);

            if (values.Contains(MaintArea_ID))
            {
                model.MaintAreaId = Convert.ToInt32(values[MaintArea_ID]);
            }

            if (values.Contains(PlantSection_ID))
            {
                model.PlantSectionId = Convert.ToInt32(values[PlantSection_ID]);
            }

            if (values.Contains(MaintArea_NAME))
            {
                model.MaintAreaName = Convert.ToString(values[MaintArea_NAME]);
            }

            if (values.Contains(MaintArea_DESC))
            {
                model.MaintAreaDesc = Convert.ToString(values[MaintArea_DESC]);
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

        public Object MaintAreas_GetData(DataSourceLoadOptions loadOptions)
        {
            // Oh well, this causes a loop, so will need to find waht field is causing an objhect depth to exceed 32 or be cyclic. 
            // var dataSet = _context.Tag.AsQueryable();

            var dataSet = from rec in _context.MaintArea
                              //select new { rec.TagId, rec.TagNumber, rec.TagService, rec.TagFloc, rec.TagFlocDesc, rec.EngDiscId, rec.SubSystem.SubSystemNum };
                          select new { rec.MaintAreaId, rec.PlantSectionId, rec.MaintAreaName, rec.MaintAreaDesc };

            return DataSourceLoader.Load(dataSet, loadOptions);
        }

        [HttpPost]
        public IActionResult MaintAreas_Insert(string values)
        {
            var newMaintArea = new MaintArea();
            JsonConvert.PopulateObject(values, newMaintArea);

            if (!TryValidateModel(newMaintArea))
                return BadRequest();

            _context.MaintArea.Add(newMaintArea);
            _context.SaveChanges();

            return Ok(newMaintArea);
        }


        [HttpPut]
        public IActionResult MaintAreas_Update(int key, string values)
        {
            // TODO override to update tag state. 
            var order = _context.MaintArea.First(o => o.MaintAreaId == key);
            JsonConvert.PopulateObject(values, order);

            if (!TryValidateModel(order))
                return BadRequest();

            _context.SaveChanges();

            return Ok(order);
        }

        [HttpDelete]
        public void MaintAreas_Delete(int key)
        {
            var order = _context.MaintArea.First(o => o.MaintAreaId == key);
            _context.MaintArea.Remove(order);
            _context.SaveChanges();
        }

        [HttpPost]
        public object MaintAreas_Batch(List<DataChange> changes)
        {
            foreach (var change in changes)
            {
                MaintArea order;

                if (change.Type == "update" || change.Type == "remove")
                {
                    var key = Convert.ToInt32(change.Key);
                    order = _context.MaintArea.First(o => o.MaintAreaId == key);
                }
                else
                {
                    order = new MaintArea();
                }

                if (change.Type == "insert" || change.Type == "update")
                {
                    JsonConvert.PopulateObject(change.Data.ToString(), order);

                    if (!TryValidateModel(order))
                        return BadRequest();

                    if (change.Type == "insert")
                    {
                        _context.MaintArea.Add(order);
                    }
                    change.Data = order;
                }
                else if (change.Type == "remove")
                {
                    _context.MaintArea.Remove(order);
                }
            }

            _context.SaveChanges();

            return Ok(changes);
        }
        public ActionResult ExportToExcel()
        {
            return View();
        }
        public Boolean ValidateName(MaintArea maintarea)
        {
            var valid = _context.MaintArea.Any(x => x.MaintAreaName == maintarea.MaintAreaName);
            return !valid;
        }
    }
}


