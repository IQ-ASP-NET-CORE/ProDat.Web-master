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
    public class TagViewsController : Controller
    {
        private readonly TagContext _context;

        public TagViewsController(TagContext context)
        {
            _context = context;
        }

        // GET: TagViews

        public async Task<IActionResult> Index(string columnSetsName = "Default")
        {
            Dictionary<string, ColParams> colIndex = new Dictionary<string, ColParams>();
            var col_customisations = _context.ColumnSets
                                        .Where(x => x.ColumnSetsEntity == "TagView")
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
                           .Where(x => x.EntityName == "TagView")
                           .Include(x => x.EntityAttributeRequirements);

            ViewData["SapValidationJson"] = JsonConvert.SerializeObject(EAId);

            // ViewBag required to update GridView properties and maintain state for selected view
            ViewBag.colIndex = colIndex;
            ViewBag.columnSetsName = columnSetsName;

            return View();
        }

        private void PopulateModel(TagView model, IDictionary values)
        {
            string TagView_ID = nameof(TagView.TagViewId);
            string TagView_NAME = nameof(TagView.TagViewName);
            string TagView_ACTIVE = nameof(TagView.Active);

            if (values.Contains(TagView_ID))
            {
                model.TagViewId = Convert.ToInt32(values[TagView_ID]);
            }

            if (values.Contains(TagView_NAME))
            {
                model.TagViewName = Convert.ToString(values[TagView_NAME]);
            }

            if (values.Contains(TagView_ACTIVE))
            {
                model.Active = Convert.ToBoolean(values[TagView_ACTIVE]);
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

        public Object TagViews_GetData(DataSourceLoadOptions loadOptions)
        {
            // Oh well, this causes a loop, so will need to find waht field is causing an objhect depth to exceed 32 or be cyclic. 
            // var dataSet = _context.Tag.AsQueryable();

            var dataSet = from rec in _context.TagView
                              //select new { rec.TagId, rec.TagNumber, rec.TagService, rec.TagFloc, rec.TagFlocDesc, rec.EngDiscId, rec.SubSystem.SubSystemNum };
                          select new { rec.TagViewId, rec.TagViewName, rec.Active };

            return DataSourceLoader.Load(dataSet, loadOptions);
        }

        [HttpPost]
        public IActionResult TagViews_Insert(string values)
        {
            var newTagView = new TagView();
            JsonConvert.PopulateObject(values, newTagView);

            if (!TryValidateModel(newTagView))
                return BadRequest();

            _context.TagView.Add(newTagView);
            _context.SaveChanges();

            return Ok(newTagView);
        }


        [HttpPut]
        public IActionResult TagViews_Update(int key, string values)
        {
            // TODO override to update tag state. 
            var order = _context.TagView.First(o => o.TagViewId == key);
            JsonConvert.PopulateObject(values, order);

            if (!TryValidateModel(order))
                return BadRequest();

            _context.SaveChanges();

            return Ok(order);
        }

        [HttpDelete]
        public void TagViews_Delete(int key)
        {
            var order = _context.TagView.First(o => o.TagViewId == key);
            _context.TagView.Remove(order);
            _context.SaveChanges();
        }

        [HttpPost]
        public object TagViews_Batch(List<DataChange> changes)
        {
            foreach (var change in changes)
            {
                TagView order;

                if (change.Type == "update" || change.Type == "remove")
                {
                    var key = Convert.ToInt32(change.Key);
                    order = _context.TagView.First(o => o.TagViewId == key);
                }
                else
                {
                    order = new TagView();
                }

                if (change.Type == "insert" || change.Type == "update")
                {
                    JsonConvert.PopulateObject(change.Data.ToString(), order);

                    if (!TryValidateModel(order))
                        return BadRequest();

                    if (change.Type == "insert")
                    {
                        _context.TagView.Add(order);
                    }
                    change.Data = order;
                }
                else if (change.Type == "remove")
                {
                    _context.TagView.Remove(order);
                }
            }

            _context.SaveChanges();

            return Ok(changes);
        }
        public ActionResult ExportToExcel()
        {
            return View();
        }

        public Boolean ValidateName(TagView tagview)
        {
            var valid = _context.TagView.Any(x => x.TagViewName == tagview.TagViewName);
            return !valid;
        }
    }
}