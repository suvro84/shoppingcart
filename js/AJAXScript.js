// JScript File

var xmlHttp;
function ajaxFunction()
{
    //var xmlHttp;
    try
    {
        xmlHttp=new XMLHttpRequest();
    }
    catch(e)
    {
        //IE
        try
        {
            xmlHttp=new ActiveXObject("Msxml2.XMLHTTP");
        }
        catch(e)
        {
            try
            {
                xmlHttp=new ActiveXObject("Microsoft.XMLHTTP");
            }
            catch(e)
            {
                alert("Your browser doesnt support AJAX");
                return null;
            }
        }
    }
    xmlHttp.onreadystatechange=statechange;
    return xmlHttp;
}
function loadAll()
{
    var xmlHttpobj=ajaxFunction();
    if(xmlHttpobj != null)
    {
        xmlHttpobj.open("GET","NewPage.aspx?name=",true);
        xmlHttpobj.send(null);
    }
}
function getLoading()
{
    return "<div style='width:100%;height:300px;padding-top:150px'><center><img src='images/pleasewait.gif'/><br><font color='red'><b>Loading....</b></font></center></div>";
}
function statechange()
{
    if(xmlHttp.readyState==2)
    {
    
        document.getElementById("MainPlaceHolder").innerHTML="Loading...........";
//        LoadResponse(getLoading(),"MainPlaceHolder");
    }
    else if(xmlHttp.readyState==4)
    {
    //alert(xmlHttp.responseXML);
//    if(xmlHttp.status==200)
    LoadResponse(xmlHttp.responseText,"MainPlaceHolder");
//    else
//        alert("Error Occurred while Fetching Last Request. ErrorCode:" + xmlHttp.status);
        //document.getElementById("MainPlaceHolder").innerHTML=xmlHttp.responseText;
    }
    
}
function loadData()
{
    var xmlHttpobj=ajaxFunction();
    
    if(xmlHttpobj != null)
    {
        try{
        var params = "name=" + document.getElementById("ddllist").value;
        
//        xmlHttpobj.setRequestHeader("Content-type","application/x-www-form-urlencoded");
//        alert("1");
//        xmlHttpobj.setRequestHeader("Content-length",params.length);
//        alert("2");
//        xmlHttpobj.setRequestHeader("Connection","close");
        
        //xmlHttpobj.open("POST","NewPage.aspx",true);
        xmlHttpobj.open("GET","NewPage.aspx?name=" + document.getElementById("ddllist").value,true);
        xmlHttpobj.send(null);
        }
        catch(e)
        {
            alert(e.toString());
        }
    }
 }
 
 function loaddetail()
 {


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
//    self_findfile.xmlHttpReq_findfile.open("POST", "frmAjaxCart.aspx", true);
     self_findfile.xmlHttpReq_findfile.open("POST", "edit_reg.aspx", true);
     
     self_findfile.xmlHttpReq_findfile.setRequestHeader("Content-Type", "application/x-www-form-urlencoded");
    self_findfile.xmlHttpReq_findfile.onreadystatechange = function() 
    {
     if (self_findfile.xmlHttpReq_findfile.readyState == 4) 
        {    
           
            LoadResponse(self_findfile.xmlHttpReq_findfile.responseText,"MainPlaceHolder");
           
//           else
//            {
//                alert("Some problem in adding cart!!plz try again.....");
//            }
        }
        if (self_findfile.xmlHttpReq_findfile.readyState < 4) 
        {     
//             spnbuy.style.display="none";
//             spnload.style.display="block";
document.getElementById("reg").style.display="none";
document.getElementById("MainPlaceHolder").innerHTML="Loading...........";

        }
    }
    
    
    
    
//     self_findfile.send(null);
//  alert(xmlHttpobj);
  
  
  
  
  
    var xmlHttpobj=ajaxFunction();

    if(xmlHttpobj != null)
    {
//         alert(xmlHttpobj);
//document.getElemntById("reg").style.visibility = "hidden";
document.getElementById("reg").style.display="none";
       xmlHttpobj.open("POST","edit_reg.aspx",true);
    xmlHttpobj.setRequestHeader("Content-Type", "application/x-www-form-urlencoded");

//        xmlHttpobj.open("GET","edit_reg.aspx",true);
        xmlHttpobj.send(null);
    }
 }
 
 
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
         alert(GetValue[0]);
//                    if(self.xmlHttpReq.responseText.indexOf(' ' != -1))
//                    {
//                        GetValue=self.xmlHttpReq.responseText.split("");
//                    }  
//              if(GetValue[0].toString()!="")
//                    {
//                        if(GetValue[0].toString()=="t")
//                        {
                        
                        
                        
                         if(GetValue[0]=="t")
                          {
                         spnbuy.style.display="block";
            spnload.style.display="none";
            alert('data inserted successfully');
            LoadResponse(ret,"MainPlaceHolder");
//             window.location="reg.aspx";
                        }
             
//            if(ret==="t")
//            {
////            alert(ret);
//            spnbuy.style.display="block";
//            spnload.style.display="none";              
////              window.location="reg.aspx";
//            }
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
    }
    catch(ex)
    {
        container.innerHTML=response;
    }
 }   
 
     