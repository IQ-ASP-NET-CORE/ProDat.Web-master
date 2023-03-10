using ProDat.Web2.Data;
using ProDat.Web2.Models;
using ProDat.Web2.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;

namespace ProDat.Web2.ViewModels
{
    public class EngDataKeyListViewModel
    {

        public int coloumnNumber { get; set; }
        
        public string alias { get; set; }

        public int engDataCodeId { get; set; }
        
        public string engDataCodeName { get; set; }

        public string engDataCodeDesc { get; set; }

        public string engDataClass { get; set; }

        public int hideFromUI { get; set; }

        public int? KeyListId { get; set; }

        public int Width { get; set; }

        public int Height { get; set; }

        public int? Parent { get; set; }

    }
}
