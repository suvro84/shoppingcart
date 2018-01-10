// JScript File
function statechange()
{
   
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

    if(xmlHttp.readyState==2)
    {
        //document.getElementById("MainPlaceHolder").innerHTML="Loading...........";
         alert(xmlHttp.responseXML);
        LoadResponse(getLoading(),"MainPlaceHolder");
    }

     else if(xmlHttp.readyState==4)
    {
    //alert(xmlHttp.responseXML);
    if(xmlHttp.status==200)
    LoadResponse(xmlHttp.responseText,"MainPlaceHolder");
    else
        alert("Error Occurred while Fetching Last Request. ErrorCode:" + xmlHttp.status);
        //document.getElementById("MainPlaceHolder").innerHTML=xmlHttp.responseText;
    }
}
function getLoading()
{
    return "<div style='width:100%;height:300px;padding-top:150px'><center><img src='images/pleasewait.gif'/><br><font color='red'><b>Loading....</b></font></center></div>";
}
function loadAll()
{
    var xmlHttpobj=ajaxFunction();
//    if(xmlHttpobj != null)
//    {
//        xmlHttpobj.open("GET","NewPage.aspx?name=",true);
//        xmlHttpobj.send(null);
//    }
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
   