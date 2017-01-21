<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="QBOtherfeatures.ascx.vb" Inherits="StudentsAssessment.QBOtherfeatures" %>
<div id="divAddExplanation" runat="server">
    <h3>ADD EXPLANATION TO QUESTION</h3>
    <strong>Please type an explanation for the created question below that is 250 characters or less</strong>:<br />
    <asp:textbox id="txtExplanation" runat="server" textmode="Multiline" Rows="5" MaxLength="250" Height="76px" Width="378px"></asp:textbox>
    <p>
        <asp:LinkButton ID="lnkOK"  runat="server" Font-underline="False" CSSClass="divCreateQActions" ForeColor="White">OK</asp:LinkButton>
        <asp:LinkButton ID="lnkCancel"  runat="server" Font-underline="False" CSSClass="divCreateQActions" ForeColor="White">CANCEL</asp:LinkButton>
    </p>
</div>

<div id="divAddImage" runat="server">
    <h3>ADD AN IMAGE TO QUESTION</h3>
    <strong>Please click on "Browse..." button below to select an image from your computer:</strong><br />
    <asp:FileUpload ID="fupldImage" runat="server" Height="27px" Width="383px" />
    <p>
        <asp:LinkButton ID="lnkOK1"  runat="server" Font-underline="False" CSSClass="divCreateQActions" ForeColor="White">OK</asp:LinkButton>
        <asp:LinkButton ID="lnkCancel1"  runat="server" Font-underline="False" CSSClass="divCreateQActions" ForeColor="White">CANCEL</asp:LinkButton>
    </p>
</div>

<div id="divStandardFilter" runat="server">
    <h3>SELECTED FILTER: STANDARD</h3>
    <strong>Please choose the desired standard from the list below.  The list of questions will then be filtered by the chosen standard.</strong><br /><br />
    <asp:DropDownList ID="ddlStandards" runat="server">
    </asp:DropDownList>
    <p>
        <asp:LinkButton ID="lnkOK2"  runat="server" Font-underline="False" CSSClass="divCreateQActions" ForeColor="White">OK</asp:LinkButton>
        <asp:LinkButton ID="lnkCancel2"  runat="server" Font-underline="False" CSSClass="divCreateQActions" ForeColor="White">CANCEL</asp:LinkButton>
    </p>
</div>

<div id="divSubjectFilter" runat="server">
    <h3>SELECTED FILTER: SUBJECTS</h3>
    <strong>Please choose the desired subject from the list below.  The list of questions will then be filtered by the chosen subject.</strong><br /><br />
    <asp:DropDownList ID="ddlSubjects" runat="server">
    </asp:DropDownList>
    <br />
    <br />
    <p>
        <asp:LinkButton ID="lnkOK3"  runat="server" Font-underline="False" CSSClass="divCreateQActions" ForeColor="White">OK</asp:LinkButton>
        <asp:LinkButton ID="lnkCancel3"  runat="server" Font-underline="False" CSSClass="divCreateQActions" ForeColor="White">CANCEL</asp:LinkButton>
    </p>
</div>

<div id="divGradeFilter" runat="server">
    <h3>SELECTED FILTER: GRADE</h3>
    <strong>Please choose the desired grade level from the list below.  The list of questions will then be filtered by the chosen grade level.</strong><br /><br />
    <asp:DropDownList ID="ddlGrades" runat="server">
    </asp:DropDownList>
    <br />
    <br />
    <p>
        <asp:LinkButton ID="lnkOK4"  runat="server" Font-underline="False" CSSClass="divCreateQActions" ForeColor="White">OK</asp:LinkButton>
        <asp:LinkButton ID="lnkCancel4"  runat="server" Font-underline="False" CSSClass="divCreateQActions" ForeColor="White">CANCEL</asp:LinkButton>
    </p>
</div>

<div id="divQIDFilter" runat="server">
    <h3>SELECTED FILTER: QUESTION ID</h3>
    <strong>Please choose the desired question ID from the list below.  The list of questions will then be filtered by the chosen question ID.</strong><br /><br />
    <asp:DropDownList ID="ddlQIDs" runat="server">
    </asp:DropDownList>
    <br />
    <br />
    <p>
        <asp:LinkButton ID="lnkOK5"  runat="server" Font-underline="False" CSSClass="divCreateQActions" ForeColor="White">OK</asp:LinkButton>
        <asp:LinkButton ID="lnkCancel5"  runat="server" Font-underline="False" CSSClass="divCreateQActions" ForeColor="White">CANCEL</asp:LinkButton>
    </p>
</div>

<div id="divQTypeFilter" runat="server">
    <h3>SELECTED FILTER: QUESTION TYPE</h3>
    <strong>Please choose the desired question TYPE from the list below.  The list of questions will then be filtered by the chosen question TYPE.</strong><br /><br />
    <asp:DropDownList ID="ddlQTypes" runat="server">
    </asp:DropDownList>
    <br />
    <br />
    <p>
        <asp:LinkButton ID="lnkOK6"  runat="server" Font-underline="False" CSSClass="divCreateQActions" ForeColor="White">OK</asp:LinkButton>
        <asp:LinkButton ID="lnkCancel6"  runat="server" Font-underline="False" CSSClass="divCreateQActions" ForeColor="White">CANCEL</asp:LinkButton>
    </p>
</div>

<div id="divSearchFilter" runat="server">
    <h3>SELECTED FILTER: SEARCH</h3>
    <strong>Please choose the type of search you want to perform.  Then type appropriate search term in the text box below:</strong><br /><br />
    <asp:RadioButtonList ID="rblSearchtypes" runat="server" RepeatDirection="Horizontal">
        <asp:ListItem Text="Search by explanation" Value="1" Selected="true"></asp:ListItem>
        <asp:ListItem Text="Search by words in question text" Value="1"></asp:ListItem>
    </asp:RadioButtonList><br />
    <asp:TextBox ID="txtSearchTerm" runat="server"></asp:TextBox>
    <br />
    <br />
    <p>
        <asp:LinkButton ID="lnkOK7"  runat="server" Font-underline="False" CSSClass="divCreateQActions" ForeColor="White">OK</asp:LinkButton>
        <asp:LinkButton ID="lnkCancel7"  runat="server" Font-underline="False" CSSClass="divCreateQActions" ForeColor="White">CANCEL</asp:LinkButton>
    </p>
</div>

<asp:Label ID="lblStatus" runat="server" CssClass="Statusarea"></asp:Label>