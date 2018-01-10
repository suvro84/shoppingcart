<%@ Page Language="C#" AutoEventWireup="true" CodeFile="24x7_Fail.aspx.cs" Inherits="_24x7_Fail" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:Label ID="lblError" runat="server" Text="Label"></asp:Label>
        <asp:Button ID="btnTryAgainSame" runat="server" Text="Button" OnClick="btnTryAgainSame_Click" />
        <asp:Button ID="btnTryAgainNext" runat="server" Text="Button" OnClick="btnTryAgainNext_Click" />
        <asp:Button ID="btnTryAgainPoption" runat="server" Text="Button" OnClick="btnTryAgainPoption_Click" />
        <div id="dvBtnSame" runat="server">
        </div>
        <div id="dvBtnNext" runat="server">
        </div>
        <div id="dvBtnOther" runat="server">
        </div>
        <div id="dvMain" runat="server">
        </div>
    </div>
    <input id="hdnCombo" type="hidden" runat="server" />
    <input id="hdnSiteId" type="hidden" runat="server" />
    <input id="hdnPgId" type="hidden" runat="server" />
    <input id="hdnPoId" type="hidden" runat="server" />
    <input id="hdPoPgRank" type="hidden" runat="server" />
     <input id="hdnOrder" type="hidden" runat="server" />
        <input id="hdnPgName" type="hidden" runat="server" />
        <input id="hdnPoName" type="hidden" runat="server" />
     
     
      <div id="topMsg" runat="server">
        </div>
    
    </form>
</body>
</html>
