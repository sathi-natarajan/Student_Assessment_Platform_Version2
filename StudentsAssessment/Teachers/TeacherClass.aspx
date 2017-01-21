<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="TeacherClass.aspx.vb" Inherits="StudentsAssessment.TeacherClass" %>

<%@ Register TagPrefix="uc1" TagName="TeachersPanel" Src="TeachersLoggedInPanel.ascx" %>
<%@ Register TagPrefix="uc1" TagName="TeachersMenu" Src="~/Teachers/TeachersMenu.ascx" %>
<%@ Register TagPrefix="uc1" TagName="SiteHeader" Src="~/SiteHeader.ascx" %>
<%@ Register TagPrefix="uc1" TagName="SiteFooter" Src="~/SiteFooter.ascx" %>


<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
     <title>Teachers Interface -Teacher Class</title>
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
        <div id="divCurrentlyActiveClass" runat="server" style="margin-top:-500px;margin-left:-50px;">
            <div id="divActivateClass" runat="server">
                <strong>Please select a class to activate:</strong><br />
            <asp:DropDownList ID="ddlClasses" runat="server"></asp:DropDownList>
           <div class="divCreateQActions" style="width:160px;font-weight:bold;margin-top:-19px;margin-left:100px;">
                <asp:LinkButton ID="lnkActivateClass"  runat="server" Font-underline="False" ForeColor="White">ACTIVATE CLASS</asp:LinkButton>
			</div>
                </div>
            <strong>Currently active class:</strong><asp:label id="lblActiveClass" runat="server"></asp:label>
            <div id="divOnlyclass" runat="server">The currently active class is the only class taught by this teacher</div>
        </div>
        <p>&nbsp;</p>

      <%--  <div id="divAdminPanel" runat="server">
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
        </div>--%>
        
        <div style="margin-left:150px;margin-top:-5px;">
               <h3>ADDING A STUDENT TO THIS TEACHER'S CURRENTLY ACTIVE CLASS OR REMOVING A STUDENT FROM IT</h3>
        <table id="tblTeacherClass" border="0" cellpadding="5" cellspacing="0">
            <tr>
                <td valign="top">
                     <div style="height:55px;background-color:mediumpurple;color:white;">
                            <strong><br />LIST OF STUDENTS ALREADY IN CLASS: <asp:Label ID="lblActiveClass1" runat="server"></asp:Label></strong>
                     </div>
                    <div id="divListClassesTaught_TeacherClass" >
                        &nbsp;<asp:PlaceHolder ID="StudentsAlreadyInHolder" runat="server"></asp:PlaceHolder>
                    </div>
                     
                </td>
                <td valign="top">
                    <div style="height:55px;background-color:mediumpurple;color:white;">
                        <strong>OTHER STUDENTS TO ADD TO CLASS: <asp:Label ID="lblActiveClass2" runat="server"></asp:Label></strong>
                    </div>
                    <div id="divListStudents">
                        <asp:PlaceHolder ID="StudentsNOTInHolder" runat="server"></asp:PlaceHolder>
                    </div>
                </td>
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
                     <asp:LinkButton ID="lnkAddStudents" runat="server" Font-bold="true" Font-Underline="false" 
        ForeColor="White" BackColor="darkgreen">ADD STUDENT TO ACTIVE CLASS</asp:LinkButton>(will move entries from right to left list)<br /><br />
                  <asp:LinkButton ID="lnkRemoveStudents" runat="server" Font-bold="true" Font-Underline="false" 
        ForeColor="White" BackColor="darkgreen">REMOVE STUDENT FROM ACTIVE CLASS</asp:LinkButton>(will move entries from left to right list)<br /><br />
                </td>
            </tr>
        </table>   
            </div>
     
        <p>&nbsp;</p><p>&nbsp;</p><p>&nbsp;</p>
    </div>
    <uc1:SiteFooter id="SiteFooter" runat="server"></uc1:SiteFooter>
    </form>
</body>
</html>
