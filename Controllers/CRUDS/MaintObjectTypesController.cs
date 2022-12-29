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
    public class MaintObjectTypesController : Controller
    {
        private readonly TagContext _context;

        public MaintObjectTypesController(TagContext context)
        {
            _context = context;
        }

        // GET: MaintObjectTypes

        public async Task<IActionResult> Index(string columnSetsName = "Default")
        {
            Dictionary<string, ColParams> colIndex = new Dictionary<string, ColParams>();
            var col_customisations = _context.ColumnSets
                                        .Where(x => x.ColumnSetsEntity == "MaintObjectType")
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
                           .Where(x => x.EntityName == "MaintObjectType")
                           .Include(x => x.EntityAttributeRequirements);

            ViewData["SapValidationJson"] = JsonConvert.SerializeObject(EAId);

            // ViewBag required to update GridView properties and maintain state for selected view
            ViewBag.colIndex = colIndex;
            ViewBag.columnSetsName = columnSetsName;

            return View();
        }

        private void PopulateModel(MaintObjectType model, IDictionary values)
        {
            string MaintObjectType_ID = nameof(MaintObjectType.MaintObjectTypeId);
            string MaintObjectType_NAME = nameof(MaintObjectType.MaintObjectTypeName);
            string MaintObjectType_DESC = nameof(MaintObjectType.MaintObjectTypeDesc);
            string MaintObjectType_DESCEXT = nameof(MaintObjectType.MaintObjectTypeDescExt);
            string STD_NOUN_MODIFIER = nameof(MaintObjectType.StdNounModifier);

            if (values.Contains(MaintObjectType_ID))
            {
                model.MaintObjectTypeId = Convert.ToInt32(values[MaintObjectType_ID]);
            }

            if (values.Contains(MaintObjectType_NAME))
            {
                model.MaintObjectTypeName = Convert.ToString(values[MaintObjectType_NAME]);
            }

            if (values.Contains(MaintObjectType_DESC))
            {
                model.MaintObjectTypeDesc = Convert.ToString(values[MaintObjectType_DESC]);
            }

            if (values.Contains(MaintObjectType_DESCEXT))
            {
                model.MaintObjectTypeDescExt = Convert.ToString(values[MaintObjectType_DESCEXT]);
            }

            if (values.Contains(STD_NOUN_MODIFIER))
            {
                model.StdNounModifier = Convert.ToString(values[STD_NOUN_MODIFIER]);
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

        public Object MaintObjectTypes_GetData(DataSourceLoadOptions loadOptions)
        {
            // Oh well, this causes a loop, so will need to find waht field is causing an objhect depth to exceed 32 or be cyclic. 
            // var dataSet = _context.Tag.AsQueryable();

            var dataSet = from rec in _context.MaintObjectType
                              //select new { rec.TagId, rec.TagNumber, rec.TagService, rec.TagFloc, rec.TagFlocDesc, rec.EngDiscId, rec.SubSystem.SubSystemNum };
                          select new { rec.MaintObjectTypeId, rec.MaintObjectTypeName, rec.MaintObjectTypeDesc, rec.MaintObjectTypeDescExt, rec.StdNounModifier };

            return DataSourceLoader.Load(dataSet, loadOptions);
        }

        [HttpPost]
        public IActionResult MaintObjectTypes_Insert(string values)
        {
            var newMaintObjectType = new MaintObjectType();
            JsonConvert.PopulateObject(values, newMaintObjectType);

            if (!TryValidateModel(newMaintObjectType))
                return BadRequest();

            _context.MaintObjectType.Add(newMaintObjectType);
            _context.SaveChanges();

            return Ok(newMaintObjectType);
        }


        [HttpPut]
        public IActionResult MaintObjectTypes_Update(int key, string values)
        {
            // TODO override to update tag state. 
            var order = _context.MaintObjectType.First(o => o.MaintObjectTypeId == key);
            JsonConvert.PopulateObject(values, order);

            if (!TryValidateModel(order))
                return BadRequest();

            _context.SaveChanges();

            return Ok(order);
        }

        [HttpDelete]
        public void MaintObjectTypes_Delete(int key)
        {
            var order = _context.MaintObjectType.First(o => o.MaintObjectTypeId == key);
            _context.MaintObjectType.Remove(order);
            _context.SaveChanges();
        }

        [HttpPost]
        public object MaintObjectTypes_Batch(List<DataChange> changes)
        {
            foreach (var change in changes)
            {
                MaintObjectType order;

                if (change.Type == "update" || change.Type == "remove")
                {
                    var key = Convert.ToInt32(change.Key);
                    order = _context.MaintObjectType.First(o => o.MaintObjectTypeId == key);
                }
                else
                {
                    order = new MaintObjectType();
                }

                if (change.Type == "insert" || change.Type == "update")
                {
                    JsonConvert.PopulateObject(change.Data.ToString(), order);

                    if (!TryValidateModel(order))
                        return BadRequest();

                    if (change.Type == "insert")
                    {
                        _context.MaintObjectType.Add(order);
                    }
                    change.Data = order;
                }
                else if (change.Type == "remove")
                {
                    _context.MaintObjectType.Remove(order);
                }
            }

            _context.SaveChanges();

            return Ok(changes);
        }
        public ActionResult ExportToExcel()
        {
            return View();
        }

        public Boolean ValidateName(MaintObjectType maintobjecttype)
        {
            var valid = _context.MaintObjectType.Any(x => x.MaintObjectTypeName == maintobjecttype.MaintObjectTypeName);
            return !valid;
        }
    }
}
