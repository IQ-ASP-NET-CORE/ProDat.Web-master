using System; 
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ProDat.Web2.Models
{
    public class RelationshipType
    {
        public RelationshipType()
        {
            RelationshipToOperations = new HashSet<RelationshipToOperation>();
        }

        public int RelationshipTypeId { get; set; }

        [Required]
        [Remote(action: "ValidateName", controller: "RelationShipTypes", ErrorMessage = "Name exists.")]
        public string RelationshipTypeName { get; set; }
        public string RelationshipTypeDesc { get; set; }

        public virtual ICollection<RelationshipToOperation> RelationshipToOperations { get; set; }
    }
}
