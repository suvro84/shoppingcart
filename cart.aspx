<%@ Page Language="C#" AutoEventWireup="true" CodeFile="cart.aspx.cs" Inherits="cart" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Untitled Page</title>

    <script type="text/ecmascript" src="js/jsCommonFunction.js"></script>

    <%--         <script type="text/javascript" src="js/jsCartFunction_ab.js"></script>
--%>

    <script type="text/javascript" src="js/delete.js"></script>

    <script type="text/javascript">
 var obj;
        var down=false;
        function restoreNumbers()
        {
            if(typeof obj == 'undefined') return;
            var newobj = document.getElementsByName("price");
            for(var i=0;i<newobj.length;i++)
            {
                var val = newobj[i].getAttribute('val');
                if(val == obj[i].id)
                {
                    newobj[i].value=obj[i].value;
                }
            }
        }
        function Update(){
            var objs = document.getElementsByName("price");
          //  alert(objs);
            obj = new Array();
            var selectedids = '';
            for(var i=0;i<objs.length;i++)
            {
                selectedids += objs[i].getAttribute("val") + '#' + objs[i].value + ',';
               obj.push({id:objs[i].getAttribute("val"),value :objs[i].value});
            }
            if(selectedids!=null)
            {
                
               alert(selectedids);
                loadPage(selectedids);                
            }
        }
//        else
//        {
//            alert("Objects not found. Try again.");
//        }
//        }
    function loadPage(mode)
    {
        var xmlHttpReq = false;
        var self = this;
        var selectedids = '';
        if(typeof arguments[0] != 'undefined')
        selectedids = arguments[0];
//        alert(selectedids);
        if (window.XMLHttpRequest) 
        {
            self.xmlHttpReq = new XMLHttpRequest();        
        }
        else if (window.ActiveXObject) 
        {
            self.xmlHttpReq = new ActiveXObject("Microsoft.XMLHTTP");         
        }
//        self.xmlHttpReq.open('POST', "frmAjaxPage_commonFunctions.aspx", true);  
  self.xmlHttpReq.open('POST', "ajaxupdate.aspx", true);            
        self.xmlHttpReq.setRequestHeader('Content-Type', 'application/x-www-form-urlencoded');
        self.xmlHttpReq.onreadystatechange = function() 
        {
            if(self.xmlHttpReq.readyState == 1)
            {
//                LoadResponse("Loading...", "dvupdate");
                   document.getElementById("dvAjaxPic").style.display="block";
//                  document.getElementById("objMainDiv").style.display="none";
            }
            if (self.xmlHttpReq.readyState == 4) 
            {    
               // document.getElementById("objMainDiv").style.display="none";
                 document.getElementById("dvAjaxPic").style.display="none";
                
               // LoadResponse(self.xmlHttpReq.responseText,"dvMaindiv");
                LoadResponse(self.xmlHttpReq.responseText,"objMainDiv");
                 document.getElementById("dvMaindiscount").style.display="none"
                restoreNumbers();
            }
        }                
        self.xmlHttpReq.send("mode=3"+"&strBoxIds=" + selectedids);
    }
    function LoadResponse(response,control)
     {
    // alert(control);
     var container=document.getElementById(control);
     
     try{
            while(container.firstChild)
                container.removeChild(container.firstChild);
            var t=document.createElement('div');
            t.innerHTML=response;
            container.appendChild(t);
        }
        catch(ex)
        {
            container.innerHTML=response;
        }
     }   

    </script>

</head>
<body>
    <form id="form1" runat="server">
        <div id="dvMaindiv">
        </div>
        <div id="objErrLabel">
            <%-- <a href="index.aspx"  title="Back to Shopping"><img src="images/continue-shop.gif"= alt="Continue Shopping" title="Continue Shopping" width="193" height="35" border="0"></a>--%>
        </div>
        <div id="dvAjaxPic" style="display: none">
            <img src="images/loadcart.gif" />
        </div>
        <div id="objMainDiv" runat="server">
            <asp:GridView ID="GridView1" AutoGenerateColumns="false" Width="100%" runat="server">
                <Columns>
                    <asp:TemplateField HeaderText="s/l No">
                        <ItemTemplate>
                            <asp:Label ID="lblrecid" Text='<%#Eval("recid") %>' runat="server"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField Visible="false">
                        <ItemTemplate>
                            <asp:Label ID="lblitem_id" Visible="false" Text='<%#Eval("item_id") %>' runat="server"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField>
                        <ItemTemplate>
                            <asp:Image ID="image_name" ImageUrl='<%#Eval("image_name")%>' Width="50px" Height="50px"
                                runat="server" />
                            <asp:Label ID="lblimage_name" Text='<%#Eval("image_name")%>' Visible="false" runat="server"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Product Name">
                        <ItemTemplate>
                            <asp:Label ID="lblitem_name" Text='<%#Eval("item_name") %>' runat="server"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Price">
                        <ItemTemplate>
                            <asp:Label ID="lblitem_cost" Text='<%#Eval("item_cost") %>' runat="server"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Qty">
                        <ItemTemplate>
                            <%--<asp:TextBox ID="txtQty" Width="20px" Text="1" runat="server"></asp:TextBox>--%>
                            <input type="text" id="btnUpd_<%#Eval("item_id") %>" onblur="javascript:chkNumeric(this);"
                                onkeypress="javascript:return IsNumeric(event)" name="price" value="<%#Eval("qty") %>"
                                val="<%#Eval("item_id") %>" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Total Cost">
                        <ItemTemplate>
                            <%-- <asp:Label ID="lbltotcost" Text='<%#Eval("tot_cost") %>' runat="server"></asp:Label>--%>
                            <asp:Label ID="lbltotcost" Text='<%#gettotal(Convert.ToDouble(Eval("item_cost")),Convert.ToInt32(Eval("qty"))) %>'
                                runat="server"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="">
                        <ItemTemplate>
                            <%--  <a href="javascript:deleteFromCart(<%#Eval("recid") %>,<%#Eval("item_id")%>);">Delete</a>--%>
                            <%-- <a href="javascript:deletecart(<%#Eval("recid") %>,<%#Eval("item_id")%>);">Delete</a>--%>
                            <a href="#" onclick="javascript:deletecart(<%#Eval("recid") %>,<%#Eval("item_id")%>);">
                                Delete</a>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
        </div>
        <div id="dvMaindiscount" runat="server">
            <table cellpadding="0" cellspacing="0" width="100%">
                <tr>
                    <td style="height: 17px">
                        Sub Total:
                    </td>
                    <td style="height: 17px">
                        <asp:Label ID="lblSubTot" runat="server" Text=""></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td>
                        Shipping:
                    </td>
                    <td>
                        Free
                    </td>
                </tr>
                <tr>
                    <td style="height: 24px">
                        Discount Code:
                    </td>
                    <td style="height: 24px">
                        <div id="dvdiscount" runat="server">
                            <input id="txtDiscount" type="text" runat="server" />
                            <a href="javascript:updateCartForDiscount(4,1)">Update</a>
                        </div>
                    </td>
                </tr>
                <tr>
                    <td>
                        Savings:
                    </td>
                    <td>
                        <asp:Label ID="lblSavings" runat="server" Text=""></asp:Label></td>
                </tr>
                <tr>
                    <td>
                        Grand Total:
                    </td>
                    <td>
                        <asp:Label ID="lblGrndTot" runat="server" Text=""></asp:Label></td>
                </tr>
            </table>
        </div>
        <div id="dvbuttons">
            <a href="Default.aspx">Continue Shopping</a>&nbsp; <a href="javascript:Update(3)">Update</a>
            <a href="frmOption.aspx">CheckOut</a>
        </div>
    </form>
</body>
</html>
