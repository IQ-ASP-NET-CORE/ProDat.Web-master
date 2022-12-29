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
    public class TaskListGroupsController : Controller
    {
        private readonly TagContext _context;

        public TaskListGroupsController(TagContext context)
        {
            _context = context;
        }

        // GET: TaskListGroups

        public async Task<IActionResult> Index(string columnSetsName = "Default")
        {
            Dictionary<string, ColParams> colIndex = new Dictionary<string, ColParams>();
            var col_customisations = _context.ColumnSets
                                        .Where(x => x.ColumnSetsEntity == "TaskListGroup")
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
                           .Where(x => x.EntityName == "TaskListGroup")
                           .Include(x => x.EntityAttributeRequirements);

            ViewData["SapValidationJson"] = JsonConvert.SerializeObject(EAId);

            // ViewBag required to update GridView properties and maintain state for selected view
            ViewBag.colIndex = colIndex;
            ViewBag.columnSetsName = columnSetsName;

            return View();
        }

        private void PopulateModel(TaskListGroup model, IDictionary values)
        {
            string TaskListGroup_ID = nameof(TaskListGroup.TaskListGroupId);
            string TaskListGroup_NAME = nameof(TaskListGroup.TaskListGroupName);
            string TaskListGroup_DESC = nameof(TaskListGroup.TaskListGroupDesc);

            if (values.Contains(TaskListGroup_ID))
            {
                model.TaskListGroupId = Convert.ToInt32(values[TaskListGroup_ID]);
            }

            if (values.Contains(TaskListGroup_NAME))
            {
                model.TaskListGroupName = Convert.ToString(values[TaskListGroup_NAME]);
            }

            if (values.Contains(TaskListGroup_DESC))
            {
                model.TaskListGroupDesc = Convert.ToString(values[TaskListGroup_DESC]);
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

        public Object TaskListGroups_GetData(DataSourceLoadOptions loadOptions)
        {
            // Oh well, this causes a loop, so will need to find waht field is causing an objhect depth to exceed 32 or be cyclic. 
            // var dataSet = _context.Tag.AsQueryable();

            var dataSet = from rec in _context.TaskListGroup
                              //select new { rec.TagId, rec.TagNumber, rec.TagService, rec.TagFloc, rec.TagFlocDesc, rec.EngDiscId, rec.SubSystem.SubSystemNum };
                          select new { rec.TaskListGroupId, rec.TaskListGroupName, rec.TaskListGroupDesc };

            return DataSourceLoader.Load(dataSet, loadOptions);
        }

        [HttpPost]
        public IActionResult TaskListGroups_Insert(string values)
        {
            var newTaskListGroup = new TaskListGroup();
            JsonConvert.PopulateObject(values, newTaskListGroup);

            if (!TryValidateModel(newTaskListGroup))
                return BadRequest();

            _context.TaskListGroup.Add(newTaskListGroup);
            _context.SaveChanges();

            return Ok(newTaskListGroup);
        }


        [HttpPut]
        public IActionResult TaskListGroups_Update(int key, string values)
        {
            // TODO override to update tag state. 
            var order = _context.TaskListGroup.First(o => o.TaskListGroupId == key);
            JsonConvert.PopulateObject(values, order);

            if (!TryValidateModel(order))
                return BadRequest();

            _context.SaveChanges();

            return Ok(order);
        }

        [HttpDelete]
        public void TaskListGroups_Delete(int key)
        {
            var order = _context.TaskListGroup.First(o => o.TaskListGroupId == key);
            _context.TaskListGroup.Remove(order);
            _context.SaveChanges();
        }

        [HttpPost]
        public object TaskListGroups_Batch(List<DataChange> changes)
        {
            foreach (var change in changes)
            {
                TaskListGroup order;

                if (change.Type == "update" || change.Type == "remove")
                {
                    var key = Convert.ToInt32(change.Key);
                    order = _context.TaskListGroup.First(o => o.TaskListGroupId == key);
                }
                else
                {
                    order = new TaskListGroup();
                }

                if (change.Type == "insert" || change.Type == "update")
                {
                    JsonConvert.PopulateObject(change.Data.ToString(), order);

                    if (!TryValidateModel(order))
                        return BadRequest();

                    if (change.Type == "insert")
                    {
                        _context.TaskListGroup.Add(order);
                    }
                    change.Data = order;
                }
                else if (change.Type == "remove")
                {
                    _context.TaskListGroup.Remove(order);
                }
            }

            _context.SaveChanges();

            return Ok(changes);
        }
        public ActionResult ExportToExcel()
        {
            return View();
        }

        public Boolean ValidateName(TaskListGroup tasklistgroup)
        {
            var valid = _context.TaskListGroup.Any(x => x.TaskListGroupName == tasklistgroup.TaskListGroupName);
            return !valid;
        }
    }
}