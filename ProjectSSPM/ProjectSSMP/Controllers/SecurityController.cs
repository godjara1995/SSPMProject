using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using ProjectSSMP.Models;
using ProjectSSMP.Models.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ProjectSSMP.Controllers
{
    public class SecurityController : BaseController
    {
        public SecurityController(sspmContext context) => this.context = context;

        
        public IActionResult Login()
        {

            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(LgoinInputModel inputModel)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            if (!validateuser(inputModel.Username, inputModel.Password))
            {
                ModelState.AddModelError("ErrorLogin", "Username หรือ Password ผิด");
                return View();
            }
            if (!checkstatususer(inputModel.Username))
            {
                ModelState.AddModelError("ErrorLogin", "รหัสของคุณถูกระงับการใช้งาน");
                return View();
            }
          
            // create claims
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, inputModel.Username), new Claim("LastChange", DateTime.Now.ToString())
            };

            // create identity
            var identity = new ClaimsIdentity(claims, "cookie");

            // create principal
            var principal = new ClaimsPrincipal(identity);

            // sign-in
            await HttpContext.SignInAsync(
                    scheme: "FiverSecurityScheme",
                    principal: principal);

            return RedirectToAction("Index", "Home");

        }
        public async Task<IActionResult> Logout(string requestPath)
        {
            await HttpContext.SignOutAsync(
                    scheme: "FiverSecurityScheme");

            return RedirectToAction("Login");
        }
        private bool validateuser(string user , string pass)
        {
            var userid = (from u in context.UserSspm where u.Username.Equals(user) select u).FirstOrDefault();
            if (userid == null)
            {
                return false;
            }
                
            if(userid.Password != pass)
            {
                return false;
            }
            return true;
        }
        private bool checkstatususer(string user)
        {
            var userid = (from u in context.UserSspm where u.Username.Equals(user) select u).FirstOrDefault();
            if(userid.Status == "D")
            {
                return false;
            }

            return true;
        }

    }
}