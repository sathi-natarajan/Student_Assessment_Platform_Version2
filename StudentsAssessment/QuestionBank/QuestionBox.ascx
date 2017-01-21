<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="QuestionBox.ascx.vb" Inherits="StudentsAssessment.QuestionBox" %>
<table id="tblQuestionBox" border="1" cellspacing="0" cellpadding="0" style="width:450px">
		<colgroup>
			<col width="350px"/>
			<col width="200px"/>
			<col width="50px"/>
			<col width="15px"/> <!--Edit-->
	        <col width="15px"/> <!--Trash-->
		</colgroup>
		<tr>
			<td colspan="3">
				<div id="divQuestionNo" runat="server"></div>
			</td>
			<td rowspan="3">
                <asp:LinkButton ID="lnkEditQuestion" runat="server" Font-underline="False" ForeColor="White">E<br/>D<br/>I<br/>T</asp:LinkButton>
			</td>
			<td rowspan="3">
                <asp:LinkButton ID="lnkDeleteQuestion" runat="server" Font-underline="False" ForeColor="White">T<br/>R<br/>A<br/>S</br/>H</asp:LinkButton>
			</td>
			<td rowspan="3">
                <asp:LinkButton ID="lnkDuplicateQID" runat="server" Font-underline="False" ForeColor="White">
				    DUPL. <br/>QUESTION.<br/> NEW ID
                </asp:LinkButton>
			</td>
			<td  rowspan="3">
                <asp:LinkButton ID="lnkAddtoAssessment" runat="server" Font-underline="False" ForeColor="White">
				    ADD TO ASSESSMENT
                </asp:LinkButton>
			</td>
		</tr>
		<tr style="height:50px;">
			<td>
                <div id="divQSnapshot" runat="server">SNAPSNAPSHOT OF QUESTION</div>
                <div id="divEditQSnapshot" runat="server">
                    <asp:TextBox ID="txtQuestionSnapshot" runat="server" TextMode="MultiLine" Width="220px"></asp:TextBox>
                </div>
			</td>
		</tr>
		<tr>
			<td colspan="3">
				<div id="divStandard" runat="server">STANDARD</div>
			</td>
		</tr>
	</table>