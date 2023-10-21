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
    public partial class teacherProfile : System.Web.UI.Page
    {
        string email = "";
        string firstname = "";
        string lastname = "";
        string password = "";
        string phone = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                if (Session["type"] == null || string.IsNullOrEmpty(Session["type"].ToString()) || (Session["type"].ToString() != "Teacher" && Session["type"].ToString() != "Admin"))
                {
                    Response.Redirect("loginpage.aspx");
                }

                if (Session["currentUser"].ToString() != "")
                {
                    email = Session["currentUser"].ToString();
                    using (SqlConnection con = new SqlConnection("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=F:\\B.TECH\\SEM-5\\WDDN\\Student-Portal\\Student-Portal\\App_Data\\Student_Portal.mdf;Integrated Security=True"))
                    {
                        con.Open();
                        string query = "SELECT * FROM TEACHERS WHERE Email = @Email";
                        using (SqlCommand command = new SqlCommand(query, con))
                        {
                            command.Parameters.AddWithValue("@Email", email);

                            using (SqlDataReader reader = command.ExecuteReader())
                            {
                                if (reader.Read())
                                {
                                    firstname = reader["Firstname"].ToString();
                                    lastname = reader["Lastname"].ToString();
                                    password = reader["Password"].ToString();
                                    phone = reader["Phone"].ToString();
                                }
                            }
                        }
                    }

                    txt_firstname.Text = firstname;
                    txt_lastname.Text = lastname;
                    txt_password.Text = password;
                    txt_phone.Text = phone;
                    txt_email.Text = email;
                }
                else
                {
                    Response.Redirect("loginpage.aspx");
                }
            }
        }

        protected void btn_showProfile_Click(object sender, EventArgs e)
        {
            using (SqlConnection con = new SqlConnection("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=F:\\B.TECH\\SEM-5\\WDDN\\Student-Portal\\Student-Portal\\App_Data\\Student_Portal.mdf;Integrated Security=True"))
            {
                con.Open();
                string query = "UPDATE Teachers SET Firstname = @Firstname, Lastname = @Lastname, Password = @Password, Phone = @Phone WHERE Email = @Email";
                using (SqlCommand command = new SqlCommand(query, con))
                {
                    command.Parameters.AddWithValue("@Email", email);
                    command.Parameters.AddWithValue("@Firstname", firstname);
                    command.Parameters.AddWithValue("@Lastname", lastname);
                    command.Parameters.AddWithValue("@Password", password);
                    command.Parameters.AddWithValue("@Phone", phone);

                    command.ExecuteNonQuery();
                }
            }

            lbl_error.Text = "";
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
    }
}