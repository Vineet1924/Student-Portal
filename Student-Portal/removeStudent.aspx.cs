using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Student_Portal
{
    public partial class removeStudent : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["type"] == null || string.IsNullOrEmpty(Session["type"].ToString()) || (Session["type"].ToString() != "Teacher" && Session["type"].ToString() != "Admin"))
            {
                Response.Redirect("loginpage.aspx");
            }
            else
            {
                SqlConnection con = new SqlConnection("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=F:\\B.TECH\\SEM-5\\WDDN\\Student-Portal\\Student-Portal\\App_Data\\Student_Portal.mdf;Integrated Security=True");
                con.Open();
                string query = "SELECT * FROM STUDENTS";
                SqlCommand command = new SqlCommand(query, con);

                SqlDataReader reader = command.ExecuteReader();

                StringBuilder tableHtml = new StringBuilder();
                tableHtml.Append("<table class='styled-table'>");
                tableHtml.Append("<thead><tr>");
                tableHtml.Append("<th>Enrollment No</th>");
                tableHtml.Append("<th>First Name</th>");
                tableHtml.Append("<th>Last Name</th>");
                tableHtml.Append("<th>DOB</th>");
                tableHtml.Append("<th>Email</th>");
                tableHtml.Append("<th>Phone</th>");
                tableHtml.Append("<th>Department</th>");
                tableHtml.Append("<th>Semester</th>");
                tableHtml.Append("<th></th>");
                tableHtml.Append("</tr></thead>");


                while (reader.Read())
                {
                    string enrollmentNo = reader["Enrollment_No"].ToString();
                    string firstName = reader["Firstname"].ToString();
                    string lastName = reader["Lastname"].ToString();
                    string DOB = reader["DOB"].ToString();
                    DateTime dobDateTime = DateTime.Parse(DOB);
                    string formattedDOB = dobDateTime.ToString("yyyy-MM-dd");
                    string email = reader["Email"].ToString();
                    string phone = reader["Phone"].ToString();
                    string department = reader["Department"].ToString();
                    string semester = reader["Semester"].ToString();

                    tableHtml.Append("<tr>");
                    tableHtml.Append("<td>" + enrollmentNo + "</td>");
                    tableHtml.Append("<td>" + firstName + "</td>");
                    tableHtml.Append("<td>" + lastName + "</td>");
                    tableHtml.Append("<td>" + formattedDOB + "</td>");
                    tableHtml.Append("<td>" + email + "</td>");
                    tableHtml.Append("<td>" + phone + "</td>");
                    tableHtml.Append("<td>" + department + "</td>");
                    tableHtml.Append("<td>" + semester + "</td>");
                    tableHtml.Append("<td><button class='btn btn-danger btn-block mb-4' "
                    + "onclick='openModal(" + enrollmentNo + ")' "
                    + ">Remove</button></td>");




                    tableHtml.Append("</tr>");
                }

                tableHtml.Append("</tbody></table>");
                studentTable.Text = tableHtml.ToString();
            }
        }

        protected void btn_removeStudent_Click(object sender, EventArgs e)
        {
            string enrollmentNo = hdnEnrollmentNo.Value;

            using (SqlConnection con = new SqlConnection("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=F:\\B.TECH\\SEM-5\\WDDN\\Student-Portal\\Student-Portal\\App_Data\\Student_Portal.mdf;Integrated Security=True"))
            {
                string query = "DELETE FROM STUDENTS WHERE Enrollment_No = @EnrollmentNo";

                SqlCommand command = new SqlCommand(query, con);
                command.Parameters.AddWithValue("@EnrollmentNo", enrollmentNo);

                con.Open();
                int result = command.ExecuteNonQuery();
                con.Close();

                Page_Load(sender,e);
            }
        }
    }
}