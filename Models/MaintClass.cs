using System; 
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ProDat.Web2.Models
{
    public class MaintClass
    {
        public MaintClass()
        {
            //Tags = new HashSet<Tag>();
        }

        public int MaintClassId { get; set; }

        [Required]
        [Remote(action: "ValidateName", controller: "MaintClasses", ErrorMessage = "Name exists.")]
        public string MaintClassName { get; set; }
        public string MaintClassDesc { get; set; }

        public virtual ICollection<MaintObjectTypeXMaintClass> MaintObjectTypeXMaintClass { get; set;}

        public virtual ICollection<MaintClassXEngDataCode> MaintClassXEngDataCode { get; set; }

        //public virtual ICollection<Tag> Tags { get; set; }
    }
}
