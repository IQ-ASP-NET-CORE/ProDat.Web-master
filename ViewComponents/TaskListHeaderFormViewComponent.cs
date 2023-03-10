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
    public class TaskListHeaderFormViewComponent : ViewComponent
    {
        private readonly TagContext _context;

        public TaskListHeaderFormViewComponent(TagContext context)
        {
            _context = context;
        }

        public IViewComponentResult Invoke(int height, int width, int? parent)
        {
            if (parent == null)
                return View();

            var rec = _context.TaskListHeader
                        .Where(x => x.TaskListHeaderId == parent).FirstOrDefault();

            var viewModel = new TaskListHeaderFormUC3
            {
                height = height,
                width = width,
                parent = parent,
                TaskListHeader = rec
            };

            return View(viewModel);
        }

    }
}
