using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ProDat.Web2.Models
{
    public class FlocXmaintItem
    {
        // Is really TagId
        public int FlocId { get; set; }
        public int MaintItemId { get; set; }

        public virtual Tag Floc { get; set; }
        public virtual MaintItem MaintItem { get; set; }
    }
}
