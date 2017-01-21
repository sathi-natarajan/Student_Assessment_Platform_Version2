<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="TeacherProfile.aspx.vb" Inherits="StudentsAssessment.TeacherProfile" %>

<%@ Register TagPrefix="uc1" TagName="SiteHeader" Src="~/SiteHeader.ascx" %>
<%@ Register TagPrefix="uc1" TagName="SiteFooter" Src="~/SiteFooter.ascx" %>
<%@ Register TagPrefix="uc1" TagName="TeachersMenu" Src="~/Teachers/TeachersMenu.ascx" %>
<%@ Register TagPrefix="uc1" TagName="TeachersPanel" Src="~/Teachers/TeachersLoggedInPanel.ascx" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
     <title>Teachers Interface -Teacher Class</title>
    <link href="~/Styles.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        <uc1:SiteHeader id="SiteHeader1" runat="server"></uc1:SiteHeader>
        <div id="Content" style="margin-top:200px;">
            <div id="divTeachersLoginInfo">
                <uc1:TeachersPanel id="TeachersPanel1" runat="server"></uc1:TeachersPanel>
	        </div>
            <div id="divNotLoggedIn1" runat="server" style="background-color:#000;color:#fff;font-weight:bold;">
                NO TEACHER OR ADMIN IS CURRENTLY LOGGED IN
            </div>
            <p>&nbsp;</p><p>&nbsp;</p>
            <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                    <table border="0" cellpadding="3" cellspacing="3">
                        <colgroup>
                            <col width="100px" valign="top" />
                            <col width="800px" valign="top"/>
                            <col width="600px" valign="top"/>
                        </colgroup>
                        <tr>
                            <td>
                               <div id="divTeachersMenuQuestionbank" style="margin-top:-500px;">
	                                <uc1:TeachersMenu id="TeachersMenu1" runat="server"></uc1:TeachersMenu>
                                </div>  
                            </td>
                            <td>
                                <div style="background-color:mediumpurple;text-align:center;border:solid 2px white;width:600px;">
                                    <h3>PROFILE FOR <asp:Label ID="lblTeachername" runat="server"></asp:Label></h3>
                                </div>                                                           
                                <div id="divProfile" runat="server">
                                    <table id="tblProfile" cellpadding="3" cellspacing="3" border="0">
                                        <tr>
                                            <td>First name: </td>
                                            <td align="left"><asp:textbox ID="txtFirstname" runat="server" MaxLength="25" CssClass="txtFlatBorders"></asp:textbox></td>
                                        </tr>
                                        <tr>
                                          <td>Last name: </td>
                                            <td align="left"><asp:textbox ID="txtLastname" runat="server" MaxLength="25" CssClass="txtFlatBorders"></asp:textbox></td>
                                        </tr>
                                        <tr>
                                          <td>Description:</td>
                                            <td align="left">
                                               <asp:textbox ID="txtDesc" runat="server" MaxLength="25" 
                                                CssClass="ProfileTextboxes" TextMode="Multiline" Rows="5"></asp:textbox>
                                            </td>
                                        </tr>
                                        <tr>
                                          <td>User name: </td>
                                          <td align="left"><asp:textbox ID="txtUsername" runat="server" MaxLength="25" CssClass="txtFlatBorders"></asp:textbox></td>
                                        </tr>
                                        <tr>
                                          <td>Password: </td>
                                          <td align="left"><asp:textbox ID="txtPassword" runat="server" MaxLength="25" TextMode="Password" CssClass="txtFlatBorders"></asp:textbox></td>
                                        </tr>
                                        <tr>
                                          <td>Date of join:</td>
                                          <td align="left">
                                              <asp:textbox ID="txtJoindate" runat="server" MaxLength="25" CssClass="txtFlatBorders"></asp:textbox>
                                              <ajaxToolkit:CalendarExtender ID="CalendarExtender1" TargetControlID="txtJoindate" runat="server" />
                                          </td>
                                        </tr>
                                        <tr>
                                            <td>Date of termination (if any): </td>
                                            <td align="left">
                                                <asp:textbox ID="txtTermdate" runat="server" MaxLength="25" CssClass="txtFlatBorders"></asp:textbox>
                                               <ajaxToolkit:CalendarExtender ID="CalendarExtender2" TargetControlID="txtTermdate" runat="server" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td >Part-time staff?: </td>
                                            <td align="left">
                                                <asp:RadiobuttonList ID="chkblPT" runat="server">
                                                    <asp:ListItem Text="Yes" Value="yes"></asp:ListItem>
                                                    <asp:ListItem Text="No" Value="No"></asp:ListItem>
                                                </asp:RadiobuttonList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>Is staff an admin?: </td>
                                            <td align="left">
                                                <asp:RadiobuttonList ID="chkblAdmin" runat="server">
                                                    <asp:ListItem Text="Yes" Value="yes"></asp:ListItem>
                                                    <asp:ListItem Text="No" Value="No"></asp:ListItem>
                                                </asp:RadiobuttonList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>Is staff ACTIVE?: </td>
                                            <td align="left">
                                                <asp:RadiobuttonList ID="chkblActive" runat="server">
                                                    <asp:ListItem Text="Yes" Value="yes"></asp:ListItem>
                                                    <asp:ListItem Text="No" Value="No"></asp:ListItem>
                                                </asp:RadiobuttonList>
                                            </td>
                                        </tr>
                                          <tr>
                                            <td colspan="2">
                                                <div style="height:25px;background-color:mediumpurple;color:white;width:600px;">
                                                    <strong>SCHOOLS <asp:Label ID="lblTeachername4" runat="server"></asp:Label> BELONGS TO</strong>
                                                </div>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="2">
                                                <div id="divSchoolsBelonging" >
                                                    &nbsp;<asp:PlaceHolder ID="SchoolsBelongingHolder" runat="server"></asp:PlaceHolder>
                                                </div>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="2">
                                                <div style="height:25px;background-color:mediumpurple;color:white;width:600px;">
                                                    <strong>CLASSES TAUGHT BY <asp:Label ID="lblTeachername1" runat="server"></asp:Label></strong>
                                                </div>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="2">
                                                <div id="divListClassesTaught" >
                                                    &nbsp;<asp:PlaceHolder ID="ClassesHolder" runat="server"></asp:PlaceHolder>
                                                </div>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="2">
                                                <div style="height:25px;background-color:mediumpurple;color:white;width:600px;">
                                                    <strong>SUBJECTS TAUGHT BY <asp:Label ID="lblTeachername2" runat="server"></asp:Label></strong>
                                                </div>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="2">
                                                <div id="divListSubjectsTaught">
                                                    <asp:PlaceHolder ID="SubjectsHolder1" runat="server"></asp:PlaceHolder>
                                                </div>
                                            </td>
                                        </tr>
                                         <tr>
                                            <td colspan="2">
                                                <div style="height:25px;background-color:mediumpurple;color:white;width:600px;">
                                                    <strong>STUDENTS TAUGHT BY <asp:Label ID="lblTeachername3" runat="server"></asp:Label></strong>
                                                </div>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="2">
                                                <div id="divListStudentsTaught">
                                                    <asp:PlaceHolder ID="StudentsHolder1" runat="server"></asp:PlaceHolder>
                                                </div>
                                            </td>
                                        </tr>
                                       
                                    </table>
                                </div><br /><br />
                                <asp:LinkButton ID="lnkSaveProfile" runat="server" BackColor="darkgreen" Font-bold="true" Font-Underline="false"
                                ForeColor="White">SAVE PROFILE</asp:LinkButton>&nbsp;&nbsp;&nbsp;&nbsp;
                                <asp:LinkButton ID="lnkReturn" runat="server" Font-bold="true" Font-Underline="false" 
                                ForeColor="White" BackColor="darkgreen">RETURN</asp:LinkButton><br />
                               <asp:Label ID="lblStatus" runat="server" CSSClass="Statusarea" Font-Bold="true"></asp:Label>           
                            </td>
                           </tr>
                    </table>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
        <p>&nbsp;</p><p>&nbsp;</p><p>&nbsp;</p>
    <uc1:SiteFooter id="SiteFooter" runat="server"></uc1:SiteFooter>
    </form>
</body>
</html>
