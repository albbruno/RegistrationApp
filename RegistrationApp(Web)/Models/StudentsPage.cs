using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using University;

namespace RegistrationApp_Web_.Models
{
    public class StudentsPage
    {
        public List<iStudent> AllStudents = new List<iStudent>();

        public int StudentId { get; set; }


        public bool FindStudent(int id, out iStudent student)
        {
            bool check = false;
            student = null;

            foreach (var std in AllStudents)
            {
                if (std.StudentId == id)
                {
                    student = std;
                    check = true;
                    break;
                }
            }

            return check;
        }

        public void Set()
        {
            AllStudents = ConnectionObj.GetAllStudents();  
        }
    }
}