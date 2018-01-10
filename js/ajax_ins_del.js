// JScript File

 function ins()
 {
  var name=document.getElementById("txtname").value;
   var address=document.getElementById("txtaddress").value;
 var spnbuy=document.getElementById("spnBuyBtn");
    var spnload=document.getElementById("spnload");
    //alert("btn: " + spnbuy + " ajax img: " + spnload);
    var xmlHttpReq_findfile = false;
    var self_findfile = this;
    if (window.XMLHttpRequest) 
    {
        self_findfile.xmlHttpReq_findfile = new XMLHttpRequest();
    }
    else if (window.ActiveXObject) 
    {
        self_findfile.xmlHttpReq_findfile = new ActiveXObject("Microsoft.XMLHTTP");
    }
   self_findfile.xmlHttpReq_findfile.open("POST", "frmAjaxCart.aspx", true);
   //  self_findfile.xmlHttpReq_findfile.open("POST", "edit_reg.aspx", true);
    
    self_findfile.xmlHttpReq_findfile.setRequestHeader("Content-Type", "application/x-www-form-urlencoded");
    self_findfile.xmlHttpReq_findfile.onreadystatechange = function() 
    {
        if (self_findfile.xmlHttpReq_findfile.readyState == 4) 
        {    
            var ret= self_findfile.xmlHttpReq_findfile.responseText;
//            var t=ret.substring(0,ret.indexOf(" ");
             
           var GetValue=new Array();
           GetValue=ret.split('');
//         alert(GetValue[0]);
      
            if(GetValue[0]=="t")
                          {
                         spnbuy.style.display="block";
            spnload.style.display="none";
//            alert('data inserted successfully');
//            LoadResponse(ret,"MainPlaceHolder");
}
            else
            {
                alert("Some problem in adding cart!!plz try again.....");
            }
        }
        if (self_findfile.xmlHttpReq_findfile.readyState < 4) 
        {     
             spnbuy.style.display="none";
             spnload.style.display="block";
        }
    }
    
    self_findfile.xmlHttpReq_findfile.send("name="+ name +"&address="+ address); 
//self_findfile.xmlHttpReq_findfile.send("proId="+ proID +"&catId="+ catId +"&mode="+Mode+"&randN="+Math.random()); 
    flag=true;
 
 
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
//       document.getElementById("dvloadGV").style.display="none";
    }
    catch(ex)
    {
        container.innerHTML=response;
    }
 }   
 
 
 
 function deleteFromCart(recId, prodId)
{   
    var objErrLabel=document.getElementById("lblError");
    var objMainDiv=document.getElementById("dvCart");
//    alert(objErrLabel + " : " + objMainDiv);
    
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



// JScript File

function del(id)
 {
// var objErrLabel=document.getElementById("lblError");
    var objMainDiv=document.getElementById("objMainDiv");
    //alert(objErrLabel + " : " + objMainDiv);
//    alert(id);
     
        if(id!=null)
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
            self.xmlHttpReq.open('POST', "from_ajax_delete.aspx", true);             
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
//                            objErrLabel.innerHTML=GetValue[1].toString();                         
                            objMainDiv.innerHTML="<ul><li><a href=\"index.aspx\"><img class=\"shoppingBtn\" src=\"images/continue_shopping_btn.gif\" /></a></li></ul>";  
                        }
                        else
                        {
//                              alert(GetValue[1]);
//                      alert(GetValue[0]);


 document.getElementById("objMainDiv").style.display="block";
                            objMainDiv.innerHTML=GetValue[1].toString();
//                            objErrLabel.innerHTML=""; 
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
//            self.xmlHttpReq.send("mode=2&recId="+recId + "&prodId="+prodId); 
// alert(id);
 self.xmlHttpReq.send("mode=2&id="+id); 
//  self.xmlHttpReq.send("id="+id); 

        }
        else
        {
            alert("Invalid parameter. Try again.");
        }
    
   

 
 
 }


