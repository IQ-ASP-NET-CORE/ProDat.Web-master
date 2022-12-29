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
    public class PmassembliesController : Controller
    {
        private readonly TagContext _context;

        public PmassembliesController(TagContext context)
        {
            _context = context;
        }

        // GET: Pmassemblies

        public async Task<IActionResult> Index(string columnSetsName = "Default")
        {
            Dictionary<string, ColParams> colIndex = new Dictionary<string, ColParams>();
            var col_customisations = _context.ColumnSets
                                        .Where(x => x.ColumnSetsEntity == "Pmassembly")
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
                           .Where(x => x.EntityName == "Pmassembly")
                           .Include(x => x.EntityAttributeRequirements);

            ViewData["SapValidationJson"] = JsonConvert.SerializeObject(EAId);

            // ViewBag required to update GridView properties and maintain state for selected view
            ViewBag.colIndex = colIndex;
            ViewBag.columnSetsName = columnSetsName;

            return View();
        }

        private void PopulateModel(Pmassembly model, IDictionary values)
        {
            string Pmassembly_ID = nameof(Pmassembly.PmassemblyId);
            string Pmassembly_NAME = nameof(Pmassembly.PmassemblyName);
            string Pmassembly_SHORTTEXT = nameof(Pmassembly.ShortText);
            string Pmassembly_MAKE = nameof(Pmassembly.Make);
            string Pmassembly_MODEL = nameof(Pmassembly.Model);
            string Pmassembly_REV = nameof(Pmassembly.Rev);

            if (values.Contains(Pmassembly_ID))
            {
                model.PmassemblyId = Convert.ToInt32(values[Pmassembly_ID]);
            }

            if (values.Contains(Pmassembly_NAME))
            {
                model.PmassemblyName = Convert.ToString(values[Pmassembly_NAME]);
            }

            if (values.Contains(Pmassembly_SHORTTEXT))
            {
                model.ShortText = Convert.ToString(values[Pmassembly_SHORTTEXT]);
            }

            if (values.Contains(Pmassembly_MODEL))
            {
                model.Model = Convert.ToString(values[Pmassembly_MODEL]);
            }
            if (values.Contains(Pmassembly_MAKE))
            {
                model.Make = Convert.ToString(values[Pmassembly_MAKE]);
            }

            if (values.Contains(Pmassembly_REV))
            {
                model.Rev = Convert.ToString(values[Pmassembly_REV]);
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

        public Object Pmassemblies_GetData(DataSourceLoadOptions loadOptions)
        {
            // Oh well, this causes a loop, so will need to find waht field is causing an objhect depth to exceed 32 or be cyclic. 
            // var dataSet = _context.Tag.AsQueryable();

            var dataSet = from rec in _context.Pmassembly
                              //select new { rec.TagId, rec.TagNumber, rec.TagService, rec.TagFloc, rec.TagFlocDesc, rec.EngDiscId, rec.SubSystem.SubSystemNum };
                          select new { rec.PmassemblyId, rec.PmassemblyName, rec.ShortText, rec.Make, rec.Model, rec.Rev };

            return DataSourceLoader.Load(dataSet, loadOptions);
        }

        [HttpPost]
        public IActionResult Pmassemblies_Insert(string values)
        {
            var newPmassembly = new Pmassembly();
            JsonConvert.PopulateObject(values, newPmassembly);

            if (!TryValidateModel(newPmassembly))
                return BadRequest();

            _context.Pmassembly.Add(newPmassembly);
            _context.SaveChanges();

            return Ok(newPmassembly);
        }


        [HttpPut]
        public IActionResult Pmassemblies_Update(int key, string values)
        {
            // TODO override to update tag state. 
            var order = _context.Pmassembly.First(o => o.PmassemblyId == key);
            JsonConvert.PopulateObject(values, order);

            if (!TryValidateModel(order))
                return BadRequest();

            _context.SaveChanges();

            return Ok(order);
        }

        [HttpDelete]
        public void Pmassemblies_Delete(int key)
        {
            var order = _context.Pmassembly.First(o => o.PmassemblyId == key);
            _context.Pmassembly.Remove(order);
            _context.SaveChanges();
        }

        [HttpPost]
        public object Pmassemblies_Batch(List<DataChange> changes)
        {
            foreach (var change in changes)
            {
                Pmassembly order;

                if (change.Type == "update" || change.Type == "remove")
                {
                    var key = Convert.ToInt32(change.Key);
                    order = _context.Pmassembly.First(o => o.PmassemblyId == key);
                }
                else
                {
                    order = new Pmassembly();
                }

                if (change.Type == "insert" || change.Type == "update")
                {
                    JsonConvert.PopulateObject(change.Data.ToString(), order);

                    if (!TryValidateModel(order))
                        return BadRequest();

                    if (change.Type == "insert")
                    {
                        _context.Pmassembly.Add(order);
                    }
                    change.Data = order;
                }
                else if (change.Type == "remove")
                {
                    _context.Pmassembly.Remove(order);
                }
            }

            _context.SaveChanges();

            return Ok(changes);
        }
        public ActionResult ExportToExcel()
        {
            return View();
        }

        public Boolean ValidateName(Pmassembly pmassembly)
        {
            var valid = _context.Pmassembly.Any(x => x.PmassemblyName == pmassembly.PmassemblyName);
            return !valid;
        }
    }
}
