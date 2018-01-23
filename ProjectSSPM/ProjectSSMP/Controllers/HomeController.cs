using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProjectSSMP.Models;
using Newtonsoft.Json;


namespace ProjectSSMP.Controllers
{
    public class HomeController : BaseController         
    {
        public HomeController(sspmContext context) => this.context = context;

        [Authorize]
        
        
        public IActionResult Index()
        {
            var loggedInUser = HttpContext.User;
            var loggedInUserName = loggedInUser.Identity.Name;
            var userid = (from u in context.UserSspm where u.Username.Equals(loggedInUserName) select u).FirstOrDefault();

            var userMenu = (from mg in context.MenuGroup
                            join ma in context.MenuAuthentication on mg.MenuId equals ma.MenuId
                            join ua in context.UserAssignGroup on ma.GroupId equals ua.GroupId
                            where ua.UserId.Equals(userid.UserId)
                            select new
                            {
                                mg.MenuName
                            }).ToList();

            

            ViewBag.userMenu = GetMenu("Charoon");
            
            
            return View();
        }

        public IActionResult About()
        {
            
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
