using System;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ProDat.Web2.Models
{
    public class Area
    {
        public int AreaId { get; set; }

        //magic that makes MaintenancePlantID the foreign key
        [Display(Name = "Maint. Plant")]
        public int MaintenancePlantId { get; set; }

        [Required]
        [Display(Name = "Area Name")]
        [Remote(action: "ValidateName", controller: "Areas", ErrorMessage = "Name exists.")]
        public string AreaName { get; set; }

        [Display(Name = "Area Desc")]
        public string AreaDisc { get; set; }

        //public int EngPlantSectionId { get; set; }

        //public string Longititude { get; set; }

        //public string Latitude { get; set; }

        //public string Elevation { get; set; }

        public virtual MaintenancePlant MaintenancePlant { get; set; }
        public virtual ICollection<Location> Locations { get; set; }
        //public virtual EngPlantSection EngPlantSections { get; set; }
    }
}
