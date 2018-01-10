<!--
function formValidate(oSrc, args)
{
    var objTerms=document.getElementById("chkTerms");
    //alert("obj: " + objTerms + " checked: " + objTerms.checked);
    if(objTerms!=null)
    {
        if(objTerms.checked)
        {
            var formobj=document.forms["formcheck"];
            if(formobj)
            {
                // Enter name of mandatory fields
	            // Enter field description to appear in the dialog box
	            var fieldRequired = Array("billFName", "billLName", "billAddress1", "billZip", "billCountry", "hdnBillCountryName", "billState", "hdnBillStateName", "billCity", "billPhNo", "billEmail", "shipDate", "shipFName", "shipLName", "shipAddress1", "shipZip", "shipCountry", "shipState", "hdnShipStateName", "shipCity", "hdnShipCityName", "shipPhNo");
	            // Enter field description to appear in the dialog box
	            var fieldDescription = Array("Billing First Name", "Billing Last Name", "Billing Address", "Billing Zip Code", "Billing Country", "Billing Country", "Billing State", "Billing State", "Billing City", "Billing Phone Number", "Billing Email Id", "Shipping Date", "Shipping First Name", "Shipping Last Name", "Shipping Address", "Shipping Zip Number", "Shipping Country", "Shipping State", "Shipping State", "Shipping City", "Shipping City", "Shipping Phone Number");
	            // Dialog message
	            var alertMsg = "Please complete the following fields:\n";
            	
	            var l_Msg = alertMsg.length;
            	
	            for (var i = 0; i < fieldRequired.length; i++)
	            {
		            var obj = formobj.elements[fieldRequired[i]];		    
		            if (obj)
		            {
		                // Checking Shipping Date Starts
			            if(obj.name=="shipDate") 
			            {
			                //if(!validateShipDate(obj.value))
			                //{
			                //    alertMsg += " - Select A valid shipping date.\n";
			                //}
			                //alert(obj.value);
			                if(obj.value=="02/22/2009")
			                {
			                    alertMsg +="Due to system maintanance, we are not taking any more orders for 22-Feb-2009. Kindly choose another date.\n";
			                }
			            }           
			            // Checking Shipping Date Ends
			            switch(obj.type)
			            {
			            case "select-one":			            
			                    if (obj.selectedIndex == -1 || obj.options[obj.selectedIndex].text == "" || obj.options[obj.selectedIndex].value == "0")
			                    {
				                    alertMsg += " - " + fieldDescription[i] + "\n";
				                    obj.focus();
			                    }				    	    
				                break;
			            case "select-multiple":
				            if (obj.selectedIndex == -1)
				            {
					            alertMsg += " - " + fieldDescription[i] + "\n";
					            obj.focus();
				            }
				            break;
			            case "text":
			                if (obj.value == "" || obj.value == null)
				                {
					                alertMsg += " - " + fieldDescription[i] + "\n";
					                obj.focus();
				                }
				             else
				                {				                    
				                    // Checking Email Address Starts
				                    if(obj.name=="billEmail" || obj.name=="billEmail2" || obj.name=="shipEmail") 
				                    {
				                        if(!validateEmail(obj.value))
				                        {
				                            if(obj.name=="billEmail")
				                            {
				                                alertMsg += " - Valid Billing Email address.\n";
				                                obj.focus();
				                            }
				                            else if(obj.name=="billEmail2")
				                            {
				                                alertMsg += " - Valid Billing Email address.\n";
				                                obj.focus();
				                            }
				                            else if(obj.name=="shipEmail")
				                            {
				                                alertMsg += " - Valid Shipping Email address.\n";
				                                obj.focus();
				                            }				                    
				                        }
				                        else
				                        {
				                            if(!stringMatch('billEmail','billEmail2'))
				                            {				                    
				                                alertMsg += " - Please check both the billing email id are not same.\n";
				                            }
				                        }
				                    }           
				                    // Checking Email Address Ends
        				            
				                    // Checking Telephone Number Starts
				                    if(obj.name=="billPhNo" || obj.name=="shipPhNo") 
				                    {
				                        if(!validatePhNo(obj.value))
				                        {
				                            if(obj.name=="billPhNo")
				                            {
				                                alertMsg += " - Valid Billing Phone Number.\n";
				                                obj.focus();
				                            }
				                            else if(obj.name=="shipPhNo")
				                            {
				                                alertMsg += " - Valid Shipping Phone Number.\n";
				                                obj.focus();
				                            }			                    
				                        }
				                    }           
				                    // Checking Telephone Number Ends
        				            
				                    // Checking Zip Code Starts
				                    if(obj.name=="billZip" || obj.name=="shipZip") 
				                    {
				                        if(!validateZipCode(obj.value))
				                        {
				                            if(obj.name=="billZip")
				                            {
				                                alertMsg += " - Valid Billing Postal Code.\n";
				                                obj.focus();
				                            }
				                            else if(obj.name=="shipZip")
				                            {
				                                alertMsg += " - Valid Shipping Postal Code.\n";
				                                obj.focus();
				                            }			                    
				                        }
				                    }           
				                    // Checking Zip Code Ends        				            
				                }
			                break;
			            case "textarea":
				            if (obj.value == "" || obj.value == null)
				            {
					            alertMsg += " - " + fieldDescription[i] + "\n";
					            obj.focus();
				            }
				            break;
				        case "select":
			                if (obj.value == "" || obj.value == null)
			                {	
				                alertMsg += " - " + fieldDescription[i] + "\n";
				                obj.focus();
			                }
				            break;
        				    
        				    
			            default:
			            }
			            if (obj.type == undefined)
			            {
				            var blnchecked = false;
				            for (var j = 0; j < obj.length; j++)
				            {
					            if (obj[j].checked)
					            {
						            blnchecked = true;
					            }
				            }
				            if (!blnchecked)
				            {
					            alertMsg += " - " + fieldDescription[i] + "\n";
				            }
			            }
		            }
	            }
        	    

	            if (alertMsg.length == l_Msg)
	            {
		            //return true;
		            args.IsValid=true;
	            }
	            else
	            {
		            alert(alertMsg);
		            //return false;
		            args.IsValid=false;
	            }
	        }
	        else
	        {
	            alert("Form object not found.");
	            args.IsValid=false;
	        }
	    }
	    else
	    {
	        alert("Please check the \"I have read and agree to the Terms of Service\" to proceed further.");	        
	        args.IsValid=false;
	        objTerms.focus();
	    }
	}
}// -->


function IsNumeric(e)
{
    // Calling procedure
    // onKeyPress=\"javascript:return IsNumeric(event);\" 
    //
    //alert(e);
	var KeyID = (window.event) ? event.keyCode : e.which;
	if((KeyID >= 66 && KeyID <= 90) || (KeyID >= 97 && KeyID <= 122) || (KeyID >= 33 && KeyID <= 47) ||
	   (KeyID >= 58 && KeyID <= 64) || (KeyID >= 91 && KeyID <= 96) || (KeyID >= 123 && KeyID <= 126))
	{
		return false;
	}
    return true;
}



function validatePhNo(str)
{
    if(str)
    {
        //if(isNaN(str) || str.length<6 || str.length>15)
        if(str.length<6 || str.length>15)
        {
            return false;
        }
        else
        {
            return true;
        }    
    }
    else
    {
        return false;
    }
}

function validateZipCode(str)
{
    if(str)
    {
        if(str.length<4 || str.length>10)
        {
            return false;
        }
        else
        {
            return true;
        }    
    }
    else
    {
        return false;
    }
}
function validateEmail(str)
{
    if(str)
    {
        var at="@"
        var dot="."
        var lat=str.indexOf(at)
        var lstr=str.length
        var ldot=str.indexOf(dot)
        if (str.indexOf(at)==-1)
        {
           //alert("Invalid E-mail ID")
           return false;
        }

        if (str.indexOf(at)==-1 || str.indexOf(at)==0 || str.indexOf(at)==lstr)
        {
           //alert("Invalid E-mail ID")
           return false;
        }

        if (str.indexOf(dot)==-1 || str.indexOf(dot)==0 || str.indexOf(dot)==lstr)
        {
            //alert("Invalid E-mail ID")
            return false;
        }

         if (str.indexOf(at,(lat+1))!=-1)
         {
            //alert("Invalid E-mail ID")
            return false;
         }

         if (str.substring(lat-1,lat)==dot || str.substring(lat+1,lat+2)==dot)
         {
            //alert("Invalid E-mail ID")
            return false;
         }

         if (str.indexOf(dot,(lat+2))==-1)
         {
            //alert("Invalid E-mail ID")
            return false;
         }

         if (str.indexOf(" ")!=-1)
         {
            //alert("Invalid E-mail ID")
            return false;
         }
         return true		
	 }
	 else
	 {
	    return false;
	 }			
}

function stringMatch(object1, object2)
{
    var objToMatch=document.getElementById(object2);
    var objWithMatch=document.getElementById(object1);
    if(objToMatch && objWithMatch)
    {
        if(objWithMatch.value!=objToMatch.value)
        {            
            return false;
        }
        else
        {
            return true;
        }    
    }
    else
    {
        alert("Objects not found.");
        return false;
    }
}

function validateShipDate(shipDate)
{
    var startdate="";
    var today=new Date();
    if(today.getMonth()<9)
    {
        if(today.getDate()<9)
        {
            startdate="0" + (today.getMonth()+1)+"/"+ "0" + (today.getDate())+"/"+(today.getFullYear());  
        }
        else
        {
            startdate="0" + (today.getMonth()+1)+"/"+today.getDate()+"/"+(today.getFullYear());    
        }
    }
    else
    {
        if(today.getDate()<9)
        {
            startdate=today.getMonth()+1+"/"+ "0" + (today.getDate())+"/"+(today.getFullYear());
        }
        else
        {
            startdate=today.getMonth()+1+"/"+today.getDate()+"/"+(today.getFullYear());    
        }
    }
    if(startdate!=null && shipDate!=null)
     {    
        if (Date.parse(startdate) > Date.parse(shipDate)) 
        {        
            return false;
        }
        else
        {  
            return true;
        }
    }
    else
    {
        return false;
    }
}

function setCity(setto, objDropDown)
{
    var objSetTo=document.getElementById(setto);
    var objOtherCity=document.getElementById("dvOtherCity");
    if(objSetTo!=null && objOtherCity!=null)
    {
        var varSelIndex=objDropDown.selectedIndex;
        objSetTo.value=objDropDown.options[varSelIndex].text;
        if(objDropDown.value=="9999")
        {
            objOtherCity.style.display="block";
        }
        else
        {
            objOtherCity.style.display="none";
        }
    }
    else
    {
        alert("Hidden field not found!");
    }
}

