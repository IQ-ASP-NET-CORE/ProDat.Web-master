using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ProDat.Web2.Models
{
    public class ScePsreview
    {
        public ScePsreview()
        {
            TaskListXscePsreviews = new HashSet<TaskListXscePsreview>();
        }

        public int ScePsreviewId { get; set; }
        public string ScePsreviewName { get; set; }
        public string ScePsreviewDesc { get; set; }

        public virtual ICollection<TaskListXscePsreview> TaskListXscePsreviews { get; set; }
    }
}
