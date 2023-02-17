using System;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ProDat.Web2.Models
{
    public class KeyListxDocType
    {
        public int KeyListxDocTypeID { get; set; }
        public int KeyListId { get; set; }
        public int DocTypeId { get; set; }

        public virtual KeyList KeyList { get; set; }
        public virtual DocType DocType { get; set; }

    }
}
