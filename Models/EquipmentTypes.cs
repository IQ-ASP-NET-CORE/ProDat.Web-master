using System;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProDat.Web2.Models
{
    public class EquipmentTypes
    {
        [Key]
        public int EquipTypeID { get; set; }

        public string EquipTypeDesc  { get; set; }

        public int ModelID { get; set; }

        [ForeignKey("ModelID")]
        public virtual Model Model { get; set; }


    }
}
