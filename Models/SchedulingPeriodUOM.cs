using System; 
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
namespace ProDat.Web2.Models
{
    public class SchedulingPeriodUOM
    {
        public int SchedulingPeriodUOMId { get; set; }

        [Required]
        [Remote(action: "ValidateName", controller: "SchedulingPeriodUOMs", ErrorMessage = "Name exists.")]
        public string SchedulingPeriodUOMName { get; set; }
        public string SchedulingPeriodUOMDesc { get; set; }
	
    }
}
