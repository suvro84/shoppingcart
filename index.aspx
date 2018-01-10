<%@ Page Language="C#" AutoEventWireup="true" CodeFile="index.aspx.cs" Inherits="index" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Untitled Page</title>
    <link href="css/style.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
    </div>
    
    
    <asp:Repeater ID="rptitems" runat="server">
    <ItemTemplate>
    <div id="rightpannel">
                <span id="uc_RrightPopularGifts_lblstrOutput">
                    <h4 class="populargifts">
                        Popular Gifts For All Occasion</h4>
                    <br class="clear">
                    <ul class="popularlist">
                        <li><a href="http://send-giftstoindia.com/item.aspx?ProId=GTI0197&amp;CatId=66">
                            <img alt="Black Forest Cake" src="images/GTI0197.jpg"></a><span><%#Eval("item_name") %>/span>
                            <%#Eval("item_price)") %>
                            <span id="spnBuyBtn_GTI0197_1"><a class="cart" href="javascript:fnCartAdd(1,'<%#Eval("cid") %>','<%#Eval("pid") %>','<%#Eval("cid") %>');">Add
                                to Cart</a></span><span class="cartLoad" id="spnload_GTI0197_1" style="display: none"><img
                                    src="images/buycart.gif"></span></li></ul>
                   
                </span>

               
            </div>
    
    </ItemTemplate>
    
    </asp:Repeater>
    </form>
</body>
</html>
