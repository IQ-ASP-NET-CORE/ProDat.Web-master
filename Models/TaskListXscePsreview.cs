using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ProDat.Web2.Models
{
    public class TaskListXscePsreview
    {
        public int ScePsreviewId { get; set; }
        public int TaskListHeaderId { get; set; }

        public virtual ScePsreview ScePsreview { get; set; }
        public virtual TaskListHeader TaskListHeader { get; set; }
    }
}
