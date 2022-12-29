using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ProDat.Web2.Models
{
    public class CommSubSystem
    {
        public CommSubSystem()
        {
            Tags = new HashSet<Tag>();
        }

        public int CommSubSystemId { get; set; }
        public int SystemId { get; set; }
        
		[Required]
		public string CommSubSystemNo { get; set; }
        public string CommSubSystemName { get; set; }
        public int? Spid { get; set; }

        public virtual Sp Sp { get; set; }
        public virtual Systems Systems { get; set; }
        public virtual ICollection<Tag> Tags { get; set; }
    }
}
