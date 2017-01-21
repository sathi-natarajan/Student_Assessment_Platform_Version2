Imports System.Data.SqlClient

Public Class QBOtheradditions
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        SiteHeader1.Pagetitle = "TEACHER INTERFACE"
        SiteHeader1.PageDesc = "A place where teachers can create questions for students and store them for future use"
        TeachersMenu1.HiglightOption = TeachersMenu.OptiontoHighlight.MyQB
        'Make sure they are logged in.  Otherwise, forece them to log in
        If Session("LoggedinTeacherID") IsNot Nothing AndAlso IsNumeric(Session("LoggedinTeacherID")) Then
            divNotLoggedIn1.Visible = False
            TeachersPanel1.Visible = True
            TeachersPanel1.Name = GetTeacherName(Convert.ToInt32(Session("LoggedinTeacherID")))
        Else

            Response.Redirect("~/TeachersLogin.aspx")
        End If
        Page.MaintainScrollPositionOnPostBack = True
        LoadQuestionSnapshots()
        LoadQuestions()
    End Sub

    Private Sub CreateQuestion1_CreateQuestion_CreateQuestionClick() Handles CreateQuestion1.CreateQuestion_CreateQuestionClick
        Dim iStandard As Integer = CreateQuestion1.GetStandardSelection()
        Dim iAnswertype As Integer = CreateQuestion1.GetAnswertypeSelection()
        If iStandard < 0 Or iAnswertype < 0 Then
            lblStatus.Text = "Both Standard and the type of answer must be selected"
        Else
            If CheckAnswers() Then
                SaveQuestionAnswers()
            Else
                If String.IsNullOrEmpty(CreateQuestion1.GetQuestionStem()) Then
                    lblStatus.Text = "The question stem was not provided"
                Else
                    Select Case iAnswertype
                        Case = 1
                            lblStatus.Text = "The short answer was not provied for this question"
                        Case = 2
                            lblStatus.Text = "'True' or 'False' was not checked for this question"
                        Case = 3
                            lblStatus.Text = "Either all answers are not provided or the radiobutton for one of the provided answers is not selected"
                        Case = 4
                            lblStatus.Text = "Either all answers are not provided or the checkbox for atleast one of the provided answers is not selected"
                    End Select
                End If
            End If
        End If

    End Sub

    Private Function CheckAnswers() As Boolean
        Dim bOK As Boolean = False
        bOK = CreateQuestion1.CheckQuestionAnswers()
        Return bOK
    End Function

    Private Function GetQID(ByVal strQuestiontext As String) As Integer
        Dim bExists As Boolean = False
        Dim iQID As Integer = 0
        Dim strConn As String = GetConnectStringFromWebConfig()
        Try
            Using objconn As New SqlConnection(strConn)
                Dim strSQL As String = <![CDATA[
   SELECT QuestionID FROM Questions WHERE QuestionText=@QuestionText
    ]]>.Value()
                Using objcommand As New SqlCommand
                    With objcommand
                        .Connection = objconn
                        .Connection.Open()
                        .CommandText = strSQL
                        .Parameters.AddWithValue("@QuestionText", strQuestiontext)
                    End With

                    iQID = objcommand.ExecuteScalar()
                End Using
            End Using
        Catch ex As Exception
            iQID = 0
        End Try
        Return iQID
    End Function
    Private Sub SaveQuestionAnswers()
        Dim iStandardSelected As Integer = CreateQuestion1.GetStandardSelection()
        Dim iAnswertypeSelection As Integer = CreateQuestion1.GetAnswertypeSelection()
        Dim strConn As String = GetConnectStringFromWebConfig()
        'Dim objConn As New SqlConnection(strConn)
        Dim bValid As Boolean = False
        Dim iQuestionID As Integer = 0
        Try
            Using objconn As New SqlConnection(strConn)
                Dim strSQL As String = <![CDATA[
       INSERT INTO Questions (QuestionText,QuestionType,StandardID,TeacherID,Description) VALUES(
            @QuestionText,@QuestionType,@StandardID,@TeacherID,@Description)
    ]]>.Value()
                Using objcommand As New SqlCommand
                    With objcommand
                        .Connection = objconn
                        .Connection.Open()
                        .CommandText = strSQL
                        .Parameters.AddWithValue("@QuestionText", CreateQuestion1.GetQuestionStem())
                        .Parameters.AddWithValue("@QuestionType", iAnswertypeSelection)
                        .Parameters.AddWithValue("@StandardID", iStandardSelected)
                        .Parameters.AddWithValue("@TeacherID", Integer.Parse(Session("LoggedinTeacherID")))
                        Dim strDesc As String =
                        If String.IsNullOrEmpty(strDesc) Then
                            .Parameters.AddWithValue("@Description", DBNull.Value)
                        Else
                            .Parameters.AddWithValue("@Description", strDesc)
                        End If
                    End With
                    If objcommand.ExecuteNonQuery() <= 0 Then
                        lblStatus.Text = "Problem saving the question"
                    Else
                        lblStatus.Text = "Successfully added question"
                        Using objCommand1 As SqlCommand = New SqlCommand
                            With objCommand1
                                .Connection = objconn
                                '.Connection.Open() 'already open at this point
                                .CommandText = "SELECT @@Identity"
                            End With
                            iQuestionID = objCommand1.ExecuteScalar()
                            If iQuestionID > 0 Then
                                strSQL = <![CDATA[
       INSERT INTO Answers (QuestionID,AnswerText,IsCorrect) VALUES(
            @QuestionID,@AnswerText,@IsCorrect)
    ]]>.Value()
                                Dim iNumtimes As Integer = 0
                                Dim dictAnswers As New Dictionary(Of String, Boolean)
                                Select Case iAnswertypeSelection
                                    Case = 1
                                        iNumtimes = 1
                                        dictAnswers = Nothing
                                    Case = 2
                                        iNumtimes = 2
                                        dictAnswers = CreateQuestion1.GetTrueorFalse()
                                    Case = 3
                                        iNumtimes = 4
                                        dictAnswers = CreateQuestion1.GetMultipleChoice()
                                    Case = 4
                                        iNumtimes = 4
                                        dictAnswers = CreateQuestion1.GetMultiChoice()
                                    Case Else
                                        iNumtimes = 0
                                End Select
                                If iNumtimes = 1 Then
                                    Using objcommand2 As New SqlCommand
                                        With objcommand2
                                            .Connection = objconn
                                            '.Connection.Open()
                                            .CommandText = strSQL
                                            .Parameters.AddWithValue("@QuestionID", iQuestionID)
                                            .Parameters.AddWithValue("@AnswerText", CreateQuestion1.GetShortAnswer())
                                            .Parameters.AddWithValue("@IsCorrect", True)
                                        End With

                                        If objcommand2.ExecuteNonQuery() <= 0 Then
                                            lblStatus.Text = "Problem saving the answer"
                                        Else
                                            lblStatus.Text = "Successfully added answer"
                                        End If
                                    End Using 'objcommand2
                                ElseIf iNumtimes > 1 Then
                                    For Each strKey As String In dictAnswers.Keys
                                        Using objcommand2 As New SqlCommand
                                            With objcommand2
                                                .Connection = objconn
                                                '.Connection.Open()
                                                .CommandText = strSQL
                                                .Parameters.AddWithValue("@QuestionID", iQuestionID)
                                                .Parameters.AddWithValue("@AnswerText", strKey)
                                                .Parameters.AddWithValue("@IsCorrect", dictAnswers(strKey))
                                            End With

                                            If objcommand2.ExecuteNonQuery() <= 0 Then
                                                lblStatus.Text = "Problem saving the answer"
                                            Else
                                                lblStatus.Text = "Successfully added answer"
                                            End If
                                        End Using 'objcommand2
                                    Next 'foreach
                                End If
                            End If
                        End Using 'objCommand1
                    End If
                End Using 'objCommand
            End Using 'objconnection
            'objConn.Open()
            'Dim strSQL As String = "INSERT INTO ClassesStudents (ClassID,StudentID,TeacherID) VALUES(
            '@ClassID,@StudentID,@TeacherID)"
            'Dim objCommand As New SqlCommand(strSQL, objConn)
            'objCommand.Parameters.AddWithValue("@ClassID", iClassID)
            'objCommand.Parameters.AddWithValue("@StudentID", iStudentID)
            'objCommand.Parameters.AddWithValue("@TeacherID", iTeacherID)
            'If objCommand.ExecuteNonQuery() <= 0 Then
            '    lblStatus.Text = "Problem adding student to class"
            'Else
            '    lblStatus.Text = "Successfully added student to class"
            'End If
            'objCommand.Dispose()
            'objCommand = Nothing
            'objConn.Dispose()
            'objConn.Close()
            'objConn = Nothing
            'SqlConnection.ClearAllPools()
        Catch ex As Exception
            lblStatus.Text = "Problem with system.  Please let administrator know.  And please retry at a later time"
        End Try
    End Sub
    Private Sub LoadQuestionSnapshots()
        'Dim strQuestionsBuilder As New StringBuilder
        'strQuestionsBuilder.Append("<p>Question 1</p>")
        'strQuestionsBuilder.Append("<p>Question 1</p>")
        'strQuestionsBuilder.Append("<p>Question 1</p>")
        'strQuestionsBuilder.Append("<p>Question 1</p>")
        'strQuestionsBuilder.Append("<p>Question 1</p>")
        'strQuestionsBuilder.Append("<p>Question 1</p>")
        'strQuestionsBuilder.Append("<p>Question 1</p>")
        'strQuestionsBuilder.Append("<p>Question 1</p>")
        'strQuestionsBuilder.Append("<p>Question 1</p>")
        'strQuestionsBuilder.Append("<p>Question 1</p>")
        'QuestionsListHolder.Controls.Add(New LiteralControl(strQuestionsBuilder.ToString()))
    End Sub

    Private Sub LoadQuestions()
        Dim strConn As String = GetConnectStringFromWebConfig()
        Try
            Using objconn As New SqlConnection(strConn)
                Dim strSQL As String = <![CDATA[
              SELECT [QuestionID],[QuestionText],[QuestionType],Standardname,[TeacherID],[Description]
              FROM [AssessmentDB].[dbo].[vuQuestions]
              inner join Standards on vuQuestions.StandardID=Standards.StandardID  
                WHERE TeacherID=@TeacherID
    ]]>.Value()
                Using objcommand As New SqlCommand
                    With objcommand
                        .Connection = objconn
                        .Connection.Open()
                        .CommandText = strSQL
                        .Parameters.AddWithValue("@TeacherID", Integer.Parse(Session("LoggedinTeacherID")))
                    End With
                    Using objReader As SqlDataReader = objcommand.ExecuteReader()
                        Dim ctrlQuestion As Control = Page.LoadControl("~/QuestionBank/QuestionBox.ascx")
                        Dim qbQuestionBox As QuestionBox
                        ' QuestionsListHolder.Controls.Add(New LiteralControl("<div id=""divQuestionsListINList"">"))
                        While objReader.Read()
                            'qbQuestion = New QuestionBox 'This usage won't work for user controls
                            'qbQuestion.QuestionNo = Integer.Parse(objReader("QuestionID"))

                            QuestionsListHolder.Controls.Add(New LiteralControl("<div style=""margin-left:-150px;"">"))
                            ctrlQuestion = Page.LoadControl("~/QuestionBank/QuestionBox.ascx")
                            qbQuestionBox = CType(ctrlQuestion, QuestionBox)
                            qbQuestionBox.ID = objReader("QuestionID")
                            qbQuestionBox.QuestionNo = objReader("QuestionID")
                            qbQuestionBox.QuestionSnapshot = objReader("QuestionText").ToString.ToUpper()
                            ''Display only first 20 characters
                            'If objReader("QuestionText").ToString.Length < 20 Then
                            '    qbQuestionBox.QuestionSnapshot = objReader("QuestionText").ToString.ToUpper()
                            'Else
                            '    qbQuestionBox.QuestionSnapshot = objReader("QuestionText").ToString.ToUpper().Substring(0, 19)
                            'End If
                            qbQuestionBox.Standard = objReader("Standardname").ToString
                            AddHandler qbQuestionBox.QuestionBox_TrashQuestionClicked, AddressOf TrashQuestion
                            AddHandler qbQuestionBox.QuestionBox_EditQuestionClicked, AddressOf EditQuestion

                            QuestionsListHolder.Controls.Add(qbQuestionBox)
                            QuestionsListHolder.Controls.Add(New LiteralControl("</div>"))
                            QuestionsListHolder.Controls.Add(New LiteralControl("<br/>"))
                        End While
                        'QuestionsListHolder.Controls.Add(New LiteralControl("</div>"))
                    End Using 'objReader
                End Using 'objcommand
            End Using 'objConnection
        Catch ex As Exception
            lblStatus.Text = "Problem with system.  Please let administrator know.  And please retry at a later time"
        End Try
    End Sub

    Private Sub TrashQuestion(ByVal strQuestionID As String)
        'lblStatus.Text = "The question" + strQuestionID + " will be deleted"
        Dim strQuestionID1 As String = strQuestionID.Replace("QUES", "") 'remove "QUES" prefix
        Dim iQuestionID As Integer = Integer.Parse(strQuestionID1)

        Dim strConn As String = GetConnectStringFromWebConfig()
        Try
            Using objconn As New SqlConnection(strConn)
                Dim strSQL As String = <![CDATA[
    DELETE FROM Questions WHERE QuestionID=@QuestionID AND TeacherID=@TeacherID
    ]]>.Value()
                Using objcommand As New SqlCommand
                    With objcommand
                        .Connection = objconn
                        .Connection.Open()
                        .CommandText = strSQL
                        objcommand.Parameters.AddWithValue("@TeacherID", Integer.Parse(Session("LoggedinTeacherID")))
                        objcommand.Parameters.AddWithValue("@QuestionID", iQuestionID)
                    End With
                    If objcommand.ExecuteNonQuery() <= 0 Then
                        lblStatus.Text = "Problem while deleting this question"
                    Else
                        Using objcommand1 As New SqlCommand
                            strSQL = <![CDATA[
    DELETE FROM Answers WHERE QuestionID=@QuestionID
    ]]>.Value()
                            With objcommand1
                                .Connection = objconn
                                '.Connection.Open()
                                .CommandText = strSQL
                                .Parameters.AddWithValue("@QuestionID", iQuestionID)
                            End With

                            If objcommand1.ExecuteNonQuery() <= 0 Then
                                lblStatus.Text = "Problem while deleting answers to this question"
                            Else
                                lblStatus.Text = "Successfully removed the question's answers"
                                QuestionsListHolder.Controls.Clear()
                                LoadQuestions()
                            End If
                        End Using 'objcommand1
                    End If
                End Using 'objcommand
            End Using 'objconn
        Catch ex As Exception

        End Try
    End Sub

    Private Sub EditQuestion(ByVal strQuestionID As String)
        'lblStatus.Text = "The question" + strQuestionID + "can now be edited.  Once done, be sure to click 'CREATE QUESTION' button"
        Dim QbQuestion As New QuestionBox
        For Each ctrlControl As Control In QuestionsListHolder.Controls
            'Try using TryParse later
            If TypeOf (ctrlControl) Is QuestionBox Then
                QbQuestion = CType(ctrlControl, QuestionBox)
                If QbQuestion.ID = strQuestionID Then
                    QbQuestion.Mode = QuestionBox.Modes.Edit
                End If
            Else
            End If
        Next
    End Sub
End Class