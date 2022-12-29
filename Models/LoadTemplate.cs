using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ProDat.Web2.Models
{
    public class LoadTemplate
    {
        public LoadTemplate()
        {
            MaintLoads = new HashSet<MaintLoad>();
        }

        public int LoadTemplateId { get; set; }
        public string LoadTemplateName { get; set; }
        public string LoadTemplateDesc { get; set; }

        public virtual ICollection<MaintLoad> MaintLoads { get; set; }
    }
}
