<%@ Page Language="C#" AutoEventWireup="true" CodeFile="calender.aspx.cs" Inherits="calender" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Untitled Page</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
   <%-- <asp:Calendar id="Calendar1" OnDayRender="CalendarDRender" runat="server" BorderWidth="1px" NextPrevFormat="FullMonth" BackColor="White" Width="350px"ForeColor="Black" Height="190px" Font-Size="9pt" Font-Names="Verdana" BorderColor="White">
<TodayDayStyle BackColor="#CCCCCC"></TodayDayStyle>
<NextPrevStyle Font-Size="8pt" Font-Bold="True" ForeColor="#333333" VerticalAlign="Bottom"></NextPrevStyle>
<DayHeaderStyle Font-Size="8pt" Font-Bold="True"></DayHeaderStyle>
<SelectedDayStyle ForeColor="White" BackColor="#333399"></SelectedDayStyle>
<TitleStyle Font-Size="12pt" Font-Bold="True" BorderWidth="4px" ForeColor="#333399" BorderColor="Black" BackColor="White"></TitleStyle>
<OtherMonthDayStyle ForeColor="#999999"></OtherMonthDayStyle>
</asp:Calendar>
<asp:DataGrid id="DataGrid1" style="Z-INDEX: 102; LEFT: 23px; POSITION: absolute; TOP: 271px" runat="server" Font-Size="XX-Small" Font-Names="Verdana" Visible="False"></asp:DataGrid>
    --%>
    
    
    
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
    </form>
</body>
</html>
