using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.Identity;
using System.Web.Mvc;

namespace Identity_Assignment.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult People()
        {
            return View();
        }

        [Authorize(Roles = "Admin")]
        public ActionResult Countries()
        {
            return View();
        }
        
        [Authorize(Roles="Admin")]
        public ActionResult Cities()
        {
            return View();
        }
    }
}