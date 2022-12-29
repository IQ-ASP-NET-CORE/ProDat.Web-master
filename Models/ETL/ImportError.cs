using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProDat.Web2.Models.ETL
{
    public class ImportError
    {
        public int ImportErrorId { get; set; }
        public int ImportId { get; set; }
        public int? ImportExtractId { get; set; }

        public int? ImportTransformId { get; set; }

        // where assert occured.
        // not using anymore as we want all in one list.
        // options are: 
        //   xls to ImportExtract, 
        //   ImportExtract to ImportTransform,
        //   ImportTransform to EntityName
        public string ErrorVector { get; set; }

        // How validation failed.
        public string ErrorDescription { get; set; }

        public virtual Import Import { get; set; }

        public virtual ImportExtract ImportExtract { get; set; }

        public virtual ImportTransform ImportTransform { get; set; }


    }
}
