// JScript File

<!-- Paste this code into an external JavaScript file named: nextPrevious.js  -->

/* This script and many more are available free online at
The JavaScript Source :: http://javascript.internet.com
Created by: Solomon, the Sleuth :: http://www.freewebs.com/thesleuth/scripts/ */

// List image names without extension
var myImg= new Array(3)
  myImg[0]= "pix1";
  myImg[1]= "pix2";
  myImg[2]= "pix3";
  myImg[3]= "pix4";
  
//<A HREF="file:///F:\net05_pro\ajax_sample\slide_images\">file:///F:\net05_pro\ajax_sample\slide_images\</A>

// Tell browser where to find the image
//myImgSrc = "/img";
myImgSrc = "../slide_images/";


// Tell browser the type of file
myImgEnd = ".jpg"

var i = 0;

// Create function to load image
function loadImg(){
  document.imgSrc.src = myImgSrc + myImg[i] + myImgEnd;
}

// Create link function to switch image backward
function prev(){
  if(i<1){
    var l = i
  } else {
    var l = i-=1;
  }
  document.imgSrc.src = myImgSrc + myImg[l] + myImgEnd;
}

// Create link function to switch image forward
function next(){
  if(i>2){
    var l = i
  } else {
    var l = i+=1;
  }
  document.imgSrc.src = myImgSrc + myImg[l] + myImgEnd;
}

// Load function after page loads
window.onload=loadImg;
