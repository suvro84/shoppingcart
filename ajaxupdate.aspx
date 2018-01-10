<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ajaxupdate.aspx.cs" Inherits="ajaxupdate" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Untitled Page</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
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
                           
                          
                     <%--       <input type="text" id="btnUpd_<%#Eval("item_id") %>" onblur="javascript:chkNumeric(this);" onkeypress="javascript:return IsNumeric(event)" name="price" value="<%#Eval("qty") %>" />--%>
                     
                     
                       <input type="text" id="btnUpd_<%#Eval("item_id") %>" onblur="javascript:chkNumeric(this);"
                                onkeypress="javascript:return IsNumeric(event)" name="price" value="<%#Eval("qty") %>"
                                val="<%#Eval("item_id") %>" />
                       
                       
                       
                       
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Total Cost">
                        <ItemTemplate>
                           <%-- <asp:Label ID="lbltotcost" Text='<%#Eval("tot_cost") %>' runat="server"></asp:Label>--%>
                           <asp:Label ID="lbltotcost" Text='<%#gettotal(Convert.ToDouble(Eval("item_cost")),Convert.ToInt32(Eval("qty"))) %>' runat="server"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="">
                        <ItemTemplate>
                         
                          
                            <a href="#" onclick="javascript:deletecart(<%#Eval("recid") %>,<%#Eval("item_id")%>);">
                                Delete</a>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
            
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
                        <td>
                            Discount Code:
                        </td>
                        <td>
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
    </form>
</body>
</html>
