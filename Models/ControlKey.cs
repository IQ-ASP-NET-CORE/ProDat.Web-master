using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ProDat.Web2.Models
{
    public class ControlKey
    {
        public int ControlKeyId { get; set; }
        public string ControlKeyName { get; set; }
        public string ControlKeyDesc { get; set; }
    }
}
