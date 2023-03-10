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
    public class SAPStatusesController : Controller
    {
        private readonly TagContext _context;

        public SAPStatusesController(TagContext context)
        {
            _context = context;
        }

        // GET: SAPStatuses

        public IActionResult Index(string columnSetsName = "Default")
        {
            Dictionary<string, ColParams> colIndex = new Dictionary<string, ColParams>();
            var col_customisations = _context.ColumnSets
                                        .Where(x => x.ColumnSetsEntity == "SAPStatus")
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
                           .Where(x => x.EntityName == "SAPStatus")
                           .Include(x => x.EntityAttributeRequirements);

            ViewData["SapValidationJson"] = JsonConvert.SerializeObject(EAId);

            // ViewBag required to update GridView properties and maintain state for selected view
            ViewBag.colIndex = colIndex;
            ViewBag.columnSetsName = columnSetsName;

            return View();
        }

        private void PopulateModel(SAPStatus model, IDictionary values)
        {
            string SAPStatus_ID = nameof(SAPStatus.SAPStatusId);
            string SAPStatus_CODE = nameof(SAPStatus.StatusCode);
            string SAPStatus_DESC = nameof(SAPStatus.Description);
            string SAPStatus_COLOURCODE = nameof(SAPStatus.ColourCode);
            string SAPStatus_FONTCOLOURCODE = nameof(SAPStatus.FontColourCode);
            string SAPStatus_FORSAPEXPORT = nameof(SAPStatus.ForSAPExport);

            if (values.Contains(SAPStatus_ID))
            {
                model.SAPStatusId = Convert.ToInt32(values[SAPStatus_ID]);
            }

            if (values.Contains(SAPStatus_CODE))
            {
                model.StatusCode = Convert.ToInt32(values[SAPStatus_CODE]);
            }

            if (values.Contains(SAPStatus_DESC))
            {
                model.Description = Convert.ToString(values[SAPStatus_DESC]);
            }

            if (values.Contains(SAPStatus_COLOURCODE))
            {
                model.ColourCode= Convert.ToString(values[SAPStatus_COLOURCODE]);
            }

            if (values.Contains(SAPStatus_FONTCOLOURCODE))
            {
                model.FontColourCode = Convert.ToString(values[SAPStatus_FONTCOLOURCODE]);
            }

            if (values.Contains(SAPStatus_FORSAPEXPORT))
            {
                model.ForSAPExport = Convert.ToBoolean(values[SAPStatus_FORSAPEXPORT]);
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

        public Object SAPStatuses_GetData(DataSourceLoadOptions loadOptions)
        {
            // Oh well, this causes a loop, so will need to find waht field is causing an objhect depth to exceed 32 or be cyclic. 
            // var dataSet = _context.Tag.AsQueryable();

            var dataSet = from rec in _context.SAPStatus
                              //select new { rec.TagId, rec.TagNumber, rec.TagService, rec.TagFloc, rec.TagFlocDesc, rec.EngDiscId, rec.SubSystem.SubSystemNum };
                          select new { rec.SAPStatusId, rec.StatusCode, rec.Description, rec.ColourCode, rec.FontColourCode, rec.ForSAPExport };

            return DataSourceLoader.Load(dataSet, loadOptions);
        }

        [HttpPost]
        public IActionResult SAPStatuses_Insert(string values)
        {
            var newSAPStatus = new SAPStatus();
            JsonConvert.PopulateObject(values, newSAPStatus);

            if (!TryValidateModel(newSAPStatus))
                return BadRequest();

            _context.SAPStatus.Add(newSAPStatus);
            _context.SaveChanges();

            return Ok(newSAPStatus);
        }


        [HttpPut]
        public IActionResult SAPStatuses_Update(int key, string values)
        {
            // TODO override to update tag state. 
            var order = _context.SAPStatus.First(o => o.SAPStatusId == key);
            JsonConvert.PopulateObject(values, order);

            if (!TryValidateModel(order))
                return BadRequest();

            _context.SaveChanges();

            return Ok(order);
        }

        [HttpDelete]
        public void SAPStatuses_Delete(int key)
        {
            var order = _context.SAPStatus.First(o => o.SAPStatusId == key);
            _context.SAPStatus.Remove(order);
            _context.SaveChanges();
        }

        [HttpPost]
        public object SAPStatuses_Batch(List<DataChange> changes)
        {
            foreach (var change in changes)
            {
                SAPStatus order;

                if (change.Type == "update" || change.Type == "remove")
                {
                    var key = Convert.ToInt32(change.Key);
                    order = _context.SAPStatus.First(o => o.SAPStatusId == key);
                }
                else
                {
                    order = new SAPStatus();
                }

                if (change.Type == "insert" || change.Type == "update")
                {
                    JsonConvert.PopulateObject(change.Data.ToString(), order);

                    if (!TryValidateModel(order))
                        return BadRequest();

                    if (change.Type == "insert")
                    {
                        _context.SAPStatus.Add(order);
                    }
                    change.Data = order;
                }
                else if (change.Type == "remove")
                {
                    _context.SAPStatus.Remove(order);
                }
            }

            _context.SaveChanges();

            return Ok(changes);
        }
        public ActionResult ExportToExcel()
        {
            return View();
        }

        public Boolean ValidateName(SAPStatus sapstatus)
        {
            var valid = _context.SAPStatus.Any(x => x.StatusCode == sapstatus.StatusCode);
            return !valid;
        }
    }
}
