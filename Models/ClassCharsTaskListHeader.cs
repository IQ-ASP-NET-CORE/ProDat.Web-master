using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ProDat.Web2.Models
{
    public class ClassCharsTaskListHeader
    {
        public int ClassCharsTaskListHeaderId { get; set; }
        public string GroupAssociation { get; set; }
        public string CntrAssociation { get; set; }
        public string Class { get; set; }
        public string ClassDesc { get; set; }
        public string Characteristic { get; set; }
        public string CharValue { get; set; }
        public string CharDesc { get; set; }
    }
}
