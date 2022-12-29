
//using DevExtreme.NETCore.Demos.Models.SampleData;
using ProDat.Web2.Data;
using ProDat.Web2.Models;
using ProDat.Web2.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;

namespace ProDat.Web2.ViewComponents
{
    public class FlocSearchDataGrid : ViewComponent
    {
        private readonly TagContext _context;

        public FlocSearchDataGrid(TagContext context)
        {
            _context = context;
        }

        public async Task<IViewComponentResult> InvokeAsync(int height,int width)
        {
            var viewModel = new UC2ComponentVM
            {
                height = height,
                width = width
            };
            return View(viewModel);
        }

    }
}
