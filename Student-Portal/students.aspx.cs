using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Student_Portal
{
    public partial class students : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
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

            if(department == "" || department == null)
            {
                lbl_error.Text = "Select department";
            } else if(semester == "" || semester == null)
            {
                lbl_error.Text = "Select semester";
            } else
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

            if(semester == "" || semester == null)
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
                TextBox txtLecture = (TextBox)row.Cells[4].FindControl("txtLecture");
                TextBox txtLab = (TextBox)row.Cells[5].FindControl("txtLab");

                int lectureAttendance = 0;
                int labAttendance = 0;

                int.TryParse(txtLecture.Text, out lectureAttendance);
                int.TryParse(txtLab.Text, out labAttendance);

                string script = $"<script type='text/javascript'>console.log('Row {i + 1}: Lecture attendance = {lectureAttendance}');</script>";
                ClientScript.RegisterStartupScript(this.GetType(), $"PrintAttendanceScript_{i}", script);

                if(lectureAttendance == 0 || labAttendance == 0)
                {
                    lbl_error.Text = "Enter attendance";
                    return;
                }

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
                            if(session == "1")
                            {
                                updateQuery = "UPDATE AttendanceAndExam SET Sessional1LabAttendance = @LabAttendance, Sessional1LectureAttendance = @LectureAttendance WHERE StudentID = @StudentID AND SubjectID = @SubjectID AND Semester = @Semester";
                            }
                            else if(session == "2")
                            {
                                updateQuery = "UPDATE AttendanceAndExam SET Sessional2LabAttendance = @LabAttendance, Sessional2LectureAttendance = @LectureAttendance WHERE StudentID = @StudentID AND SubjectID = @SubjectID AND Semester = @Semester";
                            }
                            else
                            {
                                updateQuery = "UPDATE AttendanceAndExam SET Sessional3LabAttendance = @LabAttendance, Sessional3LectureAttendance = @LectureAttendance WHERE StudentID = @StudentID AND SubjectID = @SubjectID AND Semester = @Semester";
                            }
                            using (SqlCommand updateCommand = new SqlCommand(updateQuery, con))
                            {
                                updateCommand.Parameters.AddWithValue("@StudentID", enrollmentNo);
                                updateCommand.Parameters.AddWithValue("@SubjectID", subject);
                                updateCommand.Parameters.AddWithValue("@Semester", semester);
                                updateCommand.Parameters.AddWithValue("@LabAttendance", labAttendance);
                                updateCommand.Parameters.AddWithValue("@LectureAttendance", lectureAttendance);

                                updateCommand.ExecuteNonQuery();
                            }
                        }
                        else
                        {
                            string insertQuery = "";
                            if(session == "1")
                            {
                                insertQuery = "INSERT INTO AttendanceAndExam (StudentID, SubjectID, Semester, Sessional1LabAttendance, Sessional1LectureAttendance) VALUES (@StudentID, @SubjectID, @Semester, @LabAttendance, @LectureAttendance)";
                            }
                            else if(session == "2")
                            {
                                insertQuery = "INSERT INTO AttendanceAndExam (StudentID, SubjectID, Semester, Sessional2LabAttendance, Sessional2LectureAttendance) VALUES (@StudentID, @SubjectID, @Semester, @LabAttendance, @LectureAttendance)";
                            }
                            else
                            {
                                insertQuery = "INSERT INTO AttendanceAndExam (StudentID, SubjectID, Semester, Sessional3LabAttendance, Sessional3LectureAttendance) VALUES (@StudentID, @SubjectID, @Semester, @LabAttendance, @LectureAttendance)";
                            }
                            using (SqlCommand insertCommand = new SqlCommand(insertQuery, con))
                            {
                                insertCommand.Parameters.AddWithValue("@StudentID", enrollmentNo);
                                insertCommand.Parameters.AddWithValue("@SubjectID", subject);
                                insertCommand.Parameters.AddWithValue("@Semester", semester);
                                insertCommand.Parameters.AddWithValue("@LabAttendance", labAttendance);
                                insertCommand.Parameters.AddWithValue("@LectureAttendance", lectureAttendance);

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