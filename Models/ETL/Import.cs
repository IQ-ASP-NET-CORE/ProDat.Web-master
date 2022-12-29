using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ProDat.Web2.Models.ETL
{
    public class Import
    {
        public int ImportId { get; set; }

        public string ImportStatus { get; set; } // using string as value is ever set by application only.

        public int ImportTypeId { get; set; }


        public DateTime Created { get; set; }

        public string CreatedBy { get; set; }

        [Required]
        public string CreatedComment { get; set; }

        public DateTime Approved { get; set; }
        
        public string ApprovedBy { get; set; }

        
        public string ApprovedComment { get; set; }

        [NotMapped]
        public string ResponseMessage { get; set; }

        [NotMapped]
        public IFormFile File { get; set; }

        public virtual ImportType ImportType { get; set; }


    }
}
