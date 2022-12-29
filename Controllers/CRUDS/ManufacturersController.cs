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
    public class ManufacturersController : Controller
    {
        private readonly TagContext _context;

        public ManufacturersController(TagContext context)
        {
            _context = context;
        }

        // GET: Manufacturers

        public async Task<IActionResult> Index(string columnSetsName = "Default")
        {
            Dictionary<string, ColParams> colIndex = new Dictionary<string, ColParams>();
            var col_customisations = _context.ColumnSets
                                        .Where(x => x.ColumnSetsEntity == "Manufacturer")
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
                           .Where(x => x.EntityName == "Manufacturer")
                           .Include(x => x.EntityAttributeRequirements);

            ViewData["SapValidationJson"] = JsonConvert.SerializeObject(EAId);

            // ViewBag required to update GridView properties and maintain state for selected view
            ViewBag.colIndex = colIndex;
            ViewBag.columnSetsName = columnSetsName;

            return View();
        }

        private void PopulateModel(Manufacturer model, IDictionary values)
        {
            string Manufacturer_ID = nameof(Manufacturer.ManufacturerId);
            string Manufacturer_NAME = nameof(Manufacturer.ManufacturerName);
            string Manufacturer_DESC = nameof(Manufacturer.ManufacturerDesc);

            if (values.Contains(Manufacturer_ID))
            {
                model.ManufacturerId = Convert.ToInt32(values[Manufacturer_ID]);
            }

            if (values.Contains(Manufacturer_NAME))
            {
                model.ManufacturerName = Convert.ToString(values[Manufacturer_NAME]);
            }

            if (values.Contains(Manufacturer_DESC))
            {
                model.ManufacturerDesc = Convert.ToString(values[Manufacturer_DESC]);
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

        public Object Manufacturers_GetData(DataSourceLoadOptions loadOptions)
        {
            // Oh well, this causes a loop, so will need to find waht field is causing an objhect depth to exceed 32 or be cyclic. 
            // var dataSet = _context.Tag.AsQueryable();

            var dataSet = from rec in _context.Manufacturer
                              //select new { rec.TagId, rec.TagNumber, rec.TagService, rec.TagFloc, rec.TagFlocDesc, rec.EngDiscId, rec.SubSystem.SubSystemNum };
                          select new { rec.ManufacturerId, rec.ManufacturerName, rec.ManufacturerDesc };

            return DataSourceLoader.Load(dataSet, loadOptions);
        }

        [HttpPost]
        public IActionResult Manufacturers_Insert(string values)
        {
            var newManufacturer = new Manufacturer();
            JsonConvert.PopulateObject(values, newManufacturer);

            if (!TryValidateModel(newManufacturer))
                return BadRequest();

            _context.Manufacturer.Add(newManufacturer);
            _context.SaveChanges();

            return Ok(newManufacturer);
        }


        [HttpPut]
        public IActionResult Manufacturers_Update(int key, string values)
        {
            // TODO override to update tag state. 
            var order = _context.Manufacturer.First(o => o.ManufacturerId == key);
            JsonConvert.PopulateObject(values, order);

            if (!TryValidateModel(order))
                return BadRequest();

            _context.SaveChanges();

            return Ok(order);
        }

        [HttpDelete]
        public void Manufacturers_Delete(int key)
        {
            var order = _context.Manufacturer.First(o => o.ManufacturerId == key);
            _context.Manufacturer.Remove(order);
            _context.SaveChanges();
        }

        [HttpPost]
        public object Manufacturers_Batch(List<DataChange> changes)
        {
            foreach (var change in changes)
            {
                Manufacturer order;

                if (change.Type == "update" || change.Type == "remove")
                {
                    var key = Convert.ToInt32(change.Key);
                    order = _context.Manufacturer.First(o => o.ManufacturerId == key);
                }
                else
                {
                    order = new Manufacturer();
                }

                if (change.Type == "insert" || change.Type == "update")
                {
                    JsonConvert.PopulateObject(change.Data.ToString(), order);

                    if (!TryValidateModel(order))
                        return BadRequest();

                    if (change.Type == "insert")
                    {
                        _context.Manufacturer.Add(order);
                    }
                    change.Data = order;
                }
                else if (change.Type == "remove")
                {
                    _context.Manufacturer.Remove(order);
                }
            }

            _context.SaveChanges();

            return Ok(changes);
        }
        public ActionResult ExportToExcel()
        {
            return View();
        }

        public Boolean ValidateName(Manufacturer manufacturer)
        {
            var valid = _context.Manufacturer.Any(x => x.ManufacturerName == manufacturer.ManufacturerName);
            return !valid;
        }
    }
}
