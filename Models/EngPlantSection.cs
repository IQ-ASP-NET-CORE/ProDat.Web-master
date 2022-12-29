using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ProDat.Web2.Models
{
    public class EngPlantSection
    {


        
        public int EngPlantSectionId { get; set; }
        public string EngPlantSectionName { get; set; }
        public int? EngPlantId { get; set; }
        public string Longitude { get; set; }
        public string Latitude { get; set; }


        public virtual EngPlant EngPlant { get; set; }

        public IEnumerable<Area> Areas { get; set; }


    }
}