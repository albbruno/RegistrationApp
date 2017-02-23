using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using University;

namespace RegistrationApp_Web_.Models
{
    public class StudentHome
    {

        
        public ActiveUser CurrentUser = ActiveUser.GetInstance;
        public Student currStudent;

        public void Init()
        {

            CurrentUser.Student.AddCourses(ConnectionObj.GetCourses(CurrentUser.studentId));
        }

    }
}
 