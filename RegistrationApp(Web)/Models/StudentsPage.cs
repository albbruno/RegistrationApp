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
        public List<Student> AllStudents = new List<Student>();

        public int StudentId { get; set; }


        public bool FindStudent(int id, out Student student)
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
            SetStudent();
        }

        public bool SetStudent()
        {
            bool check = false;
            string qry = $"SELECT * FROM Student";

            using (SqlConnection sqlcon = new SqlConnection("Data Source=al-database.ctprr3whvxjs.us-west-2.rds.amazonaws.com,1433;Initial Catalog=RegAppDB;Persist Security Info=True;User ID=aldatabase;Password=aldatabase2248;Encrypt=false;"))
            {
                SqlCommand cmd = new SqlCommand(qry, sqlcon);

                try
                {
                    sqlcon.Open();
                    SqlDataReader dReader = cmd.ExecuteReader();

                    while (dReader.Read())
                    {
                        int tmpColumnCount = dReader.FieldCount;

                        AllStudents.Add(new Student((int)dReader["StudentId"], (string)dReader["Name"], (int)dReader["TypeId"], new List<iCourse>()));
                    }
                    dReader.Close();
                    sqlcon.Close();
                    check = true;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    check = false;
                }

            }

            return check;
        }
    }
}