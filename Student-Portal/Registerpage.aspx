<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Registerpage.aspx.cs" Inherits="Student_Portal.Registerpage" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Sign up</title>

    <!-- Font Awesome -->
    <link
        href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/6.0.0/css/all.min.css"
        rel="stylesheet"
    />
    <!-- Google Fonts -->
    <link
      href="https://fonts.googleapis.com/css?family=Roboto:300,400,500,700&display=swap"
      rel="stylesheet"
    />
    <!-- MDB -->
    <link
      href="https://cdnjs.cloudflare.com/ajax/libs/mdb-ui-kit/6.4.2/mdb.min.css"
      rel="stylesheet"
    />

    <script>
        function openLoginPage() {
            window.location.href = 'Loginpage.aspx';
        }
    </script>
</head>
<body>
    <!-- Section: Design Block -->
    <section class="text-center">
      <!-- Background image -->
      <div class="p-5 bg-image" style="
            background-image: url('https://mdbootstrap.com/img/new/textures/full/171.jpg');
            height: 300px;
            "></div>
      <!-- Background image -->

      <div class="card mx-4 mx-md-5 shadow-5-strong" style="
            margin-top: -100px;
            background: hsla(0, 0%, 100%, 0.8);
            backdrop-filter: blur(30px);
            ">
        <div class="card-body py-5 px-md-5">

          <div class="row d-flex justify-content-center">
            <div class="col-lg-8">
              <h2 class="fw-bold mb-5">Sign up now</h2>
              <h5><asp:Label ID="lbl_error" runat="server" ForeColor="Red" Font-Names="Inter" Visible="true" /></h5>

              <form id="registerform" runat="server">

                <!-- 2 column grid layout with text inputs for the first and last names -->
                <div class="row">
                  <div class="col-md-6 mb-4">
                    <div class="form-outline">
                      <asp:TextBox ID="txt_firstname" runat="server" CssClass="form-control"></asp:TextBox>
                      <label class="form-label" for="txt_firstname">First name</label>
                    </div>
                  </div>
                  <div class="col-md-6 mb-4">
                    <div class="form-outline">
                      <asp:TextBox ID="txt_lastname" runat="server" CssClass="form-control"></asp:TextBox>
                      <label class="form-label" for="txt_lastname">Last name</label>
                    </div>
                  </div>
                </div>

                <!-- Email input -->
                <div class="form-outline mb-4">
                  <asp:TextBox ID="txt_email" runat="server" CssClass="form-control"></asp:TextBox>
                  <label class="form-label" for="txt_email">Email</label>
                </div>

                <!-- Password input -->
                <div class="form-outline mb-4">
                  <asp:TextBox ID="txt_password" runat="server" CssClass="form-control" type="password"></asp:TextBox>
                  <label class="form-label" for="txt_password">Password</label>
                </div>

                <!-- Confirm Password input -->
                <div class="form-outline mb-4">
                  <asp:TextBox ID="txt_confpassword" runat="server" CssClass="form-control"></asp:TextBox>
                  <label class="form-label" for="txt_conf-password">Confirm Password</label>
                </div>

                <!-- Submit button -->
                <asp:Button ID="btn_signup" 
                    runat="server" 
                    type="submit" 
                    CssClass="btn btn-primary btn-block mb-4" 
                    OnClick="Signup" 
                    Text="Signup" 
                    AutoPostBack="true" /> 
              </form>
                <label class="form-label">Already member ?</label>
                <label class="form-label" style="color:#3B71CA; cursor:pointer;" onclick="openLoginPage();">Sign in</label>
            </div>
          </div>
        </div>
      </div>
    </section>
    <!-- Section: Design Block -->

    <!-- MDB -->
    <script
      type="text/javascript"
      src="https://cdnjs.cloudflare.com/ajax/libs/mdb-ui-kit/6.4.2/mdb.min.js"
    ></script>
</body>
</html>
