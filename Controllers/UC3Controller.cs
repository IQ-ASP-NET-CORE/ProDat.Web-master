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
    public class UC3Controller : Controller
    {
        #region instantiate controller
        private readonly TagContext _context;

        public UC3Controller(TagContext context)
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


        #region view Component data queries

        [HttpGet]
        public IActionResult FlocListingDataGridUC3_Reload(int Height, int Width)
        {
            return ViewComponent("FlocListingDataGridUC3", new { height = Height, width = Width });
        }

        [HttpGet]
        public object FlocListingDataGridUC3_GetData(DataSourceLoadOptions loadOptions)
        {
            var dataSet = _context.Tag
                          .Where(x=> x.MaintType.MaintTypeName == "M" || x.MaintType.MaintTypeName == "R")
                          .Where(x=> x.TagDeleted == false)
                          .Include(y=> y.FlocXmaintItems)
                          .OrderBy(x=> x.TagNumber)
                          .Select(x=> new FlocListingDataGridUC3ViewModel {
                              TagId = x.TagId,
                              TagFloc = x.TagFloc,
                              TagFlocDesc = x.TagFlocDesc,
                              RTF = x.RTF,
                              MaintPlannerGroupId = x.MaintPlannerGroupId,
                              MICount = x.FlocXmaintItems.Count
                          });

            return DataSourceLoader.Load(dataSet, loadOptions);
        }

        [Authorize(Roles = "User")]
        [HttpPost]
        public IActionResult FlocListingDataGridUC3_Insert(string values)
        {
            var newOrder = new ProDat.Web2.Models.Tag();
            JsonConvert.PopulateObject(values, newOrder);

            if (!TryValidateModel(newOrder))
                return BadRequest();

            _context.Tag.Add(newOrder);
            _context.SaveChanges();

            return Ok(newOrder);
        }

        [Authorize(Roles = "User")]
        [HttpPut]
        public IActionResult FlocListingDataGridUC3_Update(int key, string values)
        {
            // TODO override to update tag state.
            var order = _context.Tag.First(o => o.TagId == key);
            JsonConvert.PopulateObject(values, order);

            if (!TryValidateModel(order))
                return BadRequest();

            return Ok(order);
        }

        [Authorize(Roles = "User")]
        [HttpDelete]
        public void FlocListingDataGridUC3_Delete(int key)
        {
            var order = _context.Tag.First(o => o.TagId == key);
            _context.Tag.Remove(order);
            _context.SaveChanges();
        }

        [HttpGet]
        public IActionResult FlocInMIDataGridUC3_Reload(int Height, int Width, int parent)
        {
            return ViewComponent("FlocInMIDataGridUC3", new { height = Height, width = Width, parent = parent });
        }

        [HttpGet]
        public object FlocInMIDataGridUC3_GetData(DataSourceLoadOptions loadOptions, int parent)
        {
            var dataSet = _context.FlocXmaintItem
                          .Where(x=> x.MaintItemId == parent)
                          .Include(y => y.Floc)
                          .OrderBy(x=> x.Floc.TagNumber)
                          .Select(x => new FlocListingDataGridUC3ViewModel
                          {
                              TagId = x.FlocId,
                              TagFloc = x.Floc.TagFloc,
                              TagFlocDesc = x.Floc.TagFlocDesc,
                              RTF = x.Floc.RTF,
                              MaintPlannerGroupId = x.Floc.MaintPlannerGroupId
                          });

            return DataSourceLoader.Load(dataSet, loadOptions);
        }

        [Authorize(Roles = "User")]
        [HttpPost]
        public JsonResult FlocXmaintItem_Insert(int MiId, int TagId)
        {
            /*
             * On drag to MaintItem, insert this floc.
             * If this is the first floc to be associated with the MI (FlocXmaintItem count is 0):
             *   Update MaintItem.fMaintItemHeaderFloc to the Flocs TagNumber (Not ID, dont know why not...).
             */
            var FlocXMI = new ProDat.Web2.Models.FlocXmaintItem();

            FlocXMI.FlocId = TagId;
            FlocXMI.MaintItemId = MiId;

            if (TryValidateModel(FlocXMI))
            {
                _context.FlocXmaintItem.Add(FlocXMI);
                _context.SaveChanges();

            }

            // test if first floc.
            var FlocCount = _context.FlocXmaintItem
                            .Where(x => x.MaintItemId == MiId)
                            .Count();

            if (FlocCount == 1)
            {
                // Update MI.HeaderFlocId
                //var FlocNumber = _context.Tag
                //                 .Where(x => x.TagId == TagId)
                //                 .Select(x => x.TagFloc).FirstOrDefault();

                var Mi = _context.MaintItem
                         .Where(x => x.MaintItemId == MiId).FirstOrDefault();

                Mi.HeaderFlocId = TagId;
                _context.SaveChanges();
            }

            return Json("OK");
        }

        [Authorize(Roles = "User")]
        [HttpDelete]
        public JsonResult FlocXmaintItem_Delete(int MIId, int TagId)
        {
            var flocXMaintItem = _context.FlocXmaintItem
                                    .Where(x=> x.FlocId==TagId)
                                    .Where(x=> x.MaintItemId== MIId)
                                    .First();

            if (flocXMaintItem != null)
            {
                _context.FlocXmaintItem.Remove(flocXMaintItem);
                _context.SaveChanges();

                // test if Floc is header floc. If so, unset value.
                var mi = _context.MaintItem
                                    .Where(x => x.MaintItemId == MIId)
                                    .First();
                if (mi.HeaderFlocId == TagId)
                {
                    mi.HeaderFlocId = null;
                    _context.SaveChanges();
                }

                return Json("OK");
            }
            return Json("BAD");
        }

        [HttpGet]
        public object MaintenanceStrategiesDataGrid_GetData(DataSourceLoadOptions loadOptions)
        {
            var dataSet = _context.MaintStrategy
                          .OrderBy(x=> x.MaintStrategyName)
                          .Select(x=> new { x.MaintStrategyId, x.MaintStrategyName, x.MaintStrategyDesc });

            return DataSourceLoader.Load(dataSet, loadOptions);
        }

        [HttpGet]
        public IActionResult MaintenanceStrategiesDataGrid_Reload(int Height, int Width)
        {
            return ViewComponent("MaintenanceStrategiesDataGrid", new { height = Height, width = Width });
        }

        [HttpGet]
        public object MaintPlanDataGrid_GetData(DataSourceLoadOptions loadOptions)
        {
            var dataSet = _context.MaintPlan
                          //.Where(x=> x.MaintStrategyId == parent)
                          .OrderBy(x=> x.MaintPlanName)
                          .Select( rec=> new { rec.MaintPlanId, rec.MaintStrategyId, rec.MaintPlanName, rec.ShortText });

            return DataSourceLoader.Load(dataSet, loadOptions);
        }

        [HttpGet]
        public IActionResult MaintPlanDataGrid_Reload(int Height, int Width, int? Parent)
        {
            return ViewComponent("MaintPlanDataGrid", new { height = Height, width = Width, parent = Parent });
        }

        [HttpGet]
        public IActionResult MaintenanceItemsDataGridUC3_Reload(int Height, int Width)
        {
            return ViewComponent("MaintenanceItemsDataGridUC3", new { height = Height, width = Width });
        }

        [HttpGet]
        public object MaintenanceItemsDataGridUC3_GetData(DataSourceLoadOptions loadOptions)
        {
            var dataSet = _context.MaintItem
                          .Select( rec=> new { rec.MaintItemId, rec.MaintItemNum, rec.MaintItemShortText, rec.MaintPlanId  });

           return DataSourceLoader.Load(dataSet, loadOptions);
        }

        [HttpGet]
        public object TaskListOperationsDataGrid_GetData(DataSourceLoadOptions loadOptions, int? parent)
        {
            if (parent == null)
                parent = 0;
            var dataSet = _context.TaskListOperations
                          .Where (rec=> rec.TaskListHeaderId == parent)
                          .OrderBy(x => x.OperationShortText)
                          .Select(rec=> new { rec.TaskListHeaderId, rec.TaskListOperationId, rec.OperationShortText, rec.OperationNum, rec.DocId, rec.MaintWorkCentreId });

            return DataSourceLoader.Load(dataSet, loadOptions);
        }

        [HttpGet]
        public IActionResult TaskListOperationsDataGrid_Reload(int Height, int Width, int? Parent)
        {
            return ViewComponent("TaskListOperationsDataGrid", new { height = Height, width = Width, parent = Parent });
        }

        [HttpGet]
        public IActionResult TaskListHeader_Reload(int Height, int Width)
        {
            return ViewComponent("TaskListHeader", new { height = Height, width = Width });
        }

        [HttpGet]
        public IActionResult TaskListHeaderMasterDetail_Reload(int Height, int Width)
        {
            return ViewComponent("TaskListHeaderMasterDetail", new { height = Height, width = Width });
        }

        [HttpGet]
        public object TaskListHeader_GetData(DataSourceLoadOptions loadOptions)
        {
            var data = _context.MaintItemXmaintTaskListHeader
                            .Include(x=> x.TaskListHeader)
                            .Include(x=> x.MaintItem)
                            //.OrderBy(x=> x.TaskListHeader.TaskListGroup.TaskListGroupName)
                            .OrderBy(x => x.TaskListHeader.Counter)
                            .Select(x=> new TaskListHeader_UC3v2 {
                                                TaskListHeader = new Models.TaskListHeader
                                                {
                                                    TaskListHeaderId = x.TaskListHeaderId,
                                                    TaskListGroupId = x.TaskListHeader.TaskListGroupId,
                                                    Counter = x.TaskListHeader.Counter,
                                                    TaskListShortText = x.TaskListHeader.TaskListShortText,
                                                    MaintWorkCentreId = x.TaskListHeader.MaintWorkCentreId,
                                                    MaintPlannerGroupId = x.TaskListHeader.MaintPlannerGroupId,
                                                    MaintenancePlantId = x.TaskListHeader.MaintenancePlantId,
                                                    MaintStrategyId = x.TaskListHeader.MaintStrategyId,
                                                    SysCondId = x.TaskListHeader.SysCondId,
                                                    MaintPackageId = x.TaskListHeader.MaintPackageId,
                                                    PmassemblyId = x.TaskListHeader.PmassemblyId,
                                                    TasklistCatId = x.TaskListHeader.TasklistCatId,
                                                    PerformanceStandardId = x.TaskListHeader.PerformanceStandardId,
                                                    PerfStdAppDel = x.TaskListHeader.PerfStdAppDel,
                                                    ScePsReviewId = x.TaskListHeader.ScePsReviewId,
                                                    RegulatoryBodyId = x.TaskListHeader.RegulatoryBodyId,
                                                    RegBodyAppDel = x.TaskListHeader.RegBodyAppDel,
                                                    ChangeRequired = x.TaskListHeader.ChangeRequired,
                                                    TaskListClassId = x.TaskListHeader.TaskListClassId,
                                                    StatusId = x.TaskListHeader.StatusId
                                                },
                                                MaintPlanId = (int)x.MaintItem.MaintPlanId,
                                                MaintItemId = x.MaintItemId,
                                           }
                            );

            return DataSourceLoader.Load(data, loadOptions);
        }

        [HttpGet]
        public object TaskListOperations_GetData(int TaskListHeaderId, DataSourceLoadOptions loadOptions)
        {
                var TLOs = _context.TaskListOperations
                            .Where(e => e.TaskListHeaderId == TaskListHeaderId);
                return DataSourceLoader.Load(TLOs, loadOptions);
        }

        [HttpGet]
        public IActionResult MaintenanceStrategiesForm_Reload(int Height, int Width, int? Parent)
        {
            return ViewComponent("MaintenanceStrategiesForm", new { height = Height, width = Width, parent = Parent });
        }

        [HttpGet]
        public IActionResult MaintPlanForm_Reload(int Height, int Width, int? Parent)
        {
            return ViewComponent("MaintPlanForm", new { height = Height, width = Width, parent = Parent });
        }

        [HttpGet]
        public IActionResult MaintItemForm_Reload(int Height, int Width, int? Parent)
        {
            return ViewComponent("MaintItemForm", new { height = Height, width = Width, parent = Parent });
        }

        [HttpGet]
        public IActionResult TaskListHeaderForm_Reload(int Height, int Width, int? Parent)
        {
            return ViewComponent("TaskListHeaderForm", new { height = Height, width = Width, parent = Parent });
        }

        #endregion


        #region Ajax updating

        [Authorize(Roles = "User")]
        [HttpPost]
        public async Task<IActionResult> MaintStratUpdate(string sId, string attributeName, string newValue)
        {
            // retrieve tag to update.
            var record = await _context.MaintStrategy.FindAsync(int.Parse(sId));
            if (record == null)
            {
                return BadRequest();
            }

            //determine attribute type being set
            PropertyInfo myProp = record.GetType().GetProperty(attributeName);
            var oldValue = myProp.GetValue(record) ?? "Null";

            string propertyType = myProp.PropertyType.Name;

            if (propertyType != "String" && propertyType != "Boolean" && propertyType != "DateTime")
            {
                // need to find a way to get type without looking everywhere.
                propertyType = myProp.PropertyType.GenericTypeArguments[0].Name;
            }

            // prepare Validation context for attribute.
            ValidationContext vc = new ValidationContext(record, null, null);
            vc.MemberName = attributeName;
            ICollection<ValidationResult> results = new List<ValidationResult>();

            // Validate and set property, or return badRequest
            if (propertyType == "Int32")
            {
                int? intNewValue = null;
                if (newValue == "-1")
                {
                    intNewValue = null;
                }
                else
                {
                    intNewValue = int.Parse(newValue);
                }
                bool Valid = Validator.TryValidateProperty(intNewValue, vc, results);
                if (Valid)
                {
                    myProp.SetValue(record, intNewValue, null);
                }
                else
                {
                    return BadRequest();
                }
            }
            else if (propertyType == "String")
            {
                bool Valid = Validator.TryValidateProperty(newValue, vc, results);
                if (Valid)
                {
                    myProp.SetValue(record, newValue, null);
                }
                else
                {
                    return Ok();
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
                    myProp.SetValue(record, setValue, null);
                }
            }
            else if (propertyType == "DateTime")
            {
                // 'Tue Jan 08 1901 00:00:00 GMT+0800 (W. Australia Standard Time)'
                // ugly.. see if any built in tools for this.
                var converted = DateTime.Parse(newValue.Substring(4,11));
                bool Valid = Validator.TryValidateProperty(converted, vc, results);
                if (Valid)
                {
                    myProp.SetValue(record, converted, null);
                }
                else
                {
                    return Ok();
                }
            }
            else
            {
                return BadRequest();
            }


            var Created = DateTime.UtcNow;
            var CreatedBy = User.Identity.Name;

            _context.Historian.Add(
                        new Historian
                        {
                            AttributeName = attributeName,
                            AttributeValue = newValue,
                            EntityName = 3,
                            Pk1 = int.Parse(sId),
                            CreatedBy = CreatedBy,
                            Created = Created
                        }
                          );

            await _context.SaveChangesAsync();

            // Apply Historian + user date etc.


            // return 200.
            return Ok(new { message = "Success" });

        }

        [Authorize(Roles = "User")]
        [HttpPost]
        public async Task<IActionResult> MaintPlanUpdate(string sId, string attributeName, string newValue)
        {
            // retrieve tag to update.
            var record = await _context.MaintPlan.FindAsync(int.Parse(sId));
            if (record == null)
            {
                return BadRequest();
            }

            //determine attribute type being set
            PropertyInfo myProp = record.GetType().GetProperty(attributeName);
            var oldValue = myProp.GetValue(record) ?? "Null";

            string propertyType = myProp.PropertyType.Name;

            if (propertyType != "String" && propertyType != "Boolean" && propertyType != "DateTime")
            {
                // need to find a way to get type without looking everywhere.
                propertyType = myProp.PropertyType.GenericTypeArguments[0].Name;
            }

            // prepare Validation context for attribute.
            ValidationContext vc = new ValidationContext(record, null, null);
            vc.MemberName = attributeName;
            ICollection<ValidationResult> results = new List<ValidationResult>();

            // Validate and set property, or return badRequest
            if (propertyType == "Int32")
            {
                int? intNewValue = null;
                if (newValue == "-1")
                {
                    intNewValue = null;
                }
                else
                {
                    intNewValue = int.Parse(newValue);
                }
                bool Valid = Validator.TryValidateProperty(intNewValue, vc, results);
                if (Valid)
                {
                    myProp.SetValue(record, intNewValue, null);
                }
                else
                {
                    return BadRequest();
                }
            }
            else if (propertyType == "Double")
            {
                double? intNewValue = null;
                if (newValue == "-1")
                {
                    intNewValue = null;
                }
                else
                {
                    intNewValue = double.Parse(newValue);
                }
                bool Valid = Validator.TryValidateProperty(intNewValue, vc, results);
                if (Valid)
                {
                    myProp.SetValue(record, intNewValue, null);
                }
                else
                {
                    return BadRequest();
                }
            }
            else if (propertyType == "String")
            {
                bool Valid = Validator.TryValidateProperty(newValue, vc, results);
                if (Valid)
                {
                    myProp.SetValue(record, newValue, null);
                }
                else
                {
                    return Ok();
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
                    myProp.SetValue(record, setValue, null);
                }
            }
            else if (propertyType == "DateTime")
            {
                // 'Tue Jan 08 1901 00:00:00 GMT+0800 (W. Australia Standard Time)'
                // ugly.. see if any built in tools for this.
                var converted = DateTime.Parse(newValue.Substring(4, 11));
                bool Valid = Validator.TryValidateProperty(converted, vc, results);
                if (Valid)
                {
                    myProp.SetValue(record, converted, null);
                }
                else
                {
                    return Ok();
                }
            }
            else
            {
                return BadRequest();
            }


            var Created = DateTime.UtcNow;
            var CreatedBy = User.Identity.Name;

            _context.Historian.Add(
                        new Historian
                        {
                            AttributeName = attributeName,
                            AttributeValue = newValue,
                            EntityName = 5,
                            Pk1 = int.Parse(sId),
                            CreatedBy = CreatedBy,
                            Created = Created
                        }
                          );

            await _context.SaveChangesAsync();

            // Apply Historian + user date etc.

            // return 200.
            return Ok(new { message = "Success" });

        }

        [Authorize(Roles = "User")]
        [HttpPost]
        public async Task<IActionResult> MaintItemUpdate(string sId, string attributeName, string newValue)
        {
            // retrieve tag to update.
            var record = await _context.MaintItem.FindAsync(int.Parse(sId));
            if (record == null)
            {
                return BadRequest();
            }

            //determine attribute type being set
            PropertyInfo myProp = record.GetType().GetProperty(attributeName);
            var oldValue = myProp.GetValue(record) ?? "Null";

            string propertyType = myProp.PropertyType.Name;

            if (propertyType != "String" && propertyType != "Boolean" && propertyType != "DateTime" && propertyType != "Int32")
            {
                // need to find a way to get type without looking everywhere.
                propertyType = myProp.PropertyType.GenericTypeArguments[0].Name;
            }

            // prepare Validation context for attribute.
            ValidationContext vc = new ValidationContext(record, null, null);
            vc.MemberName = attributeName;
            ICollection<ValidationResult> results = new List<ValidationResult>();

            // Validate and set property, or return badRequest
            if (propertyType == "Int32")
            {
                int? intNewValue = null;
                if (newValue == "-1")
                {
                    intNewValue = null;
                }
                else
                {
                    intNewValue = int.Parse(newValue);
                }
                bool Valid = Validator.TryValidateProperty(intNewValue, vc, results);
                if (Valid)
                {
                    myProp.SetValue(record, intNewValue, null);
                }
                else
                {
                    return BadRequest();
                }
            }
            else if (propertyType == "String")
            {
                bool Valid = Validator.TryValidateProperty(newValue, vc, results);
                if (Valid)
                {
                    myProp.SetValue(record, newValue, null);
                }
                else
                {
                    return Ok();
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
                    myProp.SetValue(record, setValue, null);
                }
            }
            else if (propertyType == "DateTime")
            {
                // 'Tue Jan 08 1901 00:00:00 GMT+0800 (W. Australia Standard Time)'
                // ugly.. see if any built in tools for this.
                var converted = DateTime.Parse(newValue.Substring(4, 11));
                bool Valid = Validator.TryValidateProperty(converted, vc, results);
                if (Valid)
                {
                    myProp.SetValue(record, converted, null);
                }
                else
                {
                    return Ok();
                }
            }
            else
            {
                return BadRequest();
            }

            var Created = DateTime.UtcNow;
            var CreatedBy = User.Identity.Name;

            _context.Historian.Add(
                        new Historian
                        {
                            AttributeName = attributeName,
                            AttributeValue = newValue,
                            EntityName = 3,
                            Pk1 = int.Parse(sId),
                            CreatedBy = CreatedBy,
                            Created = Created
                        }
                          );

            await _context.SaveChangesAsync();

            // Apply Historian + user date etc.

            // return 200.


            return Ok(new { message = "Success" });

        }

        [Authorize(Roles = "User")]
        [HttpPost]
        public async Task<IActionResult> TaskListHeaderUpdate(string sId, string attributeName, string newValue)
        {
            // retrieve tag to update.
            var record = await _context.TaskListHeader.FindAsync(int.Parse(sId));
            if (record == null)
            {
                return BadRequest();
            }

            //determine attribute type being set
            PropertyInfo myProp = record.GetType().GetProperty(attributeName);
            var oldValue = myProp.GetValue(record) ?? "Null";

            string propertyType = myProp.PropertyType.Name;

            if (propertyType != "String" && propertyType != "Boolean" && propertyType != "DateTime" && propertyType != "Int32")
            {
                // need to find a way to get type without looking everywhere.
                propertyType = myProp.PropertyType.GenericTypeArguments[0].Name;
            }

            // prepare Validation context for attribute.
            ValidationContext vc = new ValidationContext(record, null, null);
            vc.MemberName = attributeName;
            ICollection<ValidationResult> results = new List<ValidationResult>();

            // Validate and set property, or return badRequest
            if (propertyType == "Int32")
            {
                int? intNewValue = null;
                if (newValue == "-1")
                {
                    intNewValue = null;
                }
                else
                {
                    intNewValue = int.Parse(newValue);
                }
                bool Valid = Validator.TryValidateProperty(intNewValue, vc, results);
                if (Valid)
                {
                    myProp.SetValue(record, intNewValue, null);
                }
                else
                {
                    return BadRequest();
                }
            }
            else if (propertyType == "String")
            {
                bool Valid = Validator.TryValidateProperty(newValue, vc, results);
                if (Valid)
                {
                    myProp.SetValue(record, newValue, null);
                }
                else
                {
                    return Ok();
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
                    myProp.SetValue(record, setValue, null);
                }
            }
            else if (propertyType == "DateTime")
            {
                // 'Tue Jan 08 1901 00:00:00 GMT+0800 (W. Australia Standard Time)'
                // ugly.. see if any built in tools for this.
                var converted = DateTime.Parse(newValue.Substring(4, 11));
                bool Valid = Validator.TryValidateProperty(converted, vc, results);
                if (Valid)
                {
                    myProp.SetValue(record, converted, null);
                }
                else
                {
                    return Ok();
                }
            }
            else
            {
                return BadRequest();
            }

            await _context.SaveChangesAsync();

            // Apply Historian + user date etc.

            // return 200.
            var Created = DateTime.UtcNow;
            var CreatedBy = User.Identity.Name;

            _context.Historian.Add(
                        new Historian
                        {
                            AttributeName = attributeName,
                            AttributeValue = newValue,
                            EntityName = 2,
                            Pk1 = int.Parse(sId),
                            CreatedBy = CreatedBy,
                            Created = Created
                        }
                          );
            _context.SaveChanges();

            return Ok(new { message = "Success" });

        }

        #endregion


    } // controller
}
