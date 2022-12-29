using System; 
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ProDat.Web2.Models
{
    public class PerformanceStandard
    {
        public PerformanceStandard()
        {
            Tags = new HashSet<Tag>();
            TaskListHeaders = new HashSet<TaskListHeader>();
        }

        public int PerformanceStandardId { get; set; }

        [Required]
        [Remote(action: "ValidateName", controller: "PerformanceStandards", ErrorMessage = "Name exists.")]
        public string PerformanceStandardName { get; set; }
        public string PerformanceStandardDesc { get; set; }

        public virtual ICollection<Tag> Tags { get; set; }
        public virtual ICollection<TaskListHeader> TaskListHeaders { get; set; }
    }
}
