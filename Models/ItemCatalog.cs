using System;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using System.IO;

namespace ProDat.Web2.Models
{
    public class ItemCatalog
    {
        [Key]
        public int ItemCatalogID { get; set; }

        public int ItemCatalogClientNum { get; set; }

        public string ItemcatalogDescription { get; set; }
    }
}
