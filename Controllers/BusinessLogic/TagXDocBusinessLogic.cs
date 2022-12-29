using Microsoft.EntityFrameworkCore;
using ProDat.Web2.Data;
using ProDat.Web2.Models;
using ProDat.Web2.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProDat.Web2.Controllers.TagXDoc
{
    public class TagXDocBusinessLogic
    {
        private TagContext _context;

        public TagXDocBusinessLogic(TagContext context)
        {
            _context = context;
        }

        public IQueryable<TagXdoc> GetTags(TagXdocSearchViewModel searchModel)
        {
            var result = (from t in _context.TagXdoc
                                .Include(t => t.Doc)
                                .Include(t => t.Tag)
                                .Include(t=> t.Doc.DocType)
                             select t).AsQueryable();

            if (searchModel.TagId >0)
            {
                result = result.Where(x => x.TagId == searchModel.TagId);
            }

            if (searchModel.DocId > 0)
            {
                result = result.Where(x => x.DocId == searchModel.DocId);
            }

            if (!string.IsNullOrEmpty(searchModel.TagNumber))
            {
                result = result.Where(x => x.Tag.TagNumber.Contains(searchModel.TagNumber));
            }

            if (!string.IsNullOrEmpty(searchModel.DocNum))
            {
                result = result.Where(x => x.Doc.DocNum.Contains(searchModel.DocNum));
            }
            return result;
        }

    }
}
