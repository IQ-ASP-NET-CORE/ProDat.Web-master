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
    public class MaintSortProcessesController : Controller
    {
        private readonly TagContext _context;

        public MaintSortProcessesController(TagContext context)
        {
            _context = context;
        }

        // GET: MaintSortProcesses

        public IActionResult Index(string columnSetsName = "Default")
        {
            Dictionary<string, ColParams> colIndex = new Dictionary<string, ColParams>();
            var col_customisations = _context.ColumnSets
                                        .Where(x => x.ColumnSetsEntity == "MaintSortProcess")
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
                           .Where(x => x.EntityName == "MaintSortProcess")
                           .Include(x => x.EntityAttributeRequirements);

            ViewData["SapValidationJson"] = JsonConvert.SerializeObject(EAId);

            // ViewBag required to update GridView properties and maintain state for selected view
            ViewBag.colIndex = colIndex;
            ViewBag.columnSetsName = columnSetsName;

            return View();
        }

        private void PopulateModel(MaintSortProcess model, IDictionary values)
        {
            string MaintSortProcess_ID = nameof(MaintSortProcess.MaintSortProcessId);
            string MaintSortProcess_NAME = nameof(MaintSortProcess.MaintSortProcessName);
            string MaintSortProcess_DESC = nameof(MaintSortProcess.MaintSortProcessDesc);

            if (values.Contains(MaintSortProcess_ID))
            {
                model.MaintSortProcessId = Convert.ToInt32(values[MaintSortProcess_ID]);
            }

            if (values.Contains(MaintSortProcess_NAME))
            {
                model.MaintSortProcessName = Convert.ToString(values[MaintSortProcess_NAME]);
            }

            if (values.Contains(MaintSortProcess_DESC))
            {
                model.MaintSortProcessDesc = Convert.ToString(values[MaintSortProcess_DESC]);
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

        public Object MaintSortProcesses_GetData(DataSourceLoadOptions loadOptions)
        {
            // Oh well, this causes a loop, so will need to find waht field is causing an objhect depth to exceed 32 or be cyclic. 
            // var dataSet = _context.Tag.AsQueryable();

            var dataSet = from rec in _context.MaintSortProcess
                              //select new { rec.TagId, rec.TagNumber, rec.TagService, rec.TagFloc, rec.TagFlocDesc, rec.EngDiscId, rec.SubSystem.SubSystemNum };
                          select new { rec.MaintSortProcessId, rec.MaintSortProcessName, rec.MaintSortProcessDesc };

            return DataSourceLoader.Load(dataSet, loadOptions);
        }

        [HttpPost]
        public IActionResult MaintSortProcesses_Insert(string values)
        {
            var newMaintSortProcess = new MaintSortProcess();
            JsonConvert.PopulateObject(values, newMaintSortProcess);

            if (!TryValidateModel(newMaintSortProcess))
                return BadRequest();

            _context.MaintSortProcess.Add(newMaintSortProcess);
            _context.SaveChanges();

            return Ok(newMaintSortProcess);
        }


        [HttpPut]
        public IActionResult MaintSortProcesses_Update(int key, string values)
        {
            // TODO override to update tag state. 
            var order = _context.MaintSortProcess.First(o => o.MaintSortProcessId == key);
            JsonConvert.PopulateObject(values, order);

            if (!TryValidateModel(order))
                return BadRequest();

            _context.SaveChanges();

            return Ok(order);
        }

        [HttpDelete]
        public void MaintSortProcesses_Delete(int key)
        {
            var order = _context.MaintSortProcess.First(o => o.MaintSortProcessId == key);
            _context.MaintSortProcess.Remove(order);
            _context.SaveChanges();
        }

        [HttpPost]
        public object MaintSortProcesses_Batch(List<DataChange> changes)
        {
            foreach (var change in changes)
            {
                MaintSortProcess order;

                if (change.Type == "update" || change.Type == "remove")
                {
                    var key = Convert.ToInt32(change.Key);
                    order = _context.MaintSortProcess.First(o => o.MaintSortProcessId == key);
                }
                else
                {
                    order = new MaintSortProcess();
                }

                if (change.Type == "insert" || change.Type == "update")
                {
                    JsonConvert.PopulateObject(change.Data.ToString(), order);

                    if (!TryValidateModel(order))
                        return BadRequest();

                    if (change.Type == "insert")
                    {
                        _context.MaintSortProcess.Add(order);
                    }
                    change.Data = order;
                }
                else if (change.Type == "remove")
                {
                    _context.MaintSortProcess.Remove(order);
                }
            }

            _context.SaveChanges();

            return Ok(changes);
        }
        public ActionResult ExportToExcel()
        {
            return View();
        }

        public Boolean ValidateName(MaintSortProcess maintsortprocess)
        {
            var valid = _context.MaintSortProcess.Any(x => x.MaintSortProcessName == maintsortprocess.MaintSortProcessName);
            return !valid;
        }
    }
}
