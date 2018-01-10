<%@ Page Language="C#" AutoEventWireup="true" CodeFile="24x7_PaymentOption.aspx.cs" Inherits="_24x7_PaymentOption" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
        <asp:ImageButton ID="ImgSaveContinue" runat="server" 
             style="width: 14px" onclick="ImgSaveContinue_Click" />
        <asp:ImageButton ID="ImgSaveMakeNewOrder" runat="server" 
            onclick="ImgSaveMakeNewOrder_Click" style="height: 16px; width: 14px" />
      
        <asp:Button ID="btnSubmit" runat="server" Text="Button" 
            onclick="btnSubmit_Click" />
        
    <div id="dvPaymentOptList" runat="server">
    
    </div>
      <div id="dvBtn" runat="server">
    
    </div>
        
    </div>
    <input id="HiddenSbill" runat="server" type="hidden" />
     <input id="HiddenCombo" runat="server" type="hidden" />
      <input id="Hidden2" runat="server" type="hidden" />
    
     <input id="hdnPgId" type="hidden" runat="server" />
    <input id="hdnPoId" type="hidden" runat="server" />
    <input id="hdPoPgRank" type="hidden" runat="server" />
     <input id="hdnOrder" type="hidden" runat="server" />
    
    <span id="tblAddMOre" runat="server"></span>
     <span id="tblAddNew" runat="server"></span>
      <span id="trOption1" runat="server"></span>
         <span id="divPayoption" runat="server"></span>
            <span id="ptext" runat="server"></span>
      
        <span id="topMsg" runat="server"></span>
    
    </form>
</body>
</html>
