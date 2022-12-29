using System; 
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ProDat.Web2.Models
{
    public class Po
    {
        public Po()
        {
            TagPos = new HashSet<TagPo>();
        }

        public int PoId { get; set; }

        [Required]
        [Remote(action: "ValidateName", controller: "Pos", ErrorMessage = "Name exists.")]
        public string PoName { get; set; }
        public string PoCompany { get; set; }
        public string PoDesc { get; set; }

        public virtual ICollection<TagPo> TagPos { get; set; }
    }
}
