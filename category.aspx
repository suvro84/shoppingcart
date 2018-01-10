<%@ Page Language="C#" AutoEventWireup="true" CodeFile="category.aspx.cs" Inherits="category" %>

<%@ Register Src="uc_SubCat_new.ascx" TagName="uc_SubCat_new" TagPrefix="uc2" %>

<%@ Register Src="uc_subCat.ascx" TagName="uc_subCat" TagPrefix="uc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Untitled Page</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <asp:Label ID="lblbreadcrum" runat="server"></asp:Label>
        <uc2:uc_SubCat_new ID="Uc_SubCat_new1" runat="server" />
<%--        <uc1:uc_subCat ID="Uc_subCat1" runat="server" />
--%>



    </div>
    </form>
</body>
</html>
