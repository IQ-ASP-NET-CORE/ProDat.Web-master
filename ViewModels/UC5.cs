using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProDat.Web2.ViewModels
{
    public class UC5
    {
        // will need to store these in db somewhere.
        public int KeyListId { get; set; }

        public int KeyListName { get; set; }

        public string EngClassName { get; set; }

        public string EngClassDesc { get; set; }

        public string EngDataCodeName { get; set; }

        public int height { get; set; }
        public int width { get; set; }
        public int? parent { get; set; }
        public string customstring { get; set; }

        public string EngDataCodes { get; set; }
        
        public int KeyListAttributes_H { get; set; }
        public int KeyListAttributes_W { get; set; }

        public int UnassignedAtt_H { get; set; }
        public int UnassignedAtt_W { get; set; }
        public int KeyListDocTypes_H { get; set; }
        public int KeyListDocTypes_W { get; set; }
        public int KeyListDocUnass_H { get; set; }
        public int KeyListDocUnass_W { get; set; }
        public int InclClass_H { get; set; }
        public int InclClass_W { get; set; }

        public int UnassClass_H { get; set; }
        public int UnassClass_W { get; set; }


    }
}