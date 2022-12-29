using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ProDat.Web2.Models
{
    public class EnvZone
    {
        public int EnvZoneId { get; set; }
        public string EnvZoneName { get; set; }
        public string EnvZoneDesc { get; set; }
    }
}
