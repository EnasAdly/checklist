<%@ Page Language="C#" Async="true" AutoEventWireup="true" MasterPageFile="~/App2.Master" CodeBehind="DailyCheck.aspx.cs" Inherits="WebApplication6.DailyCheck" %>

<asp:Content ID="BodyCintent" ContentPlaceHolderID="Apph" runat="server">

<%--    TestCopy--%>
    <!DOCTYPE html>

    <html xmlns="http://www.w3.org/1999/xhtml">
    <head>
        <title></title>
        <style type="text/css">
            body {
                font-family: Arial;
                font-size: 10pt;
            }

            table {
                border: 1px solid #ccc;
                border-collapse: collapse;
                background-color: #fff;
            }

                table th {
                    background-color: #B8DBFD;
                    color: #333;
                    font-weight: bold;
                }

                table th, table td {
                    padding: 5px;
                    border: 1px solid #ccc;
                }

                table, table table td {
                    border: 0px solid #ccc;
                }

            label1 {
                margin-left: 72%;
            }
        </style>
         <%-- <script type="text/javascript" language="javascript">

            var idleinteral = setInterval("reloadPage()", 30000);

            function reloadPage() {
                location.reload();
            }

          </script>--%>
    </head>
    <body style="background-image:url('Images/background.jpg'); background-repeat: no-repeat;background-size: cover;">
        <form runat="server" style="width: initial;">
            <asp:ScriptManager ID="ScriptManager1" runat="server">
            </asp:ScriptManager>
            <div style="height: 100%; width: 100%; background: var(--bs-border-color-translucent); color: var(--bs-border-color-translucent);"></div>
            <!-- Start: 2 Rows 1+2 Columns -->
            <div style="padding-inline: 2%">
                <div class="row">
                    <div >
                        <!-- Start: Navbar Centered Links -->
                        <nav class="navbar navbar-light navbar-expand-md py-3" style="box-shadow: 0px 0px;">
                             <img  src="Images/barakalogo.png" width="8%" alt=" "/>
                            <div class="container">
                                <a class="navbar-brand d-flex align-items-center" href="#"><span><strong><em><span style="color: floralwhite; font-size:x-large; font-style:oblique">Applications</span></em></strong></span></a>
                                <button data-bs-toggle="collapse" class="navbar-toggler" data-bs-target="#navcol-3">
                                    <span class="visually-hidden">Toggle navigation</span>

                                    <span class="navbar-toggler-icon"></span>

                                </button>
                                <div style="margin-left: 55%;">
                                    <asp:Label ID="useri" runat="server"  ForeColor="FloralWhite" style="padding-right:10px;"  Font-Size="Larger"  Font-Bold="true" Text="Label"></asp:Label>
                                </div>
                                <asp:Button ID="Button1" class="btn btn-primary" runat="server" Font-Bold="true"  OnClick="logout_Click" Text="Log Out" />

                            </div>
                        </nav>
                        <!-- End: Navbar Centered Links -->
                    </div>
                </div>
                <div class="row" style="margin-top: 1%;">
                    <div style="height: 393%; border-style: inset; width: 217px;background-color:lightblue; font-style: oblique;">
                        <%--<asp:CheckBoxList ID="chkHobbies"  OnSelectedIndexChanged="CheckBox_Checked_Unchecked" AutoPostBack="true" Style="padding: 50px" ForeColor="Blue" runat="server">
                        </asp:CheckBoxList>--%>
                        <asp:RadioButtonList ID="chkHobbies"  OnSelectedIndexChanged="CheckBox_Checked_Unchecked" AutoPostBack="true" Style="padding: 50px" ForeColor="Blue" runat="server"></asp:RadioButtonList>

                    </div>
                    <div  style="width: 851px;">
                        <div>
                        </div>
                        <!-- Start: MUSA_panel-table -->
                        <link href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/4.4.0/css/font-awesome.min.css" rel='stylesheet' type='text/css'>

                        <div class="container">
                            <div class="row" style="margin-bottom: 12px">


                                <div>
                                    
                                    <div class="panel panel-default panel-table">
                                        <div class="panel-heading">
                                            <div class="row">
                                                <div style="margin-bottom: -4%">
                                                    

                                                    <%--<button type="button" class="btn btn-sm btn-primary btn-create">Create New</button>--%>
                                                </div>

                                                <div style="margin-left: 89%; margin-bottom: 1%; margin-top: 1%;">

<%--                                                    <asp:Button ID="submit" Font-Size="Large" Font-Italic="true" OnClick="submit_Click" CssClass="btn btn-sm btn-primary btn-create" BackColor="Red" Font-Bold="true" runat="server" Text="Submit Daily Check" />--%>
                                                    <asp:Button ID="btnExport" Font-Size="Large" Font-Italic="true" BackColor="Red" Font-Bold="true" CssClass="btn btn-sm btn-primary btn-create" OnClick= "Generatebtn"  runat="server" Text="Generate" />

<%--                                                    <asp:Button ID="Button2" Font-Size="Large" Font-Italic="true" CssClass="btn btn-sm btn-primary btn-create" onClick="FailedA_Click"  runat="server" Text="Failed Applications" />--%>
<%--                                                    <div style="margin-top: 1%">
                                                    <asp:Button ID="UpdateID" Visible="true"  OnClick="UpdateID_Click" CssClass="btn btn-sm btn-primary btn-create" runat="server" Text="Update Applications" />
                                                        </div>--%>
                                                </div>

                                            </div>
                                        </div>
                                    </div>
                                    <div style="width: 164%; height: 93%;">
                                        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                            <ContentTemplate>
                                            <%--    <asp:GridView ID="GridView3" runat="server" AutoGenerateColumns="false"
                                                    DataKeyNames="ApplicationID"
                                                    AllowPaging="true"
                                                    CellPadding="1"
                                                    OnRowEditing="GridView3_RowEditing"
                                                    OnRowCancelingEdit="GridView3_RowCancelingEdit"
                                                    OnRowUpdating="GridView3_RowUpdating"
                                                    CellSpacing="1"
                                                    PageSize="10"
                                                    OnPageIndexChanging="GridView3_PageIndexChanging"
                                                    Font-Bold="true"
                                                    Visible="true"
                                                    Font-Size="Smaller"
                                                    Style="text-align: center"
                                                    AlternatingRowStyle-Width="12"
                                                    EmptyDataText="No Record has been added.">
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="ID" Visible="false">
                                                            <ItemTemplate>
                                                                <asp:Label ID="AppID" runat="server" Text='<%# Eval("ApplicationId")%>'></asp:Label>
                                                            </ItemTemplate>

                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Application Name" ItemStyle-Width="150" Visible="false">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblName" runat="server" Text='<%# Eval("ApplicationName")%>'></asp:Label>
                                                            </ItemTemplate>
                                                          
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Server/Port" ItemStyle-Width="150">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblServerPort" runat="server" Text='<%# Eval("Serverport")%>'></asp:Label>
                                                            </ItemTemplate>

                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="CheckPoint" ItemStyle-Width="150px">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblstatusname" runat="server" Text='<%# Eval("statusname")%>'></asp:Label>
                                                            </ItemTemplate>
                                                            
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Validation" ItemStyle-Width="150">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblstutsDes" runat="server" Text='<%# Eval("StatusDesc")%>'></asp:Label>
                                                            </ItemTemplate>
                                                            
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Failed/success" ItemStyle-Width="150">
                                                            <ItemTemplate>
                                                                <div style="padding-left: -3px">
                                                                    <asp:RadioButton ID="Radiofale" GroupName="sts" Checked="true" Text="Failed" runat="server" />
                                                                </div>
                                                                <div style="padding-left: 12px">
                                                                    <asp:RadioButton ID="Radiosucc" GroupName="sts" Checked="true" Text="Success"  runat="server" />
                                                                </div>

                                                            </ItemTemplate>
                                                          

                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="ApplicationStatus" ItemStyle-Width="150">
                                                            <ItemTemplate>
                                                                <asp:Label ID="SuccessId" runat="server" Text='<%# Eval("success")%>'></asp:Label>
                                                            </ItemTemplate>
                                                            
                                                        </asp:TemplateField>
                                                          <asp:TemplateField HeaderText="Comment" ItemStyle-Width="150">
                                                            <ItemTemplate>
                                                                <asp:Label ID="CommentId" runat="server" Text='<%# Eval("Comment")%>'></asp:Label>
                                                            </ItemTemplate>
                                                            <EditItemTemplate>
                                                                <asp:TextBox ID="CommentBox" runat="server" placeholder='Add Comment' Width="140"></asp:TextBox>
                                                            </EditItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:CommandField ButtonType="Link" ShowEditButton="true" 
                                                                ItemStyle-Width="150" />

                                                    </Columns>
                                                </asp:GridView>--%>
                                                <asp:Label ForeColor="Green" ID="Completed" runat="server" Font-Bold="true" Font-Size="large" Text="Completed" Visible="false"></asp:Label>
                                                <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="false" 
                                                    DataKeyNames="ApplicationID"
                                                    AllowPaging="true"
                                                    CellPadding="1"
                                                    OnRowEditing="GridView_RowEditing"
                                                    OnRowCancelingEdit="GridView_RowCancelingEdit"
                                                    OnRowUpdating="GridView_RowUpdating"
                                                    CellSpacing="1"
                                                    PageSize="10"
                                                    OnPageIndexChanging="GridView1_PageIndexChanging"
                                                    Font-Bold="true"
                                                    Font-Size="Smaller"
                                                    Style="text-align: center; border:ridge;
                                                    border-color:floralwhite; background:border-box;"
                                                    AlternatingRowStyle-Width="12"
                                                    EmptyDataText="No Record has been added.">
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="ID" Visible="false">
                                                            <ItemTemplate>
                                                                <asp:Label ID="AppID" runat="server" Text='<%# Eval("ApplicationId")%>'></asp:Label>
                                                            </ItemTemplate>

                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Application Name" ItemStyle-Width="150" Visible="false">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblName" runat="server" Text='<%# Eval("ApplicationName")%>'></asp:Label>
                                                            </ItemTemplate>
                                                          
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Server/Port" ItemStyle-Width="150">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblServerPort" runat="server" Text='<%# Eval("Serverport")%>'></asp:Label>
                                                            </ItemTemplate>

                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Check Point" ItemStyle-Width="150px">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblstatusname" runat="server" Text='<%# Eval("statusname")%>'></asp:Label>
                                                            </ItemTemplate>
                                                            
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Validation" ItemStyle-Width="150">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblstutsDes" runat="server" Text='<%# Eval("StatusDesc")%>'></asp:Label>
                                                            </ItemTemplate>
                                                            
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Failed/success" ItemStyle-Width="150">
                                                            
                                                            <ItemTemplate>
                                                                 <div style="padding-left: -3px">
                                                                    <asp:RadioButton ID="Radiofale" GroupName="sts"  Text="Failed" runat="server" />
                                                                </div>
                                                                <div style="padding-left: 10px">
                                                                    <asp:RadioButton ID="Radiosucc" GroupName="sts"  Text="Success"  runat="server" />
                                                                </div>
                                                            </ItemTemplate>
                                                             <EditItemTemplate>
                                                                <div style="padding-left: -3px">
                                                                    <asp:RadioButton ID="Radiofale" GroupName="sts" Checked="true" Text="Failed" runat="server" />
                                                                </div>
                                                                <div style="padding-left: 12px">
                                                                    <asp:RadioButton ID="Radiosucc" GroupName="sts"  Text="Success"  runat="server" />
                                                                </div>

                                                            </EditItemTemplate>
                                                          

                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="ApplicationStatus" ItemStyle-Width="150">
                                                            <ItemTemplate>
                                                                <asp:Label ID="SuccessId" runat="server" Text='<%# Eval("success")%>'></asp:Label>
                                                            </ItemTemplate>
                                                            
                                                        </asp:TemplateField>
                                                          <asp:TemplateField HeaderText="Comment" ItemStyle-Width="150">
                                                            <ItemTemplate>
                                                                <asp:Label ID="CommentId"  runat="server"  Text='<%# Eval("Comment")%>'></asp:Label>
                                                            </ItemTemplate>
                                                            <EditItemTemplate>
                                                                <asp:TextBox ID="CommentBox"  runat="server" placeholder='Add Comment' Width="140"></asp:TextBox>
                                                            </EditItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField>
                                                            <ItemTemplate>
                                                                <asp:Button ID="btn_Edit" runat="server" Font-Bold="true"  BackColor="White" Text="Edit" CommandName="Edit" />
                                                            </ItemTemplate>
                                                        <EditItemTemplate>
                                                                <div style="padding-bottom: 15%;">
                                                                <asp:Button ID="btn_Update1" ForeColor="Black" BackColor="White" Font-Bold="true" BorderColor="Red" runat="server" Text="Update" CommandName="Update" />
                                                                     </div>
                                                                <div>
                                                                <asp:Button ID="btn_Cancel1" runat="server" Font-Bold="true"  BackColor="White" ForeColor="Black" BorderColor="Red"  Text="Cancel" CommandName="Cancel" />
                                                                   </div>
                                                            </EditItemTemplate>
                                                            </asp:TemplateField>
                                                    </Columns>
                                                </asp:GridView>
                                                <script type="text/javascript" src="https://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js">

                                                </script>
                                                <%-- <script type="text/javascript" src="grid/scripts/jquery.blockUI.js"></script>
                                                    <script type="text/javascript">
                                                        $(function () {
                                                            BlockUI("dvGrid");
                                                            $.blockUI.defaults.css = {};
                                                        });
                                                        function BlockUI(elementID) {
                                                            var prm = Sys.WebForms.PageRequestManager.getInstance();
                                                            prm.add_beginRequest(function () {
                                                                $("#" + elementID).block({
                                                                    message: '<div align = "center">' + '<img src="grid/images/loadingAnim.gif"/></div>',
                                                                    css: {},
                                                                    overlayCSS: { backgroundColor: '#000000', opacity: 0.6, border: '3px solid #63B2EB' }
                                                                });
                                                            });
                                                            prm.add_endRequest(function () {
                                                                $("#" + elementID).unblock();
                                                            });
                                                        };
                                                    </script>--%>
                                            </ContentTemplate>

                                        </asp:UpdatePanel>

                                    </div>



                                </div>
                            </div>
                        </div>
                        <!-- End: MUSA_panel-table -->
                    </div>
                </div>
            </div>
            <asp:HiddenField ID="H_GID" runat="server" />
            <!-- End: 2 Rows 1+2 Columns -->
            <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.2.2/dist/js/bootstrap.bundle.min.js"></script>
        </form>
    </body>
    </html>
</asp:Content>
