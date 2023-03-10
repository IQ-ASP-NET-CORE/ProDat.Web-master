
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
    public class FlocInMIDataGridUC3ViewComponent : ViewComponent
    {
        private readonly TagContext _context;

        public FlocInMIDataGridUC3ViewComponent(TagContext context)
        {
            _context = context;
        }

        public IViewComponentResult Invoke(int height, int width, int? parent)
        {
            // retrieve MI name into vm.
            string MINum = null;
            if (parent != null)
            {
                MINum = _context.MaintItem
                .Where(x => x.MaintItemId == parent)
                .Select(x => x.MaintItemNum)
                .FirstOrDefault();
            }

            var viewModel = new UC2ComponentVM
            {
                height = height,
                width = width,
                parent = parent,
                customstring = MINum
            };
            return View(viewModel);
        }

    }
}
