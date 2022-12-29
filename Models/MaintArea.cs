using System; 
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ProDat.Web2.Models
{
    public class MaintArea
    {
        public MaintArea()
        {
            MaintLocations = new HashSet<MaintLocation>();
        }

        public int MaintAreaId { get; set; }

        [Display(Name = "Plant Section")]
        public int? PlantSectionId { get; set; }
		
		[Required]
        [Display(Name = "Maint. Area")]
        [Remote(action: "ValidateName", controller: "MaintAreas", ErrorMessage = "Name exists.")]
        public string MaintAreaName { get; set; }
		
		[Display(Name = "Maint. Area Desc")]
        public string MaintAreaDesc { get; set; }

        public virtual PlantSection PlantSection { get; set; }
        
        // tell JSON serialisation to not fall into a loop.
        [JsonIgnore]
        [System.Runtime.Serialization.IgnoreDataMember]
        public virtual ICollection<MaintLocation> MaintLocations { get; set; }
    }
}
