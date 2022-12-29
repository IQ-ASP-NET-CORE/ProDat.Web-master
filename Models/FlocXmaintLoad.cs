using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ProDat.Web2.Models
{
    public class FlocXmaintLoad
    {
        public int TagId { get; set; }
        public int MaintLoadId { get; set; }

        public virtual MaintLoad MaintLoad { get; set; }
        public virtual Tag Tag { get; set; }
    }
}
