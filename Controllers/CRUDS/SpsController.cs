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
    public class SpsController : Controller
    {
        private readonly TagContext _context;

        public SpsController(TagContext context)
        {
            _context = context;
        }

        // GET: Sps

        public IActionResult Index(string columnSetsName = "Default")
        {
            Dictionary<string, ColParams> colIndex = new Dictionary<string, ColParams>();
            var col_customisations = _context.ColumnSets
                                        .Where(x => x.ColumnSetsEntity == "Sp")
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
                           .Where(x => x.EntityName == "Sp")
                           .Include(x => x.EntityAttributeRequirements);

            ViewData["SapValidationJson"] = JsonConvert.SerializeObject(EAId);

            // ViewBag required to update GridView properties and maintain state for selected view
            ViewBag.colIndex = colIndex;
            ViewBag.columnSetsName = columnSetsName;

            return View();
        }

        private void PopulateModel(Sp model, IDictionary values)
        {
            string Sp_ID = nameof(Sp.Spid);
            string Project_ID = nameof(Sp.ProjectId);
            string Sp_NUM = nameof(Sp.Spnum);
            string Sp_DESC = nameof(Sp.Spdesc);

            if (values.Contains(Sp_ID))
            {
                model.Spid = Convert.ToInt32(values[Sp_ID]);
            }

            if (values.Contains(Project_ID))
            {
                model.ProjectId = Convert.ToInt32(values[Project_ID]);
            }

            if (values.Contains(Sp_NUM))
            {
                model.Spnum = Convert.ToString(values[Sp_NUM]);
            }

            if (values.Contains(Sp_DESC))
            {
                model.Spdesc = Convert.ToString(values[Sp_DESC]);
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

        public Object Sps_GetData(DataSourceLoadOptions loadOptions)
        {
            // Oh well, this causes a loop, so will need to find waht field is causing an objhect depth to exceed 32 or be cyclic. 
            // var dataSet = _context.Tag.AsQueryable();

            var dataSet = from rec in _context.Sp
                              //select new { rec.TagId, rec.TagNumber, rec.TagService, rec.TagFloc, rec.TagFlocDesc, rec.EngDiscId, rec.SubSystem.SubSystemNum };
                          select new { rec.Spid, rec.ProjectId, rec.Spnum, rec.Spdesc };

            return DataSourceLoader.Load(dataSet, loadOptions);
        }

        [HttpPost]
        public IActionResult Sps_Insert(string values)
        {
            var newSp = new Sp();
            JsonConvert.PopulateObject(values, newSp);

            if (!TryValidateModel(newSp))
                return BadRequest();

            _context.Sp.Add(newSp);
            _context.SaveChanges();

            return Ok(newSp);
        }


        [HttpPut]
        public IActionResult Sps_Update(int key, string values)
        {
            // TODO override to update tag state. 
            var order = _context.Sp.First(o => o.Spid == key);
            JsonConvert.PopulateObject(values, order);

            if (!TryValidateModel(order))
                return BadRequest();

            _context.SaveChanges();

            return Ok(order);
        }

        [HttpDelete]
        public void Sps_Delete(int key)
        {
            var order = _context.Sp.First(o => o.Spid == key);
            _context.Sp.Remove(order);
            _context.SaveChanges();
        }

        [HttpPost]
        public object Sps_Batch(List<DataChange> changes)
        {
            foreach (var change in changes)
            {
                Sp order;

                if (change.Type == "update" || change.Type == "remove")
                {
                    var key = Convert.ToInt32(change.Key);
                    order = _context.Sp.First(o => o.Spid == key);
                }
                else
                {
                    order = new Sp();
                }

                if (change.Type == "insert" || change.Type == "update")
                {
                    JsonConvert.PopulateObject(change.Data.ToString(), order);

                    if (!TryValidateModel(order))
                        return BadRequest();

                    if (change.Type == "insert")
                    {
                        _context.Sp.Add(order);
                    }
                    change.Data = order;
                }
                else if (change.Type == "remove")
                {
                    _context.Sp.Remove(order);
                }
            }

            _context.SaveChanges();

            return Ok(changes);
        }
        public ActionResult ExportToExcel()
        {
            return View();
        }

        public Boolean ValidateName(Sp sp)
        {
            var valid = _context.Sp.Any(x => x.Spnum == sp.Spnum);
            return !valid;
        }
    }
}


