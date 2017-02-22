using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace University
{
    public class Student : User, iStudent
    {
        public int StudentId { get { return mId; } }
        public string Name { get { return mName; } }
        public int Type { get { return mType; } }
        public Dictionary<string, iCourse> Courses { get { return mCourses; } }

        private int mId;
        private string mName;
        private int mType;
        private Dictionary<string, iCourse> mCourses;

        public Student()
        {
        }

        public Student(string username, string password, string email, int id, string name, int type, List<iCourse> courses = null) : base(username, password, email)
        {
            mId = id;
            mName = name;
            mType = type;

            mCourses = new Dictionary<string, iCourse>();

            if (courses != null)
                AddCourses(courses);

        }

        public Student(int id, string name, int type, List<iCourse> courses = null)
        {
            mId = id;
            mName = name;
            mType = type;

            mCourses = new Dictionary<string, iCourse>();

            if (courses != null)
                AddCourses(courses);

        }

        public bool AddCourse(iCourse aCourse)
        {
            if (aCourse != null)
            {
                try
                {
                    mCourses.Add(aCourse.Name, aCourse);
                }
                catch(Exception ex)
                {
                }

                return true;
            }
            else
                return false;
        }

        public bool AddCourses(List<iCourse> someCourses)
        {
            foreach (iCourse aCourse in someCourses)
            {
                AddCourse(aCourse);
            }

            return true;
        }


    }
}
