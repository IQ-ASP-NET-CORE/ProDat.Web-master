using System; 
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ProDat.Web2.Models
{
    public class Pbs
    {
        public int PbsId { get; set; }

        [Required]
        [Remote(action: "ValidateName", controller: "Pbs", ErrorMessage = "Name exists.")]
        public string PbsName { get; set; }
        public string PbsDesc { get; set; }
    }
}
