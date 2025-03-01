using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace _2Laboras
{
    /// <summary>
    /// Constructor class
    /// </summary>
    public class Student
    {
        public string ProjectName { get; }
        public string Surname { get; }
        public string Name { get; }
        public string Group { get; }

        public Student(string projectName, 
            string surname, string name, string group)
        {
            this.ProjectName = projectName;
            this.Surname = surname;
            this.Name = name;
            this.Group = group;
        }
    }
}