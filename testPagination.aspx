<%@ Page Language="C#" AutoEventWireup="true" CodeFile="testPagination.aspx.cs" Inherits="testPagination" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Untitled Page</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <asp:DataList ID="dtlArticle" runat="server" RepeatDirection="Vertical" RepeatColumns="3">
            <ItemTemplate>
                <table width="100%" cellpadding="0" cellspacing="0">
                    <tr>
                        <td>
                          
                                <%#Eval("pname") %>
                            </a>
                        </td>
                    </tr>
                    
                    
                </table>
            </ItemTemplate>
        </asp:DataList>
        
        <div id="dvPage" runat="server"></div>
    </div>
    </form>
</body>
</html>
