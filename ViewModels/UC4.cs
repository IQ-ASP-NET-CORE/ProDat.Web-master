using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProDat.Web2.ViewModels
{
    public class UC4
    {
        // will need to store these in db somewhere. 
        public int TagProperties_H { get; set; }

        public int PMAssemblies_H { get; set; }

        public int MaintenanceItems_H { get; set; }

        public int UnassignedTags_H { get; set; }

        public int NonMaintained_H { get; set; }

        public int MaintTree_H { get; set; }

        public int MaintTree_W { get; set; }

        public int TagProperties_W { get; set; }

        public int PMAssemblies_W { get; set; }

        public int MaintenanceItems_W { get; set; }

        public int UnassignedTags_W { get; set; }

        public int NonMaintained_W { get; set; }



    }
}