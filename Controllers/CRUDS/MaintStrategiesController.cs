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
    public class MaintStrategiesController : Controller
    {
        private readonly TagContext _context;

        public MaintStrategiesController(TagContext context)
        {
            _context = context;
        }

        // GET: MaintStrategies

        public IActionResult Index(string columnSetsName = "Default")
        {
            Dictionary<string, ColParams> colIndex = new Dictionary<string, ColParams>();
            var col_customisations = _context.ColumnSets
                                        .Where(x => x.ColumnSetsEntity == "MaintStrategy")
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
                           .Where(x => x.EntityName == "MaintStrategy")
                           .Include(x => x.EntityAttributeRequirements);

            ViewData["SapValidationJson"] = JsonConvert.SerializeObject(EAId);

            // ViewBag required to update GridView properties and maintain state for selected view
            ViewBag.colIndex = colIndex;
            ViewBag.columnSetsName = columnSetsName;

            return View();
        }

        private void PopulateModel(MaintStrategy model, IDictionary values)
        {
            string MaintStrategy_ID = nameof(MaintStrategy.MaintStrategyId);
            string Doc_ID = nameof(MaintStrategy.DocId);
            string MaintStrategy_NAME = nameof(MaintStrategy.MaintStrategyName);
            string MaintStrategy_DESC = nameof(MaintStrategy.MaintStrategyDesc);

            if (values.Contains(MaintStrategy_ID))
            {
                model.MaintStrategyId = Convert.ToInt32(values[MaintStrategy_ID]);
            }

            if (values.Contains(Doc_ID))
            {
                model.DocId = Convert.ToInt32(values[Doc_ID]);
            }

            if (values.Contains(MaintStrategy_NAME))
            {
                model.MaintStrategyName = Convert.ToString(values[MaintStrategy_NAME]);
            }

            if (values.Contains(MaintStrategy_DESC))
            {
                model.MaintStrategyDesc = Convert.ToString(values[MaintStrategy_DESC]);
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

        public Object MaintStrategies_GetData(DataSourceLoadOptions loadOptions)
        {
            // Oh well, this causes a loop, so will need to find waht field is causing an objhect depth to exceed 32 or be cyclic. 
            // var dataSet = _context.Tag.AsQueryable();

            var dataSet = from rec in _context.MaintStrategy
                              //select new { rec.TagId, rec.TagNumber, rec.TagService, rec.TagFloc, rec.TagFlocDesc, rec.EngDiscId, rec.SubSystem.SubSystemNum };
                          select new { rec.MaintStrategyId, rec.DocId, rec.MaintStrategyName, rec.MaintStrategyDesc };

            return DataSourceLoader.Load(dataSet, loadOptions);
        }

        [HttpPost]
        public IActionResult MaintStrategies_Insert(string values)
        {
            var newMaintStrategy = new MaintStrategy();
            JsonConvert.PopulateObject(values, newMaintStrategy);

            if (!TryValidateModel(newMaintStrategy))
                return BadRequest();

            _context.MaintStrategy.Add(newMaintStrategy);
            _context.SaveChanges();

            return Ok(newMaintStrategy);
        }


        [HttpPut]
        public IActionResult MaintStrategies_Update(int key, string values)
        {
            // TODO override to update tag state. 
            var order = _context.MaintStrategy.First(o => o.MaintStrategyId == key);
            JsonConvert.PopulateObject(values, order);

            if (!TryValidateModel(order))
                return BadRequest();

            _context.SaveChanges();

            return Ok(order);
        }

        [HttpDelete]
        public void MaintStrategies_Delete(int key)
        {
            var order = _context.MaintStrategy.First(o => o.MaintStrategyId == key);
            _context.MaintStrategy.Remove(order);
            _context.SaveChanges();
        }

        [HttpPost]
        public object MaintStrategies_Batch(List<DataChange> changes)
        {
            foreach (var change in changes)
            {
                MaintStrategy order;

                if (change.Type == "update" || change.Type == "remove")
                {
                    var key = Convert.ToInt32(change.Key);
                    order = _context.MaintStrategy.First(o => o.MaintStrategyId == key);
                }
                else
                {
                    order = new MaintStrategy();
                }

                if (change.Type == "insert" || change.Type == "update")
                {
                    JsonConvert.PopulateObject(change.Data.ToString(), order);

                    if (!TryValidateModel(order))
                        return BadRequest();

                    if (change.Type == "insert")
                    {
                        _context.MaintStrategy.Add(order);
                    }
                    change.Data = order;
                }
                else if (change.Type == "remove")
                {
                    _context.MaintStrategy.Remove(order);
                }
            }

            _context.SaveChanges();

            return Ok(changes);
        }
        public ActionResult ExportToExcel()
        {
            return View();
        }

        public Boolean ValidateName(MaintStrategy maintstrategy)
        {
            var valid = _context.MaintStrategy.Any(x => x.MaintStrategyName == maintstrategy.MaintStrategyName);
            return !valid;
        }
    }
}