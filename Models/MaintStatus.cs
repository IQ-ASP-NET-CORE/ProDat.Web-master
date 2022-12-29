using System;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ProDat.Web2.Models
{
    public class MaintStatus
    {
        public int MaintStatusId { get; set; }

        [Required]
        [Remote(action: "ValidateName", controller: "MaintStatuses", ErrorMessage = "Name exists.")]
        public string MaintStatusName { get; set; }
        public string MaintStatusDesc { get; set; }

        public virtual IEnumerable<Tag> Tags { get; set; }
    }
}
