<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="QBFilters.ascx.vb" Inherits="StudentsAssessment.QBFilters" %>
<table id="tblQuestionFilter" border="0" cellpadding="2" cellspacing="3">
    <tr>
        <td>FILTERS</td>
    </tr>
    <tr>
        <td style="background-color:white">&nbsp;</td>
    </tr>
    <tr>
        <td><asp:linkbutton id="lnkGradefilter" runat="server" ForeColor="White" Font-Underline="false">GRADE</asp:linkbutton></td>
    </tr>
    <tr>
        <td><asp:linkbutton id="lnkSubjectfilter" runat="server" ForeColor="White" Font-Underline="false">SUBJECT</asp:linkbutton></td>
    </tr>
    <tr>
        <td><asp:linkbutton id="lnkStandardfilter" runat="server" ForeColor="White" Font-Underline="false">STANDARD</asp:linkbutton></td>
    </tr>
    <tr>
        <td><asp:linkbutton id="lnkQIDfilter" runat="server" ForeColor="White" Font-Underline="false">Q ID</asp:linkbutton></td>
    </tr>
    <tr>
        <td><asp:linkbutton id="lnkQuestiontype" runat="server" ForeColor="White" Font-Underline="false">QUES. TYPE</asp:linkbutton></td>
    </tr>
    <tr>
        <td><asp:linkbutton id="lnkSearch" runat="server" ForeColor="White" Font-Underline="false">SEARCH</asp:linkbutton></td>
    </tr> 
    <tr>
        <td style="background-color:white">&nbsp;</td>
    </tr>
     <tr>
        <td><asp:linkbutton id="lnkShowall" runat="server" ForeColor="White" Font-Underline="false">SHOW ALL <br />QUESTIONS</asp:linkbutton></td>
    </tr> 
    <%--<tr id="trButtons" runat="server" visible="false">
       <td>
            <asp:linkbutton id="lnkOK" runat="server" ForeColor="White" Font-Underline="false" Visible="false">OK</asp:linkbutton>
        </td>  
    </tr>
    <tr id="trButtons1" runat="server" visible="false">
        <td><asp:linkbutton id="lnkCancel" runat="server" ForeColor="White" Font-Underline="false" Visible="false">CANCEL</asp:linkbutton></td>
    </tr>--%>
</table>