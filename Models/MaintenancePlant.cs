using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ProDat.Web2.Models
{
    public class MaintenancePlant
    {
        public MaintenancePlant()
        {
            Areas = new HashSet<Area>();
            Projects = new HashSet<Project>();
            TaskListHeaders = new HashSet<TaskListHeader>();
        }

        public int MaintenancePlantId { get; set; }
        [Required]
        [Display(Name = "Maint. Plant Number")]
        public string MaintenancePlantNum { get; set; }
        [Display(Name = "Maint. Plant Desc")]
        public string MaintenancePlantDesc { get; set; }

        public virtual ICollection<Area> Areas { get; set; }
        public virtual ICollection<Project> Projects { get; set; }
        public virtual ICollection<TaskListHeader> TaskListHeaders { get; set; }
    }
}
