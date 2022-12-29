using System; 
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ProDat.Web2.Models
{
    public class Pmassembly
    {
        public Pmassembly()
        {
            FlocXpmassemblys = new HashSet<FlocXpmassembly>();
            TaskListHeaders = new HashSet<TaskListHeader>();
        }

        public int PmassemblyId { get; set; }

        [Required]
        [Remote(action: "ValidateName", controller: "Areas", ErrorMessage = "Name exists.")]
        public string PmassemblyName { get; set; }
        public string ShortText { get; set; }
        public string Make { get; set; }
        public string Model { get; set; }
        public string Rev { get; set; }

        public virtual ICollection<FlocXpmassembly> FlocXpmassemblys { get; set; }
        public virtual ICollection<TaskListHeader> TaskListHeaders { get; set; }
    }
}
