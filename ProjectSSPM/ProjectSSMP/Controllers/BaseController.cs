using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ProjectSSMP.Models;
using ProjectSSMP.Models.Menu;

namespace ProjectSSMP.Controllers
{
    public class BaseController : Controller
    {
        public sspmContext context;
       

        public List<GetMenuModelcs>  GetMenu()
        {
            var loggedInUser = HttpContext.User;
            var loggedInUserName = loggedInUser.Identity.Name;
            var userid = (from u in context.UserSspm where u.Username.Equals(loggedInUserName) select u).FirstOrDefault();

            var userMenu = (from mg in context.MenuGroup
                            join ma in context.MenuAuthentication on mg.MenuId equals ma.MenuId
                            join ua in context.UserAssignGroup on ma.GroupId equals ua.GroupId
                            where ua.UserId.Equals(userid.UserId)
                            select new GetMenuModelcs
                            {
                                MenuId =mg.MenuId,
                                MenuName = mg.MenuName,
                                MenuUrl  =  mg.MenuUrl,
                                MenuIcon = mg.MenuIcon
                            }).ToList();

            
            return userMenu; ;
        }

    }
}