using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProDat.Web2.Models.ETL
{
    public class ImportExtract
    {
        /// <summary>
        /// Extraction Layer.
        /// Content exists in here after extract function runs without asset!
        /// 
        ///  stores excel import file (FILE) in ready to transform / validate format.
        ///  2. for each row in FILE:
        ///        for each COL in FILE (non special, containing data):
        ///             c) runExtractFunction(rowAsDictionary) (asserts to ImportError, stores to ImportExtract)
        ///         ImportID, function, params
        /// </summary>

        public int ImportExtractId { get; set; }

        public int ImportId { get; set; }

        // EntityName is 1:1 map to a core DataModel. e.g. Tag, TaskListHeader
        public string EntityName { get; set; }

        // PseudoPK = unique string for PK. e.g. 
        // e.g. TagID = getPk(TagName)
        public string EntityPseudoPK { get; set; }

        // 2nd unique String to id complex PKs
        // e.g. TagXDoc
        public string EntityPseudoPK2 { get; set; }


        // Determine Star Information
        // FK2 required to determine Chained Star ID. 
        // e.g: subsystemId = f(subSystemName, systemName)
        public string EntityPseudoFKName { get; set; }
        public string EntityPseudoFKValue { get; set; }


        // not needing these yet... may be able to remove.
        public string EntityPseudoFK2Name { get; set; }
        public string EntityPseudoFK2Value { get; set; }

        // used with Entity Name return function to process row
        public string AttributeName { get; set; }

        // value to set. Will be handled by attributes function, i.e. x(Entity, AttributeName)
        // ignore NULL values, treat [NO VALUE] as action to set to null.
        public string AttributeValue { get; set; }

        public virtual Import Import { get; set; }

        public virtual ImportError ImportError {get; set;}

        public ImportExtract Clone()
        {
            return (ImportExtract)this.MemberwiseClone();
        }

    }
}
