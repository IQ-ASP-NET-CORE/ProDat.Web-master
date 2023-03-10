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
    public class EngClassDataGridViewComponent : ViewComponent
    {
        private readonly TagContext _context;

        public EngClassDataGridViewComponent(TagContext context)
        {
            _context = context;
        }

        public IViewComponentResult Invoke(int height, int width, int? parent)


        {
            if (parent == null) { parent = 1; }

            var rec = _context.EngDataCode
                        .Where(x => x.KeyListId == parent);

            var viewModel = new EngDataKeyListViewModel
            {
                Height = height,
                Width = width,
                KeyListId = (int)parent
            };

            //var viewModel = new EngDataKeyListViewModel
            //{
            //    KeyListId = (int)parent,
            //    //height = height,
            //    //width = width

            //};
            return View(viewModel);
        }

    }
}
