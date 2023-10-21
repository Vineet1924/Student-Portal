using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Services.Description;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Student_Portal
{
    public class Student
    {
        public string EnrollmentNo { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Phone { get; set; }
        public string DateOfBirth { get; set; }
        public string Department { get; set; }
        public string Semester { get; set; }
    }

    public partial class addStudent : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["type"] == null || string.IsNullOrEmpty(Session["type"].ToString()) || (Session["type"].ToString() != "Teacher" && Session["type"].ToString() != "Admin"))
            {
                Response.Redirect("loginpage.aspx");
            }

            PopulateDepartmentDropdown();
        }

        protected void btn_insertStudent_Click(object sender, EventArgs e)
        {
            string enrollmentNo = txt_enrollmentNo.Text;
            string firstName = txt_firstname.Text;
            string lastName = txt_lastname.Text;
            string email = txt_email.Text;
            string password = txt_password.Text;
            string phone = txt_phone.Text;
            string shortDate = "";
            string department = departmentDropdown.SelectedValue;
            string semester = "1";

            string dateOfBirth = Request.Form["dateofbirth"];

            if (!string.IsNullOrEmpty(dateOfBirth))
            {
                DateTime parsedDateOfBirth;
                if (DateTime.TryParse(dateOfBirth, out parsedDateOfBirth))
                {
                    shortDate = parsedDateOfBirth.ToShortDateString();
                }
                else
                {

                }
            }
            else
            {
                lbl_error.Text = "Please select a date.";
            }

            if (enrollmentNo == "" || enrollmentNo == null)
            {
                lbl_error.Text = "Enter Enrollment Number";
                return;
            } else if(firstName == "" || firstName ==null)
            {
                lbl_error.Text = "Enter first name";
                return;
            }
            else if (lastName == "" || lastName == null)
            {
                lbl_error.Text = "Enter last name";
                return;
            }
            else if (email == "" || email == null)
            {
                lbl_error.Text = "Enter email";
                return;
            }
            else if (!IsValidEmail(email))
            {
                lbl_error.Text = "Format Email";
                return;
            }
            else if (password == "" || password == null)
            {
                lbl_error.Text = "Enter password";
                return;
            }
            else if (phone == "" || phone.Length != 10)
            {
                lbl_error.Text = "Enter valid phone number";
                return;
            }
            else if (shortDate == "" || shortDate == null)
            {
                lbl_error.Text = "Enter DOB";
                return;
            }
            else if (department == "" || department == null)
            {
                lbl_error.Text = "Select department";
                return;
            }

            if (isEmailExist(email))
            {
                lbl_error.Text = "User already exist";
                return;
            }

            if(isEnrollmentExist(enrollmentNo))
            {
                lbl_error.Text = "User already exist";
                return;
            }

            lbl_error.Text = "";

            Student newStudent = new Student
            {
                EnrollmentNo = enrollmentNo,
                FirstName = firstName,
                LastName = lastName,
                Email = email,
                Password = password,
                Phone = phone,
                DateOfBirth = shortDate,
                Department = department,
                Semester = semester,
            };

            using (SqlConnection con = new SqlConnection("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=F:\\B.TECH\\SEM-5\\WDDN\\Student-Portal\\Student-Portal\\App_Data\\Student_Portal.mdf;Integrated Security=True"))
            {
                string query = "INSERT INTO Students (Enrollment_No, Firstname, Lastname, DOB, Email, Password, Phone, Department, Semester) " +
                               "VALUES (@EnrollmentNo, @FirstName, @LastName, @DateOfBirth, @Email, @Password, @Phone, @Department, @Semester)";

                SqlCommand command = new SqlCommand(query, con);
                command.Parameters.AddWithValue("@EnrollmentNo", newStudent.EnrollmentNo);
                command.Parameters.AddWithValue("@FirstName", newStudent.FirstName);
                command.Parameters.AddWithValue("@LastName", newStudent.LastName);
                command.Parameters.AddWithValue("@Email", newStudent.Email);
                command.Parameters.AddWithValue("@Password", newStudent.Password);
                command.Parameters.AddWithValue("@Phone", newStudent.Phone);
                command.Parameters.AddWithValue("@DateOfBirth", newStudent.DateOfBirth);
                command.Parameters.AddWithValue("@Department", newStudent.Department);
                command.Parameters.AddWithValue("@Semester", newStudent.Semester);

                con.Open();
                int result = command.ExecuteNonQuery();
                con.Close();

                if (result > 0)
                {
                    Response.Write("Data inserted successfully.");
                }
                else
                {
                    Response.Write("Error inserting data.");
                }
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

        public bool isEmailExist(string email)
        {
            SqlConnection con = new SqlConnection("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=F:\\B.TECH\\SEM-5\\WDDN\\Student-Portal\\Student-Portal\\App_Data\\Student_Portal.mdf;Integrated Security=True");

            using (con)
            {
                con.Open();
                string query = "SELECT COUNT(*) FROM Students WHERE Email = @email";

                using (SqlCommand command = new SqlCommand(query, con))
                {
                    command.Parameters.AddWithValue("@Email", email);
                    int rowCount = (int)command.ExecuteScalar();
                    return rowCount > 0;
                }
            }
        }

        public bool isEnrollmentExist(string enrollmentNo)
        {
            SqlConnection con = new SqlConnection("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=F:\\B.TECH\\SEM-5\\WDDN\\Student-Portal\\Student-Portal\\App_Data\\Student_Portal.mdf;Integrated Security=True");

            using (con)
            {
                con.Open();
                string query = "SELECT COUNT(*) FROM Students WHERE Enrollment_No = @enrollmentNo";

                using (SqlCommand command = new SqlCommand(query, con))
                {
                    command.Parameters.AddWithValue("@enrollmentNo", enrollmentNo);
                    int rowCount = (int)command.ExecuteScalar();
                    return rowCount > 0;
                }
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
    }
}