using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProDat.Web2.Models
{
    public class UC2ViewColumns
    {
        public int UC2ViewColumnsId { get; set; }
        public int sectionOrder { get; set; }
        public string sectionName { get; set; }
        public int height { get; set; }
        public int width { get; set; }

        public virtual IEnumerable<UC2ViewColumnsUser> UC2ViewColumnsUser { get; set; }
    }
}
