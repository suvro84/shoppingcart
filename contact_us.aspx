<%@ Page Language="C#" AutoEventWireup="true" CodeFile="contact_us.aspx.cs" Inherits="contact_us" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head>
<meta http-equiv="Content-Type" content="text/html; charset=iso-8859-1" />
<title>:: Welcome to Reliable Technologies ::</title>
<link rel="stylesheet" href="css/style.css" type="text/css" />
<script type="text/javascript" language="javascript">
    function Check(oSrc,args)
    {
        var flag=true;
        var printError="";
        var emailChk=document.getElementById("txtEmail").value;
        
        if(document.getElementById("txtfirstName").value=="")
        {
            printError=printError + "Please provide your first name." + "\n";
            flag=false;
        }
        
        if(document.getElementById("txtlastName").value=="")
        {
            printError=printError+ "Please provide your last name." + "\n";
            flag=false;
        }
        
        if(document.getElementById("txtEmail").value=="")
        {
            printError=printError+ "Please provide your email." + "\n";
            flag=false;
        }
        
        if(test(emailChk))
        {
            flag=true;
        }
        else
        {
            printError=printError+ "Email is not in correct format." + "\n";
            flag=false;
        }
        
        if(document.getElementById("txtQuery").value=="")
        {
            printError=printError+ "Please provide your query regarding us." + "\n";
            flag=false;
        }
        
        if(flag==true)
        {
            args.IsValid=true;
        }
        else
        {
            args.IsValid=false;
            alert(printError);
        }
    }
    function test(src)
    {
        var emailReg = "^[\\w-_\.]*[\\w-_\.]\@[\\w]\.+[\\w]+[\\w]$";
        var regex = new RegExp(emailReg);
        return regex.test(src);
    }
    </script>
</head>
<body>
<!--Header Start -->
<div id="header"> <a href="#"><img src="images/logo.gif" alt="Reliable Technologies" border="0" class="logo" title="Reliable Technologies" /></a>
    <h1>initiate <img src="images/red_dot.gif" alt="" title="" /> innovate <img src="images/red_dot.gif" alt="" title="" /> integrate</h1>
  <ul class="topNav">
    <li><a href="index.html" title="Home">Home</a></li>
    <li><a href="about_us.html" title="About Us">About Us</a></li>
    <li><a href="careers.aspx" title="Careers">Careers</a></li>
    <li><a href="portfolio.html" title="Portfolio">Portfolio</a></li>
    <li class="noBg"><a href="contact_us.aspx" title="Contact Us" class="active">Contact Us</a></li>
  </ul>
   <img src="images/contact_us_banner.jpg" alt="" title="" class="banner" />
</div>
<!--Header End -->
<!--Center Portion Start -->
<div id="wrapper">
				<h2 class="subHead"><span></span>Contact Us</h2>
				<br class="clear" />
			<div class="leftAdd">
				<h3>Reliable Technologies</h3>
				<p>300, B.B.Ganguly Street,<br /> 1st Floor,<br />
				   Kolkata - 700012.</p>
				<p>Phone : +91-033-22259975 / 89,<br />
              		<span class="padd">+91-033-22363213</span><br />
					<span class="fax">Fax :</span> +91-033-22368091<br /><br />
						<a href="http://reliabletechnologies.in/">www.ReliableTechnologies.in</a></p>
			</div>
			<form id="form1" runat="server" name="form1" method="post" action="" class="contactForm">
				<p class="requiredTxt">*All fields are required.</p>
  				<label>First Name :</label>
  				<asp:TextBox ID="txtfirstName" runat="server"></asp:TextBox>
  				<br class="clear" />  
				<label>Last Name :</label>
  			<asp:TextBox ID="txtlastName" runat="server"></asp:TextBox>
  				
  				<br class="clear" />
				<label>Email Address  :</label>
  				<asp:TextBox ID="txtEmail" runat="server"></asp:TextBox><br class="clear" /> 
				<label>Company Name :</label>
  				<input type="text" tabindex="4" name="textfield" /><br class="clear" /> 
				<label class="selectTxt">I have a question about :</label>
				<select name="select" tabindex="5">
    				<option>Web Designing</option>
    				<option>E-Commerce Solutions</option>
    				<option>Content Management</option>
    				<option>Web Application</option>
    				<option>Web Marketing</option>
    				<option>Web Hosting</option>
    				<option>Domain Registration</option>
    				<option>Search Engine Optimization</option>
  				</select><br class="clear" />
				<label>Comment :</label>
			<asp:TextBox ID="txtQuery" runat="server" TextMode="MultiLine" Rows="6" Columns="26"></asp:TextBox>
			<br class="clear" />
					<%--<img src="images/captcha_img.gif" class="captcha" />--%>
				
				<img src="JpegImage.aspx" alt="Captcha" class="captcha" /><br class="clear" />
				<label class="selectTxt">Enter the text that you see above :</label>
  				<%--<input type="text" tabindex="7" name="textfield" class="smallBox" />--%>
  				<asp:TextBox ID="txtCaptcha" runat="server" CssClass="smallBox"></asp:TextBox>
  				
  				<br class="clear" />
			<%--	<input type="image" src="images/submit_btn.gif" class="submitBtn" tabindex="8" />--%>
							<asp:ImageButton ID="Submit" ImageUrl="images/submit_btn.gif" CssClass="submitBtn"  runat="server" OnClick="Submit_Click" ValidationGroup="CheckServerControl"/>

<p><asp:Label ID="lblError" runat="server"></asp:Label></p>
			<asp:CustomValidator ID="CustomValidator1" runat="server" ValidationGroup="CheckServerControl" ClientValidationFunction="Check" ></asp:CustomValidator>


			</form>
				<br class="clear" />
</div>
<!--Center Portion End -->
<!--Footer Start -->
	<div id="footer">
		<p class="copyright">&copy; 2006-2009 All rights reserved. Reliable Technologies. <a href="#">Privacy Policy</a> | <a href="#">Site Map</a>
		</p>
	</div>
<!--Footer End -->

</body>
</html>
