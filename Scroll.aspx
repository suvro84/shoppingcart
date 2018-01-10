<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Scroll.aspx.cs" Inherits="Scroll" %>




<html xmlns="http://www.w3.org/1999/xhtml" >
<head>
    <title>Untitled Page</title>
</head>
<body> 
<div id="disspageie" style="position:absolute;background:#667DB3;width:180; height:220;left:0; top:0;margin:0px;overflow:hidden;padding:0px;border-style:solid; border-width:1px; border-color:#5C5C5C;"> 
  <div id="spageie" style="position:absolute; width:180; height:220; left:0; top:0; border-width:0px; overflow:hidden;clip:rect(8 180 213 0);">
 
 <input type="hidden" id="hdReports" runat="server" />
 
  </div> 
</div> 
<script language="javascript">
var OPB=false;uagent = window.navigator.userAgent.toLowerCase();
OPB=(uagent.indexOf('opera') != -1)?true:false;if((document.all)&&(OPB==false))
{document.write("<div id=\"spage\" style=\"position:absolute; width:178; height:218; left:0; top:0; border-style:solid; border-width:1px; border-color:#5C5C5C;overflow:hidden;\"><div id=\"spagens\" style=\"position:absolute; width:178; height:218; left:0; top:0; border-width:0px; overflow:hidden;clip:rect(8 180 213 0);\"></div></div>");
document.write("<scr"+"ipt language=\"javascript\" sr"+"c=\"jS/scroll.js\"></scr"+"ipt>");}
else{document.write("<div id=\"spagensbrd\" style=\"position:absolute; width:178; height:218; left:0; top:0; border-style:solid; border-width:1px; border-color:#5C5C5C;overflow:hidden;\"><div id=\"spagens\" style=\"position:absolute; width:178; height:218; left:0; top:0; border-width:0px; overflow:hidden;clip:rect(8 180 213 0);\"></div></div>");
document.write("<scr"+"ipt language=\"javascript\" sr"+"c=\"jS/scroll.js\"></scr"+"ipt>");}
</script> 
</body>
</html>


