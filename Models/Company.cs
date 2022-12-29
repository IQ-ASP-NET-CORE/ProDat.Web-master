using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ProDat.Web2.Models
{
    public class Company
    {
        
        public int CompanyId { get; set; }


        public string CompanyName { get; set; }
               

        public IEnumerable<Division> Divisions { get; set; }



    }
}

