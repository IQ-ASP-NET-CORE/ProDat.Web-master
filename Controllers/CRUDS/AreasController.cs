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
    public class AreasController : Controller
    {
        private readonly TagContext _context;

        public AreasController(TagContext context)
        {
            _context = context;
        }

        // GET: Areas
        public async Task<IActionResult> Index(string columnSetsName = "Default")
        {
            Dictionary<string, ColParams> colIndex = new Dictionary<string, ColParams>();
            var col_customisations = _context.ColumnSets
                                        .Where(x => x.ColumnSetsEntity == "Area")
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
                           .Where(x => x.EntityName == "Area")
                           .Include(x => x.EntityAttributeRequirements);

            ViewData["SapValidationJson"] = JsonConvert.SerializeObject(EAId);

            // ViewBag required to update GridView properties and maintain state for selected view
            ViewBag.colIndex = colIndex;
            ViewBag.columnSetsName = columnSetsName;

            return View();
        }
    

        private void PopulateModel(Area model, IDictionary values)
        {
            string AREA_ID = nameof(Area.AreaId);
            string MAINTENANCE_PLANT_ID = nameof(Area.MaintenancePlantId);
            string AREA_NAME = nameof(Area.AreaName);
            string AREA_DISC = nameof(Area.AreaDisc);

            if (values.Contains(AREA_ID))
            {
                model.AreaId = Convert.ToInt32(values[AREA_ID]);
            }

            if (values.Contains(MAINTENANCE_PLANT_ID))
            {
                model.MaintenancePlantId = Convert.ToInt32(values[MAINTENANCE_PLANT_ID]);
            }

            if (values.Contains(AREA_NAME))
            {
                model.AreaName = Convert.ToString(values[AREA_NAME]);
            }

            if (values.Contains(AREA_DISC))
            {
                model.AreaDisc = Convert.ToString(values[AREA_DISC]);
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

        public Object Areas_GetData(DataSourceLoadOptions loadOptions)
        {
            // Oh well, this causes a loop, so will need to find waht field is causing an objhect depth to exceed 32 or be cyclic.
            // var dataSet = _context.Tag.AsQueryable();

            var dataSet = from rec in _context.Area
                          select new { rec.AreaId, rec.MaintenancePlantId, rec.AreaName, rec.AreaDisc };


            return DataSourceLoader.Load(dataSet, loadOptions);
        }

        [HttpPost]
        public IActionResult Areas_Insert(string values)
        {
            var newArea = new Area();
            JsonConvert.PopulateObject(values, newArea);

            if (!TryValidateModel(newArea))
                return BadRequest();

            _context.Area.Add(newArea);
            _context.SaveChanges();

            return Ok(newArea);
        }


        [HttpPut]
        public IActionResult Areas_Update(int key, string values)
        {
            // TODO override to update tag state.
            var order = _context.Area.First(o => o.AreaId == key);
            JsonConvert.PopulateObject(values, order);

            if (!TryValidateModel(order))
                return BadRequest();

            _context.SaveChanges();

            return Ok(order);
        }

        [HttpDelete]
        public void Areas_Delete(int key)
        {
            var order = _context.Area.First(o => o.AreaId == key);
            _context.Area.Remove(order);
            _context.SaveChanges();
        }

        [HttpPost]
        public object Areas_Batch(List<DataChange> changes)
        {
            foreach (var change in changes)
            {
                Area order;

                if (change.Type == "update" || change.Type == "remove")
                {
                    var key = Convert.ToInt32(change.Key);
                    order = _context.Area.First(o => o.AreaId == key);
                }
                else
                {
                    order = new Area();
                }

                if (change.Type == "insert" || change.Type == "update")
                {
                    JsonConvert.PopulateObject(change.Data.ToString(), order);

                    if (!TryValidateModel(order))
                        return BadRequest();

                    if (change.Type == "insert")
                    {
                        _context.Area.Add(order);
                    }
                    change.Data = order;
                }
                else if (change.Type == "remove")
                {
                    _context.Area.Remove(order);
                }
            }

            _context.SaveChanges();

            return Ok(changes);
        }
        public ActionResult ExportToExcel()
        {
            return View();
        }
        public Boolean ValidateName(Area area)
        {
            var valid = _context.Area.Any(x => x.AreaName == area.AreaName);
            return !valid;
        }
    }
}
