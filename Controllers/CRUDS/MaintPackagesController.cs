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
    public class MaintPackagesController : Controller
    {
        private readonly TagContext _context;

        public MaintPackagesController(TagContext context)
        {
            _context = context;
        }

        // GET: MaintPackages

        public async Task<IActionResult> Index(string columnSetsName = "Default")
        {
            Dictionary<string, ColParams> colIndex = new Dictionary<string, ColParams>();
            var col_customisations = _context.ColumnSets
                                        .Where(x => x.ColumnSetsEntity == "MaintPackage")
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
                           .Where(x => x.EntityName == "MaintPackage")
                           .Include(x => x.EntityAttributeRequirements);

            ViewData["SapValidationJson"] = JsonConvert.SerializeObject(EAId);

            // ViewBag required to update GridView properties and maintain state for selected view
            ViewBag.colIndex = colIndex;
            ViewBag.columnSetsName = columnSetsName;

            return View();
        }

        private void PopulateModel(MaintPackage model, IDictionary values)
        {
            string MAINTPACKAGE_ID = nameof(MaintPackage.MaintPackageId);
            string MAINTPACKAGE_NAME = nameof(MaintPackage.MaintPackageName);
            string MAINTPACKAGE_CYCLE_LENGTH = nameof(MaintPackage.MaintPackageCycleLength);
            string MAINTPACKAGE_CYCLE_UNIT = nameof(MaintPackage.MaintPackageCycleUnit);
            string MAINTPACKAGE_CYCLE_TEXT = nameof(MaintPackage.MaintPackageCycleText);

            if (values.Contains(MAINTPACKAGE_ID))
            {
                model.MaintPackageId = Convert.ToInt32(values[MAINTPACKAGE_ID]);
            }

            if (values.Contains(MAINTPACKAGE_NAME))
            {
                model.MaintPackageName = Convert.ToString(values[MAINTPACKAGE_NAME]);
            }

            if (values.Contains(MAINTPACKAGE_CYCLE_LENGTH))
            {
                model.MaintPackageCycleLength = Convert.ToInt32(values[MAINTPACKAGE_CYCLE_LENGTH]);
            }

            if (values.Contains(MAINTPACKAGE_CYCLE_UNIT))
            {
                model.MaintPackageCycleUnit = Convert.ToString(values[MAINTPACKAGE_CYCLE_UNIT]);
            }

            if (values.Contains(MAINTPACKAGE_CYCLE_TEXT))
            {
                model.MaintPackageCycleText = Convert.ToString(values[MAINTPACKAGE_CYCLE_TEXT]);
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

        public Object MaintPackages_GetData(DataSourceLoadOptions loadOptions)
        {
            // Oh well, this causes a loop, so will need to find waht field is causing an objhect depth to exceed 32 or be cyclic. 
            // var dataSet = _context.Tag.AsQueryable();

            var dataSet = from rec in _context.MaintPackage
                              //select new { rec.TagId, rec.TagNumber, rec.TagService, rec.TagFloc, rec.TagFlocDesc, rec.EngDiscId, rec.SubSystem.SubSystemNum };
                          select new { rec.MaintPackageId, rec.MaintPackageName, rec.MaintPackageCycleLength, rec.MaintPackageCycleUnit, rec.MaintPackageCycleText };

            return DataSourceLoader.Load(dataSet, loadOptions);
        }

        [HttpPost]
        public IActionResult MaintPackages_Insert(string values)
        {
            var newMaintPackage = new MaintPackage();
            JsonConvert.PopulateObject(values, newMaintPackage);

            if (!TryValidateModel(newMaintPackage))
                return BadRequest();

            _context.MaintPackage.Add(newMaintPackage);
            _context.SaveChanges();

            return Ok(newMaintPackage);
        }


        [HttpPut]
        public IActionResult MaintPackages_Update(int key, string values)
        {
            // TODO override to update tag state. 
            var order = _context.MaintPackage.First(o => o.MaintPackageId == key);
            JsonConvert.PopulateObject(values, order);

            if (!TryValidateModel(order))
                return BadRequest();

            _context.SaveChanges();

            return Ok(order);
        }

        [HttpDelete]
        public void MaintPackages_Delete(int key)
        {
            var order = _context.MaintPackage.First(o => o.MaintPackageId == key);
            _context.MaintPackage.Remove(order);
            _context.SaveChanges();
        }

        [HttpPost]
        public object MaintPackages_Batch(List<DataChange> changes)
        {
            foreach (var change in changes)
            {
                MaintPackage order;

                if (change.Type == "update" || change.Type == "remove")
                {
                    var key = Convert.ToInt32(change.Key);
                    order = _context.MaintPackage.First(o => o.MaintPackageId == key);
                }
                else
                {
                    order = new MaintPackage();
                }

                if (change.Type == "insert" || change.Type == "update")
                {
                    JsonConvert.PopulateObject(change.Data.ToString(), order);

                    if (!TryValidateModel(order))
                        return BadRequest();

                    if (change.Type == "insert")
                    {
                        _context.MaintPackage.Add(order);
                    }
                    change.Data = order;
                }
                else if (change.Type == "remove")
                {
                    _context.MaintPackage.Remove(order);
                }
            }

            _context.SaveChanges();

            return Ok(changes);
        }
        public ActionResult ExportToExcel()
        {
            return View();
        }

        public Boolean ValidateName(MaintPackage maintpackage)
        {
            var valid = _context.MaintPackage.Any(x => x.MaintPackageName == maintpackage.MaintPackageName);
            return !valid;
        }
    }
}
