<%@ Page Language="C#" AutoEventWireup="true" CodeFile="UploadVideo.aspx.cs" Inherits="UploadVideo" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Untitled Page</title>

    <script language="javascript" type="text/javascript">
function showWait()
{
    if ($get('FileVideo').value.length > 0)
    {
        $get('UpdateProgress1').style.display = 'block';
    }
}
    </script>

</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:ScriptManager ID="ScriptManager1" runat="server">
            </asp:ScriptManager>
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <Triggers>
                    <asp:PostBackTrigger ControlID="btnUpload" />
                </Triggers>
                <ContentTemplate>
                    <asp:FileUpload ID="FileVideo" runat="server" />
                    <asp:Label ID="lblUploadResult" runat="server" Text=""></asp:Label>
                    <%--  <asp:Button ID="btnUpload" OnClientClick="javascript:showWait();" runat="server" Text="Upload" OnClick="btnUpload_Click" />--%>
                    <asp:Button ID="btnUpload" runat="server" Text="Upload" OnClick="UploadFile" OnClientClick="javascript:showWait();" />
                    <asp:UpdateProgress ID="UpdateProgress1" AssociatedUpdatePanelID="UpdatePanel1" runat="server">
                        <ProgressTemplate>
                            <img src="images/loadcart.gif" />
                        </ProgressTemplate>
                    </asp:UpdateProgress>
                </ContentTemplate>
            </asp:UpdatePanel>
            <%--  <input type="file" runat="server" id="FileVideo" />--%>
        </div>
    </form>
</body>
</html>
