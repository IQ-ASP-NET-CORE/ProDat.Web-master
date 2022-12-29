using System; 
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ProDat.Web2.Models
{
    public class PlannerPlant
    {
        public PlannerPlant()
        {
            Tags = new HashSet<Tag>();
        }

        public int PlannerPlantId { get; set; }

        [Required]
        [Remote(action: "ValidateName", controller: "PlannerPlants", ErrorMessage = "Name exists.")]
        public string PlannerPlantName { get; set; }
        public string PlannerPlantDesc { get; set; }

        public virtual ICollection<Tag> Tags { get; set; }
    }
}
