using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace University
{
    public interface iStudent
    {
        int StudentId { get; }
        string Name { get; }
        int Type { get; }
        Dictionary<string, iCourse> Courses { get; }
    }
}
