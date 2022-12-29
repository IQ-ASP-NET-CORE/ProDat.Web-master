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
    public class MaintEdcCodesController : Controller
    {
        private readonly TagContext _context;

        public MaintEdcCodesController(TagContext context)
        {
            _context = context;
        }

        // GET: MaintEdcCodes

        public async Task<IActionResult> Index(string columnSetsName = "Default")
        {
            Dictionary<string, ColParams> colIndex = new Dictionary<string, ColParams>();
            var col_customisations = _context.ColumnSets
                                        .Where(x => x.ColumnSetsEntity == "MaintEdcCode")
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
                           .Where(x => x.EntityName == "MaintEdcCode")
                           .Include(x => x.EntityAttributeRequirements);

            ViewData["SapValidationJson"] = JsonConvert.SerializeObject(EAId);

            // ViewBag required to update GridView properties and maintain state for selected view
            ViewBag.colIndex = colIndex;
            ViewBag.columnSetsName = columnSetsName;

            return View();
        }

        private void PopulateModel(MaintEdcCode model, IDictionary values)
        {
            string MaintEdcCode_ID = nameof(MaintEdcCode.MaintEdcCodeId);
            string MaintEdcCode_NAME = nameof(MaintEdcCode.MaintEdcCodeName);
            string MaintEdcCode_DESC = nameof(MaintEdcCode.MaintEdcCodeDesc);

            if (values.Contains(MaintEdcCode_ID))
            {
                model.MaintEdcCodeId = Convert.ToInt32(values[MaintEdcCode_ID]);
            }

            if (values.Contains(MaintEdcCode_NAME))
            {
                model.MaintEdcCodeName = Convert.ToString(values[MaintEdcCode_NAME]);
            }

            if (values.Contains(MaintEdcCode_DESC))
            {
                model.MaintEdcCodeDesc = Convert.ToString(values[MaintEdcCode_DESC]);
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

        public Object MaintEdcCodes_GetData(DataSourceLoadOptions loadOptions)
        {
            // Oh well, this causes a loop, so will need to find waht field is causing an objhect depth to exceed 32 or be cyclic. 
            // var dataSet = _context.Tag.AsQueryable();

            var dataSet = from rec in _context.MaintEdcCode
                              //select new { rec.TagId, rec.TagNumber, rec.TagService, rec.TagFloc, rec.TagFlocDesc, rec.EngDiscId, rec.SubSystem.SubSystemNum };
                          select new { rec.MaintEdcCodeId, rec.MaintEdcCodeName, rec.MaintEdcCodeDesc };

            return DataSourceLoader.Load(dataSet, loadOptions);
        }

        [HttpPost]
        public IActionResult MaintEdcCodes_Insert(string values)
        {
            var newMaintEdcCode = new MaintEdcCode();
            JsonConvert.PopulateObject(values, newMaintEdcCode);

            if (!TryValidateModel(newMaintEdcCode))
                return BadRequest();

            _context.MaintEdcCode.Add(newMaintEdcCode);
            _context.SaveChanges();

            return Ok(newMaintEdcCode);
        }


        [HttpPut]
        public IActionResult MaintEdcCodes_Update(int key, string values)
        {
            // TODO override to update tag state. 
            var order = _context.MaintEdcCode.First(o => o.MaintEdcCodeId == key);
            JsonConvert.PopulateObject(values, order);

            if (!TryValidateModel(order))
                return BadRequest();

            _context.SaveChanges();

            return Ok(order);
        }

        [HttpDelete]
        public void MaintEdcCodes_Delete(int key)
        {
            var order = _context.MaintEdcCode.First(o => o.MaintEdcCodeId == key);
            _context.MaintEdcCode.Remove(order);
            _context.SaveChanges();
        }

        [HttpPost]
        public object MaintEdcCodes_Batch(List<DataChange> changes)
        {
            foreach (var change in changes)
            {
                MaintEdcCode order;

                if (change.Type == "update" || change.Type == "remove")
                {
                    var key = Convert.ToInt32(change.Key);
                    order = _context.MaintEdcCode.First(o => o.MaintEdcCodeId == key);
                }
                else
                {
                    order = new MaintEdcCode();
                }

                if (change.Type == "insert" || change.Type == "update")
                {
                    JsonConvert.PopulateObject(change.Data.ToString(), order);

                    if (!TryValidateModel(order))
                        return BadRequest();

                    if (change.Type == "insert")
                    {
                        _context.MaintEdcCode.Add(order);
                    }
                    change.Data = order;
                }
                else if (change.Type == "remove")
                {
                    _context.MaintEdcCode.Remove(order);
                }
            }

            _context.SaveChanges();

            return Ok(changes);
        }
        public ActionResult ExportToExcel()
        {
            return View();
        }

        public Boolean ValidateName(MaintEdcCode maintedccode)
        {
            var valid = _context.MaintEdcCode.Any(x => x.MaintEdcCodeName == maintedccode.MaintEdcCodeName);
            return !valid;
        }
    }
}