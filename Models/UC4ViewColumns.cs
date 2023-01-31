using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProDat.Web2.Models
{
    public class UC4ViewColumns
    {
        public int UC4ViewColumnsId { get; set; }
        public int sectionOrder { get; set; }
        public string sectionName { get; set; }
        public int height { get; set; }
        public int width { get; set; }

        public virtual IEnumerable<UC4ViewColumnsUser> UC4ViewColumnsUser { get; set; }
    }
}