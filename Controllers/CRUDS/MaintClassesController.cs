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
    public class MaintClassesController : Controller
    {
        private readonly TagContext _context;

        public MaintClassesController(TagContext context)
        {
            _context = context;
        }

        // GET: MaintClasses

        public async Task<IActionResult> Index(string columnSetsName = "Default")
        {
            Dictionary<string, ColParams> colIndex = new Dictionary<string, ColParams>();
            var col_customisations = _context.ColumnSets
                                        .Where(x => x.ColumnSetsEntity == "MaintClass")
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
                           .Where(x => x.EntityName == "MaintClass")
                           .Include(x => x.EntityAttributeRequirements);

            ViewData["SapValidationJson"] = JsonConvert.SerializeObject(EAId);

            // ViewBag required to update GridView properties and maintain state for selected view
            ViewBag.colIndex = colIndex;
            ViewBag.columnSetsName = columnSetsName;

            return View();
        }

        private void PopulateModel(MaintClass model, IDictionary values)
        {
            string MaintClass_ID = nameof(MaintClass.MaintClassId);
            string MaintClass_NAME = nameof(MaintClass.MaintClassName);
            string MaintClass_DESC = nameof(MaintClass.MaintClassDesc);

            if (values.Contains(MaintClass_ID))
            {
                model.MaintClassId = Convert.ToInt32(values[MaintClass_ID]);
            }

            if (values.Contains(MaintClass_NAME))
            {
                model.MaintClassName = Convert.ToString(values[MaintClass_NAME]);
            }

            if (values.Contains(MaintClass_DESC))
            {
                model.MaintClassDesc = Convert.ToString(values[MaintClass_DESC]);
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

        public Object MaintClasses_GetData(DataSourceLoadOptions loadOptions)
        {
            // Oh well, this causes a loop, so will need to find waht field is causing an objhect depth to exceed 32 or be cyclic. 
            // var dataSet = _context.Tag.AsQueryable();

            var dataSet = from rec in _context.MaintClass
                              //select new { rec.TagId, rec.TagNumber, rec.TagService, rec.TagFloc, rec.TagFlocDesc, rec.EngDiscId, rec.SubSystem.SubSystemNum };
                          select new { rec.MaintClassId, rec.MaintClassName, rec.MaintClassDesc };

            return DataSourceLoader.Load(dataSet, loadOptions);
        }

        [HttpPost]
        public IActionResult MaintClasses_Insert(string values)
        {
            var newMaintClass = new MaintClass();
            JsonConvert.PopulateObject(values, newMaintClass);

            if (!TryValidateModel(newMaintClass))
                return BadRequest();

            _context.MaintClass.Add(newMaintClass);
            _context.SaveChanges();

            return Ok(newMaintClass);
        }


        [HttpPut]
        public IActionResult MaintClasses_Update(int key, string values)
        {
            // TODO override to update tag state. 
            var order = _context.MaintClass.First(o => o.MaintClassId == key);
            JsonConvert.PopulateObject(values, order);

            if (!TryValidateModel(order))
                return BadRequest();

            _context.SaveChanges();

            return Ok(order);
        }

        [HttpDelete]
        public void MaintClasses_Delete(int key)
        {
            var order = _context.MaintClass.First(o => o.MaintClassId == key);
            _context.MaintClass.Remove(order);
            _context.SaveChanges();
        }

        [HttpPost]
        public object MaintClasses_Batch(List<DataChange> changes)
        {
            foreach (var change in changes)
            {
                MaintClass order;

                if (change.Type == "update" || change.Type == "remove")
                {
                    var key = Convert.ToInt32(change.Key);
                    order = _context.MaintClass.First(o => o.MaintClassId == key);
                }
                else
                {
                    order = new MaintClass();
                }

                if (change.Type == "insert" || change.Type == "update")
                {
                    JsonConvert.PopulateObject(change.Data.ToString(), order);

                    if (!TryValidateModel(order))
                        return BadRequest();

                    if (change.Type == "insert")
                    {
                        _context.MaintClass.Add(order);
                    }
                    change.Data = order;
                }
                else if (change.Type == "remove")
                {
                    _context.MaintClass.Remove(order);
                }
            }

            _context.SaveChanges();

            return Ok(changes);
        }
        public ActionResult ExportToExcel()
        {
            return View();
        }
        public Boolean ValidateName(MaintClass maintclass)
        {
            var valid = _context.MaintClass.Any(x => x.MaintClassName == maintclass.MaintClassName);
            return !valid;
        }
    }
}
