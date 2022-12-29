using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProDat.Web2.ViewModels
{
    public class MaintHeirarchyNode
    {
        // this is for TreeView Only; its Id must be unique. 
        // e.g. MI cannot be miId, as multiple associations are allowed.,
        //  so its a complex key of TagId::MiId, for associated objects (that are not tags.)
        public string Id { get; set; }
        
        // This items unique key in the database.
        public string dbId { get; set; }

        public string ParentId { get; set; }
        
        public string Name { get; set; }
        
        // Changes DnD behaviour: and cannot drag content into a node if this is set to false. 
        public bool IsDirectory { get; set; }
        
        // testing...
        public bool hasItems { get; set; }

        // MaintItem, Navigational, Maintainable.
        public string nodeType { get; set; }

        public string icon { get; set; }
        
        public bool IsExpanded { get; set; }
        
        // Overrides Text colour?
        public bool IsDeleted { get; set; }

        public string MaintClassId { get; set; }

        // Determines Text Colour
        public string MaintStatus { get; set; }

        // SAPStatusId
        public int SAPStatusId { get; set; }

        public IEnumerable<MaintHeirarchyNode> Items { get; set; }
    }
}
