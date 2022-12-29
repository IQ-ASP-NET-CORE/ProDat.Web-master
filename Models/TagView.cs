using System;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ProDat.Web2.Models
{
    public class TagView
    {
        public int TagViewId { get; set; }

        [Required]
        [Remote(action: "ValidateName", controller: "TagViews", ErrorMessage = "Name exists.")]
        public string TagViewName { get; set; }
        public bool Active { get; set; }

        public virtual ICollection<TagViewColumns> TagViewColumns { get; set; }

    }
}
