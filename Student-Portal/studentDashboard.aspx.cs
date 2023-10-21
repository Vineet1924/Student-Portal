using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Policy;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Student_Portal
{
    public partial class studentDashboard : System.Web.UI.Page
    {
        string enno = "";
        string email = "";
        string firstname = "";
        string lastname = "";
        string dob = "";
        string phone = "";
        string department = "";
        string semester = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["type"] == null || string.IsNullOrEmpty(Session["type"].ToString()) || Session["type"].ToString() != "Student")
                {
                    Response.Redirect("loginpage.aspx");
                }

                if (Session["currentUser"].ToString() != "")
                {
                    email = Session["currentUser"].ToString();
                    using (SqlConnection con = new SqlConnection("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=F:\\B.TECH\\SEM-5\\WDDN\\Student-Portal\\Student-Portal\\App_Data\\Student_Portal.mdf;Integrated Security=True"))
                    {
                        con.Open();
                        string query = "SELECT * FROM STUDENTS WHERE Email = @Email";
                        using (SqlCommand command = new SqlCommand(query, con))
                        {
                            command.Parameters.AddWithValue("@Email", email);

                            using (SqlDataReader reader = command.ExecuteReader())
                            {
                                if (reader.Read())
                                {
                                    firstname = reader["Firstname"].ToString();
                                    Session["fname"] = firstname;
                                    lastname = reader["Lastname"].ToString();
                                    Session["lname"] = lastname;
                                    enno = reader["Enrollment_No"].ToString();
                                    Session["Enrollment"] = enno;
                                    email = reader["Email"].ToString();
                                    dob = reader["DOB"].ToString();
                                    semester = reader["Semester"].ToString();
                                    Session["Semester"] = semester;
                                    phone = reader["Phone"].ToString();
                                    department = reader["Department"].ToString();
                                }
                            }
                        }
                    }

                    txt_firstname.Text = firstname;
                    txt_lastname.Text = lastname;
                    txt_phone.Text = phone;
                    txt_email.Text = email;
                    txt_department.Text = getDepartment(int.Parse(department));
                    txt_dob.Text = dob;
                    txt_sem.Text = semester;
                    txt_enno.Text = enno;

                    txt_email.ReadOnly = true;
                    txt_firstname.ReadOnly = true;
                    txt_lastname.ReadOnly = true;
                    txt_enno.ReadOnly = true;
                    txt_dob.ReadOnly = true;
                    txt_sem.ReadOnly = true;
                    txt_department.ReadOnly = true;
                    txt_phone.ReadOnly = true;
                }
                else
                {
                    Response.Redirect("loginpage.aspx");
                }
            }
        }

        protected string getDepartment(int departmentID)
        {
            using (SqlConnection con = new SqlConnection("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=F:\\B.TECH\\SEM-5\\WDDN\\Student-Portal\\Student-Portal\\App_Data\\Student_Portal.mdf;Integrated Security=True"))
            {
                con.Open();
                string query = "SELECT DEPARTMENTNAME FROM DEPARTMENT WHERE DEPARTMENTID = @deptID";
                using (SqlCommand command = new SqlCommand(query, con))
                {
                    command.Parameters.AddWithValue("@deptID", departmentID);
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return reader["DepartmentName"].ToString();
                        }
                    }
                }
            }
            return null;
        }
    }
}