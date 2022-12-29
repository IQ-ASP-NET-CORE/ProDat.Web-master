using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProDat.Web2.TagLibrary
{
    public class ViewColsData
    {
        public int viewId { get; set; }

        // e.g. EngDiscId
        public string fkField { get; set; }

        // Required as not always reducible from fkField e.g. KeyDocId
        public string entityName { get; set; }
    
        // S M L
        public string fieldType { get; set; }

        public int width { get; set; }


        public ViewColsData(int viewId, string fkField, string entityName, string fieldType, int width)
        {
            this.viewId = viewId;
            this.fkField = fkField;
            this.entityName = entityName;
            this.fieldType = fieldType;
            this.width = width;
        }

        public ViewColsData()
        {
            this.viewId = -1;
            this.fkField = null;
            this.entityName = null;
            this.fieldType = null;
            this.width = -1;
        }
    }
}
