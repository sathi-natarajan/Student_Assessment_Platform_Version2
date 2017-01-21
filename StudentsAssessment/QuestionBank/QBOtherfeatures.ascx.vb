Imports System.Data.SqlClient

Public Class QBOtherfeatures
    Inherits System.Web.UI.UserControl

    Public Enum Features
        AddExplanation
        AddImage
        Filter_Standard
        Filter_Subject
        Filter_Grade
        Filter_QID
        Filter_QuestionType
        Filter_Search
        None
    End Enum

    Public Enum SearchOptions
        ByExplanation
        ByWordsinQuestiontext
    End Enum
    Shared strExplanation As String
    Shared enumFeature As Features
    Public Event QbOtherfeatures_OKClick()

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub

    Protected Sub lnkOK_Click(sender As Object, e As EventArgs) Handles lnkOK.Click, lnkOK1.Click, lnkOK2.Click, lnkOK3.Click, lnkOK4.Click,
            lnkOK5.Click, lnkOK6.Click, lnkOK7.Click
        Dim lnkButton As LinkButton = CType(sender, LinkButton)
        Select Case lnkButton.ID
            Case "lnkOK"
                strExplanation = txtExplanation.Text
        End Select
        RaiseEvent QbOtherfeatures_OKClick()
    End Sub

    Public ReadOnly Property Explanation() As String
        'Set(value As String)
        '    strExplanation = value
        '    txtExplanation.Text = value
        'End Set
        Get
            Return strExplanation
        End Get
    End Property

    Public ReadOnly Property StandardID() As String
        Get
            Return ddlStandards.SelectedValue
        End Get
    End Property

    Public ReadOnly Property GradeID() As String
        Get
            Return ddlGrades.SelectedValue
        End Get
    End Property

    Public ReadOnly Property QuestionID() As String
        Get
            Return ddlQIDs.SelectedValue
        End Get
    End Property

    Public ReadOnly Property SubjectsID() As String
        Get
            Return ddlSubjects.SelectedValue
        End Get
    End Property

    Public ReadOnly Property QuestionTypeID() As String
        Get
            Return ddlQTypes.SelectedValue
        End Get
    End Property

    Public ReadOnly Property SearchTerm() As String
        Get
            Return txtSearchTerm.Text
        End Get
    End Property

    Public ReadOnly Property SearchOption() As String
        Get
            Return txtSearchTerm.Text
        End Get
    End Property

    Public Sub ClearExplanation()
        txtExplanation.Text = ""
    End Sub

    Public Property SelectedFeature() As Features
        Get
            Return enumFeature
        End Get
        Set(value As Features)
            Select Case value
                Case Features.AddExplanation
                    divAddExplanation.Visible = True
                    divAddImage.Visible = False
                    divStandardFilter.Visible = False
                    divSubjectFilter.Visible = False
                    divGradeFilter.Visible = False
                    divQIDFilter.Visible = False
                    divQTypeFilter.Visible = False
                    divSearchFilter.Visible = False
                    enumFeature = Features.AddExplanation
                    '
                Case Features.AddImage
                    divAddExplanation.Visible = False
                    divAddImage.Visible = True
                    divStandardFilter.Visible = False
                    divSubjectFilter.Visible = False
                    divGradeFilter.Visible = False
                    divGradeFilter.Visible = False
                    divQIDFilter.Visible = False
                    divQTypeFilter.Visible = False
                    divSearchFilter.Visible = False
                    enumFeature = Features.AddImage
                Case Features.Filter_Standard
                    divAddExplanation.Visible = False
                    divAddImage.Visible = False
                    divStandardFilter.Visible = True
                    divSubjectFilter.Visible = False
                    divGradeFilter.Visible = False
                    divQIDFilter.Visible = False
                    divQTypeFilter.Visible = False
                    divSearchFilter.Visible = False
                    enumFeature = Features.Filter_Standard
                    ddlStandards.Items.Clear()
                    LoadStandards()
                Case Features.Filter_Subject
                    divAddExplanation.Visible = False
                    divAddImage.Visible = False
                    divStandardFilter.Visible = False
                    divSubjectFilter.Visible = True
                    divGradeFilter.Visible = False
                    divQIDFilter.Visible = False
                    divQTypeFilter.Visible = False
                    divSearchFilter.Visible = False
                    enumFeature = Features.Filter_Subject
                    ddlSubjects.Items.Clear()
                    LoadSubjects()
                Case Features.Filter_Grade
                    divAddExplanation.Visible = False
                    divAddImage.Visible = False
                    divStandardFilter.Visible = False
                    divSubjectFilter.Visible = False
                    divGradeFilter.Visible = True
                    divQIDFilter.Visible = False
                    divQTypeFilter.Visible = False
                    divSearchFilter.Visible = False
                    enumFeature = Features.Filter_Grade
                    ddlGrades.Items.Clear()
                    LoadGrades()
                Case Features.Filter_QID
                    divAddExplanation.Visible = False
                    divAddImage.Visible = False
                    divStandardFilter.Visible = False
                    divSubjectFilter.Visible = False
                    divGradeFilter.Visible = False
                    divQIDFilter.Visible = True
                    divQTypeFilter.Visible = False
                    enumFeature = Features.Filter_QID
                    divSearchFilter.Visible = False
                    ddlQIDs.Items.Clear()
                    LoadQIDs()
                Case Features.Filter_QuestionType
                    divAddExplanation.Visible = False
                    divAddImage.Visible = False
                    divStandardFilter.Visible = False
                    divSubjectFilter.Visible = False
                    divGradeFilter.Visible = False
                    divQIDFilter.Visible = False
                    divQTypeFilter.Visible = True
                    divSearchFilter.Visible = False
                    enumFeature = Features.Filter_QuestionType
                    ddlQTypes.Items.Clear()
                    LoadQuestionTypes()
                Case Features.Filter_Search
                    divAddExplanation.Visible = False
                    divAddImage.Visible = False
                    divStandardFilter.Visible = False
                    divSubjectFilter.Visible = False
                    divGradeFilter.Visible = False
                    divQIDFilter.Visible = False
                    divQTypeFilter.Visible = False
                    divSearchFilter.Visible = True
                    enumFeature = Features.Filter_QuestionType
                Case Else
                    divAddExplanation.Visible = False
                    divAddImage.Visible = False
                    divStandardFilter.Visible = False
                    divSubjectFilter.Visible = False
                    divGradeFilter.Visible = False
                    divQIDFilter.Visible = False
                    enumFeature = Features.None
            End Select
        End Set
    End Property

    Private Sub LoadStandards()
        Dim strConn As String = GetConnectStringFromWebConfig()
        Try
            Using objconn As New SqlConnection(strConn)
                Dim strSQL As String = <![CDATA[
              SELECT * from Standards
    ]]>.Value()
                Using objcommand As New SqlCommand
                    With objcommand
                        .Connection = objconn
                        .Connection.Open()
                        .CommandText = strSQL
                    End With
                    Using objReader As SqlDataReader = objcommand.ExecuteReader()
                        Dim liItem As ListItem
                        While objReader.Read()
                            'qbQuestion = New QuestionBox 'This usage won't work for user controls
                            'qbQuestion.QuestionNo = Integer.Parse(objReader("QuestionID"))
                            liItem = New ListItem
                            liItem.Text = objReader("Standardname").ToString
                            liItem.Value = objReader("StandardID").ToString
                            ddlStandards.Items.Add(liItem)
                        End While
                    End Using 'objReader
                End Using 'objcommand
            End Using 'objConnection
        Catch ex As Exception
            lblStatus.Text = "Problem loading standards.  Please let administrator know.  And please retry at a later time"
        End Try
    End Sub

    Private Sub LoadSubjects()
        Dim strConn As String = GetConnectStringFromWebConfig()
        Try
            Using objconn As New SqlConnection(strConn)
                Dim strSQL As String = <![CDATA[
              SELECT * from Subjects
    ]]>.Value()
                Using objcommand As New SqlCommand
                    With objcommand
                        .Connection = objconn
                        .Connection.Open()
                        .CommandText = strSQL
                    End With
                    Using objReader As SqlDataReader = objcommand.ExecuteReader()
                        Dim liItem As ListItem
                        While objReader.Read()
                            'qbQuestion = New QuestionBox 'This usage won't work for user controls
                            'qbQuestion.QuestionNo = Integer.Parse(objReader("QuestionID"))
                            liItem = New ListItem
                            liItem.Text = objReader("Subjectname").ToString
                            liItem.Value = objReader("SubjectID").ToString
                            ddlSubjects.Items.Add(liItem)
                        End While
                    End Using 'objReader
                End Using 'objcommand
            End Using 'objConnection
        Catch ex As Exception
            lblStatus.Text = "Problem loading subjects.  Please let administrator know.  And please retry at a later time"
        End Try
    End Sub
    Private Sub LoadGrades()
        Dim strConn As String = GetConnectStringFromWebConfig()
        Try
            Using objconn As New SqlConnection(strConn)
                Dim strSQL As String = <![CDATA[
              SELECT * from Classes
    ]]>.Value()
                Using objcommand As New SqlCommand
                    With objcommand
                        .Connection = objconn
                        .Connection.Open()
                        .CommandText = strSQL
                    End With
                    Using objReader As SqlDataReader = objcommand.ExecuteReader()
                        Dim liItem As ListItem
                        While objReader.Read()
                            'qbQuestion = New QuestionBox 'This usage won't work for user controls
                            'qbQuestion.QuestionNo = Integer.Parse(objReader("QuestionID"))
                            liItem = New ListItem
                            liItem.Text = objReader("Classname").ToString
                            liItem.Value = objReader("ClassID").ToString
                            ddlGrades.Items.Add(liItem)
                        End While
                    End Using 'objReader
                End Using 'objcommand
            End Using 'objConnection
        Catch ex As Exception
            lblStatus.Text = "Problem loading grade levels.  Please let administrator know.  And please retry at a later time"
        End Try
    End Sub

    Private Sub LoadQIDs()
        Dim strConn As String = GetConnectStringFromWebConfig()
        Try
            Using objconn As New SqlConnection(strConn)
                Dim strSQL As String = <![CDATA[
              SELECT * from vuQuestions
    ]]>.Value()
                Using objcommand As New SqlCommand
                    With objcommand
                        .Connection = objconn
                        .Connection.Open()
                        .CommandText = strSQL
                    End With
                    Using objReader As SqlDataReader = objcommand.ExecuteReader()
                        Dim liItem As ListItem
                        While objReader.Read()
                            'qbQuestion = New QuestionBox 'This usage won't work for user controls
                            'qbQuestion.QuestionNo = Integer.Parse(objReader("QuestionID"))
                            liItem = New ListItem
                            liItem.Text = objReader("QuestionID").ToString
                            liItem.Value = objReader("QuestionID").ToString
                            ddlQIDs.Items.Add(liItem)
                        End While
                    End Using 'objReader
                End Using 'objcommand
            End Using 'objConnection
        Catch ex As Exception
            lblStatus.Text = "Problem loading question IDs.  Please let administrator know.  And please retry at a later time"
        End Try
    End Sub

    Private Sub LoadQuestionTypes()
        Dim liItem As ListItem
        liItem = New ListItem
        liItem.Text = "Short Answer"
        liItem.Value = "1"
        ddlQTypes.Items.Add(liItem)
        liItem = New ListItem
        liItem.Text = "Yes/No"
        liItem.Value = "2"
        ddlQTypes.Items.Add(liItem)
        liItem = New ListItem
        liItem.Text = "Multiple Choice"
        liItem.Value = "3"
        ddlQTypes.Items.Add(liItem)
        liItem = New ListItem
        liItem.Text = "Multi Choice"
        liItem.Value = "4"
        ddlQTypes.Items.Add(liItem)
    End Sub
End Class