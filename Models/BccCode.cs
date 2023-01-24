using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ProDat.Web2.Models
{
    public class BccCode
    {
        public int BccCodeId { get; set; }

        [Required]
        public int BccCodeNumber { get; set; }

        public string BccCodeDesc { get;set; }

        public ICollection<EngDataClassxEngDataCode> EngDataClasses { get; set;}

    }
}
