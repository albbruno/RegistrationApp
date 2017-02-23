using RegistrationApp_Web_.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using University;

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
            iStudent tmpStd;
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
                    bool fullTime;

                    if(!ConnectionObj.CheckStudentCourse(mdl.CourseId, mdl.StudentId, out fullTime))
                    {
                        ConnectionObj.AddCourseToSchedule(mdl.aStudent.StudentId, mdl.CourseId);
                        ConnectionObj.UpdateStudentHour(mdl.aStudent.StudentId, fullTime);


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
                ConnectionObj.RemoveCourse(mdl.StudentId, mdl.CourseId);
                mdl.Set();
            }

            mdl.Set();

            return View("StudentPage", mdl);
        }

        public ActionResult CoursePage()
        {
            CoursesPage mdl = new CoursesPage();
            mdl.Init();
            return View("CoursePage", mdl);
        }

        [HttpGet]
        public PartialViewResult ChooseCourse()
        {
            return  PartialView();
        }

        [HttpPost]
        public PartialViewResult ChooseCourse(StudentCourseList mdl)
        {


            if (mdl.AddRemoveStr == "ADD")
            {
                try
                {
                    bool fullTime;

                    if (!ConnectionObj.CheckStudentCourse(mdl.CourseId, mdl.StudentId, out fullTime))
                    {
                        ConnectionObj.AddCourseToSchedule(mdl.StudentId, mdl.CourseId);
                        ConnectionObj.UpdateStudentHour(mdl.StudentId, fullTime);

                        mdl.DisplayMessege = "";
                    }
                }
                catch (Exception ex)
                {
                    mdl.DisplayMessege = ex.Message;
                }
            }
            else if (mdl.AddRemoveStr == "REMOVE")
            {
                ConnectionObj.RemoveCourse(mdl.StudentId, mdl.CourseId);
            }

            mdl.Students = ConnectionObj.GetStudents(mdl.CourseId);

            return PartialView("StudentRoster", mdl);
        }
    }
}