using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProDat.Web2.ViewModels
{
    public class FlocListingDataGridUC3ViewModel
    {
        public int TagId { get; set; }
        public string TagFloc { get; set; }
        public string TagFlocDesc { get; set; }
        public bool RTF { get; set; }
        public int? MaintPlannerGroupId { get; set; }
        public int MICount { get; set; }

    }
}
