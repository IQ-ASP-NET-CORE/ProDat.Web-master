using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProDat.Web2.Models
{
    public class TagViewColumns
    {
        public int TagViewColumnsId { get; set; }

        public int TagViewId {get; set;}

        public int TagViewOrder { get; set; }

        public string ColumnName { get; set; }

        public int ColumnWidth { get; set; }

        public string starField { get; set; }
        public virtual IEnumerable<TagViewColumnsUser> TagViewColumnsUser { get; set; }

    }
}
