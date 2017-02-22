using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace University
{
    public interface iCourse
    {
        int CourseId { get; }

        string Name { get; }

        TimeSpan Time { get; }

        int CreditHour { get; }

        List<iStudent> Roster { get; }

    }
}
