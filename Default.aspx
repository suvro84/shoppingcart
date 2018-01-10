<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" %>

<%@ Register Src="uc_Articles.ascx" TagName="uc_Articles" TagPrefix="uc2" %>
<%@ Register Src="uc_leftcat.ascx" TagName="uc_leftcat" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.1//EN" "http://www.w3.org/TR/xhtml11/DTD/xhtml11.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Untitled Page</title>

    <script type="text/ecmascript" src="js/jsCommonFunction.js"></script>

</head>
<body>
    <form id="form1" runat="server">
        <%--<asp:ScriptManager ID="ScriptManager1" runat="server" />--%>
        <uc1:uc_leftcat ID="Uc_leftcat1" runat="server" />
        <asp:DataList ID="dtlstdisplay" runat="server" RepeatDirection="Horizontal" RepeatColumns="3">
            <ItemTemplate>
                <table width="100%" cellpadding="0" cellspacing="0">
                    <tr>
                        <td>
                            <a href="item.aspx?pid=<%#Eval("pid")%>">
                                <img src="images/<%#Eval("image_name")%>" border="0" height="40px" width="60px" /></a>
                            <%-- <img src="images/<%#Eval("pic_name")%>" />--%>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <a href="item.aspx?pid=<%#Eval("pid") %>&cid=<%#Eval("cid")%>">
                                <%#Eval("item_name") %>
                            </a>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <%-- <span id="spnBuyBtn_'<%#Eval("pid") %>'"><a href="javascript:fnCartAdd(1,'<%#Eval("pid") %>','<%#Eval("cid") %>');">
                                Add to Cart</a></span><span id="spnload_'<%#Eval("pid") %>'" style="display: none"><img
                                    src="images/buycart.gif"></span>--%>
                            <span id="spnBuyBtn_<%#Eval("item_id") %>"><a href="javascript:fnCartAdd(1,'<%#Eval("item_id") %>','<%#Eval("pid") %>','<%#Eval("cid") %>');">
                                Add to Cart</a> </span>
                                <span id="spnload_<%#Eval("item_id") %>" style="display: none">
                                    <img src="images/buycart.gif"></span>
                        </td>
                    </tr>
                </table>
            </ItemTemplate>
        </asp:DataList>
        <asp:Label ID="lblpage" Text="" runat="server"></asp:Label>
        <asp:LinkButton ID="btnprev" Text="Prev" runat="server" OnClick="btnprev_Click" />
        <asp:LinkButton ID="btnnext" Text="Next" runat="server" OnClick="btnnext_Click" />
        <br />
        <br />
        <br />
        <div>
            <uc2:uc_Articles ID="Uc_Articles1" runat="server"></uc2:uc_Articles>
        </div>
    </form>
</body>
</html>
