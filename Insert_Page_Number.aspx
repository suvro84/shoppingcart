<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Insert_Page_Number.aspx.cs" Inherits="Insert_Page_Number" %>

<%--<%@ Register Src="ucFooter.ascx" TagName="ucFooter" TagPrefix="uc2" %>
--%>
<%--<%@ Register Src="ucHeader.ascx" TagName="ucHeader" TagPrefix="uc1" %>
--%><%--<%@ Register Src="UcLeftPannel.ascx" TagName="ucLeftPannel" TagPrefix="uc3" %>
--%><!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=iso-8859-1" />
    <title>:: Add Page Id ::</title>
    <link href="css/style.css" rel="stylesheet" type="text/css" />

    <script type="text/javascript" src="js/jquery.js"></script>

    <script type="text/javascript" src="js/ddaccordion.js"></script>

    <script type="text/javascript" src="js/ddaccordion1.js"></script>

    <script type="text/javascript" src="js/boxover.js"></script>

    <link href="css/style.css" rel="stylesheet" type="text/css" />

    <script type="text/javascript">
     function ValidateText(tb)
{
//   alert(document.getElementById(tb).value);
    if (tb.lenght > 0)
    {
       return true;
    }
    else
    {
        alert("Please enter  Page Id");
        document.getElementById(tb).focus();

        return false;
    }
}

function IsNumeric(e)
{
    // Calling procedure
    // onKeyPress=\"javascript:return IsNumeric(event);\" 
    //
    //alert(e);
	var KeyID = (window.event) ? event.keyCode : e.which;
	if((KeyID >= 65 && KeyID <= 90) || (KeyID >= 97 && KeyID <= 122) || (KeyID >= 33 && KeyID <= 47) ||
	   (KeyID >= 58 && KeyID <= 64) || (KeyID >= 91 && KeyID <= 96) || (KeyID >= 123 && KeyID <= 126))
	{
		return false;
	}
    return true;
}
    
     function cleartxtbox()
    {
        document.getElementById("txtSearchString").value="";
    }
    
    </script>

</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        <table width="100%" border="0" cellspacing="0" cellpadding="0">
            <%--<uc1:ucHeader ID="UcHeader1" runat="server" />--%>
            <tr>
                <td align="left" valign="top">
                    <table width="100%" border="0" cellspacing="0" cellpadding="0">
                        <tr>
                            <td width="41" align="left" valign="top" class="shadowBGtop">
                                &nbsp;
                            </td>
                            <td align="left" valign="top">
                                <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                    <tr>
                                        <td width="4" align="left" valign="top" class="leftCornerBg" style="height: 7px">
                                        </td>
                                        <td align="left" valign="top" class="shadowBGtop1" style="height: 7px">
                                        </td>
                                        <td width="4" align="left" valign="top" class="rightCornerBg" style="height: 7px">
                                        </td>
                                    </tr>
                                    <tr>
                                        <td width="4" align="left" valign="top" class="leftBg">
                                            &nbsp;
                                        </td>
                                        <td align="left" valign="top" class="tablePadd">
                                            <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                                <tr>
                                                    <td width="206" align="left" valign="top">
                                                        <!--Left Panel Start -->
                                                        <%--<uc3:ucLeftPannel ID="UcLeftPannel" runat="server" />--%>
                                                        <!--Left Panel End -->
                                                    </td>
                                                    <td align="left" valign="top" class="leftBorder">
                                                        <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                                            <tr>
                                                                <td width="139" colspan="3" align="left" valign="top" class="labelField1 nameWidth">
                                                                    <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                                                                        <ContentTemplate>
                                                                            <asp:Label ID="lblMsg" runat="server" Font-Bold="True" ForeColor="Red" Width="196px"></asp:Label>
                                                                        </ContentTemplate>
                                                                    </asp:UpdatePanel>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td width="139" align="left" valign="top" class="nameWidth">
                                                                    <span class="formInfo" title="header=body=[Please select the name of the website.]">
                                                                        Site Name<img src="images/help.gif" alt="" /></span>
                                                                </td>
                                                                <td width="249" align="left" valign="top" class="nameWidth">
                                                                    <asp:UpdatePanel ID="UpdatePanel5" runat="server">
                                                                        <ContentTemplate>
                                                                            <asp:DropDownList ID="ddlSite" AutoPostBack="true" CssClass="selectField1" runat="server"
                                                                                OnSelectedIndexChanged="ddlSite_SelectedIndexChanged">
                                                                            </asp:DropDownList>
                                                                        </ContentTemplate>
                                                                    </asp:UpdatePanel>
                                                                </td>
                                                                <td align="left" valign="top" class="siteloadImage">
                                                                    <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="UpdatePanel5">
                                                                        <ProgressTemplate>
                                                                            <span id="dvAjaxPic" class="loadImage1">
                                                                                <img src="http://www.reliablelinkexchange.com/images/loading.gif" />
                                                                            </span>
                                                                        </ProgressTemplate>
                                                                    </asp:UpdateProgress>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td colspan="3">
                                                                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                                                        <ContentTemplate>
                                                                            <div id="dvData" visible="false" runat="server">
                                                                                <table cellpadding="0" cellspacing="0" width="100%">
                                                                                    <tr>
                                                                                        <td align="left" valign="top" class="nameWidth4">
                                                                                            <span class="formInfo" title="header=body=[Please select the subpage .]">Select 
                                                                                            subpage<img
                                                                                                src="images/help.gif" alt="" /></span>
                                                                                        </td>
                                                                                        <td width="249" align="left" valign="top">
                                                                                            <asp:DropDownList ID="ddlSubPage" CssClass="selectField1" runat="server">
                                                                                            </asp:DropDownList>
                                                                                        </td>
                                                                                        <td align="left" valign="top" class="siteloadImage">
                                                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" SetFocusOnError="true" ControlToValidate="ddlSubPage"
                                                                                                runat="server" ErrorMessage="Required"></asp:RequiredFieldValidator>
                                                                                        </td>
                                                                                    </tr>
                                                                                    <tr>
                                                                                        <td align="left" valign="top" class="nameWidth4">
                                                                                            <span class="formInfo" title="header=body=[Please enter the Search text string]">
                                                                                            Search text string<img src="images/help.gif" alt="" /></span>
                                                                                        </td>
                                                                                        <td width="249" align="left" valign="top">
                                                                                            <%--   <input type="text" id="txtSearchString" runat="server" name="txtSearchString" onclick="cleartxtbox();">--%>
                                                                                            <input type="text" id="txtSearchString" onclick="cleartxtbox();" class="nameField"
                                                                                                runat="server" />
                                                                                        </td>
                                                                                        <td align="left" valign="top" class="siteloadImage">
                                                                                            <asp:RequiredFieldValidator ID="rfvSearchString" SetFocusOnError="true" ControlToValidate="txtSearchString"
                                                                                                runat="server" ErrorMessage="Required"></asp:RequiredFieldValidator>
                                                                                        </td>
                                                                                    </tr>
                                                                                    <tr>
                                                                                        <td align="left" valign="top" class="nameWidth4">
                                                                                            <asp:UpdatePanel ID="UpdatePanel6" runat="server">
                                                                                                <ContentTemplate>
                                                                                                    <asp:Button ID="btnSearch" runat="server" Text="Search" OnClick="btnSearch_Click" />
                                                                                                </ContentTemplate>
                                                                                            </asp:UpdatePanel>
                                                                                        </td>
                                                                                        <td>
                                                                                            <asp:UpdateProgress ID="UpdateProgress3" runat="server" AssociatedUpdatePanelID="UpdatePanel6">
                                                                                                <ProgressTemplate>
                                                                                                    <span id="dvAjaxPic" class="loadImage1">
                                                                                                        <img src="http://www.reliablelinkexchange.com/images/loading.gif" />
                                                                                                    </span>
                                                                                                </ProgressTemplate>
                                                                                            </asp:UpdateProgress>
                                                                                        </td>
                                                                                    </tr>
                                                                                </table>
                                                                            </div>
                                                                        </ContentTemplate>
                                                                        <Triggers>
                                                                            <asp:AsyncPostBackTrigger ControlID="ddlSite" EventName="SelectedIndexChanged" />
                                                                        </Triggers>
                                                                    </asp:UpdatePanel>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td colspan="3" align="left" valign="top" class="labelField1 nameWidth">
                                                                    <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                                                                        <ContentTemplate>
                                                                            <asp:Label ID="lblSearch" runat="server" Text=""></asp:Label>
                                                                        </ContentTemplate>
                                                                    </asp:UpdatePanel>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td colspan="3" align="left" valign="top">
                                                                    &nbsp;
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td colspan="2" align="left" valign="top">
                                                                    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                                                        <ContentTemplate>
                                                                            <asp:GridView ID="GridView1" AutoGenerateColumns="False" runat="server" Width="100%"
                                                                                OnRowCommand="GridView1_RowCommand">
                                                                                <Columns>
                                                                                    <asp:TemplateField HeaderText="ID" >
                                                                                        <ItemTemplate>
                                                                                            <asp:Label ID="lblid"  Text='<%#Eval("id")%>' runat="server"></asp:Label>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateField>
                                                                                    <asp:TemplateField HeaderText="ClientAd">
                                                                                        <ItemTemplate>
                                                                                            <asp:Label ID="lblHTMLcode" Text='<%#Eval("HTMLcode")%>' runat="server"></asp:Label>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateField>
                                                                                    <asp:TemplateField HeaderText="Contact">
                                                                                        <ItemTemplate>
                                                                                            <%--<asp:Label ID="lblreci" Text='<%#Eval("reci")%>' runat="server"></asp:Label>--%>
                                                                                            <asp:Label ID="Label1" Text='<%#get_data(Convert.ToString(Eval("reci")),Convert.ToString(Eval("email")))%>'
                                                                                                runat="server"></asp:Label>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateField>
                                                                                      <asp:TemplateField HeaderText="pageid">
                                                                                        <ItemTemplate>
                                                                                            
                                                                                            <asp:Label ID="lblpageid"  Text='<%#Eval("pageid")%>' runat="server"></asp:Label>
                                                                                                
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateField>
                                                                                    <asp:TemplateField HeaderText="PageID">
                                                                                        <ItemTemplate>
                                                                                            <asp:TextBox onKeyPress="javascript:return IsNumeric(event);" ID="txtpageid" runat="server"
                                                                                                Width="45px"></asp:TextBox>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateField>
                                                                                    <asp:TemplateField HeaderText="Update">
                                                                                        <ItemTemplate>
                                                                                            <asp:LinkButton ID="btnUpdate" CommandArgument="<%# Container.DataItemIndex %>" CommandName="Upd"
                                                                                                runat="server">Update</asp:LinkButton>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateField>
                                                                                </Columns>
                                                                            </asp:GridView>
                                                                        </ContentTemplate>
                                                                        <Triggers>
                                                                            <asp:AsyncPostBackTrigger ControlID="btnSearch" EventName="Click" />
                                                                        </Triggers>
                                                                    </asp:UpdatePanel>
                                                                </td>
                                                                <td>
                                                                    <asp:UpdateProgress ID="UpdateProgress2" runat="server" AssociatedUpdatePanelID="UpdatePanel2">
                                                                        <ProgressTemplate>
                                                                            <span id="dvAjaxPic" class="loadImage1">
                                                                                <img src="http://www.reliablelinkexchange.com/images/loading.gif" />
                                                                            </span>
                                                                        </ProgressTemplate>
                                                                    </asp:UpdateProgress>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                        <td width="4" align="left" valign="top" class="rightBg">
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td width="41" align="left" valign="top" class="shadowBGtop">
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
          <%--  <uc2:ucFooter ID="UcFooter1" runat="server" />--%>
        </table>
    </form>
</body>
</html>
