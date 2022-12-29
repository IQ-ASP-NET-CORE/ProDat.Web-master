using System;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ProDat.Web2.Models
{
    public class KeyListxEngClass
    {
        public int KeyListxEngClassId { get; set; }
        public int KeyListId { get; set; }

        public int EngClassID { get; set; }

        //public IEnumerable<EngDataCode> EngDataCodes { get; set; }

        public virtual KeyList KeyList { get; set; }

        public virtual EngClass EngClass { get; set; }

    }
}

