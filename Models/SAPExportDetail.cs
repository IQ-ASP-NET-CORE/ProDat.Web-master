using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ProDat.Web2.Models
{
    public class SAPExportDetail
    {

        public int SAPExportDetailId { get; set; }
		
		[Required]
        [Display(Name ="Internal Name")]
        public string OutputName { get; set; }

        [Required]
        [Display(Name = "Report Name")]
        public string FileName { get; set; }

        [Display(Name = "Sheet Name")]
        public string SheetName { get; set; }

        [Display(Name = "Column Order")]
        public int ColumnOrder { get; set; }

        [Display(Name = "Column Header Legible")]
        public string ColumnHeader_Legible { get; set; }

        [Display(Name = "Column Header SAP")]
        public string ColumnHeader_SAP { get; set; }

        [Display(Name = "PathName")]
        public string PathName { get; set; }

        // not sure we need below here. Validation rules are stored in EntityAttribute, EntityAttributeRequirement.
        // how do these affect the output report?
        [Display(Name = "Data Type")]
        public string DataType { get; set; }

        [Display(Name = "Limit")]
        public int limit { get; set; }

        [Display(Name = "Mandatory")]
        public string Mandatory { get; set; }


        // FK Entities 1:1
        //public virtual EnvZone EnvZone { get; set; }


        // FK Entities 1:*
        //public virtual ICollection<FlocXmaintItem> FlocXmaintItems { get; set; }

    }
}
