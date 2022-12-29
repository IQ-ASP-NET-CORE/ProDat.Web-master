using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ProDat.Web2.Models;

namespace ProDat.Web2.ViewModels
{
    public class TaskListHeader_UC3v2
    {
        public TaskListHeader TaskListHeader { get; set;} 

        // Below added to model from TaskListHeader_GetData
        public int MaintItemId { get; set; }
        public int MaintPlanId { get; set; }  // cant get this?
    }
}
