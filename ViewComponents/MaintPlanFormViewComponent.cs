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
    public class MaintPlanFormViewComponent : ViewComponent
    {
        private readonly TagContext _context;

        public MaintPlanFormViewComponent(TagContext context)
        {
            _context = context;
        }

        public async Task<IViewComponentResult> InvokeAsync(int height, int width, int? parent)
        {
            if (parent == null)
                return View();

            var rec = await _context.MaintPlan
                       .Where(x => x.MaintPlanId == parent).FirstOrDefaultAsync();

            // todo create new View Model
            var viewModel = new MaintenancePlanFormUC3
            {
                height = height,
                width = width,
                parent = parent,
                MaintPlan = rec
            };

            return View(viewModel);
        }

    }
}
