<%@ Page Language="C#" AutoEventWireup="true" CodeFile="chapter.aspx.cs" Inherits="chapter" %>

<%@ Register Src="uc_Articles.ascx" TagName="uc_Articles" TagPrefix="uc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Untitled Page</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <uc1:uc_Articles ID="Uc_Articles1" runat="server" />
    
    
    <asp:DataList ID="dtlArticle" runat="server" RepeatDirection="Vertical" RepeatColumns="3">
            <ItemTemplate>
                <table width="100%" cellpadding="0" cellspacing="0">
                    <tr>
                        <td>
                          
                                <%#Eval("Article_Name") %>
                            </a>
                        </td>
                    </tr>
                    <%--<tr>
                        <td>
                          
                                <%#Eval("Article_Matter") %>
                            </a>
                        </td>
                    </tr>--%>
                    
                </table>
            </ItemTemplate>
        </asp:DataList>
        
        <div id="dvPage" runat="server"></div>
    </div>
    </form>
</body>
</html>
