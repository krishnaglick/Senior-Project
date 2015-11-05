using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DataAccess.Contexts;
using Models;

namespace SrProj.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Title = "Home Page";

            return View();
        }

        public ViewResult Roles()
        {
            var role = new Role
            {
                ID = 1, RoleDescription = "Non-Admin Use", RoleName = "Volunteer",
            };
            return View(role);
        }
    }
}
