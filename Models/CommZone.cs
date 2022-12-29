using System; 
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ProDat.Web2.Models
{
    public class CommZone
    {
        public CommZone()
        {
            Tags = new HashSet<Tag>();
        }

        public int CommZoneId { get; set; }
        [Display(Name = "Project")]
        public int ProjectId { get; set; }
        [Required]
        [Remote(action: "ValidateName", controller: "CommZones", ErrorMessage = "Name exists.")]
        public string CommZoneName { get; set; }
        public string CommZoneDesc { get; set; }

        public virtual Project Project { get; set; }
        public virtual ICollection<Tag> Tags { get; set; }
    }
}
