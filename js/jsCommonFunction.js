
function setCatIdfromDrowdown(setto, val)
{
    var objHdnCatCityId=document.getElementById(setto);
    //alert("Coming value : " + val + " Hidden obj: " + objHdnCatCityId);
    if(objHdnCatCityId!=null)
    {
        objHdnCatCityId.value="";
        objHdnCatCityId.value=val;
        //alert(objHdnCatCityId.value);
    }
    else
    {
        alert("Hidden field not found!");
    }
}

function setValToObj(setto, val)
{
    var objSetTo=document.getElementById(setto);
    //alert("Coming value : " + val + " obj: " + objSetTo);
    if(objSetTo!=null)
    {
        objSetTo.value="";
        objSetTo.value=val;
       // val.selectedIndex.options[val.selectedIndex].text;
    }
    else
    {
        alert("Hidden field not found!");
    }
}

function uncheckRadio(rad1, rad2)
{
	var radText=document.getElementById(rad1);
	var radHtml=document.getElementById(rad2);
	//alert("Existing " +radExisting + " New " + radNew );
	if(radText !=null)
	{
		radText.checked=false;
	}
	if(radHtml !=null)
	{
		radHtml.checked=false;
	}
	
}
function divVisibleInvisible(div1, div2, val)
{
    //alert(div1 + "," + div1 + "," + val);
	//If the value comes 1 : div2 to show div1 to hide
	//If the value comes 2 : div1 to show div2 to hide
	var dvObj1=document.getElementById(div1);
	var dvObj2=document.getElementById(div2);
	if(dvObj1!=null && dvObj2!=null)
	{
		if(val==1)
		{
			dvObj1.style.display="none";
			dvObj2.style.display="block";
		}
		else if(val==2)
		{
			dvObj1.style.display="block";
			dvObj2.style.display="none";
		}
		else
		{
			dvObj1.style.display="none";
			dvObj2.style.display="none";			
		}		
	}
	else
	{
		alert("Object not found.");
	}
	
}

function clearAllRadio(formName)
{
	var theForm=document.getElementById(formName);
	var z = 0; 
	if(theForm!=null)
	{
	    for(z=0; z<theForm.length;z++)
        {            
            if(theForm[z].type == 'radio')
            {
                theForm[z].checked = false;
            }
        }
    }
    else
    {
        alert("Object not found.");
    }
    
}


function setTodayDate(objName)
{
    var objCal=document.getElementById(objName);	   
    var today=new Date();    
    if(objCal!=null)
    {	        
        //objCal.value=today.getMonth()+1+"/"+today.getDate()+"/"+(today.getFullYear());    
        if(today.getMonth()<9)
        {
            
            if(today.getDate()<9)
            {
                objCal.value="0" + (today.getMonth()+1)+"/"+ "0" + (today.getDate())+"/"+(today.getFullYear());  
            }
            else
            {
                objCal.value="0" + (today.getMonth()+1)+"/"+today.getDate()+"/"+(today.getFullYear());    
            }
        }
        else
        {
            if(today.getDate()<9)
            {
                objCal.value=today.getMonth()+1+"/"+ "0" + (today.getDate())+"/"+(today.getFullYear());
            }
            else
            {
                objCal.value=today.getMonth()+1+"/"+today.getDate()+"/"+(today.getFullYear());    
            }
        }
    }
}


function setDDtextToObj(setto, objDropDown)
{
    var objSetTo=document.getElementById(setto);
    //alert("set to : " + objSetTo + " obj: " + objDropDown);
    if(objSetTo!=null)
    {
        var varSelIndex=objDropDown.selectedIndex;
//        objSetTo.value=objDropDown.options[varSelIndex].text;
       objSetTo.value=varSelIndex;
        alert(objSetTo.value);
    }
    else
    {
        alert("Hidden field not found!");
    }
}
function fnCartAdd(Mode,item_id)
{
if(Mode!="")
  { 
//    alert("mode:"+Mode+"item_id:"+item_id);
    //alert("proId="+ proID +"&catId="+ catId +"&mode="+Mode+"&Counter="+intcounter); 
    var spnbuy=document.getElementById("spnBuyBtn_"+item_id);
    var spnload=document.getElementById("spnload_"+item_id);
 //  alert("btn: " + spnbuy + " ajax img: " + spnload);
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
     self_findfile.xmlHttpReq_findfile.open("POST", "ajaxupdate.aspx", true);
    
    self_findfile.xmlHttpReq_findfile.setRequestHeader("Content-Type", "application/x-www-form-urlencoded");
// alert(self_findfile.xmlHttpReq_findfile.responseText);
    self_findfile.xmlHttpReq_findfile.onreadystatechange = function() 
    {
        if (self_findfile.xmlHttpReq_findfile.readyState == 4) 
        {    
            var ret= self_findfile.xmlHttpReq_findfile.responseText;
             if(ret.indexOf('~' != -1))
                    {
                        GetValue=ret.split("~");
                    }
                     //   alert(GetValue[0].toString());
                    
                     if(GetValue[0].toString()!="")
                    {
                        if(GetValue[0].toString()=="1")
                        {
                       window.location="cart.aspx";
                        }
                    }
            
//           alert(ret);
//            if(ret=="t")
//            {
////            spnbuy.style.display="block";
////            spnload.style.display="none";
//                window.location="cart.aspx";
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
    
//    self_findfile.xmlHttpReq_findfile.send("proId="+ proID +"&catId="+ catId +"&mode="+Mode+"&randN="+Math.random()); 
//self_findfile.xmlHttpReq_findfile.send("proId="+ proID +"&catId="+ catId +"&mode="+Mode+"&randN="+Math.random()); 

 self_findfile.xmlHttpReq_findfile.send("item_id="+ item_id +"&mode="+Mode+"&randN="+Math.random()); 
    flag=true;
  }

}


function loaddetail()
 {

  var xmlHttpobj=ajaxFunction();
//  alert(xmlHttpobj);
    if(xmlHttpobj != null)
    {
//         alert(xmlHttpobj);
//document.getElemntById("reg").style.visibility = "hidden";
document.getElementById("reg").style.display="none";
//       xmlHttpobj.open("POST","edit_reg.aspx",true);
        xmlHttpobj.open("GET","edit_reg.aspx",true);
        xmlHttpobj.send(null);
    }
 }



//function for add to cart from item page by subhro

function fnCartAddFrmItem(Mode,proID,catId)
{
if(Mode!="")
  { 
  
    //alert("proId="+ proID +"&catId="+ catId +"&mode="+Mode+"&randN="+Math.random()); 
    
//    var spnbuy=document.getElementById("spnBuyBtn_"+intcounter);

 var spnbuy=document.getElementById("spnBuyBtn");
 var qty=document.getElementById("hdnQty").value;
 
    var spnload=document.getElementById("spnload");
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
            if(ret=="t")
            {
                window.location="cart.aspx";
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
    
    self_findfile.xmlHttpReq_findfile.send("qty="+ qty +"&proId="+ proID +"&catId="+ catId +"&mode="+Mode+"&randN="+Math.random()); 
    flag=true;
  }

}

//==========================end

