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
    public class MaintenanceStrategiesFormViewComponent : ViewComponent
    {
        private readonly TagContext _context;

        public MaintenanceStrategiesFormViewComponent(TagContext context)
        {
            _context = context;
        }

        public async Task<IViewComponentResult> InvokeAsync(int height,int width, int parent = -1)
        {
            var ms = _context.MaintStrategy
                                .Where(x => x.MaintStrategyId == parent).FirstOrDefault();

            var viewModel = new MaintenanceStrategiesFormUC3
            {
                height = height,
                width = width,
                parent = parent,
                MaintStrategy = ms
            };

            return View(viewModel);
        }

    }
}
