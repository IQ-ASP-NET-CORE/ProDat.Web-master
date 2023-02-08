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
    public class EngDiscsController : Controller
    {
        private readonly TagContext _context;

        public EngDiscsController(TagContext context)
        {
            _context = context;
        }

        // GET: EngDiscs
        public async Task<IActionResult> Index(string columnSetsName = "Default")
        {
            Dictionary<string, ColParams> colIndex = new Dictionary<string, ColParams>();
            var col_customisations = _context.ColumnSets
                                        .Where(x => x.ColumnSetsEntity == "EngDisc")
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
                           .Where(x => x.EntityName == "EngDisc")
                           .Include(x => x.EntityAttributeRequirements);

            ViewData["SapValidationJson"] = JsonConvert.SerializeObject(EAId);

            // ViewBag required to update GridView properties and maintain state for selected view
            ViewBag.colIndex = colIndex;
            ViewBag.columnSetsName = columnSetsName;

            return View();
        }

        private void PopulateModel(EngDisc model, IDictionary values)
        {
            string ENGDISC_ID = nameof(EngDisc.EngDiscId);
            string ENGDISC_NAME = nameof(EngDisc.EngDiscName);
            string ENGDISC_DESC = nameof(EngDisc.EngDiscDesc);

            if (values.Contains(ENGDISC_ID))
            {
                model.EngDiscId = Convert.ToInt32(values[ENGDISC_ID]);
            }

            if (values.Contains(ENGDISC_NAME))
            {
                model.EngDiscName = Convert.ToString(values[ENGDISC_NAME]);
            }

            if (values.Contains(ENGDISC_DESC))
            {
                model.EngDiscDesc = Convert.ToString(values[ENGDISC_DESC]);
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

        public Object EngDiscs_GetData(DataSourceLoadOptions loadOptions)
        {
            // Oh well, this causes a loop, so will need to find waht field is causing an objhect depth to exceed 32 or be cyclic.
            // var dataSet = _context.Tag.AsQueryable();

            var dataSet = from rec in _context.EngDisc
                              //select new { rec.TagId, rec.TagNumber, rec.TagService, rec.TagFloc, rec.TagFlocDesc, rec.EngDiscId, rec.SubSystem.SubSystemNum };
                          select new { rec.EngDiscId, rec.EngDiscName, rec.EngDiscDesc };

            return DataSourceLoader.Load(dataSet, loadOptions);
        }

        [HttpPost]
        public IActionResult EngDiscs_Insert(string values)
        {
            var newEngDisc = new EngDisc();
            JsonConvert.PopulateObject(values, newEngDisc);

            if (!TryValidateModel(newEngDisc))
                return BadRequest();

            _context.EngDisc.Add(newEngDisc);
            _context.SaveChanges();

            return Ok(newEngDisc);
        }


        [HttpPut]
        public IActionResult EngDiscs_Update(int key, string values)
        {
            // TODO override to update tag state.
            var order = _context.EngDisc.First(o => o.EngDiscId == key);
            JsonConvert.PopulateObject(values, order);

            if (!TryValidateModel(order))
                return BadRequest();

            _context.SaveChanges();

            return Ok(order);
        }

        [HttpDelete]
        public void EngDiscs_Delete(int key)
        {
            var order = _context.EngDisc.First(o => o.EngDiscId == key);
            _context.EngDisc.Remove(order);
            _context.SaveChanges();
        }

        [HttpPost]
        public object EngDiscs_Batch(List<DataChange> changes)
        {
            foreach (var change in changes)
            {
                EngDisc order;

                if (change.Type == "update" || change.Type == "remove")
                {
                    var key = Convert.ToInt32(change.Key);
                    order = _context.EngDisc.First(o => o.EngDiscId == key);
                }
                else
                {
                    order = new EngDisc();
                }

                if (change.Type == "insert" || change.Type == "update")
                {
                    JsonConvert.PopulateObject(change.Data.ToString(), order);

                    if (!TryValidateModel(order))
                        return BadRequest();

                    if (change.Type == "insert")
                    {
                        _context.EngDisc.Add(order);
                    }
                    change.Data = order;
                }
                else if (change.Type == "remove")
                {
                    _context.EngDisc.Remove(order);
                }
            }

            _context.SaveChanges();

            return Ok(changes);
        }
        public ActionResult ExportToExcel()
        {
            return View();
        }

        public Boolean ValidateName(EngDisc eng_disc)
        {
            var valid = _context.EngDisc.Any(x => x.EngDiscName == eng_disc.EngDiscName);
            return !valid;

        }
    }
}
