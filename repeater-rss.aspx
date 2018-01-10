<%@ Page Language="C#" AutoEventWireup="true" CodeFile="repeater-rss.aspx.cs" Inherits="data_repeater_rss" EnableViewState="false" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Repeater RSS</title>
    <style type="text/css">
    body {
    font-family:arial;
    font-size:12px;
    }
   
    img {
    border-top:solid 1px #c0c0c0;
    border-left:solid 1px #c0c0c0;
    border-right:solid 2px #c0c0c0;
    border-bottom:solid 2px #c0c0c0;
    padding:4px;
    float:left;
    }
    
    .clear {
    clear:both;
    }
    
    .container {
    width:100%;
    float:left;
    }

    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div style="width:400px">
        <asp:XmlDataSource ID="XmlDataSource1" runat="server" DataFile="http://gdata.youtube.com/feeds/api/videos?vq=comedy&orderby=relevance&start-index=1&max-results=12&alt=rss&format=5"></asp:XmlDataSource>

        <asp:Repeater ID="Repeater1" runat="server" EnableViewState="false">
        <ItemTemplate>

                <div class="container">
                    <h4>
                        <%#XPath("media:title", xmlN) %>
                    </h4>
                    <p align="left">
                        <img src="<%#XPath("media:thumbnail/@url", xmlN) %>" alt="<%#XPath("media:title", xmlN) %>" hspace="10" />
                        <%#XPath("media:description", xmlN) %>
                        <br /><br />
                        <b>Tag(s): </b>
                        <%#XPath("media:keywords", xmlN)%>
                        <br /><br />
                        <b>Category(s): </b>
                        <%#XPath("media:category", xmlN)%>
                    </p>
                    <div class="clear">
                    <hr noshade="noshade" />
                    </div>
                   
                </div>

        </ItemTemplate>
        </asp:Repeater>

    </div>
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <div style="margin-top: 20px; width: 500px">
            <b>Coding Support Provided By <a href="http://www.top54u.com">www.top54u.com</a><br />
                <a href="http://programming.top54u.com">Programming Ezine</a></b>
        </div>
    </form>
</body>
</html>
