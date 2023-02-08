using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;

namespace ProDat.Web2.Models.ETL
{
    public class ImportTransform
    {
        /// <summary>
        ///
        /// when Validation passes, records are added to this table.
        /// Content is is here if valid only!
        /// Use a function x(entityName, AttributeName) with assert to ImportError,
        /// to validate content & populate this table.
        ///
        /// </summary>
        public int ImportTransformId { get; set; }

        public int ImportId { get; set; }


        // values required to perform validated sql below.

        // same, changed, new, renamed
        public string LoadType { get; set; }

        //Type of Load. Only Tag exists at this time.
        public string EntityName { get; set; }

        //PK of entity to update (e,g, Tag, etc)
        public int EntityPK { get; set; }

        // used for rename only.
        public string EntityPseudoPK { get; set; }

        // Other modules may require complex key
        public int EntityPK2 { get; set; }

        public string AttributeName { get; set; }
        public string AttributeNameOrg { get; set; }


        // for updating Tag Table. How to report? holds FK not visual for star.
        public string AttributeValue { get; set; }

        // for reporting.
        public string AttributeValueTxt { get; set; }

        public string AttributeValueOldTxt { get; set; }
        // required for reporting and updating Historian.
        public string AttributeValueOld { get; set; }

        public string AttributeValueType { get; set; }

        public virtual Import Import { get; set;}

    }
}
