using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Mvc;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using ProDat.Web2.Data;
using ProDat.Web2.Models;

namespace ProDat.Web2.Controllers
{
    // Why is this route here? 
    [Route("api/[controller]/[action]")]
    public class EngDataClassesController : Controller
    {
        private TagContext _context;

        public EngDataClassesController(TagContext context) {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Get(DataSourceLoadOptions loadOptions) {
            var engdataclass = _context.EngDataClass.Select(i => new {
                i.EngDataClassId,
                i.EngDataClassName
            });

            // If underlying data is a large SQL table, specify PrimaryKey and PaginateViaPrimaryKey.
            // This can make SQL execution plans more efficient.
            // For more detailed information, please refer to this discussion: https://github.com/DevExpress/DevExtreme.AspNet.Data/issues/336.
            // loadOptions.PrimaryKey = new[] { "EngDataClassId" };
            // loadOptions.PaginateViaPrimaryKey = true;

            return Json(await DataSourceLoader.LoadAsync(engdataclass, loadOptions));
        }

        [HttpPost]
        public async Task<IActionResult> Post(string values) {
            var model = new EngDataClass();
            var valuesDict = JsonConvert.DeserializeObject<IDictionary>(values);
            PopulateModel(model, valuesDict);

            if(!TryValidateModel(model))
                return BadRequest(GetFullErrorMessage(ModelState));

            var result = _context.EngDataClass.Add(model);
            await _context.SaveChangesAsync();

            return Json(new { result.Entity.EngDataClassId });
        }

        [HttpPut]
        public async Task<IActionResult> Put(int key, string values) {
            var model = await _context.EngDataClass.FirstOrDefaultAsync(item => item.EngDataClassId == key);
            if(model == null)
                return StatusCode(409, "Object not found");

            var valuesDict = JsonConvert.DeserializeObject<IDictionary>(values);
            PopulateModel(model, valuesDict);

            if(!TryValidateModel(model))
                return BadRequest(GetFullErrorMessage(ModelState));

            await _context.SaveChangesAsync();
            return Ok();
        }

        [HttpDelete]
        public async Task Delete(int key) {
            var model = await _context.EngDataClass.FirstOrDefaultAsync(item => item.EngDataClassId == key);

            _context.EngDataClass.Remove(model);
            await _context.SaveChangesAsync();
        }


        private void PopulateModel(EngDataClass model, IDictionary values) {
            string ENG_DATA_CLASS_ID = nameof(EngDataClass.EngDataClassId);
            string ENG_DATA_CLASS_NAME = nameof(EngDataClass.EngDataClassName);

            if(values.Contains(ENG_DATA_CLASS_ID)) {
                model.EngDataClassId = Convert.ToInt32(values[ENG_DATA_CLASS_ID]);
            }

            if(values.Contains(ENG_DATA_CLASS_NAME)) {
                model.EngDataClassName = Convert.ToString(values[ENG_DATA_CLASS_NAME]);
            }
        }

        private string GetFullErrorMessage(ModelStateDictionary modelState) {
            var messages = new List<string>();

            foreach(var entry in modelState) {
                foreach(var error in entry.Value.Errors)
                    messages.Add(error.ErrorMessage);
            }

            return String.Join(" ", messages);
        }
    }
}