using System; 
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ProDat.Web2.Models
{
    public class SortField
    {
        public SortField()
        {
            Tags = new HashSet<Tag>();
        }

        public int SortFieldId { get; set; }

        [Required]
        [Remote(action: "ValidateName", controller: "SortFields", ErrorMessage = "Name exists.")]
        public string SortFieldName { get; set; }
        public string SortFieldDesc { get; set; }

        public virtual ICollection<Tag> Tags { get; set; }
    }
}
