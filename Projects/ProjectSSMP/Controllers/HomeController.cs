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
    public class HomeController : Controller
    {
        private readonly sspmContext _context;
        
        public HomeController(sspmContext context)
        {
            _context = context;
        }
        [Authorize]
        public IActionResult Index()
        {
            var loggedInUser = HttpContext.User;
            var loggedInUserName = loggedInUser.Identity.Name;
            var userid = (from u in _context.UserSspm where u.Username.Equals(loggedInUserName) select u).FirstOrDefault();
            
            var userMenu = (from mg in _context.MenuGroup
                            join ma in _context.MenuAuthentication on mg.MenuId equals ma.MenuId
                            join ua in _context.UserAssignGroup on ma.GroupId equals ua.GroupId
                            where ua.UserId.Equals(userid.UserId)
                            select new {
                                 mg.MenuName
                            }).ToList();
            var manuname = (from mg in _context.MenuGroup select mg).ToList();

            ViewBag.userMenu = manuname;
            
            
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
