// JScript File

function del(id)
 {
    var objMainDiv=document.getElementById("objMainDiv");
//    alert(id);
    if(objMainDiv!=null)
    {    
        if(id!=null)
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
//                 alert(GetValue[0]);
//                      alert(GetValue[1]);
                   
                    if(GetValue[0].toString()!="")
                    {
                        if(GetValue[0].toString()=="0")
                        {
//                            objMainDiv.innerHTML="No Record in data base";  
                        }
                        else
                        {

// document.getElementById("objMainDiv").style.display="block";
 objMainDiv.innerHTML=GetValue[2].toString();

                        }
                    }
                    else
                    {
                        alert("Ajax return not found...");
                    }
                }
                else
                {
//                    alert('h1');
                    document.getElementById("dvAjaxPic").style.display="block";
                    objMainDiv.style.display="none";
                }
            }                
 self.xmlHttpReq.send("mode=3&id="+id); 

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
 
 function loadGV()
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
            self.xmlHttpReq.open('POST', "from_ajax_delete.aspx", true);             
            self.xmlHttpReq.setRequestHeader('Content-Type', 'application/x-www-form-urlencoded');
            self.xmlHttpReq.onreadystatechange = function() 
            {
                if (self.xmlHttpReq.readyState == 4) 
                {          

                    var GetValue=new Array();
                    
                    
                    if(self.xmlHttpReq.responseText.indexOf('~' != -1))
                    {
                        GetValue=self.xmlHttpReq.responseText.split("~");
                    }
//                    alert(GetValue[0]);
//                      alert(GetValue[1]);
//                       alert(GetValue[2]);


                    if(GetValue[0].toString()!="")
                    {
                        if(GetValue[0].toString()=="0")
                        {

                        LoadResponse(GetValue[2],"MainPlaceHolder");
                        }
                        else
                        {
                              

                        }
                    }
                    else
                    {
                        alert("Ajax return not found...");
                    }
                }
                else
                {

                }
            }        
 
 self.xmlHttpReq.send("m=2"); 

        }
     //for insertion
     
//    function ins()
// {
//       var name=document.getElementById("txtname").value;
//   var address=document.getElementById("txtaddress").value;
// var spnbuy=document.getElementById("spnBuyBtn");
//    var spnload=document.getElementById("spnload");
//            var xmlHttpReq = false;
//            var self = this;
//            if (window.XMLHttpRequest) 
//            {
//                self.xmlHttpReq = new XMLHttpRequest();        
//            }
//            else if (window.ActiveXObject) 
//            {
//                self.xmlHttpReq = new ActiveXObject("Microsoft.XMLHTTP");         
//            }
//            self.xmlHttpReq.open('POST', "from_ajax_delete.aspx", true);             
//            self.xmlHttpReq.setRequestHeader('Content-Type', 'application/x-www-form-urlencoded');
//            self.xmlHttpReq.onreadystatechange = function() 
//            {
//                alert(self.xmlHttpReq.responseText);
//                if (self.xmlHttpReq.readyState == 4) 
//                {          
//                  
//                    var GetValue=new Array();
//                    if(self.xmlHttpReq.responseText.indexOf('~' != -1))
//                    {
//                        GetValue=self.xmlHttpReq.responseText.split("~");
//                    }
//                     alert(GetValue[0]);
//                      alert(GetValue[1]);
//                   
//                    if(GetValue[0].toString()!="")
//                    {
//                        if(GetValue[0].toString()=="0")
//                        {
//                            objMainDiv.innerHTML="insertion failed";  
//                        }
//                        else
//                        {

//  spnbuy.style.display="block";
//            spnload.style.display="none";
//                        }
//                    }
//                    else
//                    {
//                        alert("Ajax return not found...");
//                    }
//                }
//                else
//                {
//                     spnbuy.style.display="none";
//             spnload.style.display="block";
//                }
//            }               
// self.xmlHttpReq.send("name="+ name +"&address="+ address); 

//        
//        
//    
//   
// }
 
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
    
    self_findfile.xmlHttpReq_findfile.setRequestHeader("Content-Type", "application/x-www-form-urlencoded");
    self_findfile.xmlHttpReq_findfile.onreadystatechange = function() 
    {
        if (self_findfile.xmlHttpReq_findfile.readyState == 4) 
        {    
            var ret= self_findfile.xmlHttpReq_findfile.responseText;
             
           var GetValue=new Array();
           GetValue=ret.split('');
//      alert(GetValue[0]);
//     alert(GetValue[1]);
            if(GetValue[0]=="t")
                          {
                         spnbuy.style.display="block";
            spnload.style.display="none";
}
            else
            {
                alert("Some problem in inserting!!plz try again.....");
            }
        }
        if (self_findfile.xmlHttpReq_findfile.readyState < 4) 
        {     
             spnbuy.style.display="none";
             spnload.style.display="block";
        }
    }
    
    self_findfile.xmlHttpReq_findfile.send("name="+ name +"&address="+ address); 
    flag=true;
 
 
 }       
      
      
      
      
      function update(mode,id)
{
//   alert("got it: " + id );
//   var txtBoxIds=document.getElementById("repdisplay_ctl00_txtprice").value;
//   alert(txtBoxIds.value);
//    var objErrLabel=document.getElementById("lblError");

 
// var cb=document.getElementsByName("repdisplay$ctl0"+i+"$txtprice");
 
 var selectedids="";
  var GetIds=new Array();
var count=document.getElementById("hdtot").value;
alert(count);
//alert(document.getElementsByName("repdisplay$ctl0"+count+"$txtprice"));
        for(var i=0;i<count;i++)
        {
 var cb=document.getElementById("repdisplay_ctl0"+i+"_txtprice").value;
         
//          alert(cb);
          
//          if(GetIds[i]==null)
//                GetIds[i]+=cb;
//                else

         if(selectedids=="")
                selectedids+=cb.toString();
                else
                selectedids+="," + cb.toString();
//GetIds[i]=cb;

            }
//  alert(selectedids[0].toString());
// alert(selectedids[1].toString());
//alert(selectedids[2].toString());
    var objMainDiv=document.getElementById("objMainDiv");
//    alert(objMainDiv);
    
    if(objMainDiv!=null)
    {   
//        var varProdId=new Array();
//        varProdId=txtBoxIds.split("|");
//        //alert(varProdId);
//        var varTxtBoxProdId="";   
//        for(i=0;i<varProdId.length;i++)
//        {
//            if(varProdId[i].toString()!="")
//            {                
//                if(document.getElementById(varProdId[i].toString()))
//                {
//                    //alert(varTxtBoxProdId);
//                    varTxtBoxProdId=varTxtBoxProdId + varProdId[i].toString() + "~" + document.getElementById(varProdId[i].toString()).value + "|";
//                    //alert(varTxtBoxProdId);
//                }
//            }
//        }
        
        if(selectedids!=null)
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
            self.xmlHttpReq.open('POST', "from_ajax_update.aspx", true);             
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
//                    alert(GetValue[0].toString());
                    alert(GetValue[1].toString());
                    if(GetValue[0].toString()!="")
                    {
                        if(GetValue[0].toString()=="0")
                        {
//                            objErrLabel.innerHTML=GetValue[1].toString();                         
                            objMainDiv.innerHTML="";  
                        }
                        else
                        {
                            objMainDiv.innerHTML=GetValue[1].toString();;
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
            self.xmlHttpReq.send("mode=" + 3 + "&strBoxIds="+selectedids+"&id="+id);
        }
    }
    else
    {
        alert("Objects not found. Try again.");
    }
}  



      function loadPage(mode)
    {
        var xmlHttpReq = false;
        var self = this;
        var selectedids = '';
        if(typeof arguments[0] != 'undefined')
        selectedids = arguments[0];
//        alert(selectedids);
        if (window.XMLHttpRequest) 
        {
            self.xmlHttpReq = new XMLHttpRequest();        
        }
        else if (window.ActiveXObject) 
        {
            self.xmlHttpReq = new ActiveXObject("Microsoft.XMLHTTP");         
        }
        self.xmlHttpReq.open('POST', "frmAjaxCommonFunctions.aspx", true);             
        self.xmlHttpReq.setRequestHeader('Content-Type', 'application/x-www-form-urlencoded');
        self.xmlHttpReq.onreadystatechange = function() 
        {
            if(self.xmlHttpReq.readyState == 1)
            {
                LoadResponse("Loading...", "dvMaindiv");
            }
            if (self.xmlHttpReq.readyState == 4) 
            {    
                LoadResponse(self.xmlHttpReq.responseText,"dvMaindiv");  
                restoreNumbers();
            }
        }                
        self.xmlHttpReq.send("mode=" + mode + "&strBoxIds=" + selectedids);
    }
    function LoadResponse(response,control)
     {
     var container=document.getElementById(control);
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