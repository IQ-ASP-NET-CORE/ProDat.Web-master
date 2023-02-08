using DevExtreme.AspNet.Mvc;
using DevExtreme.AspNet.Data;
//using System;
//using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ProDat.Web2.Data;
using System.Linq.Dynamic.Core;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using Newtonsoft.Json;
using ProDat.Web2.ViewModels;
using Microsoft.AspNetCore.Authorization;

namespace ProDat.Web2.Controllers
{
    public class UC5Controller : Controller
    {
        #region instantiate controller
        private readonly TagContext _context;

        public UC5Controller(TagContext context)
        {
            _context = context;
        }
        #endregion

        public IActionResult Index()
        {
            // confirm user has mfa, else redirect to MFA setup.
            var claimTwoFactorEnabled =
               User.Claims.FirstOrDefault(t => t.Type == "amr");

            if (claimTwoFactorEnabled != null && "mfa".Equals(claimTwoFactorEnabled.Value))
            {
                // continue
            }
            else
            {
                return Redirect(
                    // Modified by MWM
                    "/Identity/Account/Login");
                //"/Identity/Account/Manage/TwoFactorAuthentication");
            }

            ViewBag.GlobalProjectDescription = _context.Project.First().ProjectName;
            return View();
        }

        public IActionResult ColumnsBasedADataSource() {
        return View();
        }

    } // controller
}
