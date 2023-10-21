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
    public partial class updateStudent : System.Web.UI.Page
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
                    tableHtml.Append("<td><button class='btn btn-primary btn-block mb-4' "
                    + "onclick='openModal(\"" + enrollmentNo + "\",\"" + firstName + "\",\"" + lastName + "\",\"" + formattedDOB + "\",\"" + email + "\",\"" + phone + "\")' "
                    + ">Update</button></td>");




                    tableHtml.Append("</tr>");
                }

                tableHtml.Append("</tbody></table>");
                studentTable.Text = tableHtml.ToString();
            }
        }
        bool IsValidEmail(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }

        protected void btn_updateStudent_Click(object sender, EventArgs e)
        {
            string enrollmentNo = hdnEnrollmentNo.Value;
            string fname = txt_firstname.Text;
            string lname = txt_lastname.Text;
            string email = txt_email.Text;
            string phone = txt_phone.Text;
            string dob = dateofbirth.Value;


            lbl_error.Text = email;

            if (fname == "" || fname == null)
            {
                lbl_error.Text = "Enter Firstname";
                return;
            }
            else if (lname == "" || lname == null)
            {
                lbl_error.Text = "Enter Lastname";
                return;
            }
            else if (email == "" || email == null)
            {
                lbl_error.Text = "email";
                return;
            }
            else if (!IsValidEmail(email))
            {
                lbl_error.Text = "Format Email";
                return;
            }
            else if (phone == "" || phone == null)
            {
                lbl_error.Text = "Enter phonenumber";
                return;
            }
            else if (phone == "" || phone.Length != 10)
            {
                lbl_error.Text = "Enter valid phone number";
                return;
            }
            else if (dob == "" || dob == null)
            {
                lbl_error.Text = "Enter dob";
                return;
            }
            else
            {
                lbl_error.Text = "";
                using (SqlConnection con = new SqlConnection("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=F:\\B.TECH\\SEM-5\\WDDN\\Student-Portal\\Student-Portal\\App_Data\\Student_Portal.mdf;Integrated Security=True"))
                {
                    string query = @"UPDATE STUDENTS 
                        SET Firstname = @FirstName, 
                            Lastname = @LastName, 
                            DOB = @DOB, 
                            Email = @Email, 
                            Phone = @Phone
                        WHERE Enrollment_No = @EnrollmentNo";


                    SqlCommand command = new SqlCommand(query, con);
                    command.Parameters.AddWithValue("@FirstName", fname);
                    command.Parameters.AddWithValue("@LastName", lname);
                    command.Parameters.AddWithValue("@Email", email);
                    command.Parameters.AddWithValue("@Phone", phone);
                    command.Parameters.AddWithValue("@DOB", dob);
                    command.Parameters.AddWithValue("@EnrollmentNo", enrollmentNo);

                    con.Open();
                    int result = command.ExecuteNonQuery();
                    con.Close();

                    Page_Load(sender, e);
                }
            }
        }
    }
}