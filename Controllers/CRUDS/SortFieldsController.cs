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
    public class SortFieldsController : Controller
    {
        private readonly TagContext _context;

        public SortFieldsController(TagContext context)
        {
            _context = context;
        }

        // GET: SortFields

        public async Task<IActionResult> Index(string columnSetsName = "Default")
        {
            Dictionary<string, ColParams> colIndex = new Dictionary<string, ColParams>();
            var col_customisations = _context.ColumnSets
                                        .Where(x => x.ColumnSetsEntity == "SortField")
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
                           .Where(x => x.EntityName == "SortField")
                           .Include(x => x.EntityAttributeRequirements);

            ViewData["SapValidationJson"] = JsonConvert.SerializeObject(EAId);

            // ViewBag required to update GridView properties and maintain state for selected view
            ViewBag.colIndex = colIndex;
            ViewBag.columnSetsName = columnSetsName;

            return View();
        }

        private void PopulateModel(SortField model, IDictionary values)
        {
            string SortField_ID = nameof(SortField.SortFieldId);
            string SortField_NAME = nameof(SortField.SortFieldName);
            string SortField_DESC = nameof(SortField.SortFieldDesc);

            if (values.Contains(SortField_ID))
            {
                model.SortFieldId = Convert.ToInt32(values[SortField_ID]);
            }

            if (values.Contains(SortField_NAME))
            {
                model.SortFieldName = Convert.ToString(values[SortField_NAME]);
            }

            if (values.Contains(SortField_DESC))
            {
                model.SortFieldDesc = Convert.ToString(values[SortField_DESC]);
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

        public Object SortFields_GetData(DataSourceLoadOptions loadOptions)
        {
            // Oh well, this causes a loop, so will need to find waht field is causing an objhect depth to exceed 32 or be cyclic. 
            // var dataSet = _context.Tag.AsQueryable();

            var dataSet = from rec in _context.SortField
                              //select new { rec.TagId, rec.TagNumber, rec.TagService, rec.TagFloc, rec.TagFlocDesc, rec.EngDiscId, rec.SubSystem.SubSystemNum };
                          select new { rec.SortFieldId, rec.SortFieldName, rec.SortFieldDesc };

            return DataSourceLoader.Load(dataSet, loadOptions);
        }

        [HttpPost]
        public IActionResult SortFields_Insert(string values)
        {
            var newSortField = new SortField();
            JsonConvert.PopulateObject(values, newSortField);

            if (!TryValidateModel(newSortField))
                return BadRequest();

            _context.SortField.Add(newSortField);
            _context.SaveChanges();

            return Ok(newSortField);
        }


        [HttpPut]
        public IActionResult SortFields_Update(int key, string values)
        {
            // TODO override to update tag state. 
            var order = _context.SortField.First(o => o.SortFieldId == key);
            JsonConvert.PopulateObject(values, order);

            if (!TryValidateModel(order))
                return BadRequest();

            _context.SaveChanges();

            return Ok(order);
        }

        [HttpDelete]
        public void SortFields_Delete(int key)
        {
            var order = _context.SortField.First(o => o.SortFieldId == key);
            _context.SortField.Remove(order);
            _context.SaveChanges();
        }

        [HttpPost]
        public object SortFields_Batch(List<DataChange> changes)
        {
            foreach (var change in changes)
            {
                SortField order;

                if (change.Type == "update" || change.Type == "remove")
                {
                    var key = Convert.ToInt32(change.Key);
                    order = _context.SortField.First(o => o.SortFieldId == key);
                }
                else
                {
                    order = new SortField();
                }

                if (change.Type == "insert" || change.Type == "update")
                {
                    JsonConvert.PopulateObject(change.Data.ToString(), order);

                    if (!TryValidateModel(order))
                        return BadRequest();

                    if (change.Type == "insert")
                    {
                        _context.SortField.Add(order);
                    }
                    change.Data = order;
                }
                else if (change.Type == "remove")
                {
                    _context.SortField.Remove(order);
                }
            }

            _context.SaveChanges();

            return Ok(changes);
        }
        public ActionResult ExportToExcel()
        {
            return View();
        }

        public Boolean ValidateName(SortField sortfield)
        {
            var valid = _context.SortField.Any(x => x.SortFieldName == sortfield.SortFieldName);
            return !valid;
        }
    }
}
