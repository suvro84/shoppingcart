<%@ Page Language="C#" AutoEventWireup="true" CodeFile="EditCategory.aspx.cs" Inherits="EditCategory" %>

<%@ Register Src="ucMenu.ascx" TagName="ucMenu" TagPrefix="uc1" %>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Untitled Page</title>
</head>
<body>
    <form id="form1" runat="server">
        <div>

            <asp:TreeView ID="TreeView1" ExpandDepth="0" PopulateNodesFromClient="true" ShowLines="true"
                ShowExpandCollapse="true" runat="server" OnTreeNodePopulate="TreeView1_TreeNodePopulate1" />
       
        </div>
        
         <asp:Button ID="brnAdd" runat="server" Text="Add" OnClick="brnAdd_Click" />
        <asp:Button ID="btnCheckAll" runat="server" Text="Check All" OnClick="btnCheckAll_Click" />
        
        
        <uc1:ucMenu ID="UcMenu1" runat="server" />
        
        
        
          
        
    </form>
   
</body>
</html>
