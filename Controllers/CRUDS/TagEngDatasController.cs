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
    public class TagEngDatasController : Controller
    {
        private readonly TagContext _context;

        public TagEngDatasController(TagContext context)
        {
            _context = context;
        }

        // GET: TagEngDatas

        public async Task<IActionResult> Index(int id, string columnSetsName = "Default")
        {
            Dictionary<string, ColParams> colIndex = new Dictionary<string, ColParams>();
            var col_customisations = _context.ColumnSets
                                        .Where(x => x.ColumnSetsEntity == "TagEngData")
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
                           .Where(x => x.EntityName == "TagEngData")
                           .Include(x => x.EntityAttributeRequirements);

            ViewData["SapValidationJson"] = JsonConvert.SerializeObject(EAId);

            // ViewBag required to update GridView properties and maintain state for selected view
            ViewBag.colIndex = colIndex;
            ViewBag.columnSetsName = columnSetsName;

            var t = _context.Tag.Where(x => x.TagId == id).FirstOrDefault();
            ViewBag.TagId = id;
            ViewBag.TagNumDesc = t.TagNumber + " " + t.TagFlocDesc;

            return View();
        }

        private void PopulateModel(TagEngData model, IDictionary values)
        {
            string TagEngData_ID = nameof(TagEngData.TagId);
            string EngDataCode_ID = nameof(TagEngData.EngDataCodeId);
            string TagEngData_VALUE = nameof(TagEngData.EngDatavalue);
            string TagEngData_SOURCE = nameof(TagEngData.EngDatasource);
            string TagEngData_COMMENT = nameof(TagEngData.EngDataComment);

            if (values.Contains(TagEngData_ID))
            {
                model.TagId = Convert.ToInt32(values[TagEngData_ID]);
            }

            if (values.Contains(EngDataCode_ID))
            {
                model.EngDataCodeId = Convert.ToInt32(values[EngDataCode_ID]);
            }

            if (values.Contains(TagEngData_VALUE))
            {
                model.EngDatavalue = Convert.ToString(values[TagEngData_VALUE]);
            }

            if (values.Contains(TagEngData_SOURCE))
            {
                model.EngDatasource = Convert.ToString(values[TagEngData_SOURCE]);
            }

            if (values.Contains(TagEngData_COMMENT))
            {
                model.EngDataComment = Convert.ToString(values[TagEngData_COMMENT]);
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

        public Object TagEngDatas_GetData(DataSourceLoadOptions loadOptions, int TagId)
        {
            if (TagId > 0)
            {
                var dataSet = from rec in _context.TagEngData
                              where rec.TagId == TagId
                              select new { rec.TagId, rec.EngDataCodeId, rec.EngDatavalue, rec.EngDatasource, rec.EngDataComment };
                return DataSourceLoader.Load(dataSet, loadOptions);
            }
            else
            {
                var dataSet2 = from rec in _context.TagEngData
                               select new { rec.TagId, rec.EngDataCodeId, rec.EngDatavalue, rec.EngDatasource, rec.EngDataComment };
                return DataSourceLoader.Load(dataSet2, loadOptions);
            }
        }

        [HttpPost]
        public IActionResult TagEngDatas_Insert(string values)
        {
            var newTagEngData = new TagEngData();
            JsonConvert.PopulateObject(values, newTagEngData);

            if (!TryValidateModel(newTagEngData))
                return BadRequest();

            _context.TagEngData.Add(newTagEngData);
            _context.SaveChanges();

            return Ok(newTagEngData);
        }


        [HttpPut]
        public IActionResult TagEngDatas_Update(string key, string values)
        {
            var definition = new { TagId = 0, EngDataCodeId = 0 };
            var keys = JsonConvert.DeserializeAnonymousType(key, definition);
            var order = _context.TagEngData.First(x => x.TagId == keys.TagId && x.EngDataCodeId == keys.EngDataCodeId);

            JsonConvert.PopulateObject(values, order);

            if (!TryValidateModel(order))
                return BadRequest();

            _context.SaveChanges();

            return Ok(order);
        }

        [HttpDelete]
        public void TagEngDatas_Delete(string key)
        {
            var definition = new { TagId = 0, EngDataCodeId = 0 };
            var keys = JsonConvert.DeserializeAnonymousType(key, definition);

            var order = _context.TagEngData.First(o => o.TagId == keys.TagId && o.EngDataCodeId == keys.EngDataCodeId);
            _context.TagEngData.Remove(order);
            _context.SaveChanges();
        }

        [HttpPost]
        public object TagEngDatas_Batch(List<DataChange> changes)
        {
            foreach (var change in changes)
            {
                TagEngData order;

                if (change.Type == "update" || change.Type == "remove")
                {
                    var key = Convert.ToString(change.Key);
                    var definition = new { TagId = 0, EngDataCodeId = 0 };
                    var keys = JsonConvert.DeserializeAnonymousType(key, definition);

                    order = _context.TagEngData.First(o => o.TagId == keys.TagId && o.EngDataCodeId == keys.EngDataCodeId);
                }
                else
                {
                    order = new TagEngData();
                }

                if (change.Type == "insert" || change.Type == "update")
                {
                    JsonConvert.PopulateObject(change.Data.ToString(), order);

                    if (!TryValidateModel(order))
                        return BadRequest();

                    if (change.Type == "insert")
                    {
                        _context.TagEngData.Add(order);
                    }
                    change.Data = order;
                }
                else if (change.Type == "remove")
                {
                    _context.TagEngData.Remove(order);
                }
            }

            _context.SaveChanges();

            return Ok(changes);
        }
        public ActionResult ExportToExcel()
        {
            return View();
        }

        public Boolean ValidateName(TagEngData tagengdata)
        {
            var valid = _context.TagEngData.Any(x => x.EngDatavalue == tagengdata.EngDatavalue);
            return !valid;
        }
    }
}


