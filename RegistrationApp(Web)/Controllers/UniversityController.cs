using RegistrationApp_Web_.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using University;

namespace RegistrationApp_Web_.Controllers
{
    public class UniversityController : Controller
    {
        ActiveUser currentUser = ActiveUser.GetInstance;
        // GET: University
        [HttpGet]
        public ActionResult Index()
        {
            ViewBag.Title = "Test";
            currentUser.Clear();
            return RedirectToAction("Login", "University");
        }

        [HttpGet]
        public ViewResult Login()
        {
            ViewBag.Title = "This Is The Login Page";
            currentUser.Clear();
            return View();
        }

        [HttpPost]
        public ActionResult Login(SignIn loginInfo)
        {
            ViewBag.Title = "TestLoggingIn";

            if (ConnectionObj.CheckUserInfo(loginInfo.Username, loginInfo.Password))
            {
                currentUser.Username = loginInfo.Username;
                currentUser.Password = loginInfo.Password;
                currentUser.userid = ConnectionObj.GetUserId(loginInfo.Username);
                currentUser.studentId = ConnectionObj.GetStudentId(currentUser.userid);



                if (currentUser.studentId > -1)
                {
                    currentUser.Student = ConnectionObj.GetStudent(currentUser.studentId);
                    return RedirectToAction("Home", "Student");
                }
                else
                    return RedirectToAction("Home", "Administrator"); 
            }

            return View();
        }
    }
}