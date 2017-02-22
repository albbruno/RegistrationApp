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

        public int CourseId { get; set; }

        public string DisplayMessege;

        public void Set()
        { 
            SetCourses();
        }

        public bool ConflictingCourse(int courseId)
        {
            //do stuff based on course id
            //check if the student has the course by id
            bool check = false;
            int timeCheck = 0;
            iCourse course;


            foreach (KeyValuePair<string, iCourse> someCourse in (CurrentUser.Student.Courses.ToList()))
            {
                if(someCourse.Value.CourseId == courseId)
                {
                    check = true;
                    break;
                }
            }

            if (check == true)
            {
                throw new Exception("This Course is already on your schedule");
            }

            course = GetCourse(courseId);

            foreach (KeyValuePair<string, iCourse> someCourse in (CurrentUser.Student.Courses.ToList()))
            {
                if ((someCourse.Value.Time.Hours > course.Time.Hours - someCourse.Value.CreditHour) && (someCourse.Value.Time.Hours < course.Time.Hours + course.CreditHour))
                {
                    check = true;
                    break;
                }
            }

            if (check == true)
            {
                throw new Exception("This conflicts with the timeslots of your current course schedule");
            }

            return check;
        }

        public bool AddCourseToSchedule(int student, int course)
        {
            bool updated = false;

            using (SqlConnection sqlcon = new SqlConnection("Data Source=al-database.ctprr3whvxjs.us-west-2.rds.amazonaws.com,1433;Initial Catalog=RegAppDB;Persist Security Info=True;User ID=aldatabase;Password=aldatabase2248;Encrypt=false;"))
            {

                SqlCommand cmd = new SqlCommand($"AddToStudentCourse", sqlcon);
                cmd.Parameters.Add(new SqlParameter("@studentId", student));
                cmd.Parameters.Add(new SqlParameter("@courseId", course));

                cmd.CommandType = CommandType.StoredProcedure;

                //cmd.Parameters.Add("@password", SqlDbType.VarChar);

                //cmd.Parameters["@username"].Value = username;
                //cmd.Parameters["@password"].Value = password;

                try
                {
                    sqlcon.Open();
                    int num = cmd.ExecuteNonQuery();
                    updated = true;

                }
                catch (Exception ex)
                {
                    Console.WriteLine($"The issue is: {ex.Message}");
                    updated = false;

                }

            }

            return updated;
        }

        private iCourse GetCourse(int id)
        {

            bool check = false;
            string qry = $"SELECT * FROM Course WHERE CourseId = {id}";
            iCourse course = null;

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
                        course = (new Course((int)dReader["CourseId"], (string)dReader["Name"], (TimeSpan)dReader["Time"], (int)dReader["CreditHour"]));
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
            return course;
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

    }
}