// JScript File

    function deletecart(recid,item_id)
    {
         //   alert("recid:"+recid+"item_id:"+item_id);
           if(recid!=null && item_id!=null)
            {
//             loadPage(recid,item_id);

   var xmlHttpReq = false;
        var self = this;
        var selectedids = '';

        if (window.XMLHttpRequest) 
        {
            self.xmlHttpReq = new XMLHttpRequest();        
        }
        else if (window.ActiveXObject) 
        {
            self.xmlHttpReq = new ActiveXObject("Microsoft.XMLHTTP");         
        }
        self.xmlHttpReq.open('POST', "ajaxupdate.aspx", true);             
        self.xmlHttpReq.setRequestHeader('Content-Type', 'application/x-www-form-urlencoded');
        self.xmlHttpReq.onreadystatechange = function() 
        {
            if(self.xmlHttpReq.readyState == 1)
            {
//                LoadResponse("Loading...", "objMainDiv");
                 
                  document.getElementById("dvAjaxPic").style.display="block";
                  document.getElementById("objMainDiv").style.display="none";
                 
              
            }
            if (self.xmlHttpReq.readyState == 4) 
            {    
              
              
               var GetValue=new Array();
                    if(self.xmlHttpReq.responseText.indexOf('~' != -1))
                    {
                        GetValue=self.xmlHttpReq.responseText.split("~");
                    }
                    if(GetValue[0].toString()!="")
                    {
                        //alert(GetValue[0].toString());
                        if(GetValue[0].toString()=="2")
                        {
                      //  objErrLabel.innerHTML=GetValue[1].toString();                         
                        objMainDiv.innerHTML="<ul><li><a href=\"index.aspx\"><img class=\"shoppingBtn\" src=\"images/continue_shopping_btn.gif\" /></a></li></ul>";  
                          document.getElementById("dvMaindiscount").style.display="none"
                        }
                        else
                        {
//                           alert(GetValue[1].toString());
//                            objMainDiv.innerHTML=GetValue[1].toString();
//                            objErrLabel.innerHTML=""; 
                               document.getElementById("dvAjaxPic").style.display="none";
                               document.getElementById("objMainDiv").style.display="block";
//                               LoadResponse(self.xmlHttpReq.responseText,"objMainDiv"); 
                               LoadResponse(GetValue[1].toString(),"objMainDiv"); 
                               document.getElementById("dvMaindiscount").style.display="none"
                                if(document.getElementById("GridView1")==null)
             {
              // alert(document.getElementById("GridView1"));
               document.getElementById("objMainDiv").innerHTML="<a href=\"default.aspx\"><img class=\"shoppingBtn\" src=\"images/continue-shop.gif\" /></a></li></ul>";  
               document.getElementById("dvMaindiscount").style.display="none"  
               document.getElementById("dvbuttons").style.display="none"  

               
             }
                              
                              

                        }
                    }
              
                
               
            }
        }                
//        self.xmlHttpReq.send("mode=" + mode + "&strBoxIds=" + selectedids);
          self.xmlHttpReq.send("mode=2&recId="+recid + "&item_id="+item_id); 


                
            }
    
    }
    
    function loadPage(recid,item_id)
    {
        alert("recid:"+recid+"item_id:"+item_id);
        var xmlHttpReq = false;
        var self = this;
        var selectedids = '';

        if (window.XMLHttpRequest) 
        {
            self.xmlHttpReq = new XMLHttpRequest();        
        }
        else if (window.ActiveXObject) 
        {
            self.xmlHttpReq = new ActiveXObject("Microsoft.XMLHTTP");         
        }
        self.xmlHttpReq.open('POST', "frmAjaxPage_commonFunctions.aspx", true);             
        self.xmlHttpReq.setRequestHeader('Content-Type', 'application/x-www-form-urlencoded');
        self.xmlHttpReq.onreadystatechange = function() 
        {
            if(self.xmlHttpReq.readyState == 1)
            {
                LoadResponse("Loading...", "dvMaindiv");
            }
            if (self.xmlHttpReq.readyState == 4) 
            {    
              //  alert(self.xmlHttpReq.responseText);
           //  document.getElementById("objMainDiv").style.display="none";
              //  document.getElementById("dvupdate").style.display="none";
              // document.getElementById("objMainDiv").removeChild(document.getElementById("objMainDiv"))"; 
              // document.getElementById("objMainDiv").innerHTML="";
               LoadResponse(self.xmlHttpReq.responseText,"dvMaindiv"); 
               
               //LoadResponse(self.xmlHttpReq.responseText,"objMainDiv");  
              
            }
        }                
//        self.xmlHttpReq.send("mode=" + mode + "&strBoxIds=" + selectedids);
          self.xmlHttpReq.send("mode=2&recId="+recid + "&item_id="+item_id); 
    }
    function LoadResponse(response,control)
     {
     var container=document.getElementById(control);
//      document.getElementById("objMainDiv").style.display="none";
  alert(container);
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



function updateCartForDiscount(mode, siteId)
{
    var objDisc=document.getElementById("txtDiscount");
    var objErrLabel=document.getElementById("dvAjaxPic");
    var objMainDiv=document.getElementById("objMainDiv");
//  var  dvMaindiscount=document.getElementById("dvMaindiscount");
  //  alert(mode + " : " + mode + " siteid:" + siteId + " error div: " + objErrLabel + " main div: " + objMainDiv);
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
            self.xmlHttpReq.open('POST', "ajaxupdate.aspx", true);             
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
                //alert(GetValue[0].toString());
                   // alert(GetValue[1].toString());
                     // alert(GetValue[2].toString());
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
                             document.getElementById("dvMaindiscount").style.display="none"
                        }
                        else
                        {
                            //objMainDiv.innerHTML=GetValue[1].toString();
                          //  alert(GetValue[1].toString());
//                             document.getElementById("dvMaindiscount").innerHTML="";
//                           document.getElementById("dvMaindiscount").innerHTML=GetValue[1].toString();
                             LoadResponse(GetValue[1].toString(),"objMainDiv");
                              document.getElementById("dvMaindiscount").style.display="none"
                           // document.getElementById("dvMaindiscount").style.display="none"; 
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
       