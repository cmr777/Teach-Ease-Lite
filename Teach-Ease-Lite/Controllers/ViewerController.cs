using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Teach_Ease_Lite.Controllers
{
    public class ViewerController : Controller
    {
        // GET: Viewer
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult CourseListPanel()
        {
            return PartialView("_PartialCourseListPanel");
        }
        [HttpPost]
        public ActionResult CourseCategories()
        {
            return PartialView("_PartialCourseCategories");
        }
        [HttpPost]
        public ActionResult CourseList()
        {
            return PartialView("_PartialCourseList");
        }
        [HttpPost]
        public ActionResult CourseDetail()
        {
            return PartialView("_PartialCourseDetail");
        }
    }
}