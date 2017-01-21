<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="Questionbank.aspx.vb" Inherits="StudentsAssessment.Questionbank" %>

<%@ Register TagPrefix="uc1" TagName="CreateQuestion" Src="CreateQuestion.ascx" %>
<%--<%@ Register TagPrefix="uc1" TagName="QuestionBox" Src="QuestionBox.ascx" %>--%>
<%@ Register TagPrefix="uc1" TagName="QBFilters" Src="QBFilters.ascx" %>
<%@ Register TagPrefix="uc1" TagName="QBOtherFeatures" Src="QBOtherFeatures.ascx" %>
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
                                                <div style="background-color:mediumpurple;text-align:center;border:solid 2px white;width:600px;">
                                                    <h3>QUESTION BANK</h3>
                                                </div>
                                                <div class="divCreateQActions" style="width:160px;font-weight:bold;margin-left:425px;background-color:red;">
                                                    <asp:LinkButton ID="lnkCreateQuestion"  runat="server" Font-underline="False" ForeColor="White">CREATE QUESTION</asp:LinkButton>
			                                    </div>
                                                <uc1:QBFilters id="QBFilters1" runat="server"></uc1:QBFilters> 
                                                 
                                                <div id="divQuestionsList" runat="server">
                                                     <asp:PlaceHolder ID="QuestionsListHolder" runat="server">
                                                    </asp:PlaceHolder>
                                                </div>
                                                <div id="divOtherfeatures" runat="server">
                                                    <uc1:QBOtherFeatures id="QBOtherFeatures1" runat="server"></uc1:QBOtherFeatures>
                                                </div>
                                            </td>
                                            <td>
                                                 <div id="divCreateQ_Heading" runat="server" Visible="false" style="background-color:mediumpurple;text-align:center;border:solid 2px white;margin-top:-30px;">
                                                    <h3>CREATE QUESTION</h3>
                                                </div>
                                                <div id="divCreateQuestionArea" runat="server" Visible="false">
                                                    <uc1:CreateQuestion id="CreateQuestion1" runat="server"></uc1:CreateQuestion>
                                                </div><br />
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
