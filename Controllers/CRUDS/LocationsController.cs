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
    public class LocationsController : Controller
    {
        private readonly TagContext _context;

        public LocationsController(TagContext context)
        {
            _context = context;
        }

        // GET: Locations

        public async Task<IActionResult> Index(string columnSetsName = "Default")
        {
            Dictionary<string, ColParams> colIndex = new Dictionary<string, ColParams>();
            var col_customisations = _context.ColumnSets
                                        .Where(x => x.ColumnSetsEntity == "Location")
                                        .Where(x => x.ColumnSetsName == columnSetsName)
                                        .Select(x => new
                                        {
                                            x.ColumnName,
                                            x.ColumnOrder,
                                            x.ColumnWidth,
                                            x.ColumnVisible
                                        });

            foreach (var cust in col_customisations)
            {
                colIndex.Add(cust.ColumnName, new ColParams(cust.ColumnOrder, cust.ColumnWidth));
            }

            // SAP Validation 
            var EAId = _context.EntityAttribute
                           .Where(x => x.EntityName == "Location")
                           .Include(x => x.EntityAttributeRequirements);

            ViewData["SapValidationJson"] = JsonConvert.SerializeObject(EAId);

            // ViewBag required to update GridView properties and maintain state for selected view
            ViewBag.colIndex = colIndex;
            ViewBag.columnSetsName = columnSetsName;

            return View();
        }

        private void PopulateModel(Location model, IDictionary values)
        {
            string LOCATION_ID = nameof(Location.LocationID);
            string AREA_ID = nameof(Location.AreaId);
            string LOCATION_NAME = nameof(Location.LocationName);
            string LOCATION_DESC = nameof(Location.LocationDesc);

            if (values.Contains(LOCATION_ID))
            {
                model.LocationID = Convert.ToInt32(values[LOCATION_ID]);
            }

            if (values.Contains(AREA_ID))
            {
                model.AreaId = Convert.ToInt32(values[AREA_ID]);
            }

            if (values.Contains(LOCATION_NAME))
            {
                model.LocationName = Convert.ToString(values[LOCATION_NAME]);
            }

            if (values.Contains(LOCATION_DESC))
            {
                model.LocationDesc = Convert.ToString(values[LOCATION_DESC]);
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

        public Object Locations_GetData(DataSourceLoadOptions loadOptions)
        {
            // Oh well, this causes a loop, so will need to find waht field is causing an objhect depth to exceed 32 or be cyclic. 
            // var dataSet = _context.Tag.AsQueryable();

            var dataSet = from rec in _context.Location
                              //select new { rec.TagId, rec.TagNumber, rec.TagService, rec.TagFloc, rec.TagFlocDesc, rec.EngDiscId, rec.SubSystem.SubSystemNum };
                          select new { rec.LocationID, rec.AreaId, rec.LocationName, rec.LocationDesc };

            return DataSourceLoader.Load(dataSet, loadOptions);
        }

        [HttpPost]
        public IActionResult Locations_Insert(string values)
        {
            var newLocation = new Location();
            JsonConvert.PopulateObject(values, newLocation);

            if (!TryValidateModel(newLocation))
                return BadRequest();

            _context.Location.Add(newLocation);
            _context.SaveChanges();

            return Ok(newLocation);
        }


        [HttpPut]
        public IActionResult Locations_Update(int key, string values)
        {
            var order = _context.Location.First(o => o.LocationID == key);
            JsonConvert.PopulateObject(values, order);

            if (!TryValidateModel(order))
                return BadRequest();

            _context.SaveChanges();

            return Ok(order);
        }

        [HttpDelete]
        public void Locations_Delete(int key)
        {
            var order = _context.Location.First(o => o.LocationID == key);
            _context.Location.Remove(order);
            _context.SaveChanges();
        }

        [HttpPost]
        public object Locations_Batch(List<DataChange> changes)
        {
            foreach (var change in changes)
            {
                Location order;

                if (change.Type == "update" || change.Type == "remove")
                {
                    var key = Convert.ToInt32(change.Key);
                    order = _context.Location.First(o => o.LocationID == key);
                }
                else
                {
                    order = new Location();
                }

                if (change.Type == "insert" || change.Type == "update")
                {
                    JsonConvert.PopulateObject(change.Data.ToString(), order);

                    if (!TryValidateModel(order))
                        return BadRequest();

                    if (change.Type == "insert")
                    {
                        _context.Location.Add(order);
                    }
                    change.Data = order;
                }
                else if (change.Type == "remove")
                {
                    _context.Location.Remove(order);
                }
            }

            _context.SaveChanges();

            return Ok(changes);
        }
        public ActionResult ExportToExcel()
        {
            return View();
        }
        public Boolean ValidateName(Location location)
        {
            var valid = _context.Location.Any(x => x.LocationName == location.LocationName);
            return !valid;
        }
    }
}