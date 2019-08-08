<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="Details.aspx.vb" Inherits="HOMS.Landing" %>

<!DOCTYPE html>
<link rel="stylesheet" type="text/css" href="Styles/front/jrz5lhn.css">
<link rel="stylesheet" type="text/css" href="Styles/front/iziModal.min.css" />
<link rel="stylesheet" type="text/css" href="Styles/front/style.css">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
  <meta charset="utf-8">
  <title>Home Owner's Manual</title>
  <meta name="" content="">
  <meta name="" content="">
  <META HTTP-EQUIV="Pragma" CONTENT="no-cache">
  <META HTTP-EQUIV="Expires" CONTENT="-1">
  <style type="text/css" media="print">
    body {
      user-select: none;
      display:none;
      visibility:hidden;
    }
</style>
</head>
<body onmousedown = "return false" onselect = "return false">
     <form id="form1" runat="server">
         <section class="landingpage">
            <div class="banner">
                <img src="Styles/front/images/300pxlogo.png">
            </div>
            <div class="header">
                <asp:Image runat="server" ID="imageMain" />
            </div>
            <div class="infotable">
                <table class="infotablecontent">
                    <tr>
                        <td class="title">Home<br>Type</td>
                        <td class="info">
                            <asp:Label runat="server" ID="phaseCode"></asp:Label><asp:Label runat="server" ID="schematic" cssClass="capitalize"></asp:Label>
                            <br /><asp:Label runat="server" ID="phaseType"></asp:Label><asp:Label runat="server" cssClass="firstCapital" ID="phaseName"></asp:Label>
                            <br /><asp:Label runat="server" ID="unitType"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="title">Owner<br>Name(s)</td>
                        <td class="info">
                            <asp:Label runat="server" ID="ownerName" CssClass="capitalize"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="title">Lot No.</td>
                        <td class="info"><asp:Label runat="server" ID="lotNo"></asp:Label></td>
                    </tr>
                    <tr>
                        <td class="title">Property<br>Address</td>
                        <td class="info">
                            <asp:Label runat="server" ID="address"></asp:Label>
                        </td>
                    </tr>
                </table>
            </div>

            <p  style="font-family:Sans-serif;font-weight:300;color:black;font-size:35px;padding-bottom:20px;">
	            Please click button to view or download.</p>

    <div class="btnrow1">
       <%--<asp:LinkButton OnClientClick="javascript:return openModal(this);" data-izimodal-open="#modal-iframe" ID="homeOwnerManual" runat="server" class="buttonclick trigger">Home Owner's Manual</asp:LinkButton>--%>
       <asp:LinkButton ID="homeOwnerManual" runat="server" target="_blank" class="buttonclick">Home Owner's Manual</asp:LinkButton>
        <asp:LinkButton ID="schematicDrawing" runat="server" target="_blank" class="buttonclick">Schematic Drawing</asp:LinkButton>
    </div>
	<div class="btnrow2">
        <asp:LinkButton ID="defectForm" runat="server" target="_blank" class="buttonclick">Defect Inspection Form</asp:LinkButton>
		<asp:LinkButton ID="residentHouseRules" runat="server" target="_blank" class="buttonclick">Resident House Rules</asp:LinkButton>
    </div> <br>
	
	<p style="font-family:Sans-serif;font-weight:300;color:black;font-size:35px;">Your information here is private & confidential.<br>Please do not share with unauthorised person(s).</p><br>
	<p style="font-family:Sans-serif;font-weight:300;color:black;font-size:25px;">© Copyright 2019 S P Setia Bhd Group. All Rights Reserved.</p>
	<p  style="font-family:Sans-serif;font-weight:300;color:black;font-size:25px;padding-bottom:20px;"><a href="http://www.spsetia.com.my/terms-and-conditions/privacy-policy.htm" target="_blank">Privacy Policy</a> & <a href="http://www.spsetia.com.my/terms-and-conditions/terms-conditions.htm" target="_blank">Terms And Conditions of Use.</p>
     <div id="modal-iframe" data-iziModal-fullscreen="true"  data-iziModal-title="Setia Eco Templer"  data-iziModal-subtitle="Home Owner's Manual"  data-iziModal-icon="icon-home"></div>
      </section>
        <div>
            <asp:Label ID="lblmessage" runat="server" />
            <asp:GridView ID="GridView1" runat="server">
           </asp:GridView>
        </div>
    </form>
</body>
</html>

<script type="text/javascript" src="Styles/js/jquery-3.4.0.min.js"></script>
<script type="text/javascript" src="Styles/js/iziModal.min.js"></script>
<script type="text/javascript" src="Styles/js/homs.js"></script>