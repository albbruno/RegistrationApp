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
            //CurrentUser.courseIds = GetCourseIds();
            //SetStudent();
            SetCourses();
        }
        


        private bool SetCourses()
        {

            bool check = false;
            string qry = $"SELECT * FROM Course INNER JOIN Student_Course ON Course.CourseId = Student_Course.CourseId WHERE Student_Course.StudentId = {CurrentUser.studentId}";
            List<iCourse> coursesList = new List<iCourse>();

            using (SqlConnection sqlcon = new SqlConnection("Data Source=al-database.ctprr3whvxjs.us-west-2.rds.amazonaws.com,1433;Initial Catalog=RegAppDB;Persist Security Info=True;User ID=aldatabase;Password=aldatabase2248;Encrypt=false;"))
            {


                SqlCommand cmd = new SqlCommand(qry, sqlcon);

                try
                {
                    sqlcon.Open();
                    //the rest was all setup. this is the juicy bit
                    SqlDataReader dReader = cmd.ExecuteReader();

                    while (dReader.Read())
                    {
                        int tmpColumnCount = dReader.FieldCount;
                        coursesList.Add(new Course((int)dReader["CourseId"], (string)dReader["Name"], (TimeSpan)dReader["Time"], (int)dReader["CreditHour"]));
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


            CurrentUser.Student.AddCourses(coursesList);

            return check;
        }

        public List<int> GetCourseIds()
        {
            string qry = $"SELECT * FROM Student_Course";

            List<int> courseIds = null;

            using (SqlConnection sqlcon = new SqlConnection("Data Source=al-database.ctprr3whvxjs.us-west-2.rds.amazonaws.com,1433;Initial Catalog=RegAppDB;Persist Security Info=True;User ID=aldatabase;Password=aldatabase2248;Encrypt=false;"))
            {

                SqlCommand cmd = new SqlCommand(qry, sqlcon);

                try
                {
                    sqlcon.Open();
                    //the rest was all setup. this is the juicy bit
                    SqlDataReader dReader = cmd.ExecuteReader();

                    while (dReader.Read())
                    {
                        int tmpColumnCount = dReader.FieldCount;

                        courseIds.Add((int)dReader["StudentId"]);
                        //Console.Write($"");
                        //your code. write stuff to console
                        //Console.WriteLine($"Bear: {dReader["Name"]} \nWeight: {dReader["Weight"]} \nHome: {dReader["CaveId"]} \n \n");
                    }
                    dReader.Close();
                    sqlcon.Close();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    courseIds = null;
                }

            }

            return courseIds;

        }

        public bool SetStudent()
        {
            bool check = false;
            string qry = $"SELECT * FROM Student WHERE StudentId = {CurrentUser.studentId}";
            Student std = null;
            using (SqlConnection sqlcon = new SqlConnection("Data Source=al-database.ctprr3whvxjs.us-west-2.rds.amazonaws.com,1433;Initial Catalog=RegAppDB;Persist Security Info=True;User ID=aldatabase;Password=aldatabase2248;Encrypt=false;"))
            {

                SqlCommand cmd = new SqlCommand(qry, sqlcon);

                try
                {
                    sqlcon.Open();
                    //the rest was all setup. this is the juicy bit
                    SqlDataReader dReader = cmd.ExecuteReader();

                    while (dReader.Read())
                    {
                        int tmpColumnCount = dReader.FieldCount;
                        
                        currStudent = new Student(CurrentUser.Username, CurrentUser.Password, "",(int)dReader["StudentId"], (string)dReader["Name"], (int)dReader["TypeId"], new List<iCourse>());
                        //Console.Write($"");
                        //your code. write stuff to console
                        //Console.WriteLine($"Bear: {dReader["Name"]} \nWeight: {dReader["Weight"]} \nHome: {dReader["CaveId"]} \n \n");
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
 