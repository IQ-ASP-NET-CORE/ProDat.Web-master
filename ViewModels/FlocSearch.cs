using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProDat.Web2.ViewModels
{
    public class FlocSearch
    {
        public string TagId { get; set; }
        
        public string TagFloc { get; set; }

        public string MaintStatusId { get; set; }

        public string MaintTypeId { get; set; }

        public string FlocDesc { get; set; }

        //public IEnumerable<MaintHeirarchyNode> Items { get; set; }
    }
}
