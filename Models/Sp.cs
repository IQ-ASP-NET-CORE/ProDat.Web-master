using System; 
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ProDat.Web2.Models
{
    public class Sp
    {
        public Sp()
        {
            CommSubSystems = new HashSet<CommSubSystem>();
        }

        public int Spid { get; set; }

        [Display (Name = "Project")]
        public int ProjectId { get; set; }

        [Required]
        [Remote(action: "ValidateName", controller: "Sps", ErrorMessage = "Name exists.")]
        public string Spnum { get; set; }
        public string Spdesc { get; set; }

        public virtual Project Project { get; set; }
        public virtual ICollection<CommSubSystem> CommSubSystems { get; set; }
    }
}
