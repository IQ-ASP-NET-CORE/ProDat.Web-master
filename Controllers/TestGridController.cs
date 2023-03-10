using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ClosedXML.Excel;
using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using ProDat.Web2.Data;
using ProDat.Web2.Models;
using ProDat.Web2.Models.DataGrid;
using ProDat.Web2.TagLibrary;
using ProDat.Web2.ViewModels;

namespace ProDat.Web2.Controllers
{
    /*
     * Provides functionality to manage Tag Register. 
     * Note: Star lookup data sources are defined in Lookups Controller.
     */

    public class TestGridController : Controller
    {
        private readonly TagContext _context;
        private readonly ILogger<TagContext> _logger;
        private readonly IModelMetadataProvider _provider;
        private IConfiguration _configuration;

        public TestGridController(TagContext context, ILogger<TagContext> logger, IModelMetadataProvider provider, IConfiguration Configuration)
        {
            _provider = provider;
            _context = context;
            _logger = logger;
            _configuration = Configuration;
        }

        public IActionResult Index(string columnSetsName = "Default")
        {
            return View();
        }

        #region data mangement for grid view.

        public Object TagRegister_GetData(DataSourceLoadOptions loadOptions)
        {
            // Oh well, this causes a loop, so will need to find waht field is causing an objhect depth to exceed 32 or be cyclic. 
            // var dataSet = _context.Tag.AsQueryable();

            var dataSet = from rec in _context.Tag
                              //select new { rec.TagId, rec.TagNumber, rec.TagService, rec.TagFloc, rec.TagFlocDesc, rec.EngDiscId, rec.SubSystem.SubSystemNum };
                          select new UC1_TagsVM(rec);

            return DataSourceLoader.Load(dataSet, loadOptions);
        }

        #endregion



    } //UC1 Controller

} // Namespace

