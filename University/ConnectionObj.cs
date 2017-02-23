using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace University
{
    public static class ConnectionObj
    {
        public static bool CheckStudentCourse(int courseId, int studentId, out bool fullTime)
        {
            //do stuff based on course id
            //check if the student has the course by id
            bool check = false;
            iCourse course;
            List<iCourse> allCourse = GetAllCourses();
            List<iCourse> studentsCourses = GetCourses(studentId);
            int creditHourCount = 0;
            fullTime = false;



            foreach (var crs in studentsCourses)
            {
                creditHourCount += crs.CreditHour;
            }

            if(creditHourCount > 6)//hardcoded cap. should be global var
            {
                check = true;
                throw new Exception("Student Course Load Is Filled");

            }

            foreach (iCourse someCourse in studentsCourses)
            {
                if (someCourse.CourseId == courseId)
                {
                    check = true;
                    break;
                }
            }

            if (check == true)
            {
                throw new Exception("This Course is already on your schedule");
            }

            course = ConnectionObj.GetCourse(courseId);

            foreach (iCourse someCourse in studentsCourses)
            {
                if ((someCourse.Time.Hours > course.Time.Hours - someCourse.CreditHour) && (someCourse.Time.Hours < course.Time.Hours + course.CreditHour))
                {
                    check = true;
                    break;
                }
            }

            if (check == true)
            {
                throw new Exception("This conflicts with the timeslots of your current course schedule");
            }



            if(GetRosterCount(courseId) >= 20)//hard coded cap. should be put in course table
            {
                throw new Exception("This course is full");
            }

            if (check == false)
            {
                if ((creditHourCount+course.CreditHour) >= 3)
                {
                    fullTime = true;
                }
                else if ((creditHourCount+ course.CreditHour) < 3)
                {
                    fullTime = false;
                }
            }


            return check;
        }

        public static int GetRosterCount(int courseId)
        {
            int count = 0;

            using (SqlConnection sqlcon = new SqlConnection(ConnectionHelper.conString))
            {

                SqlCommand cmd = new SqlCommand($"GetCourseCount", sqlcon);
                cmd.Parameters.Add(new SqlParameter("@courseId", courseId));

                cmd.CommandType = CommandType.StoredProcedure;
                SqlParameter returnValue = new SqlParameter();
                returnValue.Direction = ParameterDirection.ReturnValue;
                cmd.Parameters.Add(returnValue);
                try
                {
                    sqlcon.Open();
                    int num = cmd.ExecuteNonQuery();
                    count = (int)returnValue.Value;

                }
                catch (Exception ex)
                {
                    Console.WriteLine($"The issue is: {ex.Message}");
                    count = 0;

                }

            }

            return count;

        }
        public static int GetUserId(string username)
        {
            int id = -1;

            using (SqlConnection sqlcon = new SqlConnection(ConnectionHelper.conString))
            {

                SqlCommand cmd = new SqlCommand($"GetUserId", sqlcon);
                cmd.Parameters.Add(new SqlParameter("@username", username));

                cmd.CommandType = CommandType.StoredProcedure;
                SqlParameter returnValue = new SqlParameter();
                returnValue.Direction = ParameterDirection.ReturnValue;
                cmd.Parameters.Add(returnValue);

                try
                {

                    sqlcon.Open();
                    int num = cmd.ExecuteNonQuery();
                    Console.WriteLine($"This is the non query output: {returnValue.Value}");
                    id = (int)returnValue.Value;
                    sqlcon.Close();
                }
                catch (Exception ex)
                {

                    Console.WriteLine($"The issue is: {ex.Message}");
                    id = -1;
                }

            }

            return id;
        }
        public static bool CheckUserInfo(string username, string password)
        {
            bool infoCorrect = false;

            using (SqlConnection sqlcon = new SqlConnection(ConnectionHelper.conString))
            {

                SqlCommand cmd = new SqlCommand($"LoginInfoCheck", sqlcon);
                cmd.Parameters.Add(new SqlParameter("@username", username));
                cmd.Parameters.Add(new SqlParameter("@password", password));

                cmd.CommandType = CommandType.StoredProcedure;
                SqlParameter returnValue = new SqlParameter();
                returnValue.Direction = ParameterDirection.ReturnValue;
                cmd.Parameters.Add(returnValue);


                try
                {
                    sqlcon.Open();
                    cmd.ExecuteNonQuery(); //int num = 
                    Console.WriteLine($"This is the non query output: {returnValue.Value}");
                    infoCorrect = (bool)returnValue.Value;

                    sqlcon.Close();

                }
                catch (Exception ex)
                {

                    Console.WriteLine($"The issue is: {ex.Message}");
                    infoCorrect = false;

                }

            }

            return infoCorrect;
        }

        public static bool UpdateStudentHour(int studentId, bool fullTime)
        {
            bool updated = false;

            using (SqlConnection sqlcon = new SqlConnection(ConnectionHelper.conString))
            {

                SqlCommand cmd = new SqlCommand($"UpdateCreditHour", sqlcon);
                cmd.Parameters.Add(new SqlParameter("@studentId", studentId));
                cmd.Parameters.Add(new SqlParameter("@fullTime", fullTime));

                cmd.CommandType = CommandType.StoredProcedure;

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

        public static List<iStudent> GetAllStudents()
        {
            string qry = $"SELECT * FROM Student";
            List<iStudent> students = new List<iStudent>();

            using (SqlConnection sqlcon = new SqlConnection(ConnectionHelper.conString))
            {
                SqlCommand cmd = new SqlCommand(qry, sqlcon);

                try
                {
                    sqlcon.Open();
                    SqlDataReader dReader = cmd.ExecuteReader();

                    while (dReader.Read())
                    {
                        int tmpColumnCount = dReader.FieldCount;

                        students.Add(new Student((int)dReader["StudentId"], (string)dReader["Name"], (int)dReader["TypeId"], new List<iCourse>()));
                    }
                    dReader.Close();
                    sqlcon.Close();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    students = null;
                }

            }

            return students;
        }
        public static bool RemoveCourse(int student, int course)
        {
            bool updated = false;

            using (SqlConnection sqlcon = new SqlConnection(ConnectionHelper.conString))
            {

                SqlCommand cmd = new SqlCommand($"RemoveFromStudentCourse", sqlcon);
                cmd.Parameters.Add(new SqlParameter("@studentId", student));
                cmd.Parameters.Add(new SqlParameter("@courseId", course));

                cmd.CommandType = CommandType.StoredProcedure;

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
        public static bool AddCourseToSchedule(int student, int course)
        {
            bool updated = false;



            using (SqlConnection sqlcon = new SqlConnection(ConnectionHelper.conString))
            {

                SqlCommand cmd = new SqlCommand($"AddToStudentCourse", sqlcon);
                cmd.Parameters.Add(new SqlParameter("@studentId", student));
                cmd.Parameters.Add(new SqlParameter("@courseId", course));

                cmd.CommandType = CommandType.StoredProcedure;

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
        public static List<iCourse> GetAllCourses()
        {
            string qry = $"SELECT * FROM Course";
            List<iCourse> courses = new List<iCourse>();

            using (SqlConnection sqlcon = new SqlConnection(ConnectionHelper.conString))
            {
                SqlCommand cmd = new SqlCommand(qry, sqlcon);

                try
                {
                    sqlcon.Open();
                    SqlDataReader dReader = cmd.ExecuteReader();

                    while (dReader.Read())
                    {
                        int tmpColumnCount = dReader.FieldCount;
                        courses.Add(new Course((int)dReader["CourseId"], (string)dReader["Name"], (TimeSpan)dReader["Time"], (int)dReader["CreditHour"]));
                    }
                    dReader.Close();
                    sqlcon.Close();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    courses = null;
                }
            }
            return courses;
        }

        public static List<iCourse> GetCourses(int id)
        {
            string qry = $"SELECT * FROM Course INNER JOIN Student_Course ON Course.CourseId = Student_Course.CourseId WHERE Student_Course.StudentId = {id}";
            List<iCourse> coursesList = new List<iCourse>();

            using (SqlConnection sqlcon = new SqlConnection(ConnectionHelper.conString))
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

                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }

            return coursesList;
        }

        public static Student GetStudent(int id)
        {
            string qry = $"SELECT * FROM Student WHERE StudentId = {id}";
            Student std = null;
            using (SqlConnection sqlcon = new SqlConnection(ConnectionHelper.conString))
            {

                SqlCommand cmd = new SqlCommand(qry, sqlcon);

                try
                {
                    sqlcon.Open();

                    SqlDataReader dReader = cmd.ExecuteReader();

                    while (dReader.Read())
                    {
                        int tmpColumnCount = dReader.FieldCount;

                        std = new Student((int)dReader["StudentId"], (string)dReader["Name"], (int)dReader["TypeId"], new List<iCourse>());

                        //std = new Student(CurrentUser.Username, CurrentUser.Password, "", (int)dReader["StudentId"], (string)dReader["Name"], (int)dReader["TypeId"], new List<iCourse>());
                    }
                    dReader.Close();
                    sqlcon.Close();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }

            }

            return std;
        }

        public static int GetStudentId(int userId)
        {
            int id = -1;

            using (SqlConnection sqlcon = new SqlConnection(ConnectionHelper.conString))
            {

                SqlCommand cmd = new SqlCommand($"GetStudentId", sqlcon);
                cmd.Parameters.Add(new SqlParameter("@userId", userId));

                cmd.CommandType = CommandType.StoredProcedure;
                SqlParameter returnValue = new SqlParameter();
                returnValue.Direction = ParameterDirection.ReturnValue;
                cmd.Parameters.Add(returnValue);

                //cmd.Parameters.Add("@password", SqlDbType.VarChar);
                //cmd.Parameters["@username"].Value = username;
                //cmd.Parameters["@password"].Value = password;

                try
                {
                    sqlcon.Open();
                    int num = cmd.ExecuteNonQuery();
                    Console.WriteLine($"This is the non query output: {returnValue.Value}");
                    id = (int)returnValue.Value;
                    sqlcon.Close();
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"The issue is: {ex.Message}");
                    id = -1;
                }

            }

            return id;
        }

        public static List<iStudent> GetStudents(int cId)
        {

            string qry = $"SELECT * FROM Student INNER JOIN Student_Course ON Student.StudentId = Student_Course.StudentId WHERE Student_Course.CourseId = {cId}";
            List<iStudent> studentList = new List<iStudent>();

            using (SqlConnection sqlcon = new SqlConnection(ConnectionHelper.conString))
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
                        studentList.Add(new Student((int)dReader["StudentId"], (string)dReader["Name"], (int)dReader["TypeId"]));
                    }
                    dReader.Close();
                    sqlcon.Close();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }

            return studentList;
        }

        public static iCourse GetCourse(int id)
        {
            string qry = $"SELECT * FROM Course WHERE CourseId = {id}";
            iCourse course = null;

            using (SqlConnection sqlcon = new SqlConnection(ConnectionHelper.conString))
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
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
            return course;
        }
    }
}
