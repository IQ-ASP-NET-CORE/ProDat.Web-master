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

            var retVal = dataSet.ToList();

            return DataSourceLoader.Load(retVal, loadOptions);
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
                          };

            var retVal = dataSet.ToList();

            return DataSourceLoader.Load(retVal, loadOptions);
        }

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
                          };

            var retVal = dataSet.ToList();

            return DataSourceLoader.Load(retVal, loadOptions);
        }

        public object KeylistxDocType_GetData(DataSourceLoadOptions loadOptions, int keylistId)
        {

            var dataSet = from i in _context.KeyListxDocType
                          join e in _context.DocType on i.DocTypeId equals e.DocTypeId
                          where i.KeyListId == keylistId

                          select new
                          {
                              e.DocTypeName,
                              e.DocTypeDesc
                          };

            var retVal = dataSet.ToList();

            return DataSourceLoader.Load(retVal, loadOptions);
        }
        public object KeylistxDocTypeNoMatch_GetData(DataSourceLoadOptions loadOptions, int keylistId)
        {

            var dataSet = from i in _context.KeyListxDocType
                          join e in _context.DocType on i.DocTypeId equals e.DocTypeId
                          where i.KeyListId != keylistId

                          select new
                          {
                              e.DocTypeName,
                              e.DocTypeDesc
                          };

            var retVal = dataSet.ToList();

            return DataSourceLoader.Load(retVal, loadOptions);
        }

        public IActionResult ColumnsBasedADataSource() {
        return View();
        }

    } // controller
}
