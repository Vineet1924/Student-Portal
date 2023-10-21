using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml.Linq;

namespace Student_Portal
{
    public partial class Loginpage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Session["type"] = "";
            Session["currentUser"] = "";
            Session["semester"] = "";
        }
        protected void Signin(object sender, EventArgs e)
        {
            string email = txt_email.Text.Trim();
            string password = txt_password.Text.Trim();

            if(!isValidEmail(email))
            {
                displayError("Invalid email format.");
                return;
            }

            authenticateUser(email, password);
        }

        public void displayError(string errorMessage)
        {
            lbl_error.Visible = true;
            lbl_error.Text = errorMessage;
        }

        public void hideError()
        {
            lbl_error.Text = "";
            lbl_error.Visible = false;
        }
        bool isValidEmail(string email)
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

        public void authenticateUser(string email, string password)
        {
            if(isTeacher(email, password))
            {
                Session["type"] = "Teacher";
                Response.Redirect("teacherDashboard.aspx");
            } else if (isStudent(email, password))
            {
                Session["type"] = "Student";
                Response.Redirect("studentDashboard.aspx");
            } else if (isAdmin(email, password))
            {
                Session["type"] = "Admin";
                Response.Redirect("adminDashboard.aspx");
            } else
            {
                displayError("Username or password may be incorrect");
            }
        }

        public bool isTeacher(string email, string password)
        {
            SqlConnection con = new SqlConnection("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=F:\\B.TECH\\SEM-5\\WDDN\\Student-Portal\\Student-Portal\\App_Data\\Student_Portal.mdf;Integrated Security=True");

            using (con)
            {
                con.Open();
                string query = "SELECT COUNT(*) FROM Teachers WHERE Email = @email And Password = @password";

                using (SqlCommand command = new SqlCommand(query, con))
                {
                    command.Parameters.AddWithValue("@Email", email);
                    command.Parameters.AddWithValue("@Password", password);

                    int rowCount = (int)command.ExecuteScalar();
                    Session["currentUser"] = email;
                    return rowCount > 0;
                }
            }
        }

        public bool isStudent(string email, string password)
        {
            SqlConnection con = new SqlConnection("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=F:\\B.TECH\\SEM-5\\WDDN\\Student-Portal\\Student-Portal\\App_Data\\Student_Portal.mdf;Integrated Security=True");
            using (con)
            {
                con.Open();
                string query = "SELECT COUNT(*) FROM Students WHERE Email = @email And Password = @password";

                using (SqlCommand command = new SqlCommand(query, con))
                {
                    command.Parameters.AddWithValue("@Email", email);
                    command.Parameters.AddWithValue("@Password", password);

                    int rowCount = (int)command.ExecuteScalar();
                    Session["currentUser"] = email;
                    return rowCount > 0;
                }
            }
        }

        public bool isAdmin(string email, string password)
        {
            if(email == "admin@gmail.com" && password == "admin@1234")
            {
                Session["currentUser"] = email;
                return true;
            }
            return false;
        }
    }
}