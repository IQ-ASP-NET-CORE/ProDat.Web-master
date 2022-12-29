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
    public class TaskListHeaderViewComponent : ViewComponent
    {
        private readonly TagContext _context;

        public TaskListHeaderViewComponent(TagContext context)
        {
            _context = context;
        }

        public async Task<IViewComponentResult> InvokeAsync(int height, int width)
        {
            var viewModel = new UC2ComponentVM
            {
                height = height,
                width = width
            };

            // get data from sql into dict. 
            Dictionary<string, ColParams> colIndex = new Dictionary<string, ColParams>();
            var col_customisations = await _context.ColumnSets
                                        .Where(x => x.ColumnSetsName == "Default")
                                        .Where(x => x.ColumnSetsEntity == "TaskListHeader")
                                        .Select(x => new {
                                            x.ColumnName,
                                            x.ColumnOrder,
                                            x.ColumnWidth,
                                            x.ColumnVisible
                                        })
                                        .ToListAsync();

            foreach (var cust in col_customisations)
            {
                colIndex.Add(cust.ColumnName, new ColParams(cust.ColumnOrder, cust.ColumnWidth, cust.ColumnVisible));
            }
            ViewBag.colIndex = colIndex;

            return View(viewModel);
        }

    }
}
