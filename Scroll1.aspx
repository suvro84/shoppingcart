<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Scroll1.aspx.cs" Inherits="Scroll1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Untitled Page</title>
</head>
// Deluxe Scroller
// Author: Howard Covitz
// Date: May 4, 2007
// All rights reserved, yada yada yada
function startScroller(dn_newsID,scrollWindowHeight,scrollWindowWidth,scrollListHeight,scrollInterval,debug)
{

 // Set defaults
 if (!dn_newsID){
  dn_newsID = 'articleScroller' ;
 }
 if (!scrollWindowHeight){
  scrollWindowHeight = 200 ;
 }
 if (!scrollWindowWidth){
  scrollWindowWidth = 350;
 }
 if (!scrollListHeight){
  scrollListHeight = 900;
 }
 if (!scrollInterval){
  scrollInterval = 30;
 }
 if (!debug){
  debug= false;
 }


 var n=document.getElementById(dn_newsID);
 if(!n){return;}
 n.style.height= scrollWindowHeight + 'px'; //sets window height

 var c=n.getElementsByTagName('div')[0]; // carOne
 var d=n.getElementsByTagName('div')[1]; // carTwo
 
 
 
 c.innerHTML = d.innerHTML;
 c.style.width= (scrollWindowWidth - 30) + 'px'; //sets data list width, should be less than window width.
 d.style.width= (scrollWindowWidth - 30) + 'px'; //sets data list width, should be less than window width.
// var scrollListHeight = c.offsetHeight; 
 if(debug){
  alert(c.offsetHeight);
 }
 if (c.offsetHeight == 0){ //will hopefully only happen in IE if nested down far
  var strContent = c.innerHTML;
  //determine character count: strip out HTML tags first
  strContent = strContent.replace(/&(lt|gt);/g, function (strMatch, p1){
     return (p1 == "lt")? "<" : ">"; });
   strContent = strContent.replace(/<\/?[^>]+(>|$)/g, "");  
   var contentLen = strContent.length;
   //following optimized for stylesheet listed later
   var charsPerLine = 60;
   var lineHeight = 15;
   var numItems = 8;
   var sepLineHeight = 20;
   var buffer = 20;
   scrollListHeight = Math.round(((contentLen/charsPerLine) * lineHeight) + (numItems * sepLineHeight) + buffer);
   
 }
 else{
  scrollListHeight = c.offsetHeight + 15; //pad for firefox????
 }

 c.scrollInterval = scrollInterval;
 c.scrollListHeight= scrollListHeight;
 c.scrollWindowHeight =  scrollWindowHeight;
 if(scrollWindowHeight > scrollListHeight){
  //c.dn_startpos= scrollListHeight; // Should be presumed height of data list or window height, whichever is smaller  
 }
 c.dn_scrollpos = c.scrollWindowHeight;

 d.scrollInterval = scrollInterval;
 d.dn_endpos= scrollListHeight * -2;  //double check this logic...maybe this is no longer used below
 d.dn_startpos = scrollWindowHeight;//since it is relative positioned, it will line up right after carOne
 d.dn_scrollpos = d.dn_startpos;
 
 c.myinterval = setInterval('scrollDOMnews("' + dn_newsID + '")',c.scrollInterval);

 c.onmouseover=function(){clearInterval(c.myinterval);}
 c.onmouseout=function(){c.myinterval =setInterval('scrollDOMnews("' + dn_newsID + '")',c.scrollInterval);}
 d.onmouseover=function(){clearInterval(c.myinterval);}
 d.onmouseout=function(){c.myinterval =setInterval('scrollDOMnews("' + dn_newsID + '")',c.scrollInterval);}

}

function scrollDOMnews(dn_newsID)
{
 var c=document.getElementById(dn_newsID).getElementsByTagName('div')[0]; 
 c.style.top=c.dn_scrollpos+'px'; 
 if(c.dn_scrollpos== Math.round((c.scrollListHeight * -1))){ //ie -700
  c.dn_scrollpos=c.scrollListHeight; //directly below car two
 }
 c.dn_scrollpos--;
 scrollDOMnews2(dn_newsID); 
}
function scrollDOMnews2(dn_newsID)
{
 var c=document.getElementById(dn_newsID).getElementsByTagName('div')[1]; 
 c.style.top=c.dn_scrollpos+'px'; 
 if(c.dn_scrollpos==Math.round(c.dn_endpos)){
  c.dn_scrollpos= 0;
 }
 c.dn_scrollpos--; 
}


 /* stop scroller when window is closed */
window.onunload=function()
{
 // this will have to be a nice to have:
 //clearInterval(dn_interval);
}


/*
EXPECTED FORMAT OF HTML

<div class="marquee" id="articleScroller" style="overflow:hidden;postion:relative;">
 <div style="position:relative;"></div>
 <div style="position:relative;">
  {CONTENT GOES HERE -- just don't use DIV tags}
 </div>
</div>
USAGE:
startScroller("articleScroller",200,350,30);
startScroller("articleScroller2",200,350,30);

In IE, optimized for following styles:
.marquee{
 padding:0px;
 margin:0px;
}
.marquee ul{
 margin:0px;
 padding:0px;
}
.marquee li{
    padding-bottom:3px;
 margin:0px;
}
.marquee li a:link, .marquee li a:visited {
 font: 11px Arial, Verdana, sans-serif;
 text-decoration:underline;
 font-weight:bold;
}
.marquee li a:hover, .marquee li a:active {
 font: 11px Arial, Verdana, sans-serif;
 text-decoration: none;
 font-weight:bold;
}
.marquee p{
 padding:0px;
 margin:0px;
}

*/
</html>
