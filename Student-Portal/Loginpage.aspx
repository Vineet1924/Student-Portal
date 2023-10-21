<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Loginpage.aspx.cs" Inherits="Student_Portal.Loginpage" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Sign in</title>
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

    <script type="text/javascript">
        history.pushState(null, null, document.URL);
        window.addEventListener('popstate', function () {
            history.pushState(null, null, document.URL);
        });
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
              <h2 class="fw-bold mb-5">Sign in now</h2>
              <h5><asp:Label ID="lbl_error" runat="server" ForeColor="Red" Font-Names="Inter" Visible="true" /></h5>
              <form id="loginform" runat="server">
                
                <!-- Email input -->
                <div class="form-outline mb-4">
                  <asp:TextBox ID="txt_email" runat="server" CssClass="form-control"></asp:TextBox>
                  <label class="form-label" for="txt_email">Email</label>
                </div>

                <!-- Password input -->
                <div class="form-outline mb-4">
                  <asp:TextBox ID="txt_password" runat="server" CssClass="form-control"></asp:TextBox>
                  <label class="form-label" for="txt_password">Password</label>
                </div>

                <!-- Submit button -->
                <asp:Button ID="btn_signin" 
                    runat="server" 
                    type="submit" 
                    CssClass="btn btn-primary btn-block mb-4" 
                    OnClick="Signin" 
                    Text="Signin" 
                    AutoPostBack="true" /> 
              </form>
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
