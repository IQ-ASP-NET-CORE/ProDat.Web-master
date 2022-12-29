using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using ProDat.Web2.Areas.Identity.Data;

namespace ProDat.Web2.Areas.Identity.Services
{
    public class UserAPIController : Controller
    {
        private readonly UserManager<ProDatWeb2User> _userManager;
        private readonly SignInManager<ProDatWeb2User> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly ILogger<UserAPIController> _logger;

        public UserAPIController(
            UserManager<ProDatWeb2User> userManager,
            SignInManager<ProDatWeb2User> signInManager,
            ILogger<UserAPIController> logger,
            RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _logger = logger;
        }

        [TempData]
        public string StatusMessage { get; set; }

        public async Task<IActionResult> AddUserRole(string Email, string Role)
        {
            var user = await _userManager.FindByEmailAsync(Email);
            if (user == null)
            {
                return NotFound($"Unable to load user {Email}");
            }

            // get role object. 
            var identityRole = await _roleManager.FindByNameAsync(Role);
                        
            await _userManager.AddToRoleAsync(user, identityRole.NormalizedName);
            _logger.LogInformation($"Added {Email} to {Role}.");

            StatusMessage = $"Added {Email} to {Role}.";

            return Ok(StatusMessage);
        }

        public async Task<IActionResult> RemUserRole(string Email, string Role)
        {
            var user = await _userManager.FindByEmailAsync(Email);
            if (user == null)
            {
                return NotFound($"Unable to load user {Email}");
            }

            // get role object. 
            var identityRole = await _roleManager.FindByNameAsync(Role);

            await _userManager.RemoveFromRoleAsync(user, identityRole.NormalizedName);

            _logger.LogInformation($"Removed {Email} from {Role}.");

            StatusMessage = $"Removed {Email} from {Role}.";

            return Ok(StatusMessage);
        }

        public async Task<IActionResult> ResetUserPwd(string Email, string newPwd)
        {
            var user = await _userManager.FindByEmailAsync(Email);
            if (user == null)
            {
                return NotFound($"Unable to load user {Email}");
            }


            // get reset token
            var task = _userManager.GeneratePasswordResetTokenAsync(user);
            task.Wait();
            var token = task.Result;
            
            // reset password
            await _userManager.ResetPasswordAsync(user, token, newPwd);

            _logger.LogInformation($"Reset {Email} password.");

            StatusMessage = $"Reset {Email} password.";

            return Ok(StatusMessage);
        }
    }
}
