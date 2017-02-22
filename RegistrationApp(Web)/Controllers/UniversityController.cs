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
            return RedirectToAction("Login", "University");
        }

        [HttpGet]
        public ViewResult Login()
        {
            ViewBag.Title = "This Is The Login Page";
            
            return View();
        }

        [HttpPost]
        public ActionResult Login(SignIn loginInfo)
        {
            ViewBag.Title = "TestLoggingIn";

            if (loginInfo.CheckUserInfo(loginInfo.Username, loginInfo.Password))
            {
                currentUser.Username = loginInfo.Username;
                currentUser.Password = loginInfo.Password;
                currentUser.userid = loginInfo.GetUserId(loginInfo.Username);
                currentUser.studentId = loginInfo.GetStudentId(currentUser.userid);



                if (currentUser.studentId > -1)
                {
                    currentUser.Student = loginInfo.GetStudent(currentUser.studentId);
                    return RedirectToAction("Home", "Student");
                }
                else
                    return RedirectToAction("Home", "Administrator"); 
                //redirect to a admin page
            }
            return View(loginInfo);
        }
    }
}