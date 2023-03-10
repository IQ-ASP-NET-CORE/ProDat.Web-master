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
    public class RegulatoryBodiesController : Controller
    {
        private readonly TagContext _context;

        public RegulatoryBodiesController(TagContext context)
        {
            _context = context;
        }

        // GET: RegulatoryBodies

        public IActionResult Index(string columnSetsName = "Default")
        {
            Dictionary<string, ColParams> colIndex = new Dictionary<string, ColParams>();
            var col_customisations = _context.ColumnSets
                                        .Where(x => x.ColumnSetsEntity == "RegulatoryBody")
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
                           .Where(x => x.EntityName == "RegulatoryBody")
                           .Include(x => x.EntityAttributeRequirements);

            ViewData["SapValidationJson"] = JsonConvert.SerializeObject(EAId);

            // ViewBag required to update GridView properties and maintain state for selected view
            ViewBag.colIndex = colIndex;
            ViewBag.columnSetsName = columnSetsName;

            return View();
        }

        private void PopulateModel(RegulatoryBody model, IDictionary values)
        {
            string RegulatoryBody_ID = nameof(RegulatoryBody.RegulatoryBodyId);
            string RegulatoryBody_NAME = nameof(RegulatoryBody.RegulatoryBodyName);
            string RegulatoryBody_DESC = nameof(RegulatoryBody.RegulatoryBodyDesc);

            if (values.Contains(RegulatoryBody_ID))
            {
                model.RegulatoryBodyId = Convert.ToInt32(values[RegulatoryBody_ID]);
            }

            if (values.Contains(RegulatoryBody_NAME))
            {
                model.RegulatoryBodyName = Convert.ToString(values[RegulatoryBody_NAME]);
            }

            if (values.Contains(RegulatoryBody_DESC))
            {
                model.RegulatoryBodyDesc = Convert.ToString(values[RegulatoryBody_DESC]);
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

        public Object RegulatoryBodies_GetData(DataSourceLoadOptions loadOptions)
        {
            // Oh well, this causes a loop, so will need to find waht field is causing an objhect depth to exceed 32 or be cyclic. 
            // var dataSet = _context.Tag.AsQueryable();

            var dataSet = from rec in _context.RegulatoryBody
                              //select new { rec.TagId, rec.TagNumber, rec.TagService, rec.TagFloc, rec.TagFlocDesc, rec.EngDiscId, rec.SubSystem.SubSystemNum };
                          select new { rec.RegulatoryBodyId, rec.RegulatoryBodyName, rec.RegulatoryBodyDesc };

            return DataSourceLoader.Load(dataSet, loadOptions);
        }

        [HttpPost]
        public IActionResult RegulatoryBodies_Insert(string values)
        {
            var newRegulatoryBody = new RegulatoryBody();
            JsonConvert.PopulateObject(values, newRegulatoryBody);

            if (!TryValidateModel(newRegulatoryBody))
                return BadRequest();

            _context.RegulatoryBody.Add(newRegulatoryBody);
            _context.SaveChanges();

            return Ok(newRegulatoryBody);
        }


        [HttpPut]
        public IActionResult RegulatoryBodies_Update(int key, string values)
        {
            // TODO override to update tag state. 
            var order = _context.RegulatoryBody.First(o => o.RegulatoryBodyId == key);
            JsonConvert.PopulateObject(values, order);

            if (!TryValidateModel(order))
                return BadRequest();

            _context.SaveChanges();

            return Ok(order);
        }

        [HttpDelete]
        public void RegulatoryBodies_Delete(int key)
        {
            var order = _context.RegulatoryBody.First(o => o.RegulatoryBodyId == key);
            _context.RegulatoryBody.Remove(order);
            _context.SaveChanges();
        }

        [HttpPost]
        public object RegulatoryBodies_Batch(List<DataChange> changes)
        {
            foreach (var change in changes)
            {
                RegulatoryBody order;

                if (change.Type == "update" || change.Type == "remove")
                {
                    var key = Convert.ToInt32(change.Key);
                    order = _context.RegulatoryBody.First(o => o.RegulatoryBodyId == key);
                }
                else
                {
                    order = new RegulatoryBody();
                }

                if (change.Type == "insert" || change.Type == "update")
                {
                    JsonConvert.PopulateObject(change.Data.ToString(), order);

                    if (!TryValidateModel(order))
                        return BadRequest();

                    if (change.Type == "insert")
                    {
                        _context.RegulatoryBody.Add(order);
                    }
                    change.Data = order;
                }
                else if (change.Type == "remove")
                {
                    _context.RegulatoryBody.Remove(order);
                }
            }

            _context.SaveChanges();

            return Ok(changes);
        }
        public ActionResult ExportToExcel()
        {
            return View();
        }

        public Boolean ValidateName(RegulatoryBody regulatorybody)
        {
            var valid = _context.RegulatoryBody.Any(x => x.RegulatoryBodyName == regulatorybody.RegulatoryBodyName);
            return !valid;
        }
    }
}
