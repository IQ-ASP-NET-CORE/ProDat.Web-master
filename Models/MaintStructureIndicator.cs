using System; 
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ProDat.Web2.Models
{
    public class MaintStructureIndicator
    {
        public int MaintStructureIndicatorId { get; set; }

        [Required]
        [Remote(action: "ValidateName", controller: "Areas", ErrorMessage = "Name exists.")]
        public string MaintStructureIndicatorName { get; set; }
        public string MaintStructureIndicatorDesc { get; set; }

        public virtual ICollection<Tag> Tags { get; set; }
    }
}
