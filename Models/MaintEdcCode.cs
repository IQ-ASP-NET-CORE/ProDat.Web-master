using System; 
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ProDat.Web2.Models
{
    public class MaintEdcCode
    {
        public MaintEdcCode()
        {
            Tags = new HashSet<Tag>();
        }

        public int MaintEdcCodeId { get; set; }

        [Required]
        [Remote(action: "ValidateName", controller: "Areas", ErrorMessage = "Name exists.")]
        public string MaintEdcCodeName { get; set; }
        public string MaintEdcCodeDesc { get; set; }

        public virtual ICollection<Tag> Tags { get; set; }
    }
}
