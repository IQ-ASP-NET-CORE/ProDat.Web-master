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
    public class RelationshipTypesController : Controller
    {
        private readonly TagContext _context;

        public RelationshipTypesController(TagContext context)
        {
            _context = context;
        }

        // GET: RelationshipTypes

        public IActionResult Index(string columnSetsName = "Default")
        {
            Dictionary<string, ColParams> colIndex = new Dictionary<string, ColParams>();
            var col_customisations = _context.ColumnSets
                                        .Where(x => x.ColumnSetsEntity == "RelationshipType")
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
                           .Where(x => x.EntityName == "RelationshipType")
                           .Include(x => x.EntityAttributeRequirements);

            ViewData["SapValidationJson"] = JsonConvert.SerializeObject(EAId);

            // ViewBag required to update GridView properties and maintain state for selected view
            ViewBag.colIndex = colIndex;
            ViewBag.columnSetsName = columnSetsName;

            return View();
        }

        private void PopulateModel(RelationshipType model, IDictionary values)
        {
            string RelationshipType_ID = nameof(RelationshipType.RelationshipTypeId);
            string RelationshipType_NAME = nameof(RelationshipType.RelationshipTypeName);
            string RelationshipType_DESC = nameof(RelationshipType.RelationshipTypeDesc);

            if (values.Contains(RelationshipType_ID))
            {
                model.RelationshipTypeId = Convert.ToInt32(values[RelationshipType_ID]);
            }

            if (values.Contains(RelationshipType_NAME))
            {
                model.RelationshipTypeName = Convert.ToString(values[RelationshipType_NAME]);
            }

            if (values.Contains(RelationshipType_DESC))
            {
                model.RelationshipTypeDesc = Convert.ToString(values[RelationshipType_DESC]);
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

        public Object RelationshipTypes_GetData(DataSourceLoadOptions loadOptions)
        {
            // Oh well, this causes a loop, so will need to find waht field is causing an objhect depth to exceed 32 or be cyclic. 
            // var dataSet = _context.Tag.AsQueryable();

            var dataSet = from rec in _context.RelationshipType
                              //select new { rec.TagId, rec.TagNumber, rec.TagService, rec.TagFloc, rec.TagFlocDesc, rec.EngDiscId, rec.SubSystem.SubSystemNum };
                          select new { rec.RelationshipTypeId, rec.RelationshipTypeName, rec.RelationshipTypeDesc };

            return DataSourceLoader.Load(dataSet, loadOptions);
        }

        [HttpPost]
        public IActionResult RelationshipTypes_Insert(string values)
        {
            var newRelationshipType = new RelationshipType();
            JsonConvert.PopulateObject(values, newRelationshipType);

            if (!TryValidateModel(newRelationshipType))
                return BadRequest();

            _context.RelationshipType.Add(newRelationshipType);
            _context.SaveChanges();

            return Ok(newRelationshipType);
        }


        [HttpPut]
        public IActionResult RelationshipTypes_Update(int key, string values)
        {
            // TODO override to update tag state. 
            var order = _context.RelationshipType.First(o => o.RelationshipTypeId == key);
            JsonConvert.PopulateObject(values, order);

            if (!TryValidateModel(order))
                return BadRequest();

            _context.SaveChanges();

            return Ok(order);
        }

        [HttpDelete]
        public void RelationshipTypes_Delete(int key)
        {
            var order = _context.RelationshipType.First(o => o.RelationshipTypeId == key);
            _context.RelationshipType.Remove(order);
            _context.SaveChanges();
        }

        [HttpPost]
        public object RelationshipTypes_Batch(List<DataChange> changes)
        {
            foreach (var change in changes)
            {
                RelationshipType order;

                if (change.Type == "update" || change.Type == "remove")
                {
                    var key = Convert.ToInt32(change.Key);
                    order = _context.RelationshipType.First(o => o.RelationshipTypeId == key);
                }
                else
                {
                    order = new RelationshipType();
                }

                if (change.Type == "insert" || change.Type == "update")
                {
                    JsonConvert.PopulateObject(change.Data.ToString(), order);

                    if (!TryValidateModel(order))
                        return BadRequest();

                    if (change.Type == "insert")
                    {
                        _context.RelationshipType.Add(order);
                    }
                    change.Data = order;
                }
                else if (change.Type == "remove")
                {
                    _context.RelationshipType.Remove(order);
                }
            }

            _context.SaveChanges();

            return Ok(changes);
        }
        public ActionResult ExportToExcel()
        {
            return View();
        }

        public Boolean ValidateName(RelationshipType relationshiptype)
        {
            var valid = _context.RelationshipType.Any(x => x.RelationshipTypeName == relationshiptype.RelationshipTypeName);
            return !valid;
        }
    }
}
