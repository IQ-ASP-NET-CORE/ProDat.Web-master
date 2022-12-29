using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProDat.Web2.ViewModels
{
    public class TaskListHeader_UC3
    {
            public int TaskListHeaderId { get; set; }
            public string TaskListGroupId { get; set; } //group e.g. IQGP-0002
            public int Counter { get; set; }
            public string TaskListShortText { get; set; }  // description
            public int MaintItemId { get; set; }
            public int MaintPlanId { get; set; }  // cant get this?
    }
}
