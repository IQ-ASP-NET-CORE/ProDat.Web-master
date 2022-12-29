using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Text;
using DocumentFormat.OpenXml.Drawing.Charts;
using System.Reflection;
using ProDat.Web2.Models;
using System.ComponentModel.DataAnnotations;

namespace ProDat.Web2.TagLibrary
{
    public static class MyHtmlHelpers
    {
        public static IHtmlContent SearchFilter(this IHtmlHelper htmlHelper, string fieldName, string thClass, string formId, string controlClass, ProDat.Web2.ViewModels.TagRegisterSearchViewModel currentSearchModel, Dictionary<string, List<string>> ddls, string width)
        {
            StringBuilder sb = new StringBuilder();

            Type myType = typeof(ProDat.Web2.ViewModels.TagRegisterSearchViewModel);
            PropertyInfo myPropInfo = myType.GetProperty(fieldName, BindingFlags.Public | BindingFlags.Instance);

            // Build a inputList:
            if (fieldName.EndsWith("Id"))
            {
                string shortFieldName = fieldName.Substring(0, fieldName.Length - 2);
                //sb.Append("<th class=\"" + thClass + "\" width=\""+ width +"\">");
                sb.Append("<th class=\"" + thClass + "\" style=\"width: "+width+"px; position: relative;\">");
                var inputList = "";
                if (currentSearchModel != null)
                {
                    inputList = (string)myPropInfo.GetValue(currentSearchModel);
                }

                // < input name = "EngDiscId" list = "EngDisc" value = "@inputList" form = "search1" class= "search" >
                sb.Append("<input name =\"" + fieldName + "\" list=\"" + shortFieldName + "\" value=\"" + inputList + "\" form=\"" + formId + "\" class=\"search\">");

                // < datalist id = "EngDisc" >
                sb.Append("<datalist id=\"" + shortFieldName + "\">");

                List<string> eds = ddls[shortFieldName + "Search"];
                foreach (var el in eds)
                {
                    //<option value="@el">@el</option>
                    sb.Append("<option value=\"" + el + "\">" + el + "</option>");
                }

                //   </datalist>
                // </th>
                sb.Append("</datalist></th>");
                return new HtmlString(sb.ToString());
            }
            else
            {
                // else create a text box:
                var txtValue = "";
                if (currentSearchModel != null)
                {
                    txtValue = (string)myPropInfo.GetValue(currentSearchModel); ;
                }
                // <th class="searchHeader" width="300px">
                //sb.Append("<th class=\"" + thClass + "\" width=\"" + width + "\">");
                sb.Append("<th class=\"" + thClass + "\" style=\"width: " + width + "px; position: relative;\">");
                // <input type = "text" form="search1" class="search" name="TagFlocDesc" value="@(modelExists ? currentSearchModel.TagFlocDesc : "")" />
                sb.Append("<input type=\"text\" form=\"" + formId + "\" class=\"" + controlClass + "\" name=\"" + fieldName + "\" value=\"" + txtValue + "\" />");
                // </th>
                sb.Append("</th>");

                return new HtmlString(sb.ToString());
            }
        }

        public static IHtmlContent HeaderColumns(this IHtmlHelper htmlHelper, string fieldName, string sortParam, string controller)
        {
            StringBuilder sb = new StringBuilder();
            string image = "";
            string displayName = "";
            //if (width == null)
            //    width = "";

            Type myType = typeof(ProDat.Web2.Models.Tag);
            var displayInfo = myType.GetProperty(fieldName, BindingFlags.Public | BindingFlags.Instance).GetCustomAttribute<DisplayAttribute>();
            if (displayInfo != null)
                displayName = displayInfo.Name;

            sb.Append("<th class=\"headerColumn\">");
            if (sortParam.Contains(fieldName))
            {
                if (sortParam.EndsWith("_descending"))
                    image = "/img/asc.png";
                else
                    image = "/img/desc.png";
            }
            else
            {
                sortParam = fieldName;
            }

            // <img src="@image"/><a asp-action="Index" asp-route-SortOrder="@sortParam">@Html.DisplayNameFor(Model => Model.TagNumber)</a>
            sb.Append("<img src=\"" + image + "\"/><a href=\\" + controller + "?SortOrder=" + sortParam + ">" + displayName + "</a>");
            sb.Append("</th>");

            return new HtmlString(sb.ToString());
        }

        public static IHtmlContent DataColumn(this IHtmlHelper htmlHelper, string fieldName, string controlCss, bool firstRow, string disabled, Tag item)
        {
            StringBuilder sb = new StringBuilder();
            Type myType = typeof(ProDat.Web2.Models.Tag);
            var propInfo = myType.GetProperty(fieldName, BindingFlags.Public | BindingFlags.Instance);

            var dataValue = propInfo.GetValue(item);
            string sValue = "";
            if (dataValue != null)
                sValue = dataValue.ToString();

            string openCol = "";
            string closeCol = "";
            if (firstRow)
            {
                openCol = "<th class=\"dataColumn\">";
                closeCol = "</th>";
            }
            else
            {
                openCol = "<td class=\"dataColumn\">";
                closeCol = "</td>";
            }

            if (fieldName.EndsWith("Id"))
            {
                // ddls
                List<string> selfRef = new List<string> { "MaintParentId", "EngParentId" };
                List<string> numRef = new List<string> { "SubSystemId", "asdf" };

                ExpressionGet t = new ExpressionGet();

                // get lookup value using reflection, based on type.
                // 1. Self referencing field? EngParentId|MaintParentId
                // 2. Num (SubSystemId)
                // 3. Name
                string expression = "";
                if (selfRef.Contains(fieldName))
                {

                    //get tag.MaintParent or tag.EngParent.TagNumber
                    if (fieldName == "MaintParentId")
                        expression = "MaintParent.TagNumber";
                    else
                        expression = "EngParent.TagNumber";
                }
                else if (numRef.Contains(fieldName))
                {
                    // retrieve entityName.EntityName+'Num'
                    string entityName = fieldName.Substring(0, fieldName.Length - 2);
                    expression = entityName + "." + entityName + "Num";
                }
                else
                {
                    // retrieve entityName.EntityName+'Name'
                    string entityName = fieldName.Substring(0, fieldName.Length - 2);
                    expression = entityName + "." + entityName + "Name";
                }

                var RetVal = t.getChildPropertyValue(expression, item);
                if (RetVal == null)
                    sValue = "";
                else
                    sValue = RetVal.ToString();

                //  <td>
                sb.Append(openCol);
                //       <select id = "PerformanceStandardId" class="recordDDl" @sDisabled>
                sb.Append("<select id=\"" + fieldName + "\" class=\"" + controlCss + "\" " + disabled + ">");

                if (sValue != null)
                {
                    //< option value = "@item.PerformanceStandardId" selected > @item.PerformanceStandard.PerformanceStandardName </ option >
                    sb.Append("<option value=\"" + sValue + "\" selected>" + sValue + "</option>");
                }
                else
                {
                    //< option value = "-1" selected > -</ option >
                    sb.Append("<option value=\"-1\" selected>-</option>");
                }
                sb.Append("</select>");
                sb.Append(closeCol);
            }
            else
            {
                // input boxes
                //@Html.TextBoxFor(modelItem => item.TagNumber, isReadOnly ? new { @readOnly = "" } : null)
                //<input id="item_TagNumber" name="item.TagNumber" type="text" value="KTN-AT-0402" />
                sb.Append(openCol);

                if (disabled == "disabled")
                    sb.Append("<input id=\"item_" + fieldName + "\" name=\"item." + fieldName + "\" type=\"text\" value=\"" + dataValue + "\" readOnly=\"\" />");
                else
                    sb.Append("<input id=\"item_" + fieldName + "\" name=\"item." + fieldName + "\" type=\"text\" value=\"" + dataValue + "\" />");

                sb.Append(closeCol);

            }

            return new HtmlString(sb.ToString());
        }

    }
}
