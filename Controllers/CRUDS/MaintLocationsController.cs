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
    public class MaintLocationsController : Controller
    {
        private readonly TagContext _context;

        public MaintLocationsController(TagContext context)
        {
            _context = context;
        }

        // GET: MaintLocations

        public IActionResult Index(string columnSetsName = "Default")
        {
            Dictionary<string, ColParams> colIndex = new Dictionary<string, ColParams>();
            var col_customisations = _context.ColumnSets
                                        .Where(x => x.ColumnSetsEntity == "MaintLocation")
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
                           .Where(x => x.EntityName == "MaintLocation")
                           .Include(x => x.EntityAttributeRequirements);

            ViewData["SapValidationJson"] = JsonConvert.SerializeObject(EAId);

            // ViewBag required to update GridView properties and maintain state for selected view
            ViewBag.colIndex = colIndex;
            ViewBag.columnSetsName = columnSetsName;

            return View();
        }

        private void PopulateModel(MaintLocation model, IDictionary values)
        {
            string MaintLocation_ID = nameof(MaintLocation.MaintLocationId);
            string MaintArea_ID = nameof(MaintLocation.MaintAreaId);
            string MaintLocation_NAME = nameof(MaintLocation.MaintLocationName);
            string MaintLocation_DESC = nameof(MaintLocation.MaintLocationDesc);

            if (values.Contains(MaintLocation_ID))
            {
                model.MaintLocationId = Convert.ToInt32(values[MaintLocation_ID]);
            }

            if (values.Contains(MaintArea_ID))
            {
                model.MaintAreaId = Convert.ToInt32(values[MaintArea_ID]);
            }

            if (values.Contains(MaintLocation_NAME))
            {
                model.MaintLocationName = Convert.ToString(values[MaintLocation_NAME]);
            }

            if (values.Contains(MaintLocation_DESC))
            {
                model.MaintLocationDesc = Convert.ToString(values[MaintLocation_DESC]);
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

        public Object MaintLocations_GetData(DataSourceLoadOptions loadOptions)
        {
            // Oh well, this causes a loop, so will need to find waht field is causing an objhect depth to exceed 32 or be cyclic. 
            // var dataSet = _context.Tag.AsQueryable();

            var dataSet = from rec in _context.MaintLocation
                              //select new { rec.TagId, rec.TagNumber, rec.TagService, rec.TagFloc, rec.TagFlocDesc, rec.EngDiscId, rec.SubSystem.SubSystemNum };
                          select new { rec.MaintLocationId, rec.MaintAreaId, rec.MaintLocationName, rec.MaintLocationDesc };

            return DataSourceLoader.Load(dataSet, loadOptions);
        }

        [HttpPost]
        public IActionResult MaintLocations_Insert(string values)
        {
            var newMaintLocation = new MaintLocation();
            JsonConvert.PopulateObject(values, newMaintLocation);

            if (!TryValidateModel(newMaintLocation))
                return BadRequest();

            _context.MaintLocation.Add(newMaintLocation);
            _context.SaveChanges();

            return Ok(newMaintLocation);
        }


        [HttpPut]
        public IActionResult MaintLocations_Update(int key, string values)
        {
            // TODO override to update tag state. 
            var order = _context.MaintLocation.First(o => o.MaintLocationId == key);
            JsonConvert.PopulateObject(values, order);

            if (!TryValidateModel(order))
                return BadRequest();

            _context.SaveChanges();

            return Ok(order);
        }

        [HttpDelete]
        public void MaintLocations_Delete(int key)
        {
            var order = _context.MaintLocation.First(o => o.MaintLocationId == key);
            _context.MaintLocation.Remove(order);
            _context.SaveChanges();
        }

        [HttpPost]
        public object MaintLocations_Batch(List<DataChange> changes)
        {
            foreach (var change in changes)
            {
                MaintLocation order;

                if (change.Type == "update" || change.Type == "remove")
                {
                    var key = Convert.ToInt32(change.Key);
                    order = _context.MaintLocation.First(o => o.MaintLocationId == key);
                }
                else
                {
                    order = new MaintLocation();
                }

                if (change.Type == "insert" || change.Type == "update")
                {
                    JsonConvert.PopulateObject(change.Data.ToString(), order);

                    if (!TryValidateModel(order))
                        return BadRequest();

                    if (change.Type == "insert")
                    {
                        _context.MaintLocation.Add(order);
                    }
                    change.Data = order;
                }
                else if (change.Type == "remove")
                {
                    _context.MaintLocation.Remove(order);
                }
            }

            _context.SaveChanges();

            return Ok(changes);
        }

        public ActionResult ExportToExcel()
        {
            return View();
        }

        public Boolean ValidateName(MaintLocation maintlocation)
        {
            var valid = _context.MaintLocation.Any(x => x.MaintLocationName == maintlocation.MaintLocationName);
            return !valid;
        }
    }
}