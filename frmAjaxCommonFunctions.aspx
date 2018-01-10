<%@ Page Language="C#" AutoEventWireup="true" CodeFile="frmAjaxCommonFunctions.aspx.cs" Inherits="frmAjaxCommonFunctions" %>

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
                   
                      <asp:TemplateField HeaderText="s/l No" >
                        <ItemTemplate>
                            <asp:Label ID="lblrecid"  Text='<%#Eval("recid") %>' runat="server"></asp:Label>
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
                            <asp:TextBox ID="txtQty" Width="20px" Text="1" runat="server"></asp:TextBox>
                        </ItemTemplate>
                    </asp:TemplateField>
                      <asp:TemplateField HeaderText="Total Cost">
                        <ItemTemplate>
                            <asp:Label ID="lbltotcost" Text='<%#Eval("item_cost") %>' runat="server"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    
                    <asp:TemplateField HeaderText="">
                        <ItemTemplate>
                            <a href="javascript:deleteFromCart(<%#Eval("recid") %>,<%#Eval("item_id")%>);">Delete</a>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
    </div>
    </form>
</body>
</html>
