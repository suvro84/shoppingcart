<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ajaxSubmitForm.aspx.cs" Inherits="ajaxSubmitForm" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Untitled Page</title>
        <script language="javaScript" type="text/javascript" src="js/jsCommonFunction.js"></script>

</head>
<body>
    <form id="form1" runat="server">
    <div>
      <asp:DropDownList ID="billState" onchange="javascript:setDDtextToObj('hdnBillStateName', this);" runat="server">
    </asp:DropDownList>
    </div>
    
     <asp:DropDownList ID="shipState"  onchange="javascript:setDDtextToObj('hdnShipCityName', this);" runat="server">
    </asp:DropDownList>
    </form>
  
</body>
</html>
