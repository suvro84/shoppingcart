// JScript File

// --- Script for character limitation in text area [.. Starts ..]---------
var ns6=document.getElementById&&!document.all
function restrictinput(maxlength,e,placeholder)
{
	if (window.event&&event.srcElement.value.length>=maxlength)
	{
		alert("Maximum Character Limit Reached.");
		return false
	}
	else if (e.target&&e.target==eval(placeholder)&&e.target.value.length>=maxlength)
	{
		var pressedkey=/[a-zA-Z0-9\.\,\/]/ //detect alphanumeric keys
		if (pressedkey.test(String.fromCharCode(e.which)))
			e.stopPropagation()
	}
}

function countlimit(maxlength,e,placeholder)
{
	var theform=eval(placeholder)
	var lengthleft=maxlength-theform.value.length
	var placeholderobj=document.all? document.all[placeholder] : document.getElementById(placeholder)
	if (window.event||e.target&&e.target==eval(placeholder))
	{
		if (lengthleft<0)
		theform.value=theform.value.substring(0,maxlength)
		if(lengthleft<=10)
		{
		    placeholderobj.innerHTML="<font color='red'>"+lengthleft+"</font>";
		}
		else
		{
		    placeholderobj.innerHTML=lengthleft
        }		    
	}
}

function displaylimit(thename, theid, thelimit)
{
	var theform=theid!=""? document.getElementById(theid) : thename
	var limit_text='<p><b><span  id="'+theform.toString()+'">'+thelimit+'</span></b> characters remaining on your input limit</p>'
	if (document.all||ns6)
		document.write(limit_text)
	if (document.all)
	{
		eval(theform).onkeypress=function(){ return restrictinput(thelimit,event,theform)
											//alert("Max 10 character");
											}
		eval(theform).onkeyup=function(){ countlimit(thelimit,event,theform)}
	}
	else if (ns6)
	{
		document.body.addEventListener('keypress', function(event) { restrictinput(thelimit,event,theform) }, true); 
		document.body.addEventListener('keyup', function(event) { countlimit(thelimit,event,theform) }, true); 
	}
}
// --- [Script for character limitation in text area] [.. Ends ..]---------

// --- This function used to select all the text in textbox [.. Starts ..] ----
function SelectAll(a)
{
    var text_val=eval("document.form1."+a.name);
    text_val.focus();
    text_val.select();
}
// --- This function used to select all the text in textbox [.. Ends ..] ----

// -- this function used in AddSiteDetails.aspx [.. starts..]
function CheckURL()
{
    if(document.getElementById("txtlogopath").value=="http://www.yourdomain.com/pictures/logo.jpg")
    {
        alert("Image Logo Path is incorrect, please provide correct path.");
        document.getElementById("txtlogopath").focus;
        return false;
    }
    return true;
}
// -- this function used in AddSiteDetails.aspx [.. ends ..]

// -- this function used in InitiateLink.aspx [.. starts ..]
function hideMailSubjectDiv()
{
    if(document.getElementById("mailChkBox_HTML"))
    {
        if(document.getElementById("mailChkBox_HTML").checked==false)
        {
            document.getElementById("div_mailSubject_HTML").style.display="none";
            document.getElementById("div_mailLabel_HTML").style.display="none";
        }
    }
    if(document.getElementById("mailChkBox_Desc"))
    {
        if(document.getElementById("mailChkBox_Desc").checked==false)
        {
            document.getElementById("div_mailSubject_Desc").style.display="none";
            document.getElementById("div_mailLabel_Desc").style.display="none";
        }
    }            
}
function div_mailSubjectVisible_Desc()
{
    if(document.getElementById("mailChkBox_Desc"))
    {
        if(document.getElementById("mailChkBox_Desc").checked==true)
        {
            document.getElementById("div_mailSubject_Desc").style.display="block";
            document.getElementById("div_mailLabel_Desc").style.display="block";
        }
        else
        {
            document.getElementById("div_mailSubject_Desc").style.display="none";
            document.getElementById("div_mailLabel_Desc").style.display="none";
        }
    }  
}
function div_mailSubjectVisible_HTML()
{
    if(document.getElementById("mailChkBox_HTML"))
    {
        if(document.getElementById("mailChkBox_HTML").checked==true)
        {
            document.getElementById("div_mailSubject_HTML").style.display="block";
            document.getElementById("div_mailLabel_HTML").style.display="block";
        }
        else
        {
            document.getElementById("div_mailSubject_HTML").style.display="none";
            document.getElementById("div_mailLabel_HTML").style.display="none";
        }
    }
}
// -- this function used in InitiateLink.aspx [.. ends ..]
