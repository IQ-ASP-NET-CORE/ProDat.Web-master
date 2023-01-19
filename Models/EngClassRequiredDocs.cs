using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ProDat.Web2.Models
{
    public class EngClassRequiredDocs
    {
        //public EngClassRequiredDocs()
        //{
        //    RequiredDocs = new HashSet<EngClassRequiredDocs>();
        //}
        public int EngClassRequiredDocsId { get; set; }
        public int EngClassId { get; set; }
        public int DocTypeId { get; set; }
        public int BCC { get; set; }

        public virtual DocType DocType { get; set; }

        //public virtual ICollection<EngClassRequiredDocs> RequiredDocs { get; set; }

        public virtual EngClass EngClass { get; set; }
    }
}
