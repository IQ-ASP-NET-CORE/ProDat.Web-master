using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ProDat.Web2.Models
{
    public class Ipf
    {
        public Ipf()
        {
            Tags = new HashSet<Tag>();
        }

        public int IpfId { get; set; }
        public string IpfName { get; set; }
        public string IpfDesc { get; set; }

        public virtual ICollection<Tag> Tags { get; set; }
    }
}
