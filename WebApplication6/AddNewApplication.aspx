<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site3.Master" CodeBehind="AddNewApplication.aspx.cs" Inherits="WebApplication6.AddNewApplication" %>

<asp:Content ID="BodyCintent" ContentPlaceHolderID="head3" runat="server">
    <!DOCTYPE html>
    <html lang="en">
    <%--<head>
   
</head>--%>
    <body style="background-image: url('Images/background.jpg'); background-repeat: no-repeat; background-size: cover;">
        <!-- Start: Black Navbar -->
        <nav class="navbar navbar-light navbar-expand-md navbar-fixed-top navigation-clean-button" style="background: border-box; border-radius: 20; border-top-left-radius: 20; border-top-right-radius: 20; border-bottom-right-radius: 20; border-bottom-left-radius: 20; border-style: none; padding-top: 0; padding-bottom: 10px;">
            <div class="container">

                <button data-bs-toggle="collapse" class="navbar-toggler" data-bs-target="#navcol-1">
                    <span class="visually-hidden">Toggle navigation</span>
                    <span class="navbar-toggler-icon"></span>
                </button>
                <div>
                    <a class="navbar-brand" href="App2.aspx"><span style="font-size: xx-large">Applications </span></a>
                </div>
                <%--<div class="collapse navbar-collapse" id="navcol-1" style="color: rgb(255,255,255);">
                    <ul class="navbar-nav nav-right">
                        <li class="nav-item"><a class="nav-link active" href="ApplicationDetails.aspx" style="color: rgba(224,217,217,0.9);">
                            <strong>Applications</strong></a></li>
                        <li class="nav-item"><a class="nav-link" href="about.html" style="color: rgba(224,217,217,0.9);"><strong>Applications</strong></a></li>
                        <li class="nav-item"></li>
                        <li class="nav-item"></li>
                    </ul>
                    <!-- Start: Actions -->
                    <p class="ms-auto navbar-text actions"><a class="btn btn-light action-button" role="button" href="signup.html" style="color: rgba(0,0,0,0.9); background: var(--bs-gray-200); border-radius: 10px; border-style: solid; border-color: rgba(0,0,0,0.9); font-size: 16px; padding: 5px 8px;">DashBoard</a></p>
                    <!-- End: Actions -->
                </div>--%>
            </div>
        </nav>
        <!-- End: Black Navbar -->
        <!-- Start: MUSA_panel-table -->
        <link href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/4.4.0/css/font-awesome.min.css" rel='stylesheet' type='text/css'>

    

                <p></p>
                <h1></h1>
        <div class="container">

                <form runat="server">



                    

                    <div class="row">
                        <div class="col">
                            <div class="card text-white bg-primary" style="border-color: floralwhite; border-width: medium; width: fit-content; padding: 2%; padding-left: 1%; margin-left:77%">

                                <asp:Button ID="AddNewApp" OnClick="AddNewApp_Click" CssClass="btn btn-sm btn-primary btn-create" Font-Size="XX-Large" runat="server" Text="Add New Application" />



                            </div>
                        </div>
                        <div class="col">
                            <div class="card text-white bg-primary" style="border-color: floralwhite; border-width: medium; width: fit-content; padding: 2%; padding-left: 1%;margin-left:33%"">

                                <asp:Button ID="CreateApp" OnClick="CreatNew_Config" CssClass="btn btn-sm btn-primary btn-create" Font-Size="XX-Large" runat="server" Text="Add New Configuration" />



                            </div>
                        </div>
                    </div>
                    



                    <div >
                        <asp:PlaceHolder ID="AddNewAppH" runat="server" Visible="true">
                          <h3 class="panel-title" style="text-decoration: underline;text-decoration-color: floralwhite;font-style: italic;color: currentColor;">Add New Application</h3>
                        </asp:PlaceHolder>

                        <asp:PlaceHolder ID="NewConfigH" runat="server" Visible="true">
                        <h3 class="panel-title" style="text-decoration: underline;text-decoration-color: floralwhite;font-style: italic;color: currentColor;">Add New Application</h3>

                        </asp:PlaceHolder>
                    </div>
                    <div class="panel-body" style="margin-top:3%">
                        <asp:PlaceHolder ID="PlaceHolder1" runat="server" Visible="true">
                            <table class="table table-striped table-bordered table-list" style="border-style:groove">
                                <thead >
                                    <tr style="text-align:center; font-style:italic;font-size:large">
                                        <th>Application Name</th>
                                        <th>Server Port</th>
                                        <th>Check Point</th>
                                        <th>Validation</th>
                                    </tr>
                                </thead>
                                <tbody style="text-align:center">
                                    <tr >
                                        <td class="hidden-xs">

                                            <asp:DropDownList ID="DropApplication" runat="server">
                                            </asp:DropDownList>
                                        </td>
                                        <td class="hidden-xs">

                                            <asp:DropDownList ID="DropServer" runat="server">
                                            </asp:DropDownList>
                                        </td>

                                        <td class="hidden-xs">

                                            <asp:TextBox ID="statusBoc"  runat="server" required="required" placeholder="Check Point" autofocus="autofocus"></asp:TextBox>
                                        </td>


                                        <td class="hidden-xs">

                                            <asp:TextBox ID="DescriptionId"  runat="server" required="required" placeholder="Validation" autofocus="autofocus"></asp:TextBox>
                                        </td>

                                    </tr>
                                </tbody>
                            </table>
                           <asp:Button ID="Button2" runat="server" BackColor="Red" ForeColor="White" Font-Bold="true" OnClick="Button2_Click" Text="Submit" />

                        </asp:PlaceHolder>
                        <asp:PlaceHolder ID="AddNewID" runat="server" Visible="false">
                            <table class="table table-striped table-bordered table-list" style="border-style:groove">
                                <thea >
                                    <tr style="text-align:center; font-style:italic;font-size:large">
                                        <th>Application Name</th>
                                        <th>Server Port</th>
                                        <th>Check Point</th>
                                        <th>Validation</th>
                                        <th>Team</th>
                                    </tr>
                                </thea>
                                <tbody style="text-align:center">
                                    <tr style="text-align:center">
                                        <td class="hidden-xs">

                                            <asp:TextBox ID="AppName"  placeholder="Application Name" required="required" autofocus="autofocus" runat="server" />
                                        </td>
                                        <td class="hidden-xs">

                                            <asp:TextBox ID="ServerPort"  runat="server" placeholder="ServerPort" required="required" autofocus="autofocus" />
                                        </td>

                                        <td class="hidden-xs">

                                            <asp:TextBox ID="CheckPointID"  runat="server" placeholder="Check Point" required="required" autofocus="autofocus"></asp:TextBox>
                                        </td>


                                        <td class="hidden-xs">

                                            <asp:TextBox ID="ValidationID" runat="server" placeholder="Validation" required="required" autofocus="autofocus"></asp:TextBox>
                                        </td>
                                        <td class="hidden-xs">
                                            <asp:DropDownList ID="DropTeam" runat="server">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                </tbody>
                            </table>
                            <asp:Button ID="Submit" runat="server" BackColor="Red" ForeColor="White" Font-Bold="true" OnClick="Submit_Click" Text="Submit" />

                        </asp:PlaceHolder>
                    </div>
                     </form>
                    </div>
               

         

        <!-- End: MUSA_panel-table -->
        <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.2.2/dist/js/bootstrap.bundle.min.js"></script>
    </body>
    </html>
</asp:Content>
