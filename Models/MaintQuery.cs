using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ProDat.Web2.Models
{
    public class MaintQuery
    {
        public MaintQuery()
        {
            FlocXmaintQuerys = new HashSet<FlocXmaintQuery>();
        }

        public int MaintQueryId { get; set; }
        public int? MaintQueryNoteId { get; set; }
        public string MaintQueryNum { get; set; }
        public string MaintQueryTitle { get; set; }
        public DateTime? MaintQueryRaisedDate { get; set; }
        public string MaintQueryRaisedBy { get; set; }
        public string MaintQueryLongDesc { get; set; }
        public string MaintQueryClosedBy { get; set; }
        public DateTime? MaintQueryClosedDate { get; set; }
        public string MaintQueryClosingNotes { get; set; }

        public virtual MaintQueryNote MaintQueryNote { get; set; }
        public virtual ICollection<FlocXmaintQuery> FlocXmaintQuerys { get; set; }
    }
}
