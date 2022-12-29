using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ProDat.Web2.Models
{
    public class ClassCharacteristics
    {
        public int Id { get; set; }
        public string Class { get; set; }
        public string ClassDesc { get; set; }
        public string Characteristic { get; set; }
        public string CharDesc { get; set; }
        public string DropdownTextValue { get; set; }
        public string DropdownValDesc { get; set; }
        public string DropdownText { get; set; }
    }
}
