using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ProDat.Web2.Models
{
    public class RbiSil
    {
        public RbiSil()
        {
            Tags = new HashSet<Tag>();
        }

        public int RbiSilId { get; set; }
        public string RbiSilName { get; set; }
        public string RbiSilDesc { get; set; }
        public DateTime? RbiSilDate { get; set; }

        public virtual ICollection<Tag> Tags { get; set; }
    }
}
