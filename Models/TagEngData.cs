using System; 
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ProDat.Web2.Models
{
    public class TagEngData
    {
        public int TagId { get; set; }
        
        [Display (Name = "Eng Data Code")]
        public int EngDataCodeId { get; set; }

        [Display(Name = "Attribute Value")]
        [Remote(action: "ValidateName", controller: "TagEngDatas", ErrorMessage = "Name exists.")]
        public string EngDatavalue { get; set; }
        
        [Display(Name = "Data Source")]
        public string EngDatasource { get; set; }

        [Display(Name = "Comment")]
        public string EngDataComment { get; set; }

        [Display(Name = "Atribute Name")]
        public virtual EngDataCode EngDataCode { get; set; }

        //public IEnumerable<EngDataCode> EngDataCodes { get; set; }

        public virtual Tag Tag { get; set; }
    }
}
