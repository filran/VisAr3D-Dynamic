using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademicSystem2
{
    class AcademicSystem
    {
        public List<Course> course { get; set; }
        public List<Discipline> disciplines { get; set; }
        public List<Professor> professors { get; set; }
        public List<Student> students { get; set; }


        public AcademicSystem()
        {
            course = new List<Course>();
            disciplines = new List<Discipline>();
            professors = new List<Professor>();
            students = new List<Student>();
        }
    }
}
