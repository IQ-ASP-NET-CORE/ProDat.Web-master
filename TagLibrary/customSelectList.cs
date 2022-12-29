using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProDat.Web2.TagLibrary
{
    public class customSelectList : SelectList
    {
        customSelectList(System.Collections.IEnumerable items, string dataValueField, string dataTextField) : base(items, dataValueField, dataTextField)
        {
        }
    }
}
