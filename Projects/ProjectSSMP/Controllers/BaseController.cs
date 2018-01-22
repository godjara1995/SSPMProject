using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ProjectSSMP.Models;

namespace ProjectSSMP.Controllers
{
    public class BaseController : Controller
    {
        private readonly sspmContext _context;

        public BaseController(sspmContext context)
        {
            _context = context;
        }
        

    }
}