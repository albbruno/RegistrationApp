using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using University;

namespace RegistrationApp_Web_.Models
{
    public class CoursesPage
    {
        public List<iCourse> AllCourses = new List<iCourse>();

        public void Init()
        {
            AllCourses = ConnectionObj.GetAllCourses();
        }
    }
}