var sc_width=screen.width;var sc_height=screen.height;var sc_referer=""+document.referrer;try{sc_referer=""+parent.document.referrer}catch(ex){sc_referer=""+document.referrer}var sc_os="";var sc_title="";var sc_url="";var sc_unique=0;var sc_returning=0;var sc_returns=0;var sc_base_dir;var sc_click_dir;var sc_error=0;var sc_remove=0;var sc_http_url="http";var sc_link_back_start="";var sc_link_back_end="";var sc_security_code="";var sc_cls=-1;var sc_host="statcounter.com";if(window.sc_click_stat){sc_cls=window.sc_click_stat}if(window.sc_https){if(sc_https==1){sc_doc_loc=''+document.location;myRE=new RegExp("^https","i");if(sc_doc_loc.match(myRE)){sc_http_url="https"}}}if(window.sc_local){sc_base_dir=sc_local}else{if(window.sc_partition){if(sc_cls==-1&&sc_partition==3){sc_cls=1}var sc_counter="";if(window.sc_partition!=34&&sc_partition<=45){sc_counter=sc_partition+1}sc_base_dir=sc_http_url+"://c"+sc_counter+"."+sc_host+"/"}else{sc_base_dir=sc_http_url+"://c1."+sc_host+"/"}}sc_click_dir=sc_base_dir;if(window.sc_text){sc_base_dir+="text.php?"}else{sc_base_dir+="t.php?"}if(window.sc_project){sc_base_dir+="sc_project="+sc_project}else if(window.usr){sc_base_dir+="usr="+usr}else{sc_error=1}if(window.sc_remove_link){sc_link_back_start="";sc_link_back_end=""}else{sc_link_back_start="<a class=\"statcounter\" href=\"http://www."+sc_host+"\" target=\"_blank\">";sc_link_back_end="<\/a>"}sc_date=new Date();sc_time=sc_date.getTime();sc_time_difference=3600000;sc_title=""+document.title;sc_url=""+document.location;sc_referer=sc_referer.substring(0,255);sc_title=sc_title.substring(0,150);sc_url=sc_url.substring(0,150);sc_referer=escape(sc_referer);if(encodeURIComponent){sc_title=encodeURIComponent(sc_title)}else{sc_title=escape(sc_title)}sc_url=escape(sc_url);if(window.sc_security){sc_security_code=sc_security}var sc_tracking_url=sc_base_dir+"&resolution="+sc_width+"&h="+sc_height+"&camefrom="+sc_referer+"&u="+sc_url+"&t="+sc_title+"&java=1&security="+sc_security_code+"&sc_random="+Math.random();var sc_clstr="<span class=\"statcounter\">";var sc_cltext="\" alt=\"StatCounter - Free Web Tracker and Counter\" border=\"0\">";var sc_strout=sc_clstr+sc_link_back_start+"<img src=\""+sc_tracking_url+sc_cltext+sc_link_back_end+"</span>";if(sc_error==1){document.writeln("Code corrupted. Insert fresh copy.")}else if(sc_remove==1){}else if(window.sc_invisible){if(window.sc_invisible==1){if(window.sc_call){sc_call++}else{sc_call=1}eval("var sc_img"+sc_call+" = new Image();sc_img"+sc_call+".src = \""+sc_tracking_url+"&invisible=1\"")}else{document.writeln(sc_strout)}}else if(window.sc_text){document.writeln('<scr'+'ipt language="JavaScript"'+' src='+sc_tracking_url+"&text="+sc_text+'></scr'+'ipt>')}else{document.writeln(sc_strout)}if(sc_cls>0){if(clickstat_done!=1){var clickstat_done=1;var clickstat_project=window.sc_project;var clickstat_security=window.sc_security_code;var dlext="7z|aac|avi|csv|doc|exe|flv|gif|gz|jpe?g|js|mp(3|4|e?g)|mov|pdf|phps|png|ppt|rar|sit|tar|torrent|txt|wma|wmv|xls|xml|zip";if(typeof(window.sc_download_type)=='string'){dlext=window.sc_download_type}var ltype="https?|ftp|telnet|ssh|ssl|mailto";var second="ac|co|gov|ltd|me|mod|net|nic|nhs|org|plc|police|sch|com";var dl=new RegExp("\\.("+dlext+")$","i");var lnk=new RegExp("^("+ltype+"):","i");var domsec=new RegExp("\^("+second+")$","i");var host_name=location.host.replace(/^www\./i,"");var host_splitted=host_name.split(".");var domain=host_splitted.pop();var host_split=host_splitted.pop();if(domsec.test(host_split)){domain=host_split+"."+domain;host_split=host_splitted.pop()}domain=host_split+"."+domain;var lnklocal_mask="^https?:\/\/(.*)"+domain;var lnklocal=new RegExp(lnklocal_mask,"i");if(document.getElementsByTagName){var anchors=document.getElementsByTagName('a');for(var i=0;i<anchors.length;i++){var anchor=anchors[i];if(anchor.onmousedown){var original_click=anchor.onmousedown;var s=original_click.toString().split("\n").join(" ");var bs=s.indexOf('{');var head=s.substr(0,bs);var ps=head.indexOf('(');var pe=head.indexOf(')');var params=head.substring(ps+1,pe);var plist=params.split(",");var body=s.substr(bs+1,s.length-bs-2);var insert="sc_clickstat_call(this,'"+sc_click_dir+"');";var final_body=insert+body;var ev_head="new Function (";var ev_params="";var ev_sep="";for(var sc_i=0;sc_i<plist.length;sc_i++){ev_params=ev_sep+"'"+plist[sc_i]+"'";ev_sep=","}if(ev_sep==","){ev_params+=","}var ev_foot="final_body);";var ev_final=ev_head+ev_params+ev_foot;anchor.onmousedown=eval(ev_final)}else{anchor.onmousedown=new Function("event","sc_clickstat_call(this,'"+sc_click_dir+"');return true;")}}}function sc_none(){return}function sc_clickstat_call(adata,sc_click_dir){if(adata){var clickmode=0;if(lnk.test(adata)){if((lnklocal.test(adata))){if(dl.test(adata)){clickmode=1}else{if(sc_cls==2){clickmode=2}}}else{clickmode=2}}if(clickmode!=0){var sc_link=escape(adata);if(sc_link.length>0){var sc_req=sc_click_dir+"click.gif?sc_project="+clickstat_project+"&security="+clickstat_security+"&c="+sc_link+"&m="+clickmode+"&rand="+Math.random();var sc_req_image=new Image(1,1);sc_req_image.onload=sc_none;sc_req_image.src=sc_req;var d=typeof(window.sc_delay)!="undefined"?sc_delay:250;var n=new Date();var t=n.getTime()+d;while(n.getTime()<t)var n=new Date()}}}}}}