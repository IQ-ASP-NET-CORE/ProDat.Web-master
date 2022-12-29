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
    [Route("api/[controller]/[action]")]
    public class KeyListController : Controller
    {
        private TagContext _context;

        public KeyListController(TagContext context) {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Get(DataSourceLoadOptions loadOptions) {
            var keylist = _context.KeyList.Select(i => new {
                i.KeyListId,
                i.KeyListName
            });

            // If underlying data is a large SQL table, specify PrimaryKey and PaginateViaPrimaryKey.
            // This can make SQL execution plans more efficient.
            // For more detailed information, please refer to this discussion: https://github.com/DevExpress/DevExtreme.AspNet.Data/issues/336.
            // loadOptions.PrimaryKey = new[] { "KeyListId" };
            // loadOptions.PaginateViaPrimaryKey = true;

            return Json(await DataSourceLoader.LoadAsync(keylist, loadOptions));
        }

        [HttpPost]
        public async Task<IActionResult> Post(string values) {
            var model = new KeyList();
            var valuesDict = JsonConvert.DeserializeObject<IDictionary>(values);
            PopulateModel(model, valuesDict);

            if(!TryValidateModel(model))
                return BadRequest(GetFullErrorMessage(ModelState));

            var result = _context.KeyList.Add(model);
            await _context.SaveChangesAsync();

            return Json(new { result.Entity.KeyListId });
        }

        [HttpPut]
        public async Task<IActionResult> Put(int key, string values) {
            var model = await _context.KeyList.FirstOrDefaultAsync(item => item.KeyListId == key);
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
            var model = await _context.KeyList.FirstOrDefaultAsync(item => item.KeyListId == key);

            _context.KeyList.Remove(model);
            await _context.SaveChangesAsync();
        }


        private void PopulateModel(KeyList model, IDictionary values) {
            string ENG_DATA_CLASS_ID = nameof(KeyList.KeyListId);
            string ENG_DATA_CLASS_NAME = nameof(KeyList.KeyListName);

            if(values.Contains(ENG_DATA_CLASS_ID)) {
                model.KeyListId = Convert.ToInt32(values[ENG_DATA_CLASS_ID]);
            }

            if(values.Contains(ENG_DATA_CLASS_NAME)) {
                model.KeyListName = Convert.ToString(values[ENG_DATA_CLASS_NAME]);
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