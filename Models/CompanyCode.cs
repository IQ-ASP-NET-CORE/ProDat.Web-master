using System; 
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ProDat.Web2.Models
{
    public class CompanyCode
    {
        public CompanyCode()
        {
            Tags = new HashSet<Tag>();
        }

        public int CompanyCodeId { get; set; }
        [Required]
        [Remote(action: "ValidateName", controller: "CompanyCodes", ErrorMessage = "Name exists.")]
        public string CompanyCodeName { get; set; }
        public string CompanyCodeDesc { get; set; }

        public virtual ICollection<Tag> Tags { get; set; }
    }
}
