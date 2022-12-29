using System; 
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ProDat.Web2.Models
{
    public class Systems
    {
        public Systems()
        {
            CommSubSystems = new HashSet<CommSubSystem>();
            SubSystems = new HashSet<SubSystem>();
        }

        public int SystemsId { get; set; }

        [Required]
        [Remote(action: "ValidateNum", controller: "Systems", ErrorMessage = "Num exists.")]
        public string SystemNum { get; set; }
        
        public string SystemName { get; set; }

        public virtual ICollection<CommSubSystem> CommSubSystems { get; set; }
        public virtual ICollection<SubSystem> SubSystems { get; set; }
    }
}
