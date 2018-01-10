<%@ Page Language="C#" AutoEventWireup="true" CodeFile="video.aspx.cs" Inherits="video" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Untitled Page</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <object id="objMediaPlayer" width="320" height="282" codebase="http://activex.microsoft.com/activex/controls/mplayer/en/nsmp2inf.cab#Version=5,1,52,701" classid="CLSID:22d6f312-b0f6-11d0-94ab-0080c74c7e95" standby="Loading Microsoft Windows Media Player components..." type = "application/x-mplayer2" VIEWASTEXT>
<param name="fileName" value="http://example.com/video.asp?file={5D550BAD-F77B-4442-BC0E-36B9AA6BEE73}&bitrate=384k&extension=WMV">
<param name="animationatStart" value="true">
<param name="transparentatStart" value="true">
<param name="autoStart" value="true">
<param name="showControls" value="true">
<param name="volume" value="0">
<embed src="E:\G\Entertainment\VideoSongs\Awara bhamra.dat" width="320" height="282" volume="0" type="video/x-ms-wmv">
</object>



<%--<object id="MediaPlayer" height="200" classid="CLSID:22D6F312-B0F6-11D0-94AB-0080C74C7E95"
standby="Loading Windows Media Player components..." type="application/x-oleobject" width="200">
<param name="FileName" value="mobile.wmv">
<param name="ShowControls" value="true">
<param name="ShowStatusBar" value="false">
<param name="ShowDisplay" value="false">
<param name="autostart" value="true">
<embed type="application/x-mplayer2" src="E:\G\Entertainment\VideoSongs\Awara bhamra.dat" name="MediaPlayer" width="100%"
height="190" showcontrols="1" showstatusbar="0" showdisplay="0" autostart="0"> </embed>
</object>--%>



 <%--<object id='Object1' width="320" height="285" 
      classid='CLSID:22d6f312-b0f6-11d0-94ab-0080c74c7e95' 
      codebase='http://activex.microsoft.com/activex/controls/mplayer/en/nsmp2inf.cab#Version=5,1,52,701'
      standby='Loading Microsoft Windows Media Player components...' type='application/x-oleobject'>
      <param name='fileName' value="E:\G\Entertainment\VideoSongs\Awara bhamra.dat"/>
      <param name='animationatStart' value='true'/>
      <param name='transparentatStart' value='true'/>
      <param name='autoStart' value="true"/>
      <param name='showControls' value="true"/>
      <param name='loop' value="false"/>     
      </object>--%>

<%--<OBJECT ID="Player"
CLASSID="CLSID:6BF52A52-394A-11d3-B153-00C04F79FAA6"
VIEWASTEXT>
                <PARAM name="autoStart" value="True">
                <PARAM name="URL" value="SomeMediaFile.mpg">
                <PARAM name="rate" value="1">
                <PARAM name="balance" value="0">
                <PARAM name="enabled" value="true">
                <PARAM name="enabledContextMenu" value="true">
                <PARAM name="fullScreen" value="false">
                <PARAM name="playCount" value="1">
                <PARAM name="volume" value="100">
</OBJECT>
--%>


<%--<object id='mediaPlayer' width="320" height="285" 
      classid='CLSID:22d6f312-b0f6-11d0-94ab-0080c74c7e95' 
      codebase='http://activex.microsoft.com/activex/controls/mplayer/en/nsmp2inf.cab#Version=5,1,52,701'
      standby='Loading Microsoft Windows Media Player components...' type='application/x-oleobject'>
      <param name='fileName' value="E:\Dhak\Dhak.dat"/>
      <param name='animationatStart' value='true'/>
      <param name='transparentatStart' value='true'/>
      <param name='autoStart' value="true"/>
      <param name='showControls' value="true"/>
      <param name='loop' value="false"/>     
      </object>--%>
      
      
      <%--<object width="320" height="240"
classid="clsid:CFCDAA03-8BE4-11cf-B84B-0020AFBBCCFA">
<param name="controls" value="ImageWindow" />
<param name="autostart" value="true" />
<param name="src" value="male.ram" />
</object>--%>
    </div>
    </form>
</body>
</html>
