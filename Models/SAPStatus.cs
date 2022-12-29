using System;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ProDat.Web2.Models
{
    public class SAPStatus
    {
        public int SAPStatusId { get; set; }

        [Required]
        [Remote(action: "ValidateName", controller: "SAPStatuses", ErrorMessage = "Name exists.")]
        public int StatusCode { get; set; }
        public string Description { get; set; }
        public string ColourCode { get; set; }
        public string FontColourCode { get; set; }
        public bool ForSAPExport { get; set; }

        //public virtual ICollection<Tag> Tags { get; set; }
    }
}
