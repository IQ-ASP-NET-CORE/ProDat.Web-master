using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ProDat.Web2.Models
{
    public class FlocXmaintQuery
    {
        public int FlocId { get; set; }
        public int MaintQueryId { get; set; }

        public virtual Tag Floc { get; set; }
        public virtual MaintQuery MaintQuery { get; set; }
    }
}
