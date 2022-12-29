using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace ProDat.Web2.Controllers
{
    public class History : Controller
    {
        // Takes entity name + id as parameter
        public IActionResult Index(int id, string entity="Tag")
        {


            return View();
        }
    }
}
