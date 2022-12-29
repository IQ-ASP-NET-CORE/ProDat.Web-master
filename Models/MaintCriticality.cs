using System; 
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ProDat.Web2.Models
{
    public class MaintCriticality
    {
        public MaintCriticality()
        {
            Tags = new HashSet<Tag>();
        }

        public int MaintCriticalityId { get; set; }

        [Required]
		[Display(Name = "Maint. Criticality")]
        [Remote(action: "ValidateName", controller: "MaintCriticalities", ErrorMessage = "Name exists.")]
        public string MaintCriticalityName { get; set; }
		
		[Display(Name = "Maint. Criticality Desc")]
        public string MaintCriticalityDesc { get; set; }
		
		//todo - move out of here
		public string dlValue
        {
            get
            {
                return MaintCriticalityName + " - " + MaintCriticalityDesc;
            }
        }
		
        public virtual ICollection<Tag> Tags { get; set; }
    }
}
