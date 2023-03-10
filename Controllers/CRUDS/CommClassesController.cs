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
    public class CommClassesController : Controller
    {
        private readonly TagContext _context;

        public CommClassesController(TagContext context)
        {
            _context = context;
        }

        // GET: CommClasses

        public IActionResult Index(string columnSetsName = "Default")
        {
            Dictionary<string, ColParams> colIndex = new Dictionary<string, ColParams>();
            var col_customisations = _context.ColumnSets
                                        .Where(x => x.ColumnSetsEntity == "CommClass")
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
                           .Where(x => x.EntityName == "CommClass")
                           .Include(x => x.EntityAttributeRequirements);

            ViewData["SapValidationJson"] = JsonConvert.SerializeObject(EAId);

            // ViewBag required to update GridView properties and maintain state for selected view
            ViewBag.colIndex = colIndex;
            ViewBag.columnSetsName = columnSetsName;

            return View();
        }

        private void PopulateModel(CommClass model, IDictionary values)
        {
            string CommClass_ID = nameof(CommClass.CommClassId);
            string CommClass_NAME = nameof(CommClass.CommClassName);
            string CommClass_DESC = nameof(CommClass.CommClassDesc);

            if (values.Contains(CommClass_ID))
            {
                model.CommClassId = Convert.ToInt32(values[CommClass_ID]);
            }

            if (values.Contains(CommClass_NAME))
            {
                model.CommClassName = Convert.ToString(values[CommClass_NAME]);
            }

            if (values.Contains(CommClass_DESC))
            {
                model.CommClassDesc = Convert.ToString(values[CommClass_DESC]);
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

        public Object CommClasses_GetData(DataSourceLoadOptions loadOptions)
        {
            // Oh well, this causes a loop, so will need to find waht field is causing an objhect depth to exceed 32 or be cyclic. 
            // var dataSet = _context.Tag.AsQueryable();

            var dataSet = from rec in _context.CommClass
                              //select new { rec.TagId, rec.TagNumber, rec.TagService, rec.TagFloc, rec.TagFlocDesc, rec.EngDiscId, rec.SubSystem.SubSystemNum };
                          select new {rec.CommClassDesc, rec.CommClassId, rec.CommClassName };

            return DataSourceLoader.Load(dataSet, loadOptions);
        }

        [HttpPost]
        public IActionResult CommClasses_Insert(string values)
        {
            var newCommClass = new CommClass();
            JsonConvert.PopulateObject(values, newCommClass);

            if (!TryValidateModel(newCommClass))
                return BadRequest();

            _context.CommClass.Add(newCommClass);
            _context.SaveChanges();

            return Ok(newCommClass);
        }


        [HttpPut]
        public IActionResult CommClasses_Update(int key, string values)
        {
            // TODO override to update tag state. 
            var order = _context.CommClass.First(o => o.CommClassId == key);
            JsonConvert.PopulateObject(values, order);

            if (!TryValidateModel(order))
                return BadRequest();

            _context.SaveChanges();

            return Ok(order);
        }

        [HttpDelete]
        public void CommClasses_Delete(int key)
        {
            var order = _context.CommClass.First(o => o.CommClassId == key);
            _context.CommClass.Remove(order);
            _context.SaveChanges();
        }

        [HttpPost]
        public object CommClasses_Batch(List<DataChange> changes)
        {
            foreach (var change in changes)
            {
                CommClass order;

                if (change.Type == "update" || change.Type == "remove")
                {
                    var key = Convert.ToInt32(change.Key);
                    order = _context.CommClass.First(o => o.CommClassId == key);
                }
                else
                {
                    order = new CommClass();
                }

                if (change.Type == "insert" || change.Type == "update")
                {
                    JsonConvert.PopulateObject(change.Data.ToString(), order);

                    if (!TryValidateModel(order))
                        return BadRequest();

                    if (change.Type == "insert")
                    {
                        _context.CommClass.Add(order);
                    }
                    change.Data = order;
                }
                else if (change.Type == "remove")
                {
                    _context.CommClass.Remove(order);
                }
            }

            _context.SaveChanges();

            return Ok(changes);
        }
        public ActionResult ExportToExcel()
        {
            return View();
        }
        public Boolean ValidateName(CommClass commclass)
        {
            var valid = _context.CommClass.Any(x => x.CommClassName == commclass.CommClassName);
            return !valid;
        }
    }
}
