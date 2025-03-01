using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace _2Laboras
{
    /// <summary>
    /// Constructor class
    /// </summary>
    public class Projects
    {
        public string Project { get; }
        public int Hours { get; }

        public Projects(string project, int hour)
        {
            this.Project = project;
            this.Hours = hour;
        }
    }
}