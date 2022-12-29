using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ProDat.Web2.Models
{
    public class Division
    {
        
        public int DivisionId { get; set; }

        public string DivisionName { get; set; }
        public int? CompanyId { get; set; }

        public virtual Company Company { get; set; }

        public IEnumerable<EngPlant> EngPlants { get; set; }
    }
}
