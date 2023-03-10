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
    public class MaintItemFormViewComponent : ViewComponent
    {
        private readonly TagContext _context;

        public MaintItemFormViewComponent(TagContext context)
        {
            _context = context;
        }

        public IViewComponentResult Invoke(int height, int width, int? parent)
        {
            if (parent == null)
                return View();

            var rec = _context.MaintItem
                        .Where(x => x.MaintItemId == parent).FirstOrDefault();

            // todo create new View Model
            var viewModel = new MaintenanceItemFormUC3
            {
                height = height,
                width = width,
                parent = parent,
                MaintItem = rec
            };

            return View(viewModel);

        }
    }
}

