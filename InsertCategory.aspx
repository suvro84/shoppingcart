<%@ Page Language="C#" AutoEventWireup="true" CodeFile="InsertCategory.aspx.cs" Inherits="InsertCategory" %>

<%@ Register Src="ucMenu.ascx" TagName="ucMenu" TagPrefix="uc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Untitled Page</title>
    
     <link href="css/admin_style.css" rel="stylesheet" type="text/css" />
<script type="text/javascript" src="js/c_smartmenus.js"></script>
<script type="text/javascript" src="js/c_config.js"></script>
   
<script type="text/javascript">
function fun()
{
  var objs = document.getElementsByName("txt");
          //  alert(objs);
            obj = new Array();
            var selectedids = '';
            for(var i=0;i<objs.length;i++)
            {
                selectedids += objs[i].getAttribute("id") + '#' + objs[i].value + ',';
              // obj.push({id:objs[i].getAttribute("val"),value :objs[i].value});
            }
            alert(selectedids);

}

</script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
<%--        <uc1:ucMenu ID="UcMenu1" runat="server" />
--%>        
       

 
<div id="dvMenu" runat="server" ></div>

        
        
        
        <input type="text" id="txtbox1" value="1" name="txt" val="txt1" />
                <input type="text" id="txtbox2" value="2"  name="txt" val="txt1" />

        <input type="text" id="txtbox3" value="3"  name="txt" val="txt1" />

    <a onclick="fun();">Click</a>
    </div>
    </form>
</body>
</html>
