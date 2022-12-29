using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ProDat.Web2.Models
{
    public class Rbm
    {
        public Rbm()
        {
            Tags = new HashSet<Tag>();
        }

        public int RbmId { get; set; }
        public string RbmName { get; set; }
        public string RbmDesc { get; set; }

        public virtual ICollection<Tag> Tags { get; set; }
    }
}
