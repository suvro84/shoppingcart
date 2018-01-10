function deleteFromCart(recId, prodId)
{   
    var objErrLabel=document.getElementById("lblError");
    var objMainDiv=document.getElementById("dvCart");
    //alert(objErrLabel + " : " + objMainDiv);
    
    if(objErrLabel!=null && objMainDiv!=null)
    {    
        if(recId!=null && prodId!=null)
        {
            //alert(recId + " : " + prodId);
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
            self.xmlHttpReq.open('POST', "ajaxCartFunction.aspx", true);             
            self.xmlHttpReq.setRequestHeader('Content-Type', 'application/x-www-form-urlencoded');
            self.xmlHttpReq.onreadystatechange = function() 
            {
                if (self.xmlHttpReq.readyState == 4) 
                {          
                    document.getElementById("dvAjaxPic").style.display="none";
                    objMainDiv.style.display="block";
                    var GetValue=new Array();
                    if(self.xmlHttpReq.responseText.indexOf('~' != -1))
                    {
                        GetValue=self.xmlHttpReq.responseText.split("~");
                    }
                    if(GetValue[0].toString()!="")
                    {
                        if(GetValue[0].toString()=="0")
                        {
                            objErrLabel.innerHTML=GetValue[1].toString();                         
                            objMainDiv.innerHTML="<ul><li><a href=\"index.aspx\"><img class=\"shoppingBtn\" src=\"images/continue_shopping_btn.gif\" /></a></li></ul>";  
                        }
                        else
                        {
                            objMainDiv.innerHTML=GetValue[1].toString();;
                            objErrLabel.innerHTML=""; 
                        }
                    }
                    else
                    {
                        alert("Ajax return not found...");
                    }
                }
                else
                {
                    document.getElementById("dvAjaxPic").style.display="block";
                    objMainDiv.style.display="none";
                }
            }                
            self.xmlHttpReq.send("mode=2&recId="+recId + "&prodId="+prodId); 
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

//delet all form cart

function deleteAllFromCart()
{   
    var objErrLabel=document.getElementById("lblError");
    var objMainDiv=document.getElementById("dvCart");
    //alert("error label: " + objErrLabel + " : main div: " + objMainDiv);    
    if(objErrLabel!=null && objMainDiv!=null)
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
        self.xmlHttpReq.open('POST', "ajaxCartFunction.aspx", true);             
        self.xmlHttpReq.setRequestHeader('Content-Type', 'application/x-www-form-urlencoded');
        self.xmlHttpReq.onreadystatechange = function() 
        {
            if (self.xmlHttpReq.readyState == 4) 
            {          
                document.getElementById("dvAjaxPic").style.display="none";
                objMainDiv.style.display="block";
                var GetValue=new Array();
                if(self.xmlHttpReq.responseText.indexOf('~' != -1))
                {
                    GetValue=self.xmlHttpReq.responseText.split("~");
                }
                if(GetValue[0].toString()!="")
                {
                    if(GetValue[0].toString()=="0")
                    {
                        objErrLabel.innerHTML=GetValue[1].toString();                         
                        objMainDiv.innerHTML="<ul><li><a href=\"index.aspx\"><img class=\"shoppingBtn\" src=\"images/continue_shopping_btn.gif\" /></a></li></ul>";  
                    }
                    else
                    {
                        objMainDiv.innerHTML=GetValue[1].toString();;
                        objErrLabel.innerHTML=""; 
                    }
                }
                else
                {
                    alert("Ajax return not found...");
                }
            }
            else
            {
                document.getElementById("dvAjaxPic").style.display="block";
                objMainDiv.style.display="none";
            }
        }
            self.xmlHttpReq.send("mode=4"); 
    }
    else
    {
        alert("Objects not found. Try again.");
    }

}



function updateCart(mode, txtBoxIds)
{
    //alert("got it: " + mode );
    var objErrLabel=document.getElementById("lblError");
    var objMainDiv=document.getElementById("dvCart");
    //alert(mode + " : " + txtBoxIds);
    
    if(objErrLabel!=null && objMainDiv!=null)
    {   
        var varProdId=new Array();
        varProdId=txtBoxIds.split("|");
        //alert(varProdId);
        var varTxtBoxProdId="";   
        for(i=0;i<varProdId.length;i++)
        {
            if(varProdId[i].toString()!="")
            {                
                if(document.getElementById(varProdId[i].toString()))
                {
                    //alert(varTxtBoxProdId);
                    varTxtBoxProdId=varTxtBoxProdId + varProdId[i].toString() + "~" + document.getElementById(varProdId[i].toString()).value + "|";
                    //alert(varTxtBoxProdId);
                }
            }
        }
        
        if(varTxtBoxProdId!=null)
        {
            //alert(varTxtBoxProdId);
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
            self.xmlHttpReq.open('POST', "ajaxCartFunction.aspx", true);             
            self.xmlHttpReq.setRequestHeader('Content-Type', 'application/x-www-form-urlencoded');
            self.xmlHttpReq.onreadystatechange = function() 
            {
                if (self.xmlHttpReq.readyState == 4) 
                {          
                    document.getElementById("dvAjaxPic").style.display="none";
                    objMainDiv.style.display="block";
                    var GetValue=new Array();
                    if(self.xmlHttpReq.responseText.indexOf('~' != -1))
                    {
                        GetValue=self.xmlHttpReq.responseText.split("~");
                    }
                    if(GetValue[0].toString()!="")
                    {
                        if(GetValue[0].toString()=="0")
                        {
                            objErrLabel.innerHTML=GetValue[1].toString();                         
                            objMainDiv.innerHTML="";  
                        }
                        else
                        {
                            objMainDiv.innerHTML=GetValue[1].toString();;
                            objErrLabel.innerHTML=""; 
                        }
                    }
                    else
                    {
                        alert("Ajax return not found...");
                    }
                }
                else
                {
                    document.getElementById("dvAjaxPic").style.display="block";
                    objMainDiv.style.display="none";
                }
            }                
            self.xmlHttpReq.send("mode=" + mode + "&strBoxIds="+varTxtBoxProdId);
        }
    }
    else
    {
        alert("Objects not found. Try again.");
    }
}
function updateCartForDiscount(mode, siteId, objName)
{
    var objDisc=document.getElementById(objName);
    var objErrLabel=document.getElementById("lblError");
    var objMainDiv=document.getElementById("dvCart");
    //alert(mode + " : " + mode + " siteid:" + siteId + " error div: " + objErrLabel + " main div: " + objMainDiv);
    if(objErrLabel!=null && objMainDiv!=null && objDisc!=null)
    {
        //alert("got it: " + mode + " object: " + objDisc + " value: " + objDisc.value);
        var varDiscCode=objDisc.value;
        objErrLabel.innerHTML="";
        if(varDiscCode!=null && varDiscCode!="")
        {
            var varDiscCode=objDisc.value;
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
            self.xmlHttpReq.open('POST', "ajaxCartFunction.aspx", true);             
            self.xmlHttpReq.setRequestHeader('Content-Type', 'application/x-www-form-urlencoded');
            self.xmlHttpReq.onreadystatechange = function() 
            {
                if (self.xmlHttpReq.readyState == 4) 
                {          
                    document.getElementById("dvAjaxPic").style.display="none";
                    objMainDiv.style.display="block";
                    var GetValue=new Array();
                    if(self.xmlHttpReq.responseText.indexOf('~' != -1))
                    {
                        GetValue=self.xmlHttpReq.responseText.split("~");
                    }
                    if(GetValue[0].toString()!="")
                    {
                        if(GetValue[0].toString()=="0")
                        {
                            objErrLabel.innerHTML=GetValue[1].toString();                         
                            objMainDiv.innerHTML="";  
                            alert(GetValue[1].toString()); 
                        }
                        if(GetValue[0].toString()=="2")
                        {
                            objErrLabel.innerHTML=GetValue[1].toString();                         
                            objMainDiv.innerHTML=GetValue[2].toString();  
                            alert(GetValue[1].toString()); 
                        }
                        else
                        {
                            objMainDiv.innerHTML=GetValue[1].toString();;
                            objErrLabel.innerHTML=""; 
                        }
                    }
                    else
                    {
                        alert("Ajax return not found...");
                    }
                }
                else
                {
                    document.getElementById("dvAjaxPic").style.display="block";
                    objMainDiv.style.display="none";                    
                }
            }                
            //self.xmlHttpReq.send("mode=" + mode + "&discCode="+varDiscCode);
            self.xmlHttpReq.send("mode=" + mode + "&siteId=" + siteId + "&discCode="+varDiscCode);
        }
        else
        {
            alert("Please enter your discount code. Then try again.");
        }
    }
    else
    {
        alert("Object not found!");
    }
}

function chkNumeric(obj)
{
    if(obj)
    {
        var varVal=obj.value;
        if(isNaN(varVal)||(varVal< 1))
        {
            alert("Quantity Must be at Least one.");
            obj.value=1;
        }
    }
    else
    {
        alert("Object not found!");
    }
}


function NumericCheck(strString)
//  check for valid numeric strings	
{
   var strValidChars = "0123456789";
   var strChar;
   var blnResult = true;

   if (strString.length == 0) return false;

   //  test strString consists of valid characters listed above
   for (i = 0; i < strString.length && blnResult == true; i++)
      {
      strChar = strString.charAt(i);
      if (strValidChars.indexOf(strChar) == -1)
         {
         blnResult = false;
         }
      }
   return blnResult;
}


function IsNumeric(e)
{
    // Calling procedure
    // onKeyPress=\"javascript:return IsNumeric(event);\" 
    //
    //alert(e);
	var KeyID = (window.event) ? event.keyCode : e.which;
	if((KeyID >= 65 && KeyID <= 90) || (KeyID >= 97 && KeyID <= 122) || (KeyID >= 33 && KeyID <= 47) ||
	   (KeyID >= 58 && KeyID <= 64) || (KeyID >= 91 && KeyID <= 96) || (KeyID >= 123 && KeyID <= 126))
	{
		return false;
	}
    return true;
}