using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ProDat.Web2.Models
{
    public class EngPlant
    {

        
        public int EngPlantId { get; set; }
        public string EngPlantName { get; set; }
        public int? DivisionId { get; set; }
        public string Longitude { get; set; }
        public string Latitude { get; set; }

        //public virtual Division Division { get; set; }

        public IEnumerable<EngPlantSection> EngPlantSections { get; set; }
    }
}