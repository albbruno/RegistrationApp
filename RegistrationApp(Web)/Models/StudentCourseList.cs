using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using University;

namespace RegistrationApp_Web_.Models
{
    public class StudentCourseList
    {
        public ActiveUser CurrentUser = ActiveUser.GetInstance;

        public int StudentId { get; set; }
        public int CourseId { get; set; }

        public string AddRemoveStr { get; set; }

        public List<iStudent> Students = new List<iStudent>();

        public string DisplayMessege;

        public void Set()
        {
            CurrentUser.Student.AddCourses(ConnectionObj.GetCourses(CurrentUser.studentId));
        }

    }

 
}