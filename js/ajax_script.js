// JScript File

function del(id) 
{

var objMainDiv=document.getElementById("MainPlaceHolder");if(objMainDiv!=null) 
{ 

if(id!=null) 
{

var xmlHttpReq_Del = false;

var self = this;if (window.XMLHttpRequest) 
{

self.xmlHttpReq_Del = new XMLHttpRequest(); 
}

else if (window.ActiveXObject) 
{

self.xmlHttpReq_Del = new ActiveXObject("Microsoft.XMLHTTP"); 
}

self.xmlHttpReq_Del.open('POST', "from_ajax_delete.aspx", true); 

self.xmlHttpReq_Del.setRequestHeader('Content-Type', 'application/x-www-form-urlencoded'); 
 

self.xmlHttpReq_Del.onreadystatechange = function() 
{

if (self.xmlHttpReq_Del.readyState == 4) 
{ 

//alert(self.xmlHttpReq.responseText);

document.getElementById("dvAjaxPic").style.display="none";objMainDiv.style.display="block"; 
 

var GetValue=new Array();

if(self.xmlHttpReq_Del.responseText.indexOf('~' != -1)) 
{

GetValue=self.xmlHttpReq_Del.responseText.split("~"); 
} 

if(GetValue[0].toString()!="") 
{

if(GetValue[0].toString()=="0") 
{

 

}

else

{

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

document.getElementById("dvAjaxPic").style.display="block";objMainDiv.style.display="none"; 
}

} 

self.xmlHttpReq_Del.send("mode=3&id="+id); 
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

var container=document.getElementById("dvTest"); 
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

var GetValue=new Array(); if(self.xmlHttpReq.responseText.indexOf('~' != -1)) 
{

GetValue=self.xmlHttpReq.responseText.split("~"); 
}

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
 

self_findfile.xmlHttpReq_findfile.setRequestHeader("Content-Type", "application/x-www-form-urlencoded");self_findfile.xmlHttpReq_findfile.onreadystatechange = function() 
{

if (self_findfile.xmlHttpReq_findfile.readyState == 4) 
{ 

var ret= self_findfile.xmlHttpReq_findfile.responseText; 
 

var GetValue=new Array();

GetValue=ret.split('');

// alert(GetValue[0]);

// alert(GetValue[1]);

if(GetValue[0]=="t") 
{

spnbuy.style.display="block";spnload.style.display="none"; 
}

else

{

alert("Some problem in inserting!!plz try again....."); 
}

}

if (self_findfile.xmlHttpReq_findfile.readyState < 4) 
{ 

spnbuy.style.display="none";spnload.style.display="block"; 
}

}

 

self_findfile.xmlHttpReq_findfile.send("name="+ name +"&address="+ address); flag=true; 
 

 

} 

