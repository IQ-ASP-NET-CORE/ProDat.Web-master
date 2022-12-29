using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ProDat.Web2.Models
{
    public class RelationshipToOperation
    {
        public int RelationshipToOperationId { get; set; }
        public string RelationshipToOperationName { get; set; }
        public int? RelationshipTypeId { get; set; }
        public string RelationshipToOperationNotes { get; set; }

        public virtual RelationshipType RelationshipType { get; set; }
    }
}
