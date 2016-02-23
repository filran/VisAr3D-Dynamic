using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademicSystem2
{
    class Discipline
    {
        public String name { get; set; }
        public List<Professor> professor { get; set; } //ONE professor for each discipline
        public List<Student> students { get; set; } //some students

        public Discipline()
        {
            professor = new List<Professor>();
            students = new List<Student>();
        }

    }
}
