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
    public class ProjectsController : Controller
    {
        private readonly TagContext _context;

        public ProjectsController(TagContext context)
        {
            _context = context;
        }

        // GET: Projects

        public async Task<IActionResult> Index(string columnSetsName = "Default")
        {
            Dictionary<string, ColParams> colIndex = new Dictionary<string, ColParams>();
            var col_customisations = _context.ColumnSets
                                        .Where(x => x.ColumnSetsEntity == "Project")
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
                           .Where(x => x.EntityName == "Project")
                           .Include(x => x.EntityAttributeRequirements);

            ViewData["SapValidationJson"] = JsonConvert.SerializeObject(EAId);

            // ViewBag required to update GridView properties and maintain state for selected view
            ViewBag.colIndex = colIndex;
            ViewBag.columnSetsName = columnSetsName;

            return View();
        }

        private void PopulateModel(Project model, IDictionary values)
        {
            string Project_ID = nameof(Project.ProjectId);
            string MaintenancePlant_ID = nameof(Project.MaintenancePlantId);
            string Project_NAME = nameof(Project.ProjectName);
            string Project_CODE = nameof(Project.ProjectCode);
            string MaintenanceRootTag_ID = nameof(Project.MaintenanceRootTagId);
            string MaintHierarchy_LOADDEPTH = nameof(Project.MaintHierarchy_LoadDepth);

            if (values.Contains(Project_ID))
            {
                model.ProjectId = Convert.ToInt32(values[Project_ID]);
            }

            if (values.Contains(MaintenancePlant_ID))
            {
                model.MaintenancePlantId = Convert.ToInt32(values[MaintenancePlant_ID]);
            }

            if (values.Contains(Project_NAME))
            {
                model.ProjectName = Convert.ToString(values[Project_NAME]);
            }

            if (values.Contains(Project_CODE))
            {
                model.ProjectCode = Convert.ToString(values[Project_CODE]);
            }

            if (values.Contains(MaintenanceRootTag_ID))
            {
                model.MaintenanceRootTagId = Convert.ToInt32(values[MaintenanceRootTag_ID]);
            }

            if (values.Contains(MaintHierarchy_LOADDEPTH))
            {
                model.MaintHierarchy_LoadDepth = Convert.ToInt32(values[MaintHierarchy_LOADDEPTH]);
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

        public Object Projects_GetData(DataSourceLoadOptions loadOptions)
        {
            // Oh well, this causes a loop, so will need to find waht field is causing an objhect depth to exceed 32 or be cyclic. 
            // var dataSet = _context.Tag.AsQueryable();

            var dataSet = from rec in _context.Project
                              //select new { rec.TagId, rec.TagNumber, rec.TagService, rec.TagFloc, rec.TagFlocDesc, rec.EngDiscId, rec.SubSystem.SubSystemNum };
                          select new { rec.ProjectId, rec.ProjectCode, rec.ProjectName, rec.MaintenanceRootTagId, rec.MaintHierarchy_LoadDepth, rec.MaintenancePlantId };

            return DataSourceLoader.Load(dataSet, loadOptions);
        }

        [HttpPost]
        public IActionResult Projects_Insert(string values)
        {
            var newProject = new Project();
            JsonConvert.PopulateObject(values, newProject);

            if (!TryValidateModel(newProject))
                return BadRequest();

            _context.Project.Add(newProject);
            _context.SaveChanges();

            return Ok(newProject);
        }


        [HttpPut]
        public IActionResult Projects_Update(int key, string values)
        {
            // TODO override to update tag state. 
            var order = _context.Project.First(o => o.ProjectId == key);
            JsonConvert.PopulateObject(values, order);

            if (!TryValidateModel(order))
                return BadRequest();

            _context.SaveChanges();

            return Ok(order);
        }

        [HttpDelete]
        public void Projects_Delete(int key)
        {
            var order = _context.Project.First(o => o.ProjectId == key);
            _context.Project.Remove(order);
            _context.SaveChanges();
        }

        [HttpPost]
        public object Projects_Batch(List<DataChange> changes)
        {
            foreach (var change in changes)
            {
                Project order;

                if (change.Type == "update" || change.Type == "remove")
                {
                    var key = Convert.ToInt32(change.Key);
                    order = _context.Project.First(o => o.ProjectId == key);
                }
                else
                {
                    order = new Project();
                }

                if (change.Type == "insert" || change.Type == "update")
                {
                    JsonConvert.PopulateObject(change.Data.ToString(), order);

                    if (!TryValidateModel(order))
                        return BadRequest();

                    if (change.Type == "insert")
                    {
                        _context.Project.Add(order);
                    }
                    change.Data = order;
                }
                else if (change.Type == "remove")
                {
                    _context.Project.Remove(order);
                }
            }

            _context.SaveChanges();

            return Ok(changes);
        }
        public ActionResult ExportToExcel()
        {
            return View();
        }

        public Boolean ValidateCode(Project project)
        {
            var valid = _context.Project.Any(x => x.ProjectCode == project.ProjectCode);
            return !valid;
        }
    }
}