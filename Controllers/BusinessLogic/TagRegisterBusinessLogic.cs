using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations.Operations;
using ProDat.Web2.Data;
using ProDat.Web2.Models;
using ProDat.Web2.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ProDat.Web2.Controllers.TagRegister
{
    public class TagRegisterBusinessLogic
    {
        private TagContext _context;

        public TagRegisterBusinessLogic(TagContext context)
        {
            _context = context;
        }

        public IQueryable<Tag> GetTags(TagRegisterSearchViewModel searchModel)
        {
            //var result = _context.Tag.AsQueryable();
            var result = (from t in _context.Tag
                                //.Include(t => t.EngParent)
                                .Include(t => t.EngDisc)
                                .Include(t => t.MaintCriticality)
                                .Include(t => t.MaintLocation)
                                .Include(t => t.MaintObjectType)
                                .Include(t => t.SubSystem)
                                .Include(t => t.SubSystem.Systems)
                                .Include(t => t.MaintWorkCentre)
                                // costly? to test and confirm.
                                .Include(t => t.MaintParent)
                                .Include(t => t.MaintStructureIndicator)
                                .Include(t => t.PerformanceStandard)
                                .Include(t => t.ExMethod)
                          select t).AsQueryable();

            if (searchModel != null)
            {
                if (!string.IsNullOrEmpty(searchModel.TagNumber))
                {
                    // see TokenisedQuery for use.
                    TokenisedQuery dicSearch = new TokenisedQuery(searchModel.TagNumber);
                    var Token = dicSearch.getToken();

                    if (Token.ContainsKey("StartsWith"))
                        result = result.Where(x => x.TagNumber.StartsWith(Token["StartsWith"].First()));
                    if (Token.ContainsKey("EndsWith"))
                        result = result.Where(x => x.TagNumber.EndsWith(Token["EndsWith"].First()));
                    if (Token.ContainsKey("Contains"))
                    {
                        foreach(string token in Token["Contains"])
                        {
                            result = result.Where(x => x.TagNumber.Contains(token));
                        }
                    }
                }
                if (!string.IsNullOrEmpty(searchModel.TagFloc))
                {
                    TokenisedQuery dicSearch = new TokenisedQuery(searchModel.TagFloc);
                    var Token = dicSearch.getToken();

                    if (Token.ContainsKey("StartsWith"))
                        result = result.Where(x => x.TagFloc.StartsWith(Token["StartsWith"].First()));
                    if (Token.ContainsKey("EndsWith"))
                        result = result.Where(x => x.TagFloc.EndsWith(Token["EndsWith"].First()));
                    if (Token.ContainsKey("Contains"))
                    {
                        foreach (string token in Token["Contains"])
                        {
                            result = result.Where(x => x.TagFloc.Contains(token));
                        }
                    }
                }
                if (!string.IsNullOrEmpty(searchModel.TagFlocDesc))
                {
                    TokenisedQuery dicSearch = new TokenisedQuery(searchModel.TagFlocDesc);
                    var Token = dicSearch.getToken();

                    if (Token.ContainsKey("StartsWith"))
                        result = result.Where(x => x.TagFlocDesc.StartsWith(Token["StartsWith"].First()));
                    if (Token.ContainsKey("EndsWith"))
                        result = result.Where(x => x.TagFlocDesc.EndsWith(Token["EndsWith"].First()));
                    if (Token.ContainsKey("Contains"))
                    {
                        foreach (string token in Token["Contains"])
                        {
                            result = result.Where(x => x.TagFlocDesc.Contains(token));
                        }
                    }
                }
                if (!string.IsNullOrEmpty(searchModel.TagService))
                {
                    TokenisedQuery dicSearch = new TokenisedQuery(searchModel.TagService);
                    var Token = dicSearch.getToken();

                    if (Token.ContainsKey("StartsWith"))
                        result = result.Where(x => x.TagService.StartsWith(Token["StartsWith"].First()));
                    if (Token.ContainsKey("EndsWith"))
                        result = result.Where(x => x.TagService.EndsWith(Token["EndsWith"].First()));
                    if (Token.ContainsKey("Contains"))
                    {
                        foreach (string token in Token["Contains"])
                        {
                            result = result.Where(x => x.TagService.Contains(token));
                        }
                    }
                }
                if (!string.IsNullOrEmpty(searchModel.TagSource))
                {
                    TokenisedQuery dicSearch = new TokenisedQuery(searchModel.TagSource);
                    var Token = dicSearch.getToken();

                    if (Token.ContainsKey("StartsWith"))
                        result = result.Where(x => x.TagSource.StartsWith(Token["StartsWith"].First()));
                    if (Token.ContainsKey("EndsWith"))
                        result = result.Where(x => x.TagSource.EndsWith(Token["EndsWith"].First()));
                    if (Token.ContainsKey("Contains"))
                    {
                        foreach (string token in Token["Contains"])
                        {
                            result = result.Where(x => x.TagSource.Contains(token));
                        }
                    }
                }
                if (!string.IsNullOrEmpty(searchModel.TagComment))
                {
                    TokenisedQuery dicSearch = new TokenisedQuery(searchModel.TagComment);
                    var Token = dicSearch.getToken();

                    if (Token.ContainsKey("StartsWith"))
                        result = result.Where(x => x.TagComment.StartsWith(Token["StartsWith"].First()));
                    if (Token.ContainsKey("EndsWith"))
                        result = result.Where(x => x.TagComment.EndsWith(Token["EndsWith"].First()));
                    if (Token.ContainsKey("Contains"))
                    {
                        foreach (string token in Token["Contains"])
                        {
                            result = result.Where(x => x.TagComment.Contains(token));
                        }
                    }
                }
                if (!string.IsNullOrEmpty(searchModel.TagVendorTag))
                {
                    TokenisedQuery dicSearch = new TokenisedQuery(searchModel.TagVendorTag);
                    var Token = dicSearch.getToken();

                    if (Token.ContainsKey("StartsWith"))
                        result = result.Where(x => x.TagVendorTag.StartsWith(Token["StartsWith"].First()));
                    if (Token.ContainsKey("EndsWith"))
                        result = result.Where(x => x.TagVendorTag.EndsWith(Token["EndsWith"].First()));
                    if (Token.ContainsKey("Contains"))
                    {
                        foreach (string token in Token["Contains"])
                        {
                            result = result.Where(x => x.TagVendorTag.Contains(token));
                        }
                    }
                }
                if (!string.IsNullOrEmpty(searchModel.TagRawNumber))
                {
                    TokenisedQuery dicSearch = new TokenisedQuery(searchModel.TagRawNumber);
                    var Token = dicSearch.getToken();

                    if (Token.ContainsKey("StartsWith"))
                        result = result.Where(x => x.TagRawNumber.StartsWith(Token["StartsWith"].First()));
                    if (Token.ContainsKey("EndsWith"))
                        result = result.Where(x => x.TagRawNumber.EndsWith(Token["EndsWith"].First()));
                    if (Token.ContainsKey("Contains"))
                    {
                        foreach (string token in Token["Contains"])
                        {
                            result = result.Where(x => x.TagRawNumber.Contains(token));
                        }
                    }
                }
                if (!string.IsNullOrEmpty(searchModel.TagRawDesc))
                {
                    TokenisedQuery dicSearch = new TokenisedQuery(searchModel.TagRawDesc);
                    var Token = dicSearch.getToken();

                    if (Token.ContainsKey("StartsWith"))
                        result = result.Where(x => x.TagRawDesc.StartsWith(Token["StartsWith"].First()));
                    if (Token.ContainsKey("EndsWith"))
                        result = result.Where(x => x.TagRawDesc.EndsWith(Token["EndsWith"].First()));
                    if (Token.ContainsKey("Contains"))
                    {
                        foreach (string token in Token["Contains"])
                        {
                            result = result.Where(x => x.TagRawDesc.Contains(token));
                        }
                    }
                }
                if (!string.IsNullOrEmpty(searchModel.MaintScePsJustification))
                {
                    TokenisedQuery dicSearch = new TokenisedQuery(searchModel.MaintScePsJustification);
                    var Token = dicSearch.getToken();

                    if (Token.ContainsKey("StartsWith"))
                        result = result.Where(x => x.MaintScePsJustification.StartsWith(Token["StartsWith"].First()));
                    if (Token.ContainsKey("EndsWith"))
                        result = result.Where(x => x.MaintScePsJustification.EndsWith(Token["EndsWith"].First()));
                    if (Token.ContainsKey("Contains"))
                    {
                        foreach (string token in Token["Contains"])
                        {
                            result = result.Where(x => x.MaintScePsJustification.Contains(token));
                        }
                    }
                }
                if (!string.IsNullOrEmpty(searchModel.TagMaintCritComments))
                {
                    TokenisedQuery dicSearch = new TokenisedQuery(searchModel.TagMaintCritComments);
                    var Token = dicSearch.getToken();

                    if (Token.ContainsKey("StartsWith"))
                        result = result.Where(x => x.TagMaintCritComments.StartsWith(Token["StartsWith"].First()));
                    if (Token.ContainsKey("EndsWith"))
                        result = result.Where(x => x.TagMaintCritComments.EndsWith(Token["EndsWith"].First()));
                    if (Token.ContainsKey("Contains"))
                    {
                        foreach (string token in Token["Contains"])
                        {
                            result = result.Where(x => x.TagMaintCritComments.Contains(token));
                        }
                    }
                }
                if (!string.IsNullOrEmpty(searchModel.TagRbmMethod))
                {
                    TokenisedQuery dicSearch = new TokenisedQuery(searchModel.TagRbmMethod);
                    var Token = dicSearch.getToken();

                    if (Token.ContainsKey("StartsWith"))
                        result = result.Where(x => x.TagRbmMethod.StartsWith(Token["StartsWith"].First()));
                    if (Token.ContainsKey("EndsWith"))
                        result = result.Where(x => x.TagRbmMethod.EndsWith(Token["EndsWith"].First()));
                    if (Token.ContainsKey("Contains"))
                    {
                        foreach (string token in Token["Contains"])
                        {
                            result = result.Where(x => x.TagRbmMethod.Contains(token));
                        }
                    }
                }
                if (!string.IsNullOrEmpty(searchModel.TagVib))
                {
                    TokenisedQuery dicSearch = new TokenisedQuery(searchModel.TagVib);
                    var Token = dicSearch.getToken();

                    if (Token.ContainsKey("StartsWith"))
                        result = result.Where(x => x.TagVib.StartsWith(Token["StartsWith"].First()));
                    if (Token.ContainsKey("EndsWith"))
                        result = result.Where(x => x.TagVib.EndsWith(Token["EndsWith"].First()));
                    if (Token.ContainsKey("Contains"))
                    {
                        foreach (string token in Token["Contains"])
                        {
                            result = result.Where(x => x.TagVib.Contains(token));
                        }
                    }
                }
                if (!string.IsNullOrEmpty(searchModel.TagSrcKeyList))
                {
                    TokenisedQuery dicSearch = new TokenisedQuery(searchModel.TagSrcKeyList);
                    var Token = dicSearch.getToken();

                    if (Token.ContainsKey("StartsWith"))
                        result = result.Where(x => x.TagSrcKeyList.StartsWith(Token["StartsWith"].First()));
                    if (Token.ContainsKey("EndsWith"))
                        result = result.Where(x => x.TagSrcKeyList.EndsWith(Token["EndsWith"].First()));
                    if (Token.ContainsKey("Contains"))
                    {
                        foreach (string token in Token["Contains"])
                        {
                            result = result.Where(x => x.TagSrcKeyList.Contains(token));
                        }
                    }
                }
                if (!string.IsNullOrEmpty(searchModel.TagBomReq))
                {
                    TokenisedQuery dicSearch = new TokenisedQuery(searchModel.TagBomReq);
                    var Token = dicSearch.getToken();

                    if (Token.ContainsKey("StartsWith"))
                        result = result.Where(x => x.TagBomReq.StartsWith(Token["StartsWith"].First()));
                    if (Token.ContainsKey("EndsWith"))
                        result = result.Where(x => x.TagBomReq.EndsWith(Token["EndsWith"].First()));
                    if (Token.ContainsKey("Contains"))
                    {
                        foreach (string token in Token["Contains"])
                        {
                            result = result.Where(x => x.TagBomReq.Contains(token));
                        }
                    }
                }
                if (!string.IsNullOrEmpty(searchModel.TagSpNo))
                {
                    TokenisedQuery dicSearch = new TokenisedQuery(searchModel.TagSpNo);
                    var Token = dicSearch.getToken();

                    if (Token.ContainsKey("StartsWith"))
                        result = result.Where(x => x.TagSpNo.StartsWith(Token["StartsWith"].First()));
                    if (Token.ContainsKey("EndsWith"))
                        result = result.Where(x => x.TagSpNo.EndsWith(Token["EndsWith"].First()));
                    if (Token.ContainsKey("Contains"))
                    {
                        foreach (string token in Token["Contains"])
                        {
                            result = result.Where(x => x.TagSpNo.Contains(token));
                        }
                    }
                }
                if (!string.IsNullOrEmpty(searchModel.TagCharacteristic))
                {
                    TokenisedQuery dicSearch = new TokenisedQuery(searchModel.TagCharacteristic);
                    var Token = dicSearch.getToken();

                    if (Token.ContainsKey("StartsWith"))
                        result = result.Where(x => x.TagCharacteristic.StartsWith(Token["StartsWith"].First()));
                    if (Token.ContainsKey("EndsWith"))
                        result = result.Where(x => x.TagCharacteristic.EndsWith(Token["EndsWith"].First()));
                    if (Token.ContainsKey("Contains"))
                    {
                        foreach (string token in Token["Contains"])
                        {
                            result = result.Where(x => x.TagCharacteristic.Contains(token));
                        }
                    }
                }
                if (!string.IsNullOrEmpty(searchModel.TagCharValue))
                {
                    TokenisedQuery dicSearch = new TokenisedQuery(searchModel.TagCharValue);
                    var Token = dicSearch.getToken();

                    if (Token.ContainsKey("StartsWith"))
                        result = result.Where(x => x.TagCharValue.StartsWith(Token["StartsWith"].First()));
                    if (Token.ContainsKey("EndsWith"))
                        result = result.Where(x => x.TagCharValue.EndsWith(Token["EndsWith"].First()));
                    if (Token.ContainsKey("Contains"))
                    {
                        foreach (string token in Token["Contains"])
                        {
                            result = result.Where(x => x.TagCharValue.Contains(token));
                        }
                    }
                }
                if (!string.IsNullOrEmpty(searchModel.TagCharDesc))
                {
                    TokenisedQuery dicSearch = new TokenisedQuery(searchModel.TagCharDesc);
                    var Token = dicSearch.getToken();

                    if (Token.ContainsKey("StartsWith"))
                        result = result.Where(x => x.TagCharDesc.StartsWith(Token["StartsWith"].First()));
                    if (Token.ContainsKey("EndsWith"))
                        result = result.Where(x => x.TagCharDesc.EndsWith(Token["EndsWith"].First()));
                    if (Token.ContainsKey("Contains"))
                    {
                        foreach (string token in Token["Contains"])
                        {
                            result = result.Where(x => x.TagCharDesc.Contains(token));
                        }
                    }
                }

                // ####################
                // bool value searches
                if (searchModel.TagMaintQuery != null)
                {
                    result = result.Where(x => x.TagMaintQuery.Equals(searchModel.TagMaintQuery));
                }
                if (searchModel.Tagnoneng != null)
                {
                    // bool value search
                    result = result.Where(x => x.Tagnoneng.Equals(searchModel.Tagnoneng));
                }

                if (searchModel.TagDeleted == null)
                {
                    // exclude deleted (default behaviour)
                    result = result.Where(x => x.TagDeleted.Equals(false));
                }
                else
                {
                    // include deleted
                    if (searchModel.TagDeleted == false)
                    {
                        result = result.Where(x => x.TagDeleted.Equals(true));
                    }
                    // deleted only (requires no criteria, so omitted)
                }



                // #######################
                // STAR Searches
                if (searchModel.EngDiscId != null)
                {
                    TokenisedQuery dicSearch = new TokenisedQuery(searchModel.EngDiscId);
                    var Token = dicSearch.getToken();

                    if (Token.ContainsKey("StartsWith"))
                        result = result.Where(x => x.EngDisc.EngDiscName.StartsWith(Token["StartsWith"].First()));
                    if (Token.ContainsKey("EndsWith"))
                        result = result.Where(x => x.EngDisc.EngDiscName.EndsWith(Token["EndsWith"].First()));
                    if (Token.ContainsKey("Contains"))
                    {
                        foreach (string token in Token["Contains"])
                        {
                            result = result.Where(x => x.EngDisc.EngDiscName.Contains(token));
                        }
                    }
                }
                if (searchModel.EngParentId != null)
                {
                    //result = result.Where(x => x.EngParent.TagNumber.Contains(searchModel.EngParentId));

                    // see TokenisedQuery for use.
                    TokenisedQuery dicSearch = new TokenisedQuery(searchModel.EngParentId);
                    var Token = dicSearch.getToken();

                    if (Token.ContainsKey("StartsWith"))
                        result = result.Where(x => x.EngParent.TagNumber.StartsWith(Token["StartsWith"].First()));
                    if (Token.ContainsKey("EndsWith"))
                        result = result.Where(x => x.EngParent.TagNumber.EndsWith(Token["EndsWith"].First()));
                    if (Token.ContainsKey("Contains"))
                    {
                        foreach (string token in Token["Contains"])
                        {
                            result = result.Where(x => x.EngParent.TagNumber.Contains(token));
                        }
                    }

                }
                if (searchModel.MaintLocationId != null)
                {
                    // see TokenisedQuery for use.
                    TokenisedQuery dicSearch = new TokenisedQuery(searchModel.MaintLocationId);
                    var Token = dicSearch.getToken();

                    if (Token.ContainsKey("StartsWith"))
                        result = result.Where(x => x.MaintLocation.MaintLocationName.StartsWith(Token["StartsWith"].First()));
                    if (Token.ContainsKey("EndsWith"))
                        result = result.Where(x => x.MaintLocation.MaintLocationName.EndsWith(Token["EndsWith"].First()));
                    if (Token.ContainsKey("Contains"))
                    {
                        foreach (string token in Token["Contains"])
                        {
                            result = result.Where(x => x.MaintLocation.MaintLocationName.Contains(token));
                        }
                    }

                    // TODO (MAYBE)?
                    // if above filters to 0 records, search description instead. 
                    // e.g. someone types Storage into Maint Loc, return all descriptions containing 'storage'?

                }
                if (searchModel.MaintCriticalityId != null)
                {
                    // left as is, as only options are A|B|C.
                    result = result.Where(x => x.MaintCriticality.MaintCriticalityName.Contains(searchModel.MaintCriticalityId));
                }
                if (searchModel.MaintObjectTypeId != null)
                {
                    //result = result.Where(x => x.MaintObjectType.MaintObjectTypeName.Contains(searchModel.MaintObjectTypeId));
                    // see TokenisedQuery for use.
                    TokenisedQuery dicSearch = new TokenisedQuery(searchModel.MaintObjectTypeId);
                    var Token = dicSearch.getToken();

                    if (Token.ContainsKey("StartsWith"))
                        result = result.Where(x => x.MaintObjectType.MaintObjectTypeName.StartsWith(Token["StartsWith"].First()));
                    if (Token.ContainsKey("EndsWith"))
                        result = result.Where(x => x.MaintObjectType.MaintObjectTypeName.EndsWith(Token["EndsWith"].First()));
                    if (Token.ContainsKey("Contains"))
                    {
                        foreach (string token in Token["Contains"])
                        {
                            result = result.Where(x => x.MaintObjectType.MaintObjectTypeName.Contains(token));
                        }
                    }
                }
                if(searchModel.SubSystemId != null)
                {
                    //result = result.Where(x => x.SubSystem.SubSystemName.Contains(searchModel.SubSystemId));
                    // see TokenisedQuery for use.
                    TokenisedQuery dicSearch = new TokenisedQuery(searchModel.SubSystemId);
                    var Token = dicSearch.getToken();

                    if (Token.ContainsKey("StartsWith"))
                        result = result.Where(x => x.SubSystem.SubSystemNum.StartsWith(Token["StartsWith"].First()));
                    if (Token.ContainsKey("EndsWith"))
                        result = result.Where(x => x.SubSystem.SubSystemNum.EndsWith(Token["EndsWith"].First()));
                    if (Token.ContainsKey("Contains"))
                    {
                        foreach (string token in Token["Contains"])
                        {
                            result = result.Where(x => x.SubSystem.SubSystemNum.Contains(token));
                        }
                    }
                }
                if (searchModel.SystemId != null)
                {
                    result = result.Where(x => x.SubSystem.Systems.SystemNum.Contains(searchModel.SystemId));
                }
                if (searchModel.EngClassId != null)
                {
                    result = result.Where(x => x.EngClass.EngClassName.Contains(searchModel.EngClassId));
                }
                if (searchModel.MaintParentId != null)
                {
                    TokenisedQuery dicSearch = new TokenisedQuery(searchModel.MaintParentId);
                    var Token = dicSearch.getToken();

                    if (Token.ContainsKey("StartsWith"))
                        result = result.Where(x => x.MaintParent.TagNumber.StartsWith(Token["StartsWith"].First()));
                    if (Token.ContainsKey("EndsWith"))
                        result = result.Where(x => x.MaintParent.TagNumber.EndsWith(Token["EndsWith"].First()));
                    if (Token.ContainsKey("Contains"))
                    {
                        foreach (string token in Token["Contains"])
                        {
                            result = result.Where(x => x.MaintParent.TagNumber.Contains(token));
                        }
                    }
                }
                if (searchModel.LocationId != null)
                {
                    result = result.Where(x => x.Location.LocationName.Contains(searchModel.LocationId));
                }
                if (searchModel.MaintTypeId != null)
                {
                    result = result.Where(x => x.MaintType.MaintTypeName.Contains(searchModel.MaintTypeId));
                }
                //if (searchModel.MaintStatusId != null)
                // {
                //     result = result.Where(x => x.MaintStatus.Contains(searchModel.MaintStatusId));
                //}
                if (searchModel.EngStatusId != null)
                {
                    result = result.Where(x => x.EngStatus.EngStatusName.Contains(searchModel.EngStatusId));
                }
                if (searchModel.CommissioningSubsystemId != null)
                {
                    result = result.Where(x => x.CommissioningSubsystem.CommSubSystemName.Contains(searchModel.CommissioningSubsystemId));
                }
                if (searchModel.CommZoneId != null)
                {
                    result = result.Where(x => x.CommZone.CommZoneName.Contains(searchModel.CommZoneId));
                }
                if (searchModel.ModelId != null)
                {
                    result = result.Where(x => x.Model.ModelName.Contains(searchModel.ModelId));
                }
                if (searchModel.ManufacturerId != null)
                {
                    result = result.Where(x => x.Manufacturer.ManufacturerName.Contains(searchModel.ManufacturerId));
                }
                if (searchModel.ExMethodId != null)
                {
                    result = result.Where(x => x.ExMethod.ExMethodName.Contains(searchModel.ExMethodId));
                }
                if (searchModel.MaintWorkCentreId != null)
                {
                    //result = result.Where(x => x.MaintWorkCentre.MaintWorkCentreName.Contains(searchModel.MaintWorkCentreId));
                    TokenisedQuery dicSearch = new TokenisedQuery(searchModel.MaintWorkCentreId);
                    var Token = dicSearch.getToken();

                    if (Token.ContainsKey("StartsWith"))
                        result = result.Where(x => x.MaintWorkCentre.MaintWorkCentreName.StartsWith(Token["StartsWith"].First()));
                    if (Token.ContainsKey("EndsWith"))
                        result = result.Where(x => x.MaintWorkCentre.MaintWorkCentreName.EndsWith(Token["EndsWith"].First()));
                    if (Token.ContainsKey("Contains"))
                    {
                        foreach (string token in Token["Contains"])
                        {
                            result = result.Where(x => x.MaintWorkCentre.MaintWorkCentreName.Contains(token));
                        }
                    }
                }
                if (searchModel.MaintStructureIndicatorId != null)
                {
                    TokenisedQuery dicSearch = new TokenisedQuery(searchModel.MaintStructureIndicatorId);
                    var Token = dicSearch.getToken();

                    if (Token.ContainsKey("StartsWith"))
                        result = result.Where(x => x.MaintStructureIndicator.MaintStructureIndicatorName.StartsWith(Token["StartsWith"].First()));
                    if (Token.ContainsKey("EndsWith"))
                        result = result.Where(x => x.MaintStructureIndicator.MaintStructureIndicatorName.EndsWith(Token["EndsWith"].First()));
                    if (Token.ContainsKey("Contains"))
                    {
                        foreach (string token in Token["Contains"])
                        {
                            result = result.Where(x => x.MaintStructureIndicator.MaintStructureIndicatorName.Contains(token));
                        }
                    }
                }
                if (searchModel.CommClassId != null)
                {
                    result = result.Where(x => x.CommClass.CommClassName.Contains(searchModel.CommClassId));
                }
                if (searchModel.MaintPlannerGroupId != null)
                {
                    result = result.Where(x => x.MaintPlannerGroup.MaintPlannerGroupName.Contains(searchModel.MaintPlannerGroupId));
                }
                if (searchModel.MaintenanceplanId != null)
                {
                    result = result.Where(x => x.Maintenanceplan.MaintPlanName.Contains(searchModel.MaintenanceplanId));
                }
                if (searchModel.PerformanceStandardId != null)
                {
                    //result = result.Where(x => x.PerformanceStandard.PerformanceStandardName.Contains(searchModel.PerformanceStandardId));
                    TokenisedQuery dicSearch = new TokenisedQuery(searchModel.PerformanceStandardId);
                    var Token = dicSearch.getToken();

                    if (Token.ContainsKey("StartsWith"))
                        result = result.Where(x => x.PerformanceStandard.PerformanceStandardName.StartsWith(Token["StartsWith"].First()));
                    if (Token.ContainsKey("EndsWith"))
                        result = result.Where(x => x.PerformanceStandard.PerformanceStandardName.EndsWith(Token["EndsWith"].First()));
                    if (Token.ContainsKey("Contains"))
                    {
                        foreach (string token in Token["Contains"])
                        {
                            result = result.Where(x => x.PerformanceStandard.PerformanceStandardName.Contains(token));
                        }
                    }
                }
                //if (searchModel.MaintClassId != null)
                //{
                //    result = result.Where(x => x.MaintClass.MaintClassName.Contains(searchModel.MaintClassId));
                //}
                if (searchModel.KeyDocId != null)
                {
                    result = result.Where(x => x.KeyDoc.DocNum.Contains(searchModel.KeyDocId));
                }
                //if (searchModel.PoId != null)
                //{
                //    result = result.Where(x => x.Po.Contains(searchModel.PoId));
                //}
                if (searchModel.VibId != null)
                {
                    result = result.Where(x => x.Vib.VibName.Contains(searchModel.KeyDocId));
                }
                if (searchModel.RbiSilId != null)
                {
                    result = result.Where(x => x.RbiSil.RbiSilName.Contains(searchModel.RbiSilId));
                }
                if (searchModel.IpfId != null)
                {
                    result = result.Where(x => x.Ipf.IpfName.Contains(searchModel.IpfId));
                }
                if (searchModel.RcmId != null)
                {
                    result = result.Where(x => x.Rcm.RcmName.Contains(searchModel.RcmId));
                }
                if (searchModel.MaintScePsReviewTeamId != null)
                {
                    result = result.Where(x => x.MaintScePsReviewTeam.MaintScePsReviewTeamName.Contains(searchModel.MaintScePsReviewTeamId));
                }
                if (searchModel.RbmId != null)
                {
                    result = result.Where(x => x.Rbm.RbmName.Contains(searchModel.RbmId));
                }
                if (searchModel.MaintSortProcessId != null)
                {
                    result = result.Where(x => x.MaintSortProcess.MaintSortProcessName.Contains(searchModel.MaintSortProcessId));
                }
                if (searchModel.MaintEdcCodeId != null)
                {
                    result = result.Where(x => x.MaintEdcCode.MaintEdcCodeName.Contains(searchModel.MaintEdcCodeId));
                }
            }
            
            return result;
        }


        public class TokenisedQuery
        {
            private Dictionary<string, List<string>> dicSearch = new Dictionary<string, List<string>>();
            public TokenisedQuery(string searchValue) 
            {
                /* if wildcards found, tokenise string into dictionary, else return original value in dic as 'contains'.
                 * 
                 *   dicSearch[StartsWith|Contains|EndsWith] = [list]
0                *   
                 *   where first wildcard found: 
                 *   1. search upto wildcard as 'starts with'.
                 *   2. Remove wildcard
                 *   3. process remainder of string.
                 *   
                 *   where subsequent wildcard found
                 *   1. search upto wildcard as 'contains'
                 *   2. remove wildcard
                 *   3. process remainder of string
                 *   
                 *   If subsequent wildcards, and last token does not end with wildcard
                 *   1. search last token as 'ends with'
                 *   2. else search as 'contains'
                 */

                //init dicSearch
                dicSearch["Contains"] = new List<string>() { };
                dicSearch["EndsWith"] = new List<string>() { };
                dicSearch["StartsWith"] = new List<string>() { };

                int count = searchValue.Length - searchValue.Replace("*", "").Length;

                if (count == 0)
                {
                    dicSearch["Contains"].Add(searchValue);
                }
                else if (count == 1)
                {
                    if (searchValue.StartsWith("*"))
                        dicSearch["EndsWith"].Add(searchValue.Substring(1));
                    else if (searchValue.EndsWith("*"))
                        dicSearch["StartsWith"].Add(searchValue.Substring(0, searchValue.Length - 1));
                    else
                    {
                        // mid string wildcard:
                        // set starts with & Ends with
                        dicSearch["StartsWith"].Add(searchValue.Substring(0, searchValue.IndexOf('*')));
                        dicSearch["EndsWith"].Add(searchValue.Substring(searchValue.IndexOf('*') + 1));
                    }
                }
                else
                {
                    // TODO: remove count = 1 content as this else statement performs its functionality anyway.
                    
                    // special condition: wildcards added to front/back to emulate 'contains' only.
                    if (searchValue.StartsWith("*") && searchValue.EndsWith("*") && count == 2)
                    {
                        dicSearch["Contains"].Add(searchValue.Replace("*", ""));
                    }
                    else
                    {
                        // multiple wildcards
                        // 1. create loop to find 1st wildcard. 
                        // 2. based on position (start|mid|end) create the search item and put in dicSearch.
                        // 3. remove token part upto next wildcard, thne repeat until all wildcards consumed.

                        string tempsearchVal = searchValue;
                        while (count > 0)
                        {
                            // determine * positions.
                            var idx1 = tempsearchVal.IndexOf("*");
                            var idx2 = tempsearchVal.IndexOf("*", idx1 + 1);

                            if (idx1 == 0 && idx2 != -1) // contains
                            {
                                dicSearch["Contains"].Add(tempsearchVal.Substring(1, idx2 - 1));
                                tempsearchVal = tempsearchVal.Substring(idx2);
                            }
                            else if (idx1 > 0) //starts with
                            {
                                dicSearch["StartsWith"].Add(tempsearchVal.Substring(0, idx1));
                                tempsearchVal = tempsearchVal.Substring(idx1);
                            }
                            else if (idx1 == 0 && idx2 == -1)  //EndsWith
                            {
                                dicSearch["EndsWith"].Add(tempsearchVal.Substring(1, tempsearchVal.Length - 1));
                                tempsearchVal = "";
                            }

                            // retest count
                            if (tempsearchVal == "*")
                                count = 0;
                            else
                                count = tempsearchVal.Length - tempsearchVal.Replace("*", "").Length;

                        } // iterate
                    } // special condition.
                } // convert multiple wildcards

                // cleanup dictionary.
                if (dicSearch["Contains"].Count() == 0)
                    dicSearch.Remove("Contains");
                if (dicSearch["StartsWith"].Count() == 0)
                    dicSearch.Remove("StartsWith");
                if (dicSearch["EndsWith"].Count() == 0)
                    dicSearch.Remove("EndsWith");
            }

            public Dictionary<string, List<string>> getToken()
            {
                return dicSearch;
            }
        }

    }

       

}
