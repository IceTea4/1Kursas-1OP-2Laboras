using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using System.IO;

namespace _2Laboras
{
    /// <summary>
    /// Reading and printing class
    /// </summary>
    public class InOut
    {
        /// <summary>
        /// Reads professors and their info from the file
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public static LinkList ReadProfesorLifo(string fileName)
        {
            string projectName;
            string surname;
            string name;
            int hours;
            string line;
            LinkList list = new LinkList();

            using (var file = 
                new System.IO.StreamReader(fileName, Encoding.UTF8))
            {
                while ((line = file.ReadLine()) != null)
                {
                    string[] values = line.Split(new char[] { ';' }, 
                        StringSplitOptions.RemoveEmptyEntries);

                    projectName = values[0];

                    string[] strings = values[1].Split(new char[] { ' ' }, 
                        StringSplitOptions.RemoveEmptyEntries);

                    surname = strings[0];
                    name = strings[1];
                    hours = int.Parse(strings[2]);

                    Profesor profesor = new Profesor(surname, name, 
                        projectName, hours);

                    list.SetLifo(profesor);
                }
            }

            return list;
        }

        /// <summary>
        /// Reads info about students
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public static List<Student> ReadStudents(string fileName)
        {
            List<Student> students = new List<Student>();

            string name;
            string surname;
            string group;
            string project;
            string line;

            using (var file = new System.IO.StreamReader(fileName, 
                Encoding.UTF8))
            {
                while ((line = file.ReadLine()) != null)
                {
                    string[] values = line.Split(new char[] { ';' }, 
                        StringSplitOptions.RemoveEmptyEntries);

                    project = values[0];

                    string[] strings = values[1].Split(new char[] { ' ' }, 
                        StringSplitOptions.RemoveEmptyEntries);

                    surname = strings[0];
                    name = strings[1];
                    group = strings[2];

                    Student student = new Student(project, surname, 
                        name, group);

                    students.Add(student);
                }
            }

            return students;
        }

        /// <summary>
        /// Prints professors with their info to the file in a table
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="profesors"></param>
        /// <param name="heading"></param>
        public static void PrintProfesors(string fileName, 
            LinkList profesors, string heading)
        {
            File.AppendAllText(fileName, $"{heading}\r\n", Encoding.UTF8);

            List<string> lines = new List<string>();

            lines.Add(new string('-', 64));
            lines.Add(String.Format($"| {"Pavardė",-9} | {"Vardas",-9} " +
                $"| {"Projekto pavadinimas",-22} | {"Valandų sk.",-11} |"));
            lines.Add(new string('-', 64));

            for (profesors.Begin(); profesors.Exist(); profesors.Next())
            {
                if (profesors.Get() != null 
                    && profesors.Get().Project.Count == 1)
                {
                    lines.Add(profesors.Get().ToString());
                    lines.Add(new string('-', 64));
                }
                else if (profesors.Get() != null)
                {
                    lines.Add(String.Format($"| {profesors.Get().Surname,-9} " +
                        $"| {profesors.Get().Name,-9} | " +
                        $"{profesors.Get().Project[0].Project,-22} | " +
                        $"{profesors.Get().Project[0].Hours,11} |"));

                    for (int i = 1; i < profesors.Get().Project.Count; i++)
                    {
                        lines.Add(String.Format($"| {"",-9} | {"",-9} | " +
                            $"{profesors.Get().Project[i].Project,-22} | " +
                            $"{profesors.Get().Project[i].Hours,11} |"));
                    }

                    lines.Add(new string('-', 64));
                }

            }

            lines.Add("");

            File.AppendAllLines(fileName, lines, Encoding.UTF8);
        }

        /// <summary>
        /// Print students with their info to the file in a table
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="students"></param>
        public static void PrintStudents(string fileName, List<Student> 
            students)
        {
            List<string> lines = new List<string>();

            lines.Add(new string('-', 64));
            lines.Add(String.Format($"| {"Pavardė",-9} | {"Vardas",-9} " +
                $"| {"Projekto pavadinimas",-22} | {"Grupė",-11} |"));
            lines.Add(new string('-', 64));

            foreach (Student student in students)
            {
                lines.Add(String.Format($"| {student.Surname,-9} " +
                    $"| {student.Name,-9} | {student.ProjectName,-22} " +
                    $"| {student.Group,-11} |"));

                lines.Add(new string('-', 64));
            }

            lines.Add("");

            File.AppendAllLines(fileName, lines, Encoding.UTF8);
        }
    }
}