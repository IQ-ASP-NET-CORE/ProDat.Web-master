using System; 
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ProDat.Web2.Models
{
    public class Manufacturer
    {
        public Manufacturer()
        {
            Tags = new HashSet<Tag>();
        }

        public int ManufacturerId { get; set; }

        [Required]
        [Remote(action: "ValidateName", controller: "Manufacturers", ErrorMessage = "Name exists.")]
        public string ManufacturerName { get; set; }
        public string ManufacturerDesc { get; set; }

        public virtual ICollection<Tag> Tags { get; set; }
    }
}
