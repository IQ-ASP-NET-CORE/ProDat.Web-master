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
    public class MaintPlannerGroupsController : Controller
    {
        private readonly TagContext _context;

        public MaintPlannerGroupsController(TagContext context)
        {
            _context = context;
        }

        // GET: MaintPlannerGroups

        public async Task<IActionResult> Index(string columnSetsName = "Default")
        {
            Dictionary<string, ColParams> colIndex = new Dictionary<string, ColParams>();
            var col_customisations = _context.ColumnSets
                                        .Where(x => x.ColumnSetsEntity == "MaintPlannerGroup")
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
                           .Where(x => x.EntityName == "MaintPlannerGroup")
                           .Include(x => x.EntityAttributeRequirements);

            ViewData["SapValidationJson"] = JsonConvert.SerializeObject(EAId);

            // ViewBag required to update GridView properties and maintain state for selected view
            ViewBag.colIndex = colIndex;
            ViewBag.columnSetsName = columnSetsName;

            return View();
        }

        private void PopulateModel(MaintPlannerGroup model, IDictionary values)
        {
            string MaintPlannerGroup_ID = nameof(MaintPlannerGroup.MaintPlannerGroupId);
            string MaintPlannerGroup_NAME = nameof(MaintPlannerGroup.MaintPlannerGroupName);
            string MaintPlannerGroup_DESC = nameof(MaintPlannerGroup.MaintPlannerGroupDesc);

            if (values.Contains(MaintPlannerGroup_ID))
            {
                model.MaintPlannerGroupId = Convert.ToInt32(values[MaintPlannerGroup_ID]);
            }

            if (values.Contains(MaintPlannerGroup_NAME))
            {
                model.MaintPlannerGroupName = Convert.ToString(values[MaintPlannerGroup_NAME]);
            }

            if (values.Contains(MaintPlannerGroup_DESC))
            {
                model.MaintPlannerGroupDesc = Convert.ToString(values[MaintPlannerGroup_DESC]);
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

        public Object MaintPlannerGroups_GetData(DataSourceLoadOptions loadOptions)
        {
            // Oh well, this causes a loop, so will need to find waht field is causing an objhect depth to exceed 32 or be cyclic. 
            // var dataSet = _context.Tag.AsQueryable();

            var dataSet = from rec in _context.MaintPlannerGroup
                              //select new { rec.TagId, rec.TagNumber, rec.TagService, rec.TagFloc, rec.TagFlocDesc, rec.EngDiscId, rec.SubSystem.SubSystemNum };
                          select new { rec.MaintPlannerGroupId, rec.MaintPlannerGroupName, rec.MaintPlannerGroupDesc };

            return DataSourceLoader.Load(dataSet, loadOptions);
        }

        [HttpPost]
        public IActionResult MaintPlannerGroups_Insert(string values)
        {
            var newMaintPlannerGroup = new MaintPlannerGroup();
            JsonConvert.PopulateObject(values, newMaintPlannerGroup);

            if (!TryValidateModel(newMaintPlannerGroup))
                return BadRequest();

            _context.MaintPlannerGroup.Add(newMaintPlannerGroup);
            _context.SaveChanges();

            return Ok(newMaintPlannerGroup);
        }


        [HttpPut]
        public IActionResult MaintPlannerGroups_Update(int key, string values)
        {
            // TODO override to update tag state. 
            var order = _context.MaintPlannerGroup.First(o => o.MaintPlannerGroupId == key);
            JsonConvert.PopulateObject(values, order);

            if (!TryValidateModel(order))
                return BadRequest();

            _context.SaveChanges();

            return Ok(order);
        }

        [HttpDelete]
        public void MaintPlannerGroups_Delete(int key)
        {
            var order = _context.MaintPlannerGroup.First(o => o.MaintPlannerGroupId == key);
            _context.MaintPlannerGroup.Remove(order);
            _context.SaveChanges();
        }

        [HttpPost]
        public object MaintPlannerGroups_Batch(List<DataChange> changes)
        {
            foreach (var change in changes)
            {
                MaintPlannerGroup order;

                if (change.Type == "update" || change.Type == "remove")
                {
                    var key = Convert.ToInt32(change.Key);
                    order = _context.MaintPlannerGroup.First(o => o.MaintPlannerGroupId == key);
                }
                else
                {
                    order = new MaintPlannerGroup();
                }

                if (change.Type == "insert" || change.Type == "update")
                {
                    JsonConvert.PopulateObject(change.Data.ToString(), order);

                    if (!TryValidateModel(order))
                        return BadRequest();

                    if (change.Type == "insert")
                    {
                        _context.MaintPlannerGroup.Add(order);
                    }
                    change.Data = order;
                }
                else if (change.Type == "remove")
                {
                    _context.MaintPlannerGroup.Remove(order);
                }
            }

            _context.SaveChanges();

            return Ok(changes);
        }
        public ActionResult ExportToExcel()
        {
            return View();
        }

        public Boolean ValidateName(MaintPlannerGroup maintplannergroup)
        {
            var valid = _context.MaintPlannerGroup.Any(x => x.MaintPlannerGroupName == maintplannergroup.MaintPlannerGroupName);
            return !valid;
        }
    }
}
