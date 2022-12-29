using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ProDat.Web2.Models
{
    public class FlocXpmassembly
    {
        public int TagId { get; set; }
        public int PmassemblyId { get; set; }

        public virtual Pmassembly Pmassembly { get; set; }
        public virtual Tag Tag { get; set; }
    }
}
