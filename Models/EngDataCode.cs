using System; 
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ProDat.Web2.Models
{
    public class EngDataCode
    {
        public EngDataCode()
        {
            //TagEngDatas = new HashSet<TagEngData>();
        }

        public int EngDataCodeId { get; set; }

        [Required]
        [Display(Name = "Attribute Name")]
        [Remote(action: "ValidateName", controller: "EngDataCodes", ErrorMessage = "Name exists.")]
        public string EngDataCodeName { get; set; }

        [Display(Name = "Attribute Desc.")]
        public string EngDataCodeDesc { get; set; }

        [Display(Name = "Notes")]
        public string EngDataCodeNotes { get; set; }

        [DefaultValue(false)]
        public bool HideFromUI { get; set; }

        [MaxLength(255)]
        public string EngDataCodeSAPDesc { get; set; }

        // Not sure what this is for? to assist with converting string value to another datatype? e.g. int, double, date?
        [MaxLength(255)]
        public string EngDataCodeDDLType { get; set; }

        public int EngDataClassId { get; set; }

        public virtual EngDataClass EngDataClasss { get; set; }

        public virtual ICollection<TagEngData> TagEngDatas { get; set; }

        public virtual ICollection<EngDataCodeDropDown> TagEngDataCodeDropdowns { get; set; }

        public virtual ICollection<MaintClassXEngDataCode> MaintClassXEngDataCode { get; set; }

        public virtual ICollection<KeyListxEngDataCode> KeyListxEngDataCodes { get; set; }

        public virtual ICollection<EngDataClassxEngDataCode> EngDataClassxEngDataCodes { get; set; }
        public int? KeyListId { get; internal set; }
    }
}
