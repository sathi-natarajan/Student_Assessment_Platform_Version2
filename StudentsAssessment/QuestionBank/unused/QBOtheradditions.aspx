<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="QBOtheradditions.aspx.vb" Inherits="StudentsAssessment.QBOtheradditions" %>

<%@ Register TagPrefix="uc1" TagName="CreateQuestion" Src="CreateQuestion.ascx" %>
<%--<%@ Register TagPrefix="uc1" TagName="QuestionBox" Src="QuestionBox.ascx" %>--%>
<%@ Register TagPrefix="uc1" TagName="QuestionFilter" Src="QuestionFilter.ascx" %>
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
        <div id="Content">
               <div id="divTeachersLoginInfo">
                    <uc1:TeachersPanel id="TeachersPanel1" runat="server"></uc1:TeachersPanel>
	            </div>
                <div id="divNotLoggedIn1" runat="server" style="background-color:#000;color:#fff;font-weight:bold;">
                    NO TEACHER OR ADMIN IS CURRENTLY LOGGED IN
                </div>
                    <p>&nbsp;</p><p>&nbsp;</p>
                <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <table id="tblQuestionbank" border="0" cellpadding="3" cellspacing="3">
                                        <colgroup>
                                            <col width="100px" valign="top" />
                                            <col width="800px" valign="top"/>
                                            <col width="600px" valign="top"/>
                                        </colgroup>
                                        <tr>
                                            <td>
                                                <div id="divTeachersMenuQuestionbank">
	                                                <uc1:TeachersMenu id="TeachersMenu1" runat="server"></uc1:TeachersMenu>
                                                </div>  
                                            </td>
                                            <td>
                                                <div style="background-color:mediumpurple;text-align:center;border:solid 2px white">
                                                    <h3>QUESTION BANK</h3>
                                                </div>
                                                <uc1:QuestionFilter id="QuestionFilter1" runat="server"></uc1:QuestionFilter>
                                                <div id="divQuestionsList">
                                                     <asp:PlaceHolder ID="QuestionsListHolder" runat="server">
                                                    </asp:PlaceHolder>
                                                </div>
                                            </td>
                                            <td>
                                                 <div style="background-color:mediumpurple;text-align:center;border:solid 2px white">
                                                    <h3>CREATE QUESTION</h3>
                                                </div>
                                                <div id="divCreateQuestionArea">
                                                     
                                                    <uc1:CreateQuestion id="CreateQuestion1" runat="server"></uc1:CreateQuestion>  
                                                    <asp:Label ID="lblStatus" runat="server" CSSClass="Statusarea" Font-Bold="true"></asp:Label>
                                                </div><br />
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
