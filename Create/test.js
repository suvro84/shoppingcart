Epoch.prototype.setDays = function (){this.daynames = new Array();var j=0;for(var i=this.startDay; i< this.startDay + 7;i++) {this.daynames[j++] = this.daylist[i];}this.monthDayCount = new Array(31,((this.curDate.getFullYear() - 2000) % 4 ? 28 : 29),31,30,31,30,31,31,30,31,30,31);};