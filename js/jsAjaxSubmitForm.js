// Load billing states on selecting the country dropdown.
function loadBillingState(ddBillCountry)
{   
    var objErrLabel=document.getElementById("lblError");
    var objBilling=document.getElementById("dvBillingState");
    var hdnBillCountryName=document.getElementById("hdnBillCountryName");
    if(ddBillCountry!=null && objErrLabel!=null && objBilling!=null && hdnBillCountryName!=null)
    {    
        var val=ddBillCountry.value;
        //alert(val);
        var ddIndex=ddBillCountry.selectedIndex;
//        hdnBillCountryName.value=ddBillCountry.options[ddIndex].text;
         hdnBillCountryName.value=ddBillCountry.options[ddIndex];
        if(val!=null && val!="0")
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
            self.xmlHttpReq.open('POST', "ajaxSubmitForm.aspx", true);             
            self.xmlHttpReq.setRequestHeader('Content-Type', 'application/x-www-form-urlencoded');
            self.xmlHttpReq.onreadystatechange = function() 
            {
                if (self.xmlHttpReq.readyState == 4) 
                {          
                    document.getElementById("dvAjaxPic").style.display="none";
                    objBilling.style.display="block";
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
                            objBilling.innerHTML="";  
                        }
                        else
                        {
                           // objBilling.innerHTML=GetValue[1].toString();
                            LoadResponse(GetValue[1].toString(),"dvBillingState")
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
                    objBilling.style.display="none";
                }
            }                
            self.xmlHttpReq.send("mode=1&countryId="+val); 
        }
        else if(val=="0")
        {            
            objBilling.innerHTML="";
            objErrLabel.innerHTML="";
            alert("Select proper country.");
        }
        else
        {            
            objBilling.innerHTML="";
            objErrLabel.innerHTML="";
            alert("Invalid parameter. Try again.");
        }
    }
    else
    {
        alert("Objects not found. Try again.");
    }
}

// Load the shipping city from selected state dropdown
function loadShippingCity(ddShipState)
{   
    var objErrLabel=document.getElementById("lblError");
    var objShipping=document.getElementById("divShippingCity");
    var hdnShipStateName=document.getElementById("hdnShipStateName");
//    alert(ddShipState.value);
//    alert(objShipping + " : " + objShipping.value);
    
    if(ddShipState!=null && objErrLabel!=null && objShipping!=null && hdnShipStateName!=null)
    {    
        var val=ddShipState.value;
        var ddIndex=ddShipState.selectedIndex;
//        hdnShipStateName.value=ddShipState.options[ddIndex].text;
        hdnShipStateName.value=ddShipState.options[ddIndex];

        if(val!=null && val!="0")
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
            self.xmlHttpReq.open('POST', "ajaxSubmitForm.aspx", true);             
            self.xmlHttpReq.setRequestHeader('Content-Type', 'application/x-www-form-urlencoded');
            self.xmlHttpReq.onreadystatechange = function() 
            {
                if (self.xmlHttpReq.readyState == 4) 
                {          
                    document.getElementById("dvAjaxPic").style.display="none";
                    objShipping.style.display="block";
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
                            objShipping.innerHTML="";  
                        }
                        else
                        {
                          // objShipping.innerHTML=GetValue[1].toString();
                             // LoadResponse(GetValue[1].toString(),"objShipping")
                            LoadResponse(GetValue[1].toString(),"divShippingCity")
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
                    objShipping.style.display="none";
                }
            }                
            self.xmlHttpReq.send("mode=2&stateId="+val); 
        }
        else if(val=="0")
        {            
            objShipping.innerHTML="";
            objErrLabel.innerHTML="";
            alert("Select proper state.");
        }
        else
        {            
            objShipping.innerHTML="";
            objErrLabel.innerHTML="";
            alert("Invalid parameter. Try again.");
        }
    }
    else
    {
        alert("Objects not found. Try again.");
    }
}

function LoadResponse(response,control)
  {
     var container=document.getElementById(control);
     try
       {
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
 


