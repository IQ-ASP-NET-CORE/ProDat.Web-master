using System; 
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ProDat.Web2.Models
{
    public class TaskListGroup
    {
        public TaskListGroup()
        {
            //TaskListHeaders = new HashSet<TaskListHeader>();
        }

        public int TaskListGroupId { get; set; }

        [Required]
        [Remote(action: "ValidateName", controller: "TaskListGroups", ErrorMessage = "Name exists.")]
        public string TaskListGroupName { get; set; }
        public string TaskListGroupDesc { get; set; }

        //public virtual ICollection<TaskListHeader> TaskListHeaders { get; set; }

        
    }
}
