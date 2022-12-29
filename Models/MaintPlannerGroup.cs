using System; 
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ProDat.Web2.Models
{
    public class MaintPlannerGroup
    {
        public MaintPlannerGroup()
        {
            Tags = new HashSet<Tag>();
        }

        public int MaintPlannerGroupId { get; set; }

        [Required]
        [Remote(action: "ValidateName", controller: "MaintPlannerGroups", ErrorMessage = "Name exists.")]
        public string MaintPlannerGroupName { get; set; }
        public string MaintPlannerGroupDesc { get; set; }

        public virtual ICollection<Tag> Tags { get; set; }
    }
}
