using System;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ProDat.Web2.Models
{
    public class MaintType
    {
        public MaintType()
        {
            Tags = new HashSet<Tag>();
        }

        public int MaintTypeId { get; set; }

        [Required]
        [Remote(action: "ValidateName", controller: "MaintTypes", ErrorMessage = "Name exists.")]
        public string MaintTypeName { get; set; }
        public string MaintTypeDesc { get; set; }

        public virtual ICollection<Tag> Tags { get; set; }

        //public virtual ICollection<MaintTypeXMaintClass> MaintTypeXMaintClasses { get; set; }
    }
}
