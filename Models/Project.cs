using System;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ProDat.Web2.Models
{
    public class Project
    {
        public Project()
        {
            CommZones = new HashSet<CommZone>();
            Sps = new HashSet<Sp>();
        }

        public int ProjectId { get; set; }

        [Required]
        [Remote(action: "ValidateCode", controller: "Projects", ErrorMessage = "Code exists.")]
        public string ProjectCode { get; set; }

        public string ProjectName { get; set; }
        public int MaintenancePlantId { get; set; }

        public int MaintenanceRootTagId { get; set; }
        public int MaintHierarchy_LoadDepth { get; set; }

        public virtual MaintenancePlant MaintenancePlant { get; set; }
        public virtual ICollection<CommZone> CommZones { get; set; }
        public virtual ICollection<Sp> Sps { get; set; }
    }
}
