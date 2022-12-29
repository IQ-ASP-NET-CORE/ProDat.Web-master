using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ProDat.Web2.Models
{
    public class Rcm
    {
        public Rcm()
        {
            Tags = new HashSet<Tag>();
        }

        public int RcmId { get; set; }
        public string RcmName { get; set; }
        public string RcmDesc { get; set; }
        public DateTime? RcmDate { get; set; }

        public virtual ICollection<Tag> Tags { get; set; }
    }
}
