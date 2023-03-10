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
    public class EngDataCodesController : Controller
    {
        private readonly TagContext _context;

        public EngDataCodesController(TagContext context)
        {
            _context = context;
        }

        // GET: EngDataCodes

        public IActionResult Index(string columnSetsName = "Default")
        {
            Dictionary<string, ColParams> colIndex = new Dictionary<string, ColParams>();
            var col_customisations = _context.ColumnSets
                                        .Where(x => x.ColumnSetsEntity == "EngDataCode")
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
                           .Where(x => x.EntityName == "EngDataCode")
                           .Include(x => x.EntityAttributeRequirements);

            ViewData["SapValidationJson"] = JsonConvert.SerializeObject(EAId);

            // ViewBag required to update GridView properties and maintain state for selected view
            ViewBag.colIndex = colIndex;
            ViewBag.columnSetsName = columnSetsName;

            return View();
        }

        private void PopulateModel(EngDataCode model, IDictionary values)
        {
            string ENGDATACODE_ID = nameof(EngDataCode.EngDataCodeId);
            string ENGDATACODE_NAME = nameof(EngDataCode.EngDataCodeName);
            string ENGDATACODE_DESC = nameof(EngDataCode.EngDataCodeDesc);
            string ENGDATACODE_NOTES = nameof(EngDataCode.EngDataCodeNotes);
            string HIDEFROMUI = nameof(EngDataCode.HideFromUI);
            string ENGDATACODE_SAPDESC = nameof(EngDataCode.EngDataCodeSAPDesc);

            if (values.Contains(ENGDATACODE_ID))
            {
                model.EngDataCodeId = Convert.ToInt32(values[ENGDATACODE_ID]);
            }

            if (values.Contains(ENGDATACODE_NAME))
            {
                model.EngDataCodeName = Convert.ToString(values[ENGDATACODE_NAME]);
            }

            if (values.Contains(ENGDATACODE_DESC))
            {
                model.EngDataCodeDesc = Convert.ToString(values[ENGDATACODE_DESC]);
            }

            if (values.Contains(ENGDATACODE_NOTES))
            {
                model.EngDataCodeNotes = Convert.ToString(values[ENGDATACODE_NOTES]);
            }

            if (values.Contains(HIDEFROMUI))
            {
                model.HideFromUI = Convert.ToBoolean(values[HIDEFROMUI]);
            }

            if (values.Contains(ENGDATACODE_SAPDESC))
            {
                model.EngDataCodeSAPDesc = Convert.ToString(values[ENGDATACODE_SAPDESC]);
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

        public Object EngDataCodes_GetData(DataSourceLoadOptions loadOptions)
        {
            // Oh well, this causes a loop, so will need to find waht field is causing an objhect depth to exceed 32 or be cyclic. 
            // var dataSet = _context.Tag.AsQueryable();

            var dataSet = from rec in _context.EngDataCode
                              //select new { rec.TagId, rec.TagNumber, rec.TagService, rec.TagFloc, rec.TagFlocDesc, rec.EngDiscId, rec.SubSystem.SubSystemNum };
                          select new { rec.EngDataCodeId, rec.EngDataCodeName, rec.EngDataCodeNotes, rec.EngDataCodeSAPDesc, rec.HideFromUI };

            return DataSourceLoader.Load(dataSet, loadOptions);
        }

        [HttpPost]
        public IActionResult EngDataCodes_Insert(string values)
        {
            var newEngDataCode = new EngDataCode();
            JsonConvert.PopulateObject(values, newEngDataCode);

            if (!TryValidateModel(newEngDataCode))
                return BadRequest();

            _context.EngDataCode.Add(newEngDataCode);
            _context.SaveChanges();

            return Ok(newEngDataCode);
        }


        [HttpPut]
        public IActionResult EngDataCodes_Update(int key, string values)
        {
            // TODO override to update tag state. 
            var order = _context.EngDataCode.First(o => o.EngDataCodeId == key);
            JsonConvert.PopulateObject(values, order);

            if (!TryValidateModel(order))
                return BadRequest();

            _context.SaveChanges();

            return Ok(order);
        }

        [HttpDelete]
        public void EngDataCodes_Delete(int key)
        {
            var order = _context.EngDataCode.First(o => o.EngDataCodeId == key);
            _context.EngDataCode.Remove(order);
            _context.SaveChanges();
        }

        [HttpPost]
        public object EngDataCodes_Batch(List<DataChange> changes)
        {
            foreach (var change in changes)
            {
                EngDataCode order;

                if (change.Type == "update" || change.Type == "remove")
                {
                    var key = Convert.ToInt32(change.Key);
                    order = _context.EngDataCode.First(o => o.EngDataCodeId == key);
                }
                else
                {
                    order = new EngDataCode();
                }

                if (change.Type == "insert" || change.Type == "update")
                {
                    JsonConvert.PopulateObject(change.Data.ToString(), order);

                    if (!TryValidateModel(order))
                        return BadRequest();

                    if (change.Type == "insert")
                    {
                        _context.EngDataCode.Add(order);
                    }
                    change.Data = order;
                }
                else if (change.Type == "remove")
                {
                    _context.EngDataCode.Remove(order);
                }
            }

            _context.SaveChanges();

            return Ok(changes);
        }
        public ActionResult ExportToExcel()
        {
            return View();
        }
        public Boolean ValidateName(EngDataCode engdatacode)
        {
            var valid = _context.EngDataCode.Any(x => x.EngDataCodeName == engdatacode.EngDataCodeName);
            return !valid;
        }
    }
}