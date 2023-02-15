using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ProDat.Web2.Models
{
    public class Doc
    {
        public Doc()
        {
            MaintStrategys = new HashSet<MaintStrategy>();
            Tags = new HashSet<Tag>();
            TagXdocs = new HashSet<TagXdoc>();
        }

        public int DocId { get; set; }

        [Display(Name = "Doc")]
        public string DocNum { get; set; }

        [Display(Name = "Alias")]
        public string DocAlias { get; set; }

        [Display(Name = "Title")]
        public string DocTitle { get; set; }
        public int? DocTypeId { get; set; }
        public string DocLink { get; set; }

        [Display(Name = "Comments")]
        public string DocComments { get; set; }

        [Display(Name = "Source")]
        public string DocSource { get; set; }

        public string DocDisc { get; set; }

        public virtual DocType DocType { get; set; }
        public virtual ICollection<MaintStrategy> MaintStrategys { get; set; }
        public virtual ICollection<Tag> Tags { get; set; }
        public virtual ICollection<TagXdoc> TagXdocs { get; set; }
    }
}
