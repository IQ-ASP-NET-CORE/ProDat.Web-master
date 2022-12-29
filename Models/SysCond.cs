using System; 
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ProDat.Web2.Models
{
    public class SysCond
    {
        public SysCond()
        {
        }

        public int SysCondId { get; set; }

        [Required]
        [Remote(action: "ValidateName", controller: "SysConds", ErrorMessage = "Name exists.")]
        public string SysCondName { get; set; }
        public string SysCondDesc { get; set; }

        //public virtual ICollection<TaskListHeader> TaskListHeaders { get; set; }
    }
}
