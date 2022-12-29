﻿using System;
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
    public class MaintWorkCentresController : Controller
    {
        private readonly TagContext _context;

        public MaintWorkCentresController(TagContext context)
        {
            _context = context;
        }

        // GET: MaintWorkCentres

        public async Task<IActionResult> Index(string columnSetsName = "Default")
        {
            Dictionary<string, ColParams> colIndex = new Dictionary<string, ColParams>();
            var col_customisations = _context.ColumnSets
                                        .Where(x => x.ColumnSetsEntity == "MaintWorkCentre")
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
                           .Where(x => x.EntityName == "MaintWorkCentre")
                           .Include(x => x.EntityAttributeRequirements);

            ViewData["SapValidationJson"] = JsonConvert.SerializeObject(EAId);

            // ViewBag required to update GridView properties and maintain state for selected view
            ViewBag.colIndex = colIndex;
            ViewBag.columnSetsName = columnSetsName;

            return View();
        }

        private void PopulateModel(MaintWorkCentre model, IDictionary values)
        {
            string MaintWorkCentre_ID = nameof(MaintWorkCentre.MaintWorkCentreId);
            string MaintWorkCentre_NAME = nameof(MaintWorkCentre.MaintWorkCentreName);
            string MaintWorkCentre_DESC = nameof(MaintWorkCentre.MaintWorkCentreDesc);

            if (values.Contains(MaintWorkCentre_ID))
            {
                model.MaintWorkCentreId = Convert.ToInt32(values[MaintWorkCentre_ID]);
            }

            if (values.Contains(MaintWorkCentre_NAME))
            {
                model.MaintWorkCentreName = Convert.ToString(values[MaintWorkCentre_NAME]);
            }

            if (values.Contains(MaintWorkCentre_DESC))
            {
                model.MaintWorkCentreDesc = Convert.ToString(values[MaintWorkCentre_DESC]);
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

        public Object MaintWorkCentres_GetData(DataSourceLoadOptions loadOptions)
        {
            // Oh well, this causes a loop, so will need to find waht field is causing an objhect depth to exceed 32 or be cyclic. 
            // var dataSet = _context.Tag.AsQueryable();

            var dataSet = from rec in _context.MaintWorkCentre
                              //select new { rec.TagId, rec.TagNumber, rec.TagService, rec.TagFloc, rec.TagFlocDesc, rec.EngDiscId, rec.SubSystem.SubSystemNum };
                          select new { rec.MaintWorkCentreId, rec.MaintWorkCentreName, rec.MaintWorkCentreDesc };

            return DataSourceLoader.Load(dataSet, loadOptions);
        }

        [HttpPost]
        public IActionResult MaintWorkCentres_Insert(string values)
        {
            var newMaintWorkCentre = new MaintWorkCentre();
            JsonConvert.PopulateObject(values, newMaintWorkCentre);

            if (!TryValidateModel(newMaintWorkCentre))
                return BadRequest();

            _context.MaintWorkCentre.Add(newMaintWorkCentre);
            _context.SaveChanges();

            return Ok(newMaintWorkCentre);
        }


        [HttpPut]
        public IActionResult MaintWorkCentres_Update(int key, string values)
        {
            // TODO override to update tag state. 
            var order = _context.MaintWorkCentre.First(o => o.MaintWorkCentreId == key);
            JsonConvert.PopulateObject(values, order);

            if (!TryValidateModel(order))
                return BadRequest();

            _context.SaveChanges();

            return Ok(order);
        }

        [HttpDelete]
        public void MaintWorkCentres_Delete(int key)
        {
            var order = _context.MaintWorkCentre.First(o => o.MaintWorkCentreId == key);
            _context.MaintWorkCentre.Remove(order);
            _context.SaveChanges();
        }

        [HttpPost]
        public object MaintWorkCentres_Batch(List<DataChange> changes)
        {
            foreach (var change in changes)
            {
                MaintWorkCentre order;

                if (change.Type == "update" || change.Type == "remove")
                {
                    var key = Convert.ToInt32(change.Key);
                    order = _context.MaintWorkCentre.First(o => o.MaintWorkCentreId == key);
                }
                else
                {
                    order = new MaintWorkCentre();
                }

                if (change.Type == "insert" || change.Type == "update")
                {
                    JsonConvert.PopulateObject(change.Data.ToString(), order);

                    if (!TryValidateModel(order))
                        return BadRequest();

                    if (change.Type == "insert")
                    {
                        _context.MaintWorkCentre.Add(order);
                    }
                    change.Data = order;
                }
                else if (change.Type == "remove")
                {
                    _context.MaintWorkCentre.Remove(order);
                }
            }

            _context.SaveChanges();

            return Ok(changes);
        }
        public ActionResult ExportToExcel()
        {
            return View();
        }

        public Boolean ValidateName(MaintWorkCentre maintworkcent)
        {
            var valid = _context.MaintWorkCentre.Any(x => x.MaintWorkCentreName == maintworkcent.MaintWorkCentreName);
            return !valid;
        }
    }
}
