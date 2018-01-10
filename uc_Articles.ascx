<%@ Control Language="C#" AutoEventWireup="true" CodeFile="uc_Articles.ascx.cs" Inherits="uc_Articles" %>
<%--<asp:Repeater ID="rptArticles" runat="server" OnItemDataBound="rptArticles_ItemDataBound">
<HeaderTemplate>
    <asp:Label ID="lblHeader_Article" runat="server" Text=""></asp:Label>

</HeaderTemplate>
<ItemTemplate >
<table cellpadding="0" cellspacing="0" width="100%">
<tr>
<td>
<a href="chapter.aspx?Chapter_Id=<%#Eval("Chapter_Id")%>&pageno=1"> 
<%#Eval("Chapter_Name")%>
</a>
</td>
</tr>



</table>
</ItemTemplate>
</asp:Repeater>--%>
<br />

<%--<asp:Repeater ID="rptSubArticles" runat="server" OnItemDataBound="rptSubArticles_ItemDataBound">
<HeaderTemplate>
    <asp:Label ID="lblHeader" runat="server" Text=""></asp:Label>

</HeaderTemplate>
<ItemTemplate >
<table cellpadding="0" cellspacing="0" width="100%">
<tr>
<td>
<a href="chapter.aspx?Chapter_Id=<%#Eval("Chapter_Id")%>&pageno=1"> 
<%#Eval("Chapter_Name")%>
</a>
</td>
</tr>

</table>
</ItemTemplate>
</asp:Repeater>--%>
<asp:Label ID="lblCategory" runat="server" Text=""></asp:Label>