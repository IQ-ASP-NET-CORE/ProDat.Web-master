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

        //Gets all Engdatcodes related to the selected keylistID
        public object KeylistxEngDataCode_GetData(DataSourceLoadOptions loadOptions, int keylistId)
        {

            var dataSet = from i in _context.KeyListxEngDataCode
                          join e in _context.EngDataCode on i.EngDataCode equals e.EngDataCodeId
                          join t in _context.EngDataClass on e.EngDataClassId equals t.EngDataClassId
                          where i.KeyListId == keylistId

                          select new
                          {
                            coloumnNumber = i.ColumnNumber,
                            alias =  i.Alias ?? "No Alias",
                            engDataCodeId =  e.EngDataCodeId,
                            engDataCodeName = e.EngDataCodeName ?? "Missing Code Name",
                            engDataCodeDesc = e.EngDataCodeDesc ?? "Missing Description",
                            engdataclass = t.EngDataClassName ?? "Missing Data Class Name",
                            hideFromUI = e.HideFromUI,
                            keylistid = i.KeyListId
                          };

            var retVal = dataSet.ToList();

            return DataSourceLoader.Load(retVal, loadOptions);
        }

        //Gets all the Engdatacodes that are not related to the keylistID
        public object KeylistxEngDataCodeNoMatch_GetData(DataSourceLoadOptions loadOptions, int keylistId)
        {

            var dataSet = from i in _context.KeyListxEngDataCode
                          join e in _context.EngDataCode on i.EngDataCode equals e.EngDataCodeId
                          join t in _context.EngDataClass on e.EngDataClassId equals t.EngDataClassId
                          where i.KeyListId != keylistId

                          select new
                          {
                              coloumnNumber = i.ColumnNumber,
                              alias = i.Alias ?? "No Alias",
                              engDataCodeId = e.EngDataCodeId,
                              engDataCodeName = e.EngDataCodeName ?? "Missing Code Name",
                              engDataCodeDesc = e.EngDataCodeDesc ?? "Missing Description",
                              engdataclass = t.EngDataClassName ?? "Missing Data Class Name",
                              hideFromUI = e.HideFromUI,
                              keylistid = i.KeyListId
                          };

            var retVal = dataSet.ToList();

            return DataSourceLoader.Load(retVal, loadOptions);
        }

        //Gets all the engclasses that are related the to the keylistID
        public object KeylistxEngClass_GetData(DataSourceLoadOptions loadOptions, int keylistId)
        {

            var dataSet = from i in _context.KeyListxEngClass
                          join e in _context.EngClass on i.EngClassID equals e.EngClassId
                          where i.KeyListId == keylistId

                          select new
                          {
                             engClassName = e.EngClassName ?? "Missing Class Name",
                             engClassId = e.EngClassId,
                             engClassDesc = e.EngClassDesc ?? "Missing Description",
                             superClassId =  e.SuperClassID
                          };

            var retVal = dataSet.ToList();

            return DataSourceLoader.Load(retVal, loadOptions);
        }

        //Gets all the engclasses that are not related to the keylistID
        public object KeylistxEngClassNoMatches_GetData(DataSourceLoadOptions loadOptions, int keylistId)
        {

            var dataSet = from i in _context.KeyListxEngClass
                          join e in _context.EngClass on i.EngClassID equals e.EngClassId
                          where i.KeyListId != keylistId

                          select new
                          {
                              engClassName = e.EngClassName ?? "Missing Class Name",
                              engClassId = e.EngClassId,
                              engClassDesc = e.EngClassDesc ?? "Missing Description",
                              superClassId = e.SuperClassID
                          };

            var retVal = dataSet.ToList();

            return DataSourceLoader.Load(retVal, loadOptions);
        }

        //Gets all the documentTypes that are related to the keylistID
        public object KeylistxDocType_GetData(DataSourceLoadOptions loadOptions, int keylistId)
        {

            var dataSet = from i in _context.KeyListxDocType
                          join e in _context.DocType on i.DocTypeId equals e.DocTypeId
                          where i.KeyListId == keylistId

                          select new
                          {
                            docName = e.DocTypeName ?? "Missing Document Name",
                            docType = e.DocTypeDesc ?? "Missing Description"
                          };

            var retVal = dataSet.ToList();

            return DataSourceLoader.Load(retVal, loadOptions);
        }

        //Gets all the documentTypes that are not related to the keylistID
        public object KeylistxDocTypeNoMatch_GetData(DataSourceLoadOptions loadOptions, int keylistId)
        {

            var dataSet = from i in _context.KeyListxDocType
                          join e in _context.DocType on i.DocTypeId equals e.DocTypeId
                          where i.KeyListId != keylistId

                          select new
                          {
                              docName = e.DocTypeName ?? "Missing Document Name",
                              docType = e.DocTypeDesc ?? "Missing Description"
                          };

            var retVal = dataSet.ToList();

            return DataSourceLoader.Load(retVal, loadOptions);
        }


        //Inserts a new keylist entry connecting the engdatacode to the keylist
        public JsonResult KeylistxEngDataCode_Insert(int keylistId, int engDataCodeId, string alias)
        {
            var newkeylistXengdatacode = new KeyListxEngDataCode();

            newkeylistXengdatacode.KeyListId = keylistId;
            newkeylistXengdatacode.EngDataCode = engDataCodeId;
            newkeylistXengdatacode.Alias = alias;

            if (TryValidateModel(newkeylistXengdatacode))
            {
                _context.KeyListxEngDataCode.Add(newkeylistXengdatacode);
                _context.SaveChanges();
            }
            return Json("OK");
        }

        //finds the keylist with engdatacode and deletes the values from the table
        public JsonResult KeylistxEngDataCode_Delete(int keylistId, int engDataCodeId)
        {
            var keylistxengdatacodeDel = _context.KeyListxEngDataCode
                                        .Where(x => x.KeyListId == keylistId)
                                        .Where(x => x.EngDataCode == engDataCodeId)
                                        .First();


            if (keylistxengdatacodeDel != null)
            {
                _context.KeyListxEngDataCode.Remove(keylistxengdatacodeDel);
                _context.SaveChanges();
            }
            return Json("OK");
        }

        //Updates the alias value using the keylistID and original alias values as a keys for the searching.
        public JsonResult KeylistxEngDataCode_Update(int keylistId, string alias, string newValue)
        {
            var keylistxengdatacodeUpd = _context.KeyListxEngDataCode
                                        .Where(x => x.KeyListId == keylistId)
                                        .Where(x => x.Alias == alias)
                                        .First();


            if (keylistxengdatacodeUpd != null)
            {
                keylistxengdatacodeUpd.Alias = newValue;
                _context.SaveChanges();
            }

            return Json("OK");
        }

        //Inserts new keylist entry for engclass for the keylist
        public JsonResult KeylistxEngClass_Insert(int keylistId, int engclassId)
        {
            var newkeylistXengclass = new KeyListxEngClass();

            newkeylistXengclass.KeyListId = keylistId;
            newkeylistXengclass.EngClassID = engclassId;

            if (TryValidateModel(newkeylistXengclass))
            {
                _context.KeyListxEngClass.Add(newkeylistXengclass);
                _context.SaveChanges();
            }
            return Json("OK");
        }

        //Finds the engclass and keylist values and deletes them from the table.
        public JsonResult KeylistxEngClass_Delete(int keylistId, int engclassId)
        {
            var keylistxengclassDel = _context.KeyListxEngClass
                                        .Where(x => x.KeyListId == keylistId)
                                        .Where(x => x.EngClassID == engclassId)
                                        .First();


            if (keylistxengclassDel != null)
            {
                _context.KeyListxEngClass.Remove(keylistxengclassDel);
                _context.SaveChanges();
            }
            return Json("OK");
        }

        public IActionResult ColumnsBasedADataSource() {
        return View();
        }

    } // controller
}
