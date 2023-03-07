using System;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ProDat.Web2.Models
{
    public class NonAllowedWords
    {
        public string? NonAllowedWord { get; set; }
        public string? Comment { get; set; }

    }
}
