using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProDat.Web2.ViewModels
{
    // Used by UC2 and UC3 to constrain width of DataGrid view components
    public class UC2ComponentVM
    {
        public int height { get; set; }
        public int width { get; set; }
        public int? parent { get; set; }
        public string customstring { get; set; }
    }
}
