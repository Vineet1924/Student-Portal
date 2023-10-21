﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="addStudent.aspx.cs" Inherits="Student_Portal.addStudent" %>

<!DOCTYPE html>

<html
  lang="en"
  class="light-style layout-menu-fixed layout-compact"
  dir="ltr"
  data-theme="theme-default"
  data-assets-path="../assets/"
  data-template="vertical-menu-template-free">
  <head>
    <meta charset="utf-8" />
    <meta
      name="viewport"
      content="width=device-width, initial-scale=1.0, user-scalable=no, minimum-scale=1.0, maximum-scale=1.0" />

    <title>Add student</title>

    <meta name="description" content="" />
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
    <link href="datepicker.css" rel="stylesheet" />
    <!-- MDB -->
    <link
      href="https://cdnjs.cloudflare.com/ajax/libs/mdb-ui-kit/6.4.2/mdb.min.css"
      rel="stylesheet"
    />

    <!-- Favicon -->
    <link rel="icon" type="image/x-icon" href="../assets/img/favicon/favicon.ico" />

    <!-- Fonts -->
    <link rel="preconnect" href="https://fonts.googleapis.com" />
    <link rel="preconnect" href="https://fonts.gstatic.com" crossorigin />
    <link
      href="https://fonts.googleapis.com/css2?family=Public+Sans:ital,wght@0,300;0,400;0,500;0,600;0,700;1,300;1,400;1,500;1,600;1,700&display=swap"
      rel="stylesheet" />

    <link rel="stylesheet" href="../assets/vendor/fonts/boxicons.css" />

    <!-- Core CSS -->
    <link rel="stylesheet" href="../assets/vendor/css/core.css" class="template-customizer-core-css" />
    <link rel="stylesheet" href="../assets/vendor/css/theme-default.css" class="template-customizer-theme-css" />
    <link rel="stylesheet" href="../assets/css/demo.css" />
    <link rel="stylesheet" href="table.css" />

    <!-- Vendors CSS -->
    <link rel="stylesheet" href="../assets/vendor/libs/perfect-scrollbar/perfect-scrollbar.css" />
    <link rel="stylesheet" href="../assets/vendor/libs/apex-charts/apex-charts.css" />

    <!-- Page CSS -->

    <!-- Helpers -->
    <script src="../assets/vendor/js/helpers.js"></script>
    <script src="../assets/js/config.js"></script>
  </head>

  <body style="overflow:hidden">
    <!-- Layout wrapper -->
    <div class="layout-wrapper layout-content-navbar">
      <div class="layout-container">
        <!-- Menu -->

        <aside id="layout-menu" class="layout-menu menu-vertical menu bg-menu-theme">
          <div class="app-brand demo">
            <a href="index.html" class="app-brand-link">
              <span class="app-brand-logo demo">
                <svg
                  width="25"
                  viewBox="0 0 25 42"
                  version="1.1"
                  xmlns="http://www.w3.org/2000/svg"
                  xmlns:xlink="http://www.w3.org/1999/xlink">
                  <defs>
                    <path
                      d="M13.7918663,0.358365126 L3.39788168,7.44174259 C0.566865006,9.69408886 -0.379795268,12.4788597 0.557900856,15.7960551 C0.68998853,16.2305145 1.09562888,17.7872135 3.12357076,19.2293357 C3.8146334,19.7207684 5.32369333,20.3834223 7.65075054,21.2172976 L7.59773219,21.2525164 L2.63468769,24.5493413 C0.445452254,26.3002124 0.0884951797,28.5083815 1.56381646,31.1738486 C2.83770406,32.8170431 5.20850219,33.2640127 7.09180128,32.5391577 C8.347334,32.0559211 11.4559176,30.0011079 16.4175519,26.3747182 C18.0338572,24.4997857 18.6973423,22.4544883 18.4080071,20.2388261 C17.963753,17.5346866 16.1776345,15.5799961 13.0496516,14.3747546 L10.9194936,13.4715819 L18.6192054,7.984237 L13.7918663,0.358365126 Z"
                      id="path-1"></path>
                    <path
                      d="M5.47320593,6.00457225 C4.05321814,8.216144 4.36334763,10.0722806 6.40359441,11.5729822 C8.61520715,12.571656 10.0999176,13.2171421 10.8577257,13.5094407 L15.5088241,14.433041 L18.6192054,7.984237 C15.5364148,3.11535317 13.9273018,0.573395879 13.7918663,0.358365126 C13.5790555,0.511491653 10.8061687,2.3935607 5.47320593,6.00457225 Z"
                      id="path-3"></path>
                    <path
                      d="M7.50063644,21.2294429 L12.3234468,23.3159332 C14.1688022,24.7579751 14.397098,26.4880487 13.008334,28.506154 C11.6195701,30.5242593 10.3099883,31.790241 9.07958868,32.3040991 C5.78142938,33.4346997 4.13234973,34 4.13234973,34 C4.13234973,34 2.75489982,33.0538207 2.37032616e-14,31.1614621 C-0.55822714,27.8186216 -0.55822714,26.0572515 -4.05231404e-15,25.8773518 C0.83734071,25.6075023 2.77988457,22.8248993 3.3049379,22.52991 C3.65497346,22.3332504 5.05353963,21.8997614 7.50063644,21.2294429 Z"
                      id="path-4"></path>
                    <path
                      d="M20.6,7.13333333 L25.6,13.8 C26.2627417,14.6836556 26.0836556,15.9372583 25.2,16.6 C24.8538077,16.8596443 24.4327404,17 24,17 L14,17 C12.8954305,17 12,16.1045695 12,15 C12,14.5672596 12.1403557,14.1461923 12.4,13.8 L17.4,7.13333333 C18.0627417,6.24967773 19.3163444,6.07059163 20.2,6.73333333 C20.3516113,6.84704183 20.4862915,6.981722 20.6,7.13333333 Z"
                      id="path-5"></path>
                  </defs>
                  <g id="g-app-brand" stroke="none" stroke-width="1" fill="none" fill-rule="evenodd">
                    <g id="Brand-Logo" transform="translate(-27.000000, -15.000000)">
                      <g id="Icon" transform="translate(27.000000, 15.000000)">
                        <g id="Mask" transform="translate(0.000000, 8.000000)">
                          <mask id="mask-2" fill="white">
                            <use xlink:href="#path-1"></use>
                          </mask>
                          <use fill="#696cff" xlink:href="#path-1"></use>
                          <g id="Path-3" mask="url(#mask-2)">
                            <use fill="#696cff" xlink:href="#path-3"></use>
                            <use fill-opacity="0.2" fill="#FFFFFF" xlink:href="#path-3"></use>
                          </g>
                          <g id="Path-4" mask="url(#mask-2)">
                            <use fill="#696cff" xlink:href="#path-4"></use>
                            <use fill-opacity="0.2" fill="#FFFFFF" xlink:href="#path-4"></use>
                          </g>
                        </g>
                        <g
                          id="Triangle"
                          transform="translate(19.000000, 11.000000) rotate(-300.000000) translate(-19.000000, -11.000000) ">
                          <use fill="#696cff" xlink:href="#path-5"></use>
                          <use fill-opacity="0.2" fill="#FFFFFF" xlink:href="#path-5"></use>
                        </g>
                      </g>
                    </g>
                  </g>
                </svg>
              </span>
              <span class="app-brand-text demo menu-text fw-bold ms-2">Dashboard</span>
            </a>

            <a href="javascript:void(0);" class="layout-menu-toggle menu-link text-large ms-auto d-block d-xl-none">
              <i class="bx bx-chevron-left bx-sm align-middle"></i>
            </a>
          </div>

          <div class="menu-inner-shadow"></div>

          <ul class="menu-inner py-1">            
            <li class="menu-item">
              <a
                href="teacherDashboard.aspx"
                target="_blank"
                class="menu-link">
                <i class="menu-icon tf-icons bx bx-home-circle"></i>
                <div data-i18n="Email">Dashboard</div>
              </a>
            </li>

            <li class="menu-header small text-uppercase"><span class="menu-header-text">Operations</span></li>

            <li class="menu-item active">
              <asp:HyperLink
                runat="server"
                NavigateUrl="addStudent.aspx"
                target="_blank"
                CssClass="menu-link">
                <img src="Images/student.png" width="25" height="25"/>
                <div data-i18n="Chat" style="margin-left:10px">Add student</div>
              </asp:HyperLink>
            </li>
            <li class="menu-item">
              <asp:HyperLink
                  runat="server"
                  NavigateUrl="updateStudent.aspx"
                  target="_blank"
                  CssClass="menu-link">
                <img src="Images/user-avatar.png" width="25" height="25"/>
                <div data-i18n="Chat" style="margin-left:10px">Update student</div>
              </asp:HyperLink>
            </li>
            <li class="menu-item">
              <asp:HyperLink
                runat="server"
                NavigateUrl="removeStudent.aspx"
                target="_blank"
                CssClass="menu-link">
                <img src="Images/remove-user.png" width="22" height="22"/>
                <div data-i18n="Chat" style="margin-left:10px">Remove student</div>
              </asp:Hyperlink>
            </li>

            <li class="menu-header small text-uppercase"><span class="menu-header-text">Attendance</span></li>

            <li class="menu-item">
              <asp:HyperLink
                runat="server"
                NavigateUrl="students.aspx"
                target="_blank"
                CssClass="menu-link">
                <img src="Images/education.png" width="25" height="25"/>
                <div data-i18n="Chat" style="margin-left:10px">Students</div>
              </asp:HyperLink>
            </li>

            <li class="menu-header small text-uppercase"><span class="menu-header-text">Exam</span></li>

            <li class="menu-item">
              <asp:HyperLink
                runat="server"
                NavigateUrl="exam.aspx"
                target="_blank"
                CssClass="menu-link">
                <img src="Images/exam.png" width="25" height="25"/>
                <div data-i18n="Chat" style="margin-left:10px">Marks</div>
              </asp:HyperLink>
            </li>
            <li class="menu-item">
              <asp:HyperLink
                runat="server"
                NavigateUrl="hallTicket.aspx"
                target="_blank"
                CssClass="menu-link">
                <img src="Images/document.png" width="25" height="25"/>
                <div data-i18n="Chat" style="margin-left:10px">Hall ticket</div>
              </asp:HyperLink>
            </li>
          </ul>
        </aside>
        <!-- / Menu -->

        <!-- Layout container -->
        <div class="layout-page">
          <!-- Content wrapper -->
          <div class="content-wrapper">
            <!-- Content -->
            <div class="container-xxl flex-grow-1 container-p-y">
              <div class="row">
                <div class="col-lg-12 mb-4 order-0">
                  <div class="card" style="padding:30px;">
                    <div class="d-flex align-items-end row">
                      <div class="col-sm-12">
                        <form id="registerform" runat="server">
                            <div class="form-outline mb-4">
                              <asp:TextBox ID="txt_enrollmentNo" runat="server" CssClass="form-control"></asp:TextBox>
                              <label class="form-label">Enrollment No</label>
                            </div>
                              <!-- 2 column grid layout with text inputs for the first and last names -->
                              <div class="row">
                                <div class="col-md-6 mb-4">
                                  <div class="form-outline">
                                    <asp:TextBox ID="txt_firstname" runat="server" CssClass="form-control"></asp:TextBox>
                                    <label class="form-label">First name</label>
                                  </div>
                                </div>
                                <div class="col-md-6 mb-4">
                                  <div class="form-outline">
                                    <asp:TextBox ID="txt_lastname" runat="server" CssClass="form-control"></asp:TextBox>
                                    <label class="form-label">Last name</label>
                                  </div>
                                </div>
                              </div>
                              <!-- Email input -->
                              <div class="form-outline mb-4">
                                <asp:TextBox ID="txt_email" runat="server" CssClass="form-control" type="email"></asp:TextBox>
                                <label class="form-label" for="txt_email">Email</label>
                              </div>

                              <!-- Password input -->
                              <div class="form-outline mb-4">
                                <asp:TextBox ID="txt_password" runat="server" CssClass="form-control" type="password"></asp:TextBox>
                                <label class="form-label" for="txt_password">Password</label>
                              </div>

                              <div class="form-outline mb-4">
                                  <asp:TextBox ID="txt_phone" runat="server" CssClass="form-control" type="number"></asp:TextBox>
                                  <label class="form-label">Phone</label>
                               </div>
                           
                              
                            <div class="row">
                              <div class="col-md-6 mb-4">
                                <div style="padding-bottom:8px; margin-bottom:10px; width:1200px;">
                                     <input type="date" name="dateofbirth" Id="dateofbirth" class="datePicker" runat="server">
                                </div>
                              </div>
                              <div class="col-md-6 mb-4">

                            <asp:DropDownList ID="departmentDropdown" runat="server" CssClass="form-control">
                                <asp:ListItem Text="Select Department" Value="" />
                                
                            </asp:DropDownList>
                        </div>
                            </div>
                              <!-- Submit button -->
                              <asp:Button
                                ID="btn_insertStudent"
                                runat="server"
                                Text="Submit"
                                CssClass="btn btn-primary btn-block mb-4"    
                                OnClick="btn_insertStudent_Click" />  
                            
                        <h5><asp:Label ID="lbl_error" runat="server" ForeColor="Red" Font-Names="Inter" Visible="true" /></h5>
                        </form>
                      </div>
                    </div>
                  </div>
                </div>
                </div>
                </div>
                <div style="height:400px; width:1210px; margin-bottom:440px; margin-left:30px; overflow-x:hidden; padding-right:10px">
                    <asp:Literal runat="server" ID="studentTable"></asp:Literal>
                </div>
              </div>
            </div>
          </div>
        </div>

    <!-- Core JS -->
    <!-- build:js assets/vendor/js/core.js -->

    <script src="../assets/vendor/libs/jquery/jquery.js"></script>
    <script src="../assets/vendor/libs/popper/popper.js"></script>
    <script src="../assets/vendor/js/bootstrap.js"></script>
    <script src="../assets/vendor/libs/perfect-scrollbar/perfect-scrollbar.js"></script>
    <script src="../assets/vendor/js/menu.js"></script>

    <!-- endbuild -->

    <!-- Vendors JS -->
    <script src="../assets/vendor/libs/apex-charts/apexcharts.js"></script>

    <!-- Main JS -->
    <script src="../assets/js/main.js"></script>

    <!-- Page JS -->
    <script src="../assets/js/dashboards-analytics.js"></script>

    <!-- Place this tag in your head or just before your close body tag. -->
    <script async defer src="https://buttons.github.io/buttons.js"></script>
    <script
      type="text/javascript"
      src="https://cdnjs.cloudflare.com/ajax/libs/mdb-ui-kit/6.4.2/mdb.min.js"
    ></script>
  </body>
</html>
