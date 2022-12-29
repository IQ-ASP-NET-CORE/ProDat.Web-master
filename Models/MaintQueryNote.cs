using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ProDat.Web2.Models
{
    public class MaintQueryNote
    {
        public MaintQueryNote()
        {
            MaintQuerys = new HashSet<MaintQuery>();
        }

        public int MaintQueryNoteId { get; set; }
        public int? MaintQueryId { get; set; }
        public string MaintQueryNoteBy { get; set; }
        public DateTime? MaintQueryNoteDate { get; set; }
        public string MaintQueryNoteText { get; set; }
        public string MaintQueryNoteAttachments { get; set; }

        public virtual ICollection<MaintQuery> MaintQuerys { get; set; }
    }
}
