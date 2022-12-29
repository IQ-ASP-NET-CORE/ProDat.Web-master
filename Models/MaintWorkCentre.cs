using System; 
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ProDat.Web2.Models
{
    public class MaintWorkCentre
    {
        public MaintWorkCentre()
        {
            Tags = new HashSet<Tag>();
            TaskListHeaders = new HashSet<TaskListHeader>();
        }

        public int MaintWorkCentreId { get; set; }

        [Required]
        [Remote(action: "ValidateName", controller: "MaintWorkCentres", ErrorMessage = "Name exists.")]
        public string MaintWorkCentreName { get; set; }
        public string MaintWorkCentreDesc { get; set; }

        public string dlValue
        {
            get
            {
                return MaintWorkCentreName + " - " + MaintWorkCentreDesc;
            }
        }

        public virtual ICollection<Tag> Tags { get; set; }
        public virtual ICollection<TaskListHeader> TaskListHeaders { get; set; }
    }
}
