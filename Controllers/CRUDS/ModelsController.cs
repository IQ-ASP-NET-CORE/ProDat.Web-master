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
    public class ModelsController : Controller
    {
        private readonly TagContext _context;

        public ModelsController(TagContext context)
        {
            _context = context;
        }

        // GET: Models
        public async Task<IActionResult> Index(string columnSetsName = "Default")
        {
            Dictionary<string, ColParams> colIndex = new Dictionary<string, ColParams>();
            var col_customisations = _context.ColumnSets
                                        .Where(x => x.ColumnSetsEntity == "Model")
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
                           .Where(x => x.EntityName == "Model")
                           .Include(x => x.EntityAttributeRequirements);

            ViewData["SapValidationJson"] = JsonConvert.SerializeObject(EAId);

            // ViewBag required to update GridView properties and maintain state for selected view
            ViewBag.colIndex = colIndex;
            ViewBag.columnSetsName = columnSetsName;

            return View();
        }

        private void PopulateModel(Model model, IDictionary values)
        {
            string Model_ID = nameof(Model.ModelId);
            string MANUFACTURER_ID = nameof(Model.ManufacturerId);
            string Model_NAME = nameof(Model.ModelName);
            string Model_DESC = nameof(Model.ModelDesc);

            if (values.Contains(Model_ID))
            {
                model.ModelId = Convert.ToInt32(values[Model_ID]);
            }

            if (values.Contains(MANUFACTURER_ID))
            {
                model.ManufacturerId = Convert.ToInt32(values[MANUFACTURER_ID]);
            }

            if (values.Contains(Model_NAME))
            {
                model.ModelName = Convert.ToString(values[Model_NAME]);
            }

            if (values.Contains(Model_DESC))
            {
                model.ModelDesc = Convert.ToString(values[Model_DESC]);
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

        public Object Models_GetData(DataSourceLoadOptions loadOptions)
        {
            // Oh well, this causes a loop, so will need to find waht field is causing an objhect depth to exceed 32 or be cyclic. 
            // var dataSet = _context.Tag.AsQueryable();

            var dataSet = from rec in _context.Models
                              //select new { rec.TagId, rec.TagNumber, rec.TagService, rec.TagFloc, rec.TagFlocDesc, rec.EngDiscId, rec.SubSystem.SubSystemNum };
                          select new { rec.ModelId, rec.ManufacturerId, rec.ModelName, rec.ModelDesc };

            return DataSourceLoader.Load(dataSet, loadOptions);
        }

        [HttpPost]
        public IActionResult Models_Insert(string values)
        {
            var newModel = new Model();
            JsonConvert.PopulateObject(values, newModel);

            if (!TryValidateModel(newModel))
                return BadRequest();

            _context.Models.Add(newModel);
            _context.SaveChanges();

            return Ok(newModel);
        }


        [HttpPut]
        public IActionResult Models_Update(int key, string values)
        {
            // TODO override to update tag state. 
            var order = _context.Models.First(o => o.ModelId == key);
            JsonConvert.PopulateObject(values, order);

            if (!TryValidateModel(order))
                return BadRequest();

            _context.SaveChanges();

            return Ok(order);
        }

        [HttpDelete]
        public void Models_Delete(int key)
        {
            var order = _context.Models.First(o => o.ModelId == key);
            _context.Models.Remove(order);
            _context.SaveChanges();
        }

        [HttpPost]
        public object Models_Batch(List<DataChange> changes)
        {
            foreach (var change in changes)
            {
                Model order;

                if (change.Type == "update" || change.Type == "remove")
                {
                    var key = Convert.ToInt32(change.Key);
                    order = _context.Models.First(o => o.ModelId == key);
                }
                else
                {
                    order = new Model();
                }

                if (change.Type == "insert" || change.Type == "update")
                {
                    JsonConvert.PopulateObject(change.Data.ToString(), order);

                    if (!TryValidateModel(order))
                        return BadRequest();

                    if (change.Type == "insert")
                    {
                        _context.Models.Add(order);
                    }
                    change.Data = order;
                }
                else if (change.Type == "remove")
                {
                    _context.Models.Remove(order);
                }
            }

            _context.SaveChanges();

            return Ok(changes);
        }
        public ActionResult ExportToExcel()
        {
            return View();
        }

        public Boolean ValidateName(Model model)
        {
            var valid = _context.Models.Any(x => x.ModelName == model.ModelName);
            return !valid;
        }
    }
}
