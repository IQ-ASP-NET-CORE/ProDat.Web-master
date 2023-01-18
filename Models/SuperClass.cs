using System;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;



namespace ProDat.Web2.Models
{
    public class SuperClass
    {
        public int SuperclassID { get; set; }

        [Required]
        [Display(Name = "Superclass Name")]
        [Remote(action: "ValidateName", controller: "SuperClass", ErrorMessage = "Name exists.")]
        public string SuperclassName { get; set; }

        [Display(Name = "Superclass description")]
        public string Superclassdescription { get; set; }


        public ICollection<EngClass> EngClasses { get; set; }

    }
}
