using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProDat.Web2.Models
{
    public class FlocType
    {
        public int FlocTypeId { get; set; }
        public string FlocTypeName { get; set; }
        public string FlocTypeDesc { get; set; }

        public virtual ICollection<Tag> Tags { get; set; }

    }
}
