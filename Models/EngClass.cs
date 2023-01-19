using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ProDat.Web2.Models
{
    public class EngClass
    {
        public EngClass()
        {
            Tags = new HashSet<Tag>();
        }

        public int EngClassId { get; set; }
        public string EngClassName { get; set; }
        public string EngClassDesc { get; set; }

        public int SuperClassID { get; set; }
        public virtual SuperClass SuperClass { get; set; }

        public virtual EngClassRequiredDocs EngClassRequiredDocs { get; set; }

        public virtual ICollection<Tag> Tags { get; set; }

        public virtual ICollection<KeyListxEngClass> KeyListxEngClass { get; set; }

        public virtual ICollection<EngDataClassxEngDataCode> EngDataClassxEngDataCodes { get; set; }
    }
}
