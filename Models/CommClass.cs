using System; 
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ProDat.Web2.Models
{
    public class CommClass
    {
        public CommClass()
        {
            Tags = new HashSet<Tag>();
        }

        public int CommClassId { get; set; }
        [Required]
        [Remote(action: "ValidateName", controller: "CommClasses", ErrorMessage = "Name exists.")]
        public string CommClassName { get; set; }
        public string CommClassDesc { get; set; }

        public virtual ICollection<Tag> Tags { get; set; }
    }
}
