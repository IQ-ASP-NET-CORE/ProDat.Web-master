using DevExtreme.AspNet.Mvc;
using DevExtreme.AspNet.Data;

using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProDat.Web2.Data;
using ProDat.Web2.Models;
using ProDat.Web2.Models.DataGrid;
using ProDat.Web2.ViewModels;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Reflection;
using Microsoft.AspNetCore.Authorization;

namespace ProDat.Web2.Controllers
{
    public class UC2AController : Controller
    {
        #region instantiate controller
        private readonly TagContext _context;

        public UC2AController(TagContext context)
        {
            _context = context;
        }

        #endregion

        public IActionResult Index()
        {

            #region get instance info and update UC2 viewmodel

            // update UC2 data. Not using this for much at the moment.
            UC2 uc2 = new UC2();

            #endregion
            ViewBag.GlobalProjectDescription = _context.Project.First().ProjectName;
            return View(uc2);
        }

        #region  Display Eng Attributes associated by MaintType
        public IActionResult EngAttributesByClassification(int id)
        {
            var tag = _context.Tag.Where(x => x.TagId == id).FirstOrDefault();

            ViewBag.TagId = tag.TagId;
            ViewBag.TagNumber = tag.TagNumber;
            ViewBag.TagFlocDesc = tag.TagFlocDesc != null ? " - " + tag.TagFlocDesc : "";
            ViewBag.GlobalProjectDescription = _context.Project.First().ProjectName;
            return View();
        }
        #endregion

        #region manage MaintItem TreeView

        [Authorize(Roles = "User")]
        [HttpPost]
        public async Task<IActionResult> AjaxMaintParentUpdate(string tagId, string maintParentId, string destinationComponent)
        {
            // Validate parameters
            var tag = await _context.Tag.FindAsync(int.Parse(tagId));
            if (tag == null)
                return BadRequest();

            if(destinationComponent == "MaintTree")
            {
                // validate Parent.
                if (string.IsNullOrEmpty(maintParentId))
                    return BadRequest();

                var tagParent = await _context.Tag.FindAsync(int.Parse(maintParentId));
                if (tagParent == null)
                    return BadRequest();

                // update Tag.
                tag.MaintParentId = int.Parse(maintParentId);
                // If no MaintTypeId set, or is NonMaintainable, set to M.
                if (tag.MaintTypeId == null || tag.MaintTypeId == 4)
                    tag.MaintTypeId = 1;

                await _context.SaveChangesAsync();
            }

            if(destinationComponent == "NonMaintainedDataGrid")
            {
                MaintHierarchyNodeMovedToNonMaintainable(tag.TagId);
            }
            if (destinationComponent == "UnassignedDataGrid")
            {
                MaintHierarchyNodeMovedToUnassigned(tag.TagId);
            }


            // return 200.
            return Ok(new { message = "Success" });

        }

        //(string TagId, string Status)
        [Authorize(Roles = "User")]
        [HttpPost]
        public async Task<IActionResult> Ajax_set_Tag_maintTypeName(string TagId, string MaintTypeName)
        {
            // Validate parameters
            var tagStatusId = await _context.MaintType
                                    .Where(x => x.MaintTypeName == MaintTypeName)
                                    .Select(x => x.MaintTypeId)
                                    .FirstOrDefaultAsync();

            var tag = await _context.Tag.FindAsync(int.Parse(TagId));

            if (tag == null || tagStatusId <1)
                return BadRequest();

            tag.MaintTypeId = tagStatusId;

            await _context.SaveChangesAsync();

            // return 200.
            return Ok(new { message = "Success" });

        }

        [Authorize(Roles = "User")]
        [HttpPost]
        public async Task<IActionResult> Ajax_unset_Tag_maintParent(string TagId)
        {
            // Validate parameters
            var tag = await _context.Tag.FindAsync(int.Parse(TagId));

            if (tag == null)
                return BadRequest();

            tag.MaintParentId = null;

            await _context.SaveChangesAsync();

            // return 200.
            return Ok(new { message = "Success" });

        }


        [Authorize(Roles = "User")]
        public async Task<IActionResult> AjaxMaintTreePMAUpdate(string pmAId, string oldParent, string newParent, string destinationComponent)
        {
            // delete if moving within tree, or to PMA_DataGrid
            if (!string.IsNullOrEmpty(oldParent) || destinationComponent == "PMAssembliesDataGrid")
            {
                // validate parameters
                var rec = await _context.FlocXpmassembly.FindAsync(int.Parse(oldParent), int.Parse(pmAId));
                if (rec == null)
                     return BadRequest();

                 _context.FlocXpmassembly.Remove(rec);
            }

            // add relationship if new parent within MaintTree
            if (!string.IsNullOrEmpty(newParent) && destinationComponent == "MaintTree")
            {
                // validate parameters
                var fk1 = await _context.Pmassembly.FindAsync(int.Parse(pmAId));
                if (fk1 == null)
                    return BadRequest();

                var fk2 = await _context.Tag.FindAsync(int.Parse(newParent));
                if (fk2 == null)
                    return BadRequest();

                // add FlocxPMAssembly (pmAId, newParent)
                var rec = new FlocXpmassembly();
                rec.TagId = int.Parse(newParent);
                rec.PmassemblyId = int.Parse(pmAId);
                _context.FlocXpmassembly.Add(rec);
            }

            await _context.SaveChangesAsync();

            // return 200.
            return Ok(new { message = "Success" });

        }

        [Authorize(Roles = "User")]
        public async Task<IActionResult> AjaxMaintTreeMIUpdate(string miId, string oldParent, string newParent, string destinationComponent)
        {
            // delete when moving within tree, or out of tree.
            if (!string.IsNullOrEmpty(oldParent) || destinationComponent == "MaintenanceItemsDataGrid")
            {
                // validate parameters
                var rec = await _context.FlocXmaintItem.FirstOrDefaultAsync(x=> x.FlocId == int.Parse(oldParent) && x.MaintItemId == int.Parse(miId));
                if (rec == null)
                    return BadRequest();
                _context.FlocXmaintItem.Remove(rec);
            }

            // add new item to newParent.
            if (!string.IsNullOrEmpty(newParent) && destinationComponent == "MaintTree")
            {
                // validate parameters
                var fk1 = await _context.MaintItem.FindAsync(int.Parse(miId));
                if (fk1 == null)
                    return BadRequest();

                var fk2 = await _context.Tag.FindAsync(int.Parse(newParent));
                if (fk2 == null)
                    return BadRequest();

                // Test for existence. Exit gracefyully as its already there...
                var test_exists = _context.FlocXmaintItem.FirstOrDefault(x => x.MaintItemId == int.Parse(miId) && x.FlocId == int.Parse(newParent));

                if (test_exists != null)
                    return Ok(new { message = "Already Exists" });

                // add FlocXmaintItem (pmAId, newParent)
                var rec = new FlocXmaintItem();
                rec.FlocId = int.Parse(newParent);
                rec.MaintItemId = int.Parse(miId);
                _context.FlocXmaintItem.Add(rec);
            }

            await _context.SaveChangesAsync();

            // return 200.
            return Ok(new { message = "Success" });
        }

        [Authorize(Roles = "User")]
        public void MaintHierarchyNodeMovedToNonMaintainable(int TagId)
        {
            // recursively retrieve this tags Maintenance hierarchy structure.
            // on ascent, remove Maintenance Hierarchy Structure

            Tag tag = _context.Tag
                .Include(t => t.InverseMaintParents).ThenInclude(child => child.MaintType)
                .Include(t => t.MaintType)
                .Where(t => t.TagId == TagId)
                .FirstOrDefault();

            foreach (Tag child in tag.InverseMaintParents.OrderBy(t => t.TagNumber))
                MaintHierarchyNodeMovedToNonMaintainable(child.TagId);

            // ############################
            // Reached leaf nodes.
            // Update ONLY Maintenance Tags
            // or null value, on ascent.
            // ############################

            if (tag.MaintType == null || (tag.MaintType != null && tag.MaintType.MaintTypeName == "M"))
            {
                tag.MaintTypeId = 4;
                tag.MaintParentId = null;

                var pma_rec = _context.FlocXpmassembly.Where(x => x.TagId == TagId);
                foreach (var rec in pma_rec)
                {
                    _context.FlocXpmassembly.Remove(rec);
                }

                var mi_rec = _context.FlocXmaintItem.Where(x => x.FlocId == TagId);
                foreach (var rec in mi_rec)
                {
                    _context.FlocXmaintItem.Remove(rec);
                }

                _context.SaveChangesAsync();
            }

            return;
        }

        [Authorize(Roles = "User")]
        public void MaintHierarchyNodeMovedToUnassigned(int TagId)
        {
            // recursively retrieve this tags Maintenance hierarchy structure.
            // on ascent, remove Maintenance Hierarchy Structure

            Tag tag = _context.Tag
                .Include(t => t.InverseMaintParents).ThenInclude(child => child.MaintType)
                .Include(t => t.MaintType)
                .Where(t => t.TagId == TagId)
                .FirstOrDefault();

            foreach (Tag child in tag.InverseMaintParents.OrderBy(t => t.TagNumber))
                MaintHierarchyNodeMovedToUnassigned(child.TagId);

            // ############################
            // Reached leaf nodes.
            // Update ONLY Maintenance Tags
            // or null value, on ascent.
            // ############################

            if (tag.MaintType == null || (tag.MaintType != null && tag.MaintType.MaintTypeName == "M"))
            {
                // remove from hierarchy and set to 'M'
                // leave assigned MI/PMA intact.
                tag.MaintParentId = null;
                tag.MaintTypeId = 1;

                // commented out, may want to keep assigned MI/PMA?
                //var pma_rec = _context.FlocXpmassembly.Where(x => x.TagId == TagId);
                //foreach (var rec in pma_rec)
                //{
                //    _context.FlocXpmassembly.Remove(rec);
                //}

                //var mi_rec = _context.FlocXmaintItem.Where(x => x.FlocId == TagId);
                //foreach (var rec in mi_rec)
                //{
                //    _context.FlocXmaintItem.Remove(rec);
                //}

                _context.SaveChangesAsync();
            }

            return;
        }

        // Load MaintTreeDemand
        public object GetHierarchicalDataForDragAndDropDemand(DataSourceLoadOptions loadOptions)
        {
            // retrieve data into variable: IEnumerable<MaintHeirarchyNode>
            var maintItems = new List<MaintHeirarchyNode>();

            // TODO: change from hard coded root node to use MaintTypeId to specify projects root nodes.
            //       e.g. Call BuildMaintHierarchy for all tags where MaintClassId = 1
            MaintHeirarchyNode node = BuildMaintHierarchyDemand("Plant");
            maintItems.Add(node);

            // controller specific Content takes two parameters, viewComponent does not.
            //return Content(JsonConvert.SerializeObject(DataSourceLoader.Load(maintItems, loadOptions)), "application/json");
            return Content(JsonConvert.SerializeObject(DataSourceLoader.Load(maintItems, loadOptions)));
        }

        public MaintHeirarchyNode BuildMaintHierarchyDemand(string TagNumber, int depth = 0)
        {
            IList<MaintHeirarchyNode> children = new List<MaintHeirarchyNode>();

            // Retrieve Max Depth to show on load from Project.MaxDepth

            int MaintHierarchy_LoadDepth = _context.Project
                                .Select(x => x.MaintHierarchy_LoadDepth)
                                .FirstOrDefault();

            // Use of thenInclude is slow.
            // might retrieve set of tags with a MaintParentID, and MaintType in (N, M, P), then build hierarchy.
            Tag tag = _context.Tag
                .Include(t => t.InverseMaintParents).ThenInclude(child => child.MaintType)
                .Include(t => t.MaintType)
                .Where(t => t.TagNumber == TagNumber)
                .FirstOrDefault();


            foreach (Tag child in tag.InverseMaintParents
                            .Where(t => t.TagDeleted == false)
                            .Where(t => t.MaintType != null)
                            .Where(u => u.MaintType.MaintTypeName == "N" ||
                                        u.MaintType.MaintTypeName == "M" ||
                                        u.MaintType.MaintTypeName == "P")
                            .OrderBy(x => x.TagFloc))
            {
                // some arbitrary number to get out of infinite loop, caused by someone updating MaintParentId to item below its own structure.
                if (depth < 3)
                {
                    children.Add(BuildMaintHierarchyDemand(child.TagNumber, depth + 1));
                }
            }

            // Add PMA as children for current Tag, using FlocXMaintItem relationship. Is not hierarchical.
            var flocXPMAs = _context.FlocXpmassembly
                                    .Include(x => x.Pmassembly)
                                    .Where(t => t.TagId == tag.TagId)
                                    .ToList();

            foreach (var flocXPMA in flocXPMAs)
            {
                children.Add(new MaintHeirarchyNode
                {
                    Id = tag.TagId + ":" + flocXPMA.PmassemblyId.ToString(),
                    dbId = flocXPMA.PmassemblyId.ToString(),
                    ParentId = tag.TagId.ToString(),
                    Name = flocXPMA.Pmassembly.PmassemblyName,
                    IsDirectory = false,
                    IsDeleted = false, // not a feature of PMA.
                    nodeType = "PMA",
                });
            }

            return new MaintHeirarchyNode
            {
                Id = tag.TagId.ToString(),
                dbId = tag.TagId.ToString(),
                ParentId = tag.MaintParentId.ToString(),
                Name = tag.TagFloc + ": " + tag.TagFlocDesc,
                IsDirectory = true, // required to allow DnD onto node.
                nodeType = tag.MaintType.MaintTypeName,
                IsDeleted = tag.TagDeleted,
                IsExpanded = depth <= MaintHierarchy_LoadDepth ? true : false,
                SAPStatusId = tag.SAPStatusId != null ? (int)tag.SAPStatusId : 0,
                MaintStatus = tag.SAPStatusId.ToString(),
                Items = children
            };
        }

        // Load MaintTree
        public object GetHierarchicalDataForDragAndDrop(DataSourceLoadOptions loadOptions)
        {
            // retrieve data into variable: IEnumerable<MaintHeirarchyNode>
            var maintItems = new List<MaintHeirarchyNode>();

            // TODO: change from hard coded root node to use MaintTypeId to specify projects root nodes.
            //       e.g. Call BuildMaintHierarchy for all tags where MaintClassId = 1
            MaintHeirarchyNode node = BuildMaintHierarchy("Plant");
            maintItems.Add(node);

            // controller specific Content takes two parameters, viewComponent does not.
            //return Content(JsonConvert.SerializeObject(DataSourceLoader.Load(maintItems, loadOptions)), "application/json");
            return Content(JsonConvert.SerializeObject(DataSourceLoader.Load(maintItems, loadOptions)));
        }

        // Service class to Load MaintTree.
        public MaintHeirarchyNode BuildMaintHierarchy(string TagNumber, int depth=0)
        {
            IList<MaintHeirarchyNode> children = new List<MaintHeirarchyNode>();

            // Retrieve Max Depth to show on load from Project.MaxDepth

            int MaintHierarchy_LoadDepth = _context.Project
                                .Select(x => x.MaintHierarchy_LoadDepth)
                                .FirstOrDefault();

            // Use of thenInclude is slow.
            // might retrieve set of tags with a MaintParentID, and MaintType in (N, M, P), then build hierarchy.
            Tag tag = _context.Tag
                .Include(t => t.InverseMaintParents).ThenInclude(child => child.MaintType)
                .Include(t=> t.MaintType)
                .Where(t => t.TagNumber == TagNumber)
                .FirstOrDefault();


            foreach (Tag child in tag.InverseMaintParents
                            .Where(t => t.TagDeleted == false)
                            .Where(t => t.MaintType != null)
                            .Where(u => u.MaintType.MaintTypeName == "N" ||
                                        u.MaintType.MaintTypeName == "M" ||
                                        u.MaintType.MaintTypeName == "P")
                            .OrderBy(x => x.TagFloc))
            {
                // some arbitrary number to get out of infinite loop, caused by someone updating MaintParentId to item below its own structure.
                if (depth < 15)
                {
                    children.Add(BuildMaintHierarchy(child.TagNumber, depth + 1));
                }
            }

            // Add PMA as children for current Tag, using FlocXMaintItem relationship. Is not hierarchical.
            var flocXPMAs = _context.FlocXpmassembly
                                    .Include(x=> x.Pmassembly)
                                    .Where(t => t.TagId == tag.TagId)
                                    .ToList();

            foreach (var flocXPMA in flocXPMAs)
            {
                children.Add(new MaintHeirarchyNode
                {
                    Id = tag.TagId + ":" + flocXPMA.PmassemblyId.ToString(),
                    dbId = flocXPMA.PmassemblyId.ToString(),
                    ParentId = tag.TagId.ToString(),
                    Name = flocXPMA.Pmassembly.PmassemblyName,
                    IsDirectory = false,
                    IsDeleted = false, // not a feature of PMA.
                    nodeType = "PMA",
                });
            }

            return new MaintHeirarchyNode
            {
                Id = tag.TagId.ToString(),
                dbId = tag.TagId.ToString(),
                ParentId = tag.MaintParentId.ToString(),
                Name = tag.TagFloc + ": " + tag.TagFlocDesc,
                IsDirectory = true, // required to allow DnD onto node.
                nodeType = tag.MaintType.MaintTypeName,
                IsDeleted = tag.TagDeleted,
                IsExpanded = depth <= MaintHierarchy_LoadDepth ? true: false,
                SAPStatusId = tag.SAPStatusId != null ? (int)tag.SAPStatusId : 0,
                MaintStatus = tag.SAPStatusId.ToString(),
                Items = children
            };
        }


        // Load MaintTree on Demand
        [HttpGet]
        public object GetMaintTreeDemand(string parentId)
        {
            IList<MaintHeirarchyNode> children = new List<MaintHeirarchyNode>();

            // return root node if initial query
            if (parentId == null || parentId == "0")
            {
                Tag root = _context.Tag
               .Include(t => t.InverseMaintParents).ThenInclude(child => child.MaintType)
               .Include(t => t.MaintType)
               .Where(t => t.TagId == 1)
               .FirstOrDefault();

                children.Add(new MaintHeirarchyNode
                {
                    Id = root.TagId.ToString(),
                    dbId = root.TagId.ToString(),
                    Name = root.TagFloc + ": " + root.TagFlocDesc,
                    IsDirectory = true, // required to allow DnD onto node.
                    hasItems = true,
                    nodeType = root.MaintType.MaintTypeName,
                    IsDeleted = root.TagDeleted,
                    IsExpanded = false,
                    SAPStatusId = root.SAPStatusId != null ? (int)root.SAPStatusId : 0,
                    MaintStatus = root.SAPStatusId.ToString()
                });
                return children;
            }

            Tag tag = _context.Tag
                .Include(t => t.InverseMaintParents).ThenInclude(child => child.MaintType)
                .Include(t => t.MaintType)
                .Where(t => t.TagId == int.Parse(parentId))
                .FirstOrDefault();


            foreach (Tag child in tag.InverseMaintParents
                            .Where(t => t.TagDeleted == false)
                            .Where(t => t.MaintType != null)
                            .Where(u => u.MaintType.MaintTypeName == "N" ||
                                        u.MaintType.MaintTypeName == "M" ||
                                        u.MaintType.MaintTypeName == "P")
                            .OrderBy(x => x.TagFloc))
            {
                // must load up childs children and PMA counts for hasChildren Test
                Tag x = _context.Tag
                    .Include(t => t.InverseMaintParents)
                    .Where(t => t.TagId == child.TagId)
                    .FirstOrDefault();

                var y = _context.FlocXpmassembly
                                    .Include(x => x.Pmassembly)
                                    .Where(t => t.TagId == child.TagId)
                                    .ToList();

                // Todo, hasChildren to test for PMA, so exmansion will show these next load.
                bool hasChildren = x.InverseMaintParents.Count >0 || y.Count>0;

                children.Add(new MaintHeirarchyNode
                {
                    Id = child.TagId.ToString(),
                    dbId = child.TagId.ToString(),
                    ParentId = child.MaintParentId.ToString(),
                    Name = child.TagFloc + ": " + child.TagFlocDesc,
                    IsDirectory = true, // required to allow DnD onto node.
                    hasItems = hasChildren,
                    nodeType = child.MaintType.MaintTypeName,
                    IsDeleted = child.TagDeleted,
                    IsExpanded = false,
                    SAPStatusId = child.SAPStatusId != null ? (int)child.SAPStatusId : 0,
                    MaintStatus = child.SAPStatusId.ToString()
                });
            }

            // Add PMA as children for current Tag, using FlocXMaintItem relationship. Is not hierarchical.
            var flocXPMAs = _context.FlocXpmassembly
                                    .Include(x => x.Pmassembly)
                                    .Where(t => t.TagId == tag.TagId)
                                    .ToList();

            foreach (var flocXPMA in flocXPMAs)
            {
                children.Add(new MaintHeirarchyNode
                {
                    Id = tag.TagId + ":" + flocXPMA.PmassemblyId.ToString(),
                    dbId = flocXPMA.PmassemblyId.ToString(),
                    ParentId = tag.TagId.ToString(),
                    Name = flocXPMA.Pmassembly.PmassemblyName,
                    IsDirectory = false,
                    IsDeleted = false, // not a feature of PMA.
                    nodeType = "PMA",
                });
            }

            return children;

        }

        public IActionResult ReloadMaintTree(int Height, int Width)
        {
            return ViewComponent("MaintTree", new { height = Height, width = Width });
        }

        public IActionResult ReloadMaintTreeDemand(int Height, int Width)
        {
            return ViewComponent("MaintTreeDemand", new { height = Height, width = Width });
        }

        #endregion


        #region Manage PMAsseblies List Data Grid.

        // select record set

        [HttpGet]
        public object PMAssemblies_GetData(DataSourceLoadOptions loadOptions)
        {
            var dataSet = from rec in _context.Pmassembly
                          select new { rec.PmassemblyId, rec.PmassemblyName, rec.ShortText };

            return DataSourceLoader.Load(dataSet, loadOptions);
        }

        #endregion

        #region Manage Maintenance Items List Data Grid.

        // select record set

        [HttpGet]
        public object MaintenanceItems_GetData(DataSourceLoadOptions loadOptions)
        {
            var dataSet = from rec in _context.MaintItem
                          select new { rec.MaintItemId, rec.MaintItemNum, rec.MaintItemLongText, rec.MaintItemShortText };

            return DataSourceLoader.Load(dataSet, loadOptions);
        }

        #endregion

        #region Manage Non Maintainable Data Grid.


        public IActionResult ReloadNonMaintainedDataGrid(int Height, int Width)
        {
            return ViewComponent("NonMaintainedDataGrid", new { height = Height, width = Width });
        }

        public IActionResult ReloadPMAssembliesDataGrid(int Height, int Width)
        {
            return ViewComponent("PMAssembliesDataGrid", new { height = Height, width = Width });
        }

        public IActionResult ReloadMaintItemDataGrid(int Height, int Width)
        {
            return ViewComponent("MaintenanceItemsDataGrid", new { height = Height, width = Width });
        }

        [HttpGet]
        public object NonMaintained_GetData(DataSourceLoadOptions loadOptions)
        {
            var dataSet = from rec in _context.Tag
                         where rec.MaintType.MaintTypeName == "R"
                        select new { rec.TagId, rec.TagNumber, rec.TagService,  rec.TagFloc, rec.TagFlocDesc, rec.EngDiscId, rec.SubSystem.SubSystemNum};

            return DataSourceLoader.Load(dataSet, loadOptions);
        }

        [Authorize(Roles = "User")]
        [HttpPost]
        public IActionResult NonMaintained_Insert(string values)
        {
            var newOrder = new Tag();
            JsonConvert.PopulateObject(values, newOrder);

            if (!TryValidateModel(newOrder))
                return BadRequest();

            _context.Tag.Add(newOrder);
            _context.SaveChanges();

            return Ok(newOrder);
        }

        [Authorize(Roles = "User")]
        [HttpPut]
        public IActionResult NonMaintained_Update(int key, string values)
        {
            // TODO override to update tag state.
            var order = _context.Tag.First(o => o.TagId == key);
            JsonConvert.PopulateObject(values, order);

            if (!TryValidateModel(order))
                return BadRequest();

            _context.SaveChanges();

            return Ok(order);
        }

        [Authorize(Roles = "User")]
        [HttpDelete]
        public void NonMaintained_Delete(int key)
        {
            var order = _context.Tag.First(o => o.TagId == key);
            _context.Tag.Remove(order);
            _context.SaveChanges();
        }

        [Authorize(Roles = "User")]
        [HttpPost]
        public object NonMaintained_Batch(List<DataChange> changes)
        {
            foreach (var change in changes)
            {
                Tag order;

                if (change.Type == "update" || change.Type == "remove")
                {
                    var key = Convert.ToInt32(change.Key);
                    order = _context.Tag.First(o => o.TagId == key);
                }
                else
                {
                    order = new Tag();
                }

                if (change.Type == "insert" || change.Type == "update")
                {
                    JsonConvert.PopulateObject(change.Data.ToString(), order);

                    if (!TryValidateModel(order))
                        return BadRequest();

                    if (change.Type == "insert")
                    {
                        _context.Tag.Add(order);
                    }
                    change.Data = order;
                }
                else if (change.Type == "remove")
                {
                    _context.Tag.Remove(order);
                }
            }

            _context.SaveChanges();

            return Ok(changes);
        }



        #endregion


        #region Manage Unassigned Data Grid.

        [HttpGet]
        public object Unassigned_GetData(DataSourceLoadOptions loadOptions)
        {
            var dataSet = _context.Tag
                            .Include(x => x.MaintType)
                            .Include(x => x.SubSystem)
                            .Where(x => x.MaintType == null || x.MaintType.MaintTypeName == "M" || x.MaintType.MaintTypeName == "P")
                            .Where(y=> y.MaintParentId == null)
                            .Select(z=> new { z.TagId, z.TagNumber, z.TagService, z.TagFloc, z.TagFlocDesc, z.EngDiscId, z.MaintTypeId, z.MaintType.MaintTypeName, z.SubSystem.SubSystemNum } );

            return DataSourceLoader.Load(dataSet, loadOptions);
        }
        public IActionResult ReloadUnassignedDataGrid(int Height, int Width)
        {
            return ViewComponent("UnassignedDataGrid", new { height = Height, width = Width });
        }

        [Authorize(Roles = "User")]
        [HttpPost]
        public IActionResult Unassigned_Insert(string values)
        {
            var newOrder = new Tag();
            JsonConvert.PopulateObject(values, newOrder);

            if (!TryValidateModel(newOrder))
                return BadRequest();

            _context.Tag.Add(newOrder);
            _context.SaveChanges();

            return Ok(newOrder);
        }

        [Authorize(Roles = "User")]
        [HttpPut]
        public IActionResult Unassigned_Update(int key, string values)
        {
            // TODO override to update tag state.
            var order = _context.Tag.First(o => o.TagId == key);
            JsonConvert.PopulateObject(values, order);

            if (!TryValidateModel(order))
                return BadRequest();

            _context.SaveChanges();

            return Ok(order);
        }

        [Authorize(Roles = "User")]
        [HttpDelete]
        public void Unassigned_Delete(int key)
        {
            var order = _context.Tag.First(o => o.TagId == key);
            _context.Tag.Remove(order);
            _context.SaveChanges();
        }

        [Authorize(Roles = "User")]
        [HttpPost]
        public object Unassigned_Batch(List<DataChange> changes)
        {
            foreach (var change in changes)
            {
                Tag order;

                if (change.Type == "update" || change.Type == "remove")
                {
                    var key = Convert.ToInt32(change.Key);
                    order = _context.Tag.First(o => o.TagId == key);
                }
                else
                {
                    order = new Tag();
                }

                if (change.Type == "insert" || change.Type == "update")
                {
                    JsonConvert.PopulateObject(change.Data.ToString(), order);

                    if (!TryValidateModel(order))
                        return BadRequest();

                    if (change.Type == "insert")
                    {
                        _context.Tag.Add(order);
                    }
                    change.Data = order;
                }
                else if (change.Type == "remove")
                {
                    _context.Tag.Remove(order);
                }
            }

            _context.SaveChanges();

            return Ok(changes);
        }

        #endregion


        #region Manage TagProperties

        [HttpGet]
        public object TagProperties_GetDataGrid(DataSourceLoadOptions loadOptions, int tagId = 1561)
        {
            var TagProp = new List<EngDataClassDataViewModel>();

            // Retrieve data for TagEngData. Called by DevEx Gridview for TagProperties.
            // ComplexKey = TagId + ':' + EngDataCodeId
            var tagDataCodeInfo = _context.Tag
                            .Where(x => x.TagId == tagId)
                            .Include(x => x.TagEngDatas)
                              .ThenInclude(x => x.EngDataCode)
                                .ThenInclude( x=> x.MaintClassXEngDataCode)
                            .FirstOrDefault();

            var tagMaintClassInfo = _context.Tag
                            .Where(x => x.TagId == tagId)
                            .Include(x => x.MaintObjectType)
                              .ThenInclude(x => x.MaintObjectTypeXMaintClass)
                                .ThenInclude (x=> x.MaintClass)
                            .FirstOrDefault();

            var tagMaintClasses = new List<MaintClass>();
            if(tagMaintClassInfo.MaintObjectType != null) {
                foreach(var i in tagMaintClassInfo.MaintObjectType.MaintObjectTypeXMaintClass)
                    tagMaintClasses.Add(i.MaintClass);
            }

            // Get TagEngData.
            foreach (var engData in tagDataCodeInfo.TagEngDatas)
            {
                EngDataClassDataViewModel tmp = new EngDataClassDataViewModel();

                // assigned based on MaintClass
                bool assigned = false;
                // Required Eng Data
                foreach (var required in engData.EngDataCode.MaintClassXEngDataCode)
                {
                    foreach(var maintClass in tagMaintClasses)
                    {
                        if (required.MaintClassId == maintClass.MaintClassId)
                        {
                            assigned = true;
                            tmp.MaintClassName = maintClass.MaintClassName;
                            tmp.TagId = engData.TagId;
                            tmp.EngDataCodeId = engData.EngDataCodeId;
                            tmp.DevExKey = engData.TagId + ":" + tmp.EngDataCodeId;
                            tmp.EngDatavalue = engData.EngDatavalue;
                            tmp.EngDataCodeName = engData.EngDataCode.EngDataCodeName;
                            tmp.EngDataCodeSAPDesc = engData.EngDataCode.EngDataCodeSAPDesc;
                        }
                    }
                }
                // EngData, no MaintClass or MaintClass Not Required.
                //need to handle where its required by a class not for this type as well.
                if(engData.EngDataCode.MaintClassXEngDataCode == null || assigned == false)
                {
                    tmp.MaintClassName = "Other";
                    tmp.TagId = engData.TagId;
                    tmp.EngDataCodeId = engData.EngDataCodeId;
                    tmp.DevExKey = engData.TagId + ":" + tmp.EngDataCodeId;
                    tmp.EngDatavalue = engData.EngDatavalue;
                    //tmp.EngDataCodeName = engData.EngDataCode.EngDataCodeName;
                    tmp.EngDataCodeSAPDesc = engData.EngDataCode.EngDataCodeSAPDesc;
                }

                TagProp.Add(tmp);
            }

            return DataSourceLoader.Load(TagProp, loadOptions);
        }

        [Authorize(Roles = "User")]
        [HttpPut]
        public IActionResult TagProperties_UpdateDataGrid(string key, string values)
        {
            // key = TagId:EngDataCode
            var keys = key.Split(':');
            int tagId = int.Parse(keys[0]);
            int engDataCodeId = int.Parse(keys[1]);

            //retrieve item to update
            var item = _context.TagEngData.Find(tagId, engDataCodeId);

            if(item == null)
                return BadRequest();

            // Replace change to DDLValue with EngDataValue. Why is this here?
            if (values.Contains("DDL"))
            {
                values = values.Replace("DDL", "EngDatavalue");
            }

            JsonConvert.PopulateObject(values, item);

            if (!TryValidateModel(item))
                return BadRequest();

            _context.SaveChanges();

            return Ok();
        }

        public IActionResult ReloadTagProperties(int Height, int Width, int TagId=1628)
        {
            return ViewComponent("TagProperties", new { height = Height, width = Width, tagId=TagId });
        }


        #endregion


    }
}
