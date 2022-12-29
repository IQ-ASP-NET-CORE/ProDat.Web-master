using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ProDat.Web2.Models
{
    public class TagXdocSearchViewModel
    {
        public bool? Posted { get; set; }

        public int TagId { get; set; }

        public int DocId { get; set; }

        public string DocNum { get; set;  }

        public string TagNumber { get; set; }

        public DateTime? DateCreated { get; set; }

        public string XComment { get; set; }

        public virtual Doc Doc { get; set; }

        public virtual Tag Tag { get; set; }
    }
}
