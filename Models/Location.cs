using System; 
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using DocumentFormat.OpenXml.Office.CoverPageProps;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProDat.Web2.Models
{
    public class Location
    {
        public Location()
        {
            //Tags = new HashSet<Tag>();
        }

        public int LocationID { get; set; }

        [Display(Name = "Area")]
        public int AreaId { get; set; }


        //[Remote(action: "ValidateName", controller: "Locations", ErrorMessage = "Name exists.")]
        //[Required]
        public string LocationName { get; set; }
        public string LocationDesc { get; set; }

        public virtual Area Area { get; set; }

        public string Longitude { get; set; }

        public string Latitude { get; set; }

        public int Elevation { get; set; }

        public int ParentLocationID { get; set; }

        public string LocationType { get; set; }
        
        [ForeignKey("ParentLocationID")]
        public virtual Location ParentLocation { get; set; }

        //public virtual ICollection<Location> Children { get; set; }

        //public virtual ICollection<Tag> Tags { get; set; }
    }
}