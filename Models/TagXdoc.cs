using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ProDat.Web2.Models
{
    public class TagXdoc
    {
        public int TagId { get; set; }
        public int DocId { get; set; }

        [Display(Name = "Date Created")]
        public DateTime? DateCreated { get; set; }

        [Display(Name = "Comment")]
        public string XComment { get; set; }

        public virtual Doc Doc { get; set; }
        public virtual Tag Tag { get; set; }


    }
}
