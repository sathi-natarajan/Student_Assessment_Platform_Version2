Public Class QuestionBox
    Inherits System.Web.UI.UserControl

    Dim strQuestionID As String

    Public Enum Modes
        Edit
        Display
    End Enum

    Shared enumMode As Modes = Modes.Display
    Public Event QuestionBox_EditQuestionClicked(ByVal strQuestionID As String)
    Public Event QuestionBox_TrashQuestionClicked(ByVal strQuestionID As String)
    Public Event QuestionBox_DuplicateQNoClicked(ByVal strQuestionID As String)
    Public Event QuestionBox_AddtoAssessmentClicked(ByVal strQuestionID As String)
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        divQSnapshot.Visible = True
        divEditQSnapshot.Visible = False
    End Sub

    Public Property Mode() As Modes
        Get
            Return enumMode
        End Get
        Set(value As Modes)
            enumMode = value
            If enumMode = Modes.Edit Then
                divQSnapshot.Visible = False
                divEditQSnapshot.Visible = True
                lnkEditQuestion.Text = "D<br/>O<br/>N<br/>E"
            Else
                divQSnapshot.Visible = True
                divEditQSnapshot.Visible = False
                lnkEditQuestion.Text = "E<br/>D<br/>I<br/>T"
            End If
        End Set
    End Property
    Public Property QuestionNo() As String
        Get
            Return divQuestionNo.InnerText
        End Get
        Set(value As String)
            divQuestionNo.InnerText = value
            strQuestionID = value
        End Set
    End Property


    ''' <summary>
    ''' This is the regular displayed question.  Note that this will appear truncated if >20 chars
    ''' </summary>
    ''' <returns></returns>
    Public Property QuestionSnapshot() As String
        Get
            Return divQSnapshot.InnerText
        End Get
        Set(value As String)
            ''Display only first 20 characters
            If value.Length < 20 Then
                divQSnapshot.InnerText = value
            Else
                divQSnapshot.InnerText = value.Substring(0, 19)
            End If
            txtQuestionSnapshot.Text = value
        End Set
    End Property


    ''' <summary>
    ''' This is the edited question (in EDIT mode).  This is in its full form.
    ''' </summary>
    ''' <returns></returns>
    Public Property EditedQuestion() As String
        Get
            Return txtQuestionSnapshot.Text
        End Get
        Set(value As String)
            ''Display only first 20 characters
            'If value.Length < 20 Then
            '    divQSnapshot.InnerText = value
            'Else
            '    divQSnapshot.InnerText = value.Substring(0, 19)
            'End If
            txtQuestionSnapshot.Text = value
        End Set
    End Property

    Public Property Standard() As String
        Get
            Return divStandard.InnerText
        End Get
        Set(value As String)
            divStandard.InnerText = value
        End Set
    End Property

    Protected Sub lnkEditQuestion_Click(sender As Object, e As EventArgs) Handles lnkEditQuestion.Click
        RaiseEvent QuestionBox_EditQuestionClicked(strQuestionID)
    End Sub

    Protected Sub lnkDeleteQuestion_Click(sender As Object, e As EventArgs) Handles lnkDeleteQuestion.Click
        RaiseEvent QuestionBox_TrashQuestionClicked(strQuestionID)
    End Sub

    Protected Sub lnkDuplicateQID_Click(sender As Object, e As EventArgs) Handles lnkDuplicateQID.Click
        RaiseEvent QuestionBox_DuplicateQNoClicked(strQuestionID)
    End Sub

    Protected Sub lnkAddtoAssessment_Click(sender As Object, e As EventArgs) Handles lnkAddtoAssessment.Click
        RaiseEvent QuestionBox_AddtoAssessmentClicked(strQuestionID)
    End Sub


End Class