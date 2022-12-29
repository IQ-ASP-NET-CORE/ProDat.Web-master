using System; 
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ProDat.Web2.Models
{
    public class MaintSortProcess
    {
        public MaintSortProcess()
        {
            MaintPlans = new HashSet<MaintPlan>();
            Tags = new HashSet<Tag>();
        }

        public int MaintSortProcessId { get; set; }

        [Required]
        [Remote(action: "ValidateName", controller: "MaintSortProcesses", ErrorMessage = "Name exists.")]
        public string MaintSortProcessName { get; set; }
        public string MaintSortProcessDesc { get; set; }

        public virtual ICollection<MaintPlan> MaintPlans { get; set; }
        public virtual ICollection<Tag> Tags { get; set; }
    }
}
