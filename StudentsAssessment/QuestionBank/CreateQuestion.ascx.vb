Public Class CreateQuestion
    Inherits System.Web.UI.UserControl

    Public Event CreateQuestion_CreateQuestionClick()
    Public Event CreateQuestion_AddExplanationClick()
    Public Event CreateQuestion_AddImageClick()
    Public Enum OptiontoHighlight
        Standard1
        Standard2
        Standard3
    End Enum

    Shared iStandardSelection As Integer = -1
    Shared iAnswerType As Integer = -1

    Public Property ErrorMessage() As String

    Private Sub RestrictfieldLengths()
        txtQuestionStem.MaxLength = Integer.Parse(AssessmentTableLengths.Questions.QuestionsText)

        txtChoiceA.MaxLength = Integer.Parse(AssessmentTableLengths.Answers.AnswerText)
        txtChoiceB.MaxLength = Integer.Parse(AssessmentTableLengths.Answers.AnswerText)
        txtChoiceC.MaxLength = Integer.Parse(AssessmentTableLengths.Answers.AnswerText)
        txtChoiceD.MaxLength = Integer.Parse(AssessmentTableLengths.Answers.AnswerText)

        txtChoiceA1.MaxLength = Integer.Parse(AssessmentTableLengths.Answers.AnswerText)
        txtChoiceB1.MaxLength = Integer.Parse(AssessmentTableLengths.Answers.AnswerText)
        txtChoiceC1.MaxLength = Integer.Parse(AssessmentTableLengths.Answers.AnswerText)
        txtChoiceD1.MaxLength = Integer.Parse(AssessmentTableLengths.Answers.AnswerText)
        txtShortAns.MaxLength = Integer.Parse(AssessmentTableLengths.Answers.AnswerText)
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'Dim strjQueryBuilder As New StringBuilder
        'strjQueryBuilder.Append("<script type=""text/javascript"">" + vbCrLf)
        ''strjQueryBuilder.Append("$(""#<%=lnkbtnAddExplanation.ClientID()%>"").click(Function() {" + vbCrLf)
        ''strjQueryBuilder.Append("jPrompt('Please provide an explanation to add to question:', '', 'Prompt Dialog', function (r) {" + vbCrLf)
        ''strjQueryBuilder.Append("If (r) {" + vbCrLf)
        ''strjQueryBuilder.Append("$(""#<%=hdnExplanation.ClientID()%>"").val(r); //This Is working" + vbCrLf)
        ''strjQueryBuilder.Append("jAlert('Please do not forget to click on CREATE QUESTION to have this explanation saved with question', 'ACTION REQUIRED'); //Working" + vbCrLf)
        ''strjQueryBuilder.Append("             }});" + vbCrLf)
        ''strjQueryBuilder.Append("return false;});")
        'strjQueryBuilder.Append("alert(""here"");")
        'strjQueryBuilder.Append("</script>")
        'Page.ClientScript.RegisterClientScriptBlock(Me.GetType(), "MessageBox", strjQueryBuilder.ToString())
        RestrictfieldLengths()
    End Sub

    Protected Sub lnkStandard1_Click(sender As Object, e As EventArgs) Handles lnkStandard1.Click
        'lnkStandard1.BackColor = Drawing.Color.Red
        'lnkStandard2.BackColor = Drawing.Color.DimGray
        'lnkStandard3.BackColor = Drawing.Color.DimGray
        iStandardSelection = 1
        'highlighting and unhighlighting
        divStandard1.Style.Add("background-color", "red")
        divStandard2.Style.Add("background-color", "dimgray")
        divStandard3.Style.Add("background-color", "dimgray")
    End Sub

    Protected Sub lnkStandard2_Click(sender As Object, e As EventArgs) Handles lnkStandard2.Click
        'lnkStandard1.BackColor = Drawing.Color.DimGray
        'lnkStandard2.BackColor = Drawing.Color.Red
        'lnkStandard3.BackColor = Drawing.Color.DimGray
        iStandardSelection = 2
        divStandard1.Style.Add("background-color", "dimgray")
        divStandard2.Style.Add("background-color", "red")
        divStandard3.Style.Add("background-color", "dimgray")
    End Sub

    Protected Sub lnkStandard3_Click(sender As Object, e As EventArgs) Handles lnkStandard3.Click
        'lnkStandard1.BackColor = Drawing.Color.DimGray
        'lnkStandard2.BackColor = Drawing.Color.DimGray
        'lnkStandard3.BackColor = Drawing.Color.Red
        iStandardSelection = 3
        divStandard1.Style.Add("background-color", "dimgray")
        divStandard2.Style.Add("background-color", "dimgray")
        divStandard3.Style.Add("background-color", "red")
    End Sub

    Protected Sub lnkAnswertype1_Click(sender As Object, e As EventArgs) Handles lnkAnswertype1.Click
        'lnkAnswertype1.BackColor = Drawing.Color.Red
        'lnkAnswertype2.BackColor = Drawing.Color.DimGray
        'lnkAnswertype3.BackColor = Drawing.Color.DimGray
        'lnkAnswertype4.BackColor = Drawing.Color.DimGray
        divShortAns.Visible = True
        divTrueFalse.Visible = False
        divMultipleChoices.Visible = False
        divMultiChoices.Visible = False
        iAnswerType = 1

        divAnswertype1.Style.Add("background-color", "red")
        divAnswertype2.Style.Add("background-color", "dimgray")
        divAnswertype3.Style.Add("background-color", "dimgray")
        divAnswertype4.Style.Add("background-color", "dimgray")
        txtQuestionStem.Text = ""
        txtShortAns.Text = ""
    End Sub

    Protected Sub lnkAnswertype2_Click(sender As Object, e As EventArgs) Handles lnkAnswertype2.Click
        'lnkAnswertype1.BackColor = Drawing.Color.DimGray
        'lnkAnswertype2.BackColor = Drawing.Color.Red
        'lnkAnswertype3.BackColor = Drawing.Color.DimGray
        'lnkAnswertype4.BackColor = Drawing.Color.DimGray
        divShortAns.Visible = False
        divTrueFalse.Visible = True
        divMultipleChoices.Visible = False
        divMultiChoices.Visible = False
        iAnswerType = 2
        divAnswertype1.Style.Add("background-color", "dimgray")
        divAnswertype2.Style.Add("background-color", "red")
        divAnswertype3.Style.Add("background-color", "dimgray")
        divAnswertype4.Style.Add("background-color", "dimgray")
        txtQuestionStem.Text = ""
    End Sub

    Protected Sub lnkAnswertype3_Click(sender As Object, e As EventArgs) Handles lnkAnswertype3.Click
        'lnkAnswertype1.BackColor = Drawing.Color.DimGray
        'lnkAnswertype2.BackColor = Drawing.Color.DimGray
        'lnkAnswertype3.BackColor = Drawing.Color.Red
        'lnkAnswertype4.BackColor = Drawing.Color.DimGray
        divShortAns.Visible = False
        divTrueFalse.Visible = False
        divMultipleChoices.Visible = True
        divMultiChoices.Visible = False
        iAnswerType = 3
        divAnswertype1.Style.Add("background-color", "dimgray")
        divAnswertype2.Style.Add("background-color", "dimgray")
        divAnswertype3.Style.Add("background-color", "red")
        divAnswertype4.Style.Add("background-color", "dimgray")
        txtQuestionStem.Text = ""
    End Sub

    Protected Sub lnkAnswertype4_Click(sender As Object, e As EventArgs) Handles lnkAnswertype4.Click
        'lnkAnswertype1.BackColor = Drawing.Color.DimGray
        'lnkAnswertype2.BackColor = Drawing.Color.DimGray
        'lnkAnswertype3.BackColor = Drawing.Color.DimGray
        'lnkAnswertype4.BackColor = Drawing.Color.Red
        divShortAns.Visible = False
        divTrueFalse.Visible = False
        divMultipleChoices.Visible = False
        divMultiChoices.Visible = True
        iAnswerType = 4
        divAnswertype1.Style.Add("background-color", "dimgray")
        divAnswertype2.Style.Add("background-color", "dimgray")
        divAnswertype3.Style.Add("background-color", "dimgray")
        divAnswertype4.Style.Add("background-color", "red")
        txtQuestionStem.Text = ""
    End Sub

    Public ReadOnly Property GetStandardSelection() As Integer
        Get
            'If lnkStandard1.ForeColor = Drawing.Color.Red Then
            '    Return 1
            'ElseIf lnkStandard2.ForeColor = Drawing.Color.Red Then
            '    Return 2
            'ElseIf lnkStandard3.ForeColor = Drawing.Color.Red Then
            '    Return 3
            'Else
            '    Return -1 'none selected?  some problem
            'End If
            Return iStandardSelection
        End Get
    End Property

    Public ReadOnly Property GetAnswertypeSelection() As Integer
        Get
            Return iAnswerType
        End Get
    End Property

    Public ReadOnly Property GetShortAnswer() As String
        Get
            Return txtShortAns.Text
        End Get
    End Property

    Public ReadOnly Property GetTrueorFalse() As Dictionary(Of String, Boolean)
        Get
            Dim objChoices As New Dictionary(Of String, Boolean)

            If rbtnTrue.Checked Then
                objChoices.Add("True", True)
                objChoices.Add("False", False)
            Else
                objChoices.Add("True", False)
                objChoices.Add("False", True)
            End If
            Return objChoices
        End Get
    End Property

    Public ReadOnly Property GetMultipleChoice() As Dictionary(Of String, Boolean)
        Get
            Dim objChoices As New Dictionary(Of String, Boolean)
            objChoices.Add(txtChoiceA.Text, rbtnChoiceA.Checked)
            objChoices.Add(txtChoiceB.Text, rbtnChoiceB.Checked)
            objChoices.Add(txtChoiceC.Text, rbtnChoiceC.Checked)
            objChoices.Add(txtChoiceD.Text, rbtnChoiceD.Checked)
            Return objChoices
        End Get
    End Property

    Public ReadOnly Property GetMultiChoice() As Dictionary(Of String, Boolean)
        Get
            Dim objChoices As New Dictionary(Of String, Boolean)
            objChoices.Add(txtChoiceA1.Text, chkbtnChoiceA1.Checked)
            objChoices.Add(txtChoiceB1.Text, chkbtnChoiceB1.Checked)
            objChoices.Add(txtChoiceC1.Text, chkbtnChoiceC1.Checked)
            objChoices.Add(txtChoiceD1.Text, chkbtnChoiceD1.Checked)
            Return objChoices
        End Get
    End Property

    Public ReadOnly Property GetQuestionStem() As String
        Get
            Return txtQuestionStem.Text
        End Get
    End Property

    'Public Function GetDescription() As String
    '    Return hdnExplanation.Value
    'End Function
    Public Function CheckQuestionAnswers() As Boolean
        Dim bOK As Boolean = False
        If GetStandardSelection < 0 Or iAnswerType < 0 Then
            ErrorMessage = "Both Standard and the type of answer must be selected"
            bOK = False
        Else
            bOK = Not String.IsNullOrEmpty(GetQuestionStem())
            If Not bOK Then
                ErrorMessage = "The Question stem was not provided for this question"
            Else
                Select Case iAnswerType
                    Case = 1
                        bOK = String.IsNullOrEmpty(txtShortAns.Text) = False 'the short ans must have been provided
                        If Not bOK Then ErrorMessage = "The short answer was not provied for this question"
                    Case = 2
                        bOK = rbtnFalse.Checked Or rbtnTrue.Checked 'one of them must be have been checked
                        If Not bOK Then ErrorMessage = "'True' or 'False' was not checked for this question"
                    Case = 3
                        bOK = String.IsNullOrEmpty(txtChoiceA.Text) = False And
                                String.IsNullOrEmpty(txtChoiceB.Text) = False And
                                String.IsNullOrEmpty(txtChoiceC.Text) = False And
                                String.IsNullOrEmpty(txtChoiceD.Text) = False And
                                (rbtnChoiceA.Checked Or rbtnChoiceB.Checked Or rbtnChoiceC.Checked Or rbtnChoiceD.Checked)
                        If Not bOK Then ErrorMessage = "Either all answers are not provided or the radiobutton for one of the provided answers is not selected"
                    Case = 4
                        bOK = String.IsNullOrEmpty(txtChoiceA1.Text) = False And
                                String.IsNullOrEmpty(txtChoiceB1.Text) = False And
                                String.IsNullOrEmpty(txtChoiceC1.Text) = False And
                                String.IsNullOrEmpty(txtChoiceD1.Text) = False And
                                (chkbtnChoiceA1.Checked Or chkbtnChoiceB1.Checked Or chkbtnChoiceC1.Checked Or chkbtnChoiceD1.Checked)
                        If Not bOK Then ErrorMessage = "Either all answers are not provided or the checkbox for atleast one of the provided answers is not selected"
                End Select
                If bOK Then
                    Dim strError As String = ""
                    bOK = ValidInputLengths(strError)
                    If Not bOK Then
                        ErrorMessage = strError
                    Else
                        ErrorMessage = ""
                    End If
                End If
            End If
        End If


        Return bOK
    End Function
    Protected Sub lnkCreateQuestion_Click(sender As Object, e As EventArgs) Handles lnkCreateQuestion.Click
        RaiseEvent CreateQuestion_CreateQuestionClick()
    End Sub

    Private Sub lnkbtnAddExplanation_Click(sender As Object, e As EventArgs) Handles lnkbtnAddExplanation.Click
        'Page.ClientScript.RegisterClientScriptBlock(Me.GetType(), "", "window.open('Page.aspx?QS=2','','width=500,height=500');", True)
        'Response.Redirect("QBOtheradditions.aspx?op=addexp")
        RaiseEvent CreateQuestion_AddExplanationClick()
    End Sub

    Private Sub lnkbtnAddImage_Click(sender As Object, e As EventArgs) Handles lnkbtnAddImage.Click
        'Page.ClientScript.RegisterClientScriptBlock(Me.GetType(), "", "window.open('Page.aspx?QS=2','','width=500,height=500');", True)
        'Response.Redirect("QBOtheradditions.aspx?op=addexp")
        RaiseEvent CreateQuestion_AddImageClick()
    End Sub

    ' check approp ones only dep on q type
    Private Function ValidInputLengths(ByRef strError As String) As Boolean
        Dim bValid As Boolean = True
        'Check question entry
        If txtQuestionStem.Text.Length > AssessmentTableLengths.Questions.QuestionsText Then
            strError = String.Format("QuestionStem cannot be more than {0} characters long", Integer.Parse(AssessmentTableLengths.Questions.QuestionsText))
            bValid = False
        Else
            bValid = True
        End If
        'Check answers entry depending on type
        If iAnswerType = 3 Then
            If txtChoiceA.Text.Length > AssessmentTableLengths.Answers.AnswerText Then
                strError = String.Format("ChoiceA cannot be more than {0} characters long", Integer.Parse(AssessmentTableLengths.Answers.AnswerText))
                bValid = False
            ElseIf txtChoiceB.Text.Length > AssessmentTableLengths.Answers.AnswerText Then
                strError = String.Format("ChoiceB cannot be more than {0} characters long", Integer.Parse(AssessmentTableLengths.Answers.AnswerText))
                bValid = False
            ElseIf txtChoiceC.Text.Length > AssessmentTableLengths.Answers.AnswerText Then
                strError = String.Format("ChoiceC cannot be more than {0} characters long", Integer.Parse(AssessmentTableLengths.Answers.AnswerText))
                bValid = False
            ElseIf txtChoiceD.Text.Length > AssessmentTableLengths.Answers.AnswerText Then
                strError = String.Format("ChoiceD cannot be more than {0} characters long", Integer.Parse(AssessmentTableLengths.Answers.AnswerText))
                bValid = False
            End If
        ElseIf iAnswerType = 4 Then
            If txtChoiceA1.Text.Length > AssessmentTableLengths.Answers.AnswerText Then
                strError = String.Format("ChoiceA1 cannot be more than {0} characters long", Integer.Parse(AssessmentTableLengths.Answers.AnswerText))
                bValid = False
            ElseIf txtChoiceB1.Text.Length > AssessmentTableLengths.Answers.AnswerText Then
                strError = String.Format("ChoiceB1 cannot be more than {0} characters long", Integer.Parse(AssessmentTableLengths.Answers.AnswerText))
                bValid = False
            ElseIf txtChoiceC1.Text.Length > AssessmentTableLengths.Answers.AnswerText Then
                strError = String.Format("ChoiceC1 cannot be more than {0} characters long", Integer.Parse(AssessmentTableLengths.Answers.AnswerText))
                bValid = False
            ElseIf txtChoiceD1.Text.Length > AssessmentTableLengths.Answers.AnswerText Then
                strError = String.Format("ChoiceD1 cannot be more than {0} characters long", Integer.Parse(AssessmentTableLengths.Answers.AnswerText))
                bValid = False
            End If
        ElseIf iAnswerType = 1 Then
            If txtShortAns.Text.Length > AssessmentTableLengths.Answers.AnswerText Then
                strError = String.Format("ShortAns cannot be more than {0} characters long", Integer.Parse(AssessmentTableLengths.Answers.AnswerText))
                bValid = False
            End If
        Else
            bValid = True
        End If
        Return bValid
    End Function


End Class