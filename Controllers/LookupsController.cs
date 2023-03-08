using DevExtreme.AspNet.Mvc;
using DevExtreme.AspNet.Data;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ProDat.Web2.Models;
using ProDat.Web2.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq.Dynamic.Core;

namespace ProDat.Web2.Controllers
{
    // used by UC1, 2 & 3 to retrieve lookup data.
    public class LookupsController : Controller
    {
        private readonly TagContext _context;

        public LookupsController(TagContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        #region DDLs used by devextreme components for UC1, UC2 & UC3

        // additional actions

        [HttpGet]
        public object SAPExportDetail_Lookup(DataSourceLoadOptions loadOptions)
        {
            var lookup = from i in _context.SAPExportDetail
                         orderby i.OutputName
                         select new
                         {
                             Value = i.OutputName,
                             Text = i.OutputName,
                             Short = i.OutputName
                         };

            var retVal = lookup.Distinct();
            //if (User.IsInRole("Admin"))
            //   retVal.Insert(0, new { Value = -1, Text = "(Manage Listing)", Short = "." });

            return DataSourceLoader.Load(retVal, loadOptions);
        }

        [HttpGet]
        public object EngDisc_Lookup(DataSourceLoadOptions loadOptions)
        {
            var lookup = from i in _context.EngDisc
                         orderby i.EngDiscName
                         select new
                         {
                             Value = i.EngDiscId,
                             Text = i.EngDiscDesc == null ? i.EngDiscName : i.EngDiscName +": "+ i.EngDiscDesc,
                             Short = i.EngDiscName
                         };

            var retVal = lookup.ToList();
            if (User.IsInRole("Admin"))
                retVal.Insert(0, new { Value = -1, Text = "(Manage Listing)", Short = "." });

            return DataSourceLoader.Load(retVal, loadOptions);
        }

        [HttpGet]
        public object ControlKey_Lookup(DataSourceLoadOptions loadOptions)
        {
            var lookup = from i in _context.ControlKey
                         orderby i.ControlKeyName
                         select new
                         {
                             Value = i.ControlKeyId,
                             Text = i.ControlKeyDesc == null ? i.ControlKeyName : i.ControlKeyName + ": " + i.ControlKeyDesc,
                             Short = i.ControlKeyName
                         };

            var retVal = lookup.ToList();
            if (User.IsInRole("Admin"))
                retVal.Insert(0, new { Value = -1, Text = "(Manage Listing)", Short = "." });

            return DataSourceLoader.Load(retVal, loadOptions);
        }

        [HttpGet]
        public object SchedulingPeriodUom_Lookup(DataSourceLoadOptions loadOptions)
        {
            var lookup = from i in _context.SchedulingPeriodUOM
                         orderby i.SchedulingPeriodUOMName
                         select new
                         {
                             Value = i.SchedulingPeriodUOMId,
                             Text = i.SchedulingPeriodUOMName == null ? i.SchedulingPeriodUOMName : i.SchedulingPeriodUOMName + ": " + i.SchedulingPeriodUOMDesc,
                             Short = i.SchedulingPeriodUOMName
                         };

            var retVal = lookup.ToList();
            if (User.IsInRole("Admin"))
                retVal.Insert(0, new { Value = -1, Text = "(Manage Listing)", Short = "." });

            return DataSourceLoader.Load(retVal, loadOptions);
        }


        [HttpGet]
        public object Priority_Lookup(DataSourceLoadOptions loadOptions)
        {
            var lookup = from i in _context.Priority
                         orderby i.PriorityName
                         select new
                         {
                             Value = i.PriorityId,
                             Text = i.PriorityDesc == null ? i.PriorityName : i.PriorityName + ": " + i.PriorityDesc,
                             Short = i.PriorityName
                         };

            var retVal = lookup.ToList();
            if (User.IsInRole("Admin"))
                retVal.Insert(0, new { Value = -1, Text = "(Manage Listing)", Short = "." });

            return DataSourceLoader.Load(retVal, loadOptions);
        }

        [HttpGet]
        public object HeaderFloc_Lookup(DataSourceLoadOptions loadOptions)
        {
            var lookup = _context.FlocXmaintItem
                         .Include(i=> i.Floc)
                         .OrderBy(i=> i.Floc.TagFloc)
                         .Select( x=> new
                         {
                             Value = x.FlocId,
                             Text = x.Floc.TagFloc,
                             Parent = x.MaintItemId,
                             Short = x.FlocId
                         });

            var retVal = lookup.ToList();
            if (User.IsInRole("Admin"))
                retVal.Insert(0, new { Value = -1, Text = "(Manage Listing)", Parent = 0, Short = 1 });

            return DataSourceLoader.Load(retVal, loadOptions);
        }

        [HttpGet]
        public object EnvZone_Lookup(DataSourceLoadOptions loadOptions)
        {
            var lookup = from i in _context.EnvZone
                         orderby i.EnvZoneName
                         select new
                         {
                             Value = i.EnvZoneId,
                             Text = i.EnvZoneName == null ? i.EnvZoneName : i.EnvZoneName + ": " + i.EnvZoneDesc,
                             Short = i.EnvZoneName
                         };

            var retVal = lookup.ToList();
            if (User.IsInRole("Admin"))
                retVal.Insert(0, new { Value = -1, Text = "(Manage Listing)", Short = "." });

            return DataSourceLoader.Load(retVal, loadOptions);
        }


        [HttpGet]
        public object TaskListGroup_Lookup(DataSourceLoadOptions loadOptions)
        {
            var lookup = from i in _context.TaskListGroup
                         orderby i.TaskListGroupName
                         select new
                         {
                             Value = i.TaskListGroupId,
                             Text = i.TaskListGroupName == null ? i.TaskListGroupName : i.TaskListGroupName + ": " + i.TaskListGroupDesc,
                             Short = i.TaskListGroupName
                         };

            var retVal = lookup.ToList();
            if (User.IsInRole("Admin"))
                retVal.Insert(0, new { Value = -1, Text = "(Manage Listing)", Short = "." });

            return DataSourceLoader.Load(retVal, loadOptions);
        }

        [HttpGet]
        public object TaskListHeader_Lookup(DataSourceLoadOptions loadOptions)
        {
            var lookup = from i in _context.TaskListHeader
                         orderby i.TaskListGroup.TaskListGroupName
                         select new
                         {
                             Value = i.TaskListHeaderId,
                             Text = i.TaskListGroup.TaskListGroupName +"_"+ i.Counter,
                             Short = i.TaskListGroup.TaskListGroupName + "_" + i.Counter
                         };

            var retVal = lookup.ToList();
            if (User.IsInRole("Admin"))
                retVal.Insert(0, new { Value = -1, Text = "(Manage Listing)", Short = "." });

            return DataSourceLoader.Load(retVal, loadOptions);
        }

        [HttpGet]
        public object RelationshiptoOperation_Lookup(DataSourceLoadOptions loadOptions)
        {
            var lookup = from i in _context.RelationshipToOperation
                         orderby i.RelationshipToOperationName
                         select new
                         {
                             Value = i.RelationshipToOperationId,
                             Text = i.RelationshipToOperationName,
                             Short = i.RelationshipToOperationName
                         };

            var retVal = lookup.ToList();
            if (User.IsInRole("Admin"))
                retVal.Insert(0, new { Value = -1, Text = "(Manage Listing)", Short = "." });

            return DataSourceLoader.Load(retVal, loadOptions);
        }

        [HttpGet]
        public object ScePsReview_Lookup(DataSourceLoadOptions loadOptions)
        {
            var lookup = from i in _context.ScePsreview
                         orderby i.ScePsreviewName
                         select new
                         {
                             Value = i.ScePsreviewId,
                             Text = i.ScePsreviewName == null ? i.ScePsreviewName : i.ScePsreviewName + ": " + i.ScePsreviewDesc,
                             Short = i.ScePsreviewName
                         };

            var retVal = lookup.ToList();
            if (User.IsInRole("Admin"))
                retVal.Insert(0, new { Value = -1, Text = "(Manage Listing)", Short = "." });

            return DataSourceLoader.Load(retVal, loadOptions);
        }


        [HttpGet]
        public object MaintScePsReviewTeam_Lookup(DataSourceLoadOptions loadOptions)
        {
            var lookup = from i in _context.MaintScePsReviewTeam
                         orderby i.MaintScePsReviewTeamName
                         select new
                         {
                             Value = i.MaintScePsReviewTeamId,
                             Text = i.MaintScePsReviewTeamName == null ? i.MaintScePsReviewTeamName : i.MaintScePsReviewTeamName + ": " + i.MaintScePsReviewTeamDesc,
                             Short = i.MaintScePsReviewTeamName
                         };

            var retVal = lookup.ToList();
            if (User.IsInRole("Admin"))
                retVal.Insert(0, new { Value = -1, Text = "(Manage Listing)", Short = "." });

            return DataSourceLoader.Load(retVal, loadOptions);
        }

        [HttpGet]
        public object ColumnSets_Lookup(DataSourceLoadOptions loadOptions, string ColumnSetsEntity)
        {
            if (ColumnSetsEntity != null)
            {
                var lookup = _context.ColumnSets
                         .Where(x => x.ColumnSetsEntity == ColumnSetsEntity)
                         .OrderBy(x => x.ColumnSetsOrder)
                         .Select(x => new
                         {
                             Value = 0,
                             Text = x.ColumnSetsName,
                             Short = x.ColumnSetsName
                         })
                         .ToList();
                var retVal = lookup.Distinct();
                return DataSourceLoader.Load(retVal, loadOptions);
            }
            else
            {
                var lookup = _context.ColumnSets
                    .OrderBy(x => x.ColumnSetsOrder)
                    .Select(x => new
                    {
                        Value = 0,
                        Text = x.ColumnSetsName,
                        Short = x.ColumnSetsName
                    })
                    .ToList();
                var retVal = lookup.Distinct();
                return DataSourceLoader.Load(retVal, loadOptions);
            }

        }
        //Just selected all the values in the superclass and picks the description
        [HttpGet]
        public object SuperClass_Lookup(DataSourceLoadOptions loadOptions)
        {
            var lookup = _context.SuperClass
                .OrderBy(x => x.SuperclassName)
                .Select(x => new
                {
                    Value = 0,
                    Text = x.Superclassdescription,
                    Short = x.SuperclassName
                });

            var retVal = lookup.ToList();
            if (User.IsInRole("Admin"))
                retVal.Insert(0, new { Value = -1, Text = "(Manage Listing)", Short = "." });

            return DataSourceLoader.Load(retVal, loadOptions);

        }

        //Ues the Id of the superclass to call the engclasses values
        [HttpGet]
        public object EngClassBySuperClass_Lookup(DataSourceLoadOptions loadOptions, int superClassId)
        {

            var lookup = from i in _context.EngClass
                        .Where (i => i.SuperClassID == superClassId)
                         orderby i.EngClassId
                         select new
                         {
                             Value = i.EngClassId,
                             Text = i.EngClassDesc == null ? i.EngClassName : i.EngClassName + ": " + i.EngClassDesc,
                             Parent = i.SuperClassID,
                             Short = i.EngClassName,
                         };



            var retVal = lookup.ToList();
            if (User.IsInRole("Admin"))
                retVal.Insert(0, new { Value = -1, Text = "(Manage Listing)", Parent = 0, Short = "." });

            return DataSourceLoader.Load(retVal, loadOptions);
        }


        public JsonResult KeylistxEngDataCode_Insert(int keylistId, int engDataCodeId)
        {
            var newkeylistXengdatacode = new KeyListxEngDataCode();

            newkeylistXengdatacode.KeyListId = keylistId;
            newkeylistXengdatacode.EngDataCode = engDataCodeId;

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


            return Json("OK");
        }



        //This lookup takes in the superclassID and uses it search for the engclass and the attributes that are required
        [HttpGet]
        public object SuperClassToEngCodeData_Lookup(DataSourceLoadOptions loadOptions, int superClassId)
        {

            var lookup = from i in _context.EngClass
                         join e in _context.EngDataClassxEngDataCode on i.EngClassId equals e.EngClassId
                         join t in _context.BccCode on e.BccCodeId equals t.BccCodeId
                         join y in _context.EngDataCode on e.EngDataCodeId equals y.EngDataCodeId
                         where i.SuperClassID == superClassId
                         select new
                         {
                             Value = i.EngClassId,
                             Text = i.EngClassDesc == null ? i.EngClassName : i.EngClassName + ": " + i.EngClassDesc + ": " + t.BccCodeId + ": " + y.EngDataCodeName + ": " + y.EngDataCodeDesc + (y.EngDataCodeSAPDesc ?? " BLANK ") + (y.EngDataCodeDDLType ?? " BLANK "),
                             Parent = i.SuperClassID,
                             Short = i.EngClassName
                         };

            var retVal = lookup.ToList();

            if (User.IsInRole("Admin"))
                retVal.Insert(0, new { Value = -1, Text = "(Manage Listing)", Parent = 0, Short = "." });

            return DataSourceLoader.Load(retVal, loadOptions);
        }


        //Test controller too see how the values are returned.
        [HttpGet]
        public object test_Lookup(DataSourceLoadOptions loadOptions, int superClassId)
        {

            var lookup = from i in _context.EngClass
                         join e in _context.EngDataClassxEngDataCode on i.EngClassId equals e.EngClassId
                         join t in _context.BccCode on e.BccCodeId equals t.BccCodeId
                         join y in _context.EngDataCode on e.EngDataCodeId equals y.EngDataCodeId
                         where i.SuperClassID == superClassId
                         select new
                         {
                             Value = i.EngClassId,
                             Text = i.EngClassDesc == null ? i.EngClassName : i.EngClassName + ": " + i.EngClassDesc + ": " + t.BccCodeId + ": " + y.EngDataCodeName + ": " + y.EngDataCodeDesc + (y.EngDataCodeSAPDesc ?? " BLANK ") + (y.EngDataCodeDDLType ?? " BLANK "),
                             Parent = i.SuperClassID,
                             Short = i.EngClassName
                         };

            var retVal = lookup.ToList();

            if (User.IsInRole("Admin"))
                retVal.Insert(0, new { Value = -1, Text = "(Manage Listing)", Parent = 0, Short = "." });

            return DataSourceLoader.Load(retVal, loadOptions);
        }


        [HttpGet]
        public object MaintParent_Lookup(DataSourceLoadOptions loadOptions)
        {
            var lookup = from i in _context.Tag
                         orderby i.TagNumber
                         select new
                         {
                             Value = i.TagId,
                             Text = i.TagFlocDesc == null ? i.TagNumber : i.TagNumber +": "+ i.TagFlocDesc,
                             Short = i.TagNumber
                         };

            var retVal = lookup.ToList();
            if (User.IsInRole("Admin"))
                retVal.Insert(0, new { Value = -1, Text = "(Manage Listing)", Short = "." });

            return DataSourceLoader.Load(retVal, loadOptions);
        }

        [HttpGet]
        public object EngStatus_Lookup(DataSourceLoadOptions loadOptions)
        {
            var lookup = from i in _context.EngStatus
                         orderby i.EngStatusName
                         select new
                         {
                             Value = i.EngStatusId,
                             Text = i.EngStatusName,
                             Short = i.EngStatusName
                         };

            var retVal = lookup.ToList();
            if (User.IsInRole("Admin"))
                retVal.Insert(0, new { Value = -1, Text = "(Manage Listing)", Short = "." });

            return DataSourceLoader.Load(retVal, loadOptions);
        }

        [HttpGet]
        public object CommZone_Lookup(DataSourceLoadOptions loadOptions)
        {
            var lookup = from i in _context.CommZone
                         orderby i.CommZoneName
                         select new
                         {
                             Value = i.CommZoneId,
                             Text = i.CommZoneDesc==null ? i.CommZoneName : i.CommZoneName +": "+ i.CommZoneDesc,
                             Short = i.CommZoneName
                         };

            var retVal = lookup.ToList();
            if (User.IsInRole("Admin"))
                retVal.Insert(0, new { Value = -1, Text = "(Manage Listing)", Short = "." });

            return DataSourceLoader.Load(retVal, loadOptions);
        }

        [HttpGet]
        public object Location_Lookup(DataSourceLoadOptions loadOptions)
        {
            var lookup = from i in _context.Location
                         orderby i.LocationName
                         select new
                         {
                             Value = i.LocationID,
                             Text =  i.LocationName,
                             Short = i.LocationName
                         };

            var retVal = lookup.ToList();
            if (User.IsInRole("Admin"))
                retVal.Insert(0, new { Value = -1, Text = "(Manage Listing)", Short = "." });

            return DataSourceLoader.Load(retVal, loadOptions);
        }

        [HttpGet]
        public object MaintType_Lookup(DataSourceLoadOptions loadOptions)
        {
            var lookup = from i in _context.MaintType
                         orderby i.MaintTypeName
                         select new
                         {
                             Value = i.MaintTypeId,
                             Text = i.MaintTypeDesc==null? i.MaintTypeName : i.MaintTypeName +": "+ i.MaintTypeDesc,
                             Short = i.MaintTypeDesc
                         };

            var retVal = lookup.ToList();
            if (User.IsInRole("Admin"))
                retVal.Insert(0, new { Value = -1, Text = "(Manage Listing)", Short = "." });

            return DataSourceLoader.Load(retVal, loadOptions);
        }

        [HttpGet]
        public object SAPStatus_Lookup(DataSourceLoadOptions loadOptions)
        {
            var lookup = from i in _context.SAPStatus
                         orderby i.StatusCode
                         select new
                         {
                             Value = i.SAPStatusId,
                             Text = i.Description==null ? i.StatusCode.ToString() : i.StatusCode.ToString() + ": "+ i.Description,
                             Short = i.Description
                         };

            var retVal = lookup.ToList();
            if (User.IsInRole("Admin"))
                retVal.Insert(0, new { Value = -1, Text = "(Manage Listing)", Short = "." });

            return DataSourceLoader.Load(retVal, loadOptions);
        }

        [HttpGet]
        public object EngDataCode_Lookup(DataSourceLoadOptions loadOptions)
        {
            var lookup = _context.EngDataCode
                         .OrderBy(i=> i.EngDataCodeName)
                         .Select( i=> new {
                             Value = i.EngDataCodeId,
                             Text = i.EngDataCodeName,
                             Short = i.EngDataCodeName
                         });

            var retVal = lookup.ToList();
            if (User.IsInRole("Admin"))
                retVal.Insert(0, new { Value = -1, Text = "(Manage Listing)", Short = "." });

            return DataSourceLoader.Load(retVal, loadOptions);
        }

        [HttpGet]
        public object EngDataCodeDropDown_Lookup(DataSourceLoadOptions loadOptions)
        {
            var lookup = _context.EngDataCodeDropDown
                         .OrderBy(x=> x.EngDataCodeDropDownValue)
                         .Select(i => new {
                             Value = i.EngDataCodeId,
                             Text = i.EngDataCodeDropDownDesc == null ? i.EngDataCodeDropDownValue : i.EngDataCodeDropDownValue + ": " + i.EngDataCodeDropDownDesc,
                             Parent = i.EngDataCodeId,
                             Short = i.EngDataCodeDropDownValue
                            });

            var retVal = lookup.ToList();
            if (User.IsInRole("Admin"))
                retVal.Insert(0, new { Value = -1, Text = "(Manage Listing)", Parent=0, Short = "." });

            return DataSourceLoader.Load(retVal, loadOptions);
        }

        [HttpGet]
        public object Manufacturer_Lookup(DataSourceLoadOptions loadOptions)
        {
            var lookup = from i in _context.Manufacturer
                         orderby i.ManufacturerName
                         select new
                         {
                             Value = i.ManufacturerId,
                             Text = i.ManufacturerDesc==null ? i.ManufacturerName : i.ManufacturerName +": "+ i.ManufacturerDesc,
                             Short = i.ManufacturerName
                         };

            var retVal = lookup.ToList();
            if (User.IsInRole("Admin"))
                retVal.Insert(0, new { Value = -1, Text = "(Manage Listing)", Short = "." });

            return DataSourceLoader.Load(retVal, loadOptions);
        }

        [HttpGet]
        public object Models_Lookup(DataSourceLoadOptions loadOptions)
        {
            var lookup = from i in _context.Models
                         orderby i.ModelName
                         select new
                         {
                             Value = i.ModelId,
                             Text = i.ModelDesc==null ? i.ModelName : i.ModelName + ": " + i.ModelDesc,
                             Short = i.ModelName
                         };

            var retVal = lookup.ToList();
            if (User.IsInRole("Admin"))
                retVal.Insert(0, new { Value = -1, Text = "(Manage Listing)", Short = "." });

            return DataSourceLoader.Load(retVal, loadOptions);
        }

        [HttpGet]
        public object ManModel_Lookup(DataSourceLoadOptions loadOptions)
        {

            var lookup = _context.Models
                        .Include(x=> x.Manufacturer)
                        .OrderBy(x=> x.Manufacturer.ManufacturerName)
                        .OrderBy(x => x.ModelName)
                         .Select(x => new {
                            Value = x.ModelId,
                             Parent = x.ManufacturerId ?? default,
                             Text = x.Manufacturer.ManufacturerName + ": " + x.ModelName,
                            Short = x.ModelName
                        });

            var retVal = lookup.ToList();
            if (User.IsInRole("Admin"))
                retVal.Insert(0, new { Value = -1, Parent=0, Text = "(Manage Listing)", Short = "." });


            return DataSourceLoader.Load(retVal, loadOptions);
        }

         [HttpGet]
        public object MaintPlan_Lookup(DataSourceLoadOptions loadOptions)
        {
            var lookup = from i in _context.MaintPlan
                         orderby i.MaintPlanName
                         select new
                         {
                             Value = i.MaintPlanId,
                             Text = i.MaintPlanName,
                             Short = i.MaintPlanName
                         };

            var retVal = lookup.ToList();
            if (User.IsInRole("Admin"))
                retVal.Insert(0, new { Value = -1, Text = "(Manage Listing)", Short = "." });

            return DataSourceLoader.Load(retVal, loadOptions);
        }

        //setCellValue_RbiSil
        [HttpGet]
        public object RbiSil_Lookup(DataSourceLoadOptions loadOptions)
        {
            var lookup = from i in _context.RbiSil
                         orderby i.RbiSilName
                         select new
                         {
                             Value = i.RbiSilId,
                             Text = i.RbiSilName,
                             Short = i.RbiSilDesc
                         };

            var retVal = lookup.ToList();
            if (User.IsInRole("Admin"))
                retVal.Insert(0, new { Value = -1, Text = "(Manage Listing)", Short = "." });

            return DataSourceLoader.Load(retVal, loadOptions);
        }

        [HttpGet]
        public object MaintPackage_Lookup(DataSourceLoadOptions loadOptions)
        {
            var lookup = from i in _context.MaintPackage
                         orderby i.MaintPackageId
                         select new
                         {
                             Value = i.MaintPackageId,
                             Text = i.MaintPackageName,
                             Short = i.MaintPackageName
                         };

            var retVal = lookup.ToList();
            if (User.IsInRole("Admin"))
                retVal.Insert(0, new { Value = -1, Text = "(Manage Listing)", Short = "." });

            return DataSourceLoader.Load(retVal, loadOptions);
        }

        [HttpGet]
        public object MaintenancePlant_Lookup(DataSourceLoadOptions loadOptions)
        {
            var lookup = from i in _context.MaintenancePlant
                         orderby i.MaintenancePlantNum
                         select new
                         {
                             Value = i.MaintenancePlantId,
                             Text = i.MaintenancePlantDesc==null ? i.MaintenancePlantNum : i.MaintenancePlantNum +": "+ i.MaintenancePlantDesc,
                             Short = i.MaintenancePlantNum
                         };

            var retVal = lookup.ToList();
            if (User.IsInRole("Admin"))
                retVal.Insert(0, new { Value = -1, Text = "(Manage Listing)", Short = "." });

            return DataSourceLoader.Load(retVal, loadOptions);
        }


        [HttpGet]
        public object Vib_Lookup(DataSourceLoadOptions loadOptions)
        {
            var lookup = from i in _context.Vib
                         orderby i.VibName
                         select new
                         {
                             Value = i.VibId,
                             Text = i.VibDesc==null ? i.VibName : i.VibName + ": " + i.VibDesc,
                             Short = i.VibName
                         };

            var retVal = lookup.ToList();
            if (User.IsInRole("Admin"))
                retVal.Insert(0, new { Value = -1, Text = "(Manage Listing)", Short = "." });

            return DataSourceLoader.Load(retVal, loadOptions);
        }


        [HttpGet]
        public object Rbm_Lookup(DataSourceLoadOptions loadOptions)
        {
            var lookup = from i in _context.Rbm
                         orderby i.RbmName
                         select new
                         {
                             Value = i.RbmId,
                             Text = i.RbmDesc==null ? i.RbmName : i.RbmName + ": " + i.RbmDesc,
                             Short = i.RbmName
                         };

            var retVal = lookup.ToList();
            if (User.IsInRole("Admin"))
                retVal.Insert(0, new { Value = -1, Text = "(Manage Listing)", Short = "." });

            return DataSourceLoader.Load(retVal, loadOptions);
        }

        [HttpGet]
        public object Rcm_Lookup(DataSourceLoadOptions loadOptions)
        {
            var lookup = from i in _context.Rcm
                         orderby i.RcmName
                         select new
                         {
                             Value = i.RcmId,
                             Text = i.RcmDesc==null ? i.RcmName : i.RcmName + ": " + i.RcmDesc,
                             Short = i.RcmName
                         };

            var retVal = lookup.ToList();
            if (User.IsInRole("Admin"))
                retVal.Insert(0, new { Value = -1, Text = "(Manage Listing)", Short = "." });

            return DataSourceLoader.Load(retVal, loadOptions);
        }


        [HttpGet]
        public object MaintArea_Lookup(DataSourceLoadOptions loadOptions)
        {
            var lookup = from i in _context.MaintArea
                         orderby i.MaintAreaName
                         select new
                         {
                             Value = i.MaintAreaId,
                             Text = i.MaintAreaDesc==null ? i.MaintAreaName : i.MaintAreaName + ": " + i.MaintAreaDesc,
                             Parent = i.PlantSectionId,
                             Short = i.MaintAreaName
                         };

            var retVal = lookup.ToList();
            if (User.IsInRole("Admin"))
                retVal.Insert(0, new { Value = -1, Text = "(Manage Listing)", Parent = (int?)0, Short = "." });

            return DataSourceLoader.Load(retVal, loadOptions);
        }

        [HttpGet]
        public object MaintItem_Lookup(DataSourceLoadOptions loadOptions)
        {
            var lookup = from i in _context.MaintItem
                         orderby i.MaintItemNum
                         select new
                         {
                             Value = i.MaintItemId,
                             Text = i.MaintItemShortText==null ? i.MaintItemNum : i.MaintItemNum + ": " + i.MaintItemShortText,
                             Short = i.MaintItemNum,
                         };

            var retVal = lookup.ToList();
            if (User.IsInRole("Admin"))
                retVal.Insert(0, new { Value = -1, Text = "(Manage Listing)", Short = "." });

            return DataSourceLoader.Load(retVal, loadOptions);
        }

        [HttpGet]
        public object MaintLocation_Lookup(DataSourceLoadOptions loadOptions)
        {
            var lookup = from i in _context.MaintLocation
                         orderby i.MaintLocationName
                         select new
                         {
                             Value = i.MaintLocationId,
                             Text = i.MaintLocationDesc == null ? i.MaintLocationName : i.MaintLocationName + ": " + i.MaintLocationDesc,
                             Parent = i.MaintAreaId ?? default,
                             Short = i.MaintLocationName,
                         };

            var retVal = lookup.ToList();
            if (User.IsInRole("Admin"))
                retVal.Insert(0, new { Value = -1, Text = "(Manage Listing)", Parent=0, Short = "." });

            return DataSourceLoader.Load(retVal, loadOptions);
        }

        [HttpGet]
        public object Area_Lookup(DataSourceLoadOptions loadOptions)
        {
            var lookup = from i in _context.Area
                         orderby i.AreaName
                         select new
                         {
                             Value = i.AreaId,
                             Text = i.AreaDisc == null ? i.AreaName : i.AreaName + ": " + i.AreaDisc,
                             Short = i.AreaName
                         };

            var retVal = lookup.ToList();
            if (User.IsInRole("Admin"))
                retVal.Insert(0, new { Value = -1, Text = "(Manage Listing)", Short = "." });

            return DataSourceLoader.Load(retVal, loadOptions);
        }

        [HttpGet]
        public object MaintCriticality_Lookup(DataSourceLoadOptions loadOptions)
        {
            var lookup = from i in _context.MaintCriticality
                         orderby i.MaintCriticalityName
                         select new
                         {
                             Value = i.MaintCriticalityId,
                             Text = i.MaintCriticalityDesc==null? i.MaintCriticalityName : i.MaintCriticalityName + ": " + i.MaintCriticalityDesc,
                             Short = i.MaintCriticalityName
                         };

            var retVal = lookup.ToList();
            if (User.IsInRole("Admin"))
                retVal.Insert(0, new { Value = -1, Text = "(Manage Listing)", Short = "." });

            return DataSourceLoader.Load(retVal, loadOptions);
        }

        [HttpGet]
        public object MaintObjectType_Lookup(DataSourceLoadOptions loadOptions)
        {
            var lookup = from i in _context.MaintObjectType
                         orderby i.MaintObjectTypeName
                         select new
                         {
                             Value = i.MaintObjectTypeId,
                             Text = i.MaintObjectTypeDesc==null ? i.MaintObjectTypeName : i.MaintObjectTypeName + ": " + i.MaintObjectTypeDesc,
                             Short = i.MaintObjectTypeName
                         };

            var retVal = lookup.ToList();
            if (User.IsInRole("Admin"))
                retVal.Insert(0, new { Value = -1, Text = "(Manage Listing)", Short = "." });

            return DataSourceLoader.Load(retVal, loadOptions);
        }

        [HttpGet]
        public object SubSystem_Lookup(DataSourceLoadOptions loadOptions)
        {
            var lookup = from i in _context.SubSystem
                         orderby i.SubSystemNum
                         select new
                         {
                             Value = i.SubSystemId,
                             Text = i.SubSystemName==null? i.SubSystemNum : i.SubSystemNum + ": " + i.SubSystemName,
                             Short = i.SubSystemNum
                         };

            var retVal = lookup.ToList();
            if (User.IsInRole("Admin"))
                retVal.Insert(0, new { Value = -1, Text = "(Manage Listing)", Short = "." });

            return DataSourceLoader.Load(retVal, loadOptions);
        }

        [HttpGet]
        public object MaintWorkCentre_Lookup(DataSourceLoadOptions loadOptions)
        {
            var lookup = from i in _context.MaintWorkCentre
                         orderby i.MaintWorkCentreName
                         select new
                         {
                             Value = i.MaintWorkCentreId,
                             Text = i.MaintWorkCentreDesc==null ? i.MaintWorkCentreName : i.MaintWorkCentreName + ": " + i.MaintWorkCentreDesc,
                             Short = i.MaintWorkCentreName
                         };

            var retVal = lookup.ToList();
            if (User.IsInRole("Admin"))
                retVal.Insert(0, new { Value = -1, Text = "(Manage Listing)", Short = "." });

            return DataSourceLoader.Load(retVal, loadOptions);
        }

        [HttpGet]
        public object WBSElement_Lookup(DataSourceLoadOptions loadOptions)
        {
            var lookup = from i in _context.WBSElement
                         orderby i.WBSElementName
                         select new
                         {
                             Value = i.WBSElementId,
                             Text = i.WBSElementDesc==null? i.WBSElementName : i.WBSElementName + ": " + i.WBSElementDesc,
                             Short = i.WBSElementName
                         };

            var retVal = lookup.ToList();
            if (User.IsInRole("Admin"))
                retVal.Insert(0, new { Value = -1, Text = "(Manage Listing)", Short = "." });

            return DataSourceLoader.Load(retVal, loadOptions);
        }

        //[HttpGet]
        //public object TaskListClass_Lookup(DataSourceLoadOptions loadOptions)
        //{
        //    var lookup = from i in _context.CommClass
        //                 orderby i.TaskListCatName
        //                 select new
        //                 {
        //                     Value = i.TaskListCatId,
        //                     Text = i.TaskListCatDesc == null ? i.TaskListCatName : i.TaskListCatName + ": " + i.TaskListCatDesc,
        //                     Short = i.TaskListCatName
        //                 };

        //    var retVal = lookup.ToList();
        //    if (User.IsInRole("Admin"))
        //        retVal.Insert(0, new { Value = -1, Text = "(Manage Listing)", Short = "." });

        //    return DataSourceLoader.Load(retVal, loadOptions);
        //}

        [HttpGet]
        public object TaskListCat_Lookup(DataSourceLoadOptions loadOptions)
        {
            var lookup = from i in _context.TaskListCat
                         orderby i.TaskListCatName
                         select new
                         {
                             Value = i.TaskListCatId,
                             Text = i.TaskListCatDesc==null ? i.TaskListCatName : i.TaskListCatName + ": " + i.TaskListCatDesc,
                             Short = i.TaskListCatName
                         };

            var retVal = lookup.ToList();
            if (User.IsInRole("Admin"))
                retVal.Insert(0, new { Value = -1, Text = "(Manage Listing)", Short = "." });

            return DataSourceLoader.Load(retVal, loadOptions);
        }

        [HttpGet]
        public object Doc_Lookup(DataSourceLoadOptions loadOptions)
        {
            var lookup = from i in _context.Doc
                         orderby i.DocNum
                         select new
                         {
                             Value = i.DocId,
                             Text = i.DocTitle==null ? i.DocNum : i.DocNum + ": " + i.DocTitle,
                             Short = i.DocNum
                         };

            var retVal = lookup.ToList();
            //if (User.IsInRole("Admin"))
            //    retVal.Insert(0, new { Value = -1, Text = "(Manage Listing)", Short = "." });

            return DataSourceLoader.Load(retVal, loadOptions);
        }


        [HttpGet]
        public object CompanyCode_Lookup(DataSourceLoadOptions loadOptions)
        {
            var lookup = from i in _context.CompanyCode
                         orderby i.CompanyCodeName
                         select new
                         {
                             Value = i.CompanyCodeId,
                             Text = i.CompanyCodeDesc==null? i.CompanyCodeName : i.CompanyCodeName + ": " + i.CompanyCodeDesc,
                             Short = i.CompanyCodeName
                         };

            var retVal = lookup.ToList();
            if (User.IsInRole("Admin"))
                retVal.Insert(0, new { Value = -1, Text = "(Manage Listing)", Short = "." });

            return DataSourceLoader.Load(retVal, loadOptions);
        }

        [HttpGet]
        public object SortField_Lookup(DataSourceLoadOptions loadOptions)
        {
            var lookup = from i in _context.SortField
                         orderby i.SortFieldName
                         select new
                         {
                             Value = i.SortFieldId,
                             Text = i.SortFieldDesc==null? i.SortFieldName : i.SortFieldName + ": " + i.SortFieldDesc,
                             Short = i.SortFieldName
                         };

            var retVal = lookup.ToList();
            if (User.IsInRole("Admin"))
                retVal.Insert(0, new { Value = -1, Text = "(Manage Listing)", Short = "." });

            return DataSourceLoader.Load(retVal, loadOptions);
        }

        [HttpGet]
        public object PlannerPlant_Lookup(DataSourceLoadOptions loadOptions)
        {
            var lookup = from i in _context.PlannerPlant
                         orderby i.PlannerPlantName
                         select new
                         {
                             Value = i.PlannerPlantId,
                             Text = i.PlannerPlantDesc==null ? i.PlannerPlantName : i.PlannerPlantName + ": " + i.PlannerPlantDesc,
                             Short = i.PlannerPlantName
                         };

            var retVal = lookup.ToList();
            if (User.IsInRole("Admin"))
                retVal.Insert(0, new { Value = -1, Text = "(Manage Listing)", Short = "." });

            return DataSourceLoader.Load(retVal, loadOptions);
        }

        [HttpGet]
        public object MaintPlannerGroup_Lookup(DataSourceLoadOptions loadOptions)
        {
            var lookup = from i in _context.MaintPlannerGroup
                         orderby i.MaintPlannerGroupName
                         select new
                         {
                             Value = i.MaintPlannerGroupId,
                             Text = i.MaintPlannerGroupDesc==null ? i.MaintPlannerGroupName : i.MaintPlannerGroupName + ": " + i.MaintPlannerGroupDesc,
                             Short = i.MaintPlannerGroupName
                         };

            var retVal = lookup.ToList();
            if (User.IsInRole("Admin"))
                retVal.Insert(0, new { Value = -1, Text = "(Manage Listing)", Short = "." });

            return DataSourceLoader.Load(retVal, loadOptions);
        }

        [HttpGet]
        public object CommissioningSubsystem_Lookup(DataSourceLoadOptions loadOptions)
        {
            var lookup = from i in _context.CommSubSystem
                         orderby i.CommSubSystemName
                         select new
                         {
                             Value = i.CommSubSystemId,
                             Text = i.CommSubSystemNo + ": " + i.CommSubSystemName,
                             Short = i.CommSubSystemNo
                         };

            var retVal = lookup.ToList();
            if (User.IsInRole("Admin"))
                retVal.Insert(0, new { Value = -1, Text = "(Manage Listing)", Short = "." });

            return DataSourceLoader.Load(retVal, loadOptions);
        }

        [HttpGet]
        public object ComnpanyCode_Lookup(DataSourceLoadOptions loadOptions)
        {
            var lookup = from i in _context.CompanyCode
                         orderby i.CompanyCodeName
                         select new
                         {
                             Value = i.CompanyCodeId,
                             Text = i.CompanyCodeDesc==null ? i.CompanyCodeName : i.CompanyCodeName + ": " + i.CompanyCodeDesc,
                             Short = i.CompanyCodeName
                         };

            var retVal = lookup.ToList();
            if (User.IsInRole("Admin"))
                retVal.Insert(0, new { Value = -1, Text = "(Manage Listing)", Short = "." });

            return DataSourceLoader.Load(retVal, loadOptions);
        }


        [HttpGet]
        public object MaintStructureIndicator_Lookup(DataSourceLoadOptions loadOptions)
        {
            var lookup = from i in _context.MaintStructureIndicator
                         orderby i.MaintStructureIndicatorName
                         select new
                         {
                             Value = i.MaintStructureIndicatorId,
                             Text = i.MaintStructureIndicatorDesc==null ? i.MaintStructureIndicatorName : i.MaintStructureIndicatorName + ": " + i.MaintStructureIndicatorDesc,
                             Short = i.MaintStructureIndicatorName
                         };

            var retVal = lookup.ToList();
            if (User.IsInRole("Admin"))
                retVal.Insert(0, new { Value = -1, Text = "(Manage Listing)", Short = "." });

            return DataSourceLoader.Load(retVal, loadOptions);
        }

        [HttpGet]
        public object MaintClass_Lookup(DataSourceLoadOptions loadOptions)
        {
            var lookup = from i in _context.MaintClass
                         orderby i.MaintClassName
                         select new
                         {
                             Value = i.MaintClassId,
                             Text = i.MaintClassDesc==null ? i.MaintClassName : i.MaintClassName + ": " + i.MaintClassDesc,
                             Short = i.MaintClassName
                         };

            var retVal = lookup.ToList();
            if (User.IsInRole("Admin"))
                retVal.Insert(0, new { Value = -1, Text = "(Manage Listing)", Short = "." });

            return DataSourceLoader.Load(retVal, loadOptions);
        }

        [HttpGet]
        public object ExMethod_Lookup(DataSourceLoadOptions loadOptions)
        {
            var lookup = from i in _context.ExMethod
                         orderby i.ExMethodName
                         select new
                         {
                             Value = i.ExMethodId,
                             Text = i.ExMethodDesc==null ? i.ExMethodName : i.ExMethodName + ": " + i.ExMethodDesc,
                             Short = i.ExMethodName
                         };

            var retVal = lookup.ToList();
            if (User.IsInRole("Admin"))
                retVal.Insert(0, new { Value = -1, Text = "(Manage Listing)", Short = "." });

            return DataSourceLoader.Load(retVal, loadOptions);
        }

        [HttpGet]
        public object MaintEDCCode_Lookup(DataSourceLoadOptions loadOptions)
        {
            var lookup = from i in _context.MaintEdcCode
                         orderby i.MaintEdcCodeName
                         select new
                         {
                             Value = i.MaintEdcCodeId,
                             Text = i.MaintEdcCodeDesc==null ? i.MaintEdcCodeName : i.MaintEdcCodeName + ": " + i.MaintEdcCodeDesc,
                             Short = i.MaintEdcCodeName
                         };

            var retVal = lookup.ToList();
            if (User.IsInRole("Admin"))
                retVal.Insert(0, new { Value = -1, Text = "(Manage Listing)", Short = "." });

            return DataSourceLoader.Load(retVal, loadOptions);
        }

        [HttpGet]
        public object MaintSortProcess_Lookup(DataSourceLoadOptions loadOptions)
        {
            var lookup = from i in _context.MaintSortProcess
                         orderby i.MaintSortProcessName
                         select new
                         {
                             Value = i.MaintSortProcessId,
                             Text = i.MaintSortProcessDesc==null ? i.MaintSortProcessName : i.MaintSortProcessName + ": " + i.MaintSortProcessDesc,
                             Short = i.MaintSortProcessName
                         };

            var retVal = lookup.ToList();
            if (User.IsInRole("Admin"))
                retVal.Insert(0, new { Value = -1, Text = "(Manage Listing)", Short = "." });

            return DataSourceLoader.Load(retVal, loadOptions);
        }

        [HttpGet]
        public object MaintStatus_Lookup(DataSourceLoadOptions loadOptions)
        {
            var lookup = from i in _context.MaintStatus
                         orderby i.MaintStatusName
                         select new
                         {
                             Value = i.MaintStatusId,
                             Text = i.MaintStatusDesc==null ? i.MaintStatusName : i.MaintStatusName + ": " + i.MaintStatusDesc,
                             Short = i.MaintStatusName
                         };

            var retVal = lookup.ToList();
            if (User.IsInRole("Admin"))
                retVal.Insert(0, new { Value = -1, Text = "(Manage Listing)", Short = "." });

            return DataSourceLoader.Load(retVal, loadOptions);
        }

        [HttpGet]
        public object MaintStrategy_Lookup(DataSourceLoadOptions loadOptions)
        {
            var lookup = from i in _context.MaintStrategy
                         orderby i.MaintStrategyName
                         select new
                         {
                             Value = i.MaintStrategyId,
                             Text = i.MaintStrategyDesc==null ? i.MaintStrategyName : i.MaintStrategyName + ": " + i.MaintStrategyDesc,
                             Short = i.MaintStrategyName
                         };

            var retVal = lookup.ToList();
            if (User.IsInRole("Admin"))
                retVal.Insert(0, new { Value = -1, Text = "(Manage Listing)", Short = "." });

            return DataSourceLoader.Load(retVal, loadOptions);
        }

        [HttpGet]
        public object MeasPoint_Lookup(DataSourceLoadOptions loadOptions)
        {
            var lookup = from i in _context.MeasPoint
                         orderby i.MeasPointName
                         select new
                         {
                             Value = i.MeasPointId,
                             Text = i.MeasPointData==null ? i.MeasPointName : i.MeasPointName + ": " + i.MeasPointData,
                             Short = i.MeasPointName
                         };

            var retVal = lookup.ToList();
            if (User.IsInRole("Admin"))
                retVal.Insert(0, new { Value = -1, Text = "(Manage Listing)", Short = "." });

            return DataSourceLoader.Load(retVal, loadOptions);
        }

        [HttpGet]
        public object Operation_Lookup(DataSourceLoadOptions loadOptions)
        {
            var lookup = from i in _context.Operation
                         orderby i.OperationName
                         select new
                         {
                             Value = i.OperationId,
                             Text = i.OperationName,
                             Short = i.OperationName
                         };

            var retVal = lookup.ToList();
            if (User.IsInRole("Admin"))
                retVal.Insert(0, new { Value = -1, Text = "(Manage Listing)", Short = "." });

            return DataSourceLoader.Load(retVal, loadOptions);
        }

        [HttpGet]
        public object Pbs_Lookup(DataSourceLoadOptions loadOptions)
        {
            var lookup = from i in _context.Pbs
                         orderby i.PbsName
                         select new
                         {
                             Value = i.PbsId,
                             Text = i.PbsDesc==null ? i.PbsName: i.PbsName + ": " + i.PbsDesc,
                             Short = i.PbsName
                         };

            var retVal = lookup.ToList();
            if (User.IsInRole("Admin"))
                retVal.Insert(0, new { Value = -1, Text = "(Manage Listing)", Short = "." });

            return DataSourceLoader.Load(retVal, loadOptions);
        }

        [HttpGet]
        public object PerformanceStandard_Lookup(DataSourceLoadOptions loadOptions)
        {
            var lookup = from i in _context.PerformanceStandard
                         orderby i.PerformanceStandardName
                         select new
                         {
                             Value = i.PerformanceStandardId,
                             Text =  i.PerformanceStandardName,
                             Short = i.PerformanceStandardName
                         };

            var retVal = lookup.ToList();
            if (User.IsInRole("Admin"))
                retVal.Insert(0, new { Value = -1, Text = "(Manage Listing)", Short = "." });

            return DataSourceLoader.Load(retVal, loadOptions);
        }

        [HttpGet]
        public object PlantSection_Lookup(DataSourceLoadOptions loadOptions)
        {
            var lookup = from i in _context.PlantSection
                         orderby i.PlantSectionName
                         select new
                         {
                             Value = i.PlantSectionId,
                             Text = i.PlantSectionDesc==null ? i.PlantSectionName: i.PlantSectionName + ": " + i.PlantSectionDesc,
                             Short = i.PlantSectionName
                         };

            var retVal = lookup.ToList();
            if (User.IsInRole("Admin"))
                retVal.Insert(0, new { Value = -1, Text = "(Manage Listing)", Short = "." });

            return DataSourceLoader.Load(retVal, loadOptions);
        }

        [HttpGet]
        public object PMAssembly_Lookup(DataSourceLoadOptions loadOptions)
        {
            var lookup = from i in _context.Pmassembly
                         orderby i.PmassemblyName
                         select new
                         {
                             Value = i.PmassemblyId,
                             Text =  i.PmassemblyName,
                             Short = i.PmassemblyName
                         };

            var retVal = lookup.ToList();
            if (User.IsInRole("Admin"))
                retVal.Insert(0, new { Value = -1, Text = "(Manage Listing)", Short = "." });

            return DataSourceLoader.Load(retVal, loadOptions);
        }

        [HttpGet]
        public object Po_Lookup(DataSourceLoadOptions loadOptions)
        {
            var lookup = from i in _context.Po
                         orderby i.PoName
                         select new
                         {
                             Value = i.PoId,
                             Text = i.PoDesc==null ? i.PoName : i.PoName + ": " + i.PoDesc,
                             Short = i.PoName
                         };

            var retVal = lookup.ToList();
            if (User.IsInRole("Admin"))
                retVal.Insert(0, new { Value = -1, Text = "(Manage Listing)", Short = "." });

            return DataSourceLoader.Load(retVal, loadOptions);
        }

        [HttpGet]
        public object Project_Lookup(DataSourceLoadOptions loadOptions)
        {
            var lookup = from i in _context.Project
                         orderby i.ProjectCode
                         select new
                         {
                             Value = i.ProjectId,
                             Text = i.ProjectCode + ": " + i.ProjectName,
                             Short = i.ProjectCode
                         };

            var retVal = lookup.ToList();
            if (User.IsInRole("Admin"))
                retVal.Insert(0, new { Value = -1, Text = "(Manage Listing)", Short = "." });

            return DataSourceLoader.Load(retVal, loadOptions);
        }

        [HttpGet]
        public object RegulatoryBody_Lookup(DataSourceLoadOptions loadOptions)
        {
            var lookup = from i in _context.RegulatoryBody
                         orderby i.RegulatoryBodyName
                         select new
                         {
                             Value = i.RegulatoryBodyId,
                             Text = i.RegulatoryBodyDesc==null ? i.RegulatoryBodyName: i.RegulatoryBodyName + ": " + i.RegulatoryBodyDesc,
                             Short = i.RegulatoryBodyName
                         };

            var retVal = lookup.ToList();
            if (User.IsInRole("Admin"))
                retVal.Insert(0, new { Value = -1, Text = "(Manage Listing)", Short = "." });

            return DataSourceLoader.Load(retVal, loadOptions);
        }

        [HttpGet]
        public object RelationshipType_Lookup(DataSourceLoadOptions loadOptions)
        {
            var lookup = from i in _context.RelationshipType
                         orderby i.RelationshipTypeName
                         select new
                         {
                             Value = i.RelationshipTypeId,
                             Text = i.RelationshipTypeDesc==null ? i.RelationshipTypeName: i.RelationshipTypeName + ": " + i.RelationshipTypeDesc,
                             Short = i.RelationshipTypeName
                         };

            var retVal = lookup.ToList();
            if (User.IsInRole("Admin"))
                retVal.Insert(0, new { Value = -1, Text = "(Manage Listing)", Short = "." });

            return DataSourceLoader.Load(retVal, loadOptions);
        }

        [HttpGet]
        public object SP_Lookup(DataSourceLoadOptions loadOptions)
        {
            var lookup = from i in _context.Sp
                         orderby i.Spnum
                         select new
                         {
                             Value = i.Spid,
                             Text = i.Spdesc==null? i.Spnum: i.Spnum + ": " + i.Spdesc,
                             Short = i.Spnum
                         };

            var retVal = lookup.ToList();
            if (User.IsInRole("Admin"))
                retVal.Insert(0, new { Value = -1, Text = "(Manage Listing)", Short = "." });

            return DataSourceLoader.Load(retVal, loadOptions);
        }

        [HttpGet]
        public object SysCond_Lookup(DataSourceLoadOptions loadOptions)
        {
            var lookup = from i in _context.SysCond
                         orderby i.SysCondName
                         select new
                         {
                             Value = i.SysCondId,
                             Text = i.SysCondDesc==null? i.SysCondName : i.SysCondName + ": " + i.SysCondDesc,
                             Short = i.SysCondName
                         };

            var retVal = lookup.ToList();
            if (User.IsInRole("Admin"))
                retVal.Insert(0, new { Value = -1, Text = "(Manage Listing)", Short = "." });

            return DataSourceLoader.Load(retVal, loadOptions);
        }

        [HttpGet]
        public object System_Lookup(DataSourceLoadOptions loadOptions)
        {
            var lookup = from i in _context.System
                         orderby i.SystemNum
                         select new
                         {
                             Value = i.SystemsId,
                             Text = i.SystemNum + ": " + i.SystemName,
                             Short = i.SystemNum
                         };

            var retVal = lookup.ToList();
            if (User.IsInRole("Admin"))
                retVal.Insert(0, new { Value = -1, Text = "(Manage Listing)", Short = "." });

            return DataSourceLoader.Load(retVal, loadOptions);
        }

        [HttpGet]
        public object TagEngData_Lookup(DataSourceLoadOptions loadOptions)
        {
            var lookup = from i in _context.TagEngData
                         orderby i.EngDataCode
                         select new
                         {
                             Value = i.EngDataCodeId,
                             Text = i.EngDataCode==null? i.EngDatavalue: i.EngDatavalue + ": " + i.EngDataCode,
                             Short = i.EngDatavalue
                         };

            var retVal = lookup.ToList();
            if (User.IsInRole("Admin"))
                retVal.Insert(0, new { Value = -1, Text = "(Manage Listing)", Short = "." });

            return DataSourceLoader.Load(retVal, loadOptions);
        }

        [HttpGet]
        public object EngClass_Lookup(DataSourceLoadOptions loadOptions)
        {
            var lookup = from i in _context.EngClass
                         orderby i.EngClassName
                         select new
                         {
                             Value = i.EngClassId,
                             Text = i.EngClassDesc==null ? i.EngClassName : i.EngClassName + ": " + i.EngClassDesc,
                             Short = i.EngClassName
                         };

            var retVal = lookup.ToList();
            if (User.IsInRole("Admin"))
                retVal.Insert(0, new { Value = -1, Text = "(Manage Listing)", Short = "." });

            return DataSourceLoader.Load(retVal, loadOptions);
        }

        //IPF_Lookup
        [HttpGet]
        public object Ipf_Lookup(DataSourceLoadOptions loadOptions)
        {
            var lookup = from i in _context.Ipf
                         orderby i.IpfName
                         select new
                         {
                             Value = i.IpfId,
                             Text = i.IpfDesc==null? i.IpfName : i.IpfName + ": " + i.IpfDesc,
                             Short = i.IpfName
                         };

            var retVal = lookup.ToList();
            if (User.IsInRole("Admin"))
                retVal.Insert(0, new { Value = -1, Text = "(Manage Listing)", Short = "." });

            return DataSourceLoader.Load(retVal, loadOptions);
        }


        [HttpGet]
        public object TagView_Lookup(DataSourceLoadOptions loadOptions)
        {
            //TODO: Delete this? Check if used.
            var lookup = from i in _context.TagView
                         orderby i.TagViewName
                         select new
                         {
                             Value = i.TagViewId,
                             Text = i.TagViewName + ": " + i.TagViewColumns,
                             Short = i.TagViewName
                         };

            var retVal = lookup.ToList();
            if (User.IsInRole("Admin"))
                retVal.Insert(0, new { Value = -1, Text = "(Manage Listing)", Short = "." });

            return DataSourceLoader.Load(retVal, loadOptions);
        }

        #endregion

    }
}
