using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProDat.Web2.Models.SAPRequirements
{
    public class EntityAttributeRequirement
    {
        public int EntityAttributeRequirementId { get; set; }

        public int EntityAttributeId { get; set; }

        public string EntityAttributeRequirementType { get; set; }

        public string EntityAttributeRequirementCondition { get; set; }

        public string EntityAttributeRequirementValue { get; set; }

        public string EntityAttributeRequirementValueType { get; set; }

        [JsonIgnore]
        public virtual EntityAttribute EntityAttribute { get; set; }

    }
}
