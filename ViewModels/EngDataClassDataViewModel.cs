using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ProDat.Web2.Models;

namespace ProDat.Web2.ViewModels
{
    public class EngDataClassDataViewModel
    {
        // can be null. Display those which are null in 'Other' MaintClass.
        public string MaintClassId { get; set; }

        public int TagId { get; set; }

        //public string DevExKey { get; set; }

        // Can be null. Display those which are null in 'Other' MaintClass.
        public string MaintClassName { get; set; }

        public int EngDataCodeId { get; set; }

        public string DevExKey { get; set; }

        public string EngDataCodeName { get; set; }

        public string EngDatavalue { get; set; }

        public string EngDataCodeSAPDesc { get; set; }

        //public int EngDataCodeOrder { get; set; }

        // can be null... handle nulls as freetext (can DevExpress do this?)
        //public IEnumerable<EngDataCodeDropDown> EngDataCodeDropDowns {get; set;}

    }
}
