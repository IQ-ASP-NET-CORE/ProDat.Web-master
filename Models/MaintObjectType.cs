using System; 
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ProDat.Web2.Models
{
    public class MaintObjectType
    {
        public MaintObjectType()
        {
            Tags = new HashSet<Tag>();
        }

        public int MaintObjectTypeId { get; set; }

		[Required]
		[Display(Name = "Maint. Object Name")]
        [Remote(action: "ValidateName", controller: "MaintObjectTypes", ErrorMessage = "Name exists.")]
        public string MaintObjectTypeName { get; set; }
		
		[Display(Name = "Maint. Object Desc")]
        public string MaintObjectTypeDesc { get; set; }
		
		[Display(Name = "Maint. Object Desc Ext")]
        public string MaintObjectTypeDescExt { get; set; }
        public string StdNounModifier { get; set; }

        //move this....
        public string dlValue
        {
            get
            {
                return MaintObjectTypeName + " - " + MaintObjectTypeDesc + " " + MaintObjectTypeDescExt;
            }
        }

        public virtual ICollection<Tag> Tags { get; set; }

        public virtual ICollection<MaintObjectTypeXMaintClass> MaintObjectTypeXMaintClass { get; set; }

    }
}
