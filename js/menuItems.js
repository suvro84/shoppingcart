/***********************************************
* Omni Slide Menu script - © John Davenport Scheuer
* very freely adapted from Dynamic-FX Slide-In Menu (v 6.5) script- by maXimus
* This notice MUST stay intact for legal use
* Visit Dynamic Drive at http://www.dynamicdrive.com/ for full original source code
* as first mentioned in http://www.dynamicdrive.com/forums
* username:jscheuer1
***********************************************/

//One global variable to set, use true if you want the menus to reinit when the user changes text size (recommended):
resizereinit=true;

menu[1] = {
id:'menu1', //use unique quoted id (quoted) REQUIRED!!
fontsize:'100%', // express as percentage with the % sign
linkheight:50 ,  // linked horizontal cells height
hdingwidth:210 ,  // heading - non linked horizontal cells width
// Finished configuration. Use default values for all other settings for this particular menu (menu[1]) ///

menuItems:[ // REQUIRED!!
//[name, link, target, colspan, endrow?] - leave 'link' and 'target' blank to make a header
["Admin Sections"], //create header
["Home", "admin_welcome.aspx", ""],
["Change Password", "admin_changepassword.aspx", ""],
["Manage Customer", "AdminUserDetails.aspx",""],
["Manage Venue", "Venue.aspx", ""],
["Manage Performer", "ManagePerformer.aspx", ""],
["Manage Custom Pages", "Contentpage.aspx", ""],
["Manage Featured Category", "ManageFeaturedCategory.aspx", ""],
["Manage Abbreviations", "Managabbr.aspx", ""],
["Manage Errors", "ManageErrors.aspx", ""],
["Manage All Updates", "updatesportevents.aspx", ""],
["Logout", "logout.aspx", ""],


//["FAQ", "http://www.dynamicdrive.com/faqs.htm", "", 1, "no"], //create two column row, requires d_colspan:2 (the default)
//["Email", "http://www.dynamicdrive.com/contact.htm", "",1],

//["External Links", "", ""], //create header
//["JavaScript Kit", "http://www.javascriptkit.com", "_new"],
//["Freewarejava", "http://www.freewarejava.com", "_new"],
//["Coding Forums", "http://www.codingforums.com", "_new"]  //no comma after last entry

]}; // REQUIRED!! do not edit or remove





////////////////////Stop Editing/////////////////

make_menus();