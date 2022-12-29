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
    public class TagPropertiesViewComponent : ViewComponent
    {
        private readonly TagContext _context;

        public TagPropertiesViewComponent(TagContext context)
        {
            _context = context;
        }

        public async Task<IViewComponentResult> InvokeAsync(int height, int width, int tagId = 1628)
        {
           var tag = _context.Tag
                            .Where(x => x.TagId == tagId)
                            .Include(x => x.Location)
                            .Include(x => x.SAPStatus)
                            .Include(x => x.MaintLocation)
                                .ThenInclude(y => y.MaintArea)
                            .FirstOrDefault();

            var TagProp = new TagPropertiesStandardViewModel();
            TagProp.Tag = tag;

            // tag -> MaintLocation -> MaintArea -> PlantSection
            //TagProp.PlantSectionId = tag.MaintLocationId == null ? null : tag.MaintLocation.MaintArea.PlantSectionId;
            //TagProp.MaintAreaId = tag.MaintLocationId == null ? null : tag.MaintLocation.MaintAreaId;
            
            // required to avoid recursive loop.
            if(tag.MaintParentId != null)
            {
                var parentTag = _context.Tag
                                    .Where(x => x.TagId == tag.MaintParentId)
                                    .FirstOrDefault();

                TagProp.MaintParent_TagFloc = parentTag.TagFloc;
            }

            // UI Settings
            TagProp.width = width;
            TagProp.height = height;
            TagProp.bgcolour = tag.SAPStatus == null ? "#FFFFFF": tag.SAPStatus.ColourCode;

            return View(TagProp);
        }

    }
}
