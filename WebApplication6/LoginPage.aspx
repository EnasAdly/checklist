<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site1.Master" CodeBehind="LoginPage.aspx.cs" Inherits="WebApplication6.LoginPage" %>
<asp:Content ID="BodyCintent" ContentPlaceHolderID="head" runat="server">

<!DOCTYPE html>

<html>
<head>
    <title></title>
</head>
<body>
    <div class="svg-container">
<div class="waveWrapper waveAnimation">
  <div class="waveWrapperInner bgTop">
    <div class="wave waveTop" style="background-image: url('http://front-end-noobs.com/jecko/img/wave-top.png')"></div>
  </div>
  <div class="waveWrapperInner bgMiddle">
    <div class="wave waveMiddle" style="background-image: url('http://front-end-noobs.com/jecko/img/wave-mid.png')"></div>
  </div>
  <div class="waveWrapperInner bgBottom">
    <div class="wave waveBottom" style="background-image: url('http://front-end-noobs.com/jecko/img/wave-bot.png')"></div>
  </div>
</div>
</div>

	<div data-aos="fade-down" data-aos-duration="1000" data-aos-delay="100" data-aos-once="true" class="login-card" style="font-family:Roboto, sans-serif;">
        <p class="profile-name-card"> 
            <i class="fa fa-unlock-alt d-inline" style="width:0;height:0;font-size:56px;color:rgb(104,145,162);"></i>

        </p><form class="form-signin" runat="server" id="form1">
            <span class="reauth-email" style="margin:11px;">

            </span>
<%--           <input class="form-control" type="email" id="inputEmail" required="required" placeholder="User Name" autofocus="autofocus">--%>
            <asp:TextBox ID="username" Text="aaai" CssClass="form-control" runat="server" required="required" placeholder="User Name" autofocus="autofocus"></asp:TextBox>
            <asp:TextBox ID="PassWord" Text="DeXm@s202305" CssClass="form-control" type="password" runat="server" required="required" placeholder="Password" autofocus="autofocus"></asp:TextBox>
            <asp:Label ID="LB_errorLogin" ForeColor="Red" Font-Bold="true" Font-Size="Medium" runat="server" Visible= false></asp:Label>
<%--            <input class="form-control" type="password" id="inputPassword" required="required" placeholder="Password">--%>
            <%--   <button class="btn btn-primary btn-lg d-block btn-signin w-100" style="font-family:Roboto, sans-serif;font-size:16px;font-weight:normal;font-style:normal;" type="submit">Sign in</button>--%>

            <asp:Button ID="loginBt" CssClass="btn btn-primary btn-lg d-block btn-signin w-100" style="font-family:Roboto, sans-serif;font-size:16px;font-weight:normal;font-style:normal;" runat="server" Text="Sign in" OnClick="loginBt_Click" />
            </form></div>
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.2.2/dist/js/bootstrap.bundle.min.js"></script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/aos/2.3.4/aos.js"></script>
    <script src="assets/js/script.min.js"></script></body>

</html>
    </asp:Content>