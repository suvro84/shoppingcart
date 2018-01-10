
function showPODesc(radVal, objName)
{
    var varPoId=radVal.value;
    var dvDesc=document.getElementById(objName);
    var varAjaxPic=document.getElementById("dvAjaxPic");
    var varHdnPoId=document.getElementById("hdnPoId");
	if(dvDesc!=null && varAjaxPic!=null && varHdnPoId!=null)
	{
	     varHdnPoId.value=varPoId;
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
         self.xmlHttpReq.open('POST', "ajaxPaymentOption.aspx", true);
         self.xmlHttpReq.setRequestHeader('Content-Type', 'application/x-www-form-urlencoded');
         self.xmlHttpReq.onreadystatechange = function() 
         {        
            if (self.xmlHttpReq.readyState == 4) 
            {
                varAjaxPic.style.display="none";
                dvDesc.style.display="block";
                var GetValue=new Array();
                if(self.xmlHttpReq.responseText.indexOf('~' != -1))
                {
                    GetValue=self.xmlHttpReq.responseText.split("~");
                }
                if(GetValue[0].toString()!="")
                {
                    dvDesc.innerHTML=GetValue[1].toString();                    
                }
                else
                {
                    alert("Ajax return not found...");
                }                
            }
            else
            {
                dvDesc.style.display="none";
                varAjaxPic.style.display="block";
            }
         }
        self.xmlHttpReq.send("Mode=1&poId=" + varPoId); 
	}
}

function setPoPgId(radVal, objName)
{
    var varPoId=radVal.value;
    var varHdnPoId=document.getElementById("hdnPoId");
    if(varHdnPoId!=null)
	{
	     varHdnPoId.value=varPoId;	     
	}
	else
    {
        alert("Object not found.");
    }
    
}
function setPoPgIdNew(PoId, PgId)
{
    var varHdnPoId=document.getElementById("hdnPoId");
    var varHdnPgId=document.getElementById("hdnPgId");
    alert("value: " + PoId + " " + PgId + " Po: " + varHdnPoId + " Pg: " + varHdnPgId);
    if(varHdnPoId!=null && varHdnPgId!=null)
	{
	     varHdnPoId.value=PoId;	    
	     varHdnPgId.value = PgId;
	}
	else
    {
        alert("Please try later. Object not found.");
    }
    //alert("po: " + varHdnPoId.value + " Pg: " + varHdnPgId.value);
}


function setPoId_PoName(PoId,PoName)
{
    var varHdnPoId=document.getElementById("hdnPoId");
    var varHdnPoName=document.getElementById("hdnPoName");

   // alert(PoId);
   // alert(PoName);

    if(varHdnPoId!=null && varHdnPoName!=null)
	{
	     varHdnPoId.value=PoId;	 
	     varHdnPoName.value=PoName; 
	    
	}
	else
    {
        alert("Please try later. Object not found.");
    }
    //alert("po: " + varHdnPoId.value + " Pg: " + varHdnPgId.value);
}

function validateForm(formName)
{
	//alert('hi');
	var theForm=document.getElementById(formName);
	var varHdnPoId=document.getElementById("hdnPoId");
	var z = 0; 
	if(theForm!=null)
	{
	    var cnt=0;
	    for(z=0; z<theForm.length;z++)
        {            
            if(theForm[z].type == 'radio')
            {
                if(theForm[z].checked ==true)
                {
                    cnt++;
                }
            }
        }
        if(cnt<1)
        {
            alert("Please select a payment option then try again.");
            return false;
        }
        else
        {
            //return true;
            alert(varHdnPoId.value);
            return false;
        }
    }
    else
    {
        alert("Object not found.");
    }
    
}

function validate(oSrc, args)
{
	//alert('ho');
	var theForm=document.getElementById("frmPo");
	var z = 0; 
	var cnt=0;
	if(theForm!=null)
	{	    
	    cnt=0;
	    for(z=0; z<theForm.length;z++)
        {            
            if(theForm[z].type == 'radio')
            {
                if(theForm[z].checked ==true)
                {
                    cnt++;
                }
            }
        }
        if(cnt<1)
        {   
            alert("Please select a payment option then try again.");         
            args.IsValid=false;            
        }
        else
        {
            args.IsValid=true;
        }
    }
    else
    {
        alert("Object not found.");
        args.IsValid=false;        
    }    
}

function sendWaitMail(pgName, ajaxDivName)
{
    var dvAjax=document.getElementById(ajaxDivName);
    if(pgName!=null && dvAjax!=null)
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
         self.xmlHttpReq.open('POST', "ajaxPaymentOption.aspx", true);
         self.xmlHttpReq.setRequestHeader('Content-Type', 'application/x-www-form-urlencoded');
         self.xmlHttpReq.onreadystatechange = function() 
         {        
            if (self.xmlHttpReq.readyState == 4) 
            {
                dvAjax.style.display="none";                
                var GetValue=new Array();
                if(self.xmlHttpReq.responseText.indexOf('~' != -1))
                {
                    GetValue=self.xmlHttpReq.responseText.split("~");
                }
                if(GetValue[0].toString()!="")
                {
                    if(GetValue[0].toString()=="1")
                    {
                        return true;
                    }
                    else if(GetValue[0].toString()=="0")
                    {
                        alert(GetValue[1].toString());     
                        return false;                        
                    }
                    else
                    {
                        alert(GetValue[1].toString());
                        return false;                    
                    }
                }
                else
                {
                    alert("Ajax return not found...");
                    return false;               
                }                
            }
            else
            {
                dvAjax.style.display="block";
                return false;
            }
         }
        self.xmlHttpReq.send("Mode=2&bankName=" + pgName);        
    }
    else
    {
        alert("Bank name can't be determined.");
        return false;
    }
}