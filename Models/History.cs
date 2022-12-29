using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ProDat.Web2.Models
{
    public class History
    {
        public History()
        {
        }

        public int HistoryId { get; set; }
        public int ImportId { get; set; }
        public int EntityName { get; set; }
        public int Pk1 { get; set; }
        public int Pk2 { get; set; }
        public string AttributeName { get; set; }
        public string AttributeValue { get; set; }
        public DateTime Created { get; set; }        
        public string CreatedBy { get; set; }
    }
}
