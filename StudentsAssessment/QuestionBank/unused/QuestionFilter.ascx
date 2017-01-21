<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="QuestionFilter.ascx.vb" Inherits="StudentsAssessment.QuestionFilter" %>
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
        <td><asp:linkbutton id="lnkSearch" runat="server" ForeColor="White" Font-Underline="false">SEARCH</asp:linkbutton></td>
    </tr>
</table>
