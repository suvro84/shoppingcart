<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Login.aspx.cs" Inherits="Login" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <div id="dvlogin" runat="server"></div>
      <div id="dvlogout" runat="server"></div>
        <div id="Welcome" runat="server"></div>
    
        <asp:TextBox ID="txtusername" runat="server"></asp:TextBox>
        
        <asp:TextBox ID="txtpwd" runat="server"></asp:TextBox>
        <asp:Button ID="btnLogin" runat="server" Text="Login" 
            onclick="btnLogin_Click" />
    
    </div>
    <asp:Label ID="lblError" runat="server" Text="Label"></asp:Label>
    </form>
</body>
</html>
