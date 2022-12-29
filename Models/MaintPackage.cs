using System; 
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ProDat.Web2.Models
{
    public class MaintPackage
    {
        public MaintPackage()
        {
            TaskListHeaders = new HashSet<TaskListHeader>();
        }

        public int MaintPackageId { get; set; }

        [Required]
        [Remote(action: "ValidateName", controller: "MaintPackages", ErrorMessage = "Name exists.")]
        public string MaintPackageName { get; set; }
        public int MaintPackageCycleLength { get; set; }
        public string MaintPackageCycleUnit { get; set; }
        public string MaintPackageCycleText { get; set; }

        public virtual ICollection<TaskListHeader> TaskListHeaders { get; set; }
    }
}
