using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ProDat.Web2.Models
{
    public class MaintItemXmaintTaskListHeader
    {
        public int MaintItemId { get; set; }
        public int TaskListHeaderId { get; set; }

        public virtual MaintItem MaintItem { get; set; }

        public virtual TaskListHeader TaskListHeader { get; set; }
    }
}
