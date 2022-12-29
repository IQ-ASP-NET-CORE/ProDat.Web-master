using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ProDat.Web2.Models
{
    public class MaintLoad
    {
        public MaintLoad()
        {
            FlocXmaintLoads = new HashSet<FlocXmaintLoad>();
        }

        public int MaintLoadId { get; set; }
        public string MaintLoadNum { get; set; }
        public int LoadTemplateId { get; set; }
        public string MaintLoadComment { get; set; }

        public virtual LoadTemplate LoadTemplate { get; set; }
        public virtual ICollection<FlocXmaintLoad> FlocXmaintLoads { get; set; }
    }
}
