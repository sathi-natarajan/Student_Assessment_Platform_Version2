<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="TeacherSubject.aspx.vb" Inherits="StudentsAssessment.TeacherSubject" %>

<%@ Register TagPrefix="uc1" TagName="TeachersPanel" Src="TeachersLoggedInPanel.ascx" %>
<%@ Register TagPrefix="uc1" TagName="TeachersMenu" Src="~/Teachers/TeachersMenu.ascx" %>
<%@ Register TagPrefix="uc1" TagName="SiteHeader" Src="~/SiteHeader.ascx" %>
<%@ Register TagPrefix="uc1" TagName="SiteFooter" Src="~/SiteFooter.ascx" %>


<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
     <title>Teachers Interface -Teacher Subject</title>
    <link href="~/Styles.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
         <uc1:SiteHeader id="SiteHeader1" runat="server"></uc1:SiteHeader>
    <div id="Content" style="margin-top:200px;">
         <div id="divLoggedinPanel">
            <div id="divTeachersLoginInfo">
                <uc1:TeachersPanel id="TeachersPanel1" runat="server"></uc1:TeachersPanel>
	        </div>
            <div id="divNotLoggedIn1" runat="server" style="background-color:#000;color:#fff;font-weight:bold;">
                NO TEACHER OR ADMIN IS CURRENTLY LOGGED IN
            </div>
        </div>
        <br /><br /><br />
        <div id="divTeachersMenuQuestionbank" style="margin-top:-5px;margin-left:-50px;">
	        <uc1:TeachersMenu id="TeachersMenu1" runat="server"></uc1:TeachersMenu>
        </div>  
          <div id="divCurrentlyActiveSubject" runat="server" style="margin-top:-500px;margin-left:-50px;">
            <div id="divActivateSubject" runat="server">
                 <strong>Please select a subject to activate:</strong><br />
                <asp:DropDownList ID="ddlSubjects" runat="server"></asp:DropDownList>
                <div class="divCreateQActions" style="width:160px;font-weight:bold;margin-top:-19px;margin-left:200px;">
                <asp:LinkButton ID="lnkActivateSubject"  runat="server" Font-underline="False" ForeColor="White">ACTIVATE SUBJECT</asp:LinkButton>
			</div>
            </div>
            <strong>Currently active subject: </strong><asp:label id="lblActiveSubject" runat="server"></asp:label>
            <div id="divOnlysubject" runat="server">The currently active subject is the only subject taught by this teacher</div>
        </div>
        <p>&nbsp;</p>
        <%-- <div id="divAdminPanel" runat="server">
            <h3>ADMIN PANEL</h3>
            <div id="divListOfTeachersinAdminPanel">
                <strong>Select a teacher for whom to list the classes in the classes list:<br />
                </strong>
                <asp:PlaceHolder ID="TeachersHolder" runat="server"></asp:PlaceHolder><br />
            </div>
            <br /><br />
            <div id="divAdminPanelButton">
                    <asp:LinkButton ID="lnkLoadTeachers" runat="server" Font-bold="true" Font-Underline="false" 
                ForeColor="White" BackColor="darkgreen">&nbsp;&nbsp;&nbsp;LOAD CLASSES TAUGHT BY THIS TEACHER</asp:LinkButton>
            </div>
        </div>
        <p>&nbsp;</p>--%>
        <div style="margin-left:150px;margin-top:-35px;">
                <h3>ADDING CURRENTLY ACTIVE SUBJECT TO THIS TEACHER'S CLASS OR REMOVING IT FROM THIS TEACHER'S CLASS</h3>
        <table id="tblTeacherClass" border="0" cellpadding="5" cellspacing="0">
            <tr>
                <td valign="top">
                     <div style="height:55px;background-color:mediumpurple;color:white;">
                            <strong>THIS TEACHER ALREADY TEACHES <asp:Label ID="lblActiveSubject1" runat="server"></asp:Label>
                                IN
                            </strong>
                     </div>
                    <div id="divListClassesTaught_TeacherSubject" >
                        &nbsp;<asp:PlaceHolder ID="ClassesHolderLeft" runat="server"></asp:PlaceHolder>
                    </div>
                     
                </td>
                <td valign="top">
                    <div style="height:55px;background-color:mediumpurple;color:white;">
                        <strong>OTHER CLASSES THIS TEACHER CAN START TEACHING <asp:Label ID="lblActiveSubject2" runat="server"></asp:Label> IN</strong>
                    </div>
                    <div id="divListSubjects">
                        <asp:PlaceHolder ID="ClassesHolderRight" runat="server"></asp:PlaceHolder>
                    </div>
                </td>
               <%-- <td valign="top">
                    <div id="divAdminPanel" runat="server">
                         <div  style="height:35px;background-color:mediumpurple;color:white;">
                        <strong>ADMIN PANEL</strong>
                    </div>
                    <div id="divListOfTeachersinAdminPanel">
                        <strong>Select a teacher for whom to list the classes in the classes list:<br />
                        </strong>
                        <asp:PlaceHolder ID="TeachersHolder" runat="server"></asp:PlaceHolder><br />
                    </div>
                    </div>
                   
                </td>--%>
            </tr>
            <tr>
                <td colspan="2">
                    <asp:Label ID="lblStatus" runat="server" CSSClass="Statusarea" Font-Bold="true"></asp:Label>
                </td>
            </tr>
        </table>
        <table id="tblButtons" border="0" cellpadding="0" cellspacing="0">
            <tr>
                <td colspan="2">
                    <asp:LinkButton ID="lnkReturn" runat="server" Font-bold="true" Font-Underline="false" 
        ForeColor="White" BackColor="darkgreen">RETURN</asp:LinkButton><br /><br />
                     <asp:LinkButton ID="lnkAddSubjects" runat="server" Font-bold="true" Font-Underline="false" 
        ForeColor="White" BackColor="darkgreen">ADD SUBJECT TO CLASS</asp:LinkButton>(will move entries from right to left list)<br /><br />
                  <asp:LinkButton ID="lnkRemoveSubject" runat="server" Font-bold="true" Font-Underline="false" 
        ForeColor="White" BackColor="darkgreen">REMOVE SUBJECT FROM CLASS</asp:LinkButton>(will move entries from left to right list)
                    
                </td>
                <%--  <td>
                    <div id="divAdminPanelButton" runat="server">
                         <asp:LinkButton ID="lnkLoadTeachers" runat="server" Font-bold="true" Font-Underline="false" 
                     ForeColor="White" BackColor="darkgreen">LOAD CLASSES TAUGHT BY THIS TEACHER</asp:LinkButton>
                    </div>
                </td>--%>
            </tr>
        </table> 
        </div>
      
        <p>&nbsp;</p><p>&nbsp;</p><p>&nbsp;</p>
    </div>
    <uc1:SiteFooter id="SiteFooter" runat="server"></uc1:SiteFooter>
    </form>
</body>
</html>
