<%@ Control Language="C#" AutoEventWireup="true" CodeFile="uc_calender.ascx.cs" Inherits="uc_calender" %>
<script type="text/javascript">
function fun(x)
{
alert(x);
}

function ajaxCalender(x)
{   
   // alert(document.getElementById("Uc_calender1_hdVisibleDate").value);
    var VisibleDate=document.getElementById("Uc_calender1_hdVisibleDate").value;
    var CurrentDate=document.getElementById("Uc_calender1_hdCurrentDate").value;
  //  alert(VisibleDate);
   // alert(CurrentDate);

   // var objErrLabel=document.getElementById("lblError");
    var objMainDiv=document.getElementById("Uc_calender1_dvCalender");
    //alert("error label: " + objErrLabel + " : main div: " + objMainDiv);    
   // alert(objMainDiv);
    if( objMainDiv!=null)
    {    
        var xmlHttpReq = false;
        var self = this;
        if (window.XMLHttpRequest) 
        {
            self.xmlHttpReq = new XMLHttpRequest();        
        }
        else if (window.ActiveXObject) 
        {
            self.xmlHttpReq = new ActiveXObject("Microsoft.XMLHTTP");         
        }
        self.xmlHttpReq.open('POST', "ajaxCalender.aspx", true);             
        self.xmlHttpReq.setRequestHeader('Content-Type', 'application/x-www-form-urlencoded');
        self.xmlHttpReq.onreadystatechange = function() 
        {
            if (self.xmlHttpReq.readyState == 4) 
            {          
               // document.getElementById("dvAjaxPic").style.display="none";
                objMainDiv.style.display="block";
               // alert(self.xmlHttpReq.responseText);
                var GetValue=new Array();
                if(self.xmlHttpReq.responseText.indexOf('~' != -1))
                {
                    GetValue=self.xmlHttpReq.responseText.split("~");
                }
               // alert(GetValue[1].toString());
                if(GetValue[0].toString()!="")
                {
                    if(GetValue[0].toString()=="0")
                    {
                       // objErrLabel.innerHTML=GetValue[1].toString();                         
                       // objMainDiv.innerHTML="<ul><li><a href=\"index.aspx\"><img class=\"shoppingBtn\" src=\"images/continue_shopping_btn.gif\" /></a></li></ul>";  
                    }
                    else
                    {
                       // objMainDiv.innerHTML=GetValue[1].toString();;
                        //objErrLabel.innerHTML=""; 
                      LoadResponse(GetValue[1],"Uc_calender1_dvCalender");
                        
                    }
                }
                else
                {
                    alert("Ajax return not found...");
                }
            }
            else
            {
              //  document.getElementById("dvAjaxPic").style.display="block";
                objMainDiv.style.display="none";
            }
        }
          //  self.xmlHttpReq.send("mode="+x+"&VisibleDate="+VisibleDate+"&CurrentDate="+CurrentDate); 
              self.xmlHttpReq.send("mode="+x); 

    }
    else
    {
        alert("Objects not found. Try again.");
    }

}






  function LoadResponse(response,control)
     {
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
<div id="dvCalender" runat="server">
<asp:Calendar id="calSource" runat="server" SelectionMode="DayWeekMonth" BorderWidth="1px" BackColor="#FFFFCC" Width="220px" DayNameFormat="FirstLetter" ForeColor="#663399" Height="200px" Font-Size="8pt" Font-Names="Verdana" BorderColor="#FFCC66" ShowGridLines="True" OnDayRender="calSource_DayRender">
       <%--   <TodayDayStyle ForeColor="White" BackColor="#FFCC66"></TodayDayStyle>--%>
         <%-- <SelectorStyle BackColor="#FFCC66"></SelectorStyle>--%>
          <NextPrevStyle Font-Size="9pt" ForeColor="#FFFFCC"></NextPrevStyle>
          <DayHeaderStyle Height="1px" BackColor="#FFCC66"></DayHeaderStyle>
          <SelectedDayStyle Font-Bold="True" BackColor="#CCCCFF"></SelectedDayStyle>
          <TitleStyle Font-Size="9pt" Font-Bold="True" ForeColor="#FFFFCC" BackColor="#990000"></TitleStyle>
          <OtherMonthDayStyle ForeColor="#CC9966"></OtherMonthDayStyle>
        </asp:Calendar>
        
        </div>
 
 
<asp:LinkButton ID="lnkPrev" runat="server" OnClick="lnkPrev_Click">Prev</asp:LinkButton>
<asp:LinkButton ID="lnkNext" runat="server" OnClick="lnkNext_Click">Next</asp:LinkButton>
<asp:Label runat="server" ID="calTextLbl" />


<a href="javascript:ajaxCalender('1');">Previous</a>
<a href="javascript:ajaxCalender('2');">Next</a>

<input type="hidden" id="hdNext" runat="server" />
<input type="hidden" id="hdPrev" runat="server" />