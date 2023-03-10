using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProDat.Web2.ViewModels
{
    // Used by UC5 to build out the view
    public class UC5ComponentVM
    {
        public int height { get; set; }
        public int width { get; set; }
        public int? parent { get; set; }
        public string customstring { get; set; }
    }
}