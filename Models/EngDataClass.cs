using System;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ProDat.Web2.Models
{
    public class EngDataClass
    {

        public int EngDataClassId { get; set; }

        [Required]
        [Display(Name = "Data Class Name")]
        [Remote(action: "ValidateName", controller: "EngDataClass", ErrorMessage = "Name exists.")]
        public string EngDataClassName { get; set; }

        public virtual ICollection<EngDataCode> EngDataCodes { get; set; }
    }
}
