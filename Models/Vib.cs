using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ProDat.Web2.Models
{
    public class Vib
    {
        public Vib()
        {
            Tags = new HashSet<Tag>();
        }

        public int VibId { get; set; }
        public string VibName { get; set; }
        public string VibDesc { get; set; }

        public virtual ICollection<Tag> Tags { get; set; }
    }
}
