using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademicSystem2
{
    class Course
    {
        public String name { get; set; }
        public List<Discipline> discipline { get; set;}

        public Course()
        {
            discipline = new List<Discipline>();
        }

    }
}
