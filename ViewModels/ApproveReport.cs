using ProDat.Web2.Models.ETL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProDat.Web2.ViewModels
{
    public class ApproveReport
    {
        public ICollection<ImportReport> ImportReport { get; set; }
        public virtual Import Import { get; set; }

    }
}
