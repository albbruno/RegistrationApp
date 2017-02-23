using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using University;

namespace RegistrationApp_Web_.Models
{
    public class StudentPage
    {
        public iStudent aStudent;
        public List<iCourse> Courses = new List<iCourse>();
        public int StudentId { get; set; }
        public string AddRemoveStr { get; set; }
        public int CourseId { get; set; }

        public string DisplayMessege;

        public void Set()
        {
            Courses = ConnectionObj.GetCourses(StudentId);
        }
    }

}