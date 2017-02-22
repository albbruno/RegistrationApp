using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace University
{
    public class Course : iCourse
    {
        public int CourseId { get { return mCourseId; } }
        public string Name { get { return mName; } }
        public TimeSpan Time { get { return mTime; } }
        public int CreditHour { get { return mCreditHour; } }
        public List<iStudent> Roster { get { return mRoster; } }

        int mCourseId;
        string mName;
        TimeSpan mTime;
        int mCreditHour;
        List<iStudent> mRoster;

        public Course(int cId, string cName, TimeSpan cTime, int cCreditHour)
        {
            mCourseId = cId;
            mName = cName;
            mTime = cTime;
            mCreditHour = cCreditHour;
        }

        public Course()
        {
        }

        public void AddStudent(iStudent std)
        {


        }

        public void AddStudents(List<iStudent> std)
        {


        }

    }
}
