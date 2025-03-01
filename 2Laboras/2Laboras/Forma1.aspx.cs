using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace _2Laboras
{
    public partial class Forma1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Page.IsPostBack)
            {
                Profesor profesor1 = null;
                LinkList list = (LinkList)Session["visi"];

                profesor1 = TaskUtils.FindChosenProf(TextBox1.Text, 
                    TextBox2.Text, list);

                Session["pasirinktas"] = profesor1;
            }

            Label1.Visible = false;
            Label2.Visible = false;
            Label3.Visible = false;
            Table1.Visible = false;
            Table2.Visible = false;
            Table3.Visible = false;
            Button2.Visible = false;
            Label4.Visible = false;
            Label5.Visible = false;
            Label6.Visible = false;
            TextBox1.Visible = false;
            TextBox2.Visible = false;
        }

        protected void Button1_Click(object sender, 
            EventArgs e)
        {
            LinkList profesors = 
                InOut.ReadProfesorLifo(Server.MapPath("App_Data/U22b.txt"));
            LinkList list = 
                InOut.ReadProfesorLifo(Server.MapPath("App_Data/U22b.txt"));

            List<Student> students = 
                InOut.ReadStudents(Server.MapPath("App_Data/U22a.txt"));

            File.Delete(Server.MapPath("Rezultatai.txt"));

            InOut.PrintProfesors(Server.MapPath("Rezultatai.txt"), 
                profesors, "Pradiniai duomenys:");
            InOut.PrintStudents(Server.MapPath("Rezultatai.txt"), 
                students);

            Table1Header();
            Table2Header();
            FillTable1(profesors);
            FillTable2(students);

            Button1.Visible = false;
            Label1.Visible = true;
            Label4.Visible = true;
            Label5.Visible = true;
            Table1.Visible = true;
            Table2.Visible = true;
            Button2.Visible = true;
            TextBox1.Visible = true;
            TextBox2.Visible = true;

            profesors.Sort();
            list.Sort();

            if (profesors.Check())
            {
                profesors = profesors.RemoveUnselected(students);
            }

            profesors.Sort();
            list.Sort();

            list.MergeSameProf();
            profesors.MergeSameProf();

            Profesor profesor = profesors.HasTheMostProjects();

            InOut.PrintProfesors(Server.MapPath("Rezultatai.txt"), 
                profesors, "Rezultatai:");

            if (profesor != null)
            {
                File.AppendAllText(Server.MapPath("Rezultatai.txt"), 
                    $"Daugiausiai projektų turintis dėstytojas: " +
                    $"{profesor.Surname} {profesor.Name} " +
                    $"({profesor.Project.Count})\r\n");
            }
            else
            {
                File.AppendAllText(Server.MapPath("Rezultatai.txt"), 
                    $"Nėra profesoriaus su daugiausiai projektų\r\n");
            }

            Session["profesoriai"] = profesors;
            Session["daugiausiai"] = profesor;
            Session["visi"] = list;
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            Profesor profesor = (Profesor)Session["daugiausiai"];
            LinkList profesors = (LinkList)Session["profesoriai"];
            Profesor profesor1 = (Profesor)Session["pasirinktas"];

            Label2.Visible = true;
            Label3.Visible = true;
            Table3.Visible = true;
            Label6.Visible = true;

            Table3Header();
            FillTable3(profesors);
            PrintMostProj(profesor);

            if (profesor1 == null)
            {
                Label6.Text = "Pasirinktas profesorius neegzistuoja";
                File.AppendAllText(Server.MapPath("Rezultatai.txt"), 
                    $"\r\nPasirinktas profesorius neegzistuoja");
            }
            else
            {
                Label6.Text = $"Pasirinktas profesorius: " +
                    $"{profesor1.Surname} {profesor1.Name} " +
                    $"<br />Projektai:<br />{Projektai(profesor1)}";
                File.AppendAllText(Server.MapPath("Rezultatai.txt"), 
                    $"\r\nPasirinktas profesorius: {profesor1.Surname} " +
                    $"{profesor1.Name} \r\nProjektai:\r\n{ProjektaiTxt(profesor1)}");
            }
        }

        /// <summary>
        /// For project printing
        /// </summary>
        /// <param name="profesor"></param>
        /// <returns></returns>
        private string Projektai(Profesor profesor)
        {
            string pro = "";

            foreach (Projects project in profesor.Project)
            {
                pro += project.Project + "<br />";
            }

            return pro;
        }

        /// <summary>
        /// For project printing to the file
        /// </summary>
        /// <param name="profesor"></param>
        /// <returns></returns>
        private string ProjektaiTxt(Profesor profesor)
        {
            string pro = "";

            foreach (Projects project in profesor.Project)
            {
                pro += project.Project + "\r\n";
            }

            return pro;
        }

        /// <summary>
        /// Makes table1 header
        /// </summary>
        private void Table1Header()
        {
            TableCell cell = new TableCell();
            cell.Text = "Profesoriai";
            TableCell one = new TableCell();
            one.Text = "Pavardė ";
            TableCell two = new TableCell();
            two.Text = "Vardas ";
            TableCell three = new TableCell();
            three.Text = "Projektas ";
            TableCell four = new TableCell();
            four.Text = "Valandų sk.";

            TableRow row = new TableRow();
            row.Cells.Add(cell);
            TableRow row2 = new TableRow();
            row2.Cells.Add(one);
            row2.Cells.Add(two);
            row2.Cells.Add(three);
            row2.Cells.Add(four);

            Table1.Rows.Add(row);
            Table1.Rows.Add(row2);
        }

        /// <summary>
        /// Makes table2 header
        /// </summary>
        private void Table2Header()
        {
            TableCell cell = new TableCell();
            cell.Text = "Studentai";
            TableCell one = new TableCell();
            one.Text = "Pavardė ";
            TableCell two = new TableCell();
            two.Text = "Vardas ";
            TableCell three = new TableCell();
            three.Text = "Grupė ";
            TableCell four = new TableCell();
            four.Text = "Projektas";

            TableRow row = new TableRow();
            row.Cells.Add(cell);
            TableRow row2 = new TableRow();
            row2.Cells.Add(one);
            row2.Cells.Add(two);
            row2.Cells.Add(three);
            row2.Cells.Add(four);

            Table2.Rows.Add(row);
            Table2.Rows.Add(row2);
        }

        /// <summary>
        /// Makes table3 header
        /// </summary>
        private void Table3Header()
        {
            TableCell cell = new TableCell();
            cell.Text = "Profesoriai";
            TableCell one = new TableCell();
            one.Text = "Pavardė ";
            TableCell two = new TableCell();
            two.Text = "Vardas ";
            TableCell three = new TableCell();
            three.Text = "Projektas ";
            TableCell four = new TableCell();
            four.Text = "Valandų sk.";

            TableRow row = new TableRow();
            row.Cells.Add(cell);
            TableRow row2 = new TableRow();
            row2.Cells.Add(one);
            row2.Cells.Add(two);
            row2.Cells.Add(three);
            row2.Cells.Add(four);

            Table3.Rows.Add(row);
            Table3.Rows.Add(row2);
        }

        /// <summary>
        /// Fills table1
        /// </summary>
        /// <param name="profesors"></param>
        private void FillTable1(LinkList profesors)
        {
            for (profesors.Begin(); profesors.Exist(); 
                profesors.Next())
            {
                if (profesors.Get() !=  null)
                {
                    TableCell one = new TableCell();
                    one.Text = profesors.Get().Surname;
                    TableCell two = new TableCell();
                    two.Text = profesors.Get().Name;
                    TableCell three = new TableCell();
                    three.Text = profesors.Get().Project[0].Project;
                    TableCell four = new TableCell();
                    four.Text = 
                        profesors.Get().Project[0].Hours.ToString();

                    TableRow row = new TableRow();
                    row.Cells.Add(one);
                    row.Cells.Add(two);
                    row.Cells.Add(three);
                    row.Cells.Add(four);

                    Table1.Rows.Add(row);
                }
            }
        }

        /// <summary>
        /// Fills table2
        /// </summary>
        /// <param name="students"></param>
        private void FillTable2(List<Student> students)
        {
            foreach (Student student in students)
            {
                TableCell one = new TableCell();
                one.Text = student.Surname;
                TableCell two = new TableCell();
                two.Text = student.Name;
                TableCell three = new TableCell();
                three.Text = student.Group;
                TableCell four = new TableCell();
                four.Text = student.ProjectName;

                TableRow row = new TableRow();
                row.Cells.Add(one);
                row.Cells.Add(two);
                row.Cells.Add(three);
                row.Cells.Add(four);

                Table2.Rows.Add(row);
            }
        }

        /// <summary>
        /// Fills table3
        /// </summary>
        /// <param name="profesors"></param>
        private void FillTable3(LinkList profesors)
        {
            for (profesors.Begin(); profesors.Exist(); 
                profesors.Next())
            {
                if (profesors.Get() != null)
                {
                    TableCell one = new TableCell();
                    one.Text = profesors.Get().Surname;
                    TableCell two = new TableCell();
                    two.Text = profesors.Get().Name;
                    TableCell three = new TableCell();
                    three = CellThree(profesors.Get().Project);
                    TableCell four = new TableCell();
                    four = CellFour(profesors.Get().Project);

                    TableRow row = new TableRow();
                    row.Cells.Add(one);
                    row.Cells.Add(two);
                    row.Cells.Add(three);
                    row.Cells.Add(four);

                    Table3.Rows.Add(row);
                }
            }
        }

        /// <summary>
        /// Fills table cell with projects
        /// </summary>
        /// <param name="projects"></param>
        /// <returns></returns>
        private TableCell CellThree(List<Projects> projects)
        {
            TableCell three = new TableCell();

            foreach (Projects project in projects)
            {
                three.Text += project.Project + "<br />";
            }

            return three;
        }

        /// <summary>
        /// Fills table cell with hours
        /// </summary>
        /// <param name="projects"></param>
        /// <returns></returns>
        private TableCell CellFour(List<Projects> projects)
        {
            TableCell four = new TableCell();

            foreach (Projects project in projects)
            {
                four.Text += project.Hours + "<br />";
            }

            return four;
        }

        /// <summary>
        /// Prints the professor which has most projects
        /// </summary>
        /// <param name="profesor"></param>
        private void PrintMostProj(Profesor profesor)
        {
            if (profesor != null)
            {
                Label3.Text = $"Daugiausiai projektų " +
                    $"turintis profesorius: {profesor.Surname} " +
                    $"{profesor.Name} ({profesor.Project.Count})";
            }
            else
            {
                Label3.Text = "Nėra profesoriaus su daugiausiai " +
                    "projektų";
            }
        }
    }
}