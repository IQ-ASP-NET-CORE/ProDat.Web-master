using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using ClosedXML.Excel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProDat.Web2.Data;
using ProDat.Web2.Models;
using ProDat.Web2.ViewModels;

namespace ProDat.Web2.Controllers
{

    public class ReportsController : Controller
    {
        private readonly TagContext _context;

        public ReportsController(TagContext context)
        {
            _context = context;
        }

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

            var ReportNames = await _context.SAPExportDetail
                              .OrderBy(x=> x.FileName)
                              .Select(x => x.FileName).ToListAsync();

            ViewBag.Reports = ReportNames.Distinct();
            ViewBag.GlobalProjectDescription = _context.Project.First().ProjectName;
            return View();
        }

        /*
         * hardcoded solution to get base path for each report.
         * todo: replace this with dynamic function for:
         *      basepath(fieldList)
         *      select(fieldList)
         */
        private dynamic GetRec(string outputName, string sheetname, bool ExportAll)
        {
            if (outputName == "SenexFlocs" && sheetname == "Master SAP FLOC Template")
            {
                dynamic recs = _context.Tag
                    .Include(x => x.MaintStructureIndicator)
                    .Include(x => x.MaintType)
                    .Include(x => x.PlannerPlant)
                    .Include(x => x.MaintLocation)
                        .ThenInclude(x=> x.MaintArea)
                    .Include(x => x.MaintWorkCentre)
                    .Include(x => x.MaintCriticality)
                    .Include(x => x.SortField)
                    .Include(x => x.CompanyCode)
                    .Include(x => x.MaintPlannerGroup)
                    .Include(x => x.MaintObjectType)
                    .Include(x => x.Manufacturer)
                    .Include(x=> x.MaintenancePlant)
                    
                    //.Include(x=> x.Model)  // we are using ModelDescription now?
                    //.Where(x => x.SAPStatusId == 7)

                    .Select(x => new
                    {
                        x.TagFloc,
                        x.MaintStructureIndicator.MaintStructureIndicatorName,
                        x.MaintType.MaintTypeName,
                        x.TagFlocDesc,
                        x.PlannerPlant.PlannerPlantName,
                        x.MaintLocation.MaintLocationName,
                        x.MaintWorkCentre.MaintWorkCentreName,
                        x.MaintCriticality.MaintCriticalityName,
                        x.SortField.SortFieldName,
                        x.CompanyCode.CompanyCodeName,
                        x.WBSElement.WBSElementName,
                        x.MaintPlannerGroup.MaintPlannerGroupName,
                        x.MaintObjectType.MaintObjectTypeName,
                        x.Manufacturer.ManufacturerName,
                        //x.Model.ModelName  // reverted to use of Model Description in UC2.
                        x.ModelDescription,
                        x.SerialNumber,
                        x.MaintenancePlant.MaintenancePlantNum,
                        x.MaintLocation.MaintArea.PlantSection.PlantSectionName
                    })
                    .OrderBy(x => x.TagFloc)
                    .ToList();

                return recs;
            }
            else if (outputName == "SenexFlocs" && sheetname == "FL_CHAR")
            {
                dynamic recs = from Tag in _context.Tag
                               from ted in Tag.TagEngDatas
                               from mcXedc in ted.EngDataCode.MaintClassXEngDataCode
                               select new
                               {
                                   Tag.TagFloc,
                                   ted.EngDatavalue,
                                   ted.EngDataCode.EngDataCodeSAPDesc,
                                   mcXedc.MaintClass.MaintClassName
                               };
            
                return recs;
            }
            else if (outputName == "SenexFlocs" && sheetname == "FL_CL")
            {
                dynamic recs = from tag in _context.Tag
                               from motXmc in tag.MaintObjectType.MaintObjectTypeXMaintClass
                               select new
                               {
                                   tag.TagFloc,
                                   motXmc.MaintClass.MaintClassName
                               };

                return recs;
            }
            else if (outputName == "SenexClassChar" && sheetname == "Class_Char_Value Assignment")
            {
                dynamic recs = from tag in _context.Tag
                               from ted in tag.TagEngDatas
                               from mcXedc in ted.EngDataCode.MaintClassXEngDataCode
                               
                               select new
                               {
                                   tag.TagFloc,
                                   ted.EngDatavalue,
                                   ted.EngDataCode.EngDataCodeSAPDesc,
                                   mcXedc.MaintClass.MaintClassName,
                               };

                return recs;
            }
            else if (outputName == "SenexPlanItem" && sheetname == "Item")
            {
                dynamic recs = from MaintItem in _context.MaintItem
                               select new
                               {
                                   MaintItem.MaintItemId,
                                   MaintItem.MaintPlan.MaintPlanId,
                                   MaintItem.MaintPlan.MaintStrategy.MaintStrategyName,
                                   MaintItem.MaintItemShortText,
                                   MaintItem.FMaintItemHeaderFloc,
                                   MaintItem.MaintenancePlant.MaintenancePlantNum,
                                   MaintItem.MaintPlannerGroup.MaintPlannerGroupName,
                                   MaintItem.MaintItemOrderType,
                                   MaintItem.MaintItemActivityTypeId,
                                   MaintItem.MaintWorkCentre.MaintWorkCentreName,
                                   MaintItem.SysCond.SysCondName,
                                   MaintItem.Priority.PriorityName
                               };

                return recs;
            }
            else if (outputName == "SenexPlanItem" && sheetname == "Item Obj List")
            {
                dynamic recs = from FlocXmaintItem in _context.FlocXmaintItem
                               select new
                               {
                                   FlocXmaintItem.MaintItemId,
                                   FlocXmaintItem.Floc.TagFloc
                               };

                return recs;
            }
            else if (outputName == "SenexPlanItem" && sheetname == "Plan")
            {
                dynamic recs = from MaintPlan in _context.MaintPlan
                               select new
                               {
                                   MaintPlan.MaintPlanId,
                                   MaintPlan.MaintStrategy.MaintStrategyName,
                                   MaintPlan.MeasPoint.MeasPointName,
                                   MaintPlan.ShortText,
                                   MaintPlan.MaintSortProcess.MaintSortProcessName,
                                   MaintPlan.SchedulingPeriodValue,
                                   MaintPlan.SchedulingPeriodUomId,
                                   MaintPlan.CallHorizon
                               };

                return recs;
            }
            else if (outputName == "SenexTaskList" && sheetname == "Hdr_Data")
            {
                dynamic recs = from TaskListHeader in _context.TaskListHeader
                               select new
                               {
                                   TaskListHeader.TaskListGroup.TaskListGroupName,
                                   TaskListHeader.Counter,
                                   TaskListHeader.TaskListShortText,
                                   TaskListHeader.MaintWorkCentre.MaintWorkCentreName,
                                   TaskListHeader.MaintenancePlant.MaintenancePlantNum,
                                   TaskListHeader.MaintPlannerGroup.MaintPlannerGroupName,
                                   TaskListHeader.MaintStrategy.MaintStrategyName,
                                   TaskListHeader.SysCond.SysCondName
                               };

                return recs;
            }
            else if (outputName == "SenexTaskList" && sheetname == "Ops_Data")
            {
                dynamic recs = from TaskListOperations in _context.TaskListOperations
                               select new
                               {
                                   TaskListOperations.TaskListHeader.TaskListGroup.TaskListGroupName,
                                   TaskListOperations.TaskListHeader.Counter,
                                   TaskListOperations.OperationNum,
                                   TaskListOperations.MaintWorkCentre.MaintWorkCentreName,
                                   TaskListOperations.ControlKey.ControlKeyName,
                                   TaskListOperations.MaintenancePlant.MaintenancePlantNum,
                                   TaskListOperations.OperationShortText,
                                   TaskListOperations.WorkHrs,
                                   TaskListOperations.CapNo,
                                   TaskListOperations.SysCond.SysCondName
                               };

                return recs;
            }
            else if (outputName == "SenexTaskList" && sheetname == "Ops_Pkg")
            {
                dynamic recs = from TaskListOperations in _context.TaskListOperations
                               select new
                               {
                                   TaskListOperations.TaskListHeader.TaskListGroup.TaskListGroupName, 
                                   TaskListOperations.TaskListHeader.Counter,
                                   TaskListOperations.OperationNum,
                                   TaskListOperations.MaintPackage.MaintPackageCycleLength,
                                   TaskListOperations.MaintPackage.MaintPackageCycleUnit
                               };

                return recs;
            }

            // how'd you get here? Missing a query for a report sheet.
            return Enumerable.Empty<dynamic>();
        }

        private string PathExpressionDecoder(object path, dynamic rec, string fieldPath, string PathPadding)
        {
            // convert expression into list of its parts.
            string[] parts = fieldPath.Split(" ");
            parts = parts.Where(c => c != "#EXPR#").ToArray();

            int[] pads = Array.ConvertAll(PathPadding.Split(","), int.Parse);

            // iterate parts. If linq expression, convert to its value
            for (var i = 0; i < parts.Length; i++)
            {
                if (!parts[i].StartsWith("#"))
                {
                    parts[i] = (string)GetPropertyValue(rec, parts[i])??"NULL";
                    if (pads[i] > 0)
                        parts[i] = parts[i].PadLeft(pads[i], '0');
                }
            }

            // apply any EXPRs. Bimbas...
            for (var i = 0; i < parts.Length; i++)
            {
                if (parts[i] == "#MULT#")
                {
                    if(double.TryParse(parts[i-1], out double a) && double.TryParse(parts[i+1], out double b) )
                    {
                        var newVal = a * b;
                        parts[i - 1] = null;
                        parts[i] = null;
                        parts[i + 1] = newVal.ToString("F2");
                    }
                    else
                    {
                        // cleanup
                        parts[i - 1] = null;
                        parts[i] = null;
                        parts[i + 1] = null;
                    }
                    // perform mult on prior and next.
                    
                }
                if (parts[i] == "#DIV#")
                {
                    if (double.TryParse(parts[i - 1], out double a) && double.TryParse(parts[i + 1], out double b) && b>0)
                    { 
                        var newVal = a / b;
                        parts[i - 1] = null;
                        parts[i] = null;
                        parts[i + 1] = newVal.ToString("F2");
                    }
                    else
                    {
                        // cleanup
                        parts[i - 1] = null;
                        parts[i] = null;
                        parts[i + 1] = null;
                    }
                }
            }
            parts = parts.Where(c => c != null).ToArray();


            // apply any EXPRs. Bimbas...
            for (var i = 0; i < parts.Length; i++)
            {
                if (parts[i] == "#ADD#")
                {
                    if (double.TryParse(parts[i - 1], out double a) && double.TryParse(parts[i + 1], out double b))
                    { 
                        var newVal = a + b;
                        parts[i - 1] = null;
                        parts[i] = null;
                        parts[i + 1] = newVal.ToString("F2");
                    }
                    else
                    {
                        // cleanup
                        parts[i - 1] = null;
                        parts[i] = null;
                        parts[i + 1] = null;
                    }
                }
                if (parts[i] == "#MIN#")
                {
                    if (double.TryParse(parts[i - 1], out double a) && double.TryParse(parts[i + 1], out double b))
                    {
                        var newVal = a - b;
                        parts[i - 1] = null;
                        parts[i] = null;
                        parts[i + 1] = newVal.ToString("F2");
                    }
                    else
                    {
                        // cleanup
                        parts[i - 1] = null;
                        parts[i] = null;
                        parts[i + 1] = null;
                    }
                }
            }
            parts = parts.Where(c => c != null).ToArray();

            // perform CATs.. using AMP rather than assuming concatenation, in case we want different joiners later (e.g. space vs dash)
            for (var i = 0; i < parts.Length; i++)
            {
                if (parts[i] == "#AMP#")
                {
                    var newVal = parts[i - 1] + "-" + parts[i + 1];
                    parts[i - 1] = null;
                    parts[i] = null;
                    parts[i + 1] = newVal;
                }
            }
            // compile parts into single string.
            parts = parts.Where(c => c != null).ToArray();
            string retVal = null;
            retVal = string.Join("", parts);
            return retVal;

        }

        private static string cleanPath(string path)
        {
            ////
            /// given a path, output in a more formal syntax to allow excel writer to construct value from 
            /// it. 
            /// e.g.  Tag.MaintItem.MaintItemName #AMP# Tag.Status.StatusName
            /// becomes
            ///  #EXPR# MaintItemName #AMP# StatusName
            ///  outputs with dash delimeter as:
            ///    G100-ACTIVE
            ///  e.g. 3
            ///  becomes
            ///    #LIT#3    (interprets as literal so dosent try to find literal in recordset)
            ///  
            ///  e.g. Tag.Amount #MULT# Tag.Quantity
            ///  becomes
            ///    #EXPR# Amount #MULT# Quantity
            ///  outputs as number:
            ///    34.3
            ///    
            ///  todo: 
            ///  
            ///  Add another layer to format LINQ expression. only for numbers, so can be used like so; 
            ///  e.g. PAD(Tag.System.SystemNum,2)
            ///  
            ///  Will be checked for later... no spaces or it will not be part of token!
            ///  
            ///  
            if (path == null)
                return null;
            
            string retVal = null;
            if (path.Contains("#AMP#") || path.Contains("#MULT#") || path.Contains("#DIV#") || path.Contains("#ADD#") || path.Contains("#SUB#") || path.Contains("PAD("))
            {
                // is an expression using linq values. Keep fieldname parts of linq only, but maintain operators, which will be used by excel generator to get fimal value
                retVal = "#EXPR# ";
                var tokens = path.Split(" ");
                foreach (var token in tokens)
                {
                    // TODO: Add function to remove 'PAD(' and ',*)' 
                    var temp_token = token;
                    if (temp_token.Contains("PAD("))
                    {
                        var start = temp_token.IndexOf("(") + 1;
                        var end = temp_token.LastIndexOf(",");
                        temp_token = temp_token.Substring(start, end - start);
                    }
                    if (temp_token.Contains("."))
                    {
                        temp_token = temp_token.Substring(temp_token.LastIndexOf(".") + 1);
                        retVal += temp_token + " ";
                    }
                    else
                    {
                        retVal += temp_token + " ";
                    }
                }
            }
            else if (path.Contains(".")) 
            {
                // is a singular linq expression. Whilst we're not building LINQ statement dynamically only need fieldname.
                retVal = path.Substring(path.LastIndexOf(".")+1);
            }
            else // is a literal.
            {
                retVal = "#LIT#" + path;
            }

            return retVal.Trim();
        }

        private static string pathPadding(string path)
        {
            // Given the path, search for pad functions, 
            //   e.g. #EXPR# PAD(linqExpr,2) PAD(LineExpr,4) LinqExpr
            //  returns: 2,4,-1

            if (path == null)
                return null;

            string retVal = null;
            var tokens = path.Split(" ");
            foreach (var token in tokens)
            {
                var padAmount = "-1";
                if (token.Contains("PAD("))
                {
                    // get padding amount else null
                    var pos = token.LastIndexOf(",") + 1;
                    var len = token.Length - pos-1;
                    padAmount = token.Substring(pos, len);
                }
                retVal += padAmount + ",";
            }

            return retVal.Remove(retVal.Length - 1);
        }

        // Added this to skip over handled exception
        [System.Diagnostics.DebuggerHidden]
        public static object GetPropertyValue(object obj, string name)
        {
            // Catch exceptions when value requested is null, and return empty String. Might need to be more robust...
            try {
                var RetVal = obj?.GetType().GetProperty(name).GetValue(obj, null);
                if (RetVal is int)
                    RetVal = RetVal.ToString();

                return RetVal;

            } catch {
                return "";
            };
        }
             

        public IEnumerable<string> GetSheets(string sheetname)
        {
            return _context.SAPExportDetail
                .Where(x => x.OutputName == sheetname)
                .Select(x => x.SheetName)
                .ToList().Distinct();
        }

        public string GetFileName(string ReportName)
        {
            return _context.SAPExportDetail
                .Where(x => x.OutputName == ReportName)
                .Select(x => x.FileName)
                .FirstOrDefault();
        }

        public string GetOutputName(string FileName)
        {
            return _context.SAPExportDetail
                .Where(x => x.FileName == FileName)
                .Select(x => x.OutputName)
                .FirstOrDefault();
        }

        public IEnumerable<dynamic> GetFields(string reportname, string sheetname)
        {
           return  _context.SAPExportDetail
                .Where(x => x.OutputName == reportname)
                .Where(x => x.SheetName == sheetname)
                .OrderBy(x => x.ColumnOrder)
                .Select(x => new
                {
                    x.ColumnOrder,
                    x.ColumnHeader_SAP,
                    x.ColumnHeader_Legible,
                    Path = cleanPath(x.PathName),
                    PathPadding = pathPadding(x.PathName)
                })
                .ToList();
        }


        /* 
         *  This will return report based on requested Report.
         *  Iterates ofver each sheet to be created,
         *  
         * 
         */
        public async Task<IActionResult> ExcelReport(string fileName, bool ExportAll)
        {
            using (var workbook = new XLWorkbook())
            {
                var ReportName = GetOutputName(fileName);

                var sheets = GetSheets(ReportName);

                foreach (var sheet in sheets)
                {
                    var recs = GetRec(ReportName, sheet, ExportAll);
                    var fields = GetFields(ReportName, sheet);
                    
                    // create sheet. initialise counters
                    IXLWorksheet worksheet = workbook.Worksheets.Add(sheet);
                    int currentRow = 1;
                    int colIndex = 1;

                    // add header rows
                    foreach (var field in fields)
                    {
                        worksheet.Cell(currentRow, colIndex).SetValue(field.ColumnHeader_Legible);
                        worksheet.Cell(currentRow + 1, colIndex++).SetValue(field.ColumnHeader_SAP);
                    }
                    currentRow++;

                    // populate excel sheet using field requirements.
                    // uses reflection to return the requested field fieldname
                    foreach (var rec in recs)
                    {
                        currentRow++;
                        colIndex = 0;
                        foreach (var field in fields)
                        {
                            colIndex++;

                            if (field.Path == null)
                                continue;

                            if (field.Path.StartsWith("#LIT#"))
                            {
                                var value = field.Path.Substring(5);
                                worksheet.Cell(currentRow, colIndex).SetValue(value);
                            }
                            else if (field.Path.StartsWith("#EXPR#"))
                            {
                                var value = PathExpressionDecoder(field.Path, rec, field.Path, field.PathPadding) ?? "";
                                worksheet.Cell(currentRow, colIndex).SetValue(value);
                            }
                            else
                            {
                                var value = GetPropertyValue(rec, field.Path)??"";
                                worksheet.Cell(currentRow, colIndex).SetValue(value);
                            }

                        } 
                    } 

                    worksheet.Columns("A:BB").AdjustToContents();

                } // for each sheet

                // return workbook
                using (var stream = new MemoryStream())
                {
                    workbook.SaveAs(stream);
                    var content = stream.ToArray();

                    return File(
                        content,
                        "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                        fileName + ".xlsx");
                }

            } // using workbook

        } // ExcelReport
            
    } // Class

} // Namespace
