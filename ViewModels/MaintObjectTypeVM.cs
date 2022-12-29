using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ProDat.Web2.ViewModels
{
    public class MaintObjectTypeVM
    {
        public MaintObjectTypeVM(ProDat.Web2.Models.MaintObjectType rec)
        {
            MaintObjectTypeId = rec.MaintObjectTypeId;
            MaintObjectTypeName = rec.MaintObjectTypeName;
            MaintObjectTypeDesc = rec.MaintObjectTypeDesc;
            MaintObjectTypeDescExt = rec.MaintObjectTypeDescExt;
            StdNounModifier = rec.StdNounModifier;
        }

        public int MaintObjectTypeId { get; set; }

        [Required]
        [Display(Name = "MaintObjectType Name")]
        public string MaintObjectTypeName { get; set; }

        [Display(Name = "MaintObjectType Desc")]
        public string MaintObjectTypeDesc { get; set; }

        [Display(Name = "MaintObjectType Desc Ext")]
        public string MaintObjectTypeDescExt { get; set; }

        [Display(Name = "Std Noun Modifier")]
        public string StdNounModifier { get; set; }
    }
}
