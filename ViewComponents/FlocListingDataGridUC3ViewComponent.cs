
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
    public class FlocListingDataGridUC3ViewComponent : ViewComponent
    {
        private readonly TagContext _context;

        public FlocListingDataGridUC3ViewComponent(TagContext context)
        {
            _context = context;
        }

        public async Task<IViewComponentResult> InvokeAsync(int Height,int Width)
        {
            var viewModel = new UC2ComponentVM
            {
                height = Height,
                width = Width
            };
            return View(viewModel);
        }

    }
}
