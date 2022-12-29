using System; 
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ProDat.Web2.Models
{
    public class EngStatus
    {
        public EngStatus()
        {
            Tags = new HashSet<Tag>();
        }

        public int EngStatusId { get; set; }

        [Required]
        [Remote(action: "ValidateName", controller: "EngStatuses", ErrorMessage = "Name exists.")]
        public string EngStatusName { get; set; }

        public virtual ICollection<Tag> Tags { get; set; }
    }
}
