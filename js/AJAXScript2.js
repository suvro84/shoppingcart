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
   alert(xmlHttpobj);
//    if(xmlHttpobj != null)
//    {
////     alert(xmlHttpobj);
//var load=getLoading();
//    
////        xmlHttpobj.open("GET","NewPage.aspx?name=",true);
////        xmlHttpobj.send(null);
//    }
}
function getLoading()
{
    return "<div style='width:100%;height:300px;padding-top:150px'><center><img src='images/pleasewait.gif'/><br><font color='red'><b>Loading....</b></font></center></div>";
}
function statechange()
{
    if(xmlHttp.readyState==2)
    {
        //document.getElementById("MainPlaceHolder").innerHTML="Loading...........";
        LoadResponse(getLoading(),"MainPlaceHolder");
    }
   
    
}