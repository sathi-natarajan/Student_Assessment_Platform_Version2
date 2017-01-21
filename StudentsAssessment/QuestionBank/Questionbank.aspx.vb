Imports System.Data.SqlClient
Imports StudentsAssessment

Public Class Questionbank
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
        'LoadQuestionSnapshots()
        'If Not IsPostBack Then
        QuestionsListHolder.Controls.Clear()
        LoadQuestions()
        ' End If
        divQuestionsList.Visible = True
        divOtherfeatures.Visible = False
    End Sub

    Private Sub CreateQuestion1_CreateQuestion_CreateQuestionClick() Handles CreateQuestion1.CreateQuestion_CreateQuestionClick
        lblStatus.Text = ""
        Dim iStandard As Integer = CreateQuestion1.GetStandardSelection()
        Dim iAnswertype As Integer = CreateQuestion1.GetAnswertypeSelection()
        If iStandard < 0 Or iAnswertype < 0 Then
            lblStatus.Text = "Both Standard and the type of answer must be selected"
        Else
            If CheckAnswers() Then
                SaveQuestionAnswers()
                QBOtherFeatures1.ClearExplanation()
                QBFilters1.Visible = True
                QuestionsListHolder.Controls.Clear()
                LoadQuestions()
            Else

                lblStatus.Text = CreateQuestion1.ErrorMessage

                'If String.IsNullOrEmpty(CreateQuestion1.GetQuestionStem()) Then
                '    lblStatus.Text = "The question stem was not provided"
                'Else
                '    Select Case iAnswertype
                '        Case = 1
                '            lblStatus.Text = "The short answer was not provied for this question"
                '        Case = 2
                '            lblStatus.Text = "'True' or 'False' was not checked for this question"
                '        Case = 3
                '            lblStatus.Text = "Either all answers are not provided or the radiobutton for one of the provided answers is not selected"
                '        Case = 4
                '            lblStatus.Text = "Either all answers are not provided or the checkbox for atleast one of the provided answers is not selected"
                '    End Select
                'End If
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
       INSERT INTO Questions (QuestionText,QuestionType,StandardID,TeacherID,Description,ClassID, SubjectID,Created,Updated) VALUES(
            @QuestionText,@QuestionType,@StandardID,@TeacherID,@Description,@ClassID, @SubjectID,@Created,@Updated)
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
                        .Parameters.AddWithValue("@ClassID", Integer.Parse(Session("Activeclass")))
                        .Parameters.AddWithValue("@SubjectId", Integer.Parse(Session("Activesubject")))
                        .Parameters.AddWithValue("@Created", DateTime.Today.ToShortDateString)
                        .Parameters.AddWithValue("@Updated", DateTime.Today.ToShortDateString)
                        Dim strDesc As String = ""
                        If Session("explanation") IsNot Nothing Then
                            strDesc = Session("explanation")
                        End If
                        If String.IsNullOrEmpty(strDesc) Then
                            .Parameters.AddWithValue("@Description", DBNull.Value)
                        Else
                            .Parameters.AddWithValue("@Description", strDesc)
                        End If
                    End With
                    If objcommand.ExecuteNonQuery() <= 0 Then
                        lblStatus.Text = "Problem area - SaveQuestionAnswers<br/>Problem saving the question"
                    Else
                        'lblStatus.Text = "Successfully added question"
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
                                            lblStatus.Text = "Problem area - SaveQuestionAnswers<br/>Problem saving the answer"
                                        Else
                                            lblStatus.Text = "Created question and answer(s) has/have been added successfully to question bank"
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
                                                lblStatus.Text = "Problem area - SaveQuestionAnswers<br/>Problem saving the answer"
                                            Else
                                                lblStatus.Text = "Created question and answer(s) has/have been added successfully to question bank"
                                            End If
                                        End Using 'objcommand2
                                    Next 'foreach
                                End If
                            End If
                        End Using 'objCommand1
                    End If
                End Using 'objCommand
            End Using 'objconnection
            ClearQTypeSessionVars()

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
            lblStatus.Text = "Problem area - SaveQuestionAnswers<br/>" + ex.Message
        End Try
    End Sub

    Private Sub ClearQTypeSessionVars()
        Session("explanation") = Nothing
        Session("grade_selected") = Nothing
        Session("standard_selected") = Nothing
        Session("subject_selected") = Nothing
        Session("qid_selected") = Nothing
        Session("qtype_selected") = Nothing
        Session("search_selected") = Nothing
    End Sub
    'Private Sub LoadQuestionSnapshots()
    '    'Dim strQuestionsBuilder As New StringBuilder
    '    'strQuestionsBuilder.Append("<p>Question 1</p>")
    '    'strQuestionsBuilder.Append("<p>Question 1</p>")
    '    'strQuestionsBuilder.Append("<p>Question 1</p>")
    '    'strQuestionsBuilder.Append("<p>Question 1</p>")
    '    'strQuestionsBuilder.Append("<p>Question 1</p>")
    '    'strQuestionsBuilder.Append("<p>Question 1</p>")
    '    'strQuestionsBuilder.Append("<p>Question 1</p>")
    '    'strQuestionsBuilder.Append("<p>Question 1</p>")
    '    'strQuestionsBuilder.Append("<p>Question 1</p>")
    '    'strQuestionsBuilder.Append("<p>Question 1</p>")
    '    'QuestionsListHolder.Controls.Add(New LiteralControl(strQuestionsBuilder.ToString()))
    'End Sub

    Private Sub LoadQuestions(Optional ByVal bFilter As Boolean = False)
        Dim bQuestionsLoaded As Boolean = False
        Dim strConn As String = GetConnectStringFromWebConfig()
        Try
            Using objconn As New SqlConnection(strConn)
                Dim strSQL As String = ""
                If bFilter Then
                    Dim strWhere As String = ""
                    If Session("explanation") IsNot Nothing AndAlso Session("explanation").ToString.Length > 0 Then
                        strSQL = <![CDATA[
              SELECT [QuestionID],[QuestionText],[QuestionType],Standardname,[TeacherID],[Description]
              FROM [AssessmentDB].[dbo].[vuQuestions]
              inner join Standards on vuQuestions.StandardID=Standards.StandardID
              WHERE TeacherID=@TeacherID AND Description=@Description 
    ]]>.Value()
                    ElseIf Session("grade_selected") IsNot Nothing AndAlso IsNumeric(Session("grade_selected")) = True AndAlso
                        Integer.Parse(Session("grade_selected")) > 0 Then
                        strSQL = <![CDATA[
                      SELECT [QuestionID],[QuestionText],[QuestionType],Standardname,[TeacherID],[Description]
                      FROM [AssessmentDB].[dbo].[vuQuestions]
                      inner join Standards on vuQuestions.StandardID=Standards.StandardID
                      WHERE TeacherID=@TeacherID AND ClassID=@ClassID
                        ]]>.Value()
                    ElseIf Session("standard_selected") IsNot Nothing AndAlso IsNumeric(Session("standard_selected")) = True AndAlso
                  Integer.Parse(Session("standard_selected")) > 0 Then
                        strSQL = <![CDATA[
                      SELECT [QuestionID],[QuestionText],[QuestionType],Standardname,[TeacherID],[Description]
                      FROM [AssessmentDB].[dbo].[vuQuestions]
                      inner join Standards on vuQuestions.StandardID=Standards.StandardID
                        WHERE TeacherID=@TeacherID AND vuQuestions.StandardID=@StandardID
                        ]]>.Value()
                    ElseIf Session("subject_selected") IsNot Nothing AndAlso IsNumeric(Session("subject_selected")) = True AndAlso
           Integer.Parse(Session("subject_selected")) > 0 Then
                        strSQL = <![CDATA[
                      SELECT [QuestionID],[QuestionText],[QuestionType],Standardname,[TeacherID],[Description]
                      FROM [AssessmentDB].[dbo].[vuQuestions]
                      inner join Standards on vuQuestions.StandardID=Standards.StandardID
                        WHERE TeacherID=@TeacherID AND SubjectID=@SubjectID
                        ]]>.Value()
                    ElseIf Session("qid_selected") IsNot Nothing AndAlso Session("qid_selected").ToString.Length > 0 Then
                        strSQL = <![CDATA[
                      SELECT [QuestionID],[QuestionText],[QuestionType],Standardname,[TeacherID],[Description]
                      FROM [AssessmentDB].[dbo].[vuQuestions]
                      inner join Standards on vuQuestions.StandardID=Standards.StandardID
                        WHERE TeacherID=@TeacherID AND QuestionID=@QuestionID
                        ]]>.Value()
                    ElseIf Session("qtype_selected") IsNot Nothing AndAlso IsNumeric(Session("qtype_selected")) = True AndAlso
           Integer.Parse(Session("qtype_selected")) > 0 Then
                        strSQL = <![CDATA[
                      SELECT [QuestionID],[QuestionText],[QuestionType],Standardname,[TeacherID],[Description]
                      FROM [AssessmentDB].[dbo].[vuQuestions]
                      inner join Standards on vuQuestions.StandardID=Standards.StandardID
                        WHERE TeacherID=@TeacherID AND QuestionType=@QuestionType
                        ]]>.Value()
                    Else
                        'Unfiltered
                        strSQL = <![CDATA[
              SELECT [QuestionID],[QuestionText],[QuestionType],Standardname,[TeacherID],[Description]
              FROM [AssessmentDB].[dbo].[vuQuestions]
              inner join Standards on vuQuestions.StandardID=Standards.StandardID
                WHERE TeacherID=@TeacherID
                ]]>.Value()
                    End If
                Else
                    'Unfiltered
                    strSQL = <![CDATA[
                SELECT [QuestionID],[QuestionText],[QuestionType],Standardname,[TeacherID],[Description]
              FROM [AssessmentDB].[dbo].[vuQuestions]
              inner join Standards on vuQuestions.StandardID=Standards.StandardID
                WHERE TeacherID=@TeacherID
                ]]>.Value()
                End If

                Using objcommand As New SqlCommand
                    With objcommand
                        .Connection = objconn
                        .Connection.Open()
                        .CommandText = strSQL
                        If bFilter Then
                            If Session("explanation") IsNot Nothing AndAlso Session("explanation").ToString.Length > 0 Then
                                .Parameters.AddWithValue("@Description", Session("explanation").ToString)
                            ElseIf Session("grade_selected") IsNot Nothing AndAlso IsNumeric(Session("grade_selected")) = True AndAlso
                        Integer.Parse(Session("grade_selected")) > 0 Then
                                .Parameters.AddWithValue("@ClassID", Integer.Parse(Session("grade_selected")))
                            ElseIf Session("standard_selected") IsNot Nothing AndAlso IsNumeric(Session("standard_selected")) = True AndAlso
                                                    Integer.Parse(Session("standard_selected")) > 0 Then
                                .Parameters.AddWithValue("@StandardID", Integer.Parse(Session("standard_selected")))
                            ElseIf Session("subject_selected") IsNot Nothing AndAlso IsNumeric(Session("subject_selected")) = True AndAlso
                                               Integer.Parse(Session("subject_selected")) > 0 Then
                                .Parameters.AddWithValue("@SubjectID", Integer.Parse(Session("subject_selected")))
                            ElseIf Session("qid_selected") IsNot Nothing AndAlso
                                          Session("qid_selected").ToString.Length > 0 Then
                                .Parameters.AddWithValue("@QuestionID", Session("qid_selected"))
                            ElseIf Session("qtype_selected") IsNot Nothing AndAlso
                                     Session("qtype_selected").ToString.Length > 0 Then
                                .Parameters.AddWithValue("@QuestionType", Integer.Parse(Session("qtype_selected")))
                            End If
                        End If
                        .Parameters.AddWithValue("@TeacherID", Integer.Parse(Session("LoggedinTeacherID")))
                    End With
                    Using objReader As SqlDataReader = objcommand.ExecuteReader()
                        Dim ctrlQuestion As Control = Page.LoadControl("~/QuestionBank/QuestionBox.ascx")
                        Dim qbQuestionBox As QuestionBox
                        ' QuestionsListHolder.Controls.Add(New LiteralControl("<div id=""divQuestionsListINList"">"))
                        While objReader.Read()
                            bQuestionsLoaded = True
                            'qbQuestion = New QuestionBox 'This usage won't work for user controls
                            'qbQuestion.QuestionNo = Integer.Parse(objReader("QuestionID"))

                            QuestionsListHolder.Controls.Add(New LiteralControl("<div style=""margin-left:-150px;"">"))
                            ctrlQuestion = Page.LoadControl("~/QuestionBank/QuestionBox.ascx")
                            qbQuestionBox = CType(ctrlQuestion, QuestionBox)
                            qbQuestionBox.ID = objReader("QuestionID")
                            qbQuestionBox.QuestionNo = objReader("QuestionID")
                            qbQuestionBox.QuestionSnapshot = objReader("QuestionText").ToString.ToUpper()
                            qbQuestionBox.EditedQuestion = objReader("QuestionText").ToString
                            ''Display only first 20 characters
                            'If objReader("QuestionText").ToString.Length < 20 Then
                            '    qbQuestionBox.QuestionSnapshot = objReader("QuestionText").ToString.ToUpper()
                            'Else
                            '    qbQuestionBox.QuestionSnapshot = objReader("QuestionText").ToString.ToUpper().Substring(0, 19)
                            'End If
                            qbQuestionBox.Standard = objReader("Standardname").ToString
                            AddHandler qbQuestionBox.QuestionBox_TrashQuestionClicked, AddressOf TrashQuestion
                            AddHandler qbQuestionBox.QuestionBox_EditQuestionClicked, AddressOf EditQuestion
                            AddHandler qbQuestionBox.QuestionBox_DuplicateQNoClicked, AddressOf DuplicateQuestion

                            QuestionsListHolder.Controls.Add(qbQuestionBox)
                            QuestionsListHolder.Controls.Add(New LiteralControl("</div>"))
                            QuestionsListHolder.Controls.Add(New LiteralControl("<br/>"))
                        End While
                        'QuestionsListHolder.Controls.Add(New LiteralControl("</div>"))
                    End Using 'objReader
                End Using 'objcommand
            End Using 'objConnection
        Catch ex As Exception
            lblStatus.Text = "Problem area - LoadQuestions<br/>" + ex.Message
            bQuestionsLoaded = False
        End Try
        If Not bQuestionsLoaded Then
            QuestionsListHolder.Controls.Clear()
            QuestionsListHolder.Controls.Add(New LiteralControl("<strong>No questions are available yet</strong>"))
        End If

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
                        .Parameters.AddWithValue("@TeacherID", Integer.Parse(Session("LoggedinTeacherID")))
                        .Parameters.AddWithValue("@QuestionID", iQuestionID)
                    End With
                    If objcommand.ExecuteNonQuery() <= 0 Then
                        lblStatus.Text = "Problem area - TrashQuestion<br/>Problem deleting the question"
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
                                lblStatus.Text = "Problem area - TrashQuestion<br/>Problem deleting the answers"
                            Else
                                lblStatus.Text = "selecting question WITH answer(s) has/have been removed successfully from question bank"
                                QuestionsListHolder.Controls.Clear()
                                LoadQuestions()
                            End If
                        End Using 'objcommand1
                    End If
                End Using 'objcommand
            End Using 'objconn
        Catch ex As Exception
            lblStatus.Text = "Problem area - TrashQuestion<br/>" + ex.Message
        End Try
    End Sub

    Private Sub DuplicateQuestion(ByVal strQuestionID As String)
        Dim bDuplicated As Boolean = False
        Dim bDataRead As Boolean = False
        'lblStatus.Text = "The question" + strQuestionID + " will be deleted"
        Dim strQuestionID1 As String = strQuestionID.Replace("QUES", "") 'remove "QUES" prefix
        Dim iQuestionID As Integer = Integer.Parse(strQuestionID1)

        Dim strConn As String = GetConnectStringFromWebConfig()
        Try
            Using objconn As New SqlConnection(strConn)
                Dim strSQL As String = <![CDATA[
    SELECT * FROM Questions WHERE QuestionID=@QuestionID AND TeacherID=@TeacherID
    ]]>.Value()
                Using objcommand As New SqlCommand
                    With objcommand
                        .Connection = objconn
                        .Connection.Open()
                        .CommandText = strSQL
                        .Parameters.AddWithValue("@TeacherID", Integer.Parse(Session("LoggedinTeacherID")))
                        .Parameters.AddWithValue("@QuestionID", iQuestionID)
                    End With
                    Dim strQText As String = ""
                    Dim iQuestionTypeID As Integer = 0
                    Dim iStandardID As Integer = 0
                    Dim iTeacherID As Integer = 0
                    Dim strDescription As String = ""
                    Dim iClassID As Integer = 0
                    Dim iSubjectID As Integer = 0
                    Dim dtCreated As DateTime, dtUpdated As DateTime
                    Using objReader As SqlDataReader = objcommand.ExecuteReader()
                        If objReader.Read() Then
                            strQText = objReader("Questiontext").ToString
                            iQuestionTypeID = Integer.Parse(objReader("QuestionType"))
                            iStandardID = Integer.Parse(objReader("StandardID"))
                            iTeacherID = Integer.Parse(objReader("TeacherID"))
                            strDescription = objReader("Description").ToString
                            iClassID = Integer.Parse(objReader("ClassID"))
                            iSubjectID = Integer.Parse(objReader("SubjectID"))
                            dtCreated = DateTime.Parse(objReader("Created"))
                            dtUpdated = DateTime.Parse(objReader("Updated"))
                            bDataRead = True
                        Else
                            bDataRead = False
                        End If
                    End Using 'objReader
                    If bDataRead Then

                        Using objcommandIns As New SqlCommand
                            strSQL = <![CDATA[
    INSERT INTO Questions(QuestionText,QuestionType,StandardId,TeacherID,Description,
    ClassID,SubjectID,Created,Updated) VALUES(
                                   @QuestionText,@QuestionType,@StandardId,@TeacherID,@Description,
    @ClassID,@SubjectID,@Created,@Updated)
    ]]>.Value()
                            With objcommandIns
                                .Connection = objconn
                                '.Connection.Open()
                                .CommandText = strSQL
                                .Parameters.AddWithValue("@QuestionText", strQText)
                                .Parameters.AddWithValue("@QuestionType", iQuestionTypeID)
                                .Parameters.AddWithValue("@StandardId", iStandardID)
                                .Parameters.AddWithValue("@TeacherID", iTeacherID)
                                .Parameters.AddWithValue("@Description", strDescription)
                                .Parameters.AddWithValue("@ClassID", iClassID)
                                .Parameters.AddWithValue("@SubjectId", iSubjectID)
                                .Parameters.AddWithValue("@Created", dtCreated.ToShortDateString)
                                .Parameters.AddWithValue("@Updated", dtUpdated.ToShortDateString)
                                If objcommandIns.ExecuteNonQuery() <= 0 Then
                                    lblStatus.Text = "Problem area - DuplicateQuestion<br/>Problem duplicating the question"
                                    bDuplicated = False
                                Else
                                    bDuplicated = True
                                End If
                            End With
                        End Using 'objcommandIns
                    Else
                        lblStatus.Text = "Problem area - DuplicateQuestion<br/>Problem duplicating the question"
                        bDuplicated = False
                    End If
                End Using 'objcommand
            End Using 'objconn
        Catch ex As Exception
            lblStatus.Text = "Problem area - DuplicateQuestion<br/>" + ex.Message
            bDuplicated = False
        End Try

        If bDuplicated Then
            QuestionsListHolder.Controls.Clear()
            LoadQuestions()
            lblStatus.Text = "The selected question has been duplicated successfully"
        End If
        lblStatus.Visible = True
        'Return bDuplicated
    End Sub


    Private Sub EditQuestion(ByVal strQuestionID As String)
        Dim bEdited As Boolean = False
        'lblStatus.Text = "The question" + strQuestionID + "can now be edited.  Once done, be sure to click 'CREATE QUESTION' button"
        Dim QbQuestion As New QuestionBox
        For Each ctrlControl As Control In QuestionsListHolder.Controls
            'Try using TryParse later
            If TypeOf (ctrlControl) Is QuestionBox Then
                QbQuestion = CType(ctrlControl, QuestionBox)
                If QbQuestion.ID = strQuestionID Then
                    If QbQuestion.Mode = QuestionBox.Modes.Edit Then
                        QbQuestion.QuestionSnapshot = QbQuestion.EditedQuestion 'Put in what you just edited
                        If UpdateQuestion(QbQuestion.QuestionNo, QbQuestion.EditedQuestion) Then
                            QbQuestion.Mode = QuestionBox.Modes.Display
                            bEdited = True
                            Exit For
                        Else
                            QbQuestion.Mode = QuestionBox.Modes.Display
                            bEdited = False
                        End If
                    Else
                        QbQuestion.Mode = QuestionBox.Modes.Edit
                        bEdited = False
                    End If
                End If
            Else
            End If
        Next
        If bEdited Then
            QuestionsListHolder.Controls.Clear()
            LoadQuestions()
            bEdited = False
            lblStatus.Text = "The edited question has been updated successfully in the question bank"
        End If

    End Sub

    Private Function UpdateQuestion(ByVal strQNo As String, ByVal strQText As String) As Boolean
        Dim bUpdated As Boolean = False
        Dim strConn As String = GetConnectStringFromWebConfig()
        Dim strQuestionID1 As String = strQNo.Replace("QUES", "") 'remove "QUES" prefix
        Dim iQuestionID As Integer = Integer.Parse(strQuestionID1)
        Try
            Using objconn As New SqlConnection(strConn)
                Dim strSQL As String = <![CDATA[
    UPDATE Questions SET QuestionText=@Questiontext WHERE QuestionID=@QuestionID AND TeacherID=@TeacherID
    ]]>.Value()
                Using objcommand As New SqlCommand
                    With objcommand
                        .Connection = objconn
                        .Connection.Open()
                        .CommandText = strSQL
                        .Parameters.AddWithValue("@QuestionID", iQuestionID)
                        .Parameters.AddWithValue("@TeacherID", Integer.Parse(Session("LoggedinTeacherID")))
                        .Parameters.AddWithValue("@Questiontext", strQText)
                    End With
                    If objcommand.ExecuteNonQuery() <= 0 Then
                        lblStatus.Text = "Problem area - Updatequestion<br/>Problem Updating the question"
                        bUpdated = False
                    Else
                        bUpdated = True
                    End If
                End Using
            End Using
        Catch ex As Exception
            bUpdated = False
            lblStatus.Text = "Problem area - Updatequestion<br/>" + ex.Message
        End Try
        Return bUpdated
    End Function

    'Private Sub AddExplanation(ByVal enumForScreen As QBSidebuttons.ForScreens)
    '    Dim QBOtherfeatures1 As New QBOtherfeatures
    '    For Each ctrlControl As Control In QuestionsListHolder.Controls
    '        If TypeOf (ctrlControl) Is QBOtherfeatures Then
    '            QBOtherfeatures1 = CType(ctrlControl, QBOtherfeatures)
    '            hdnExplanation.Value = QBOtherfeatures1.Explanation
    '        End If
    '    Next
    '    QuestionsListHolder.Controls.Clear()
    '    LoadQuestions()
    'End Sub

    'Private Sub QBSidebuttons1_QbSideButtons_OKClick(enumForscreen As QBSidebuttons.ForScreens) Handles QBSidebuttons1.QbSideButtons_OKClick
    '    ' hdnExplanation.Value = QBSidebuttons1
    '    Dim QBOtherfeatures1 As New QBOtherfeatures
    '    For Each ctrlControl As Control In QuestionsListHolder.Controls
    '        If TypeOf (ctrlControl) Is QBOtherfeatures Then
    '            QBOtherfeatures1 = CType(ctrlControl, QBOtherfeatures)
    '            Session("explanation") = QBOtherfeatures1.Explanation
    '        End If
    '    Next
    '    'QuestionsListHolder.Controls.Clear()
    '    divQuestionsList.Visible = True
    '    divOtherfeatures.Visible = False
    '    LoadQuestions()

    '    Select Case enumForscreen
    '        Case QBSidebuttons.ForScreens.AddExplanation
    '            lblStatus.Text = "Buttons react for AddExplanation"
    '            'QBSidebuttons1.ButtonsforScreen = QBSidebuttons.ForScreens.None
    '        Case QBSidebuttons.ForScreens.AddImage
    '            lblStatus.Text = "Buttons react for addimage"
    '            'QBSidebuttons1.ButtonsforScreen = QBSidebuttons.ForScreens.None
    '    End Select
    'End Sub

    Private Sub QBOtherFeatures1_QbOtherfeatures_OKClick() Handles QBOtherFeatures1.QbOtherfeatures_OKClick
        lblStatus.Text = ""
        ClearQTypeSessionVars()
        Select Case QBOtherFeatures1.SelectedFeature
            Case QBOtherfeatures.Features.AddExplanation
                Session("explanation") = QBOtherFeatures1.Explanation
            Case QBOtherfeatures.Features.Filter_Grade
                Session("grade_selected") = QBOtherFeatures1.GradeID
            Case QBOtherfeatures.Features.Filter_Standard
                Session("standard_selected") = QBOtherFeatures1.StandardID
            Case QBOtherfeatures.Features.Filter_QID
                'Dim strQuestionID1 As String = QBOtherFeatures1.QuestionID.Replace("QUES", "") 'remove "QUES" prefix
                'Dim iQuestionID As Integer = Integer.Parse(strQuestionID1)
                'Session("qid_selected") = iQuestionID
                Session("qid_selected") = QBOtherFeatures1.QuestionID
            Case QBOtherfeatures.Features.Filter_Subject
                Session("subject_selected") = QBOtherFeatures1.SubjectsID
            Case QBOtherfeatures.Features.Filter_QuestionType
                Session("qtype_selected") = QBOtherFeatures1.QuestionTypeID
            Case QBOtherfeatures.Features.Filter_Search
                Session("search_selected") = QBOtherFeatures1.SearchTerm
        End Select
        QBFilters1.Visible = True
        QuestionsListHolder.Controls.Clear()
        LoadQuestions(True)
    End Sub

    Private Sub CreateQuestion1_CreateQuestion_AddExplanationClick() Handles CreateQuestion1.CreateQuestion_AddExplanationClick
        lblStatus.Text = ""
        divQuestionsList.Visible = False
        divOtherfeatures.Visible = True
        QBFilters1.Visible = False
        QBOtherFeatures1.SelectedFeature = QBOtherfeatures.Features.AddExplanation
        'Dim ctrlQbOtherFeatures As Control = Page.LoadControl("~/QuestionBank/QBOtherFeatures.ascx")
        'ctrlQbOtherFeatures = Page.LoadControl("~/QuestionBank/QBOtherFeatures.ascx")
        'Dim qbOtherFeatures1 As QBOtherfeatures
        'qbOtherFeatures1 = CType(ctrlQbOtherFeatures, QBOtherfeatures)
        'QuestionsListHolder.Controls.Add(qbOtherFeatures1)
        'QBSidebuttons1.ButtonsforScreen = QBSidebuttons.ForScreens.AddExplanation
        'AddHandler QBSidebuttons1.QbSideButtons_OKClick, AddressOf AddExplanation
    End Sub

    Private Sub CreateQuestion1_CreateQuestion_AddImageClick() Handles CreateQuestion1.CreateQuestion_AddImageClick
        lblStatus.Text = ""
        divQuestionsList.Visible = False
        divOtherfeatures.Visible = True
        QBFilters1.Visible = False
        QBOtherFeatures1.SelectedFeature = QBOtherfeatures.Features.AddImage
        'Dim ctrlQbOtherFeatures As Control = Page.LoadControl("~/QuestionBank/QBOtherFeatures.ascx")
        'ctrlQbOtherFeatures = Page.LoadControl("~/QuestionBank/QBOtherFeatures.ascx")
        'Dim qbOtherFeatures1 As QBOtherfeatures
        'qbOtherFeatures1 = CType(ctrlQbOtherFeatures, QBOtherfeatures)
        'QuestionsListHolder.Controls.Add(qbOtherFeatures1)
        'QBSidebuttons1.ButtonsforScreen = QBSidebuttons.ForScreens.AddExplanation
        'AddHandler QBSidebuttons1.QbSideButtons_OKClick, AddressOf AddExplanation
    End Sub

    Private Sub QBFilters1_QBFilters_FilterClicked(enumFilter As QBFilters.Filters) Handles QBFilters1.QBFilters_FilterClicked
        lblStatus.Text = ""
        divQuestionsList.Visible = False
        divOtherfeatures.Visible = True
        QBFilters1.Visible = True
        Select Case enumFilter
            Case QBFilters.Filters.Standard
                QBOtherFeatures1.SelectedFeature = QBOtherfeatures.Features.Filter_Standard
            Case QBFilters.Filters.Grade
                QBOtherFeatures1.SelectedFeature = QBOtherfeatures.Features.Filter_Grade
            Case QBFilters.Filters.Subject
                QBOtherFeatures1.SelectedFeature = QBOtherfeatures.Features.Filter_Subject
            Case QBFilters.Filters.QID
                QBOtherFeatures1.SelectedFeature = QBOtherfeatures.Features.Filter_QID
            Case QBFilters.Filters.Questiontype
                QBOtherFeatures1.SelectedFeature = QBOtherfeatures.Features.Filter_QuestionType
            Case QBFilters.Filters.Search
                QBOtherFeatures1.SelectedFeature = QBOtherfeatures.Features.Filter_Search
            Case QBFilters.Filters.None
                divQuestionsList.Visible = True
                divOtherfeatures.Visible = False

        End Select

    End Sub

    Private Sub lnkCreateQuestion_Click(sender As Object, e As EventArgs) Handles lnkCreateQuestion.Click
        If CreateQuestion1.Visible = False Then
            divCreateQ_Heading.Visible = True
            divCreateQuestionArea.Visible = True
            lblStatus.Visible = True
        Else
            divCreateQ_Heading.Visible = False
            divCreateQuestionArea.Visible = False
            lblStatus.Text = ""
            lblStatus.Visible = False
        End If
    End Sub
End Class