using System; 
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ProDat.Web2.Models
{
    public class PlantSection
    {
        public PlantSection()
        {
            MaintAreas = new HashSet<MaintArea>();
        }

        public int PlantSectionId { get; set; }
		
		[Required]
        [Display(Name = "Plant Sect.")]
        [Remote(action: "ValidateName", controller: "Areas", ErrorMessage = "Name exists.")]
        public string PlantSectionName { get; set; }
		
		[Display(Name = "Plant Sect. Desc.")]
        public string PlantSectionDesc { get; set; }

        public virtual ICollection<MaintArea> MaintAreas { get; set; }
    }
}
