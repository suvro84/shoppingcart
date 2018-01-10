
/* This script and many more are available free online at
The JavaScript Source :: http://javascript.internet.com
Created by: Mike Hudson :: http://www.afrozeus.com */

/*
To change the values in the setupLinks function below.
You will notice there are two arrays for each of Titles and
Links. Currently there are 3 items in each array, but you can easily
expand on that by adding to the array. For example, to add a 4th record,
you would simply include the following 2 lines at the end of setupLinks
function:

arrLinks[3] = "someURL.htm";
arrTitles[3] = "Some title";
*/
function setupLinks() {
  arrLinks[0] = "http://forums.webdeveloper.com/forumdisplay.php?s=&forumid=3";
  arrTitles[0] = "If you have any questions, be sure to visit our forums.";
  arrLinks[1] = "http://javascript.internet.com/new/";
  arrTitles[1] = "Don't miss anything - check our new additions.";
  arrLinks[2] = "http://www.webreference.com/programming/javascript/diaries/";
  arrTitles[2] = "Want to learn JavaScript? Start at the beginning.";
}

var m_iInterval;
var m_Height;
//window.onload = wl;
var iScroll=0;

var arrLinks;
var arrTitles;

var arrCursor = 0;

var arrMax;
window.onload=wl;

function wl() {
  m_iInterval = setInterval(ontimer, 10);
  var base = document.getElementById("jump_base");

  m_Height = base.offsetHeight;

  var divi = parseInt(m_Height/5);
  m_Height = divi*5;

  var td1 = document.getElementById("td1");
  var td2 = document.getElementById("td2");
  var td3 = document.getElementById("td3");
  td1.height = m_Height-5;
  td2.height = m_Height-5;
  td3.height = m_Height-5;

  arrLinks = new Array();
  arrTitles = new Array();

  setupLinks();
  arrMax = arrLinks.length-1;
  setLink();
}
function setLink() {
  var ilink = document.getElementById("jump_link");
  ilink.innerHTML = arrTitles[arrCursor];
  ilink.href = arrLinks[arrCursor];
}
function ontimer() {
  var base = document.getElementById("jump_base");
  iScroll+=5;
  if (iScroll>(m_Height*2)) {
    iScroll=0;
    arrCursor++;
    if (arrCursor>arrMax)
      arrCursor=0;
    setLink();
  }
  if (iScroll==m_Height) {
    pause();
    m_iInterval = setTimeout(resume, 4000);
  }
  base.scrollTop=iScroll;
}
function pause() {
  clearInterval(m_iInterval);
}
function resume() {
  m_iInterval = setInterval(ontimer, 10);
}