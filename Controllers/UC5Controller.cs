using DevExtreme.AspNet.Mvc;
using DevExtreme.AspNet.Data;
//using System;
//using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ProDat.Web2.Data;
using System.Linq.Dynamic.Core;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using Newtonsoft.Json;
using ProDat.Web2.ViewModels;
using Microsoft.AspNetCore.Authorization;
using ProDat.Web2.Models;
using System.Data;
using ProDat.Web2.ViewComponents;




//internal class import
//{
//    import DataGrid;
//}

namespace ProDat.Web2.Controllers
{
    public class UC5Controller : Controller
    {
        #region instantiate controller
        private readonly TagContext _context;

        public UC5Controller(TagContext context)
        {
            _context = context;
        }
        #endregion

        public IActionResult Index()
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
                    // Modified by MWM
                    "/Identity/Account/Login");
                //"/Identity/Account/Manage/TwoFactorAuthentication");
            }

            ViewBag.GlobalProjectDescription = _context.Project.First().ProjectName;
            return View();
        }

        public IActionResult Simplified()
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
                    // Modified by MWM
                    "/Identity/Account/Login");
                //"/Identity/Account/Manage/TwoFactorAuthentication");
            }

            ViewBag.GlobalProjectDescription = _context.Project.First().ProjectName;
            return View();

        }


        [HttpGet]
        public object KeyList(DataSourceLoadOptions loadOptions)
        {
            var dataSet = _context.KeyList
                          .Where(x => x.KeyListId >= 0)
                          .Select(x => new { x.KeyListId, x.KeyListName });

            return DataSourceLoader.Load(dataSet, loadOptions);
        }

        public object KeyListEngClass(DataSourceLoadOptions loadOptions)
        {
            var datasetEng = _context.KeyListxEngClass
                            .Where(x => x.KeyListxEngClassId >= 0)
                            .Select(x => x.EngClass.EngClassDesc);
            //.Select(x => new { x.KeyListxEngClassId, x.KeyListId });

            return DataSourceLoader.Load(datasetEng, loadOptions);

        }

        public object KeyListxEngDataCode(DataSourceLoadOptions loadOptions, int KeyLookup)
        {
            var datasetEngClass = _context.KeyListxEngDataCode
                            //.Where(x => x.KeyListId >= 0);
                            .Where(x => x.KeyListId == KeyLookup);
            //.Select(x => new { x.EngDataCodeName, x.EngClassName, x.EngClassDescription});

            return DataSourceLoader.Load(datasetEngClass, loadOptions);

        }


        //public object KeyListDataGrid_GetData(DataSourceLoadOptions loadOptions, int? keylistId)
        //{
        //    var dataSet = _context.KeyList
        //                  //.Where(x=> x.MaintStrategyId == parent)
        //                  .OrderBy(x => x.KeyListId)
        //                  .Select(rec => new { rec.KeyListId, rec.KeyListName });

        //    return DataSourceLoader.Load(dataSet, loadOptions);


        //    var dataSet = _context.KeyListxEngDataCode
        //                  .Where(x => x.KeyListId == parent)
        //                  .OrderBy(x => x.KeyListId)
        //                  .Select(rec => new { rec.KeyListId, rec.EngDataCodes.EngDataClasss.EngDataClassId, rec.EngDataCodes.EngDataClasss.EngDataClassName, rec.KeyList.KeyListName });

        //    return DataSourceLoader.Load(dataSet, loadOptions);

        //    var dataSet = from i in _context.KeyListxEngDataCode
        //                  join e in _context.EngDataCode on i.EngDataCode equals e.EngDataCodeId
        //                  join t in _context.EngDataClassxEngDataCode on e.EngDataCodeId equals t.EngDataCodeId
        //                  join y in _context.EngClass on t.EngClassId equals y.EngClassId
        //                  where i.KeyListId == keylistId
        //                  select new
        //                  {
        //                      i.KeyListId,
        //                      i.ColumnNumber,
        //                      i.Alias,
        //                      e.EngDataCodeId,
        //                      e.EngDataCodeName,
        //                      e.EngDataCodeDesc,
        //                      e.HideFromUI,
        //                      t.BccCodeId,
        //                      y.EngClassName
        //                  };
        //    var retVal = dataSet.ToList(); return DataSourceLoader.Load(retVal, loadOptions);
        //}
        [HttpGet]
        public object KeylistxEngClass_GetData(DataSourceLoadOptions loadOptions, int? keylistId)


        {
            keylistId ??= 1;
            var dataSet = from i in _context.KeyListxEngDataCode
                          join e in _context.EngDataCode on i.EngDataCode equals e.EngDataCodeId
                          join t in _context.EngDataClass on e.EngDataClassId equals t.EngDataClassId
                          where i.KeyListId == keylistId
                          select new
                          {
                              KeyListId = i.KeyListId,
                              coloumnNumber = i.ColumnNumber,
                              alias = i.Alias ?? "No Alias",
                              engDataCodeId = e.EngDataCodeId,
                              engDataCodeName = e.EngDataCodeName ?? "Missing Code Name",
                              engDataCodeDesc = e.EngDataCodeDesc ?? "Missing Description",
                              engdataclass = t.EngDataClassName ?? "Missing Data Class Name",
                              hideFromUI = e.HideFromUI
                              
                          }; var retVal = dataSet.ToList(); return DataSourceLoader.Load(retVal, loadOptions);
        }

        [HttpGet]
        public IActionResult KeylistxEngClass_GetData_Reload(int Height, int Width, int? parent)
        {
            return ViewComponent("KeyListAttributes", new { height = Height, width = Width, parent });
        }


        [HttpGet]
        public object EngClassDataGrid_GetData(DataSourceLoadOptions loadOptions, int? parent)
        {
            parent ??= 1;

            var dataSet = _context.KeyListxEngClass
                          .Where(x => x.KeyListId == parent)
                          .OrderBy(x => x.EngClassID)
                          .OrderBy(x => x.EngClassID)
                          .Select(rec => new { rec.EngClassID, rec.EngClass.EngClassName, rec.EngClass.EngClassDesc, rec.EngClass.SuperClass.SuperclassName, rec.KeyListxEngClassId, rec.EngClass.EngClassRequiredDocs });

            return DataSourceLoader.Load(dataSet, loadOptions);
        }


        [HttpGet]
        public IActionResult KeyListAttributes_Reload(int Height, int Width, int? Parent)
        {
            return ViewComponent("KeyListAttributes", new { height = Height, width = Width, parent = Parent });
        }

        public object KeyListAttributes_GetData(DataSourceLoadOptions loadOptions, int? keylistId)
        {
            keylistId ??= 1;
            var dataSet = from i in _context.KeyListxEngDataCode
                          join e in _context.EngDataCode on i.EngDataCode equals e.EngDataCodeId
                          join t in _context.EngDataClass on e.EngDataClassId equals t.EngDataClassId
                          where i.KeyListId == keylistId
                          select new
                          {
                              KeyListId = i.KeyListId,
                              coloumnNumber = i.ColumnNumber,
                              alias = i.Alias ?? "No Alias",
                              engDataCodeId = e.EngDataCodeId,
                              engDataCodeName = e.EngDataCodeName ?? "Missing Code Name",
                              engDataCodeDesc = e.EngDataCodeDesc ?? "Missing Description",
                              engdataclass = t.EngDataClassName ?? "Missing Data Class Name",
                              hideFromUI = e.HideFromUI

                          }; var retVal = dataSet.ToList(); return DataSourceLoader.Load(retVal, loadOptions);
        }


        [HttpGet]
        public IActionResult KeyListInclusiveClass_Reload(int Height, int Width, int? Parent)
        {
            return ViewComponent("KeyListInclusiveClass", new { height = Height, width = Width, parent = Parent });
        }

        public object KeyListInclusiveClass_GetData(DataSourceLoadOptions loadOptions, int? parent)
        {
            parent ??= 1;

            var dataSet = _context.KeyListxEngClass
                          .Where(x => x.KeyListId == parent)
                          .OrderBy(x => x.EngClassID)
                          .Select(rec => new { rec.EngClassID, rec.EngClass.EngClassName, rec.EngClass.EngClassDesc, rec.EngClass.SuperClass.SuperclassName, rec.KeyListxEngClassId, rec.EngClass.EngClassRequiredDocs });

            return DataSourceLoader.Load(dataSet, loadOptions);
        }

        [HttpGet]
        public IActionResult EngClassDataGrid_Reload(int Height, int Width, int? Parent)
        {
            return ViewComponent("EngClassDataGrid", new { height = Height, width = Width, parent = Parent });
        }

        [HttpGet]
        public IActionResult UnassignedAttDataGrid_Reload(int Height, int Width, int? Parent)
        {
            return ViewComponent("UnassignedAttDataGrid", new { height = Height, width = Width, parent = Parent });
        }

        public object UnassignedAttDataGrid_GetData(DataSourceLoadOptions loadOptions, int? parent)
        {
            parent ??= 1;

            var dataSet = _context.KeyListxEngClass
                          .Where(x => x.KeyListId == parent)
                          .OrderBy(x => x.EngClassID)
                          .Select(rec => new { rec.EngClassID, rec.EngClass.EngClassName, rec.EngClass.EngClassDesc, rec.EngClass.SuperClass.SuperclassName, rec.KeyListxEngClassId, rec.EngClass.EngClassRequiredDocs });

            return DataSourceLoader.Load(dataSet, loadOptions);
        }
        [HttpGet]
        public IActionResult UnassignedDocTypesDataGrid_Reload(int Height, int Width, int? Parent)
        {
            return ViewComponent("UnassignedDocTypesDataGrid", new { height = Height, width = Width, parent = Parent });
        }

        public object UnassignedDocTypesDataGrid_GetData(DataSourceLoadOptions loadOptions, int? parent)
        {
            parent ??= 1;

            var dataSet = _context.KeyListxEngClass
                          .Where(x => x.KeyListId == parent)
                          .OrderBy(x => x.EngClassID)
                          .Select(rec => new { rec.EngClassID, rec.EngClass.EngClassName, rec.EngClass.EngClassDesc, rec.EngClass.SuperClass.SuperclassName, rec.KeyListxEngClassId, rec.EngClass.EngClassRequiredDocs });

            return DataSourceLoader.Load(dataSet, loadOptions);
        }
        [HttpGet]
        public IActionResult AssignedDocTypesDataGrid_Reload(int Height, int Width, int? Parent)
        {
            return ViewComponent("AssignedDocTypesDataGrid", new { height = Height, width = Width, parent = Parent });
        }

        public object AssignedDocTypesDataGrid_GetData(DataSourceLoadOptions loadOptions, int? parent)
        {
            parent ??= 1;

            var dataSet = _context.KeyListxEngClass
                          .Where(x => x.KeyListId == parent)
                          .OrderBy(x => x.EngClassID)
                          .Select(rec => new { rec.EngClassID, rec.EngClass.EngClassName, rec.EngClass.EngClassDesc, rec.EngClass.SuperClass.SuperclassName, rec.KeyListxEngClassId, rec.EngClass.EngClassRequiredDocs });

            return DataSourceLoader.Load(dataSet, loadOptions);
        }
        [HttpGet]
        public IActionResult UnAssignedClassDataGrid_Reload(int Height, int Width, int? Parent)
        {
            return ViewComponent("UnAssignedClassDataGrid", new { height = Height, width = Width, parent = Parent });
        }

        public object UnAssignedClassDataGrid_GetData(DataSourceLoadOptions loadOptions, int? parent)
        {
            parent ??= 1;

            var dataSet = _context.KeyListxEngClass
                          .Where(x => x.KeyListId == parent)
                          .OrderBy(x => x.EngClassID)
                          .Select(rec => new { rec.EngClassID, rec.EngClass.EngClassName, rec.EngClass.EngClassDesc, rec.EngClass.SuperClass.SuperclassName, rec.KeyListxEngClassId, rec.EngClass.EngClassRequiredDocs });

            return DataSourceLoader.Load(dataSet, loadOptions);
        }

        public object KeylistxEngDataCode_GetData(DataSourceLoadOptions loadOptions, int keylistId)
        {
            var dataSet = from i in _context.KeyListxEngDataCode
                          join e in _context.EngDataCode on i.EngDataCode equals e.EngDataCodeId
                          join t in _context.EngDataClassxEngDataCode on e.EngDataCodeId equals t.EngDataCodeId
                          join y in _context.EngClass on t.EngClassId equals y.EngClassId
                          where i.KeyListId == keylistId
                          select new
                          {
                              i.ColumnNumber,
                              i.Alias,
                              e.EngDataCodeId,
                              e.EngDataCodeName,
                              e.EngDataCodeDesc,
                              e.HideFromUI,
                              t.BccCodeId,
                              y.EngClassName
                          };
            var retVal = dataSet.ToList(); return DataSourceLoader.Load(retVal, loadOptions);
        }
        public IActionResult KeylistxEngDataCode_GetData_Reload(int Height, int Width, int? Parent)
        {
            return ViewComponent("KeyListAttributes", new { height = Height, width = Width, parent = Parent });
        }

        public object KeylistxEngClass_GetData(DataSourceLoadOptions loadOptions, int keylistId)
        {
            var dataSet = from i in _context.KeyListxEngClass
                          join e in _context.EngClass on i.EngClassID equals e.EngClassId
                          where i.KeyListId == keylistId
                          select new
                          {
                              e.EngClassName,
                              e.EngClassId,
                              e.EngClassDesc,
                              e.SuperClassID
                          }; var retVal = dataSet.ToList(); return DataSourceLoader.Load(retVal, loadOptions);
        }

        //public IActionResult KeylistxEngClass_GetData_Reload(int Height, int Width, int? Parent)
        //{
        //    return ViewComponent("KeyListAttributes", new { height = Height, width = Width, parent = Parent });
        //}

        public object KeylistxEngClassNoMatches_GetData(DataSourceLoadOptions loadOptions, int keylistId)
        {
            var dataSet = from i in _context.KeyListxEngClass
                          join e in _context.EngClass on i.EngClassID equals e.EngClassId
                          where i.KeyListId != keylistId
                          select new
                          {
                              e.EngClassName,
                              e.EngClassId,
                              e.EngClassDesc,
                              e.SuperClassID
                          }; var retVal = dataSet.ToList(); return DataSourceLoader.Load(retVal, loadOptions);
        }

        public IActionResult KeylistxEngClassNoMatches_GetData_Reload(int Height, int Width, int? Parent)
        {
            return ViewComponent("UnAssignedClassDataGrid", new { height = Height, width = Width, parent = Parent });
        }


        public object KeylistxDocType_GetData(DataSourceLoadOptions loadOptions, int keylistId)
        {
            var dataSet = from i in _context.KeyListxEngClass
                          join e in _context.DocType on i.EngClassID equals e.DocTypeId
                          where i.KeyListId == keylistId
                          select new
                          {
                              e.DocTypeName,
                              e.DocTypeDesc
                          }; var retVal = dataSet.ToList(); return DataSourceLoader.Load(retVal, loadOptions);
        }
        //public object KeylistxDocTypeNoMatch_GetData(DataSourceLoadOptions loadOptions, int keylistId)
        //{
        //    var dataSet = from i in _context.KeyListxDocType
        //                  join e in _context.DocType on i.DocTypeId equals e.DocTypeId
        //                  where i.KeyListId != keylistId
        //                  select new
        //                  {
        //                      e.DocTypeName,
        //                      e.DocTypeDesc
        //                  };
        //}

        //controller
    }
}
    public class DropDownBoxController : Controller
    {
        public ActionResult SingleSelection()
        {
            return View();
        }
    }


