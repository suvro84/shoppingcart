<%@ Page Language="C#" AutoEventWireup="true" CodeFile="updatepannel_js.aspx.cs"
    Inherits="updatepannel_js" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" dir="ltr">
<head>
    <title>Suvro Sample</title>
    <script type="text/javascript">
        function displaylimit(obj_textarea, countdown_id, word_limit) {
            var obj_CountDown = document.getElementById(countdown_id);
            if(obj_textarea != null && obj_CountDown != null){
               if (obj_textarea.value.length > word_limit) {
		            obj_textarea.value = obj_textarea.value.substring(0, word_limit);
	            } 
	            else {
		            obj_CountDown.innerHTML = word_limit - obj_textarea.value.length;
	            }
            }
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
        <ContentTemplate>
            <asp:DropDownList AutoPostBack="true" ID="DropDownList1" runat="server" OnSelectedIndexChanged="DropDownList1_SelectedIndexChanged">
                <asp:ListItem Value="1">test1</asp:ListItem>
                <asp:ListItem Value="2">test2</asp:ListItem>
            </asp:DropDownList>
            
        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="DropDownList1" EventName="SelectedIndexChanged" />
        </Triggers>
    </asp:UpdatePanel>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <textarea runat="server" style="width: 348px;" cols="0" rows="0" id="txtPageDescription"
                name="txtDescription" onkeyup="displaylimit(this, 'countdown', 800)"></textarea><br/>
            (Maximum characters: 800)<br/>
            You have <span id="countdown">10</span> characters left.
            <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
           
             <asp:TextBox ID="TextBox2" runat="server"></asp:TextBox>
            
        </ContentTemplate>
    </asp:UpdatePanel>
    </form>
</body>
</html>