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
    public class WBSElementsController : Controller
    {
        private readonly TagContext _context;

        public WBSElementsController(TagContext context)
        {
            _context = context;
        }

        // GET: WBSElements

        public async Task<IActionResult> Index(string columnSetsName = "Default")
        {
            Dictionary<string, ColParams> colIndex = new Dictionary<string, ColParams>();
            var col_customisations = _context.ColumnSets
                                        .Where(x => x.ColumnSetsEntity == "WBSElement")
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
                           .Where(x => x.EntityName == "WBSElement")
                           .Include(x => x.EntityAttributeRequirements);

            ViewData["SapValidationJson"] = JsonConvert.SerializeObject(EAId);

            // ViewBag required to update GridView properties and maintain state for selected view
            ViewBag.colIndex = colIndex;
            ViewBag.columnSetsName = columnSetsName;

            return View();
        }

        private void PopulateModel(WBSElement model, IDictionary values)
        {
            string WBSElement_ID = nameof(WBSElement.WBSElementId);
            string WBSElement_NAME = nameof(WBSElement.WBSElementName);
            string WBSElement_DESC = nameof(WBSElement.WBSElementDesc);

            if (values.Contains(WBSElement_ID))
            {
                model.WBSElementId = Convert.ToInt32(values[WBSElement_ID]);
            }

            if (values.Contains(WBSElement_NAME))
            {
                model.WBSElementName = Convert.ToString(values[WBSElement_NAME]);
            }

            if (values.Contains(WBSElement_DESC))
            {
                model.WBSElementDesc = Convert.ToString(values[WBSElement_DESC]);
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

        public Object WBSElements_GetData(DataSourceLoadOptions loadOptions)
        {
            // Oh well, this causes a loop, so will need to find waht field is causing an objhect depth to exceed 32 or be cyclic. 
            // var dataSet = _context.Tag.AsQueryable();

            var dataSet = from rec in _context.WBSElement
                              //select new { rec.TagId, rec.TagNumber, rec.TagService, rec.TagFloc, rec.TagFlocDesc, rec.EngDiscId, rec.SubSystem.SubSystemNum };
                          select new { rec.WBSElementId, rec.WBSElementName, rec.WBSElementDesc };

            return DataSourceLoader.Load(dataSet, loadOptions);
        }

        [HttpPost]
        public IActionResult WBSElements_Insert(string values)
        {
            var newWBSElement = new WBSElement();
            JsonConvert.PopulateObject(values, newWBSElement);

            if (!TryValidateModel(newWBSElement))
                return BadRequest();

            _context.WBSElement.Add(newWBSElement);
            _context.SaveChanges();

            return Ok(newWBSElement);
        }


        [HttpPut]
        public IActionResult WBSElements_Update(int key, string values)
        {
            // TODO override to update tag state. 
            var order = _context.WBSElement.First(o => o.WBSElementId == key);
            JsonConvert.PopulateObject(values, order);

            if (!TryValidateModel(order))
                return BadRequest();

            _context.SaveChanges();

            return Ok(order);
        }

        [HttpDelete]
        public void WBSElements_Delete(int key)
        {
            var order = _context.WBSElement.First(o => o.WBSElementId == key);
            _context.WBSElement.Remove(order);
            _context.SaveChanges();
        }

        [HttpPost]
        public object WBSElements_Batch(List<DataChange> changes)
        {
            foreach (var change in changes)
            {
                WBSElement order;

                if (change.Type == "update" || change.Type == "remove")
                {
                    var key = Convert.ToInt32(change.Key);
                    order = _context.WBSElement.First(o => o.WBSElementId == key);
                }
                else
                {
                    order = new WBSElement();
                }

                if (change.Type == "insert" || change.Type == "update")
                {
                    JsonConvert.PopulateObject(change.Data.ToString(), order);

                    if (!TryValidateModel(order))
                        return BadRequest();

                    if (change.Type == "insert")
                    {
                        _context.WBSElement.Add(order);
                    }
                    change.Data = order;
                }
                else if (change.Type == "remove")
                {
                    _context.WBSElement.Remove(order);
                }
            }

            _context.SaveChanges();

            return Ok(changes);
        }
        public ActionResult ExportToExcel()
        {
            return View();
        }

        public Boolean ValidateName(WBSElement wbselement)
        {
            var valid = _context.WBSElement.Any(x => x.WBSElementName == wbselement.WBSElementName);
            return !valid;
        }
    }
}