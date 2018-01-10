<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Video_Utube.aspx.cs" Inherits="Video_Utube" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Untitled Page</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
     <asp:Panel ID="Panel1" runat="server">
    <object classid="CLSID:22D6F312-B0F6-11D0-94AB-0080C74C7E95" id="player" width="320" height="260">
        <param name="url" value="<%=myUrl %>" />
        <param name="src" value="<%=myUrl %>" />
        <param name="showcontrols" value="true" />
        <param name="ShowDisplay" value="false" />
        <param name="autostart" value="true" />
        <!--[if !IE]>-->
        <embed id="videocontent" width="550" 
            height="480" type="video/avi" autstart="true" l
            oop="false" runat="server" style="border: gray 1px solid"
            src='<%=myUrl %>'/>
        <!--<![endif]-->
    </object>
</asp:Panel>
    
    
    </div>
    </form>
</body>
</html>
