using System; 
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ProDat.Web2.Models
{
    public class ExMethod
    {
        public ExMethod()
        {
            Tags = new HashSet<Tag>();
        }

        public int ExMethodId { get; set; }

        [Required]
        [Remote(action: "ValidateName", controller: "ExMethods", ErrorMessage = "Name exists.")]
        public string ExMethodName { get; set; }
        public string ExMethodDesc { get; set; }

        public virtual ICollection<Tag> Tags { get; set; }
    }
}
