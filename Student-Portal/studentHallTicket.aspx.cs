using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Student_Portal
{
    public partial class studentHallTicket : System.Web.UI.Page
    {
        string enno = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["type"] == null || string.IsNullOrEmpty(Session["type"].ToString()) || Session["type"].ToString() != "Student")
            {
                Response.Redirect("loginpage.aspx");
            }

            enno = Session["Enrollment"].ToString();
            //lblenno.Text = enno;
            //lblfname.Text = Session["fname"].ToString();
            //lblname.Text = Session["lname"].ToString();
            BindHallTicket();
        }

        protected void BindHallTicket()
        {
            int userSemester = Convert.ToInt32(Session["semester"]);

            using (SqlConnection con = new SqlConnection("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=F:\\B.TECH\\SEM-5\\WDDN\\Student-Portal\\Student-Portal\\App_Data\\Student_Portal.mdf;Integrated Security=True"))
            {
                string query = "SELECT TicketID, SubjectName, ExamDate, RoomNo FROM HallTicket WHERE SemesterID = @SemesterID";

                using (SqlCommand command = new SqlCommand(query, con))
                {
                    command.Parameters.AddWithValue("@SemesterID", userSemester);

                    SqlDataAdapter adapter = new SqlDataAdapter(command);
                    DataTable dt = new DataTable();

                    con.Open();
                    adapter.Fill(dt);
                    con.Close();

                    SubjectsGridView.DataSource = dt;
                    SubjectsGridView.DataBind();
                }
            }
        }
    }
}