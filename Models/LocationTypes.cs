using System; 
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using DocumentFormat.OpenXml.Office.CoverPageProps;

namespace ProDat.Web2.Models
{
    public class LocationTypes
    {
        public LocationTypes()
        {
            //Tags = new HashSet<Tag>();
        }

        [Display(Name = "Location Type")]
        public int LocationTypesId { get; set; }


        //[Remote(action: "ValidateName", controller: "Locations", ErrorMessage = "Name exists.")]
        [Required]
        public string LocationTypeDescription { get; set; }


        public virtual LocationTypes Locations { get; set; }
    }
}
