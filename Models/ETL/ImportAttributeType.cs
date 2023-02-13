using System;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ProDat.Web2.Models.ETL
{
    public class ImportAttributeType
    {
        public int ImportAttributeTypeId { get; set; }
        public string ImportTypeName { get; set; }
        public string StarAttributeName { get; set; }
        public int EntityId { get; set; }
        public string StarType { get; set; }
    }
}
