using DocumentFormat.OpenXml.Office2010.CustomUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProDat.Web2.ViewModels
{
    public class TreeNode
    {
        public string id { get; set; }
        //public string parent { get; set; }
        //public string EngParentId { get; set; }

        public string type { get; set; }

        //JSTree attributes
        public string text { get; set; }
        public string icon { get; set; }
        public string state { get; set; }
        public bool opened { get; set; }
        public bool disabled { get; set; }
        public bool selected { get; set; }
        public string li_attr { get; set; }
        public string a_attr { get; set; }

        public IList<TreeNode> children { get; set; }
    }
}
