using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace _2Laboras
{
    /// <summary>
    /// Constructor class
    /// </summary>
    public class Profesor
    {
        public List<Projects> Project { get; }
        public string Surname { get; }
        public string Name { get; }

        public Profesor(string surname, string name, 
            string project, int hours)
        {
            this.Surname = surname;
            this.Name = name;

            Projects projects = new Projects(project, 
                hours);

            this.Project = new List<Projects>();
            this.Project.Add(projects);
        }

        /// <summary>
        /// Adds a project to the list
        /// </summary>
        /// <param name="projects"></param>
        public void AddProject(List<Projects> projects)
        {
            foreach (Projects project in projects)
            {
                this.Project.Add(project);
            }
        }

        /// <summary>
        /// Operator overload for sorting
        /// </summary>
        /// <param name="one"></param>
        /// <param name="two"></param>
        /// <returns></returns>
        static public bool operator >(Profesor one, 
            Profesor two)
        {
            int temp = one.Surname.CompareTo(two.Surname);

            if (temp == 0)
            {
                if (one.Name.CompareTo(two.Name) > 0)
                {
                    return true;
                }

                return false;
            }
            else if (temp > 0)
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// Operator overload for sorting
        /// </summary>
        /// <param name="one"></param>
        /// <param name="two"></param>
        /// <returns></returns>
        static public bool operator <(Profesor one, 
            Profesor two)
        {
            if (one == null)
            {
                return true;
            }

            int temp = one.Surname.CompareTo(two.Surname);

            if (temp == 0)
            {
                if (one.Name.CompareTo(two.Name) < 0)
                {
                    return true;
                }

                return false;
            }
            else if (temp < 0)
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// ToString override for printing
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            string line = "";

            line = String.Format($"| {this.Surname,-9} " +
                $"| {this.Name,-9} | {this.Project[0].Project,-22} " +
                $"| {this.Project[0].Hours,11} |");

            return line;
        }
    }
}