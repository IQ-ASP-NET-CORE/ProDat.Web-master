using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
//Added Dynamic to create query strings. need to keep eye on performance.
using System.Linq.Dynamic.Core;
using System.Reflection;
using System.Threading.Tasks;
using System.Threading.Tasks.Sources;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Protocols;
using Microsoft.VisualStudio.Web.CodeGeneration.Contracts.Messaging;
using Newtonsoft.Json;
using ProDat.Web2.Controllers.TagRegister;
using ProDat.Web2.Data;
using ProDat.Web2.Models;
using ProDat.Web2.ViewModels;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Logging;
using SQLitePCL;
using ProDat.Web2.TagLibrary;
using Microsoft.CodeAnalysis.Diagnostics;
using ClosedXML.Excel;
using System.IO;
using System.Linq.Expressions;
using System.Diagnostics;
using DocumentFormat.OpenXml.Wordprocessing;
using Microsoft.EntityFrameworkCore.Query.Internal;
using Microsoft.AspNetCore.Authorization;
using ProDat.Web2.Models.SAPRequirements;
using Microsoft.Data.SqlClient;
// ADO connector.
using Microsoft.Extensions.Configuration;
using ProDat.Web2.Migrations;
using Tag = ProDat.Web2.Models.Tag;

namespace ProDat.Web2.Controllers
{
    public class TagsController : Controller
    {
        private readonly TagContext _context;
        private readonly ILogger<TagContext> _logger;
        private readonly IModelMetadataProvider _provider;
        private IConfiguration _configuration;

        public TagsController(TagContext context, ILogger<TagContext> logger, IModelMetadataProvider provider, IConfiguration Configuration)
        {
            _provider = provider;
            _context = context;
            _logger = logger;
            _configuration = Configuration;
        }

        // INDEX ###################
        // GET: Tags
        public async Task<IActionResult> Index(
            string sortOrder,
            TagRegisterSearchViewModel searchModel,
            string currentFilter,
            int? pageNumber
        )
        {
            _logger.LogInformation("Enter TagsController {Time}", DateTime.UtcNow);

            // set User, Admin flags.
            var isAdmin = false;
            if (User.IsInRole("Admin"))
                isAdmin = true;


            if (searchModel.TagViewId == null)
            {
                searchModel.TagViewId = 1;
                ViewData["CurrentSearchModel"] = searchModel;
            }


            // ####################
            // Generate json ddl & DataList dynamically.
            // ####################
            Dictionary<string, List<string>> DicHelper = new Dictionary<string, List<string>>();
            DDLBuilders ddlBuilders = new DDLBuilders();

            //Inject ADO connection using appsettings or startup.
            string AdoConnectionString = _configuration["ConnectionStrings:DefaultConnection"];

            using (SqlConnection connection = new SqlConnection(AdoConnectionString))
            {
                connection.Open();

                var cols = _context.TagViewColumns
                                .Include(x => x.TagViewColumnsUser)
                                .Where(t => t.TagViewId == searchModel.TagViewId)
                                .OrderBy(t => t.TagViewOrder).ToList();


                // retrieve just this users custom widths.
                foreach (var col in cols)
                {
                    var tmp1 = User.Identity.Name;
                    col.TagViewColumnsUser = col.TagViewColumnsUser.Where(x => x.UserName == User.Identity.Name);
                }

                foreach (var col in cols)
                {
                    // build out star lists required to deploy content.
                    if (col.ColumnName.EndsWith("Id"))
                    {
                        // Json Lists for HTTPClient
                        string ViewDataName = col.ColumnName.Substring(0, col.ColumnName.Length - 2);

                        // if bad form, ignore. Stars must specify S, M or L.
                        if (col.starField == null)
                            continue;

                        // col.starfield??
                        ViewData[ViewDataName + "ListJson"] = ddlBuilders.CustomDDls(connection, col.ColumnName, isAdmin);

                        //View construction
                        var temp = ddlBuilders.DicHelperBuilder(connection, col.ColumnName);
                        DicHelper[ViewDataName + "Search"] = temp;
                        ViewData[ViewDataName + "Search"] = temp; // DO I NEED THIS?
                    }
                }
            }
            ViewData["ddls"] = DicHelper;  // Do I need this??

            // #######################
            // more ViewBag data.
            // #######################

            //PageRecordsListJson
            List<customSelectItem> ddlList = new List<customSelectItem>();
            int[] pageArray = new int[] { 25, 75, 150 };
            foreach (int rec in pageArray)
            {
                ddlList.Add(new customSelectItem(rec, rec.ToString(), rec.ToString()));
            }
            if (isAdmin)
                ddlList.Add(new customSelectItem(-2, "--Add New Item", "--Add New Item"));
            ViewData["PageRecordsListJson"] = JsonConvert.SerializeObject(ddlList);

            //TagViewListJson
            ddlList = new List<customSelectItem>();
            foreach (var rec in _context.TagView.OrderBy(s => s.TagViewName))
            {
                ddlList.Add(new customSelectItem(rec.TagViewId, rec.TagViewName, rec.TagViewName));
            }
            if (isAdmin)
                ddlList.Add(new customSelectItem(-2, "--Add New Item", "--Add New Item"));
            ViewData["TagViewListJson"] = JsonConvert.SerializeObject(ddlList);

            //TagViewColumnsList
            List<ViewColsData> ddlList2 = new List<ViewColsData>();
            var recs = _context.TagViewColumns
                        .Where(x => x.TagViewId == searchModel.TagViewId)
                        .OrderBy(t => t.TagViewOrder).ToList();

            // retain Custom widths for this user only.
            foreach (var rec in recs)
                rec.TagViewColumnsUser = rec.TagViewColumnsUser.Where(x => x.UserName == User.Identity.Name).ToList();

            foreach (var rec in recs)
            {
                if (rec.TagViewColumnsUser.Count() > 0)
                    ddlList2.Add(new ViewColsData(rec.TagViewId, rec.ColumnName, rec.ColumnName.Substring(0, rec.ColumnName.Length - 2), rec.starField, rec.TagViewColumnsUser.FirstOrDefault().ColumnWidth));
                else
                    ddlList2.Add(new ViewColsData(rec.TagViewId, rec.ColumnName, rec.ColumnName.Substring(0, rec.ColumnName.Length - 2), rec.starField, rec.ColumnWidth));
            }

            //if (isAdmin)
            //    ddlList.Add(new customSelectItem(-2, "--Edit Columns", "--Edit Columns"));
            ViewData["TagViewColumns"] = ddlList2;
            ddlList2 = null;

            _logger.LogInformation("completed ViewData Extracts {Time}", DateTime.UtcNow);

            // SAP Validation
            var EAId = _context.EntityAttribute.Where(x => x.EntityName == "Tag")
                            .Include(x => x.EntityAttributeRequirements);
            ViewData["SapValidationJson"] = JsonConvert.SerializeObject(EAId);


            // ###############################
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
                searchModel = JsonConvert.DeserializeObject<TagRegisterSearchViewModel>(currentFilter);
                ViewData["CurrentSearchModel"] = searchModel;
                ViewData["CurrentSearchModelJson"] = currentFilter;
            }

            // #########################################
            // the business logic class ( manages search filters)
            var business = new TagRegisterBusinessLogic(_context);
            var tagModel = business.GetTags(searchModel);

            // #########################################
            // Sort requested by View
            ViewData["SortOrder"] = sortOrder;

            // set TagSortParam result for its NEXT CLICK. Default to TagNumber_desc
            string nextSortAction = sortOrder;
            if (String.IsNullOrEmpty(nextSortAction))
            {
                nextSortAction = "TagNumber_descending";
            }
            else
            {
                if (nextSortAction.EndsWith("_descending"))
                {
                    nextSortAction = nextSortAction.Substring(0, nextSortAction.Length - 11);
                }
                else
                {
                    nextSortAction = nextSortAction + "_descending";
                }
            }
            ViewData["TagSortParam"] = nextSortAction;

            if (!string.IsNullOrEmpty(sortOrder))
            {
                if (sortOrder.EndsWith("_descending"))
                {
                    tagModel = tagModel.OrderBy(sortOrder.Substring(0, sortOrder.Length - 11) + " descending");
                }
                else
                {
                    tagModel = tagModel.OrderBy(sortOrder + " ascending");
                }
            }
            else
            {
                // default sort is tagnumber ascending.
                tagModel = tagModel.OrderBy(v => v.TagNumber);
            }

            // ##############################
            // Apply Pagination
            int pageSize = 25;
            if (searchModel.PageRecordsId != null)
                pageSize = (int)searchModel.PageRecordsId;

            _logger.LogInformation("Returning TagsController View {Time}", DateTime.UtcNow);

            return View(await PaginatedList<Models.Tag>.CreateAsync(tagModel.AsNoTracking(), pageNumber ?? 1, pageSize));
        }


        // GET: Tags/Details/5
        public async Task<IActionResult> Details(int? Id)
        {
            if (Id == null)
            {
                return NotFound();
            }

            var tag = await _context.Tag
                .Include(t => t.EngParent)
                .Include(t => t.EngDisc)
                .Include(t => t.MaintCriticality)
                .Include(t => t.MaintLocation)
                .Include(t => t.MaintObjectType)
                .Include(t => t.SubSystem)
                .FirstOrDefaultAsync(m => m.TagId == Id);
            if (tag == null)
            {
                return NotFound();
            }

            return View(tag);
        }

        // GET: Tags/Create
        public IActionResult Create()
        {
            ViewData["EngParentId"] = new SelectList(_context.Tag.OrderBy(x => x.TagNumber), "TagId", "TagNumber", "");
            ViewData["EngDiscId"] = new SelectList(_context.EngDisc.OrderBy(x => x.EngDiscName), "EngDiscId", "EngDiscName", "");
            ViewData["MaintCriticalityId"] = new SelectList(_context.MaintCriticality.OrderBy(x => x.MaintCriticalityName), "MaintCriticalityId", "MaintCriticalityName", "");
            ViewData["MaintLocationId"] = new SelectList(_context.MaintLocation.OrderBy(x => x.MaintLocationName), "MaintLocationId", "MaintLocationName", "");
            ViewData["MaintObjectTypeId"] = new SelectList(_context.MaintObjectType.OrderBy(x => x.MaintObjectTypeName), "MaintObjectTypeId", "MaintObjectTypeName", "");
            ViewData["SubSystemId"] = new SelectList(_context.SubSystem.OrderBy(x => x.SubSystemNum), "SubSystemId", "SubSystemNum", "");
            return View();
        }

        // POST: Tags/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("TagId,TagNumber,TagFLOC,TagFlocDesc,TagMaintQuery,EngParentId,EngDiscId,MaintLocationId,MaintCriticalityId,MaintObjectTypeId,SubSystemId")] Models.Tag tag)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tag);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["EngParentId"] = new SelectList(_context.Tag, "TagId", "TagNumber", tag.EngParentId);
            ViewData["EngDiscId"] = new SelectList(_context.EngDisc, "EngDiscId", "EngDiscName", tag.EngDiscId);
            ViewData["MaintCriticalityId"] = new SelectList(_context.MaintCriticality, "MaintCriticalityId", "MaintCriticalityName", tag.MaintCriticalityId);
            ViewData["MaintLocationId"] = new SelectList(_context.MaintLocation, "MaintLocationId", "MaintLocationName", tag.MaintLocationId);
            ViewData["MaintObjectTypeId"] = new SelectList(_context.MaintObjectType, "MaintObjectTypeId", "MaintObjectTypeName", tag.MaintObjectTypeId);
            ViewData["SubSystemId"] = new SelectList(_context.SubSystem, "SubSystemId", "SubSystemNum", tag.SubSystemId);
            return View(tag);
        }

        // GET: Tags/Edit/5
        public async Task<IActionResult> Edit(int? Id)
        {
            if (Id == null)
            {
                return NotFound();
            }

            var tag = await _context.Tag.FindAsync(Id);
            if (tag == null)
            {
                return NotFound();
            }
            ViewData["EngParentId"] = new SelectList(_context.Tag, "TagId", "TagNumber", tag.EngParentId);
            ViewData["EngDiscId"] = new SelectList(_context.EngDisc, "EngDiscId", "EngDiscName", tag.EngDiscId);
            ViewData["MaintCriticalityId"] = new SelectList(_context.MaintCriticality, "MaintCriticalityId", "MaintCriticalityName", tag.MaintCriticalityId);
            ViewData["MaintLocationId"] = new SelectList(_context.MaintLocation, "MaintLocationId", "MaintLocationName", tag.MaintLocationId);
            ViewData["MaintObjectTypeId"] = new SelectList(_context.MaintObjectType, "MaintObjectTypeId", "MaintObjectTypeName", tag.MaintObjectTypeId);
            ViewData["SubSystemId"] = new SelectList(_context.SubSystem, "SubSystemId", "SubSystemNum", tag.SubSystemId);
            return View(tag);
        }

        // POST: Tags/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int Id, [Bind("TagId,TagNumber,TagFLOC,TagFlocDesc,TagMaintQuery,EngParentId,EngDiscId,MaintLocationId,MaintCriticalityId,MaintObjectTypeId,SubSystemId")] Models.Tag tag)
        {
            if (Id != tag.TagId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tag);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TagExists(tag.TagId))
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
            ViewData["EngParentId"] = new SelectList(_context.Tag, "TagId", "TagNumber", tag.EngParentId);
            ViewData["EngDiscId"] = new SelectList(_context.EngDisc, "EngDiscId", "EngDiscName", tag.EngDiscId);
            ViewData["MaintCriticalityId"] = new SelectList(_context.MaintCriticality, "MaintCriticalityId", "MaintCriticalityName", tag.MaintCriticalityId);
            ViewData["MaintLocationId"] = new SelectList(_context.MaintLocation, "MaintLocationId", "MaintLocationName", tag.MaintLocationId);
            ViewData["MaintObjectTypeId"] = new SelectList(_context.MaintObjectType, "MaintObjectTypeId", "MaintObjectTypeName", tag.MaintObjectTypeId);
            ViewData["SubSystemId"] = new SelectList(_context.SubSystem, "SubSystemId", "SubSystemNum", tag.SubSystemId);
            return View(tag);
        }

        // GET: Tags/Delete/5
        public async Task<IActionResult> Delete(int? Id)
        {
            if (Id == null)
            {
                return NotFound();
            }

            var tag = await _context.Tag
                .Include(t => t.EngParent)
                .Include(t => t.MaintCriticality)
                .Include(t => t.MaintLocation)
                .Include(t => t.MaintObjectType)
                .Include(t => t.SubSystem)
                .FirstOrDefaultAsync(m => m.TagId == Id);
            if (tag == null)
            {
                return NotFound();
            }

            return View(tag);
        }

        // POST: Tags/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int Id)
        {
            var tag = await _context.Tag.FindAsync(Id);
            _context.Tag.Remove(tag);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // POST: Tags/AjaxFieldUpdate/5?FieldName, FieldValue
        [Authorize(Roles = "User")]
        [HttpPost]
        public async Task<IActionResult> AjaxFieldUpdate(string sId, string attributeName, string newValue)
        {
            // ##################################
            // Updates a Tag Attribute value
            // Performs validation before update.

            // capture Validation issues here.
            List<string> Validation_errors = null;

            attributeName = attributeName.Substring(0, 4) == "Tag." ? attributeName.Substring(4) : attributeName;

            // retrieve tag to update.
            var tag = await _context.Tag.FindAsync(int.Parse(sId));
            if (int.Parse(sId) != tag.TagId)
            {
                Validation_errors.Add($"The TagId {sId} does not exist.");
                return BadRequest(new { reset_value = "", errors = Validation_errors });
            }

            // prevent circular setting of MaintParent..
            // TODO: handle problem with Ajax, so that value is reset in UI.
            if (attributeName == "MaintParentId")
            {
                if (isCircularReference(tag.TagId, int.Parse(newValue)))
                {
                    return BadRequest(new { reset_value = "", errors = Validation_errors });
                }
            }

            // ###################################
            // determine attribute type being set
            PropertyInfo myProp = tag.GetType().GetProperty(attributeName);
            var oldValue = myProp.GetValue(tag) ?? "Null";

            string propertyType = myProp.PropertyType.Name;

            if (propertyType != "String" && propertyType != "Boolean")
            {
                // need to find a way to get type without looking everywhere.
                propertyType = myProp.PropertyType.GenericTypeArguments[0].Name;
            }

            // ####################################
            // Validate attribute
            ValidationContext vc = new ValidationContext(tag, null, null);
            vc.MemberName = attributeName;
            ICollection<ValidationResult> results = new List<ValidationResult>();

            if (propertyType == "Int32")
            {
                int? intNewValue = null;
                if (newValue == "-1")
                {
                    intNewValue = default(int?);
                }
                else
                {
                    // standard int.parse will throw exception if null or invalid data.
                    // As null is allowed for optional fields, need to handle this and let Validator do its job.
                    int temp;
                    intNewValue = int.TryParse(newValue, out temp) ? temp : default(int?);
                }
                bool Valid = Validator.TryValidateProperty(intNewValue, vc, results);
                if (Valid)
                {
                    myProp.SetValue(tag, intNewValue, null);
                }
                else
                {
                    foreach (ValidationResult err in results)
                    {
                        Validation_errors.Add(err.ErrorMessage);
                    }
                    return BadRequest(new { reset_value = oldValue, errors = Validation_errors });
                }
            }
            else if (propertyType == "String")
            {
                bool Valid = Validator.TryValidateProperty(newValue, vc, results);
                if (Valid)
                {
                    myProp.SetValue(tag, newValue, null);
                }
                else
                {
                    foreach (ValidationResult err in results)
                    {
                        Validation_errors.Add(err.ErrorMessage);
                    }
                    return Ok(new { reset_value = oldValue, errors = Validation_errors });
                }
            }
            else if (propertyType == "Boolean")
            {
                bool setValue = false;
                if (newValue.Equals("true", StringComparison.InvariantCultureIgnoreCase))
                {
                    setValue = true;
                }

                bool Valid = Validator.TryValidateProperty(setValue, vc, results);
                if (Valid)
                {
                    myProp.SetValue(tag, setValue, null);
                }
            }
            else
            {
                Validation_errors.Add("unknown datatype. to be added.");
                return BadRequest(new { reset_value = oldValue, errors = Validation_errors });
            }

            var Created = DateTime.UtcNow;
            var CreatedBy = User.Identity.Name;
            _context.Historian.Add(
            new Historian
            {
                AttributeName = attributeName,
                AttributeValue = newValue,
                Pk1 = int.Parse(sId),
                CreatedBy = CreatedBy,
                Created = Created
            }
              );

            await _context.SaveChangesAsync();

            // HISTORIAN HERE.
            // Apply Historian + user date etc.


            // return 200.
            return Ok(new { message = "Success" });

        }

        // faster method to retrieve hierarchy. Used to test for creation of circular references.
        public int CheckTagHierarchy(int TagId, int sourceTagId, int depth = 0, int counter = 0)
        {
            Tag tag = _context.Tag
                .Include(t => t.InverseMaintParents)
                .Where(t => t.TagId == TagId)
                .FirstOrDefault();

            foreach (Tag child in tag.InverseMaintParents)
            {
                if (depth < 20)
                {
                    counter = counter + CheckTagHierarchy(child.TagId, sourceTagId, depth + 1, counter);
                }
                if (counter > 1)
                    return counter;
            }

            return counter + TagId == sourceTagId ? 1 : 0;
        }



        public bool isCircularReference(int TagId, int ParentId)
        {
            // Test if by setting this tags MaintParentId to ParentId, we create a circular reference.
            var hier = CheckTagHierarchy(TagId, ParentId);
            return hier > 1;
        }

        public async Task<IActionResult> AjaxFieldViewWidth(string viewId, string attributeName, string columnWidth)
        {
            // test provided values are meaningful.
            if (int.TryParse(viewId, out int iViewId) && int.TryParse(columnWidth, out int iColumnWidth))
            {
                // get existing entity from TagViewColumns
                Models.TagViewColumns ViewCol = await _context.TagViewColumns
                                    .Where(t => t.TagViewId == iViewId)
                                    .Where(t => t.ColumnName == attributeName)
                                    .FirstOrDefaultAsync();

                // If nothing returned, error out, as you are modifying a viewColumn that dosent exist.
                if (ViewCol == null)
                    return BadRequest();

                // retrieve record if exists in TagViewColumnsUser
                Models.TagViewColumnsUser UserViewCol = await _context.TagViewColumnsUser
                                   .Where(t => t.TagViewColumnsId == ViewCol.TagViewColumnsId)
                                   .Where(t => t.UserName == User.Identity.Name)
                                   .FirstOrDefaultAsync();

                if (UserViewCol == null)
                {
                    UserViewCol = new Models.TagViewColumnsUser();
                    UserViewCol.TagViewColumnsId = ViewCol.TagViewColumnsId;
                    UserViewCol.UserName = User.Identity.Name;
                    UserViewCol.ColumnWidth = iColumnWidth;
                    _context.Add(UserViewCol);
                }
                else
                {
                    //update records value
                    UserViewCol.ColumnWidth = iColumnWidth;
                }
                await _context.SaveChangesAsync();

                // return 200.
                return Ok(new { message = "Success" });
            }

            return BadRequest();

        }

        // Return Excel document to User.
        // Takes current TagRegisterSearchViewModel to filter.
        public IActionResult Excel(string currentFilter)
        {
            // Retrieve all Tags into recordset. State which star models to include.
            var tagModel = (from t in _context.Tag
                                //.Include(t => t.EngParent)
                                .Include(t => t.EngDisc)
                                .Include(t => t.MaintCriticality)
                                .Include(t => t.MaintLocation)
                                .Include(t => t.MaintObjectType)
                                .Include(t => t.SubSystem)
                                .Include(t => t.SubSystem.Systems)
                                .Include(t => t.MaintWorkCentre)
                                // additional stars
                                .Include(t => t.CommClass)
                                .Include(t => t.MaintEdcCode)
                                .Include(t => t.EngClass)
                                .Include(t => t.Ipf)
                                .Include(t => t.Location)
                                .Include(t => t.MaintPlannerGroup)
                                .Include(t => t.MaintScePsReviewTeam)
                                .Include(t => t.MaintSortProcess)
                                .Include(t => t.MaintType)
                                .Include(t => t.Manufacturer)
                                .Include(t => t.RbiSil)
                                .Include(t => t.Rbm)
                                .Include(t => t.Rcm)
                                .Include(t => t.Vib)
                                // costly? to test and confirm.
                                .Include(t => t.MaintParent)
                                .Include(t => t.MaintStructureIndicator)
                                .Include(t => t.PerformanceStandard)
                                .Include(t => t.ExMethod)
                          select t).AsQueryable();

            var TagMeta = _provider.GetMetadataForType(typeof(Models.Tag));

            using (var workbook = new XLWorkbook())
            {
                IXLWorksheet worksheet = workbook.Worksheets.Add("Tags");
                int currentRow = 1;

                // fieldNames to ignore.
                ICollection<string> ignoreFields = new Collection<string>();

                //InverseMaintParents takes long time to query as each Tag hashes whole table to get parent.
                ignoreFields.Add("InverseMaintParents");

                // Fields to export = distinct set of fieldnames in ColumnSets, so is controlled by Admin table.
                List<string> fieldsToExport = new List<string> { };
                var tmp = _context.ColumnSets
                                  .Where(x => x.ColumnSetsEntity == "Tag")
                                  .Select(x => x.ColumnName);

                fieldsToExport = (List<string>)tmp.Distinct().ToList();

                // strip Id from FieldName. We'll dig into these entities to retrieve Num or Name attribute.
                for (int j = 0; j < fieldsToExport.Count(); j++)
                    if (fieldsToExport[j].EndsWith("Id"))
                        fieldsToExport[j] = fieldsToExport[j].Substring(0, fieldsToExport[j].Length - 2);

                // build header from metadata.
                int i = 1;
                ICollection<string> fields = new Collection<string>();
                foreach (var property in TagMeta.Properties)
                {
                    if (ignoreFields.Contains(property.Name) || property.Name.EndsWith("Id"))
                        continue;

                    // If not basic attribute, it is an entity.
                    // Drill into entity and retrieve property ending with Num, else Name
                    if (property.ModelType != typeof(string) && property.ModelType != typeof(int) && property.ModelType != typeof(DateTime) && property.ModelType != typeof(bool))
                    {
                        string entityName = property.Name;
                        var ChildMeta = _provider.GetMetadataForType(property.ModelType);
                        foreach (var childProperty in ChildMeta.Properties)
                        {
                            // Stop outputting Name fields for Subsystem/system
                            if (childProperty.Name.EndsWith("SubSystemName") || childProperty.Name.EndsWith("SystemName"))
                                continue;

                            // need to test if Num exists, and take that as priority. Dont dig further if Num found.
                            if (childProperty.Name == entityName + "Num" || childProperty.Name == entityName + "Name")
                            {
                                if (fieldsToExport.Contains(entityName))
                                {
                                    fields.Add(entityName + "." + childProperty.Name);
                                    worksheet.Cell(currentRow, i++).Value = childProperty.Name;
                                    continue;
                                }
                            }
                            // dig dig... go to next level and get System Num/Name
                            if (childProperty.ModelType == typeof(Systems))
                            {
                                //string entityName2 = childProperty.Name;
                                string entityName2 = "System";
                                var ChildMeta2 = _provider.GetMetadataForType(childProperty.ModelType);
                                foreach (var childProperty2 in ChildMeta2.Properties)
                                {
                                    if (childProperty2.Name == entityName2 + "Num" || childProperty2.Name == entityName2 + "Name")
                                    {
                                        if (fieldsToExport.Contains(entityName))
                                        {
                                            fields.Add(entityName + "." + childProperty.Name + "." + childProperty2.Name);
                                            worksheet.Cell(currentRow, i++).Value = childProperty2.Name;
                                            // num comes before name on child entries.
                                            break;
                                        }
                                    }
                                }
                            }
                        }
                    }
                    else
                    {
                        if (fieldsToExport.Contains(property.Name))
                        {
                            // Add fieldname, and index.
                            fields.Add(property.Name);
                            worksheet.Cell(currentRow, i++).Value = property.Name;
                        }
                    }
                }

                worksheet.Cell(currentRow, i).Value = "NEW_TagNumber";

                // #####################
                // ##  Export Values  ##
                // #####################
                ExpressionGet t = new ExpressionGet();
                foreach (Models.Tag tag in tagModel)
                {
                    currentRow++;
                    i = 1;
                    foreach (string field in fields)
                    {
                        //if (field.Contains("Systems") && tag.SubSystem != null)
                        //    Debug.Print("hold.");

                        var RetVal = t.getChildPropertyValue(field, tag);
                        if (RetVal == null)
                            RetVal = "";

                        if (RetVal.GetType() == typeof(bool))
                        {
                            RetVal = RetVal.ToString();
                        }
                        worksheet.Cell(currentRow, i++).SetValue(RetVal);
                    }
                }

                worksheet.Columns("A:AZ").AdjustToContents();

                using (var stream = new MemoryStream())
                {
                    workbook.SaveAs(stream);
                    var content = stream.ToArray();

                    return File(
                        content,
                        "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                        "TagRegister.xlsx");
                }
            }
        }

        private bool TagExists(int Id)
        {
            return _context.Tag.Any(e => e.TagId == Id);
        }
    }
}
