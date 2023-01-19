using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ProDat.Web2.Models
{
    public class EngDataClassxEngDataCode
    {
        public int EngDataClassxEngDataCodeId { get; set; }
        public int EngClassId { get; set; }
        public int EngDataCodeId { get; set; }

        public int BccCodeId { get; set; }

        public virtual BccCode BccCode { get; set; }

        public virtual EngClass EngClass { get; set; }

        public virtual EngDataCode EngDataCode { get; set; }

    }
}
