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
    public class CommZonesController : Controller
    {
        private readonly TagContext _context;

        public CommZonesController(TagContext context)
        {
            _context = context;
        }

        // GET: CommZones

        public async Task<IActionResult> Index(string columnSetsName = "Default")
        {
            Dictionary<string, ColParams> colIndex = new Dictionary<string, ColParams>();
            var col_customisations = _context.ColumnSets
                                        .Where(x => x.ColumnSetsEntity == "CommZone")
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
                           .Where(x => x.EntityName == "CommZone")
                           .Include(x => x.EntityAttributeRequirements);

            ViewData["SapValidationJson"] = JsonConvert.SerializeObject(EAId);

            // ViewBag required to update GridView properties and maintain state for selected view
            ViewBag.colIndex = colIndex;
            ViewBag.columnSetsName = columnSetsName;

            return View();
        }

        private void PopulateModel(CommZone model, IDictionary values)
        {
            string CommZone_ID = nameof(CommZone.CommZoneId);
            string PROJECT_ID = nameof(CommZone.ProjectId);
            string CommZone_NAME = nameof(CommZone.CommZoneName);
            string CommZone_DESC = nameof(CommZone.CommZoneDesc);

            if (values.Contains(CommZone_ID))
            {
                model.CommZoneId = Convert.ToInt32(values[CommZone_ID]);
            }

            if (values.Contains(PROJECT_ID))
            {
                model.ProjectId = Convert.ToInt32(values[PROJECT_ID]);
            }

            if (values.Contains(CommZone_NAME))
            {
                model.CommZoneName = Convert.ToString(values[CommZone_NAME]);
            }

            if (values.Contains(CommZone_DESC))
            {
                model.CommZoneDesc = Convert.ToString(values[CommZone_DESC]);
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

        public Object CommZones_GetData(DataSourceLoadOptions loadOptions)
        {
            // Oh well, this causes a loop, so will need to find waht field is causing an objhect depth to exceed 32 or be cyclic. 
            // var dataSet = _context.Tag.AsQueryable();

            var dataSet = from rec in _context.CommZone
                              //select new { rec.TagId, rec.TagNumber, rec.TagService, rec.TagFloc, rec.TagFlocDesc, rec.EngDiscId, rec.SubSystem.SubSystemNum };
                          select new { rec.CommZoneId, rec.ProjectId, rec.CommZoneDesc, rec.CommZoneName };

            return DataSourceLoader.Load(dataSet, loadOptions);
        }

        [HttpPost]
        public IActionResult CommZones_Insert(string values)
        {
            var newCommZone = new CommZone();
            JsonConvert.PopulateObject(values, newCommZone);

            if (!TryValidateModel(newCommZone))
                return BadRequest();

            _context.CommZone.Add(newCommZone);
            _context.SaveChanges();

            return Ok(newCommZone);
        }


        [HttpPut]
        public IActionResult CommZones_Update(int key, string values)
        {
            // TODO override to update tag state. 
            var order = _context.CommZone.First(o => o.CommZoneId == key);
            JsonConvert.PopulateObject(values, order);

            if (!TryValidateModel(order))
                return BadRequest();

            _context.SaveChanges();

            return Ok(order);
        }

        [HttpDelete]
        public void CommZones_Delete(int key)
        {
            var order = _context.CommZone.First(o => o.CommZoneId == key);
            _context.CommZone.Remove(order);
            _context.SaveChanges();
        }

        [HttpPost]
        public object CommZones_Batch(List<DataChange> changes)
        {
            foreach (var change in changes)
            {
                CommZone order;

                if (change.Type == "update" || change.Type == "remove")
                {
                    var key = Convert.ToInt32(change.Key);
                    order = _context.CommZone.First(o => o.CommZoneId == key);
                }
                else
                {
                    order = new CommZone();
                }

                if (change.Type == "insert" || change.Type == "update")
                {
                    JsonConvert.PopulateObject(change.Data.ToString(), order);

                    if (!TryValidateModel(order))
                        return BadRequest();

                    if (change.Type == "insert")
                    {
                        _context.CommZone.Add(order);
                    }
                    change.Data = order;
                }
                else if (change.Type == "remove")
                {
                    _context.CommZone.Remove(order);
                }
            }

            _context.SaveChanges();

            return Ok(changes);
        }
        public ActionResult ExportToExcel()
        {
            return View();
        }
        public Boolean ValidateName(CommZone commzone)
        {
            var valid = _context.CommZone.Any(x => x.CommZoneName == commzone.CommZoneName);
            return !valid;
        }
    }
}