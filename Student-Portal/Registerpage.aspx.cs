using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Student_Portal
{
    public partial class Registerpage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            hideError();

            if (Session["RegistrationCompleted"] != null && (bool)Session["RegistrationCompleted"])
            {
                Response.Redirect("Loginpage.aspx");
            }
        }

        protected void Signup(object sender, EventArgs e)
        {
            string teacherId = GenerateTeacherId();
            string firstName = txt_firstname.Text.Trim();
            string lastName = txt_lastname.Text.Trim();
            string email = txt_email.Text.Trim();
            string password = txt_password.Text;
            string confirmPassword = txt_confpassword.Text;

            bool isValid = true;

            if (string.IsNullOrEmpty(firstName))
            {
                displayError("First Name is required.");
                isValid = false;
            } else if (string.IsNullOrEmpty(lastName))
            {
                displayError("Last Name is required.");
                isValid = false;
            } else if (string.IsNullOrEmpty(email))
            {
                displayError("Email is required.");
                isValid = false;
            } else if (!IsValidEmail(email))
            {
                displayError("Invalid email format.");
                isValid = false;
            } else if (string.IsNullOrEmpty(password))
            {
                displayError("Password is required.");
                isValid = false;
            } else if (password.Length < 6)
            {
                displayError("Password must be at least 6 characters long.");
                isValid = false;
            } else if (password != confirmPassword)
            {
                displayError("Passwords do not match.");
                isValid = false;
            }

            if (isValid)
            {
                hideError();

                if (!isEmailExist(email) && insertData(teacherId, firstName, lastName, email, password))
                {
                    Session["RegistrationCompleted"] = true;
                    HttpContext.Current.Response.Redirect("Loginpage.aspx");
                } else
                {
                    displayError("Email already exist.");
                }

                txt_email.Text = "";
            }          
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

        public static string GenerateTeacherId()
        {
            DateTime now = DateTime.Now;
            string formattedDateTime = now.ToString("yyyyMMddHHmmssfff");
            string teacherId = "TCHR" + formattedDateTime;
            return teacherId;
        }

        public bool isEmailExist(string email)
        {
            SqlConnection con = new SqlConnection("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=F:\\B.TECH\\SEM-5\\WDDN\\Student-Portal\\Student-Portal\\App_Data\\Student_Portal.mdf;Integrated Security=True");

            using (con)
            {
                con.Open();
                string query = "SELECT COUNT(*) FROM Teachers WHERE Email = @email";

                using (SqlCommand command = new SqlCommand(query, con))
                {
                    command.Parameters.AddWithValue("@Email", email);
                    int rowCount = (int)command.ExecuteScalar();
                    return rowCount > 0;
                }
            }
        }

        public bool insertData(string teacherId, string firstName, string lastName, string email, string password)
        {
            SqlConnection con = new SqlConnection("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=F:\\B.TECH\\SEM-5\\WDDN\\Student-Portal\\Student-Portal\\App_Data\\Student_Portal.mdf;Integrated Security=True");
            string insertQuery = "INSERT INTO TEACHERS (Teacher_Id, Firstname, Lastname, Email, Password) VALUES (@teacherId, @firstName, @lastName, @email, @password)";

            using (SqlCommand command = new SqlCommand(insertQuery, con))
            {
                command.Parameters.AddWithValue("@teacherId", teacherId);
                command.Parameters.AddWithValue("@firstName", firstName);
                command.Parameters.AddWithValue("@lastName", lastName);
                command.Parameters.AddWithValue("@email", email);
                command.Parameters.AddWithValue("@password", password);

                con.Open();
                int rowsAffected = command.ExecuteNonQuery();
                con.Close();

                if (rowsAffected > 0)
                {
                    return true;
                } else
                {
                    return false;
                }
            }
        }
    }
}