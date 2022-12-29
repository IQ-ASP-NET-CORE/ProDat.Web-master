using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProDat.Web2.TagLibrary
{
    public class customSelectItem
    {
        public string shortText { get; set; }
        public string text { get; set; }

        public int id { get; set; }

        public customSelectItem(int id, string shortText, string Text)
        {
            this.id = id;
            this.shortText = shortText;
            this.text = Text;
        }

        public customSelectItem()
        {
            //this.id = null;
            this.shortText = null;
            this.text = null;
        }
    }
}
