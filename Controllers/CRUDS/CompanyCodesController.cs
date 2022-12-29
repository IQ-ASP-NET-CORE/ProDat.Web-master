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
    public class CompanyCodesController : Controller
    {
        private readonly TagContext _context;

        public CompanyCodesController(TagContext context)
        {
            _context = context;
        }

        // GET: CompanyCodes

        public async Task<IActionResult> Index(string columnSetsName = "Default")
        {
            Dictionary<string, ColParams> colIndex = new Dictionary<string, ColParams>();
            var col_customisations = _context.ColumnSets
                                        .Where(x => x.ColumnSetsEntity == "CompanyCode")
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
                           .Where(x => x.EntityName == "CompanyCode")
                           .Include(x => x.EntityAttributeRequirements);

            ViewData["SapValidationJson"] = JsonConvert.SerializeObject(EAId);

            // ViewBag required to update GridView properties and maintain state for selected view
            ViewBag.colIndex = colIndex;
            ViewBag.columnSetsName = columnSetsName;

            return View();
        }

        private void PopulateModel(CompanyCode model, IDictionary values)
        {
            string CompanyCode_ID = nameof(CompanyCode.CompanyCodeId);
            string CompanyCode_NAME = nameof(CompanyCode.CompanyCodeName);
            string CompanyCode_DESC = nameof(CompanyCode.CompanyCodeDesc);

            if (values.Contains(CompanyCode_ID))
            {
                model.CompanyCodeId = Convert.ToInt32(values[CompanyCode_ID]);
            }

            if (values.Contains(CompanyCode_NAME))
            {
                model.CompanyCodeName = Convert.ToString(values[CompanyCode_NAME]);
            }

            if (values.Contains(CompanyCode_DESC))
            {
                model.CompanyCodeDesc = Convert.ToString(values[CompanyCode_DESC]);
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

        public Object CompanyCodes_GetData(DataSourceLoadOptions loadOptions)
        {
            // Oh well, this causes a loop, so will need to find waht field is causing an objhect depth to exceed 32 or be cyclic. 
            // var dataSet = _context.Tag.AsQueryable();

            var dataSet = from rec in _context.CompanyCode
                              //select new { rec.TagId, rec.TagNumber, rec.TagService, rec.TagFloc, rec.TagFlocDesc, rec.EngDiscId, rec.SubSystem.SubSystemNum };
                          select new { rec.CompanyCodeId, rec.CompanyCodeName, rec.CompanyCodeDesc };

            return DataSourceLoader.Load(dataSet, loadOptions);
        }

        [HttpPost]
        public IActionResult CompanyCodes_Insert(string values)
        {
            var newCompanyCode = new CompanyCode();
            JsonConvert.PopulateObject(values, newCompanyCode);

            if (!TryValidateModel(newCompanyCode))
                return BadRequest();

            _context.CompanyCode.Add(newCompanyCode);
            _context.SaveChanges();

            return Ok(newCompanyCode);
        }


        [HttpPut]
        public IActionResult CompanyCodes_Update(int key, string values)
        {
            // TODO override to update tag state. 
            var order = _context.CompanyCode.First(o => o.CompanyCodeId == key);
            JsonConvert.PopulateObject(values, order);

            if (!TryValidateModel(order))
                return BadRequest();

            _context.SaveChanges();

            return Ok(order);
        }

        [HttpDelete]
        public void CompanyCodes_Delete(int key)
        {
            var order = _context.CompanyCode.First(o => o.CompanyCodeId == key);
            _context.CompanyCode.Remove(order);
            _context.SaveChanges();
        }

        [HttpPost]
        public object CompanyCodes_Batch(List<DataChange> changes)
        {
            foreach (var change in changes)
            {
                CompanyCode order;

                if (change.Type == "update" || change.Type == "remove")
                {
                    var key = Convert.ToInt32(change.Key);
                    order = _context.CompanyCode.First(o => o.CompanyCodeId == key);
                }
                else
                {
                    order = new CompanyCode();
                }

                if (change.Type == "insert" || change.Type == "update")
                {
                    JsonConvert.PopulateObject(change.Data.ToString(), order);

                    if (!TryValidateModel(order))
                        return BadRequest();

                    if (change.Type == "insert")
                    {
                        _context.CompanyCode.Add(order);
                    }
                    change.Data = order;
                }
                else if (change.Type == "remove")
                {
                    _context.CompanyCode.Remove(order);
                }
            }

            _context.SaveChanges();

            return Ok(changes);
        }
        public ActionResult ExportToExcel()
        {
            return View();
        }
        public Boolean ValidateName(CompanyCode companycode)
        {
            var valid = _context.CompanyCode.Any(x => x.CompanyCodeName == companycode.CompanyCodeName);
            return !valid;
        }
    }
}
