<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="MessageFromAdmin.ascx.vb" Inherits="StudentsAssessment.MessageFromAdmin" %>
<style type="text/css">
	#divMessage{
		width:400px;
		height:150px;
		/**background-color:orange;*/
		color:#fff;
		font-weight:bold;
		padding:5px;
		border:2px solid #000;
        
	}

    #divMessageText{
        overflow-y:auto; /*Auto makes it appear only when overflowing of text occurs in the area*/
        height:100px;
        width:100px;
        color:#fff;
        font-weight:bold;
    }
	
	#divMessage_Header{
		background-color:orange;
		text-align:center;
        width:300px;
        border:2px solid #000;
        color:#fff;
        font-weight:bold;
	}
	
	#divMessage_Footer{
		background-color:dimgray;
		margin-top:10px;
        width:150px;
        border:2px solid #000;
        color:#fff;
        font-weight:bold;
	}
</style>
<div id="divMessage" runat="server">
		<div id="divMessage_Header">
			<asp:Label ID="lblTypeText" runat="server"></asp:Label> ON <asp:Label ID="lblDateActivity" runat="server"/>
		</div>
		<!--Make this area scrollable-->
		<div id="divMessageText" runat="server">
			THIS IS A MESSAGE COMING FROM ADMIN TO ALL STAFF<br />
            THIS IS A MESSAGE COMING FROM ADMIN TO ALL STAFF
            THIS IS A MESSAGE COMING FROM ADMIN TO ALL STAFF
		</div>
		<div id="divMessage_Footer">
			<asp:CheckBox ID="chkDonotShowAgain" runat="server" text="Do not show again" Width="150px" AutoPostBack="true"/>
		</div>
</div>