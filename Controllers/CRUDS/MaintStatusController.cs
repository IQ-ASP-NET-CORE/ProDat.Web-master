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
    public class MaintStatusesController : Controller
    {
        private readonly TagContext _context;

        public MaintStatusesController(TagContext context)
        {
            _context = context;
        }

        // GET: MaintStatuses

        public IActionResult Index(string columnSetsName = "Default")
        {
            Dictionary<string, ColParams> colIndex = new Dictionary<string, ColParams>();
            var col_customisations = _context.ColumnSets
                                        .Where(x => x.ColumnSetsEntity == "MaintStatus")
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
                           .Where(x => x.EntityName == "MaintStatus")
                           .Include(x => x.EntityAttributeRequirements);

            ViewData["SapValidationJson"] = JsonConvert.SerializeObject(EAId);

            // ViewBag required to update GridView properties and maintain state for selected view
            ViewBag.colIndex = colIndex;
            ViewBag.columnSetsName = columnSetsName;

            return View();
        }

        private void PopulateModel(MaintStatus model, IDictionary values)
        {
            string MaintStatus_ID = nameof(MaintStatus.MaintStatusId);
            string MaintStatus_NAME = nameof(MaintStatus.MaintStatusName);
            string MaintStatus_DESC = nameof(MaintStatus.MaintStatusDesc);

            if (values.Contains(MaintStatus_ID))
            {
                model.MaintStatusId = Convert.ToInt32(values[MaintStatus_ID]);
            }

            if (values.Contains(MaintStatus_NAME))
            {
                model.MaintStatusName = Convert.ToString(values[MaintStatus_NAME]);
            }

            if (values.Contains(MaintStatus_DESC))
            {
                model.MaintStatusDesc = Convert.ToString(values[MaintStatus_DESC]);
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

        public Object MaintStatuses_GetData(DataSourceLoadOptions loadOptions)
        {
            // Oh well, this causes a loop, so will need to find waht field is causing an objhect depth to exceed 32 or be cyclic. 
            // var dataSet = _context.Tag.AsQueryable();

            var dataSet = from rec in _context.MaintStatus
                              //select new { rec.TagId, rec.TagNumber, rec.TagService, rec.TagFloc, rec.TagFlocDesc, rec.EngDiscId, rec.SubSystem.SubSystemNum };
                          select new { rec.MaintStatusId, rec.MaintStatusName, rec.MaintStatusDesc };

            return DataSourceLoader.Load(dataSet, loadOptions);
        }

        [HttpPost]
        public IActionResult MaintStatuses_Insert(string values)
        {
            var newMaintStatus = new MaintStatus();
            JsonConvert.PopulateObject(values, newMaintStatus);

            if (!TryValidateModel(newMaintStatus))
                return BadRequest();

            _context.MaintStatus.Add(newMaintStatus);
            _context.SaveChanges();

            return Ok(newMaintStatus);
        }


        [HttpPut]
        public IActionResult MaintStatuses_Update(int key, string values)
        {
            // TODO override to update tag state. 
            var order = _context.MaintStatus.First(o => o.MaintStatusId == key);
            JsonConvert.PopulateObject(values, order);

            if (!TryValidateModel(order))
                return BadRequest();

            _context.SaveChanges();

            return Ok(order);
        }

        [HttpDelete]
        public void MaintStatuses_Delete(int key)
        {
            var order = _context.MaintStatus.First(o => o.MaintStatusId == key);
            _context.MaintStatus.Remove(order);
            _context.SaveChanges();
        }

        [HttpPost]
        public object MaintStatuses_Batch(List<DataChange> changes)
        {
            foreach (var change in changes)
            {
                MaintStatus order;

                if (change.Type == "update" || change.Type == "remove")
                {
                    var key = Convert.ToInt32(change.Key);
                    order = _context.MaintStatus.First(o => o.MaintStatusId == key);
                }
                else
                {
                    order = new MaintStatus();
                }

                if (change.Type == "insert" || change.Type == "update")
                {
                    JsonConvert.PopulateObject(change.Data.ToString(), order);

                    if (!TryValidateModel(order))
                        return BadRequest();

                    if (change.Type == "insert")
                    {
                        _context.MaintStatus.Add(order);
                    }
                    change.Data = order;
                }
                else if (change.Type == "remove")
                {
                    _context.MaintStatus.Remove(order);
                }
            }

            _context.SaveChanges();

            return Ok(changes);
        }
        public ActionResult ExportToExcel()
        {
            return View();
        }

        public Boolean ValidateName(MaintStatus maintstatus)
        {
            var valid = _context.MaintStatus.Any(x => x.MaintStatusName == maintstatus.MaintStatusName);
            return !valid;
        }
    }
}
