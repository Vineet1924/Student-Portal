using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml.Linq;
using System.Web.UI.HtmlControls;

namespace Student_Portal
{
    public partial class hallTicket : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                if (Session["type"] == null || string.IsNullOrEmpty(Session["type"].ToString()) || (Session["type"].ToString() != "Teacher" && Session["type"].ToString() != "Admin"))
                {
                    Response.Redirect("loginpage.aspx");
                }
                PopulateDepartmentDropdown();
            }
        }

        protected void btn_Submit_Click(object sender, EventArgs e)
        {
            foreach (GridViewRow row in SubjectsGridView.Rows)
            {
                if (row.RowType == DataControlRowType.DataRow)
                {
                    Label lblSubjectName = (Label)row.FindControl("lblSubjectName");
                    TextBox txtRoomNo = (TextBox)row.FindControl("txtRoomNo");
                    HtmlInputGenericControl examdateInput = (HtmlInputGenericControl)row.FindControl("examdate");
                    string department = departmentDropdown.SelectedValue;
                    string semester = semesterDropdown.SelectedValue;

                    string subjectName = lblSubjectName.Text;
                    string roomNo = txtRoomNo.Text;
                    string examdate = examdateInput.Value;

                    if (examdate == "")
                    {
                        lbl_error.Text = "Select date";
                        return;
                    }
                    else if (roomNo == "")
                    {
                        lbl_error.Text = "Select room";
                        return;
                    }
                    lbl_error.Text = "";
                    DateTime date = DateTime.Parse(examdate);

                    InsertDataIntoHallTicket(subjectName, date, roomNo, int.Parse(semester), department);
                }
            }
        }

        protected void btn_searchStudent_Click(object sender, EventArgs e)
        {
            string department = departmentDropdown.SelectedValue;
            string semester = semesterDropdown.SelectedValue;

            if (department == "" || department == null)
            {
                lbl_error.Text = "Select department";
            }
            else if (semester == "" || semester == null)
            {
                lbl_error.Text = "Select semester";
            }
            else
            {
                lbl_error.Text = "";
                BindSubjectsGridView(semester, department);
            }
        }

        private void PopulateDepartmentDropdown()
        {
            string query = "SELECT DepartmentID, DepartmentName FROM Department";

            using (SqlConnection con = new SqlConnection("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=F:\\B.TECH\\SEM-5\\WDDN\\Student-Portal\\Student-Portal\\App_Data\\Student_Portal.mdf;Integrated Security=True"))
            {
                using (SqlCommand command = new SqlCommand(query, con))
                {
                    con.Open();

                    SqlDataAdapter adapter = new SqlDataAdapter(command);
                    DataSet dataSet = new DataSet();

                    adapter.Fill(dataSet);

                    departmentDropdown.DataSource = dataSet;
                    departmentDropdown.DataTextField = "DepartmentName";
                    departmentDropdown.DataValueField = "DepartmentID";
                    departmentDropdown.DataBind();
                }
            }
        }

        protected void BindSubjectsGridView(string semester, string department)
        {
            string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=F:\B.TECH\SEM-5\WDDN\Student-Portal\Student-Portal\App_Data\Student_Portal.mdf;Integrated Security=True";
            string query = "SELECT SubjectName FROM Subjects WHERE Semester = @Semester AND DepartmentID = @DepartmentID";
            List<string> subjects = new List<string>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("@Semester", semester);
                    cmd.Parameters.AddWithValue("@DepartmentID", department);

                    connection.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string subjectName = reader["SubjectName"].ToString();
                            subjects.Add(subjectName);
                        }
                    }
                }
            }
            SubjectsGridView.DataSource = subjects;
            SubjectsGridView.DataBind();
        }

        private void InsertDataIntoHallTicket(string subjectName, DateTime examDate, string roomNo, int semesterID, string departmentID)
        {
            string connectionString = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=F:\\B.TECH\\SEM-5\\WDDN\\Student-Portal\\Student-Portal\\App_Data\\Student_Portal.mdf;Integrated Security=True";
            string query = "INSERT INTO HallTicket (SubjectName, ExamDate, RoomNo, SemesterID, DepartmentID) VALUES (@SubjectName, @ExamDate, @RoomNo, @SemesterID, @DepartmentID)";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("@SubjectName", subjectName);
                    cmd.Parameters.AddWithValue("@ExamDate", examDate);
                    cmd.Parameters.AddWithValue("@RoomNo", roomNo);
                    cmd.Parameters.AddWithValue("@SemesterID", semesterID);
                    cmd.Parameters.AddWithValue("@DepartmentID", departmentID);

                    connection.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}