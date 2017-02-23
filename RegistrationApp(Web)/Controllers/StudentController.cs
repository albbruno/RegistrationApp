using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using University;
using RegistrationApp_Web_.Models;

namespace RegistrationApp_Web_.Controllers
{
    public class StudentController : Controller
    {
        // GET: Student
        public ActionResult Index()
        {
            return View();
        }

        public ViewResult Home()
        {
            //SetIds();
            ViewBag.Title = "This Is The Home Page";

            StudentHome tmpMdl = new StudentHome();
            tmpMdl.Init();

            return View("StudentHome", tmpMdl);
        }

        public ViewResult CoursePage()
        {
            //SetIds();
            ViewBag.Title = "This Is The Course Page";

            CoursesPage tmpMdl = new CoursesPage();
            tmpMdl.Init();

            return View("CoursePage", tmpMdl);
        }

        [HttpGet]
        public PartialViewResult StudentCourses()
        {
            StudentCourseList tmpMdl = new StudentCourseList();
            tmpMdl.Set();

            return PartialView("StudentCourses", tmpMdl);
        }

        [HttpPost]
        public PartialViewResult StudentCourses(StudentCourseList mdl)
        {
            try
            {
                bool fullTime;

                if (!ConnectionObj.CheckStudentCourse(mdl.CourseId, mdl.CurrentUser.studentId, out fullTime))
                {
                    ConnectionObj.AddCourseToSchedule(mdl.CurrentUser.studentId, mdl.CourseId);
                    ConnectionObj.UpdateStudentHour(mdl.CurrentUser.studentId, fullTime);
                }
                mdl.DisplayMessege = "";
            }
            catch(Exception ex)
            {
                mdl.DisplayMessege = ex.Message;
            }

            mdl.Set();

            return PartialView("StudentCourses", mdl);

        }
    }
}