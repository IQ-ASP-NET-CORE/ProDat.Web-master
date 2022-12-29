using System; 
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ProDat.Web2.Models
{
    public class TaskListCat
    {
        public TaskListCat()
        {
            TaskListHeaders = new HashSet<TaskListHeader>();
        }

        public int TaskListCatId { get; set; }

        [Required]
        [Remote(action: "ValidateName", controller: "TaskListCats", ErrorMessage = "Name exists.")]
        public string TaskListCatName { get; set; }
        public string TaskListCatDesc { get; set; }

        public virtual ICollection<TaskListHeader> TaskListHeaders { get; set; }
    }
}
