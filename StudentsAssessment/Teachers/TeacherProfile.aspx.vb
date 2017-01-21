Imports System.Data.SqlClient
Imports StudentsAssessment

Public Class TeacherProfile
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        SiteHeader1.Pagetitle = "TEACHER INTERFACE"
        SiteHeader1.PageDesc = "A place to modify teacher's profile"
        TeachersMenu1.HiglightOption = TeachersMenu.OptiontoHighlight.Home
        'Make sure they are logged in.  Otherwise, forece them to log in
        If Session("LoggedinTeacherID") IsNot Nothing AndAlso IsNumeric(Session("LoggedinTeacherID")) Then
            If Session("LoggedinTeacherID") IsNot Nothing AndAlso IsNumeric(Session("LoggedinTeacherID")) Then
                If Session("IsAdmin") IsNot Nothing AndAlso Session("IsAdmin") = True Then
                    'ADMIN has logged in
                    ' pnlAdminProfile.Visible = True
                    ' LoadTeachers()
                Else
                    ' pnlAdminProfile.Visible = False
                End If
                divNotLoggedIn1.Visible = False
                TeachersPanel1.Visible = True
                TeachersPanel1.Name = GetTeacherName(Convert.ToInt32(Session("LoggedinTeacherID")))
            Else
                TeachersPanel1.Visible = False
                'pnlAdminProfile.Visible = False
                divNotLoggedIn1.Visible = True
            End If
            lblTeachername.Text = GetTeacherName(Convert.ToInt32(Session("LoggedinTeacherID")))
            lblTeachername1.Text = lblTeachername.Text
            lblTeachername2.Text = lblTeachername.Text
            lblTeachername3.Text = lblTeachername.Text
            lblTeachername4.Text = lblTeachername.Text
            If Not IsPostBack Then
                LoadProfile(Integer.Parse(Session("LoggedinTeacherID")))
                LoadClassesTaughtBy(Integer.Parse(Session("LoggedinTeacherID")))
                LoadSubjectsTaughtby(Integer.Parse(Session("LoggedinTeacherID")))
                LoadStudentsTaughtBy(Integer.Parse(Session("LoggedinTeacherID")))
                LoadSchools(Integer.Parse(Session("LoggedinTeacherID")))
            Else
                'Admin has logged in and picked a teacher.
                If Session("AdminPickedTeacher") IsNot Nothing AndAlso IsNumeric(Session("AdminPickedTeacher")) Then
                    LoadClassesTaughtBy(Integer.Parse(Session("AdminPickedTeacher")))
                    LoadSubjectsTaughtby(Session("AdminPickedTeacher"))
                    LoadStudentsTaughtBy(Integer.Parse(Session("AdminPickedTeacher")))
                    'LoadProfile(Integer.Parse(Session("AdminPickedTeacher")))
                    lblTeachername.Text = GetTeacherName(Convert.ToInt32(Session("AdminPickedTeacher")))
                    lblTeachername1.Text = lblTeachername.Text
                    lblTeachername2.Text = lblTeachername.Text
                    lblTeachername3.Text = lblTeachername.Text
                    lblTeachername4.Text = lblTeachername.Text
                Else
                    LoadClassesTaughtBy(Integer.Parse(Session("LoggedinTeacherID")))
                    LoadSubjectsTaughtby(Session("LoggedinTeacherID"))
                    LoadStudentsTaughtBy(Integer.Parse(Session("LoggedinTeacherID")))
                    'LoadProfile(Integer.Parse(Session("LoggedinTeacherID")))
                    lblTeachername.Text = GetTeacherName(Convert.ToInt32(Session("LoggedinTeacherID")))
                    lblTeachername1.Text = lblTeachername.Text
                    lblTeachername2.Text = lblTeachername.Text
                    lblTeachername3.Text = lblTeachername.Text
                    lblTeachername4.Text = lblTeachername.Text
                End If
            End If
            DisableNonModifiableFields()
            RestrictfieldLengths()
        Else
            Response.Redirect("~/TeachersLogin.aspx")
        End If
    End Sub

    Private Sub RestrictfieldLengths()
        txtFirstname.MaxLength = Integer.Parse(AssessmentTableLengths.StaffMembers.Firstname)
        txtLastname.MaxLength = Integer.Parse(AssessmentTableLengths.StaffMembers.Lastname)
        txtDesc.MaxLength = Integer.Parse(AssessmentTableLengths.StaffMembers.Description)
        txtUsername.MaxLength = Integer.Parse(AssessmentTableLengths.StaffMembers.Username)
        txtPassword.MaxLength = Integer.Parse(AssessmentTableLengths.StaffMembers.Password)
    End Sub
    Private Sub DisableNonModifiableFields()
        If Session("IsAdmin") IsNot Nothing AndAlso Session("IsAdmin") = False Then
            'NON-ADMINS can only edit description and their part-time/fulltime status
            txtFirstname.Enabled = False
            txtLastname.Enabled = False
            txtUsername.Enabled = False
            txtPassword.Enabled = False
            txtJoindate.Enabled = False
            txtTermdate.Enabled = False
            chkblActive.Enabled = False
            chkblAdmin.Enabled = False
        Else
            'ADMINS can edit entire profile except the name of course
            txtFirstname.Enabled = False
            txtLastname.Enabled = False
        End If
    End Sub
    Private Sub LoadProfile(ByVal iTeacherID As Integer)
        Dim strConn As String = GetConnectStringFromWebConfig()
        Try
            Using objconn As New SqlConnection(strConn)
                Dim strSQL As String = <![CDATA[
                SELECT * FROM StaffMembers WHERE StaffID=@TeacherID
    ]]>.Value()
                Using objcommand As New SqlCommand
                    With objcommand
                        .Connection = objconn
                        .Connection.Open()
                        .CommandText = strSQL
                        .Parameters.AddWithValue("@TeacherID", iTeacherID)
                    End With

                    Using objReader As SqlDataReader = objcommand.ExecuteReader()
                        If objReader.Read() Then
                            txtFirstname.Text = objReader("Firstname").ToString()
                            txtLastname.Text = objReader("Lastname").ToString()
                            txtDesc.Text = objReader("Description").ToString()
                            txtUsername.Text = objReader("Username").ToString()
                            txtPassword.Text = objReader("Password").ToString()
                            txtJoindate.Text = DateTime.Parse(objReader("Joindate").ToString).ToShortDateString()
                            If Not IsDBNull(objReader("Termdate")) Then
                                txtTermdate.Text = DateTime.Parse(objReader("Termdate").ToString).ToShortDateString()
                            Else
                                txtTermdate.Text = ""
                            End If
                            If objReader("Parttime") = True Then
                                chkblPT.Items(0).Selected = True
                            Else
                                chkblPT.Items(1).Selected = True
                            End If

                            If objReader("ISAdmin") = True Then
                                chkblAdmin.Items(0).Selected = True
                            Else
                                chkblAdmin.Items(1).Selected = True
                            End If

                            If objReader("Active") = True Then
                                chkblActive.Items(0).Selected = True
                            Else
                                chkblActive.Items(1).Selected = True
                            End If
                            lblStatus.Text = ""
                        Else
                            'Some problem loading profile
                            lblStatus.Text = "LoadProfile"
                        End If
                    End Using 'objReader
                End Using 'objcommand
            End Using 'objConnection

            ''''''''''''''''''''
            'Dim objConn As New SqlConnection(strConn)
            'objConn.Open()
            ' Dim strSQL As String = ""
            'Dim objCommand As New SqlCommand(strSQL, objConn)

            ' Dim objReader As SqlDataReader = objCommand.ExecuteReader


            'objReader.Close()
            'objReader = Nothing
            'objCommand.Dispose()
            'objCommand = Nothing
            'objConn.Dispose()
            'objConn.Close()
            'objConn = Nothing
            'SqlConnection.ClearAllPools()
        Catch ex As Exception
            lblStatus.Text = "Problem area - LoadProfile<br/>" + ex.Message + "<br/>Error source:" + ex.Source
        End Try
    End Sub

    Protected Sub lnkReturn_Click(sender As Object, e As EventArgs) Handles lnkReturn.Click
        Response.Redirect("~/default.aspx")
    End Sub

    Protected Sub lnkSaveProfile_Click(sender As Object, e As EventArgs) Handles lnkSaveProfile.Click
        If Validdata() Then
            If Session("AdminPickedTeacher") IsNot Nothing AndAlso IsNumeric(Session("AdminPickedTeacher")) Then
                SaveProfile(Integer.Parse(Session("AdminPickedTeacher")))
            Else
                SaveProfile(Integer.Parse(Session("LoggedinTeacherID")))
            End If

        End If
    End Sub

    Private Function Validdata() As Boolean
        Dim bValid As Boolean = True
        If txtDesc.Text.Trim <> "" Then
            If txtFirstname.Text.Length > AssessmentTableLengths.StaffMembers.Firstname Then
                lblStatus.Text = String.Format("Firstname cannot be more than {0} characters long", Integer.Parse(AssessmentTableLengths.StaffMembers.Firstname))
                bValid = False
            ElseIf txtLastname.Text.Length > AssessmentTableLengths.StaffMembers.Lastname Then
                lblStatus.Text = String.Format("Lastname cannot be more than {0} characters long", Integer.Parse(AssessmentTableLengths.StaffMembers.Lastname))
                bValid = False
            ElseIf txtDesc.Text.Length > AssessmentTableLengths.StaffMembers.Description Then
                lblStatus.Text = String.Format("Description cannot be more than {0} characters long", Integer.Parse(AssessmentTableLengths.StaffMembers.Description))
                bValid = False
            ElseIf txtUsername.Text.Length > AssessmentTableLengths.StaffMembers.Username Then
                lblStatus.Text = String.Format("Username cannot be more than {0} characters long", Integer.Parse(AssessmentTableLengths.StaffMembers.Username))
                bValid = False
            ElseIf txtUsername.Text.Length > AssessmentTableLengths.StaffMembers.Password Then
                lblStatus.Text = String.Format("Password cannot be more than {0} characters long", Integer.Parse(AssessmentTableLengths.StaffMembers.Password))
                bValid = False
            Else
                bValid = True
            End If
        End If
        Return bValid
    End Function
    Private Sub SaveProfile(ByVal iTeacherID As Integer)
        Dim strConn As String = GetConnectStringFromWebConfig()
        'Dim objConn As New SqlConnection(strConn)
        Dim bValid As Boolean = False
        Try
            Using objconn As New SqlConnection(strConn)
                Dim strSQL As String = <![CDATA[
                    UPDATE StaffMembers SET Description=@Desc, Parttime=@Parttime WHERE StaffID=@TeacherID
    ]]>.Value()
                Using objcommand As New SqlCommand
                    With objcommand
                        .Connection = objconn
                        .Connection.Open()
                        .CommandText = strSQL
                        .Parameters.AddWithValue("@TeacherID", iTeacherID)
                        .Parameters.AddWithValue("@Desc", txtDesc.Text)
                    End With

                    If chkblPT.Items(0).Selected = True Then
                        objcommand.Parameters.AddWithValue("@Parttime", True)
                    ElseIf chkblPT.Items(1).Selected = True Then
                        objcommand.Parameters.AddWithValue("@Parttime", False)
                    End If
                    If objcommand.ExecuteNonQuery() <= 0 Then
                        lblStatus.Text = "Problem updating profile"
                    Else
                        lblStatus.Text = "Successfully updated profile"
                    End If

                End Using 'objcommand
            End Using 'objConnection
        Catch ex As Exception
            lblStatus.Text = "Problem area - SaveProfile<br/>" + ex.Message + "<br/>Error source:" + ex.Source
        End Try
    End Sub

    Private Sub LoadClassesTaughtBy(ByVal iTeacherID As Integer)
        Dim strClassesBuilder As New StringBuilder
        strClassesBuilder.Append(" <table border=""0"" cellpadding=""2"" cellspacing=""2"">")
        strClassesBuilder.Append("<tr style=""background-color: mediumpurple;"">")
        strClassesBuilder.Append("<td><strong>Class name</strong></td>")
        strClassesBuilder.Append("</tr>")
        Dim strConn As String = GetConnectStringFromWebConfig()
        Try
            Using objconn As New SqlConnection(strConn)
                Dim strSQL As String = <![CDATA[
                SELECT [EntryID],TeachersClasses.ClassID,[Classes].Classname FROM [AssessmentDB].[dbo].[TeachersClasses] 
            INNER JOIN Classes on TeachersClasses.ClassID=Classes.ClassID WHERE TeachersClasses.TeacherID=@TeacherID
    ]]>.Value()
                Using objcommand As New SqlCommand
                    With objcommand
                        .Connection = objconn
                        .Connection.Open()
                        .CommandText = strSQL
                        .Parameters.AddWithValue("@TeacherID", iTeacherID)
                    End With
                    Dim bTeachingClasses As Boolean = False
                    Using objReader As SqlDataReader = objcommand.ExecuteReader()
                        While objReader.Read()
                            strClassesBuilder.Append("<tr>")
                            strClassesBuilder.Append("<td>")
                            strClassesBuilder.Append(objReader("Classname"))
                            strClassesBuilder.Append("</td>")
                            bTeachingClasses = True
                            strClassesBuilder.Append("</tr>")
                        End While
                        strClassesBuilder.Append("</table>")
                        ClassesHolder.Controls.Clear()
                        If bTeachingClasses = False Then
                            ClassesHolder.Controls.Add(New LiteralControl("This teacher currently does not teach any classes"))
                        Else
                            ClassesHolder.Controls.Add(New LiteralControl(strClassesBuilder.ToString()))
                        End If
                    End Using 'objReader
                End Using 'objcommand
            End Using 'objConnection
        Catch ex As Exception
            lblStatus.Text = "Problem area - LoadClassestaughtBy<br/>" + ex.Message + "<br/>Error source:" + ex.Source
        End Try
    End Sub

    Private Sub LoadSchools(ByVal iTeacherID As Integer)
        Dim strClassesBuilder As New StringBuilder
        strClassesBuilder.Append("<table border=""0"" cellpadding=""2"" cellspacing=""2"">")
        strClassesBuilder.Append("<tr style=""background-color: mediumpurple;"">")
        strClassesBuilder.Append("<td><strong>School name</strong></td>")
        strClassesBuilder.Append("</tr>")
        Dim strConn As String = GetConnectStringFromWebConfig()
        Try
            Using objconn As New SqlConnection(strConn)
                Dim strSQL As String = <![CDATA[
                SELECT Schoolname FROM teachersschools 
        INNER JOIN Schools ON teachersschools.schoolid=Schools.schoolid
        WHERE teacherid=@TeacherID
    ]]>.Value()
                Using objcommand As New SqlCommand
                    With objcommand
                        .Connection = objconn
                        .Connection.Open()
                        .CommandText = strSQL
                        .Parameters.AddWithValue("@TeacherID", iTeacherID)
                    End With
                    Dim bTeachingClasses As Boolean = False
                    Using objReader As SqlDataReader = objcommand.ExecuteReader()
                        While objReader.Read()
                            strClassesBuilder.Append("<tr>")
                            strClassesBuilder.Append("<td>")
                            strClassesBuilder.Append(objReader("Schoolname"))
                            strClassesBuilder.Append("</td>")
                            bTeachingClasses = True
                            strClassesBuilder.Append("</tr>")
                        End While
                        strClassesBuilder.Append("</table>")
                        SchoolsBelongingHolder.Controls.Clear()
                        If bTeachingClasses = False Then
                            SchoolsBelongingHolder.Controls.Add(New LiteralControl("This teacher currently does not belong to any school"))
                        Else
                            SchoolsBelongingHolder.Controls.Add(New LiteralControl(strClassesBuilder.ToString()))
                        End If
                    End Using 'objReader
                End Using 'objcommand
            End Using 'objConnection
        Catch ex As Exception
            lblStatus.Text = "Problem area - LoadSchools<br/>" + ex.Message + "<br/>Error source:" + ex.Source
        End Try
    End Sub

    Private Sub LoadStudentsTaughtBy(ByVal iTeacherID As Integer)
        Dim strClassesBuilder As New StringBuilder
        strClassesBuilder.Append("<table border=""0"" cellpadding=""2"" cellspacing=""2"">")
        strClassesBuilder.Append("<tr style=""background-color: mediumpurple;"">")
        strClassesBuilder.Append("<td><strong>Student's name</strong></td>")
        strClassesBuilder.Append("<td><strong>Class</strong></td>")
        strClassesBuilder.Append("</tr>")
        Dim strConn As String = GetConnectStringFromWebConfig()
        Try
            Using objconn As New SqlConnection(strConn)
                Dim strSQL As String = <![CDATA[
             Select TOP 1000 [EntryID],Classname,Firstname + ' ' +Lastname AS Studentname,[TeacherID] FROM [AssessmentDB].[dbo].[ClassesStudents] 
            INNER JOIN Students ON ClassesStudents.StudentID=Students.StudentID INNER JOIN Classes ON ClassesStudents.ClassID=Classes.ClassId 
            WHERE TeacherID=@TeacherID
    ]]>.Value()
                Using objcommand As New SqlCommand
                    With objcommand
                        .Connection = objconn
                        .Connection.Open()
                        .CommandText = strSQL
                        .Parameters.AddWithValue("@TeacherID", iTeacherID)
                    End With
                    Dim bTeachingStudents As Boolean = False
                    Using objReader As SqlDataReader = objcommand.ExecuteReader()
                        While objReader.Read()
                            strClassesBuilder.Append("<tr>")
                            strClassesBuilder.Append("<td>")
                            strClassesBuilder.Append(objReader("Studentname").ToString())
                            strClassesBuilder.Append("</td>")
                            strClassesBuilder.Append("<td>")
                            strClassesBuilder.Append(objReader("Classname").ToString())
                            strClassesBuilder.Append("</td>")
                            bTeachingStudents = True
                            strClassesBuilder.Append("</tr>")
                        End While
                        strClassesBuilder.Append("</table>")
                        StudentsHolder1.Controls.Clear()
                        If bTeachingStudents = False Then
                            StudentsHolder1.Controls.Add(New LiteralControl("This teacher currently does not teach any students in any class"))
                        Else
                            StudentsHolder1.Controls.Add(New LiteralControl(strClassesBuilder.ToString()))
                        End If
                    End Using 'objReader
                End Using 'objcommand
            End Using 'objConnection
        Catch ex As Exception
            lblStatus.Text = "Problem area - LoadStudentsTaughtBy<br/>" + ex.Message + "<br/>Error source:" + ex.Source
        End Try
    End Sub

    Private Sub LoadSubjectsTaughtby(ByVal iTeacherID As Integer)
        Dim strSubjectsTaughtBuilder As New StringBuilder
        strSubjectsTaughtBuilder.Append("<table border=""0"" cellpadding=""2"" cellspacing=""2"">")
        strSubjectsTaughtBuilder.Append("<tr style=""background-color: mediumpurple;"">")
        strSubjectsTaughtBuilder.Append("<td><strong>Class</strong></td>")
        strSubjectsTaughtBuilder.Append("<td><strong>Subject</strong></td>")
        strSubjectsTaughtBuilder.Append("</tr>")
        Dim strConn As String = GetConnectStringFromWebConfig()
        Try
            Using objconn As New SqlConnection(strConn)
                Dim strSQL As String = <![CDATA[
           SELECT EntryID, Classname,Subjectname,[TeacherID] FROM [AssessmentDB].[dbo].[ClassesSubjects] 
            inner join Subjects on ClassesSubjects.subjectid=Subjects.subjectid 
            inner join classes on ClassesSubjects.ClassID=Classes.ClassID where teacherid=@TeacherID
    ]]>.Value()
                Using objcommand As New SqlCommand
                    With objcommand
                        .Connection = objconn
                        .Connection.Open()
                        .CommandText = strSQL
                        .Parameters.AddWithValue("@TeacherID", iTeacherID)
                    End With
                    Dim bTeachingSubjects As Boolean = False
                    Using objReader As SqlDataReader = objcommand.ExecuteReader()
                        While objReader.Read()
                            bTeachingSubjects = True
                            strSubjectsTaughtBuilder.Append("<tr>")
                            strSubjectsTaughtBuilder.Append("<td>")
                            strSubjectsTaughtBuilder.Append(objReader("Classname").ToString)
                            strSubjectsTaughtBuilder.Append("</td>")
                            strSubjectsTaughtBuilder.Append("<td>")
                            strSubjectsTaughtBuilder.Append(objReader("Subjectname").ToString)
                            strSubjectsTaughtBuilder.Append("</td>")
                            strSubjectsTaughtBuilder.Append("</tr>")
                            bTeachingSubjects = True
                        End While
                        strSubjectsTaughtBuilder.Append("</table>")
                        SubjectsHolder1.Controls.Clear()
                        If bTeachingSubjects = False Then
                            SubjectsHolder1.Controls.Add(New LiteralControl("This teacher does not teach any subjects in any class"))
                        Else
                            SubjectsHolder1.Controls.Add(New LiteralControl(strSubjectsTaughtBuilder.ToString()))
                        End If
                    End Using 'objReader
                End Using 'objcommand
            End Using 'objConnection
        Catch ex As Exception
            lblStatus.Text = "Problem area - LoadSubjectsTaughtBy<br/>" + ex.Message + "<br/>Error source:" + ex.Source
        End Try
    End Sub

    'Private Sub LoadTeachers()
    '    Dim strConn As String = GetConnectStringFromWebConfig()
    '    Try
    '        Using objconn As New SqlConnection(strConn)
    '            'This gets Admin too so he/she has a change to return to their own profile display
    '            Dim strSQL As String = <![CDATA[
    '     SELECT StaffID, Firstname, Lastname FROM StaffMembers WHERE 
    '        Active>0
    ']]>.Value()
    '            Using objcommand As New SqlCommand
    '                With objcommand
    '                    .Connection = objconn
    '                    .Connection.Open()
    '                    .CommandText = strSQL
    '                End With
    '                Dim rblClasses As New RadioButtonList
    '                rblClasses.ID = "ListofTeachers"
    '                Dim liClass As ListItem
    '                Dim bTeacherFound As Boolean = False
    '                Using objReader As SqlDataReader = objcommand.ExecuteReader()
    '                    While objReader.Read()
    '                        bTeacherFound = True
    '                        liClass = New ListItem
    '                        With liClass
    '                            .Value = objReader("StaffID").ToString
    '                            .Text = objReader("Firstname").ToString + " " + objReader("Lastname").ToString()
    '                        End With
    '                        'AddHandler lnkClass.Click, AddressOf ListStudents
    '                        rblClasses.Items.Add(liClass)
    '                        lblStatus.Text = ""
    '                    End While
    '                    TeachersHolder.Controls.Clear()
    '                    If bTeacherFound = False Then
    '                        TeachersHolder.Controls.Add(New LiteralControl("No teachers are in the system currently"))
    '                    Else
    '                        TeachersHolder.Controls.Add(rblClasses)
    '                    End If
    '                End Using 'objReader
    '            End Using 'objcommand
    '        End Using 'objConnection
    '    Catch ex As Exception
    '        lblStatus.Text = "Problem area - LoadTeachers<br/>" + ex.Message
    '    End Try
    'End Sub


    'Protected Sub lnkLoadTeacherProfile_Click(sender As Object, e As EventArgs) Handles lnkLoadTeacherProfile.Click
    '    Dim ctrlControl As Control
    '    Dim iTeacherID As Integer = 0
    '    ctrlControl = ClassesHolder.FindControl("ListOfTeachers")
    '    If ctrlControl IsNot Nothing Then
    '        Dim rblList As RadioButtonList = CType(ctrlControl, RadioButtonList)
    '        iTeacherID = rblList.SelectedItem.Value
    '    Else
    '        iTeacherID = 0
    '    End If
    '    'If a teacher is not (or could not be) selected, then there is problem
    '    If iTeacherID = 0 Then
    '        lblStatus.Text = "Problem"
    '        Session("AdminPickedTeacher") = 0
    '    Else
    '        Session("AdminPickedTeacher") = iTeacherID
    '        LoadClassesTaughtBy(iTeacherID)
    '        LoadSubjectsTaughtby(iTeacherID)
    '        LoadStudentsTaughtBy(iTeacherID)
    '        LoadProfile(iTeacherID)
    '        lblTeachername.Text = GetTeacherName(iTeacherID)
    '        lblTeachername1.Text = lblTeachername.Text
    '        lblTeachername2.Text = lblTeachername.Text
    '        lblTeachername3.Text = lblTeachername.Text
    '    End If
    'End Sub
    'Private Function CheckAnswers() As Boolean
    '    Dim bOK As Boolean = False
    '    'bOK = CreateQuestion1.CheckQuestionAnswers()
    '    Return bOK
    'End Function
End Class