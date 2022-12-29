using System;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ProDat.Web2.Models
{
    public class KeyListxEngDataCode
    {
        public int KeyListxEngDataCodeId { get; set; }
        public int KeyListId { get; set; }

        [Display(Name = "Eng Data Code")]
        public int EngDataCode { get; set; }

        //public IEnumerable<EngDataCode> EngDataCodes { get; set; }

        public virtual EngDataCode EngDataCodes { get; set; }

        public virtual KeyList KeyList { get; set; }    
    }
}

