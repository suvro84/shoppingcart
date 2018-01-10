<%@ Page Language="C#" AutoEventWireup="true" CodeFile="image.aspx.cs" Inherits="image" %>

<%@ Register Src="uc_calender.ascx" TagName="uc_calender" TagPrefix="uc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Untitled Page</title>

    <script type="text/javascript">
    function fun()
    {
    var img1=document.getElementById("img1").src;
    var img5=document.getElementById("img5").src;
   // alert(img5);
       var GetValue=new Array();
           var GetValue1=new Array();
       GetValue=img5.split("/");
         GetValue1=img1.split("/");
      //   alert(img5);
       alert(GetValue[GetValue.length-1]);
      //   alert(GetValue1[GetValue1.length-1]);
 //   alert(img5.lastIndexOf("img5","/"));
   if(GetValue[GetValue.length-1]=="")
   {
   document.getElementById("img5").style.display="none";
   }
    
    }
    </script>

</head>
<body>
    <form id="form1" runat="server">
        <div>
            <a href="javascript:fun();">additionaal images</a>
            <uc1:uc_calender ID="Uc_calender1" runat="server"   />
            <div id="dvupload">
                <img id="img1" src="images/10_GTI2196.jpg" height="50px" width="50px" />
            </div>
            <div id="Div1">
                <img id="img2" src="images/10_GTI2196.jpg" height="50px" width="50px" />&nbsp;
                <asp:Label ID="lblMsg" runat="server"></asp:Label></div>
            <div id="Div2">
                <img id="img3" src="images/10_GTI2196.jpg" height="50px" width="50px" />
                <asp:TextBox ID="txtpackage_cost" runat="server"></asp:TextBox>&nbsp;
                <asp:TextBox ID="txtpackage_name" runat="server"></asp:TextBox>
            </div>
            <div id="Div3">
                <img id="img4" src="images/10_GTI2196.jpg" height="50px" width="50px" />&nbsp;<asp:Button
                    ID="Button1" runat="server" Text="Button" OnClick="Button1_Click" /></div>
            <div id="Div4">
                <img id="img5" height="50px" width="50px" />
            </div>
        </div>
        
         <select id="ddl1">
        <option >1</option>
           <option>2</option>
            <option>3</option>
    </select>
    <select id="ddl2">
        <option >1</option>
           <option>2</option>
            <option>3</option>
    </select>
    <select id="ddl3">
       <option >1</option>
           <option>2</option>
            <option>3</option>
    </select>
 <asp:button ID="btnAdd" runat="server" text="ADD" OnClick="btnAdd_Click" />
   
    <input type="hidden" id="hdnids" runat="server" />
        
    </form>
  
   
</body>
</html>
