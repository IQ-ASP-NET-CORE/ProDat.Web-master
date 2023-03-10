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
    public class KeyListInclusiveClassViewModel
    {

        public int ColumnNumber { get; set; }

        public int Alias { get; set; }

        public int EngDataCodeId { get; set; }

        public string EngDataCodeName { get; set; }

        public string EngDataCodeDesc { get; set; }
        public int HideFromUI { get; set; }
        public int BccCodeId { get; set; }

        public string EngClassName { get; set; }

        public int? Parent { get; set; }
        public int Height { get; set; }
        public int Width { get; set; }

    }
}
