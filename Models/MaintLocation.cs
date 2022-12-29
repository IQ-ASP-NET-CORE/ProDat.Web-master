using System; 
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ProDat.Web2.Models
{
    public class MaintLocation
    {
        public MaintLocation()
        {
            Tags = new HashSet<Tag>();
        }

        public int MaintLocationId { get; set; }
        
        [Display(Name = "Maint. Area")]
        public int? MaintAreaId { get; set; }
		
		[Required]
        [Display(Name = "Maint. Location")]
        [Remote(action: "ValidateName", controller: "MaintLocations", ErrorMessage = "Name exists.")]
        public string MaintLocationName { get; set; }
		
		[Display(Name = "Maint. Location Description")]
        public string MaintLocationDesc { get; set; }
		
		//todo: move out of here
		public string dlValue
        {
            get
            {
                return MaintLocationName + " - " + MaintLocationDesc;
            }
        }

        public virtual MaintArea MaintArea { get; set; }

        // tell JSON serialisation to not fall into a loop. Devex Problem, not ASP.net.
        [JsonIgnore]
        [System.Runtime.Serialization.IgnoreDataMember]
        public virtual ICollection<Tag> Tags { get; set; }
    }
}
