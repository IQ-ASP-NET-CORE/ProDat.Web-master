using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ProDat.Web2.Models
{
    public class EngDataCodeDropDown
    {
        public int EngDataCodeDropDownId { get; set; }

        public int EngDataCodeId { get; set; }

        [MaxLength(255)]
        public string EngDataCodeDropDownValue { get; set; }

        [MaxLength(255)]
        public string EngDataCodeDropDownDesc { get; set; }

        public virtual EngDataCode EngDataCode { get; set; }

    }
}
