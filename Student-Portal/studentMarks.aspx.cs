using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.MobileControls;
using System.Web.UI.WebControls;

namespace Student_Portal
{
    public partial class studentMarks : System.Web.UI.Page
    {
        string enno = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["type"] == null || string.IsNullOrEmpty(Session["type"].ToString()) || Session["type"].ToString() != "Student")
            {
                Response.Redirect("loginpage.aspx");
            }

            enno = Session["Enrollment"].ToString();
            BindWithTable();
        }

        protected void BindWithTable()
        {

            using (SqlConnection con = new SqlConnection("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=F:\\B.TECH\\SEM-5\\WDDN\\Student-Portal\\Student-Portal\\App_Data\\Student_Portal.mdf;Integrated Security=True"))
            {
                string query = "SELECT S.SubjectName, AE.Semester, AE.SessionalExam1, AE.Sessional1LabAttendance, AE.Sessional1LectureAttendance, AE.SessionalExam2, AE.Sessional2LabAttendance, AE.Sessional2LectureAttendance, AE.SessionalExam3, AE.Sessional3LabAttendance, AE.Sessional3LectureAttendance " +
                       "FROM AttendanceAndExam AE " +
                       "INNER JOIN Subjects S ON AE.SubjectID = S.SubjectID " +
                       "WHERE AE.StudentID = @ID";

                using (SqlCommand command = new SqlCommand(query, con))
                {
                    command.Parameters.AddWithValue("@ID", Session["Enrollment"].ToString());

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