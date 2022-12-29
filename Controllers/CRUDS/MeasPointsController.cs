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
    public class MeasPointsController : Controller
    {
        private readonly TagContext _context;

        public MeasPointsController(TagContext context)
        {
            _context = context;
        }

        // GET: MeasPoints

        public async Task<IActionResult> Index(string columnSetsName = "Default")
        {
            Dictionary<string, ColParams> colIndex = new Dictionary<string, ColParams>();
            var col_customisations = _context.ColumnSets
                                        .Where(x => x.ColumnSetsEntity == "MeasPoint")
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
                           .Where(x => x.EntityName == "MeasPoint")
                           .Include(x => x.EntityAttributeRequirements);

            ViewData["SapValidationJson"] = JsonConvert.SerializeObject(EAId);

            // ViewBag required to update GridView properties and maintain state for selected view
            ViewBag.colIndex = colIndex;
            ViewBag.columnSetsName = columnSetsName;

            return View();
        }

        private void PopulateModel(MeasPoint model, IDictionary values)
        {
            string MeasPoint_ID = nameof(MeasPoint.MeasPointId);
            string MeasPoint_NAME = nameof(MeasPoint.MeasPointName);
            string MeasPoint_Data = nameof(MeasPoint.MeasPointData);

            if (values.Contains(MeasPoint_ID))
            {
                model.MeasPointId = Convert.ToInt32(values[MeasPoint_ID]);
            }

            if (values.Contains(MeasPoint_NAME))
            {
                model.MeasPointName = Convert.ToString(values[MeasPoint_NAME]);
            }

            if (values.Contains(MeasPoint_Data))
            {
                model.MeasPointData = Convert.ToString(values[MeasPoint_Data]);
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

        public Object MeasPoints_GetData(DataSourceLoadOptions loadOptions)
        {
            // Oh well, this causes a loop, so will need to find waht field is causing an objhect depth to exceed 32 or be cyclic. 
            // var dataSet = _context.Tag.AsQueryable();

            var dataSet = from rec in _context.MeasPoint
                              //select new { rec.TagId, rec.TagNumber, rec.TagService, rec.TagFloc, rec.TagFlocData, rec.EngDiscId, rec.SubSystem.SubSystemNum };
                          select new { rec.MeasPointId, rec.MeasPointName, rec.MeasPointData };

            return DataSourceLoader.Load(dataSet, loadOptions);
        }

        [HttpPost]
        public IActionResult MeasPoints_Insert(string values)
        {
            var newMeasPoint = new MeasPoint();
            JsonConvert.PopulateObject(values, newMeasPoint);

            if (!TryValidateModel(newMeasPoint))
                return BadRequest();

            _context.MeasPoint.Add(newMeasPoint);
            _context.SaveChanges();

            return Ok(newMeasPoint);
        }


        [HttpPut]
        public IActionResult MeasPoints_Update(int key, string values)
        {
            // TODO override to update tag state. 
            var order = _context.MeasPoint.First(o => o.MeasPointId == key);
            JsonConvert.PopulateObject(values, order);

            if (!TryValidateModel(order))
                return BadRequest();

            _context.SaveChanges();

            return Ok(order);
        }

        [HttpDelete]
        public void MeasPoints_Delete(int key)
        {
            var order = _context.MeasPoint.First(o => o.MeasPointId == key);
            _context.MeasPoint.Remove(order);
            _context.SaveChanges();
        }

        [HttpPost]
        public object MeasPoints_Batch(List<DataChange> changes)
        {
            foreach (var change in changes)
            {
                MeasPoint order;

                if (change.Type == "update" || change.Type == "remove")
                {
                    var key = Convert.ToInt32(change.Key);
                    order = _context.MeasPoint.First(o => o.MeasPointId == key);
                }
                else
                {
                    order = new MeasPoint();
                }

                if (change.Type == "insert" || change.Type == "update")
                {
                    JsonConvert.PopulateObject(change.Data.ToString(), order);

                    if (!TryValidateModel(order))
                        return BadRequest();

                    if (change.Type == "insert")
                    {
                        _context.MeasPoint.Add(order);
                    }
                    change.Data = order;
                }
                else if (change.Type == "remove")
                {
                    _context.MeasPoint.Remove(order);
                }
            }

            _context.SaveChanges();

            return Ok(changes);
        }
        public ActionResult ExportToExcel()
        {
            return View();
        }

        public Boolean ValidateName(MeasPoint measpoint)
        {
            var valid = _context.MeasPoint.Any(x => x.MeasPointName == measpoint.MeasPointName);
            return !valid;
        }
    }
}
