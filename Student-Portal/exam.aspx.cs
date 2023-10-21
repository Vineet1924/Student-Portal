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
    public partial class exam : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["type"] == null || string.IsNullOrEmpty(Session["type"].ToString()) || (Session["type"].ToString() != "Teacher" && Session["type"].ToString() != "Admin"))
                {
                    Response.Redirect("loginpage.aspx");
                }
                else
                {
                    SqlConnection con = new SqlConnection("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=F:\\B.TECH\\SEM-5\\WDDN\\Student-Portal\\Student-Portal\\App_Data\\Student_Portal.mdf;Integrated Security=True");
                    con.Open();
                    string query = "SELECT * FROM STUDENTS ORDER BY Enrollment_No ASC";
                    SqlCommand command = new SqlCommand(query, con);

                    SqlDataAdapter adapter = new SqlDataAdapter(command);
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);

                    Students.DataSource = dataTable;
                    Students.DataBind();
                    con.Close();
                }
                PopulateDepartmentDropdown();
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
                SqlConnection con = new SqlConnection("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=F:\\B.TECH\\SEM-5\\WDDN\\Student-Portal\\Student-Portal\\App_Data\\Student_Portal.mdf;Integrated Security=True");
                con.Open();
                string query = "SELECT * FROM STUDENTS WHERE Department = @Department AND Semester = @Semester ORDER BY Enrollment_No ASC";
                using (SqlCommand command = new SqlCommand(query, con))
                {
                    command.Parameters.AddWithValue("@Department", department);
                    command.Parameters.AddWithValue("@Semester", semester);

                    SqlDataAdapter adapter = new SqlDataAdapter(command);
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);

                    Students.DataSource = dataTable;
                    Students.DataBind();
                    con.Close();
                }

                PopulateSubjectDropdown();
            }
        }

        protected void btn_submit_Click(object sender, EventArgs e)
        {
            string semester = semesterDropdown.SelectedValue;
            string subject = subjectDropdown.SelectedValue;
            string session = sessionalDropdown.SelectedValue;

            if (semester == "" || semester == null)
            {
                lbl_error.Text = "Select subject";
                return;
            }
            if (session == "" || session == null)
            {
                lbl_error.Text = "Select session";
                return;
            }
            if (semester == "" || semester == null)
            {
                lbl_error.Text = "Select semester";
            }
            for (int i = 0; i < Students.Rows.Count; i++)
            {
                GridViewRow row = Students.Rows[i];
                string enrollmentNo = row.Cells[0].Text;
                TextBox txtMarks = (TextBox)row.Cells[4].FindControl("txtMarks");

                int marks;
                int.TryParse(txtMarks.Text, out marks);

                using (SqlConnection con = new SqlConnection("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=F:\\B.TECH\\SEM-5\\WDDN\\Student-Portal\\Student-Portal\\App_Data\\Student_Portal.mdf;Integrated Security=True"))
                {
                    con.Open();
                    string checkIfExistsQuery = "SELECT COUNT(*) FROM AttendanceAndExam WHERE StudentID = @StudentID AND SubjectID = @SubjectID AND Semester = @Semester";
                    using (SqlCommand checkIfExistsCommand = new SqlCommand(checkIfExistsQuery, con))
                    {
                        checkIfExistsCommand.Parameters.AddWithValue("@StudentID", enrollmentNo);
                        checkIfExistsCommand.Parameters.AddWithValue("@SubjectID", subject);
                        checkIfExistsCommand.Parameters.AddWithValue("@Semester", semester);

                        int recordCount = (int)checkIfExistsCommand.ExecuteScalar();

                        if (recordCount > 0)
                        {
                            string updateQuery = "";
                            if (session == "1")
                            {
                                updateQuery = "UPDATE AttendanceAndExam SET SessionalExam1 = @Marks WHERE StudentID = @StudentID AND SubjectID = @SubjectID AND Semester = @Semester";
                            }
                            else if (session == "2")
                            {
                                updateQuery = "UPDATE AttendanceAndExam SET SessionalExam2 = @Marks WHERE StudentID = @StudentID AND SubjectID = @SubjectID AND Semester = @Semester";
                            }
                            else
                            {
                                updateQuery = "UPDATE AttendanceAndExam SET SessionalExam3 = @Marks WHERE StudentID = @StudentID AND SubjectID = @SubjectID AND Semester = @Semester";
                            }
                            using (SqlCommand updateCommand = new SqlCommand(updateQuery, con))
                            {
                                updateCommand.Parameters.AddWithValue("@StudentID", enrollmentNo);
                                updateCommand.Parameters.AddWithValue("@SubjectID", subject);
                                updateCommand.Parameters.AddWithValue("@Semester", semester);
                                updateCommand.Parameters.AddWithValue("@Marks", marks);

                                updateCommand.ExecuteNonQuery();
                            }
                        }
                        else
                        {
                            string insertQuery = "";
                            if (session == "1")
                            {
                                insertQuery = "INSERT INTO AttendanceAndExam (StudentID, SubjectID, Semester, SessionalExam1) VALUES (@StudentID, @SubjectID, @Semester, @Marks)";
                            }
                            else if (session == "2")
                            {
                                insertQuery = "INSERT INTO AttendanceAndExam (StudentID, SubjectID, Semester, SessionalExam2) VALUES (@StudentID, @SubjectID, @Semester, @Marks)";
                            }
                            else
                            {
                                insertQuery = "INSERT INTO AttendanceAndExam (StudentID, SubjectID, Semester, SessionalExam3) VALUES (@StudentID, @SubjectID, @Semester, @Marks)";
                            }
                            using (SqlCommand insertCommand = new SqlCommand(insertQuery, con))
                            {
                                insertCommand.Parameters.AddWithValue("@StudentID", enrollmentNo);
                                insertCommand.Parameters.AddWithValue("@SubjectID", subject);
                                insertCommand.Parameters.AddWithValue("@Semester", semester);
                                insertCommand.Parameters.AddWithValue("@Marks", marks);

                                insertCommand.ExecuteNonQuery();
                            }
                        }
                    }
                }
            }
            lbl_error.Text = "";
        }

        protected void GridView_RowDataBound(object sender, GridViewRowEventArgs e)
        {

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

        private void PopulateSubjectDropdown()
        {
            string department = departmentDropdown.SelectedValue;
            string semester = semesterDropdown.SelectedValue;

            string query = "SELECT SubjectID, SubjectName FROM Subjects where DepartmentID = @deptID AND Semester = @Semester";

            using (SqlConnection con = new SqlConnection("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=F:\\B.TECH\\SEM-5\\WDDN\\Student-Portal\\Student-Portal\\App_Data\\Student_Portal.mdf;Integrated Security=True"))
            {
                using (SqlCommand command = new SqlCommand(query, con))
                {
                    con.Open();
                    command.Parameters.AddWithValue("@deptID", department);
                    command.Parameters.AddWithValue("@Semester", semester);

                    SqlDataAdapter adapter = new SqlDataAdapter(command);
                    DataSet dataSet = new DataSet();

                    adapter.Fill(dataSet);

                    subjectDropdown.DataSource = dataSet;
                    subjectDropdown.DataTextField = "SubjectName";
                    subjectDropdown.DataValueField = "SubjectID";
                    subjectDropdown.DataBind();
                }
            }
        }
    }
}