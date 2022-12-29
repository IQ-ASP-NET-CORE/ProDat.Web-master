using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography.Xml;
using System.Threading.Tasks;
using ClosedXML.Excel;
using DocumentFormat.OpenXml.Drawing.Charts;
using DocumentFormat.OpenXml.Spreadsheet;
using Microsoft.AspNetCore.Mvc;
using ProDat.Web2.Models.ETL;
using ProDat.Web2.Models;
using ProDat.Web2.Data;
using SQLitePCL;

namespace ProDat.Web2.Controllers.Import
{
    public class UploadController : Controller
    {
        private readonly TagContext _context;

        public UploadController(TagContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            ViewBag.Image = null;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(FileUpload Data)
        {
            var newFileName = Path.GetRandomFileName() + ".xlsx";
            var uploads = "C:\\temp\\files";
            string filePath = "";

            var originalFileName = Data.File.FileName;

            if (Data.File.FileName != null && Data.File.FileName.EndsWith("xlsx"))
            {
                
                filePath = Path.Combine(uploads, newFileName);

                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await Data.File.CopyToAsync(fileStream);
                }
            }


            if (Data.File != null && Data.File.Length > 0 && Data.File.FileName.EndsWith(".xlsx"))
            {
                // This needs to be dynamic. 
                // TODO: Replace with Call to stored procedure which grabs table columns (and fixes fk value for suffix Num of Name)
                string[] ValidColumns = { "TagNumber", "TagService", "TagFLOC", "SubSystemNum", "EngClassName", "MaintLocationName", 
                                          "LocationName", "MaintTypeName", "MaintStatusName", "EngStatusName", "MaintWorkCentreName", 
                                          "EdcName", "MaintStructureIndicatorName", "CommissioningSubsystemName", "CommClassName", 
                                          "CommZoneName", "MaintPlannerGroupName", "MaintenanceplanName", "MaintCriticalityName", 
                                          "PerformanceStandardName", "MaintClassName", "KeyDocNum", "PoName", "TagSource", "TagDeleted", 
                                          "TagFlocDesc", "TagMaintQuery", "TagComment", "ModelNum", "VibName", "Tagnoneng", "TagVendorTag", 
                                          "MaintObjectTypeName", "RbiSilName", "IpfName", "RcmName", "TagRawNumber", "TagRawDesc", 
                                          "MaintScePsReviewTeamName", "MaintScePsJustification", "TagMaintCritComments", "RbmName", 
                                          "ManufacturerName", "ExMethodName", "TagRbmMethod", "TagVib", "TagSrcKeyList", "TagBomReq", 
                                          "TagSpNo", "MaintSortProcessName", "TagCharacteristic", "TagCharValue", "TagCharDesc", "EngParentID1", "EngDiscName" };
                
                // This needs to be dynamic. 
                // TODO: Replace with Call to stored procedure, or build using Tag metadata w/Reflection.
                //         if heavy load, we could build using reflection into table structure. (i.e. recreate on db change only). 
                // For now, there will only be a few of these in Tag Module.
                Dictionary<string, string[]> additionalFKs = new Dictionary<string, string[]>();
                additionalFKs.Add("SubSystemName", new[] { "SystemName" });
                additionalFKs.Add("LocationName", new[] { "AreaName" });

                // Open Excel. Iterate rows and import content into Tag.ImportExtract entity.
                using (XLWorkbook workbook = new XLWorkbook(filePath))
                {
                    // iterate sheets
                    foreach (IXLWorksheet sheet in workbook.Worksheets)
                    {
                        bool FirstRow = true;
                        string readRange = "1:1"; // determine row count

                        var x = new Tag();

                        //x.TagFlocDesc.Name


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
                                rec.EntityName = "Tag";
                                rec.ImportId = 1;

                                //PK for entityName == Tag:
                                // TODO: case select based on EntityName
                                rec.EntityPseudoPK = row.Cell(dHeader["TagNumber"]).Value.ToString();
                               
                                foreach (KeyValuePair<string, int> kv in dHeader)
                                {
                                    // only special field for now...
                                    // if more create special field list (maybe by EntityName)
                                    if (kv.Key == "TagNumber")
                                        continue;

                                    ImportExtract newRec = rec.Clone();
                                    newRec.AttributeName = kv.Key;
                                    newRec.AttributeValue = row.Cell(kv.Value).Value.ToString();
                                    
                                    if (string.IsNullOrEmpty(newRec.AttributeValue))
                                        continue;

                                    if (additionalFKs.ContainsKey(kv.Key))
                                    {
                                        // populate EntityPseudoFKName, EntityPseudoFKValue, if required.
                                        int k = 0;
                                        foreach( string attrib in additionalFKs[kv.Key])
                                        {
                                            k++;
                                            if (k == 1)
                                            {
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

                                    _context.ImportExtract.Add(newRec);
                                    await _context.SaveChangesAsync();
                                }

                            } // row loop

                            await _context.SaveChangesAsync();
                        }
                    }
                }
            }
            return View();
        } // post index
    } // Controller
} // namespace
