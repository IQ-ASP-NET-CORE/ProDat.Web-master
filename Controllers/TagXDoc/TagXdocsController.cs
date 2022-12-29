using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using NuGet.Frameworks;
using ProDat.Web2.Data;
using ProDat.Web2.Models;

namespace ProDat.Web2.Controllers.TagXDoc
{
    public class TagXdocsController : Controller
    {
        private readonly TagContext _context;

        public TagXdocsController(TagContext context)
        {
            _context = context;
        }

        // INDEX ################
        // GET: TagXdocs
        public async Task<IActionResult> Index(int? pageNumber,
                                               string sortOrder,
                                               string currentFilter,
                                               TagXdocSearchViewModel searchModel
        )
        {
            ViewData["sortOrder"] = sortOrder;
            ViewData["DocSortParm"] = String.IsNullOrEmpty(sortOrder) ? "Doc_desc" : "";


            // pagination and search state maintenance.
            if (searchModel.Posted != null)
            {
                pageNumber = 1;
                ViewData["CurrentSearchModel"] = searchModel;
                ViewData["CurrentSearchModelJson"] = JsonConvert.SerializeObject(searchModel);
            }
            else if (currentFilter != null)
            {
                // Using existing filter (if exists).
                searchModel = JsonConvert.DeserializeObject<TagXdocSearchViewModel>(currentFilter);
                ViewData["CurrentSearchModel"] = searchModel;
                ViewData["CurrentSearchModelJson"] = currentFilter;
            }

            var business = new TagXDocBusinessLogic(_context);
            var tagDocModel = business.GetTags(searchModel);

            // Apply sorts
            switch (sortOrder)
            {
                case "doc_desc":
                    tagDocModel = tagDocModel.OrderByDescending(s => s.Doc);
                    break;
                default:
                    tagDocModel = tagDocModel.OrderBy(s => s.Doc);
                    break;
            }

            // Pagination
            int pageSize = 10;
            return View(await PaginatedList<TagXdoc>.CreateAsync(tagDocModel.AsNoTracking(), pageNumber ?? 1, pageSize));
        }

        // Right Click navigation for showing Docs related to tag.
        public async Task<IActionResult> DocByTagId(int id)
        {
            var tagDocs = _context.TagXdoc
                .Include(x => x.Doc)
                .Include(x => x.Tag)
                .Include(x=> x.Doc.DocType).AsNoTracking().Where(x=> x.TagId == id);

            return View(await tagDocs.ToListAsync());
        }




        // GET: TagXdocs/Details/5
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tagXdoc = (from t in _context.TagXdoc
                                .Include(t => t.Doc)
                                .Include(t => t.Tag)
                           select t).AsQueryable();

            tagXdoc = tagXdoc.Where(t => t.TagId == id);

            if (tagXdoc == null)
            {
                return NotFound();
            }

            return View(tagXdoc);
        }







        // GET: TagXdocs/Create
        public IActionResult Create(int? Id)
        {
            ViewData["DocId"] = new SelectList(_context.Doc, "DocId", "DocNum");
            if(Id != null) { 
                ViewData["TagId"] = new SelectList(_context.Tag, "TagId", "TagNumber", Id);
            }
            else
            {
                ViewData["TagId"] = new SelectList(_context.Tag, "TagId", "TagNumber");
            }

            return View();
        }

        // POST: TagXdocs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("TagId,DocId,DateCreated,XComment")] TagXdoc tagXdoc)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tagXdoc);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["DocId"] = new SelectList(_context.Doc, "DocId", "DocNum", tagXdoc.DocId);
            ViewData["TagId"] = new SelectList(_context.Tag, "TagId", "TagNumber", tagXdoc.TagId);
            return View(tagXdoc);
        }

        // GET: TagXdocs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tagXdoc = await _context.TagXdoc.FindAsync(id);
            if (tagXdoc == null)
            {
                return NotFound();
            }
            ViewData["DocId"] = new SelectList(_context.Doc, "DocId", "DocNum", tagXdoc.DocId);
            ViewData["TagId"] = new SelectList(_context.Tag, "TagId", "TagNumber", tagXdoc.TagId);
            return View(tagXdoc);
        }

        // POST: TagXdocs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("TagId,DocId,DateCreated,XComment")] TagXdoc tagXdoc)
        {
            if (id != tagXdoc.TagId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tagXdoc);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TagXdocExists(tagXdoc.TagId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["DocId"] = new SelectList(_context.Doc, "DocId", "DocNum", tagXdoc.DocId);
            ViewData["TagId"] = new SelectList(_context.Tag, "TagId", "TagNumber", tagXdoc.TagId);
            return View(tagXdoc);
        }

        // GET: TagXdocs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tagXdoc = await _context.TagXdoc
                .Include(t => t.Doc)
                .Include(t => t.Tag)
                .FirstOrDefaultAsync(m => m.TagId == id);
            if (tagXdoc == null)
            {
                return NotFound();
            }

            return View(tagXdoc);
        }

        // POST: TagXdocs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var tagXdoc = await _context.TagXdoc.FindAsync(id);
            _context.TagXdoc.Remove(tagXdoc);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TagXdocExists(int id)
        {
            return _context.TagXdoc.Any(e => e.TagId == id);
        }
    }
}
