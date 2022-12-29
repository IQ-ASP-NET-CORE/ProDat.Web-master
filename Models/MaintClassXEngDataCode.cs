using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ProDat.Web2.Models
{
    // The set of EngData required to be populated for a given MaintClass.
    public class MaintClassXEngDataCode
    {
        public int MaintClassId { get; set; }
        public int EngDataCodeId { get; set; }

        public virtual MaintClass MaintClass { get; set; }
        public virtual EngDataCode EngDataCode { get; set; }
    }
}
