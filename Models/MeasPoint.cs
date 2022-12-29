using System; 
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ProDat.Web2.Models
{
    public class MeasPoint
    {
        public MeasPoint()
        {
            MaintPlans = new HashSet<MaintPlan>();
        }

        public int MeasPointId { get; set; }

        [Required]
        [Remote(action: "ValidateName", controller: "Areas", ErrorMessage = "Name exists.")]
        public string MeasPointData { get; set; }
        public string MeasPointName { get; set; }

        public virtual ICollection<MaintPlan> MaintPlans { get; set; }
    }
}
