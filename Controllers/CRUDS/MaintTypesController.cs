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
    public class MaintTypesController : Controller
    {
        private readonly TagContext _context;

        public MaintTypesController(TagContext context)
        {
            _context = context;
        }

        // GET: MaintTypes

        public async Task<IActionResult> Index(string columnSetsName = "Default")
        {
            Dictionary<string, ColParams> colIndex = new Dictionary<string, ColParams>();
            var col_customisations = _context.ColumnSets
                                        .Where(x => x.ColumnSetsEntity == "MaintType")
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
                           .Where(x => x.EntityName == "MaintType")
                           .Include(x => x.EntityAttributeRequirements);

            ViewData["SapValidationJson"] = JsonConvert.SerializeObject(EAId);

            // ViewBag required to update GridView properties and maintain state for selected view
            ViewBag.colIndex = colIndex;
            ViewBag.columnSetsName = columnSetsName;

            return View();
        }

        private void PopulateModel(MaintType model, IDictionary values)
        {
            string MaintType_ID = nameof(MaintType.MaintTypeId);
            string MaintType_NAME = nameof(MaintType.MaintTypeName);
            string MaintType_DESC = nameof(MaintType.MaintTypeDesc);

            if (values.Contains(MaintType_ID))
            {
                model.MaintTypeId = Convert.ToInt32(values[MaintType_ID]);
            }

            if (values.Contains(MaintType_NAME))
            {
                model.MaintTypeName = Convert.ToString(values[MaintType_NAME]);
            }

            if (values.Contains(MaintType_DESC))
            {
                model.MaintTypeDesc = Convert.ToString(values[MaintType_DESC]);
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

        public Object MaintTypes_GetData(DataSourceLoadOptions loadOptions)
        {
            // Oh well, this causes a loop, so will need to find waht field is causing an objhect depth to exceed 32 or be cyclic. 
            // var dataSet = _context.Tag.AsQueryable();

            var dataSet = from rec in _context.MaintType
                              //select new { rec.TagId, rec.TagNumber, rec.TagService, rec.TagFloc, rec.TagFlocDesc, rec.EngDiscId, rec.SubSystem.SubSystemNum };
                          select new { rec.MaintTypeId, rec.MaintTypeName, rec.MaintTypeDesc };

            return DataSourceLoader.Load(dataSet, loadOptions);
        }

        [HttpPost]
        public IActionResult MaintTypes_Insert(string values)
        {
            var newMaintType = new MaintType();
            JsonConvert.PopulateObject(values, newMaintType);

            if (!TryValidateModel(newMaintType))
                return BadRequest();

            _context.MaintType.Add(newMaintType);
            _context.SaveChanges();

            return Ok(newMaintType);
        }


        [HttpPut]
        public IActionResult MaintTypes_Update(int key, string values)
        {
            // TODO override to update tag state. 
            var order = _context.MaintType.First(o => o.MaintTypeId == key);
            JsonConvert.PopulateObject(values, order);

            if (!TryValidateModel(order))
                return BadRequest();

            _context.SaveChanges();

            return Ok(order);
        }

        [HttpDelete]
        public void MaintTypes_Delete(int key)
        {
            var order = _context.MaintType.First(o => o.MaintTypeId == key);
            _context.MaintType.Remove(order);
            _context.SaveChanges();
        }

        [HttpPost]
        public object MaintTypes_Batch(List<DataChange> changes)
        {
            foreach (var change in changes)
            {
                MaintType order;

                if (change.Type == "update" || change.Type == "remove")
                {
                    var key = Convert.ToInt32(change.Key);
                    order = _context.MaintType.First(o => o.MaintTypeId == key);
                }
                else
                {
                    order = new MaintType();
                }

                if (change.Type == "insert" || change.Type == "update")
                {
                    JsonConvert.PopulateObject(change.Data.ToString(), order);

                    if (!TryValidateModel(order))
                        return BadRequest();

                    if (change.Type == "insert")
                    {
                        _context.MaintType.Add(order);
                    }
                    change.Data = order;
                }
                else if (change.Type == "remove")
                {
                    _context.MaintType.Remove(order);
                }
            }

            _context.SaveChanges();

            return Ok(changes);
        }
        public ActionResult ExportToExcel()
        {
            return View();
        }

        public Boolean ValidateName(MaintType mainttype)
        {
            var valid = _context.MaintType.Any(x => x.MaintTypeName == mainttype.MaintTypeName);
            return !valid;
        }
    }
}
