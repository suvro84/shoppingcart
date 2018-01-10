var titlea = new Array();var texta = new Array();var linka = new Array();var trgfrma = new Array();var heightarr = new Array();var cyposarr = new Array();
cyposarr[0]=0;
cyposarr[1]=1;
cyposarr[2]=2;
cyposarr[3]=3;
cyposarr[4]=4;

//titlea[0] = "Commodity Outlook <a href=\"category.aspx?id=5\">       <img border=\"0\" alt=\"\" src=\"images/Citibank.jpg\" /></a>";
////texta[0] = "<img alt=\"\" src=\"images/Citibank.jpg\" />";
//texta[0] = "First";

//linka[0] = "first";
//trgfrma[0] = "_blank";

//titlea[1] = "nifty view..";
//texta[1] = "nifty view..";
//linka[1] = "InterestingArticles.aspx?id=48";
//trgfrma[1] = "_blank";

//titlea[2] = "Stock Market calls dt 24-4-09";
//texta[2] = "Stock Market calls dt 24-4-09";
//linka[2] = "InterestingArticles.aspx?id=47";
//trgfrma[2] = "_blank";

//titlea[3] = "Gold View";
//texta[3] = "Gold View";
//linka[3] = "InterestingArticles.aspx?id=46";
//trgfrma[3] = "_blank";

//titlea[4] = "The 7 new rules of financial security";
//texta[4] = "The 7 new rules of financial security";
//linka[4] = "http://money.cnn.com/galleries/2009/moneymag/0903/gallery.financial_rules.moneymag/index.html";
//trgfrma[4] = "_blank";



//titlea[1] = "Commodity Outlook";
//texta[0] = "<img alt="" src="images/Citibank.jpg" />";
//linka[0] = "first";
//trgfrma[0] = "_blank";
//titlea[1] = "nifty view..";
//texta[1] = "nifty view..";
//linka[1] = "InterestingArticles.aspx?id=48";
//trgfrma[1] = "_blank";
//titlea[2] = "Stock Market calls dt 24-4-09";
//texta[2] = "Stock Market calls dt 24-4-09";
//linka[2] = "InterestingArticles.aspx?id=47";
//trgfrma[2] = "_blank";
//titlea[3] = "Gold View";
//texta[3] = "Gold View";
//linka[3] = "InterestingArticles.aspx?id=46";
//trgfrma[3] = "_blank";
//titlea[4] = "The 7 new rules of financial security";
//texta[4] = "The 7 new rules of financial security";
//linka[4] = "http://money.cnn.com/galleries/2009/moneymag/0903/gallery.financial_rules.moneymag/index.html";
//trgfrma[4] = "_blank";




alert(document.getElementById('hdReports').value);
eval(document.getElementById('hdReports').value);

var mc=5;

var inoout=false;
var spage=0;
var cvar=0,say=0,tpos=0,enson=0,hidsay=0,hidson=0;
var tmpv;tmpv=180-8-8-(2*1);
var psy = new Array();divtextb ="<div id=d";
divtextb2 ="<div id=dz";divtev1=" onmouseover=\"mdivmo(";
divtev2=")\" onmouseout =\"restime(";divtev3=")\" onclick=\"butclick(";
divtev4=")\"";
divtexts = " style=\"position:absolute;visibility:hidden;width:"+tmpv+"; COLOR: DEE2ED; left:0; top:0; FONT-FAMILY: MS Sans Serif,arial,helvetica; FONT-SIZE: 8pt; FONT-STYLE: normal; FONT-WEIGHT: normal; TEXT-DECORATION: none; margin:0px; LINE-HEIGHT: 12pt; text-align:left;padding:0px;\">";
ns6span= " style=\"position:relative; COLOR: FFFFFF; width:"+tmpv+"; FONT-FAMILY: verdana,arial,helvetica; FONT-SIZE: 9pt; FONT-STYLE: normal; FONT-WEIGHT: bold; TEXT-DECORATION: none; LINE-HEIGHT: 14pt; text-align:left;padding:0px;\"";
uzun="<div id=\"enuzun\" style=\"position:absolute;left:0;top:0;\">";
uzun2="<div id=\"enuzun2\" style=\"position:absolute;left:0;top:0;\">";
var uzunobj=null,uzunobj2=null;
var uzuntop=0;
var toplay=0;
function mdivmo(gnum,gnum5)
{inoout=true;if((linka[gnum].length)>2){if(gnum5==1)
{    
objd=document.getElementById('dz'+gnum);    
objd2=document.getElementById('hgdz'+gnum);}
else{    
objd=document.getElementById('d'+gnum);    
objd2=document.getElementById('hgd'+gnum);}
objd.style.color="#DBF7BF";
objd2.style.color="#F4ECAE";
objd.style.cursor='pointer';
objd2.style.cursor='pointer';
objd.style.textDecoration='underline';
objd2.style.textDecoration='underline';
window.status=""+linka[gnum];}}
function restime(gnum2,gnum5){
	inoout=false;if(gnum5==1){    
	objd=document.getElementById('dz'+gnum2);    
	objd2=document.getElementById('hgdz'+gnum2);}
	else{    
	objd=document.getElementById('d'+gnum2);    
	objd2=document.getElementById('hgd'+gnum2);}
	objd.style.color="#DEE2ED";
	objd2.style.color="#FFFFFF";
	objd.style.textDecoration='none';
	objd2.style.textDecoration='none';
	window.status="";}
	function butclick(gnum3){
		if(linka[gnum3].substring(0,11)=="javascript:"){
			eval(""+linka[gnum3]);}else{if((linka[gnum3].length)>3){
if((trgfrma[gnum3].indexOf("_parent")>-1))
{eval("parent.window.location='"+linka[gnum3]+"'");}
else if((trgfrma[gnum3].indexOf("_top")>-1))
{eval("top.window.location='"+linka[gnum3]+"'");}
else{window.open(''+linka[gnum3],''+trgfrma[gnum3]);}}}}
function dotrans(){if(inoout==false){    
uzuntop--;    if(uzuntop<(-1*toplay))    
{    
uzuntop=0;    
uzunobj2.style.top=220+"px";    
}    
uzunobj.style.top=uzuntop+"px";
if((uzuntop+toplay)<220){uzunobj2.style.top=""+(uzuntop+toplay)+"px";}    }    
if(psy[(uzuntop*(-1))+8]==3){setTimeout('dotrans()',2000+35);}
else{setTimeout('dotrans()',35);}}function initte2()
{i=0;for(i=0;i<mc;i++){objd=document.getElementById('d'+i);heightarr[i]=objd.offsetHeight;}
toplay=8;
for(i=0;i<mc;i++){objd=document.getElementById('d'+i);
objd2=document.getElementById('dz'+i);
objd.style.visibility="visible";
objd2.style.visibility="visible";
objd.style.top=""+toplay+"px";
objd2.style.top=""+toplay+"px";
psy[toplay]=1;
toplay=toplay+heightarr[i]+10;}
uzunobj=document.getElementById('enuzun');
uzunobj.style.left=8+"px";
uzunobj.style.height=toplay+"px";
uzunobj.style.width=tmpv+"px";
uzunobj.style.top=220+"px";
uzunobj2=document.getElementById('enuzun2');
uzunobj2.style.left=8+"px";
uzunobj2.style.height=toplay+"px";
uzunobj2.style.width=tmpv+"px";
uzunobj2.style.top=220+"px";
uzuntop=220;
dotrans();}
function initte(){i=0;innertxt=""+uzun;for(i=0;i<mc;i++)
{innertxt=innertxt+""+divtextb+""+i+""+divtev1+i+",0"+divtev2+i+",0"+divtev3+i+divtev4+divtexts+"<div id=\"hgd"+i+"\""+ns6span+">"+titlea[i]+"<br></div>"+texta[i]+"</div>";}
innertxt=innertxt+"</div>";
innertxt=""+innertxt+uzun2;
for(i=0;i<mc;i++){innertxt=innertxt+""+divtextb2+""+i+""+divtev1+i+",1"+divtev2+i+",1"+divtev3+i+divtev4+divtexts+"<div id=\"hgdz"+i+"\""+ns6span+">"+titlea[i]+"<br></div>"+texta[i]+"</div>";}
innertxt=innertxt+"</div>";
spage=document.getElementById('spagens'); 
spage.innerHTML=""+innertxt;spage.style.left="0px";
spage.style.top="0px";setTimeout('initte2()',100);}
window.onload=initte;











 
