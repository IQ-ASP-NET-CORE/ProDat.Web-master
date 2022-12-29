using System; 
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ProDat.Web2.Models
{
    public class RegulatoryBody
    {
        public RegulatoryBody()
        {
            TaskListHeaders = new HashSet<TaskListHeader>();
        }

        public int RegulatoryBodyId { get; set; }

        [Required]
        [Remote(action: "ValidateName", controller: "Areas", ErrorMessage = "Name exists.")]
        public string RegulatoryBodyName { get; set; }
        public string RegulatoryBodyDesc { get; set; }

        public virtual ICollection<TaskListHeader> TaskListHeaders { get; set; }
    }
}
