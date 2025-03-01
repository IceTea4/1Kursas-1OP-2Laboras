using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace _2Laboras
{
    /// <summary>
    /// Linked List class
    /// </summary>
    public sealed class LinkList
    {
        /// <summary>
        /// Class for a Linked List Node
        /// </summary>
        private sealed class Node
        {
            public Profesor Data { get; set; }
            public Node Link { get; set; }

            public Node(Profesor value, Node address)
            {
                this.Data = value;
                this.Link = address;
            }
        }

        private Node head;
        private Node tail;
        private Node headFifo;
        private Node d;

        public LinkList()
        {
            this.tail = new Node(null, null);
            this.head = new Node(null, tail);
            headFifo = head;
            this.d = null;
        }

        /// <summary>
        /// Adding elements to the linked list 
        /// (stacking, Last in first out))
        /// </summary>
        public void SetLifo(Profesor profesor)
        {
            head.Link = new Node(profesor, head.Link);

            if (head == headFifo)
            {
                headFifo = head.Link;
            }
        }

        /// <summary>
        /// Adding elements to the linked list 
        /// (First in first out)
        /// </summary>
        public void SetFifo(Profesor profesor)
        {
            headFifo.Link = new Node(profesor, tail);
            headFifo = headFifo.Link;
        }

        /// <summary>
        /// Begining of the linked list
        /// </summary>
        public void Begin()
        {
            d = head.Link;
        }

        /// <summary>
        /// For looping the next element in the 
        /// linked list
        /// </summary>
        public void Next()
        {
            d = d.Link;
        }

        /// <summary>
        /// Checks if there is an actual element
        /// </summary>
        /// <returns></returns>
        public bool Exist()
        {
            return d != null;
        }

        /// <summary>
        /// Gets the profesor
        /// </summary>
        /// <returns></returns>
        public Profesor Get()
        {
            return d.Data;
        }

        /// <summary>
        /// Checks if linked list is not empty
        /// </summary>
        /// <returns></returns>
        public bool Check()
        {
            return headFifo.Data != null;
        }

        /// <summary>
        /// Makes a new linked list only with 
        /// chosen projects
        /// </summary>
        /// <param name="students"></param>
        /// <returns></returns>
        public LinkList RemoveUnselected(List<Student> 
            students)
        {
            LinkList selectedProfesors = new LinkList();

            foreach (Student student in students)
            {
                for (Node d = head.Link; d != null; d = d.Link)
                {
                    if (d.Data != null 
                        && d.Data.Project[0].Project 
                        == student.ProjectName)
                    {
                        selectedProfesors.SetLifo(d.Data);
                        break;
                    }
                }
            }

            return selectedProfesors;
        }

        /// <summary>
        /// Merges projects if the same professor is 
        /// responsible for them
        /// </summary>
        public void MergeSameProf()
        {
            for (Node d = head.Link; d != null; d = d.Link)
            {
                for (Node j = d.Link; j != null; j = j.Link)
                {
                    if (d.Data != null && j.Data != null 
                        && d.Data.Name == j.Data.Name 
                        && d.Data.Surname == j.Data.Surname)
                    {
                        d.Data.AddProject(j.Data.Project);
                        d.Link = j.Link;
                        j.Link = null;
                    }
                }
            }
        }

        /// <summary>
        /// Returns the professor which has the most projects
        /// </summary>
        /// <returns></returns>
        public Profesor HasTheMostProjects()
        {
            int count = 0;
            Profesor profesor = null;

            for (Node d = head; d != null; d = d.Link)
            {
                if (d.Data != null && d.Data.Project.Count > count)
                {
                    count = d.Data.Project.Count;
                    profesor = d.Data;
                }
            }

            return profesor;
        }

        /// <summary>
        /// Sorts professors by surname and name
        /// </summary>
        public void Sort()
        {
            for (Node d1 = head.Link; d1 != null; d1 = d1.Link)
            {
                Node minv = d1;

                for (Node d2 = d1.Link; d2 != null; d2 = d2.Link)
                {
                    if (d1.Data != null && d2.Data < minv.Data)
                    {
                        minv = d2;
                    }

                    Profesor profesor = d1.Data;
                    d1.Data = minv.Data;
                    minv.Data = profesor;
                }
            }
        }
    }
}