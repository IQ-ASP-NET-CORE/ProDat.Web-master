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
    public class MaintStructureIndicatorsController : Controller
    {
        private readonly TagContext _context;

        public MaintStructureIndicatorsController(TagContext context)
        {
            _context = context;
        }

        // GET: MaintStructureIndicators

        public IActionResult Index(string columnSetsName = "Default")
        {
            Dictionary<string, ColParams> colIndex = new Dictionary<string, ColParams>();
            var col_customisations = _context.ColumnSets
                                        .Where(x => x.ColumnSetsEntity == "MaintStructureIndicator")
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
                           .Where(x => x.EntityName == "MaintStructureIndicator")
                           .Include(x => x.EntityAttributeRequirements);

            ViewData["SapValidationJson"] = JsonConvert.SerializeObject(EAId);

            // ViewBag required to update GridView properties and maintain state for selected view
            ViewBag.colIndex = colIndex;
            ViewBag.columnSetsName = columnSetsName;

            return View();
        }

        private void PopulateModel(MaintStructureIndicator model, IDictionary values)
        {
            string MaintStructureIndicator_ID = nameof(MaintStructureIndicator.MaintStructureIndicatorId);
            string MaintStructureIndicator_NAME = nameof(MaintStructureIndicator.MaintStructureIndicatorName);
            string MaintStructureIndicator_DESC = nameof(MaintStructureIndicator.MaintStructureIndicatorDesc);

            if (values.Contains(MaintStructureIndicator_ID))
            {
                model.MaintStructureIndicatorId = Convert.ToInt32(values[MaintStructureIndicator_ID]);
            }

            if (values.Contains(MaintStructureIndicator_NAME))
            {
                model.MaintStructureIndicatorName = Convert.ToString(values[MaintStructureIndicator_NAME]);
            }

            if (values.Contains(MaintStructureIndicator_DESC))
            {
                model.MaintStructureIndicatorDesc = Convert.ToString(values[MaintStructureIndicator_DESC]);
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

        public Object MaintStructureIndicators_GetData(DataSourceLoadOptions loadOptions)
        {
            // Oh well, this causes a loop, so will need to find waht field is causing an objhect depth to exceed 32 or be cyclic. 
            // var dataSet = _context.Tag.AsQueryable();

            var dataSet = from rec in _context.MaintStructureIndicator
                              //select new { rec.TagId, rec.TagNumber, rec.TagService, rec.TagFloc, rec.TagFlocDesc, rec.EngDiscId, rec.SubSystem.SubSystemNum };
                          select new { rec.MaintStructureIndicatorId, rec.MaintStructureIndicatorName, rec.MaintStructureIndicatorDesc };

            return DataSourceLoader.Load(dataSet, loadOptions);
        }

        [HttpPost]
        public IActionResult MaintStructureIndicators_Insert(string values)
        {
            var newMaintStructureIndicator = new MaintStructureIndicator();
            JsonConvert.PopulateObject(values, newMaintStructureIndicator);

            if (!TryValidateModel(newMaintStructureIndicator))
                return BadRequest();

            _context.MaintStructureIndicator.Add(newMaintStructureIndicator);
            _context.SaveChanges();

            return Ok(newMaintStructureIndicator);
        }


        [HttpPut]
        public IActionResult MaintStructureIndicators_Update(int key, string values)
        {
            // TODO override to update tag state. 
            var order = _context.MaintStructureIndicator.First(o => o.MaintStructureIndicatorId == key);
            JsonConvert.PopulateObject(values, order);

            if (!TryValidateModel(order))
                return BadRequest();

            _context.SaveChanges();

            return Ok(order);
        }

        [HttpDelete]
        public void MaintStructureIndicators_Delete(int key)
        {
            var order = _context.MaintStructureIndicator.First(o => o.MaintStructureIndicatorId == key);
            _context.MaintStructureIndicator.Remove(order);
            _context.SaveChanges();
        }

        [HttpPost]
        public object MaintStructureIndicators_Batch(List<DataChange> changes)
        {
            foreach (var change in changes)
            {
                MaintStructureIndicator order;

                if (change.Type == "update" || change.Type == "remove")
                {
                    var key = Convert.ToInt32(change.Key);
                    order = _context.MaintStructureIndicator.First(o => o.MaintStructureIndicatorId == key);
                }
                else
                {
                    order = new MaintStructureIndicator();
                }

                if (change.Type == "insert" || change.Type == "update")
                {
                    JsonConvert.PopulateObject(change.Data.ToString(), order);

                    if (!TryValidateModel(order))
                        return BadRequest();

                    if (change.Type == "insert")
                    {
                        _context.MaintStructureIndicator.Add(order);
                    }
                    change.Data = order;
                }
                else if (change.Type == "remove")
                {
                    _context.MaintStructureIndicator.Remove(order);
                }
            }

            _context.SaveChanges();

            return Ok(changes);
        }
        public ActionResult ExportToExcel()
        {
            return View();
        }

        public Boolean ValidateName(MaintStructureIndicator maintstructindicator)
        {
            var valid = _context.MaintStructureIndicator.Any(x => x.MaintStructureIndicatorName == maintstructindicator.MaintStructureIndicatorName);
            return !valid;
        }
    }
}
