using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProDat.Web2.Models
{
    public class ColumnSets
    {
        public string ColumnSetsEntity { get; set; }
        public int ColumnSetsId { get; set; }
        public int ColumnSetsOrder { get; set; }
        public string ColumnSetsName { get; set; }
        public string ColumnName { get; set; }

        // attributes we may control
        public int ColumnOrder { get; set; }
        public int ColumnWidth { get; set; }
        public bool ColumnVisible { get; set; }

    }
}