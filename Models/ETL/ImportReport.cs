using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ProDat.Web2.Models.ETL
{
    public class ImportReport
    {

        public int importId { get; set; }

        public string LoadType { get; set; }

        public string EntityPseudoPK { get; set; }

        public string AttributeName { get; set; }

        public string AttributeValue { get; set; }

        public string AttributeValueOld { get; set; }

        public string ErrorDescription { get; set; }

    }
}
