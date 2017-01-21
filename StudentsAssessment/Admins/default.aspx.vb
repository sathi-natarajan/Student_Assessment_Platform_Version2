Imports System.Data.SqlClient

Public Class Admindefault
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'Show the teacher's section only when a teacher logs in.
        If Session("IsAdmin") IsNot Nothing AndAlso Session("IsAdmin") = True Then
            divNotLoggedIn1.Visible = False
            TeachersPanel1.Visible = True
            TeachersPanel1.Name = GetTeacherName(Convert.ToInt32(Session("LoggedinTeacherID")))
            LoadDashboard()
            LoadAnn1()
            LoadAnn2()
            SiteHeader1.Pagetitle = "ADMIN HOME - MAIN SCREEN"
            SiteHeader1.PageDesc = "An administrator's interface to Student Assessment Web application"
            TeachersMenu1.HiglightOption = TeachersMenu.OptiontoHighlight.Home
        Else
            TeachersPanel1.Visible = False
            divNotLoggedIn1.Visible = True
            Session.Clear()
            Session.Abandon()
            Response.Redirect("~/TeachersLogin.aspx")
        End If
    End Sub

    Private Sub LoadDashboard()
        Dim strDashboardCBuilder As New StringBuilder
        strDashboardCBuilder.Append("<div>")
        strDashboardCBuilder.Append("<ul>")
        strDashboardCBuilder.Append("<li>Key Metric1</li>")
        strDashboardCBuilder.Append("<li>Key Metric2</li>")
        strDashboardCBuilder.Append("<li>Key Metric3</li>")
        strDashboardCBuilder.Append("<li>Key Metric4</li>")
        strDashboardCBuilder.Append("<li>Key Metric5</li>")
        strDashboardCBuilder.Append("</ul>")
        strDashboardCBuilder.Append("</div>")
        divDashboardC.InnerHtml = strDashboardCBuilder.ToString()
    End Sub

    Private Sub LoadAnn1()
        Dim strAnn1Builder As New StringBuilder
        strAnn1Builder.Append("<div>")
        strAnn1Builder.Append("<ul>")
        strAnn1Builder.Append("<li>Newsfeed items</li>")
        strAnn1Builder.Append("<li>Teacher Alerts</li>")
        strAnn1Builder.Append("<li>No. students yet to take recent test</li>")
        strAnn1Builder.Append("<li>Test Completion rate</li>")
        strAnn1Builder.Append("<li>Etc</li>")
        strAnn1Builder.Append("</ul>")
        strAnn1Builder.Append("</div>")
        divAnnouncement1C.InnerHtml = strAnn1Builder.ToString()
    End Sub

    Private Sub LoadAnn2()
        'Dim strAnn2Builder As New StringBuilder
        'strAnn2Builder.Append("<div>")
        'strAnn2Builder.Append("<ul>")
        'strAnn2Builder.Append("<li>New Announcements from admin</li>")
        'Dim strAdminMessage As String = GetMessageFromAdmin()
        'strAnn2Builder.Append(strAdminMessage)
        'strAnn2Builder.Append("<li>New Test anouncements</li>")
        'strAnn2Builder.Append("<li>Info. on contents added to site</li>")
        'strAnn2Builder.Append("</ul>")
        'strAnn2Builder.Append("</div>")
        'divAnnouncement2C.InnerHtml = strAnn2Builder.ToString()
        divAnnouncement2C.Visible = False
        ShowAdminMessages()
    End Sub

    Private Sub ShowAdminMessages()
        Dim strConn As String = GetConnectStringFromWebConfig()
        Dim strMessagesBuilder As New StringBuilder
        Try
            Using objconn As New SqlConnection(strConn)
                Dim strSQL As String = <![CDATA[
                       SELECT MessageID, MessageText,MessageDate FROM Messages WHERE DoNotShowAgain=0
                       AND (ToID=-1 OR ToID=@ToID)
    ]]>.Value()
                Using objcommand As New SqlCommand
                    With objcommand
                        .Connection = objconn
                        .Connection.Open()
                        .CommandText = strSQL
                        .Parameters.AddWithValue("@ToID", Integer.Parse(Session("LoggedinTeacherID")))
                    End With
                    Dim bMessagesThere As Boolean = False

                    'Dim ctrlMessageFromAdmin As Control = Page.LoadControl("~/Admin/MessageFromAdmin.ascx")
                    'Dim objMessageFromAdmin As MessageFromAdmin
                    'objMessageFromAdmin = CType(ctrlMessageFromAdmin, MessageFromAdmin)
                    'QuestionsListHolder.Controls.Add(qbOtherFeatures1)
                    ' QBSidebuttons1.ButtonsforScreen = QBSidebuttons.ForScreens.AddExplanation
                    'AddHandler QBSidebuttons1.QbSideButtons_OKClick, AddressOf AddExplanation
                    Announcement2Holder.Controls.Add(New LiteralControl("<br/>"))
                    Using objReader As SqlDataReader = objcommand.ExecuteReader()
                        While objReader.Read()
                            Dim ctrlMessageFromAdmin As Control = Page.LoadControl("~/Admins/MessageFromAdmin.ascx")
                            Dim objMessageFromAdmin As MessageFromAdmin
                            objMessageFromAdmin = CType(ctrlMessageFromAdmin, MessageFromAdmin)
                            bMessagesThere = True
                            objMessageFromAdmin.MessageID = Integer.Parse(objReader("MessageID"))
                            objMessageFromAdmin.MessageTypeText = "MESSAGE SENT"
                            objMessageFromAdmin.MessageText = objReader("MessageText").ToString()
                            objMessageFromAdmin.DateofActivity = DateTime.Parse(objReader("MessageDate")).ToShortDateString
                            AddHandler objMessageFromAdmin.MessageFromAdmin_DonotshowAgain_CheckChanged, AddressOf DonotshowAgain_CheckChanged
                            Announcement2Holder.Controls.Add(objMessageFromAdmin)
                            Announcement2Holder.Controls.Add(New LiteralControl("<br/>"))
                        End While
                        'strMessagesBuilder.Append("</ul>")
                    End Using
                    If Not bMessagesThere Then strMessagesBuilder.Clear()
                End Using
            End Using
        Catch ex As Exception
            lblStatus.Text = "Problem area - default.ShowAdminMessages<br/>" + ex.Message
        End Try
    End Sub

    Private Sub DonotshowAgain_CheckChanged(ByVal iMessageID As Integer, ByVal bChecked As Boolean)
        'lblStatus.Text = "Check status of message #" + iMessageID.ToString + " changed.  It is now " + If(bChecked, "checked", "unchecked")
        Dim bUpdated As Boolean = False
        Dim strConn As String = GetConnectStringFromWebConfig()
        Try
            Using objconn As New SqlConnection(strConn)
                Dim strSQL As String = <![CDATA[
    UPDATE Messages SET DonotShowAgain=@DonotShowAgain WHERE MessageID=@MessageID
    ]]>.Value()
                Using objcommand As New SqlCommand
                    With objcommand
                        .Connection = objconn
                        .Connection.Open()
                        .CommandText = strSQL
                        .Parameters.AddWithValue("@DonotShowAgain", bChecked)
                        .Parameters.AddWithValue("@MessageID", iMessageID)
                    End With
                    If objcommand.ExecuteNonQuery() <= 0 Then
                        lblStatus.Text = "Problem area - DonotshowAgain_CheckChanged"
                        bUpdated = False
                    Else
                        bUpdated = True
                    End If
                End Using
            End Using
        Catch ex As Exception
            bUpdated = False
            lblStatus.Text = "Problem area - DonotshowAgain_CheckChanged<br/>" + ex.Message
        End Try
        Announcement2Holder.Controls.Clear()
        ShowAdminMessages()
    End Sub
End Class