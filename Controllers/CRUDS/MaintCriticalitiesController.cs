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
    public class MaintCriticalitiesController : Controller
    {
        private readonly TagContext _context;

        public MaintCriticalitiesController(TagContext context)
        {
            _context = context;
        }

        // GET: MaintCriticalities

        public IActionResult Index(string columnSetsName = "Default")
        {
            Dictionary<string, ColParams> colIndex = new Dictionary<string, ColParams>();
            var col_customisations = _context.ColumnSets
                                        .Where(x => x.ColumnSetsEntity == "MaintCriticality")
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
                           .Where(x => x.EntityName == "MaintCriticality")
                           .Include(x => x.EntityAttributeRequirements);

            ViewData["SapValidationJson"] = JsonConvert.SerializeObject(EAId);

            // ViewBag required to update GridView properties and maintain state for selected view
            ViewBag.colIndex = colIndex;
            ViewBag.columnSetsName = columnSetsName;

            return View();
        }

        private void PopulateModel(MaintCriticality model, IDictionary values)
        {
            string MaintCriticality_ID = nameof(MaintCriticality.MaintCriticalityId);
            string MaintCriticality_NAME = nameof(MaintCriticality.MaintCriticalityName);
            string MaintCriticality_DESC = nameof(MaintCriticality.MaintCriticalityDesc);

            if (values.Contains(MaintCriticality_ID))
            {
                model.MaintCriticalityId = Convert.ToInt32(values[MaintCriticality_ID]);
            }

            if (values.Contains(MaintCriticality_NAME))
            {
                model.MaintCriticalityName = Convert.ToString(values[MaintCriticality_NAME]);
            }

            if (values.Contains(MaintCriticality_DESC))
            {
                model.MaintCriticalityDesc = Convert.ToString(values[MaintCriticality_DESC]);
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

        public Object MaintCriticalities_GetData(DataSourceLoadOptions loadOptions)
        {
            // Oh well, this causes a loop, so will need to find waht field is causing an objhect depth to exceed 32 or be cyclic. 
            // var dataSet = _context.Tag.AsQueryable();

            var dataSet = from rec in _context.MaintCriticality
                              //select new { rec.TagId, rec.TagNumber, rec.TagService, rec.TagFloc, rec.TagFlocDesc, rec.EngDiscId, rec.SubSystem.SubSystemNum };
                          select new { rec.MaintCriticalityId, rec.MaintCriticalityName, rec.MaintCriticalityDesc };

            return DataSourceLoader.Load(dataSet, loadOptions);
        }

        [HttpPost]
        public IActionResult MaintCriticalities_Insert(string values)
        {
            var newMaintCriticality = new MaintCriticality();
            JsonConvert.PopulateObject(values, newMaintCriticality);

            if (!TryValidateModel(newMaintCriticality))
                return BadRequest();

            _context.MaintCriticality.Add(newMaintCriticality);
            _context.SaveChanges();

            return Ok(newMaintCriticality);
        }


        [HttpPut]
        public IActionResult MaintCriticalities_Update(int key, string values)
        {
            // TODO override to update tag state. 
            var order = _context.MaintCriticality.First(o => o.MaintCriticalityId == key);
            JsonConvert.PopulateObject(values, order);

            if (!TryValidateModel(order))
                return BadRequest();

            _context.SaveChanges();

            return Ok(order);
        }

        [HttpDelete]
        public void MaintCriticalities_Delete(int key)
        {
            var order = _context.MaintCriticality.First(o => o.MaintCriticalityId == key);
            _context.MaintCriticality.Remove(order);
            _context.SaveChanges();
        }

        [HttpPost]
        public object MaintCriticalities_Batch(List<DataChange> changes)
        {
            foreach (var change in changes)
            {
                MaintCriticality order;

                if (change.Type == "update" || change.Type == "remove")
                {
                    var key = Convert.ToInt32(change.Key);
                    order = _context.MaintCriticality.First(o => o.MaintCriticalityId == key);
                }
                else
                {
                    order = new MaintCriticality();
                }

                if (change.Type == "insert" || change.Type == "update")
                {
                    JsonConvert.PopulateObject(change.Data.ToString(), order);

                    if (!TryValidateModel(order))
                        return BadRequest();

                    if (change.Type == "insert")
                    {
                        _context.MaintCriticality.Add(order);
                    }
                    change.Data = order;
                }
                else if (change.Type == "remove")
                {
                    _context.MaintCriticality.Remove(order);
                }
            }

            _context.SaveChanges();

            return Ok(changes);
        }
        public ActionResult ExportToExcel()
        {
            return View();
        }

        public Boolean ValidateName(MaintCriticality maintcrit)
        {
            var valid = _context.MaintCriticality.Any(x => x.MaintCriticalityName == maintcrit.MaintCriticalityName);
            return !valid;
        }
    }
}
