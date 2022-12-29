using System; 
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ProDat.Web2.Models
{
    public class SubSystem
    {
        public SubSystem()
        {
            Tags = new HashSet<Tag>();
        }

        public int SubSystemId { get; set; }
		
		[Required]
        [Display(Name = "SubSystem")]
        [Remote(action: "ValidateNum", controller: "SubSystems", ErrorMessage = "Num exists.")]
        public string SubSystemNum { get; set; }

        public string SubSystemName { get; set; }

        [Display (Name = "System")]
        public int SystemsId { get; set; }
		
		//todo, move out of here..
		public string dlValue
        {
            get
            {
                return SystemsId.ToString() + " - " + SubSystemNum;
            }
        }

        public virtual Systems Systems { get; set; }
        public virtual ICollection<Tag> Tags { get; set; }
    }
}
