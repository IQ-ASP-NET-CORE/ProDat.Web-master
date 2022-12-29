using System; 
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ProDat.Web2.Models
{
    public class Model
    {
        public Model()
        {
            Tags = new HashSet<Tag>();
        }

        public int ModelId { get; set; }

        [Required]
        [Remote(action: "ValidateName", controller: "Models", ErrorMessage = "Name exists.")]
        public string ModelName { get; set; }
        public string ModelDesc { get; set; }
        [Display(Name = "Manufacturer")]
        public int? ManufacturerId { get; set; }
        
        public virtual Manufacturer Manufacturer {get; set;}
        public virtual ICollection<Tag> Tags { get; set; }
    }
}
