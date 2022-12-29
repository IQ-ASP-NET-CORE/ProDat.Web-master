using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProDat.Web2.Models.SAPRequirements
{
    public class EntityAttribute
    {
        public int EntityAttributeId { get; set; }
        public string EntityName { get; set; }
        public string EntityAttributeName { get; set; }

        public virtual ICollection<EntityAttributeRequirement> EntityAttributeRequirements { get; set; }

    }
}
