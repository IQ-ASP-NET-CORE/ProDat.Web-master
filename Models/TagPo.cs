using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ProDat.Web2.Models
{
    public class TagPo
    {
        public int TagId { get; set; }
        public int Poid { get; set; }

        public virtual Po Po { get; set; }
        public virtual Tag Tag { get; set; }
    }
}
