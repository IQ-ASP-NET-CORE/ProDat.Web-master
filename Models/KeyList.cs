using System;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ProDat.Web2.Models
{
    public class KeyList
    {
       

        public int KeyListId { get; set; }

        public string KeyListName { get; set; }
                
        public virtual IEnumerable<KeyListxEngDataCode> KeyListxEngDataCodes { get; set; }

        public virtual IEnumerable<KeyListxEngClass> KeyListxEngClasses { get; set; }   

    }
}

