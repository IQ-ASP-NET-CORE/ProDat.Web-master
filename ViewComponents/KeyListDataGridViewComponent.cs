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
    public class KeyListAttributes : ViewComponent
    {
        private readonly TagContext _context;

        public KeyListAttributes(TagContext context)
        {
            _context = context;
        }

        //public async Task<IViewComponentResult> InvokeAsync(int height,int width, int? parent)
        public IViewComponentResult Invoke(int height, int width, int? parent)
        {
            var viewModel = new EngDataKeyListViewModel
            {
                Parent = parent,
                Height = height,
                Width = width

            };
            return View(viewModel);
        }

    }
}
