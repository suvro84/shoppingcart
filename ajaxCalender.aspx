<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ajaxCalender.aspx.cs" Inherits="ajaxCalender" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Untitled Page</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
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
