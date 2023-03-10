using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using ClosedXML.Excel;
using DocumentFormat.OpenXml.Presentation;
using DocumentFormat.OpenXml.Wordprocessing;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using ProDat.Web2.Data;
using ProDat.Web2.Models.ETL;
using ProDat.Web2.ViewModels;

namespace ProDat.Web2.Controllers.Import
{
    public class ImportsController : Controller
    {
        private readonly TagContext _context;

        public ImportsController(TagContext context)
        {
            _context = context;
        }

        // GET: Imports
        public async Task<IActionResult> Index()
        {
            // confirm user has mfa, else redirect to MFA setup.
            var claimTwoFactorEnabled =
               User.Claims.FirstOrDefault(t => t.Type == "amr");

            if (claimTwoFactorEnabled != null && "mfa".Equals(claimTwoFactorEnabled.Value))
            {
                // continue
            }
            else
            {
                return Redirect(
                    "/Identity/Account/Manage/TwoFactorAuthentication");
            }

            var tagContext = await _context.Import.Include(i => i.ImportType).OrderByDescending(x => x.ImportStatus).ThenByDescending(x => x.Created).ToListAsync();

            return View(tagContext);
        }

        // GET: Imports/Create
        public IActionResult Create()
        {
            ViewData["ImportTypeId"] = new SelectList(_context.ImportType, "ImportTypeId", "ImportTypeName");
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create([Bind("ImportId,ImportStatus,ImportTypeId,CreatedComment,File")] Models.ETL.Import Import)
        {
            var newFileName = Path.GetRandomFileName() + ".xlsx";
            var uploads = Path.GetTempPath();
            string filePath = "";

            // if missing required input, return current view.
            if (!ModelState.IsValid)
            {
                ViewData["ImportTypeId"] = new SelectList(_context.ImportType, "ImportTypeId", "ImportTypeName", Import.ImportTypeId);
                return View(Import);
            }

            // populate Created and CreatedBy
            Import.Created = DateTime.UtcNow;
            Import.CreatedBy = User.Identity.Name;
            Import.ImportStatus = "Extracting Excel";

            // Update Import Table
            _context.Add(Import);
            await _context.SaveChangesAsync();

            // get latest ImportId.. good for 99.999% scenarios. will need transaction if #concurrent users per db (who use import module) >1
            var ImportId = _context.Import.Max(x => x.ImportId);

            // Process file into ImportExtract
            var originalFileName = Import.File.FileName;

            if (Import.File.FileName != null && Import.File.FileName.EndsWith("xlsx"))
            {
                filePath = Path.Combine(uploads, newFileName);

                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await Import.File.CopyToAsync(fileStream);
                }
            }


            if (Import.File != null && Import.File.Length > 0 && Import.File.FileName.EndsWith(".xlsx"))
            {
                // This should be dynamic. 
                // TODO: Replace with Call to stored procedure which grabs table columns (and fixes fk value for suffix Num of Name)
                // NEW_Tagnumber is not in db, so add manually always...
                string[] ValidColumns = null;
                string[] bitFields = new string[] { };
                if (Import.ImportTypeId == 1) //TAG
                { 
                    ValidColumns = new string[] { "TagNumber", "NEW_TagNumber", "TagFloc", "TagService", "TagFLOC", "SystemNum", "SubSystemNum", "EngClassName", 
                                "MaintLocationName", "LocationName", "MaintTypeName", "MaintStatusName", "EngStatusName", 
                                "MaintWorkCentreName", "EdcName", "MaintStructureIndicatorName", "CommissioningSubsystemName", "CommClassName",
                                "CommZoneName", "MaintPlannerGroupName", "MaintenanceplanName", "MaintCriticalityName", "PerformanceStandardName", 
                                "MaintClassName", "KeyDocNum", "PoName", "TagSource", "TagDeleted",
                                "TagFlocDesc", "TagMaintQuery", "TagComment", "ModelNum", "VibName", 
                                "Tagnoneng", "TagVendorTag", "MaintObjectTypeName", "RbiSilName", "IpfName", 
                                "RcmName", "TagRawNumber", "TagRawDesc", "MaintScePsReviewTeamName", "MaintScePsJustification", 
                                "TagMaintCritComments", "RbmName", "ManufacturerName", "ExMethodName", "TagRbmMethod", 
                                "TagVib", "TagSrcKeyList", "TagBomReq", "TagSpNo", "MaintSortProcessName", 
                                "TagCharacteristic", "TagCharValue", "TagCharDesc", "EngParentID1", "EngDiscName",
                                "ModelName", "SerialNumber", "PlantSectionName", "MaintenancePlantNum", 
                                // self referencing fields:
                                "MaintParentID"
                            };
                    bitFields = new string[] { "TagDeleted", "TagMaintQuery", "Tagnoneng" };
                }
                if(Import.ImportTypeId == 2) //TLH
                {
                    ValidColumns = new string[] {
                        "TaskListShortText", "PerfStdAppDel", "RegBodyAppDel", "ChangeRequired", 
                        "MaintPackageName", "MaintStrategyName", "MaintWorkCentreName", "MaintenancePlantNum", "PerformanceStandardName", 
                        "PmassemblyName", "RegulatoryBodyName", "SysCondName", "TasklistCatName", 
                        "MaintPlannerGroupName", "StatusCode", "TLHNumber"
                        //,"TaskListGroupName", "Counter" removed as these make the PseudoPK
                        //"ScePsReviewId", //"TaskListClassId", 
                    };
                }
                if (Import.ImportTypeId == 3) //MI
                { //Entity Names + field names - IDs
                    ValidColumns = new string[] {
                        "MaintItemNum", "MaintItemShortText", "FMaintItemHeaderFloc", "MaintItemObjectListFloc", "MaintItemObjectListEquip", "MaintItemMainWorkCentre",
                        "MaintItemMainWorkCentrePlant", "MaintItemOrderType", "MaintItemRevNo", "MaintItemUserStatus", "MaintItemSystemCondition_Old", "MaintItemConsequenceCategory",
                        "MaintItemConsequence", "MaintItemLikelihood", "MaintItemProposedPriority", "MaintItemProposedTi", "MaintItemLongText", "MaintItemTasklistExecutionFactor",
                        "TaskListExecutionFactor", "MaintItemDoNotRelImmed", "bDoNotRelImmed",
                        "MaintPlanName",
                        "MaintPlannerGroupName",
                        "MaintenancePlantNum",
                        "SysCondName",
                        "NEW_MINumber"

                    };
                    bitFields = new string[] { "bDoNotRellmmed" };
                }
                if (Import.ImportTypeId == 5) //MP
                {
                    ValidColumns = new string[] {
                        "MaintPlanName", "ShortText", "Sort",
                        "CycleModFactor","StartDate","ChangeStatus","StartingInstructions","CallHorizon",
                        "SchedulingPeriodValue", "NEW_MPName"
                    };
                }
                if (Import.ImportTypeId == 6) //TLO
                {
                    ValidColumns = new string[] {
                        "TaskListOperationName", "SubOperationNum", "OperationName", "OperationDescription",
                        "MaintWorkCentreName", "MaintenancePlantNum", "ControlKeyName", "SysCondName", "RelationshiptoOperationName",
                        "OperationShortText", "OperationLongText", "WorkHrs", "CapNo", "MaintPackageName",
                        "DocNum", "Ti", "OffSite", "FixedOperQty", "ChangeRequired",
                        "TaskListOperationName" // Pseudo Column part of Export. 
                        //,"TaskListHeaderId", "OperationNum" removed as these make the PseudoPK
                    };

                    // need this to handle nulls as false, else problems ensue in bit validation
                    bitFields = new string[] { "Ti", "OffSite" };
                }

                // This should be dynamic. 
                // TODO: Replace with Call to stored procedure, or build using Tag metadata w/Reflection.
                //         if heavy load, we could build using reflection into table structure. (i.e. recreate on db change only). 
                // For now, there will only be a few of these in Tag Module.
                //
                // If it encounters key, dig into child entity of matching name to get attribute Named within. 
                //dicAdditional[keyAttribute] = [requiredPseudoFK1, requiredPseudoFK2, ...]
                Dictionary<string, string[]> additionalFKs = new Dictionary<string, string[]>();
                additionalFKs.Add("SubSystemNum", new[] { "SystemNum" });
                //additionalFKs.Add("LocationName", new[] { "AreaName" });

                // Open Excel. Iterate rows and import content into Tag.ImportExtract entity.
                using (XLWorkbook workbook = new XLWorkbook(filePath))
                {
                    // iterate sheets
                    foreach (IXLWorksheet sheet in workbook.Worksheets)
                    {
                        bool FirstRow = true;
                        string readRange = "1:1"; // determine row count

                        // dictionary to map headername to column index
                        Dictionary<string, int> dHeader = new Dictionary<string, int>();
                        Dictionary<int, string> dHeaderInv = new Dictionary<int, string>();

                        foreach (IXLRow row in sheet.RowsUsed())
                        {
                            //Use firstRow to get number of columns.
                            // Create dictionary of column values. 
                            if (FirstRow)
                            {
                                //Checking the Last cell used for column generation in datatable  
                                readRange = string.Format("{0}:{1}", 1, row.LastCellUsed().Address.ColumnNumber);
                                int i = 0;
                                foreach (IXLCell cell in row.Cells(readRange))
                                {
                                    ++i;
                                    var cellValue = cell.Value.ToString();
                                    if (ValidColumns.Contains(cellValue))
                                    {
                                        dHeader.Add(cellValue, i);
                                        dHeaderInv.Add(i, cellValue);
                                    }
                                }
                                FirstRow = false;
                            }
                            else
                            {
                                /* squidge row into ImportExtract format:
                                 * for each NON SPECIAL field (tagName, ?) 
                                 *  if hasValue
                                 *    set pk
                                 *    AttributeName
                                 *    AttributeValue
                                 *    {AdditionalPseudoFKData}
                                */

                                // these values are shared based on import details.
                                // TODO: Get from Form or Database.
                                ImportExtract rec = new ImportExtract();
                                rec.EntityName = Import.ImportTypeId.ToString();
                                rec.ImportId = ImportId;

                                //PK for entityName == Tag:
                                // TODO: case select based on EntityName
                                switch (Import.ImportTypeId)
                                {
                                    case 1: //Tag
                                        rec.EntityPseudoPK = row.Cell(dHeader["TagNumber"]).Value.ToString();
                                        break;
                                    case 2: //Task List Header
                                        rec.EntityPseudoPK = row.Cell(dHeader["TLHNumber"]).Value.ToString();
                                        break;
                                    case 3: // MaintItem
                                        rec.EntityPseudoPK = row.Cell(dHeader["MaintItemNum"]).Value.ToString();
                                        break;
                                    case 4: // TagXDoc
                                        rec.EntityPseudoPK = row.Cell(dHeader["x"]).Value.ToString();
                                        break;
                                    case 5: // MaintPlan
                                        rec.EntityPseudoPK = row.Cell(dHeader["MaintPlanName"]).Value.ToString();
                                        break;
                                    case 6: // TaskListOperation
                                        rec.EntityPseudoPK = row.Cell(dHeader["TaskListOperationName"]).Value.ToString();
                                        break;
                                    default:

                                        break;
                                }


                                foreach (KeyValuePair<string, int> kv in dHeader)
                                {
                                    // Ignore Special fields already consumed.
                                    if ((Import.ImportTypeId == 1 && kv.Key == "TagNumber") ||
                                        (Import.ImportTypeId == 2 && kv.Key == "TLHNumber") ||
                                        (Import.ImportTypeId == 3 && kv.Key == "MaintItemNum") ||
                                        //(Import.ImportTypeId == 4 && kv.Key == "MaintPlanName") ||
                                        (Import.ImportTypeId == 5 && kv.Key == "MaintPlanName") ||
                                        (Import.ImportTypeId == 6 && kv.Key == "TaskListOperationName"))
                                    {
                                        continue;
                                    }

                                    // TODO this is overhead. can we do it without cloning?
                                    ImportExtract newRec = rec.Clone();
                                    newRec.AttributeName = kv.Key;
                                    var tmpVal = row.Cell(kv.Value).Value.ToString();
                                    
                                    // convert null bit field (if in header) values to false.
                                    if (bitFields.Contains(kv.Key))
                                    {
                                        if (string.IsNullOrEmpty(tmpVal))
                                            tmpVal = "false";
                                    }
                                    newRec.AttributeValue = tmpVal;

                                    if (string.IsNullOrEmpty(newRec.AttributeValue))
                                        continue;

                                    // if we encounter a STAR2, use additionalFKs to manage it.
                                    if (additionalFKs.ContainsKey(kv.Key))
                                    {
                                        // populate EntityPseudoFKName, EntityPseudoFKValue, if required.
                                        int k = 0;
                                        foreach (string attrib in additionalFKs[kv.Key])
                                        {
                                            k++;
                                            if (k == 1)
                                            {
                                                // should catch errors here; if it requires dHeader[attrid] dies not exist, will crash
                                                newRec.EntityPseudoFKName = attrib;
                                                newRec.EntityPseudoFKValue = row.Cell(dHeader[attrib]).Value.ToString();
                                            }
                                            if (k == 2)
                                            {
                                                newRec.EntityPseudoFK2Name = attrib;
                                                newRec.EntityPseudoFK2Value = row.Cell(dHeader[attrib]).Value.ToString();
                                            }
                                            //... add more if required, but I dont see a composite fk of three values existing...
                                        }
                                    }

                                    // check this. getting sql errors for 'MaintPare' which suggests truncation of name
                                    _context.ImportExtract.Add(newRec);

                                } //header loop

                            } // TODO: can be 20k+ rows to process. Add buffer to speed up import time (i.e. instead of 20k+ writes, buffer, and write 2k times)
                            await _context.SaveChangesAsync();
                        } // iterate rows

                        sheet.Columns("A:K").AdjustToContents();
                    } // iterate sheets

                } // using excelWorkbook

                // remove temp file.
                var file = new FileInfo(filePath);
                file.Delete();

            } // is excel file

            // Update Import Status if we made it this far. 
            Import.ImportStatus = "Imported excel File.";
            _context.Update(Import);
            await _context.SaveChangesAsync();

            // ARGH. If we wait for it, then it works, but on large imports this is problematic 
            // due to timeout by App server. This kills the running procedure. 
            // 
            // Problem: Cannot guarantee ImportTransform Data is complete
            //          Works when SP run manually from DB. 
            // Solution: Need Agent on SQL Server to start procedures, so timeout is not an issue. 
            //           maybe trigger on Import Table, or Scheduled Task (every 15min; run Import Job)
            //
            //var discard = _context.Database.ExecuteSqlRaw("TransformImports");



            // Return to Import screen....
            return RedirectToAction(nameof(Index));
        } // POST index


        // GET: Imports/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            ApproveReport approveReport = new ApproveReport();
            approveReport.ImportReport = await _context.ImportReport.Where(x => x.importId == id && x.LoadType != "SAME").OrderBy(x => x.LoadType).ThenBy(x => x.EntityPseudoPK).ThenBy(x => x.AttributeName).ToListAsync();
            approveReport.Import = await _context.Import.Where(x => x.ImportId == id).FirstAsync();
            return View(approveReport);
        }

        // GET: Imports/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var import = await _context.Import.FindAsync(id);
            if (import == null)
            {
                return NotFound();
            }
            ViewData["ImportTypeId"] = new SelectList(_context.ImportType, "ImportTypeId", "ImportTypeName", import.ImportTypeId);
            return View(import);
        }

        // POST: Imports/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ImportId,ImportStatus,ImportTypeId,Created,CreatedBy,CreatedComment,Approved,ApprovedBy,ApprovedComment")] Models.ETL.Import import)
        {
            if (id != import.ImportId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(import);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ImportExists(import.ImportId))
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
            ViewData["ImportTypeId"] = new SelectList(_context.ImportType, "ImportTypeId", "ImportTypeName", import.ImportTypeId);
            return View(import);
        }

        // GET: Imports/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var import = await _context.Import
                .Include(i => i.ImportType)
                .FirstOrDefaultAsync(m => m.ImportId == id);
            if (import == null)
            {
                return NotFound();
            }

            return View(import);
        }

        // POST: Imports/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var import = await _context.Import.FindAsync(id);
            _context.Import.Remove(import);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Remove(int id)
        {
            // remove transform, extract, import where importId = id
            // Too slow to remove each record individually so calling db statements.
            _context.Database.ExecuteSqlRaw("DELETE FROM ImportError WHERE ImportId={0}", id);
            _context.Database.ExecuteSqlRaw("DELETE FROM ImportTransform WHERE ImportId={0}", id);
            _context.Database.ExecuteSqlRaw("DELETE FROM ImportExtract WHERE ImportId={0}", id);

            var import = await _context.Import.FindAsync(id);
            _context.Import.Remove(import);

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Approve(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var import = await _context.Import.FindAsync(id);
            if (import == null)
            {
                return NotFound();
            }
            ViewData["ImportTypeId"] = new SelectList(_context.ImportType, "ImportTypeId", "ImportTypeName", import.ImportTypeId);
            return View(import);
        }

        // POST: Imports/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Approve(int id, [Bind("ImportId,ImportStatus,ImportTypeId,Created,CreatedBy,CreatedComment,ApprovedComment")] Models.ETL.Import import)
        {
            if (id != import.ImportId)
            {
                return NotFound();
            }

            if (ModelState.IsValid && import.ApprovedComment != null && import.ApprovedComment.Length > 3)
            {
                try
                {

                    import.Approved = DateTime.UtcNow;
                    import.ApprovedBy = User.Identity.Name;
                    import.ImportStatus = "Approved";

                    _context.Update(import);
                    await _context.SaveChangesAsync();

                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ImportExists(import.ImportId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }

                // execute MSSQL command to start processing. 
                _ = _context.Database.ExecuteSqlRaw("ImportTransformById " + id);
                return RedirectToAction(nameof(Index));

            }
            ViewData["ImportTypeId"] = new SelectList(_context.ImportType, "ImportTypeId", "ImportTypeName", import.ImportTypeId);
            import.ResponseMessage = "Missing fields. Check Approved Comment.";
            return View(import);
        }


        public IActionResult ExcelReport(int id)
        {
            // get data from ImportReport, for this importId
            var tagData = _context.ImportReport.Where(x => x.importId == id).ToList();
            tagData.OrderBy(x => x.EntityPseudoPK).ThenBy(x => x.AttributeName);

            // Load to Dictionary, then generate excel.
            //  DicTag[tagid] = DicObject
            //  DicObject[AttributeName] = reportDictionary
            //  ReportDictionary = { LoadType, Value, OldValue, ErrorDescription}
            //
            //  LoadType = SAME | NULL | CHANGED
            //  Value = txt
            //  OldValue = txt | null
            //  ErrorDescription = txt | NULL
            Dictionary<string, Dictionary<string, Dictionary<string, string>>> DicTags =
                new Dictionary<string, Dictionary<string, Dictionary<string, string>>>();

            Dictionary<string, int> Fields =
                new Dictionary<string, int>();

            // Build Header and Data Dictionary
            string oldVal;
            foreach (var row in tagData)
            {
                Fields[row.AttributeName] = 0;
                // initialise if needed because C# is crappy.
                if (!DicTags.ContainsKey(row.EntityPseudoPK))
                    DicTags.Add(row.EntityPseudoPK, new Dictionary<string, Dictionary<string, string>>());

                if (!DicTags[row.EntityPseudoPK].ContainsKey(row.AttributeName))
                    DicTags[row.EntityPseudoPK].Add(row.AttributeName, new Dictionary<string, string>());

                DicTags[row.EntityPseudoPK][row.AttributeName]["LoadType"] = row.LoadType;
                DicTags[row.EntityPseudoPK][row.AttributeName]["AttributeValue"] = row.AttributeValue;
                if (string.IsNullOrEmpty(row.AttributeValueOld))
                    oldVal = "NULL";
                else
                    oldVal = row.AttributeValueOld;
                DicTags[row.EntityPseudoPK][row.AttributeName]["AttributeValueOld"] = oldVal;
                DicTags[row.EntityPseudoPK][row.AttributeName]["ErrorDescription"] = row.ErrorDescription;

            }

            using (var workbook = new XLWorkbook())
            {
                IXLWorksheet worksheet = workbook.Worksheets.Add("ImportReport");
                int currentRow = 1;

                // fieldNames to ignore. Strip from dictionary.
                ICollection<string> ignoreFields = new Collection<string>();
                ignoreFields.Add("Placeholder prolly dont need this functionality");
                foreach (var badField in ignoreFields)
                    Fields.Remove(badField);

                // build header from metadata, and establish row# per attribute.
                // Starting to dislike C# immutable dictionaries in foreach...
                int i = 2;
                worksheet.Cell(1, 1).SetValue("TagNumber");
                worksheet.Cell(1, 1).RichText.Bold = true;
                foreach (var property in Fields.Keys.ToList())
                {
                    worksheet.Cell(currentRow, i).SetValue(property);
                    worksheet.Cell(currentRow, i).RichText.Bold = true;
                    Fields[property] = i++;
                }

                // Write Sheet
                foreach (var tag in DicTags.Keys)
                {
                    currentRow++;
                    worksheet.Cell(currentRow, 1).SetValue(tag);

                    foreach (var attribute in DicTags[tag].Keys)
                    {
                        int col_num = Fields[attribute];

                        worksheet.Cell(currentRow, col_num).SetValue(DicTags[tag][attribute]["AttributeValue"]);

                        if (string.IsNullOrEmpty(DicTags[tag][attribute]["LoadType"]))
                        {
                            worksheet.Cell(currentRow, col_num).Style.Fill.BackgroundColor = XLColor.Red;
                            worksheet.Cell(currentRow, col_num).Comment.AddText(DicTags[tag][attribute]["ErrorDescription"] ?? "");
                        }
                        else if (DicTags[tag][attribute]["LoadType"] == "CHANGED")
                        {
                            worksheet.Cell(currentRow, col_num).Style.Fill.BackgroundColor = XLColor.LightYellow;
                            worksheet.Cell(currentRow, col_num).Comment.AddText(DicTags[tag][attribute]["AttributeValueOld"]);
                        }
                        else if (DicTags[tag][attribute]["LoadType"] == "RENAME")
                        {
                            worksheet.Cell(currentRow, col_num).Style.Fill.BackgroundColor = XLColor.LightGreen;
                            worksheet.Cell(currentRow, col_num).Comment.AddText(DicTags[tag][attribute]["AttributeValueOld"]);
                        }
                        else if (DicTags[tag][attribute]["LoadType"] == "NEW")
                        {
                            worksheet.Cell(currentRow, col_num).Style.Fill.BackgroundColor = XLColor.LightBlue;
                            worksheet.Cell(currentRow, col_num).Comment.AddText(DicTags[tag][attribute]["AttributeValueOld"]);
                        }
                    }
                }

                worksheet.Columns("A:Z").AdjustToContents();

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

        private bool ImportExists(int id)
        {
            return _context.Import.Any(e => e.ImportId == id);
        }
    }
}