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
    public class PlantSectionsController : Controller
    {
        private readonly TagContext _context;

        public PlantSectionsController(TagContext context)
        {
            _context = context;
        }

        // GET: PlantSections

        public async Task<IActionResult> Index(string columnSetsName = "Default")
        {
            Dictionary<string, ColParams> colIndex = new Dictionary<string, ColParams>();
            var col_customisations = _context.ColumnSets
                                        .Where(x => x.ColumnSetsEntity == "PlantSection")
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
                           .Where(x => x.EntityName == "PlantSection")
                           .Include(x => x.EntityAttributeRequirements);

            ViewData["SapValidationJson"] = JsonConvert.SerializeObject(EAId);

            // ViewBag required to update GridView properties and maintain state for selected view
            ViewBag.colIndex = colIndex;
            ViewBag.columnSetsName = columnSetsName;

            return View();
        }

        private void PopulateModel(PlantSection model, IDictionary values)
        {
            string PlantSection_ID = nameof(PlantSection.PlantSectionId);
            string PlantSection_NAME = nameof(PlantSection.PlantSectionName);
            string PlantSection_DESC = nameof(PlantSection.PlantSectionDesc);

            if (values.Contains(PlantSection_ID))
            {
                model.PlantSectionId = Convert.ToInt32(values[PlantSection_ID]);
            }

            if (values.Contains(PlantSection_NAME))
            {
                model.PlantSectionName = Convert.ToString(values[PlantSection_NAME]);
            }

            if (values.Contains(PlantSection_DESC))
            {
                model.PlantSectionDesc = Convert.ToString(values[PlantSection_DESC]);
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

        public Object PlantSections_GetData(DataSourceLoadOptions loadOptions)
        {
            // Oh well, this causes a loop, so will need to find waht field is causing an objhect depth to exceed 32 or be cyclic. 
            // var dataSet = _context.Tag.AsQueryable();

            var dataSet = from rec in _context.PlantSection
                              //select new { rec.TagId, rec.TagNumber, rec.TagService, rec.TagFloc, rec.TagFlocDesc, rec.EngDiscId, rec.SubSystem.SubSystemNum };
                          select new { rec.PlantSectionId, rec.PlantSectionName, rec.PlantSectionDesc };

            return DataSourceLoader.Load(dataSet, loadOptions);
        }

        [HttpPost]
        public IActionResult PlantSections_Insert(string values)
        {
            var newPlantSection = new PlantSection();
            JsonConvert.PopulateObject(values, newPlantSection);

            if (!TryValidateModel(newPlantSection))
                return BadRequest();

            _context.PlantSection.Add(newPlantSection);
            _context.SaveChanges();

            return Ok(newPlantSection);
        }


        [HttpPut]
        public IActionResult PlantSections_Update(int key, string values)
        {
            // TODO override to update tag state. 
            var order = _context.PlantSection.First(o => o.PlantSectionId == key);
            JsonConvert.PopulateObject(values, order);

            if (!TryValidateModel(order))
                return BadRequest();

            _context.SaveChanges();

            return Ok(order);
        }

        [HttpDelete]
        public void PlantSections_Delete(int key)
        {
            var order = _context.PlantSection.First(o => o.PlantSectionId == key);
            _context.PlantSection.Remove(order);
            _context.SaveChanges();
        }

        [HttpPost]
        public object PlantSections_Batch(List<DataChange> changes)
        {
            foreach (var change in changes)
            {
                PlantSection order;

                if (change.Type == "update" || change.Type == "remove")
                {
                    var key = Convert.ToInt32(change.Key);
                    order = _context.PlantSection.First(o => o.PlantSectionId == key);
                }
                else
                {
                    order = new PlantSection();
                }

                if (change.Type == "insert" || change.Type == "update")
                {
                    JsonConvert.PopulateObject(change.Data.ToString(), order);

                    if (!TryValidateModel(order))
                        return BadRequest();

                    if (change.Type == "insert")
                    {
                        _context.PlantSection.Add(order);
                    }
                    change.Data = order;
                }
                else if (change.Type == "remove")
                {
                    _context.PlantSection.Remove(order);
                }
            }

            _context.SaveChanges();

            return Ok(changes);
        }
        public ActionResult ExportToExcel()
        {
            return View();
        }

        public Boolean ValidateName(PlantSection plantsection)
        {
            var valid = _context.PlantSection.Any(x => x.PlantSectionName == plantsection.PlantSectionName);
            return !valid;
        }
    }
}
