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
        public List<Course> AllCourses = new List<Course>();

        public void Init()
        {
            GetCourses();
        }

        private bool GetCourses()
        {

            bool check = false;
            string qry = $"SELECT * FROM Course";

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
                        AllCourses.Add(new Course((int)dReader["CourseId"], (string)dReader["Name"], (TimeSpan)dReader["Time"], (int)dReader["CreditHour"]));
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