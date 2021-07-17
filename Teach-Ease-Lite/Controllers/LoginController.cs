using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Teach_Ease_Lite.Models;

namespace Teach_Ease_Lite.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        [HttpGet]
        public ActionResult SignIn()
        {
            return View();
        }

        [HttpPost]
        public ActionResult SignIn(Login objLogin)
        {
            return RedirectToAction("Index", "Viewer");
        }

        [HttpGet]
        public ActionResult SignUp()
        {
            return View();
        }

        [HttpPost]
        public ActionResult SignUp(Login objLogin)
        {
            return RedirectToAction("Index", "Viewer");
        }

        [HttpGet]
        public ActionResult ForgotPassword()
        {
            return View();
        }

        [HttpPost]
        public ActionResult ForgotPassword(Login objLogin)
        {
            return RedirectToAction("Index", "Viewer");
        }

        [HttpGet]
        public ActionResult InstructorLogin()
        {
            return View();
        }

        [HttpPost]
        public ActionResult InstructorLogin(Login objLogin)
        {
            return RedirectToAction("Index", "Viewer");
        }

        [HttpGet]
        public ActionResult InstructorSignUp()
        {
            return View();
        }

        [HttpPost]
        public ActionResult InstructorSignUp(Login objLogin)
        {
            return RedirectToAction("Index", "Viewer");
        }

    }
}