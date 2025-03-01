using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.Linq;

namespace _2Laboras
{
    /// <summary>
    /// Class for calculations
    /// </summary>
    public static class TaskUtils
    {
        /// <summary>
        /// Finds the requested professor
        /// </summary>
        /// <param name="surname"></param>
        /// <param name="name"></param>
        /// <param name="list"></param>
        /// <returns></returns>
        public static Profesor FindChosenProf(string surname, 
            string name, LinkList list)
        {
            Profesor profesor = null;

            if (list != null)
            {
                for (list.Begin(); list.Exist(); list.Next())
                {
                    if (list.Get() != null && surname 
                        == list.Get().Surname && name 
                        == list.Get().Name)
                    {
                        profesor = list.Get();
                        break;
                    }
                }
            }

            return profesor;
        }
    }
}