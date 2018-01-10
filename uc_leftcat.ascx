<%@ Control Language="C#" AutoEventWireup="true" CodeFile="uc_leftcat.ascx.cs" Inherits="uc_leftcat" %>
<asp:Repeater ID="rptcatname" runat="server">
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
