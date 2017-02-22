using RegistrationApp_Web_.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RegistrationApp_Web_.Controllers
{
    public class AdministratorController : Controller
    {
        // GET: Administrator
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Home()
        {

            return View();
        }

        [HttpGet]
        public ActionResult StudentsPage()
        {
            StudentsPage mdl = new StudentsPage();

            mdl.Set();

            return View(mdl);
        }

        [HttpPost]
        public ActionResult StudentsPage(StudentsPage Mdl)
        {
            University.Student tmpStd;
            Mdl.Set();

            if (Mdl.FindStudent(Mdl.StudentId, out tmpStd))
            {
                StudentPage stdPMdl = new StudentPage();

                stdPMdl.aStudent = tmpStd;

                stdPMdl.StudentId = Mdl.StudentId;

                return RedirectToAction("StudentPage", stdPMdl);
            };

            return View(Mdl);
        }

        public ActionResult StudentPage(StudentPage mdl)
        {


            if (mdl.AddRemoveStr == "ADD")
            {
                try
                {
                    if(!mdl.ConflictingCourse(mdl.CourseId))
                    {
                        mdl.AddCourseToSchedule(mdl.aStudent.StudentId, mdl.CourseId);
                        mdl.DisplayMessege = "";
                    }
                }
                catch(Exception ex)
                {
                    mdl.DisplayMessege = ex.Message;
                }
                mdl.Set();
            }
            else if (mdl.AddRemoveStr == "REMOVE")
            {
                mdl.RemoveCourse(mdl.StudentId, mdl.CourseId);
                mdl.Set();
            }

            mdl.Set();

            return View("StudentPage", mdl);
        }

        public ActionResult StudentPage(StudentPage mdl)
        {


            if (mdl.AddRemoveStr == "ADD")
            {
                try
                {
                    if (!mdl.ConflictingCourse(mdl.CourseId))
                    {
                        mdl.AddCourseToSchedule(mdl.aStudent.StudentId, mdl.CourseId);
                        mdl.DisplayMessege = "";
                    }
                }
                catch (Exception ex)
                {
                    mdl.DisplayMessege = ex.Message;
                }
                mdl.Set();
            }
            else if (mdl.AddRemoveStr == "REMOVE")
            {
                mdl.RemoveCourse(mdl.StudentId, mdl.CourseId);
                mdl.Set();
            }

            mdl.Set();

            return View("StudentPage", mdl);
        }
    }
}