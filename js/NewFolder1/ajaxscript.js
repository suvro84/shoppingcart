// JScript File

 
//var http;
//http=createRequestObject();   
function createRequestObject()
{
    var object;
    var browser=navigator.appName;
    if(browser=="Microsoft Internet Explorer")
    {
        object=new ActiveXObject("Microsoft.XMLHTTP");
    }
    else
    {
        object=new XMLHttpRequest()
    }
    return object;    
}




function replaceAll(oldStr,findStr,repStr)
 {
  var srchNdx = 0;  // srchNdx will keep track of where in the whole line
                    // of oldStr are we searching.
  var newStr = "";  // newStr will hold the altered version of oldStr.
  
//  alert(oldStr);
  while (oldStr.indexOf(findStr,srchNdx) != -1)  
                    // As long as there are strings to replace, this loop
                    // will run. 
  {
    newStr += oldStr.substring(srchNdx,oldStr.indexOf(findStr,srchNdx));
                    // Put it all the unaltered text from one findStr to
                    // the next findStr into newStr.
    newStr += repStr;
                    // Instead of putting the old string, put in the
                    // new string instead. 
    srchNdx = (oldStr.indexOf(findStr,srchNdx) + findStr.length);
                    // Now jump to the next chunk of text till the next findStr.           
  }
  newStr += oldStr.substring(srchNdx,oldStr.length);
                    // Put whatever's left into newStr.             
  return newStr;
}


function checkSMTP()
{   
    var objMainDiv=document.getElementById("dvCheckSMTP");
   
  var SMTPServer= document.getElementById("txtSMTPServer").value;
  var SMTP_Port=  document.getElementById("txtSMTP_Port").value;
  var SMTP_UserName=document.getElementById("txtSMTP_UserName").value;
  var SMTP_Password=  document.getElementById("txtSMTP_Password").value;
   var email=  document.getElementById("txtEmail").value;

    if(objMainDiv!=null)
    {    
        if(SMTPServer!=null && SMTP_Port!=null && SMTP_UserName!=null && SMTP_Port!=null && SMTP_Password!=null && email!=null)
        {
           
//            alert(SMTPServer);
//            alert(SMTP_Port);
//            alert(SMTP_UserName);
//            alert(SMTP_Password);
            
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
            self.xmlHttpReq.open('POST', "New_AjaxPage.aspx", true);             
            self.xmlHttpReq.setRequestHeader('Content-Type', 'application/x-www-form-urlencoded');
            self.xmlHttpReq.onreadystatechange = function() 
            {
                if (self.xmlHttpReq.readyState == 4) 
                {          
                    document.getElementById("dvAjaxPic").style.display="none";
                    var GetValue=new Array();
//                    alert(self.xmlHttpReq.responseText);
                 
                   // if(self.xmlHttpReq.responseText.indexOf('~' != -1))
                      if(self.xmlHttpReq.responseText.indexOf('~')>-1)
                    {
//                        alert('hi');
                        GetValue=self.xmlHttpReq.responseText.split("~");
                        //alert(GetValue);
//                          alert(GetValue[0].toString());
//                          alert(GetValue[1].toString());
                    }
                   
                    if(GetValue[0].toString()!="")
                    {
                        if(GetValue[0].toString()=="0")
                        {
                            
                           document.getElementById("dvCheckSMTP").value=GetValue[1].toString();
                           document.getElementById("dvCheckSMTP").className="finalBtn";
                           alert(document.getElementById("btnSubmit").style.visibility);
                           if(document.getElementById("btnSubmit").style.visibility == "hidden")
                           {
                             
                              document.getElementById("btnSubmit").style.visibility = "visible";
                           }
//                           document.getElementById("btnSubmit").visible=true;

                          

                        }
                        else
                        {
    
                           document.getElementById("dvCheckSMTP").value=GetValue[1].toString();
                            document.getElementById("dvCheckSMTP").className="finalBtn";
                           
                        }
                    }
                    else
                    {
                       

                    }
                }
                else
                {
                     // alert('hi');
                      document.getElementById("dvAjaxPic").style.display="block";
                      document.getElementById("dvAjaxPic").innerHTML="<img  src=\"images/loading.gif\" alt=\"Wait...\" />"
                      //document.getElementById("dvAjaxPic").style.display="block";
                
                }
            }                
              self.xmlHttpReq.send("mode=SMTPCheck&SMTPServer=" +SMTPServer+"&SMTP_Port="+SMTP_Port+"&SMTP_UserName="+SMTP_UserName+"&SMTP_Password="+SMTP_Password+"&email="+email+"&r="+Math.random()); 
        }
        else
        {
            alert("Invalid parameter. Try again.");
        }
    }
    else
    {
        alert("Objects not found. Try again.");
    }
}





function checkSMTP1()
{
   
  var SMTPServer= document.getElementById("txtSMTPServer").value;
  var SMTP_Port=  document.getElementById("txtSMTP_Port").value;
  var SMTP_UserName=document.getElementById("txtSMTP_UserName").value;
  var SMTP_Password=  document.getElementById("password").value;
  var email=  document.getElementById("txtEmail").value;
  var SiteName=document.getElementById("txtName").value;
   
    if(document.getElementById("txtName").value=="")
        {
           alert("Please provide your site name");
           document.getElementById("txtName").focus();
           return false;
        }
        
        if(document.getElementById("txtDescription").value=="")
        {
             alert("Please provide your description.");
             document.getElementById("txtDescription").focus();
           return false;
        }
        
//         if(document.getElementById("txtSiteURL").value.indexOf('http://' != -1))
//        {
//            alert("Please provide your SiteURL.");
//            document.getElementById("txtSiteURL").focus();
//            return false;
//        }
        
         var RegExp = /^(([\w]+:)?\/\/)?(([\d\w]|%[a-fA-f\d]{2,2})+(:([\d\w]|%[a-fA-f\d]{2,2})+)?@)?([\d\w][-\d\w]{0,253}[\d\w]\.)+[\w]{2,4}(:[\d]+)?(\/([-+_~.\d\w]|%[a-fA-f\d]{2,2})*)*(\?(&?([-+_~.\d\w]|%[a-fA-f\d]{2,2})=?)*)?(#([-+_~.\d\w]|%[a-fA-f\d]{2,2})*)?$/; 
         if(!RegExp.test(document.getElementById("txtSiteURL").value))
         {
             alert("Please provide valid SiteURL.");
             document.getElementById("txtSiteURL").focus();
             return false;
         }
        if(document.getElementById("txtSiteURL").value=="")
        {
            alert("Please provide your SiteURL.");
            document.getElementById("txtSiteURL").focus();
            return false;
        }
   
    
//    
//      if(!isValidURL(document.getElementById("txtSiteURL").vlaue))
//     {
//           alert("Please enter valid SiteURL.");
//            document.getElementById("txtSiteURL").focus();
//            return false;
//     }
     
    if(SMTPServer.length==0)
   {
      alert('Please enter valid SMTPServer');
      document.getElementById("txtSMTPServer").focus();
      return false;
   }
      if(SMTP_Port.length==0)
   {
      alert('Please enter valid SMTP Port');
      document.getElementById("txtSMTP_Port").focus();

      return false;
   }
   
      if(SMTP_UserName.length==0)
   {
      alert('Please enter valid SMTP UserName');
     document.getElementById("txtSMTP_UserName").focus();

      return false;
   }
    if(!validateEmail(email))
   {
      alert('Please enter valid email address');
      document.getElementById("txtEmail").focus();

      return false;
   }
    if(SMTP_Password.length==0)
   {
      alert('Please enter valid SMTP Password');
      document.getElementById("password").focus();

      return false;
   }
   
    var iframe = document.getElementById("txtSignature_ifr");
       
             var doc = null;
            if (iframe.contentDocument)
            {
               doc = iframe.contentDocument;
            }
            else if (iframe.contentWindow)
            {
                doc=iframe.contentWindow.document;
            }
            else 
            {
                 doc=window.frames[iframe].document;
            }
           if(doc)
           {
                 var regEx = /<[^>]*>/g;
	             var  Signature=doc.getElementById("tinymce").innerHTML.toString().replace(regEx, "");
	             if(Signature=="")
	             { 
	               alert('Please enter valid Signature');
                   doc.getElementById("tinymce").focus();
                   return false;
	             }
           }


   var http1=createRequestObject();

     http1.open("GET","New_AjaxPage.aspx?mode=SMTPCheck&SMTPServer=" +SMTPServer+"&SMTP_Port="+SMTP_Port+"&SMTP_UserName="+SMTP_UserName+"&SMTP_Password="+SMTP_Password+"&email="+email+"&SiteName="+SiteName+"&r="+Math.random());                                                  
                                           
      http1.onreadystatechange=function()
      {
         //alert(http.readyState);
        if (http1.readyState == 4) 
        {        
//                  document.getElementById("dvAjaxPic").style.display="none";
                  var response = http1.responseText; 
                  var update =new Array();
                      
                  if(response.indexOf('~' != -1))
                  {            
//                    alert('hi');
                      update=response.split('~');
                   
                  }
                    if(update[0].toString()!="")
                    {
                         if(update[0].toString()=="1")
                         {
                           
                           // alert(document.getElementById("btnSubmit").style.visibility);
                            document.getElementById("dvCheckSMTP").innerHTML=update[1];
                            document.getElementById("dvCheckSMTP").className="authenticTxt";
                          //  alert(document.getElementById("<%=btnSubmit.ClientID%>").style.visibility);
                           // document.getElementById("<%=btnSubmit.ClientID%>").style.visibility = "visible";
                         //  alert(document.getElementById("btnSubmit").style.visibility);
                            document.getElementById("divSubmit").style.display = "block";
                           
                         }
                        else
                        {
                            document.getElementById("dvCheckSMTP").innerHTML=update[1]+"<input type=\"button\" id=\"btnCheckSMTP\" onclick=\"javascript:checkSMTP1();\"  name=\"Submit\" value=\"Check SMTP\" class=\"smtpBtn\" />"
;

                        }
                     
                     
                    }
                  
        }
         else
         {
                 // document.getElementById("dvAjaxPic").style.display="block";
                 // document.getElementById("dvAjaxPic").innerHTML="<img  src=\"images/_loading.gif\" alt=\"Wait...\" />Wait...";
                 document.getElementById("dvCheckSMTP").innerHTML="";
                 document.getElementById("dvCheckSMTP").innerHTML="<img  src=\"http://www.reliablelinkexchange.com/images/loading.gif\" width=\"24px\" height=\"24px\" alt=\"Checking...\" class=\"checkImg\" />Checking...";

          }
                                            
    }
           http1.send(null);  
}



function validateEmail(str)
{
    if(str)
    {
        var at="@"
        var dot="."
        var lat=str.indexOf(at)
        var lstr=str.length
        var ldot=str.indexOf(dot)
        if (str.indexOf(at)==-1)
        {
           //alert("Invalid E-mail ID")
           return false;
        }

        if (str.indexOf(at)==-1 || str.indexOf(at)==0 || str.indexOf(at)==lstr)
        {
           //alert("Invalid E-mail ID")
           return false;
        }

        if (str.indexOf(dot)==-1 || str.indexOf(dot)==0 || str.indexOf(dot)==lstr)
        {
            //alert("Invalid E-mail ID")
            return false;
        }

         if (str.indexOf(at,(lat+1))!=-1)
         {
            //alert("Invalid E-mail ID")
            return false;
         }

         if (str.substring(lat-1,lat)==dot || str.substring(lat+1,lat+2)==dot)
         {
            //alert("Invalid E-mail ID")
            return false;
         }

         if (str.indexOf(dot,(lat+2))==-1)
         {
            //alert("Invalid E-mail ID")
            return false;
         }

         if (str.indexOf(" ")!=-1)
         {
            //alert("Invalid E-mail ID")
            return false;
         }
         return true		
	 }
	 else
	 {
	    return false;
	 }			
}
function IsNumeric(e)
{
    // Calling procedure
    // onKeyPress=\"javascript:return IsNumeric(event);\" 
    //
    //alert(e);
	var KeyID = (window.event) ? event.keyCode : e.which;
	if((KeyID >= 66 && KeyID <= 90) || (KeyID >= 97 && KeyID <= 122) || (KeyID >= 33 && KeyID <= 47) ||
	   (KeyID >= 58 && KeyID <= 64) || (KeyID >= 91 && KeyID <= 96) || (KeyID >= 123 && KeyID <= 126))
	{
		return false;
	}
    return true;
}

 function isValidURL(url)
 {
   var RegExp = /^(([\w]+:)?\/\/)?(([\d\w]|%[a-fA-f\d]{2,2})+(:([\d\w]|%[a-fA-f\d]{2,2})+)?@)?([\d\w][-\d\w]{0,253}[\d\w]\.)+[\w]{2,4}(:[\d]+)?(\/([-+_~.\d\w]|%[a-fA-f\d]{2,2})*)*(\?(&?([-+_~.\d\w]|%[a-fA-f\d]{2,2})=?)*)?(#([-+_~.\d\w]|%[a-fA-f\d]{2,2})*)?$/; 
       if(RegExp.test(url)){
        return true;
    }else{
        return false;
    }
} 

function getdata(obj)
{
//    document.getElementById("lblMsg").innerHTML=" ";
  document.getElementById("lblMsg").innerHTML="";  

     
     var SiteId="";
     var http5;
    http5=createRequestObject();
//    alert(obj.type);
    if( document.getElementById("hdsite").value=="0" || document.getElementById("hdsite").value=="1")
 {
         
         if(document.getElementById("txtSite").value=="")
         {
            
             alert("Please enter a Site");
             document.getElementById("txtSite").focus();
             return;
         }
         http5.open("GET","New_AjaxPage.aspx?mode=getdata&SiteName=" + document.getElementById("txtSite").value + "&r="+Math.random()); 

         
 }
 else
 {
        var SiteId=document.getElementById("ddlSite").value;
      
        if(SiteId=='')
        {
           alert("Please select a Site");
           document.getElementById("ddlSite").focus();
           return;
        }
         
            http5.open("GET","New_AjaxPage.aspx?mode=getdata&SiteId=" + SiteId + "&r="+Math.random()); 

 }
  //   http5.open("GET","New_AjaxPage.aspx?mode=getdata&SiteId=" + SiteId + "&r="+Math.random()); 
                                                 
            http5.onreadystatechange=function()
            {
                if (http5.readyState == 4) 
                {       
                    //document.getElementById("dvAjaxPic").style.display="none"; 
//                    document.getElementById("dvAjaxPic").innerHTML=" ";
                    var response = http5.responseText; 
                  
                                  
                    var update =new Array();
                      
                        if(response.indexOf('|')>-1)
                        {            
                          update=response.split('|');
                        
                       
                         document.getElementById("dvDetail").style.display="block";
                         document.getElementById("spchange").style.display="block"; 
                        // document.getElementById("dvAjaxPic").innerHTML="";
                         document.getElementById("dvAjaxPic").style.display="none";
//                        alert(update[0].toString()); 
//                        alert(update[1].toString());    
                         document.getElementById("txtBackColor").value= update[0].toString(); 
                        
//                        document.getElementById("ddlFontType").options[document.getElementById("ddlFontSize").selectedIndex].text= update[1].toString(); 
                        
                         document.getElementById("ddlFontType").value= update[1].toString(); 
                                                    
//                        document.getElementById("ddlFontSize").options[document.getElementById("ddlFontSize").selectedIndex].text= update[2].toString(); 
                          
                        document.getElementById("ddlFontSize").value= update[2].toString(); 
                       
                        document.getElementById("txtFontColor").value= update[3].toString()   
                       
                        document.getElementById("txtBorderBGColor").value= update[4].toString(); 
                        document.getElementById("dvIframe").innerHTML="";
                        if(document.getElementById("hdsite").value=="0" || document.getElementById("hdsite").value=="1")
                        {
                            document.getElementById("dvIframe").innerHTML="<iframe  src=\"iframeImage.aspx?SiteName="+document.getElementById("txtSite").value+"\" id=\"iframeImage\" scrolling=\"no\" frameborder=\"0\" hidefocus=\"true\" style=\"text-align: center; vertical-align: middle; border-style: none; margin:0 0 10px 0; width:521px; overflow:hidden; \"></iframe>";

                        }
                        else if(document.getElementById("hdsite").value=="2")

                        {
                           document.getElementById("dvIframe").innerHTML="<iframe  src=\"iframeImage.aspx?SiteId="+document.getElementById("ddlSite").value+"\" id=\"iframeImage\" scrolling=\"no\" frameborder=\"0\" hidefocus=\"true\" style=\"text-align: center; vertical-align: middle; border-style: none; margin:0 0 10px 0; width:521px; overflow:hidden; \"></iframe>";
                        }
                        else
                        {
                           document.getElementById("dvIframe").innerHTML="<iframe  src=\"iframeImage.aspx\" id=\"iframeImage\" scrolling=\"no\" frameborder=\"0\" hidefocus=\"true\" style=\"text-align: center; vertical-align: middle; border-style: none; margin:0 0 10px 0; width:521px; overflow:hidden; \"></iframe>";

                    
                        }
                      
                      






                        }
                        else
                        {
                        document.getElementById("dvDetail").innerHTML="There is some problem";
                       
                        
                        }            
//                    if(update[0].length<1)
//                    {
//                        document.getElementById("dvDetail").style.display="none"; 
//                        alert('jj');      
//                    }
                      delete http5;
                     http5 = null;
  
                    
               }
               else
               {
              // document.getElementById("dvAjaxPic").style.display="block";
               //document.getElementById("dvAjaxPic").innerHTML="<img  src=\"http://www.reliablelinkexchange.com/images/loading.gif\" alt=\"Wait...\" />"
               document.getElementById("dvAjaxPic").style.display="none"; 
               document.getElementById("spchange").style.display="none"; 
               } 
           } 
           http5.send(null);  

}

    function updateCSS()
{
        var HeaderImage="";
        var ImageURL=""; 
        var rbHeaderImage=""; 
        var SiteName=""; 
          if(document.getElementById('ddlSite').value!='')
          {
              SiteId=document.getElementById('ddlSite').value;
               if(SiteId=='')
               {
                 alert("Please select a Site");
                 return;
               }
          }
          else
          {
             SiteName=document.getElementById('txtSite').value;
             if(SiteName=='')
             {
                alert("Please enter a Site");
                return;
              }
         }
        
       
       var BackColor=  document.getElementById("txtBackColor").value;
                        
//        var FontType=  document.getElementById("ddlFontType").options[document.getElementById("ddlFontType").selectedIndex].text; 
       var FontType=  document.getElementById("ddlFontType").value; 
      
                                                    
      // var FontSize=   document.getElementById("ddlFontSize").options[document.getElementById("ddlFontSize").selectedIndex].text; 
       var FontSize=   document.getElementById("ddlFontSize").value; 
        
        var FontColor=  document.getElementById("txtFontColor").value;  
                   
        var BorderBGColor=  document.getElementById("txtBorderBGColor").value;
      
         var iframe = document.getElementById("iframeImage");
        var doc = null;
        if (iframe.contentDocument){
           doc = iframe.contentDocument;
        }
        else if 
        (
          iframe.contentWindow)
          {
           doc = iframe.contentWindow.document;
          }
          else 
          {
            doc = window.frames[iframe].document;
          }
          if(doc)
         {
            
             if (doc.getElementById("rbHeaderImage").checked==true)
             {
             HeaderImage=doc.getElementById('HeaderImage').src;
             }
             else if (doc.getElementById("rbImageUrl").checked==true)
             {
                 ImageURL= doc.getElementById('txtImageURL').value;
             }
              else
              {
                 rbHeaderImage="1";
              }
        }
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
            self.xmlHttpReq.open('POST',"New_AjaxPage.aspx", true);             
            self.xmlHttpReq.setRequestHeader('Content-Type', 'application/x-www-form-urlencoded');
            self.xmlHttpReq.onreadystatechange = function() 
            {
                if (self.xmlHttpReq.readyState == 4) 
                {          
                   // document.getElementById("dvAjaxPic").style.display="none";
                    //alert(self.xmlHttpReq.responseText);
                    
                     var update =new Array();
                    if(self.xmlHttpReq.responseText.indexOf('~' != -1))
                    {
                        update=self.xmlHttpReq.responseText.split("~");
                    }
                    if(update[0].toString()!="")
                    {
                        if(update[0].toString()=="0")
                        {
                             document.getElementById("lblMsg").innerHTML=update[1].toString();
                        }
                        
                        else if(update[0].toString()=="1")
                        {
                            document.getElementById("lblMsg").innerHTML=update[1].toString();
                            window.location.href="EditLinkPageCustomisation.aspx?flag=update";
                           
                        }
                    }
                    else
                    {
                        alert("Ajax return not found...");
                    }
                }
                else
                {
                    
                   //  document.getElementById("dvAjaxPic").innerHTML="<img  src=\"images/_loading.gif\" alt=\"Wait...\" />Wait..."                   
                }
            }                
            if(document.getElementById('ddlSite').value!='')
          {
              self.xmlHttpReq.send("mode=updateCSS&SiteId="+SiteId+"&BackColor="+BackColor+"&FontType="+FontType+"&FontSize="+FontSize+"&FontColor="+FontColor+"&BorderBGColor="+BorderBGColor+"&HeaderImage="+HeaderImage+"&ImageURL="+ImageURL+"&rbHeaderImage="+rbHeaderImage+"&r="+Math.random());                                                  

          }
          else
          {
            self.xmlHttpReq.send("mode=updateCSS&SiteName="+SiteName+"&BackColor="+BackColor+"&FontType="+FontType+"&FontSize="+FontSize+"&FontColor="+FontColor+"&BorderBGColor="+BorderBGColor+"&HeaderImage="+HeaderImage+"&ImageURL="+ImageURL+"&rbHeaderImage="+rbHeaderImage+"&r="+Math.random());                                                  

          }
      
    
}
    
    function handleResponse(webID)
    {
        if(http.readyState == 4) 
        {
//            document.getElementById("SiteURL").innerHTML="";
            document.getElementById("txtSubPageURL").value="";
            document.getElementById("txtSubPageDesc").value="";
            var response = http.responseText;
            var update = new Array();
            alert(response);
            if(response.indexOf('|' != -1)) 
            {
                update = response.split("|");
//                document.getElementById("SiteURL").innerHTML =update[0]; 
                document.getElementById("divSiteID").innerHTML="http://www.reliablelinkexchange.com/RequestLinkExchange.aspx?r="+webID;
                document.getElementById("txtSubPageURL").value=update[0];
                document.getElementById("txtSubPageDesc").value="<a href='http://www.reliablelinkexchange.com/RequestLinkExchange.aspx?r="+webID+"' target='_blank'>Click here</a>";
            }
        }
    }
 
   function GetSiteURL1()
    {    
        var webID=document.getElementById("ddlSite").value;
         var http = createRequestObject(webID);
        http.open("GET", "New_AjaxPage.aspx?id="+webID+"&r=" + Math.random()+"&mode=GetSiteURL");
        http.onreadystatechange = function()
        {
           
           if(http.readyState == 4) 
          {
//            document.getElementById("SiteURL").innerHTML="";
            document.getElementById("txtSubPageURL").value="";
            document.getElementById("txtSubPageDesc").value="";
            var response = http.responseText;
            alert(http.responseText);
            var update = new Array();
            alert(response);
            if(response.indexOf('|' != -1)) 
            {
                update = response.split("|");
//                document.getElementById("SiteURL").innerHTML =update[0]; 
                document.getElementById("divSiteID").innerHTML="http://www.reliablelinkexchange.com/RequestLinkExchange.aspx?r="+webID;
                document.getElementById("txtSubPageURL").value=update[0];
                document.getElementById("txtSubPageDesc").value="<a href='http://www.reliablelinkexchange.com/RequestLinkExchange.aspx?r="+webID+"' target='_blank'>Click here</a>";
            }
         }
         else
         {
                document.getElementById("dvAjaxPic").innerHTML="<img  src=\"images/loading.gif\" alt=\"Wait...\" />"

         }
      }


                                           
    


        http.send(null);
    }  
    
 
 function GetSiteURL(obj)
{
         document.getElementById("lblMsg").innerHTML="";  
         var SiteId="";
         var http5;
         http5=createRequestObject();
//       alert(obj.type);
        if(obj.type=="text")
        {
         if(obj.value=='')
         {
           alert("Please enter a Site");
           return;
         }
         http5.open("GET","New_AjaxPage.aspx?mode=GetSiteURL&SiteName=" + obj.value + "&r="+Math.random()); 
     }
     else
     {
        var SiteId=document.getElementById("ddlSite").value;
      
        if(SiteId=='')
         {
           alert("Please select a Site");
           return;
         }
         
         http5.open("GET","New_AjaxPage.aspx?mode=GetSiteURL&SiteId=" + SiteId + "&r="+Math.random()); 

     }
                                                 
            http5.onreadystatechange=function()
            {
                if (http5.readyState == 4) 
                {       
                    var response = http5.responseText; 
                    var update =new Array();
                        if(response.indexOf('|')>-1)
                        {            
                          
                          document.getElementById("dvDetail").style.display="block";
                          document.getElementById("spchange").style.display="block"; 
                          document.getElementById("dvAjaxPic").innerHTML="";
//                          alert(document.getElementById("dvCode").innerHTML);
//                          document.getElementById("dvCode").innerHTML="";
                          
                          update=response.split('|');
                         // alert(update[0]);
                          //document.getElementById("divSiteID").innerHTML="http://www.reliablelinkexchange.com/RequestLinkExchange.aspx?r="+SiteId;
                         // alert(document.getElementById("txtSubPageURL"));
                          document.getElementById("txtSubPageURL").value=update[0];
                          document.getElementById("txtSubPageDesc").value="<a href='http://www.reliablelinkexchange.com/RequestLinkExchange.aspx?r="+update[1]+"' target='_blank'>Click here</a>";
                           document.getElementById("txtSubPageName").value="";
                        }
                        else
                        {
                           document.getElementById("dvDetail").innerHTML="There is some problem";
                        }            
//                    if(update[0].length<1)
//                    {
//                        document.getElementById("dvDetail").style.display="none"; 
//                        alert('jj');      
//                    }
                      delete http5;
                     http5 = null;
  
                    
               }
               else
               {
                    document.getElementById("dvAjaxPic").innerHTML="<img  src=\"images/loading.gif\" alt=\"Wait...\" />"
                   document.getElementById("spchange").style.display="none"; 
               } 
           } 
           http5.send(null);  

}
  
    
   
    
    
    function gotoeditpage(obj)
{
//    document.getElementById("lblMsg").innerHTML=" ";
  document.getElementById("lblMsg").innerHTML="";  

     
     var SiteId="";
     var http5;
    http5=createRequestObject();
//    alert(obj.type);
     if(obj.type=="text")
     {
        if(obj.value=='')
        {
           alert("Please enter a Site");
           return;
        }
       
       
       
        http5.open("GET","New_AjaxPage.aspx?mode=getdata&SiteName=" + obj.value + "&r="+Math.random()); 

     }
     else
     {
        var SiteId=document.getElementById("ddlSite").value;
      
        if(SiteId=='')
        {
           alert("Please select a Site");
           return;
        }
         
            http5.open("GET","New_AjaxPage.aspx?mode=getdata&SiteId=" + SiteId + "&r="+Math.random()); 

     }
  //   http5.open("GET","New_AjaxPage.aspx?mode=getdata&SiteId=" + SiteId + "&r="+Math.random()); 
                                                 
            http5.onreadystatechange=function()
            {
                if (http5.readyState == 4) 
                {       
                    //document.getElementById("dvAjaxPic").style.display="none"; 
//                    document.getElementById("dvAjaxPic").innerHTML=" ";
                    var response = http5.responseText; 
                
                               
                    var update =new Array();
                     // var update=URLEncode(response);
                      
                        if(response.indexOf('|')>-1)
                        {            
                          update=response.split('|');
                       
                      
                     // var url=  mode=BackColor="+BackColor+"&FontType="+FontType+"&FontSize="+FontSize+"&FontColor="+FontColor+"&BorderBGColor="+BorderBGColor+"&HeaderImage="+HeaderImage+"&ImageURL="+ImageURL+"&rbHeaderImage="+rbHeaderImage+"&r="+Math.random()
                      window.location="EditLinkPageCustomisation.aspx?update="+encodeURIComponent(response);
                      
                      //  window.location="EditLinkPageCustomisation.aspx?BackColor="+encodeURIComponent(BackColor)+"&FontType="+encodeURIComponent(update[1].toString())+"&FontSize="+encodeURIComponent(update[2].toString())+"&FontColor="+encodeURIComponent(update[3].toString())+"&BorderBGColor="+encodeURIComponent(update[4].toString())+"&SiteUrl="+update[5].toString()+"&SiteId="+update[6].toString();
                       //window.location.href= "EditLinkPageCustomisation.aspx?BackColor=SMTPCheck&FontType=" +update[0].toString()+"&FontType="+update[1].toString()+"&FontSize="+update[2].toString()+"&FontColor="+update[3].toString()+"&BorderBGColor="+update[4].toString()+"&SiteUrl="+update[5].toString()+"&SiteId="+update[6].toString();
                       
                       // window.location.href="EditLinkPageCustomisation.aspx?BackColor="+1+"&FontType="+2+"&FontSize="+Arial+"&FontColor="+9+"&BorderBGColor="+2+"&SiteUrl="+update[5].toString()+"&SiteId="+update[6].toString();

                       // window.location.href="EditLinkPageCustomisation.aspx?BackColor=1";

//                         alert(update[0].toString());
//                         document.getElementById("dvImg").style.display="none";
//                           document.getElementById("dvIframe").style.display="none";
//                         
////                         document.getElementById("dvDetail").style.display="block";
//                         document.getElementById("spchange").style.display="block"; 
//                         document.getElementById("dvAjaxPic").innerHTML="";
//                         document.getElementById("txtBackColor").value= update[0].toString(); 
//                         alert(document.getElementById("txtBackColor").value);
//                         document.getElementById("ddlFontType").value= update[1].toString(); 
//                        document.getElementById("ddlFontSize").value= update[2].toString(); 
//                        document.getElementById("txtFontColor").value= update[3].toString()   
//                        document.getElementById("txtBorderBGColor").value= update[4].toString(); 
//                        document.getElementById("dvIframe").innerHTML="";
//                        if(obj.type=="text")
//                        {
//                            document.getElementById("dvIframe").innerHTML="<iframe  src=\"iframeImage.aspx?SiteName="+obj.value+"\" id=\"iframeImage\" scrolling=\"no\" frameborder=\"0\" hidefocus=\"true\" style=\"text-align: center; vertical-align: middle; border-style: none; margin: 0px;width: 521px; height: 190px\"></iframe>";

//                        }
//                        else  if(obj.type=="select-one")
//                        {
//                           alert('hi');
//                           document.getElementById("dvIframe").innerHTML="<iframe  src=\"iframeImage.aspx?SiteId="+SiteId+"\" id=\"iframeImage\" scrolling=\"no\" frameborder=\"0\" hidefocus=\"true\" style=\"text-align: center; vertical-align: middle; border-style: none; margin: 0px;width: 521px; height: 190px\"></iframe>";
//                        }
//                        else
//                        {
//                           document.getElementById("dvIframe").innerHTML="<iframe  src=\"iframeImage.aspx\" id=\"iframeImage\" scrolling=\"no\" frameborder=\"0\" hidefocus=\"true\" style=\"text-align: center; vertical-align: middle; border-style: none; margin: 0px;width: 521px; height: 190px\"></iframe>";

//                    
//                        }
                      
                      






                        }
                        else
                        {
                        document.getElementById("dvDetail").innerHTML="There is some problem";
                       
                        
                        }            
//                    if(update[0].length<1)
//                    {
//                        document.getElementById("dvDetail").style.display="none"; 
//                        alert('jj');      
//                    }
                      delete http5;
                     http5 = null;
  
                    
               }
               else
               {
              // document.getElementById("dvAjaxPic").style.display="block";
               document.getElementById("dvAjaxPic").innerHTML="<img  src=\"images/loading.gif\" alt=\"Wait...\" />"
               document.getElementById("spchange").style.display="none"; 
               } 
           } 
           http5.send(null);  

}  

function getTemplateData()
{
    document.getElementById("lblMsg").innerHTML="";  
    var TemplateID= document.getElementById("ddlTemplateName").value;
    if(TemplateID=='')
    {
       alert("Please select a Template");
       return;
    }
     var http5;
    http5=createRequestObject();
//    alert(obj.type);
 
     http5.open("GET","New_AjaxPage.aspx?mode=TemplateData&TemplateID=" + TemplateID + "&r="+Math.random()); 
                                                 
            http5.onreadystatechange=function()
            {
                if (http5.readyState == 4) 
                {       
                    var response = http5.responseText; 
                    var update =new Array();
                    if(response.indexOf('|')>-1)
                    {            
                         update=response.split('|');
                         document.getElementById("dvTemplate").style.display="block";
                         document.getElementById("dvAjaxPic").style.display="none";
//                        alert(update[0].toString()); 
//                        alert(update[1].toString());    
                          document.getElementById("txtTemplateName").value=update[0].toString();
                          document.getElementById("txtTemplateSubject").value=update[1].toString();
//                          alert(update[2]);
                        //  alert(document.getElementById("txtTemplateCode").contentWindow.document.body);
//                          alert(tinyMCE.activeEditor.editorId);
//                          alert(document.getElementById("txtTemplateCode_ifr"));
//                          alert(document.getElementById("txtTemplateCode_ifr").innerHTML);
//                          document.getElementById("txtTemplateCode_ifr").innerHTML=update[2].toString();
                          // tinyMCE.getContent('txtTemplateCode')=update[2].toString();
                              var iframe = document.getElementById("txtTemplateCode_ifr");
                            var doc = null;
                            if (iframe.contentDocument)
                            {
                                doc = iframe.contentDocument;
                            }
                            else if (iframe.contentWindow)
                            {
                                 doc = iframe.contentWindow.document;
                            }
                           else 
                           {
                                 doc = window.frames[iframe].document;
                           }
                         if(doc)
                           {
                                 doc.getElementById("tinymce").innerHTML=update[2].toString();
                           }
                     }
                     else
                     {
                        document.getElementById("dvTemplate").innerHTML="There is some problem";
                     }            
//                    if(update[0].length<1)
//                    {
//                        document.getElementById("dvDetail").style.display="none"; 
//                        alert('jj');      
//                    }
                      delete http5;
                     http5 = null;
  
                    
               }
               else
               {
              // document.getElementById("dvAjaxPic").style.display="block";
               //document.getElementById("dvAjaxPic").innerHTML="<img  src=\"http://www.reliablelinkexchange.com/images/loading.gif\" alt=\"Wait...\" />"
                 document.getElementById("dvAjaxPic").style.display="block"; 
               } 
           } 
           http5.send(null);  

}

  function funOpenMailDiv(SlNo,Buttontype,id)
{
    
    var SiteId=document.getElementById("hdSiteId").value
     var SubPageId=document.getElementById("hdSubPageId").value
    
     var http5;
    http5=createRequestObject();
//    alert(obj.type);
    if(SiteId!="")
    {
      http5.open("GET","New_AjaxPage.aspx?mode="+Buttontype +"&r="+Math.random()+"&SiteId=" + SiteId+"&SlNo=" + SlNo+"&LnkexcMasterId="+id); 
    } 
    else
    {
      http5.open("GET","New_AjaxPage.aspx?mode="+Buttontype +"&r="+Math.random()+"&SubPageId=" + SubPageId+"&SlNo=" + SlNo+"&LnkexcMasterId="+id); 

    }                                          
            http5.onreadystatechange=function()
            {
                if (http5.readyState == 4) 
                {       
                    var response = http5.responseText; 
                    var update =new Array();
                    if(response.indexOf('|')>-1)
                    {            
                         update=response.split('|');
                         var dvMail=document.getElementById("dvMail_"+SlNo);
                         dvMail.style.display="block";
                         document.getElementById("dvAjaxPic"+SlNo).style.display="none";
                        //alert(update[2].toString()); 
                       // alert(update[3].toString());
                       // alert(update[4].toString());
                        document.getElementById("txtFrom"+SlNo).value=update[0].toString();
                         document.getElementById("txtTo"+SlNo).value=update[1].toString();
                         
                          document.getElementById("txtTemplateSubject"+SlNo).value=update[3].toString();
                         
                          var iframe = document.getElementById("txtTemplateCode"+SlNo+"_ifr");
                           // alert(document.getElementById("txtTemplateCode"+SlNo+"_ifr"));
                           // txtTemplateCode1_ifr
                            var doc = null;
                            if (iframe.contentDocument)
                            {
                                doc = iframe.contentDocument;
                            }
                            else if (iframe.contentWindow)
                            {
                                 doc = iframe.contentWindow.document;
                            }
                           else 
                           {
                                 doc = window.frames[iframe].document;
                           }
                         if(doc)
                           {
                                 doc.getElementById("tinymce").innerHTML=update[4].toString();
                           }
                         
                         
                        
                         if(Buttontype=="SendMail")
                         {   
                            
                            document.getElementById("spUpdateStatus"+SlNo).style.display="none";
                            document.getElementById("spddl"+SlNo).innerHTML=update[5].toString();
                            document.getElementById("ddlTemplate"+SlNo).value=update[2].toString();
                         }
                         else
                         {
                            document.getElementById("spUpdateStatus"+SlNo).style.display="block";
                            document.getElementById("spddl"+SlNo).innerHTML=update[5].toString();
                         }
                            
                                
                             
                             
                         
                         
                        
                        // alert(document.getElementById("txtTemplateSubject"+SlNo));
                       
                          
                     }
                     else
                     {
                       // document.getElementById("dvTemplate").innerHTML="There is some problem";
                     }            
//                    if(update[0].length<1)
//                    {
//                        document.getElementById("dvDetail").style.display="none"; 
//                        alert('jj');      
//                    }
                      delete http5;
                     http5 = null;
  
                    
               }
               else
               {
              // document.getElementById("dvAjaxPic").style.display="block";
               //document.getElementById("dvAjaxPic").innerHTML="<img  src=\"http://www.reliablelinkexchange.com/images/loading.gif\" alt=\"Wait...\" />"
                 document.getElementById("dvAjaxPic"+SlNo).style.display="block"; 
               } 
           } 
           http5.send(null);  

}

function getTemplateInfo(obj,SlNo,id)
{
//    document.getElementById("lblMsg").innerHTML="";  
    var TemplateID= obj.value;
    if(TemplateID=='')
    {
       alert("Please select a Template");
       return;
    }
     var SiteId=document.getElementById("hdSiteId").value
     var SubPageId=document.getElementById("hdSubPageId").value
    
     var http5;
    http5=createRequestObject();
     if(SiteId!="")
    {
      http5.open("GET","New_AjaxPage.aspx?mode=TemplateInfo&TemplateID=" + TemplateID + "&SiteId=" + SiteId+"&r="+Math.random()+"&LnkexcMasterId="+id); 

    } 
    else
    {
     // http5.open("GET","New_AjaxPage.aspx?mode="+TemplateInfo +"&r="+Math.random()+"&SubPageId=" + SubPageId+"&SlNo=" + SlNo+"&LnkexcMasterId="+id); 
      http5.open("GET","New_AjaxPage.aspx?mode=TemplateInfo&TemplateID=" + TemplateID + "&SubPageId=" + SubPageId+"&r="+Math.random()+"&LnkexcMasterId="+id); 

    }      
    // http5.open("GET","New_AjaxPage.aspx?mode=TemplateInfo&TemplateID=" + TemplateID + "&SiteId=" + SiteId+"&r="+Math.random()); 
                                                 
            http5.onreadystatechange=function()
            {
                if (http5.readyState == 4) 
                {       
                    var response = http5.responseText; 
                    var update =new Array();
                    if(response.indexOf('|')>-1)
                    {            
                         update=response.split('|');
                         //document.getElementById("dvTemplate").style.display="block";
                         document.getElementById("dvAjaxPic_ddl"+SlNo).style.display="none";
//                        alert(update[0].toString()); 
 //                        alert(update[1].toString());    
//                          document.getElementById("txtTemplateName").value=update[0].toString();
                          document.getElementById("txtTemplateSubject"+SlNo).value=update[0].toString();
//                          alert(update[2]);
                        //  alert(document.getElementById("txtTemplateCode").contentWindow.document.body);
//                          alert(tinyMCE.activeEditor.editorId);
//                          alert(document.getElementById("txtTemplateCode_ifr"));
//                          alert(document.getElementById("txtTemplateCode_ifr").innerHTML);
//                          document.getElementById("txtTemplateCode_ifr").innerHTML=update[2].toString();
                          // tinyMCE.getContent('txtTemplateCode')=update[2].toString();
                              var iframe = document.getElementById("txtTemplateCode"+SlNo+"_ifr");
                            
                            var doc = null;
                            if (iframe.contentDocument)
                            {
                                doc = iframe.contentDocument;
                            }
                            else if (iframe.contentWindow)
                            {
                                 doc = iframe.contentWindow.document;
                            }
                           else 
                           {
                                 doc = window.frames[iframe].document;
                           }
                         if(doc)
                           {
                                 doc.getElementById("tinymce").innerHTML=update[1].toString();
                           }
                     }
                     else
                     {
                       // document.getElementById("dvTemplate").innerHTML="There is some problem";
                     }            

                      delete http5;
                     http5 = null;
  
                    
               }
               else
               {
              // document.getElementById("dvAjaxPic").style.display="block";
               //document.getElementById("dvAjaxPic").innerHTML="<img  src=\"http://www.reliablelinkexchange.com/images/loading.gif\" alt=\"Wait...\" />"
                 document.getElementById("dvAjaxPic_ddl"+SlNo).style.display="block"; 
               } 
           } 
           http5.send(null);  

}



function getSiteData()
{
    document.getElementById("lblMsg").innerHTML="";  
    var SiteId= document.getElementById("ddlSite").value;
    if(SiteId=='')
    {
       alert("Please select a Site");
       return;
    }
     var http5;
    http5=createRequestObject();
//    alert(obj.type);
 
     http5.open("GET","New_AjaxPage.aspx?mode=SiteData&SiteId=" + SiteId + "&r="+Math.random()); 
                                                 
            http5.onreadystatechange=function()
            {
                if (http5.readyState == 4) 
                {       
                    var response = http5.responseText; 
                    var update =new Array();
                    if(response.indexOf('|')>-1)
                    {            
                         update=response.split('|');
                         document.getElementById("dvSiteDetail").style.display="block";
                         document.getElementById("dvAjaxPic").style.display="none";
                         document.getElementById("txtName").value=update[0].toString();
                          document.getElementById("txtDescription").value=update[1].toString();
                          document.getElementById("txtSiteURL").value=update[2].toString();
                          document.getElementById("txtSMTPServer").value=update[3].toString();
                          document.getElementById("txtSMTP_Port").value=update[4].toString();
                          document.getElementById("txtSMTP_UserName").value=update[5].toString();
                          document.getElementById("txtEmail").value=update[6].toString();
                          document.getElementById("password").value=update[7].toString();
                          var iframe = document.getElementById("txtSignature_ifr");
                          var doc = null;
                            if (iframe.contentDocument)
                            {
                                doc = iframe.contentDocument;
                            }
                            else if (iframe.contentWindow)
                            {
                                 doc = iframe.contentWindow.document;
                            }
                           else 
                           {
                                 doc = window.frames[iframe].document;
                           }
                         if(doc)
                           {
                                 doc.getElementById("tinymce").innerHTML=update[8].toString();
                           }
                     }
                     else
                     {
                        document.getElementById("dvSiteDetail").innerHTML="There is some problem";
                     }            
//                    if(update[0].length<1)
//                    {
//                        document.getElementById("dvDetail").style.display="none"; 
//                        alert('jj');      
//                    }
                      delete http5;
                     http5 = null;
  
                    
               }
               else
               {
                 document.getElementById("dvAjaxPic").style.display="block"; 
               } 
           } 
           http5.send(null);  

}


 function GetSubpageData()
{
         document.getElementById("lblMsg").innerHTML="";  
         var SiteId="";
         var http5;
         http5=createRequestObject();
      
        var SiteId=document.getElementById("ddlSite").value;
      
        if(SiteId=='')
         {
           alert("Please select a Site");
           return;
         }
         
         http5.open("GET","New_AjaxPage.aspx?mode=SubpageData&SiteId=" + SiteId + "&r="+Math.random()); 

            http5.onreadystatechange=function()
            {
                if (http5.readyState == 4) 
                {       
                    var response = http5.responseText; 
                    var update =new Array();
                        if(response.indexOf('|')>-1)
                        {            
                          
                          document.getElementById("dvDetail").style.display="block";
                          document.getElementById("dvAjaxPic").innerHTML="";
//                          alert(document.getElementById("dvCode").innerHTML);
//                          document.getElementById("dvCode").innerHTML="";
                          
                          update=response.split('|');
                          document.getElementById("txtSubPageURL").value=update[0];
                          document.getElementById("txtSubPageDesc").value="<a href='http://www.reliablelinkexchange.com/RequestLinkExchange.aspx?r="+update[1]+"' target='_blank'>Click here</a>";
                           document.getElementById("txtSubPageName").value="";
                        }
                        else
                        {
                           document.getElementById("dvDetail").innerHTML="There is some problem";
                        }            
//                    if(update[0].length<1)
//                    {
//                        document.getElementById("dvDetail").style.display="none"; 
//                        alert('jj');      
//                    }
                      delete http5;
                     http5 = null;
  
                    
               }
               else
               {
                    document.getElementById("dvAjaxPic").innerHTML="<img  src=\"images/loading.gif\" alt=\"Wait...\" />"
                   document.getElementById("spchange").style.display="none"; 
               } 
           } 
           http5.send(null);  

}
function Insert_MailSend(SlNo,Buttontype,id)
{
//      alert(SlNo);
//      alert(Buttontype);
//      alert(id);
    var stat="";
    var stat=document.getElementById("hdStatus").value
     var SiteId=document.getElementById("hdSiteId").value
     var SubPageId=document.getElementById("hdSubPageId").value
     var FromEmail=document.getElementById("txtFrom"+SlNo).value;
     
    if(FromEmail=="")
    {
       alert('Please enter From From Email');
       document.getElementById("txtFrom"+SlNo).focus();
       return;
    }
    var TemplateId="";
    var Body="";
    if(document.getElementById("spddl"+SlNo).innerHTML=='Reject Mail')
    {
       TemplateId="3";
       Status="3";
    }
    else if(document.getElementById("spddl"+SlNo).innerHTML=='Approve Mail')
    {
          TemplateId="5";
          Status="2";
          
    }
    else
    {
         TemplateId=document.getElementById("ddlTemplate"+SlNo).value;
         Status="";
         if(TemplateId=="")
         {
            alert('Please select template from Template dropdown');
            document.getElementById("ddlTemplate"+SlNo).focus();
            return;
         }
    }
   
   
    var Subject=document.getElementById("txtTemplateSubject"+SlNo).value;
    if(Subject=="")
    {
       alert('Please enter Subject ');
       document.getElementById("txtTemplateSubject"+SlNo).focus();
       return;
    }
    var ToEmail=document.getElementById("txtTo"+SlNo).value;
    if(ToEmail=="")
    {
       alert('Mail cannot be sent as no email id exists in the database against this entry.');
       document.getElementById("txtTo"+SlNo).focus();
       return;
    }
   
//    alert(Status);
//    alert(TemplateId);
//    alert(Subject);
    var iframe = document.getElementById("txtTemplateCode"+SlNo+"_ifr");
                           // alert(document.getElementById("txtTemplateCode"+SlNo+"_ifr"));
                           // txtTemplateCode1_ifr
                            var doc = null;
                            if (iframe.contentDocument)
                            {
                                doc = iframe.contentDocument;
                            }
                            else if (iframe.contentWindow)
                            {
                                 doc = iframe.contentWindow.document;
                            }
                           else 
                           {
                                 doc = window.frames[iframe].document;
                           }
                         if(doc)
                           {
                                var regEx = /<[^>]*>/g;
	                            Body=doc.getElementById("tinymce").innerHTML;
	                            if(doc.getElementById("tinymce").innerHTML.toString().replace(regEx, "")=="")
	                            { 
	                               alert('Please enter mail body');
                                   doc.getElementById("tinymce").focus();
                                   return false;
	                             }
                           }
                
     var http5;
    http5=createRequestObject();
//    alert(obj.type);
    http5.open("GET","New_AjaxPage.aspx?mode="+Buttontype+"&r="+Math.random()+"&TemplateId="+TemplateId+"&SlNo="+SlNo+"&LinkId="+id+"&Subject="+Subject+"&Body="+encodeURIComponent(Body)+"&Status="+Status+"&FromEmail="+FromEmail+"&ToEmail="+ToEmail); 
                                       
            http5.onreadystatechange=function()
            {
                if (http5.readyState == 4) 
                {       
                    var response = http5.responseText; 
                    var update =new Array();
                    if(response.indexOf('|')>-1)
                    {            
                         update=response.split('|');
                         var dvMail=document.getElementById("dvMail_"+SlNo);
                         dvMail.style.display="block";
                         document.getElementById("dvAjaxPic"+SlNo).style.display="none";
                        //alert(update[2].toString()); 
                       // alert(update[3].toString());
                       // alert(update[4].toString());
                        if(update[0].toString()=="0" || update[0].toString()=="2" || update[0].toString()=="3")
                        {
                           alert(update[1].toString());
                        }
                        else
                        {
                           if(Buttontype=="Insert_MailSend")
                           {
                             if(SiteId!="")
                             {
                                if(Status!="")
                                {
                                   window.location="LinkDetails-View.aspx?flag=mail&Status="+ stat+"&SiteId="+ SiteId;
                                }
                                else
                                {
                                 window.location="LinkDetails-View.aspx?flag=update&Status="+ stat+"&SiteId="+ SiteId;
                                }
                             }
                             else
                             {
                               if(Status!="")
                               {
                                 window.location="LinkDetails-View.aspx?flag=mail&Status="+ stat+"&SubPageId="+ SubPageId;
                               }
                              else
                              {
                                 window.location="LinkDetails-View.aspx?flag=update&Status="+ stat+"&SubPageId="+ SubPageId;
                              }
                             
                             }
                          } 
                          else
                          {
                            //alert(document.getElementById("btnUpdate_withoutMail"));
                           
                            if(SiteId!="")
                            {
                               window.location="LinkDetails-View.aspx?flag=update&Status="+ stat+"&SiteId="+ SiteId;
                            }
                            else
                            {
                               window.location="LinkDetails-View.aspx?flag=update&Status="+ stat+"&SubPageId="+ SubPageId;
                              
                            }
                          } 
                              //alert(update[1].toString());
                        
                        }
                         
                         
                        
                       
//                         if(Buttontype=="SendMail")
//                         {   
//                            document.getElementById("btnUpdate_withoutMail"+SlNo).style.display="none";
//                            document.getElementById("spddl"+SlNo).innerHTML=update[5].toString();
//                            document.getElementById("ddlTemplate"+SlNo).value=update[2].toString();
//                         }
//                         else
//                         {
//                            document.getElementById("spddl"+SlNo).innerHTML=update[5].toString();
//                          }
                          
                     }
                     else
                     {
                       // document.getElementById("dvTemplate").innerHTML="There is some problem";
                     }            
//                    if(update[0].length<1)
//                    {
//                        document.getElementById("dvDetail").style.display="none"; 
//                        alert('jj');      
//                    }
                      delete http5;
                     http5 = null;
  
                    
               }
               else
               {
              // document.getElementById("dvAjaxPic").style.display="block";
               //document.getElementById("dvAjaxPic").innerHTML="<img  src=\"http://www.reliablelinkexchange.com/images/loading.gif\" alt=\"Wait...\" />"
                 document.getElementById("dvMail_"+SlNo).style.display="none";
                 document.getElementById("dvAjaxPic"+SlNo).style.display="block"; 
               } 
           } 
           http5.send(null);  


}


function funopenCodeDiv()
    {
                var http5;
               http5=createRequestObject();
                if(document.getElementById("rbHTMLCode").checked==true)
                {
                     // alert('HTMLCode');
                      document.getElementById("dvHtmlCode").style.display="block";
                      document.getElementById("dvDescription").style.display="none";
                      if(document.getElementById("hdsite").value =="0" || document.getElementById("hdsite").value=="1")
                      {
                         if(document.getElementById("txtSite").value=="")
                         {
                            alert("Please provide your Site.");
                            document.getElementById("txtSite").focus();
                            return false;
                         }
                        http5.open("GET","New_AjaxPage.aspx?mode=InitiateLink&SiteName=" + document.getElementById("txtSite").value + "&type="+document.getElementById("rbHTMLCode").value+"&r="+Math.random()); 
                     }
                    else
                    {
                       
                       if(document.getElementById("ddlSite").value.length==0)
                       {
                          alert("Please select Site from Site drop down.");
                          document.getElementById("ddlSite").focus();
                          return false;
                        }
                      http5.open("GET","New_AjaxPage.aspx?mode=InitiateLink&SiteId=" + document.getElementById("ddlSite").value + "&type="+document.getElementById("rbHTMLCode").value+ "&r="+Math.random()); 
                    }
                   
                }
                else if(document.getElementById("rbDescription").checked==true)
                {
                    
                    document.getElementById("dvHtmlCode").style.display="none";
                    document.getElementById("dvDescription").style.display="block";
                    
                    if(document.getElementById("hdsite").value =="0" || document.getElementById("hdsite").value=="1")
                      {
                         if(document.getElementById("txtSite").value=="")
                         {
                            alert("Please provide your Site.");
                            document.getElementById("txtSite").focus();
                            return false;
                         }
                        http5.open("GET","New_AjaxPage.aspx?mode=InitiateLink&SiteName=" + document.getElementById("txtSite").value +"&type="+document.getElementById("rbDescription").value + "&r="+Math.random()); 
                     }
                    else
                    {
                      // alert(document.getElementById("<%=ddlSite.ClientID%>").value.length);
                       if(document.getElementById("ddlSite").value.length==0)
                       {
                          alert("Please select Site from Site drop down.");
                          document.getElementById("ddlSite").focus();
                          return false;
                        }
                      http5.open("GET","New_AjaxPage.aspx?mode=InitiateLink&SiteId=" + document.getElementById("ddlSite").value+"&type="+document.getElementById("rbDescription").value + "&r="+Math.random()); 
                    }
                }
                else
                {
                   alert("Please select How do you want to insert Data:");
                   return false;
                }
                 
            http5.onreadystatechange=function()
            {
                if (http5.readyState == 4) 
                {       
                    var response = http5.responseText; 
                    var update =new Array();
                    if(response.indexOf('|')>-1)
                    {            
                         update=response.split('|');
                         if(update[0].toString()!="0")
                         {
                          // alert(update[0].toString());
                          // alert(update[1].toString());
                          document.getElementById("dvMail").style.display="none";
                          document.getElementById("lblMsg").innerHTML="";
                           if(update[0].toString()=="HTMLCode")
                           {
//                              window.scrollBy(0,50);
                               window.scroll(400,400);
                             // window.scrollTo(200,200);
//                             document.getElementById("rb").checked=false;
                              document.getElementById("dvSubPage").innerHTML="";
                              document.getElementById("dvAddDesc").innerHTML="";
                              document.getElementById("btnHTMLCode").style.display="block";
                              
                             document.getElementById("dvAjaxPic").style.display="none";
                             document.getElementById("dvHtmlCode").style.display="block";
                             document.getElementById("dvAddHtmlCode").innerHTML="";
                             document.getElementById("txtHTMLcode").focus();
                             document.getElementById("txtHTMLcode").value="";
                             document.getElementById("txtEmail_HTML").value="";
                             document.getElementById("txtReciprocal_HTML").value="";
                             document.getElementById("txtFrom_URL_HTML").value="";
                             document.getElementById("txtPageRank_HTML").value="";
                             document.getElementById("txtfName_HTML").value="";
                             document.getElementById("txtlName_HTML").value="";
                             document.getElementById("dvAddHtmlCode").innerHTML=update[1].toString();
                             document.getElementById("dvSubPageHTMLCode").innerHTML="";
                             document.getElementById("dvSubPageHTMLCode").innerHTML=update[2].toString();
                           }
                           else
                           {
//                              document.getElementById("rb").checked=false;
                              document.getElementById("dvSubPageHTMLCode").innerHTML="";
                              document.getElementById("dvAddHtmlCode").innerHTML="";
                              document.getElementById("btnSubmit_Desc").style.display="block";
                              document.getElementById("dvAjaxPic").style.display="none";
                              document.getElementById("dvDescription").style.display="block";
                              document.getElementById("dvAddDesc").innerHTML="";
                              document.getElementById("txtUrlTitle").focus();
                              window.scrollBy(200,200);
                              document.getElementById("txtSiteURL").value="";
                              document.getElementById("txtUrlTitle").value="";
                              document.getElementById("txtHTMLCode_Desc").value="";
                              document.getElementById("txtEmail_Desc").value="";
                              document.getElementById("txtReciprocal_Desc").value="";
                              document.getElementById("txtfrom_url_Desc").value="";
                              document.getElementById("txtPageRank_Desc").value="";
                              document.getElementById("txtfName_Desc").value="";
                              document.getElementById("txtlName_Desc").value="";
                              document.getElementById("dvAddDesc").innerHTML=update[1].toString();
                              document.getElementById("dvSubPage").innerHTML="";
                              document.getElementById("dvSubPage").innerHTML=update[2].toString();
                           }
                           
                         }
                         else
                         {
                           alert("No Record in data base");
                           document.getElementById("dvHtmlCode").style.display="none";
                           document.getElementById("dvDescription").style.display="none";
                         }
                        
                     }
                     else
                     {
                        
                        document.getElementById("dvSiteDetail").innerHTML="There is some problem";
                     }            
//                    if(update[0].length<1)
//                    {
//                        document.getElementById("dvDetail").style.display="none"; 
//                        alert('jj');      
//                    }
                      delete http5;
                     http5 = null;
  
                    
               }
               else
               {
                  if(document.getElementById("rbHTMLCode").checked==true)
                  {
//                     document.getElementById("dvSubPageHTMLCode").innerHTML="<img  src=\"images/loading.gif\" />";
//                     document.getElementById("dvAddHtmlCode").innerHTML="<img  src=\"images/loading.gif\" />";  
                     document.getElementById("dvAjaxPic").style.display="block";
                     document.getElementById("dvHtmlCode").style.display="none";
                     
                  
                  }
                  else if(document.getElementById("rbDescription").checked==true)
                  {
//                     document.getElementById("dvSubPage").innerHTML="<img  src=\"images/loading.gif\" />"; 
//                     document.getElementById("dvAddDesc").innerHTML="<img  src=\"images/loading.gif\" />";
                      document.getElementById("dvAjaxPic").style.display="block";
                     document.getElementById("dvDescription").style.display="none";
                     
                  }
                 
               } 
           } 
           http5.send(null);  
    }

    
    
    function funOpenSiteDiv()
   {
    document.getElementById("lblMsg").innerHTML="";  
     var SiteId="";
     var http5;
    http5=createRequestObject();
   if( document.getElementById("hdsite").value=="0" || document.getElementById("hdsite").value=="1")
   {
         if(document.getElementById("txtSite").value=="")
         {
             alert("Please enter a Site");
             document.getElementById("txtSite").focus();
             return;
         }
         else
         {
              document.getElementById("dvRadio").style.display="block";
         }
         http5.open("GET","New_AjaxPage.aspx?mode=getSiteName&SiteName=" + document.getElementById("txtSite").value + "&r="+Math.random()); 
  }
 else
 {
        var SiteId=document.getElementById("ddlSite").value;
      
        if(SiteId=='')
        {
           alert("Please select a Site");
           document.getElementById("ddlSite").focus();
           return;
        }
        else
        {
           document.getElementById("dvRadio").style.display="block";
        }
            http5.open("GET","New_AjaxPage.aspx?mode=getSiteName&SiteId=" + SiteId + "&r="+Math.random()); 
 }
            http5.onreadystatechange=function()
            {
                if (http5.readyState == 4) 
                {       
                    var response = http5.responseText; 
                    var update =new Array();
                        if(response.indexOf('|')>-1)
                        {            
                          update=response.split('|');
                          if(update[0].toString()=="1")
                          {
                           
                             if(document.getElementById("rbHTMLCode").checked==true)
                             {
                                document.getElementById("dvHtmlCode").style.display="none";
                                document.getElementById("dvMail").style.display="none";  
                             }
                             else
                             {
                               document.getElementById("dvDescription").style.display="none";
                                document.getElementById("dvMail").style.display="none";   
                             }
                            document.getElementById("dvSite").style.display="block";
//                            document.getElementById("btnSite").style.display="block"; 
                            document.getElementById("lblSiteName").innerHTML=update[1].toString();
                          }
                          else
                          {
                            document.getElementById("dvSite").style.display="block";
//                            document.getElementById("btnSite").style.display="none"; 
                            document.getElementById("lblSiteName").innerHTML=update[1].toString();
                            
                          }
                        }
                               
//                    if(update[0].length<1)
//                    {
//                        document.getElementById("dvDetail").style.display="none"; 
//                        alert('jj');      
//                    }
                      delete http5;
                     http5 = null;
               }
               else
               {
                 document.getElementById("dvSite").style.display="block"; 
//                 document.getElementById("lblSiteName").innerHTML="<img  src=\"images/loading.gif\" />"; 
//                 document.getElementById("btnSite").style.display="none"; 
               } 
           } 
           http5.send(null);  

}

    
    
    function funOpenInitiateMailDiv1()
    { 
//           alert(funValidateInitiateLink());
            if(funValidateInitiateLink()==true)
            {
               document.getElementById("lblMsg").innerHTML=""; 
               var http5;
               var HTMLcode="";
               var SiteURL="";
               var t=""
               var SelectedAdId =encodeURIComponent(document.getElementById("hdSelectedAdId").value);
               if(document.getElementById("cbDirectory").checked==true)
               {
                  t = 3;
               }
               else
               {
                 t = 2;
               }
               http5=createRequestObject();
                if(document.getElementById("rbHTMLCode").checked==true)
                {
                     HTMLcode=encodeURIComponent(document.getElementById("txtHTMLcode").value);
                     if(document.getElementById("hdsite").value =="0" || document.getElementById("hdsite").value=="1")
                      {
                         if(document.getElementById("txtSite").value=="")
                         {
                            alert("Please provide your Site.");
                            document.getElementById("txtSite").focus();
                            return false;
                         }
                        if(document.getElementById("cbEmail_HTML").checked==true)
                        {
                          
                           
                           document.getElementById("btnHTMLCode").style.display = "none";
                          http5.open("GET","New_AjaxPage.aspx?mode=Mail_InitiateLink&SiteName=" + document.getElementById("txtSite").value + "&type="+document.getElementById("rbHTMLCode").value+"&SelectedAdId="+SelectedAdId+"&HTMLcode="+HTMLcode+"&SubPageId="+document.getElementById("ddlSubPage").value+"&Email="+document.getElementById("txtEmail_HTML").value+"&Reciprocal="+document.getElementById("txtReciprocal_HTML").value+"&From_URL="+document.getElementById("txtFrom_URL_HTML").value+"&PageRank="+document.getElementById("txtPageRank_HTML").value+"&fName="+document.getElementById("txtfName_HTML").value+"&lName="+document.getElementById("txtlName_HTML").value); 
                        } 
                        else
                        {
                          http5.open("GET","New_AjaxPage.aspx?mode=Add_InitiateLink&SiteName=" + document.getElementById("txtSite").value + "&type="+document.getElementById("rbHTMLCode").value+"&SelectedAdId="+SelectedAdId+"&HTMLcode="+HTMLcode+"&SubPageId="+document.getElementById("ddlSubPage").value+"&Email="+document.getElementById("txtEmail_HTML").value+"&Reciprocal="+document.getElementById("txtReciprocal_HTML").value+"&From_URL="+document.getElementById("txtFrom_URL_HTML").value+"&PageRank="+document.getElementById("txtPageRank_HTML").value+"&fName="+document.getElementById("txtfName_HTML").value+"&lName="+document.getElementById("txtlName_HTML").value+"&t="+t); 

                        }
                      } 
                     else
                     {
                       if(document.getElementById("ddlSite").value.length==0)
                       {
                          alert("Please select Site from Site drop down.");
                          document.getElementById("ddlSite").focus();
                          return false;
                        }
                        if(document.getElementById("cbEmail_HTML").checked==true)
                        {
	                      
	                      document.getElementById("btnHTMLCode").style.display = "none";
                          http5.open("GET","New_AjaxPage.aspx?mode=Mail_InitiateLink&SiteId=" + document.getElementById("ddlSite").value + "&type="+document.getElementById("rbHTMLCode").value+"&SelectedAdId="+SelectedAdId+"&HTMLcode="+HTMLcode+"&SubPageId="+document.getElementById("ddlSubPage").value+"&Email="+document.getElementById("txtEmail_HTML").value+"&Reciprocal="+document.getElementById("txtReciprocal_HTML").value+"&From_URL="+document.getElementById("txtFrom_URL_HTML").value+"&PageRank="+document.getElementById("txtPageRank_HTML").value+"&fName="+document.getElementById("txtfName_HTML").value+"&lName="+document.getElementById("txtlName_HTML").value+"&r="+Math.random()); 
                        } 
                        else
                        {
                          http5.open("GET","New_AjaxPage.aspx?mode=Add_InitiateLink&SiteId=" + document.getElementById("ddlSite").value + "&type="+document.getElementById("rbHTMLCode").value+"&SelectedAdId="+SelectedAdId+"&HTMLcode="+HTMLcode+"&SubPageId="+document.getElementById("ddlSubPage").value+"&Email="+document.getElementById("txtEmail_HTML").value+"&Reciprocal="+document.getElementById("txtReciprocal_HTML").value+"&From_URL="+document.getElementById("txtFrom_URL_HTML").value+"&PageRank="+document.getElementById("txtPageRank_HTML").value+"&fName="+document.getElementById("txtfName_HTML").value+"&lName="+document.getElementById("txtlName_HTML").value+ "&r="+Math.random()+"&t="+t); 
                        } 
                    }
                   
                }
                else if(document.getElementById("rbDescription").checked==true)
                {
                    if(document.getElementById("txtSiteURL").value.toString().indexOf("http://")==-1)
                    {
                       document.getElementById("txtSiteURL").value="http://"+document.getElementById("txtSiteURL").value;
                    }
                    HTMLcode = "<a href=\"" + document.getElementById("txtSiteURL").value + "\" target=\"_blank\">" + document.getElementById("txtUrlTitle").value + "</a><br>" + document.getElementById("txtHTMLCode_Desc").value ;
                    SiteURL="<a href=\"" + document.getElementById("txtSiteURL").value;
                    if(document.getElementById("hdsite").value =="0" || document.getElementById("hdsite").value=="1")
                      {
                         if(document.getElementById("txtSite").value=="")
                         {
                            alert("Please provide your Site.");
                            document.getElementById("txtSite").focus();
                            return false;
                         }
                        if(document.getElementById("cbEmail_Desc").checked==true)
                        {
                          document.getElementById("btnSubmit_Desc").style.display = "none";
                          http5.open("GET","New_AjaxPage.aspx?mode=Mail_InitiateLink&SiteName=" + document.getElementById("txtSite").value + "&type="+document.getElementById("rbDescription").value+"&SelectedAdId="+SelectedAdId+"&HTMLcode="+HTMLcode+"&SubPageId="+document.getElementById("ddlSubPage").value+"&Email="+document.getElementById("txtEmail_Desc").value+"&Reciprocal="+document.getElementById("txtReciprocal_Desc").value+"&From_URL="+document.getElementById("txtfrom_url_Desc").value+"&PageRank="+document.getElementById("txtPageRank_Desc").value+"&fName="+document.getElementById("txtfName_Desc").value+"&lName="+document.getElementById("txtlName_Desc").value+ "&r="+Math.random()); 
                        } 
                        else
                        {
                          http5.open("GET","New_AjaxPage.aspx?mode=Add_InitiateLink&SiteName=" + document.getElementById("txtSite").value + "&type="+document.getElementById("rbDescription").value+"&SelectedAdId="+SelectedAdId+"&HTMLcode="+HTMLcode+"&SubPageId="+document.getElementById("ddlSubPage").value+"&Email="+document.getElementById("txtEmail_Desc").value+"&Reciprocal="+document.getElementById("txtReciprocal_Desc").value+"&From_URL="+document.getElementById("txtfrom_url_Desc").value+"&PageRank="+document.getElementById("txtPageRank_Desc").value+"&fName="+document.getElementById("txtfName_Desc").value+"&lName="+document.getElementById("txtlName_Desc").value+ "&r="+Math.random()+"&t="+t+"&SiteURL="+SiteURL); 
                        } 
                     }
                    else
                    {
                       if(document.getElementById("ddlSite").value.length==0)
                       {
                          alert("Please select Site from Site drop down.");
                          document.getElementById("ddlSite").focus();
                          return false;
                        }
                     if(document.getElementById("cbEmail_Desc").checked==true)
                        {
                          document.getElementById("btnSubmit_Desc").style.display = "none";
                          http5.open("GET","New_AjaxPage.aspx?mode=Mail_InitiateLink&SiteId=" + document.getElementById("ddlSite").value + "&type="+document.getElementById("rbDescription").value+"&SelectedAdId="+SelectedAdId+"&HTMLcode="+HTMLcode+"&SubPageId="+document.getElementById("ddlSubPage").value+"&Email="+document.getElementById("txtEmail_Desc").value+"&Reciprocal="+document.getElementById("txtReciprocal_Desc").value+"&From_URL="+document.getElementById("txtfrom_url_Desc").value+"&PageRank="+document.getElementById("txtPageRank_Desc").value+"&fName="+document.getElementById("txtfName_Desc").value+"&lName="+document.getElementById("txtlName_Desc").value+ "&r="+Math.random()); 
                        } 
                        else
                        {
                          http5.open("GET","New_AjaxPage.aspx?mode=Add_InitiateLink&SiteId=" + document.getElementById("ddlSite").value + "&type="+document.getElementById("rbDescription").value+"&SelectedAdId="+SelectedAdId+"&HTMLcode="+HTMLcode+"&SubPageId="+document.getElementById("ddlSubPage").value+"&Email="+document.getElementById("txtEmail_Desc").value+"&Reciprocal="+document.getElementById("txtReciprocal_Desc").value+"&From_URL="+document.getElementById("txtfrom_url_Desc").value+"&PageRank="+document.getElementById("txtPageRank_Desc").value+"&fName="+document.getElementById("txtfName_Desc").value+"&lName="+document.getElementById("txtlName_Desc").value+ "&r="+Math.random()+"&t="+t+"&SiteURL="+SiteURL); 
                        } 
                    }
                }
                else
                {
                   alert("Please select How do you want to insert Data:");
                   return false;
                }
            http5.onreadystatechange=function()
            {
                if (http5.readyState == 4) 
                {       
                    var response = http5.responseText; 
                    var update =new Array();
                    if(response.indexOf('|')>-1)
                    {            
                         update=response.split('|');
                         if(update[0].toString()=="0")
                         {
                            alert("No Record in data base");
                         }
                         else if(update[0].toString()=="1")
                         {
                              document.getElementById("spMailAjax").style.display="none";
                              document.getElementById("dvMail").style.display = "none";
                              document.getElementById("lblMsg").innerHTML=="";
                              alert(update[1].toString());
                              document.getElementById("lblMsg").innerHTML=update[1].toString(); 
                         }
                         else
                         {
                              document.getElementById("spMailAjax").style.display="none";
                              document.getElementById("dvMail").style.display = "block";
                              window.scrollBy(400,400);
                              document.getElementById("spTemplateName").innerHTML=update[1].toString();
                              document.getElementById("txtTemplateSubject").value=update[2].toString();
                              document.getElementById("txtTo").value= update[4].toString();
                              document.getElementById("txtFrom").value= update[5].toString();
                              var iframe = document.getElementById("txtTemplateCode_ifr");
                              var doc = null;
                              if (iframe.contentDocument)
                              {
                                doc = iframe.contentDocument;
                              }
                              else if (iframe.contentWindow)
                              {
                                 doc = iframe.contentWindow.document;
                              }
                              else 
                              {
                                 doc = window.frames[iframe].document;
                              }
                              if(doc)
                              {
                                 doc.getElementById("tinymce").focus();
                                 doc.getElementById("tinymce").innerHTML=update[3].toString();
                              }
                         }
                     }
                     else
                     {
                        
                        document.getElementById("spMailAjax").innerHTML="There is some problem";
                     }            

                     delete http5;
                     http5 = null;
               }
               else
               {
                  if(document.getElementById("rbHTMLCode").checked==true)
                  {
                     document.getElementById("spMailAjax").style.display="block";
                     document.getElementById("dvMail").style.display="none";
                  }
                  else if(document.getElementById("rbDescription").checked==true)
                  {
                      document.getElementById("spMailAjax").style.display="block";
                      document.getElementById("dvMail").style.display="none";
                  }
               } 
           } 
           http5.send(null); 
      }  
    }
    
     function funValidateInitiateLink()
         {
//               document.getElementById("lblMsg").innerHTML=""; 
               if(document.getElementById("rbHTMLCode").checked==true)
                {
                   if(document.getElementById("txtHTMLcode").value=="")
                    {
                       alert("Please provide HTMLcode.");
                       document.getElementById("txtHTMLcode").focus();
                       return false;
                    }
                   if(document.getElementById("cbEmail_HTML").checked==true)
                   {
                      if(!validEmail(document.getElementById("txtEmail_HTML").value))
                      {
                            alert('Email entered is not in correct format');
                            document.getElementById("txtEmail_HTML").focus();
                            return false;
                      }
                  }
               }
               else if(document.getElementById("rbDescription").checked==true)
                {
                    if(document.getElementById("txtSiteURL").value=="")
                    {
                       alert("Please provide SiteURL.");
                       document.getElementById("txtSiteURL").focus();
                       return false;
                    }
//                    var v = new RegExp();
//                    v.compile("^[A-Za-z]+://[A-Za-z0-9-_]+\\.[A-Za-z0-9-_%&\?\/.=]+$");
//                    if (!v.test(document.getElementById("txtSiteURL").value))
//                    {
//            
//                          alert("Please enter valid SiteURL");
//                          document.getElementById("txtSiteURL").focus();
//                         return false;
//                     }
                   if(document.getElementById("cbEmail_Desc").checked==true)
                   {
                     if(!validEmail(document.getElementById("txtEmail_Desc").value))
                      {
                            alert('Email entered is not in correct format');
                            document.getElementById("txtEmail_Desc").focus();
                            return false;
                      }
                  }
               }
              // alert(document.getElementById("ddlSubPage").value);
               if(document.getElementById("ddlSubPage").value =="")
               {
              
                 alert("Please select SubPage from SubPage drop down.");
                 document.getElementById("ddlSubPage").focus();
                 return false;
              }

             var rb=document.getElementsByName("rb");
             var cnt=0;
               for(var i=0;i<rb.length;i++)
               {
                  if(rb[i].checked==true)
                  {
                     cnt++;
                  }
               }
            // alert(cnt);
             if(cnt<1)
             {   
               alert("Please select ad.");         
               return false;           
             }
             else
             {
               return true; 
             }
         } 

         
//    function test(src)
//    {
//        var emailReg = "^[\\w-_\.]*[\\w-_\.]\@[\\w]\.+[\\w]+[\\w]$";
//        var regex = new RegExp(emailReg);
//        return regex.test(src);
//    }
function validEmail(value) 
 {
    //Validating the email field
    var re = /^(([^<>()[\]\\.,;:\s@\"]+(\.[^<>()[\]\\.,;:\s@\"]+)*)|(\".+\"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/
    if (value.match(re)) 
    {
        return (true);
    }
    return(false);
}
      function funInitiateLink_MailSend()
    {
   
//         document.getElementById("lblMsg").innerHTML=""; 
         var SiteId=""; 
         var TemplateCode="";
         var item_name="";
         var url="";
         var t=""
         var HTMLcode="";
         var SiteURL="";
         var SelectedAdId =encodeURIComponent(document.getElementById("hdSelectedAdId").value);
        if(document.getElementById("cbDirectory").checked==true)
         {
           t = 3;
         }
          else
          {
           t = 2;
          }
         var iframe = document.getElementById("txtTemplateCode_ifr");
         var doc = null;
         if (iframe.contentDocument)
         {
            doc = iframe.contentDocument;
         }
         else if (iframe.contentWindow)
         {
                 doc=iframe.contentWindow.document;
         }
         else 
         {
                doc=window.frames[iframe].document;
         }
         if(doc)
         {
	           TemplateCode=encodeURIComponent(doc.getElementById("tinymce").innerHTML);
	            var regEx = /<[^>]*>/g;
	            if(doc.getElementById("tinymce").innerHTML.toString().replace(regEx, "")=="")
	             { 
	               alert('Please enter  Description');
                   doc.getElementById("tinymce").focus();
                   return false;
	             }
         } 
                 if(document.getElementById("rbHTMLCode").checked==true)
                {
                      HTMLcode=document.getElementById("txtHTMLcode").value;
                      if(document.getElementById("hdsite").value =="0" || document.getElementById("hdsite").value=="1")
                      {
                         if(document.getElementById("txtSite").value=="")
                         {
                            alert("Please provide your Site.");
                            document.getElementById("txtSite").focus();
                            return false;
                         }
                        //http5.open("GET","New_AjaxPage.aspx?mode=InitiateLink&SiteName=" + document.getElementById("txtSite").value + "&type="+document.getElementById("rbHTMLCode").value+"&r="+Math.random()); 
                       url="mode=initiatelink_mail&SiteName=" + document.getElementById("txtSite").value + "&type="+document.getElementById("rbHTMLCode").value+"&SelectedAdId="+SelectedAdId+"&HTMLcode="+HTMLcode+"&SubPageId="+document.getElementById("ddlSubPage").value+"&Email="+document.getElementById("txtEmail_HTML").value+"&Reciprocal="+document.getElementById("txtReciprocal_HTML").value+"&From_URL="+document.getElementById("txtFrom_URL_HTML").value+"&PageRank="+document.getElementById("txtPageRank_HTML").value+"&fName="+document.getElementById("txtfName_HTML").value+"&lName="+document.getElementById("txtlName_HTML").value+ "&r="+Math.random()+"&t="+t+"&From="+document.getElementById("txtFrom").value+"&TemplateCode="+TemplateCode+"&To="+document.getElementById("txtTo").value+"&Subject="+document.getElementById("txtTemplateSubject").value; 
                     }
                    else
                    {
                       if(document.getElementById("ddlSite").value.length==0)
                       {
                          alert("Please select Site from Site drop down.");
                          document.getElementById("ddlSite").focus();
                          return false;
                        }
                     //url="mode=initiatelink_mail&SiteId=" + document.getElementById("ddlSite").value + "&type="+document.getElementById("rbDescription").value+"&SelectedAdId="+SelectedAdId+"&HTMLcode="+HTMLcode+"&SubPageId="+document.getElementById("ddlSubPage").value+"&Email="+document.getElementById("txtEmail_Desc").value+"&Reciprocal="+document.getElementById("txtReciprocal_Desc").value+"&From_URL="+document.getElementById("txtfrom_url_Desc").value+"&PageRank="+document.getElementById("txtPageRank_Desc").value+"&fName="+document.getElementById("txtfName_Desc").value+"&lName="+document.getElementById("txtlName_Desc").value+ "&r="+Math.random()+"&t="+t+"&From="+document.getElementById("txtFrom").value+"&TemplateCode="+TemplateCode+"&To="+document.getElementById("txtTo").value+"&Subject="+document.getElementById("txtTemplateSubject").value; 
                       url="mode=initiatelink_mail&SiteId=" + document.getElementById("ddlSite").value + "&type="+document.getElementById("rbHTMLCode").value+"&SelectedAdId="+SelectedAdId+"&HTMLcode="+HTMLcode+"&SubPageId="+document.getElementById("ddlSubPage").value+"&Email="+document.getElementById("txtEmail_HTML").value+"&Reciprocal="+document.getElementById("txtReciprocal_HTML").value+"&From_URL="+document.getElementById("txtFrom_URL_HTML").value+"&PageRank="+document.getElementById("txtPageRank_HTML").value+"&fName="+document.getElementById("txtfName_HTML").value+"&lName="+document.getElementById("txtlName_HTML").value+ "&r="+Math.random()+"&t="+t+"&From="+document.getElementById("txtFrom").value+"&TemplateCode="+TemplateCode+"&To="+document.getElementById("txtTo").value+"&Subject="+document.getElementById("txtTemplateSubject").value; 

                    }
                }
                else if(document.getElementById("rbDescription").checked==true)
                {
                   if(document.getElementById("txtSiteURL").value.toString().indexOf("http://")==-1)
                    {
                       document.getElementById("txtSiteURL").value="http://"+document.getElementById("txtSiteURL").value;
                    }
                    HTMLcode = "<a href=\"" + document.getElementById("txtSiteURL").value + "\" target=\"_blank\">" + document.getElementById("txtUrlTitle").value + "</a><br>" + document.getElementById("txtHTMLCode_Desc").value ;
                    SiteURL="<a href=\"" + document.getElementById("txtSiteURL").value;

                    if(document.getElementById("hdsite").value =="0" || document.getElementById("hdsite").value=="1")
                      {
                         if(document.getElementById("txtSite").value=="")
                         {
                            alert("Please provide your Site.");
                            document.getElementById("txtSite").focus();
                            return false;
                         }
                       // http5.open("GET","New_AjaxPage.aspx?mode=initiatelink_mail&SiteName=" + document.getElementById("txtSite").value +"&type="+document.getElementById("rbDescription").value + "&r="+Math.random()); 
                       url="mode=initiatelink_mail&SiteName=" + document.getElementById("txtSite").value + "&type="+document.getElementById("rbDescription").value+"&SelectedAdId="+SelectedAdId+"&HTMLcode="+HTMLcode+"&SubPageId="+document.getElementById("ddlSubPage").value+"&Email="+document.getElementById("txtEmail_Desc").value+"&Reciprocal="+document.getElementById("txtReciprocal_Desc").value+"&From_URL="+document.getElementById("txtfrom_url_Desc").value+"&PageRank="+document.getElementById("txtPageRank_Desc").value+"&fName="+document.getElementById("txtfName_Desc").value+"&lName="+document.getElementById("txtlName_Desc").value+ "&r="+Math.random()+"&t="+t+"&From="+document.getElementById("txtFrom").value+"&TemplateCode="+TemplateCode+"&To="+document.getElementById("txtTo").value+"&Subject="+document.getElementById("txtTemplateSubject").value+"&SiteURL="+SiteURL; 
                     }
                    else
                    {
                      // alert(document.getElementById("<%=ddlSite.ClientID%>").value.length);
                       if(document.getElementById("ddlSite").value.length==0)
                       {
                          alert("Please select Site from Site drop down.");
                          document.getElementById("ddlSite").focus();
                          return false;
                        }
                      //http5.open("GET","New_AjaxPage.aspx?mode=initiatelink_mail&SiteId=" + document.getElementById("ddlSite").value+"&type="+document.getElementById("rbDescription").value + "&r="+Math.random()); 
                      url="mode=initiatelink_mail&SiteId=" + document.getElementById("ddlSite").value + "&type="+document.getElementById("rbDescription").value+"&SelectedAdId="+SelectedAdId+"&HTMLcode="+HTMLcode+"&SubPageId="+document.getElementById("ddlSubPage").value+"&Email="+document.getElementById("txtEmail_Desc").value+"&Reciprocal="+document.getElementById("txtReciprocal_Desc").value+"&From_URL="+document.getElementById("txtfrom_url_Desc").value+"&PageRank="+document.getElementById("txtPageRank_Desc").value+"&fName="+document.getElementById("txtfName_Desc").value+"&lName="+document.getElementById("txtlName_Desc").value+ "&r="+Math.random()+"&t="+t+"&From="+document.getElementById("txtFrom").value+"&TemplateCode="+TemplateCode+"&To="+document.getElementById("txtTo").value+"&Subject="+document.getElementById("txtTemplateSubject").value+"&SiteURL="+SiteURL; 
                    }
                }
                else
                {
                   alert("Please select How do you want to insert Data:");
                   return false;
                }
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
        self.xmlHttpReq.open('POST', "New_AjaxPage.aspx", true);             
        self.xmlHttpReq.setRequestHeader('Content-Type', 'application/x-www-form-urlencoded');
        self.xmlHttpReq.onreadystatechange = function() 
        {
            if (self.xmlHttpReq.readyState == 4) 
            {  
                var GetValue=new Array();
                if(self.xmlHttpReq.responseText.indexOf('|' != -1))
                {
                    GetValue=self.xmlHttpReq.responseText.split("|");
                }
                document.getElementById("spMailAjax").style.display="none";
                if(GetValue[0].toString()!="")
                {
                    if(GetValue[0].toString()=="1")
                    {
                         document.getElementById("dvMail").style.display="block"
                         document.getElementById("lblMsg").innerHTML="";
                         alert(GetValue[1].toString());
                         document.getElementById("lblMsg").innerHTML=GetValue[1].toString();
                    }
                    else
                    {                    
                         document.getElementById("dvMail").style.display="none"
                         if(document.getElementById("rbHTMLCode").checked==true)
                         {
                             document.getElementById("btnHTMLCode").style.display="block"
                         }
                         else
                         {
                            document.getElementById("btnSubmit_Desc").style.display="block"
                         }
                         document.getElementById("lblMsg").innerHTML="";
                         document.getElementById("lblMsg").innerHTML=GetValue[1].toString(); 
                                                       
                    }
                }
                else
                {
                    alert("Ajax return not found...");
                }
            }
            else
            {
                document.getElementById("lblMsg").innerHTML="";
                document.getElementById("spMailAjax").style.display="block";
                document.getElementById("dvMail").style.display="none"
               
            }
        }                
        //self.xmlHttpReq.send("mode=initiatelink_mail&SiteId="+SiteId+"&TemplateCode="+encodeURIComponent(TemplateCode)+"&item_name="+item_name+"&r="+Math.random()); 
        self.xmlHttpReq.send(url); 
    }
    
   