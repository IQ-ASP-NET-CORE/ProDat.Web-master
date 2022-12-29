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
    public class PoesController : Controller
    {
        private readonly TagContext _context;

        public PoesController(TagContext context)
        {
            _context = context;
        }

        // GET: Poes

        public async Task<IActionResult> Index(string columnSetsName = "Default")
        {
            Dictionary<string, ColParams> colIndex = new Dictionary<string, ColParams>();
            var col_customisations = _context.ColumnSets
                                        .Where(x => x.ColumnSetsEntity == "Po")
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
                           .Where(x => x.EntityName == "Po")
                           .Include(x => x.EntityAttributeRequirements);

            ViewData["SapValidationJson"] = JsonConvert.SerializeObject(EAId);

            // ViewBag required to update GridView properties and maintain state for selected view
            ViewBag.colIndex = colIndex;
            ViewBag.columnSetsName = columnSetsName;

            return View();
        }

        private void PopulateModel(Po model, IDictionary values)
        {
            string Po_ID = nameof(Po.PoId);
            string Po_NAME = nameof(Po.PoName);
            string Po_COMPANY = nameof(Po.PoCompany);
            string Po_DESC = nameof(Po.PoDesc);

            if (values.Contains(Po_ID))
            {
                model.PoId = Convert.ToInt32(values[Po_ID]);
            }

            if (values.Contains(Po_NAME))
            {
                model.PoName = Convert.ToString(values[Po_NAME]);
            }

            if (values.Contains(Po_COMPANY))
            {
                model.PoCompany = Convert.ToString(values[Po_COMPANY]);
            }

            if (values.Contains(Po_DESC))
            {
                model.PoDesc = Convert.ToString(values[Po_DESC]);
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

        public Object Poes_GetData(DataSourceLoadOptions loadOptions)
        {
            // Oh well, this causes a loop, so will need to find waht field is causing an objhect depth to exceed 32 or be cyclic. 
            // var dataSet = _context.Tag.AsQueryable();

            var dataSet = from rec in _context.Po
                              //select new { rec.TagId, rec.TagNumber, rec.TagService, rec.TagFloc, rec.TagFlocDesc, rec.EngDiscId, rec.SubSystem.SubSystemNum };
                          select new { rec.PoId, rec.PoName, rec.PoCompany, rec.PoDesc };

            return DataSourceLoader.Load(dataSet, loadOptions);
        }

        [HttpPost]
        public IActionResult Poes_Insert(string values)
        {
            var newPo = new Po();
            JsonConvert.PopulateObject(values, newPo);

            if (!TryValidateModel(newPo))
                return BadRequest();

            _context.Po.Add(newPo);
            _context.SaveChanges();

            return Ok(newPo);
        }


        [HttpPut]
        public IActionResult Poes_Update(int key, string values)
        {
            // TODO override to update tag state. 
            var order = _context.Po.First(o => o.PoId == key);
            JsonConvert.PopulateObject(values, order);

            if (!TryValidateModel(order))
                return BadRequest();

            _context.SaveChanges();

            return Ok(order);
        }

        [HttpDelete]
        public void Poes_Delete(int key)
        {
            var order = _context.Po.First(o => o.PoId == key);
            _context.Po.Remove(order);
            _context.SaveChanges();
        }

        [HttpPost]
        public object Poes_Batch(List<DataChange> changes)
        {
            foreach (var change in changes)
            {
                Po order;

                if (change.Type == "update" || change.Type == "remove")
                {
                    var key = Convert.ToInt32(change.Key);
                    order = _context.Po.First(o => o.PoId == key);
                }
                else
                {
                    order = new Po();
                }

                if (change.Type == "insert" || change.Type == "update")
                {
                    JsonConvert.PopulateObject(change.Data.ToString(), order);

                    if (!TryValidateModel(order))
                        return BadRequest();

                    if (change.Type == "insert")
                    {
                        _context.Po.Add(order);
                    }
                    change.Data = order;
                }
                else if (change.Type == "remove")
                {
                    _context.Po.Remove(order);
                }
            }

            _context.SaveChanges();

            return Ok(changes);
        }
        public ActionResult ExportToExcel()
        {
            return View();
        }

        public Boolean ValidateName(Po po)
        {
            var valid = _context.Po.Any(x => x.PoName == po.PoName);
            return !valid;
        }
    }
}
