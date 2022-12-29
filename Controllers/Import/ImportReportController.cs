using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using ClosedXML.Excel;
using DocumentFormat.OpenXml.Wordprocessing;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ProDat.Web2.Data;
using ProDat.Web2.Models.ETL;
using ProDat.Web2.ViewModels;

namespace ProDat.Web2.Controllers.Import
{
   
    public class ImportReportController : Controller
    {
        private readonly TagContext _context;
        public ImportReportController(TagContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index(int id)
        {
            ApproveReport approveReport = new ApproveReport();
            approveReport.ImportReport = await _context.ImportReport.Where(x => x.importId == id).ToListAsync();
            approveReport.Import = await _context.Import.Where(x => x.ImportId == id).FirstAsync();
            return View(approveReport);
        }
    }
}
