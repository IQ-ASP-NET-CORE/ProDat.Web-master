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
    public class PbsController : Controller
    {
        private readonly TagContext _context;

        public PbsController(TagContext context)
        {
            _context = context;
        }

        // GET: Pbs

        public async Task<IActionResult> Index(string columnSetsName = "Default")
        {
            Dictionary<string, ColParams> colIndex = new Dictionary<string, ColParams>();
            var col_customisations = _context.ColumnSets
                                        .Where(x => x.ColumnSetsEntity == "Pbs")
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
                           .Where(x => x.EntityName == "Pbs")
                           .Include(x => x.EntityAttributeRequirements);

            ViewData["SapValidationJson"] = JsonConvert.SerializeObject(EAId);

            // ViewBag required to update GridView properties and maintain state for selected view
            ViewBag.colIndex = colIndex;
            ViewBag.columnSetsName = columnSetsName;

            return View();
        }

        private void PopulateModel(Pbs model, IDictionary values)
        {
            string Pbs_ID = nameof(Pbs.PbsId);
            string Pbs_NAME = nameof(Pbs.PbsName);
            string Pbs_DESC = nameof(Pbs.PbsDesc);

            if (values.Contains(Pbs_ID))
            {
                model.PbsId = Convert.ToInt32(values[Pbs_ID]);
            }

            if (values.Contains(Pbs_NAME))
            {
                model.PbsName = Convert.ToString(values[Pbs_NAME]);
            }

            if (values.Contains(Pbs_DESC))
            {
                model.PbsDesc = Convert.ToString(values[Pbs_DESC]);
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

        public Object Pbs_GetData(DataSourceLoadOptions loadOptions)
        {
            // Oh well, this causes a loop, so will need to find waht field is causing an objhect depth to exceed 32 or be cyclic. 
            // var dataSet = _context.Tag.AsQueryable();

            var dataSet = from rec in _context.Pbs
                              //select new { rec.TagId, rec.TagNumber, rec.TagService, rec.TagFloc, rec.TagFlocDesc, rec.EngDiscId, rec.SubSystem.SubSystemNum };
                          select new { rec.PbsId, rec.PbsName, rec.PbsDesc };

            return DataSourceLoader.Load(dataSet, loadOptions);
        }

        [HttpPost]
        public IActionResult Pbs_Insert(string values)
        {
            var newPbs = new Pbs();
            JsonConvert.PopulateObject(values, newPbs);

            if (!TryValidateModel(newPbs))
                return BadRequest();

            _context.Pbs.Add(newPbs);
            _context.SaveChanges();

            return Ok(newPbs);
        }


        [HttpPut]
        public IActionResult Pbs_Update(int key, string values)
        {
            // TODO override to update tag state. 
            var order = _context.Pbs.First(o => o.PbsId == key);
            JsonConvert.PopulateObject(values, order);

            if (!TryValidateModel(order))
                return BadRequest();

            _context.SaveChanges();

            return Ok(order);
        }

        [HttpDelete]
        public void Pbs_Delete(int key)
        {
            var order = _context.Pbs.First(o => o.PbsId == key);
            _context.Pbs.Remove(order);
            _context.SaveChanges();
        }

        [HttpPost]
        public object Pbs_Batch(List<DataChange> changes)
        {
            foreach (var change in changes)
            {
                Pbs order;

                if (change.Type == "update" || change.Type == "remove")
                {
                    var key = Convert.ToInt32(change.Key);
                    order = _context.Pbs.First(o => o.PbsId == key);
                }
                else
                {
                    order = new Pbs();
                }

                if (change.Type == "insert" || change.Type == "update")
                {
                    JsonConvert.PopulateObject(change.Data.ToString(), order);

                    if (!TryValidateModel(order))
                        return BadRequest();

                    if (change.Type == "insert")
                    {
                        _context.Pbs.Add(order);
                    }
                    change.Data = order;
                }
                else if (change.Type == "remove")
                {
                    _context.Pbs.Remove(order);
                }
            }

            _context.SaveChanges();

            return Ok(changes);
        }
        public ActionResult ExportToExcel()
        {
            return View();
        }

        public Boolean ValidateName(Pbs pbs)
        {
            var valid = _context.Pbs.Any(x => x.PbsName == pbs.PbsName);
            return !valid;
        }
    }
}
