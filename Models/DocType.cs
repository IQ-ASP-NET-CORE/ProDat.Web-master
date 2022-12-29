using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ProDat.Web2.Models
{
    public class DocType
    {
        //public DocType()
        //{
        //    Docs = new HashSet<Doc>();
        //}


        public int DocTypeId { get; set; }

        [Display(Name = "Doc Type")]
        public string DocTypeName { get; set; }
        public string DocTypeDesc { get; set; }
        public int DocTypeDiscId { get; set; }

        public virtual EngDisc EngDisc { get; set; }

        public virtual ICollection<Doc> Docs { get; set; }

        public virtual ICollection<EngClassRequiredDocs> EngClassRequiredDocs { get; set; }
    }
}
