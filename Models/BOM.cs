using Org.BouncyCastle.Asn1.Crmf;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ProDat.Web2.Models
{
    public class BOM
    {
        [Key]
        public int BOMID { get; set; }

        public int EquipTypeID { get; set; }
        public int ItemCatalogID { get; set; }
        public int ItemQuantity  { get; set; }

        [ForeignKey("EquipTypeID")]
        public virtual EquipmentTypes EquipmentType { get; set; }
        [ForeignKey("ItemCatalogID")]
        public virtual ItemCatalog ItemCatalog { get; set; }


    }
}
