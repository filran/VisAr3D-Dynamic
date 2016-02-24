using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademicSystem2
{
    class Program
    {
        
        static void Main(string[] args)
        {
            //[1] add course
            //[2] add discipline to course
            //[3] add professor
            //[4] add student
            //[5] add professor to discipline
            //[6] add student to discipline

            AcademicSystem system = new AcademicSystem();

        Start:
            Console.Clear();
            Console.WriteLine("----------------------------------------------------------------------------------------------------------------");
            Console.WriteLine("MENU");
            Console.WriteLine("[1] Add Course \t\t [2] Add Discipline \t\t\t [3] Add Professor");
            Console.WriteLine("[4] Add Sutdent \t [5] List");
            Console.WriteLine("----------------------------------------------------------------------------------------------------------------");

            string cmd;
            Console.Write("Digite um comando: ");
            cmd = Console.ReadLine();

            switch(cmd)
            {
                case "1":
                    Console.WriteLine("ADD COURSE");
                    Course course = new Course();
                    Console.Write("Name: ");
                    course.name = Console.ReadLine();
                    system.course.Add(course);
                    goto Start;

                case "2":
                    Console.WriteLine("ADD DISICPLINE");
                    Discipline discipline = new Discipline();
                    Console.Write("Name: ");
                    discipline.name = Console.ReadLine();

                    Console.WriteLine("Courses List:");
                    Dictionary<int, Course> dic_course = new Dictionary<int, Course>();
                    int i = 0;
                    foreach(Course c in system.course)
                    {
                        ++i;
                        Console.WriteLine("["+i+"]\t" + c.name);
                        dic_course.Add(i, c);
                    }
                    Console.Write("Choose one course above: ");
                    int course_choose = Int32.Parse(Console.ReadLine());

                    foreach (KeyValuePair<int, Course> c in dic_course)
                    {
                        if (c.Key == course_choose)
                        {
                            foreach (Course cc in system.course)
                            {
                                if(c.Value == cc)
                                {
                                    //Console.WriteLine("É IGUAL");
                                    cc.discipline.Add(discipline);
                                }
                            }
                        }
                    }
                    system.disciplines.Add(discipline);
                    goto Start;

                case "3":
                    Console.WriteLine("ADD PROFESSOR");
                    Professor prof = new Professor();
                    Console.Write("Name: ");    prof.name = Console.ReadLine();
                    Console.Write("Birth: ");    prof.birth = Console.ReadLine();
                    Console.Write("Address: ");    prof.address = Console.ReadLine();
                    Console.Write("RG: ");    prof.rg = Console.ReadLine();
                    Console.Write("CPF: ");    prof.cpf = Console.ReadLine();

                    Console.WriteLine("Disciplines List:");
                    Dictionary<int, Discipline> dic_discipline = new Dictionary<int, Discipline>();
                    int j = 0;
                    Console.WriteLine("[0]\tExit List");
                    foreach(Discipline d in system.disciplines)
                    {
                        ++j;
                        Console.WriteLine("[" + j + "]\t" + d.name);
                        dic_discipline.Add(j, d);
                    }

                DiscChoose:
                    Console.Write("Choose a discipline: ");
                    int disc_choose = Int32.Parse(Console.ReadLine());

                    foreach (KeyValuePair<int,Discipline> d in dic_discipline)
                    {
                        if (disc_choose == d.Key)
                        {
                            foreach(Discipline dd in system.disciplines )
                            {
                                if(d.Value == dd)
                                {
                                    dd.professor.Add(prof);
                                    goto DiscChoose;
                                }
                            }
                        }
                    }
                    system.professors.Add(prof);
                    goto Start;

                case "4":
                    Console.WriteLine("ADD STUDENT");
                    Student student = new Student();
                    Console.Write("Name: ");    student.name = Console.ReadLine();
                    Console.Write("Birth: ");    student.birth = Console.ReadLine();
                    Console.Write("Address: ");    student.address = Console.ReadLine();
                    Console.Write("RG: ");    student.rg = Console.ReadLine();
                    Console.Write("CPF: ");    student.cpf = Console.ReadLine();

                    Console.WriteLine("Disciplines List:");
                    Dictionary<int, Discipline> dic_discipline2 = new Dictionary<int, Discipline>();
                    int k = 0;
                    Console.WriteLine("[0]\tExit List");
                    foreach(Discipline d in system.disciplines)
                    {
                        ++k;
                        Console.WriteLine("[" + k + "]\t" + d.name);
                        dic_discipline2.Add(k, d);
                    }

                DiscChoose2:
                    Console.Write("Choose a discipline: ");
                    int disc_choose2 = Int32.Parse(Console.ReadLine());

                    foreach (KeyValuePair<int, Discipline> d in dic_discipline2)
                    {
                        if (disc_choose2 == d.Key)
                        {
                            foreach(Discipline dd in system.disciplines )
                            {
                                if(d.Value == dd)
                                {
                                    dd.students.Add(student);
                                    goto DiscChoose2;
                                }
                            }
                        }
                    }
                    system.students.Add(student);
                    goto Start;

                case "5":
                    Console.WriteLine("LIST");
                    foreach(Course c in system.course)
                    {
                        Console.WriteLine("Course: "+c.name);
                        foreach(Discipline d in c.discipline)
                        {
                            Console.WriteLine("\tDiscipline: "+ d.name);
                            foreach(Professor p in d.professor)
                            {
                                Console.WriteLine("\t\tProfessor: "+p.name);
                            }

                            Console.WriteLine("\t\tStudents:");
                            foreach(Student s in d.students)
                            {
                                Console.WriteLine("\t\t\t"+s.name);
                            }
                        }
                    }

                    goto Start;

                //case "6":
                //    goto Start;                

                default:
                    break;
            }
        }
    }
}
