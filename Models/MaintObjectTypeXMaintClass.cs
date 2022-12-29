using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProDat.Web2.Models
{
    public class MaintObjectTypeXMaintClass
    {
        public int MaintObjectTypeId { get; set; }

        public int MaintClassId { get; set; }
        
        public virtual MaintObjectType MaintObjectType { get; set; }

        public virtual MaintClass MaintClass { get; set; }
    }
}
