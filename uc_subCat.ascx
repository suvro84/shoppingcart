<%@ Control Language="C#" AutoEventWireup="true" CodeFile="uc_subCat.ascx.cs" Inherits="uc_subCat" %>
<asp:Repeater ID="rptSubcatname"  runat="server">
<HeaderTemplate>
SubCategories
</HeaderTemplate>
<ItemTemplate >
<table cellpadding="0" cellspacing="0" width="100%">
<tr>
<td>
<a href="category.aspx?catid=<%#Eval("Category_Id")%>&pageno=1"> 
<%#Eval("Category_Name") %>
</a>
</td>
</tr>

</table>
</ItemTemplate>
</asp:Repeater>


<asp:Repeater ID="rptSubSubcatname" runat="server">

<ItemTemplate >
<table cellpadding="0" cellspacing="0" width="100%">
<tr>
<td>
<a href="category.aspx?catid=<%#Eval("Category_Id")%>&pageno=1"> 
<%#Eval("Category_Name") %>
</a>
</td>
</tr>

</table>
</ItemTemplate>
</asp:Repeater>