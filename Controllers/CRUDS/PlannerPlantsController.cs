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
    public class PlannerPlantsController : Controller
    {
        private readonly TagContext _context;

        public PlannerPlantsController(TagContext context)
        {
            _context = context;
        }

        // GET: PlannerPlants

        public async Task<IActionResult> Index(string columnSetsName = "Default")
        {
            Dictionary<string, ColParams> colIndex = new Dictionary<string, ColParams>();
            var col_customisations = _context.ColumnSets
                                        .Where(x => x.ColumnSetsEntity == "PlannerPlant")
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
                           .Where(x => x.EntityName == "PlannerPlant")
                           .Include(x => x.EntityAttributeRequirements);

            ViewData["SapValidationJson"] = JsonConvert.SerializeObject(EAId);

            // ViewBag required to update GridView properties and maintain state for selected view
            ViewBag.colIndex = colIndex;
            ViewBag.columnSetsName = columnSetsName;

            return View();
        }

        private void PopulateModel(PlannerPlant model, IDictionary values)
        {
            string PlannerPlant_ID = nameof(PlannerPlant.PlannerPlantId);
            string PlannerPlant_NAME = nameof(PlannerPlant.PlannerPlantName);
            string PlannerPlant_DESC = nameof(PlannerPlant.PlannerPlantDesc);

            if (values.Contains(PlannerPlant_ID))
            {
                model.PlannerPlantId = Convert.ToInt32(values[PlannerPlant_ID]);
            }

            if (values.Contains(PlannerPlant_NAME))
            {
                model.PlannerPlantName = Convert.ToString(values[PlannerPlant_NAME]);
            }

            if (values.Contains(PlannerPlant_DESC))
            {
                model.PlannerPlantDesc = Convert.ToString(values[PlannerPlant_DESC]);
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

        public Object PlannerPlants_GetData(DataSourceLoadOptions loadOptions)
        {
            // Oh well, this causes a loop, so will need to find waht field is causing an objhect depth to exceed 32 or be cyclic. 
            // var dataSet = _context.Tag.AsQueryable();

            var dataSet = from rec in _context.PlannerPlant
                              //select new { rec.TagId, rec.TagNumber, rec.TagService, rec.TagFloc, rec.TagFlocDesc, rec.EngDiscId, rec.SubSystem.SubSystemNum };
                          select new { rec.PlannerPlantId, rec.PlannerPlantName, rec.PlannerPlantDesc };

            return DataSourceLoader.Load(dataSet, loadOptions);
        }

        [HttpPost]
        public IActionResult PlannerPlants_Insert(string values)
        {
            var newPlannerPlant = new PlannerPlant();
            JsonConvert.PopulateObject(values, newPlannerPlant);

            if (!TryValidateModel(newPlannerPlant))
                return BadRequest();

            _context.PlannerPlant.Add(newPlannerPlant);
            _context.SaveChanges();

            return Ok(newPlannerPlant);
        }


        [HttpPut]
        public IActionResult PlannerPlants_Update(int key, string values)
        {
            // TODO override to update tag state. 
            var order = _context.PlannerPlant.First(o => o.PlannerPlantId == key);
            JsonConvert.PopulateObject(values, order);

            if (!TryValidateModel(order))
                return BadRequest();

            _context.SaveChanges();

            return Ok(order);
        }

        [HttpDelete]
        public void PlannerPlants_Delete(int key)
        {
            var order = _context.PlannerPlant.First(o => o.PlannerPlantId == key);
            _context.PlannerPlant.Remove(order);
            _context.SaveChanges();
        }

        [HttpPost]
        public object PlannerPlants_Batch(List<DataChange> changes)
        {
            foreach (var change in changes)
            {
                PlannerPlant order;

                if (change.Type == "update" || change.Type == "remove")
                {
                    var key = Convert.ToInt32(change.Key);
                    order = _context.PlannerPlant.First(o => o.PlannerPlantId == key);
                }
                else
                {
                    order = new PlannerPlant();
                }

                if (change.Type == "insert" || change.Type == "update")
                {
                    JsonConvert.PopulateObject(change.Data.ToString(), order);

                    if (!TryValidateModel(order))
                        return BadRequest();

                    if (change.Type == "insert")
                    {
                        _context.PlannerPlant.Add(order);
                    }
                    change.Data = order;
                }
                else if (change.Type == "remove")
                {
                    _context.PlannerPlant.Remove(order);
                }
            }

            _context.SaveChanges();

            return Ok(changes);
        }
        public ActionResult ExportToExcel()
        {
            return View();
        }

        public Boolean ValidateName(PlannerPlant plannerplant)
        {
            var valid = _context.PlannerPlant.Any(x => x.PlannerPlantName == plannerplant.PlannerPlantName);
            return !valid;
        }
    }
}
