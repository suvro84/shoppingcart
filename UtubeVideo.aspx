<%@ Page Language="C#" AutoEventWireup="true" CodeFile="UtubeVideo.aspx.cs" Inherits="UtubeVideo" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Untitled Page</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <form action="post_url?nexturl=http://example.com" method ="post" enctype="multipart/form-data">

<input type="file" name="file"/>
<input type="hidden" name="token" value="token_value"/>
<input type="submit" value="go" id="Submit1" />
</form>
    </div>
    </form>
</body>
</html>
