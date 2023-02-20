using System;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ProDat.Web2.Models
{
    public class EngDisc
    {
        public EngDisc()
        {
            Tags = new HashSet<Tag>();
        }

        public int EngDiscId { get; set; }

		[Required]
        [Display(Name = "Eng. Discipline")]
        [Remote(action: "ValidateName", controller: "EngDiscs", ErrorMessage = "Name exists.")]
        public string EngDiscName { get; set; }

		[Display(Name = "Eng. Discipline Description")]
        public string EngDiscDesc { get; set; }

		// Todo: Move this to a Model View. Dropdown lists for TagRegister.
		public string dlValue
        {
            get
            {
                return EngDiscName +" - "+ EngDiscDesc;
            }
        }

        public string docDisc { get; set; }

        public virtual Doc Doc { get; set; }

        public virtual ICollection<Tag> Tags { get; set; }

        public virtual ICollection<DocType> DocTypes { get; set; }
    }
}
