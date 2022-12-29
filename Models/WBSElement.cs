using System; 
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ProDat.Web2.Models
{
    public class WBSElement
    {
        public WBSElement()
        {
            Tags = new HashSet<Tag>();
        }

        public int WBSElementId { get; set; }

        [Required]
        [Remote(action: "ValidateName", controller: "WBSElements", ErrorMessage = "Name exists.")]
        public string WBSElementName { get; set; }
        public string WBSElementDesc { get; set; }

        public virtual ICollection<Tag> Tags { get; set; }
    }
}
