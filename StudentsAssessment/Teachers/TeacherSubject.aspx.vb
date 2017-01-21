Imports System.Data.SqlClient

Public Class TeacherSubject
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        SiteHeader1.Pagetitle = "TEACHER INTERFACE"
        SiteHeader1.PageDesc = "A place to add/remove subjects to/from class taught by teacher"
        TeachersMenu1.HiglightOption = TeachersMenu.OptiontoHighlight.Home
        'Make sure they are logged in.  Otherwise, forece them to log in
        If Session("LoggedinTeacherID") IsNot Nothing AndAlso IsNumeric(Session("LoggedinTeacherID")) Then
            If Session("LoggedinTeacherID") IsNot Nothing AndAlso IsNumeric(Session("LoggedinTeacherID")) Then
                If Session("IsAdmin") IsNot Nothing AndAlso Session("IsAdmin") = True Then
                    'ADMIN has logged in
                    'divAdminPanel.Visible = True
                    'divAdminPanelButton.Visible = True
                    divCurrentlyActiveSubject.Visible = False
                    'LoadTeachers()
                Else
                    ' divAdminPanel.Visible = False
                    ' divAdminPanelButton.Visible = False
                    divCurrentlyActiveSubject.Visible = True
                End If
                divNotLoggedIn1.Visible = False
                TeachersPanel1.Visible = True
                TeachersPanel1.Name = GetTeacherName(Convert.ToInt32(Session("LoggedinTeacherID")))
            Else
                TeachersPanel1.Visible = False
                divNotLoggedIn1.Visible = True
            End If



            If Not IsPostBack Then
                ddlSubjects.Items.Clear()
                LoadTeachersSubjectsandCurrentlyActive(Convert.ToInt32(Session("LoggedinTeacherID")))
                lblActiveSubject.Text = GetTeacherCurrentlyActiveSubject(Convert.ToInt32(Session("LoggedinTeacherID")))
                lblActiveSubject1.Text = lblActiveSubject.Text
                lblActiveSubject2.Text = lblActiveSubject.Text
                '' lblTeacherName.Text = GetTeacherName(Convert.ToInt32(Session("LoggedinTeacherID")))
                'lblTeachername1.Text = GetTeacherName(Convert.ToInt32(Session("LoggedinTeacherID")))
                'LoadClasses(Integer.Parse(Session("LoggedinTeacherID")))
                LoadTeachersClassesTeachingActiveSubject(Integer.Parse(Session("LoggedinTeacherID")))
                ' LoadSubjectsTaughtbyTeacher(Session("LoggedinTeacherID"))
                LoadTeachersClassesNOTTeachingActiveSubject(Integer.Parse(Session("LoggedinTeacherID")))
                ' LoadSubjects()
            Else
                'IS postback but
                'Admin has logged in and picked a teacher.
                If Session("AdminPickedTeacher") IsNot Nothing AndAlso IsNumeric(Session("AdminPickedTeacher")) Then
                    ' LoadClasses(Integer.Parse(Session("AdminPickedTeacher")))
                    LoadTeachersClassesTeachingActiveSubject(Integer.Parse(Session("AdminPickedTeacher")))
                    'LoadSubjectsTaughtbyTeacher(Session("AdminPickedTeacher"))
                    LoadTeachersClassesNOTTeachingActiveSubject(Integer.Parse(Session("AdminPickedTeacher")))
                    'lblTeacherName.Text = GetTeacherName(Convert.ToInt32(Session("AdminPickedTeacher")))
                    ' lblTeacherName.Text = GetTeacherName(Convert.ToInt32(Session("AdminPickedTeacher")))
                    ' lblTeachername1.Text = GetTeacherName(Convert.ToInt32(Session("AdminPickedTeacher")))
                Else 'Admin has not logged in....it is just a postback
                    ' LoadClasses(Integer.Parse(Session("AdminPickedTeacher")))
                    LoadTeachersClassesTeachingActiveSubject(Integer.Parse(Session("LoggedinTeacherID")))
                    'LoadSubjectsTaughtbyTeacher(Session("AdminPickedTeacher"))
                    LoadTeachersClassesNOTTeachingActiveSubject(Integer.Parse(Session("LoggedinTeacherID")))
                    ' lblTeacherName.Text = GetTeacherName(Convert.ToInt32(Session("LoggedinTeacherID")))
                    'lblTeachername1.Text = GetTeacherName(Convert.ToInt32(Session("LoggedinTeacherID")))
                End If
                'LoadSubjects()
            End If
        Else
                Response.Redirect("~/TeachersLogin.aspx")
        End If
    End Sub

    Private Sub LoadClasses(ByVal iTeacherID As Integer)
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
                    Using objReader As SqlDataReader = objcommand.ExecuteReader()
                        Dim rblClasses As New RadioButtonList
                        rblClasses.ID = "ListofClasses"
                        Dim liClass As ListItem
                        Dim bTeachingClasses As Boolean = False
                        Dim strDiv As String = ""
                        While objReader.Read()
                            liClass = New ListItem
                            bTeachingClasses = True
                            With liClass
                                .Value = objReader("ClassID").ToString
                                .Text = objReader("Classname").ToString
                            End With
                            'AddHandler lnkClass.Click, AddressOf ListStudents
                            rblClasses.Items.Add(liClass)
                            lblStatus.Text = ""
                        End While
                        ClassesHolderLeft.Controls.Clear()
                        If bTeachingClasses = False Then
                            ClassesHolderLeft.Controls.Add(New LiteralControl("This teacher currently does not teach any classes"))
                        Else
                            ClassesHolderLeft.Controls.Add(rblClasses)
                        End If
                    End Using 'objReader
                End Using 'objcommand
            End Using 'objConnection
        Catch ex As Exception
            lblStatus.Text = "Problem area - LoadClasses" + vbCrLf + ex.Message
        End Try
    End Sub

    'Private Sub LoadSubjectsTaughtbyTeacher(ByVal iTeacherID As Integer)
    '    Dim strConn As String = GetConnectStringFromWebConfig()
    '    Dim strSubjectsTaughtBuilder As New StringBuilder
    '    strSubjectsTaughtBuilder.Append("<ul>")
    '    Try
    '        Using objconn As New SqlConnection(strConn)
    '            Dim strSQL As String = <![CDATA[
    '    SELECT EntryID, Classname,Subjectname,[TeacherID] FROM [AssessmentDB].[dbo].[ClassesSubjects] 
    '        inner join Subjects on ClassesSubjects.subjectid=Subjects.subjectid 
    '        inner join classes on ClassesSubjects.ClassID=Classes.ClassID where teacherid=@TeacherID
    ']]>.Value()
    '            Using objcommand As New SqlCommand
    '                With objcommand
    '                    .Connection = objconn
    '                    .Connection.Open()
    '                    .CommandText = strSQL
    '                    .Parameters.AddWithValue("@TeacherID", iTeacherID)
    '                End With
    '                Dim bTeachingSubjects As Boolean = False
    '                Using objReader As SqlDataReader = objcommand.ExecuteReader()
    '                    While objReader.Read()
    '                        bTeachingSubjects = True
    '                        strSubjectsTaughtBuilder.Append("<li>")
    '                        strSubjectsTaughtBuilder.Append(objReader("Classname").ToString + " - " + objReader("Subjectname").ToString)
    '                        strSubjectsTaughtBuilder.Append("</li>")
    '                        lblStatus.Text = ""
    '                    End While
    '                    SubjectsHolder1.Controls.Clear()
    '                    If bTeachingSubjects = False Then
    '                        SubjectsHolder1.Controls.Add(New LiteralControl("This teacher does not teach any subjects already"))
    '                    Else
    '                        SubjectsHolder1.Controls.Add(New LiteralControl(strSubjectsTaughtBuilder.ToString()))
    '                    End If
    '                End Using 'objReader
    '            End Using 'objcommand
    '        End Using 'objConnection
    '        'Dim objConn As New SqlConnection(strConn)
    '        'objConn.Open()
    '        'Dim strSQL As String = "SELECT EntryID, Classname,Subjectname,[TeacherID] FROM [AssessmentDB].[dbo].[ClassesSubjects] 
    '        'inner join Subjects on ClassesSubjects.subjectid=Subjects.subjectid 
    '        'inner join classes on ClassesSubjects.ClassID=Classes.ClassID where teacherid=@TeacherID"
    '        'Dim objCommand As New SqlCommand(strSQL, objConn)
    '        ' objCommand.Parameters.AddWithValue("@TeacherID", iTeacherID)
    '        'Dim objReader As SqlDataReader = objCommand.ExecuteReader
    '        'Dim bTeachingSubjects As Boolean = False
    '        'Dim strDiv As String = ""
    '        'While objReader.Read()
    '        '    bTeachingSubjects = True
    '        '    strSubjectsTaughtBuilder.Append("<li>")
    '        '    strSubjectsTaughtBuilder.Append(objReader("Classname").ToString + " - " + objReader("Subjectname").ToString)
    '        '    strSubjectsTaughtBuilder.Append("</li>")
    '        '    lblStatus.Text = ""
    '        'End While
    '        'SubjectsHolder1.Controls.Clear()
    '        'If bTeachingSubjects = False Then
    '        '    SubjectsHolder1.Controls.Add(New LiteralControl("This teacher does not teach any subjects already"))
    '        'Else
    '        '    SubjectsHolder1.Controls.Add(New LiteralControl(strSubjectsTaughtBuilder.ToString()))
    '        'End If
    '        'objReader.Close()
    '        'objReader = Nothing
    '        'objCommand.Dispose()
    '        'objCommand = Nothing
    '        'objConn.Dispose()
    '        'objConn.Close()
    '        'objConn = Nothing
    '        'SqlConnection.ClearAllPools()
    '    Catch ex As Exception
    '        lblStatus.Text = "Problem with system.  Please let administrator know.  And please retry at a later time"
    '    End Try

    'End Sub

    Private Sub LoadSubjects()
        Dim strConn As String = GetConnectStringFromWebConfig()
        Try
            Using objconn As New SqlConnection(strConn)
                Dim strSQL As String = <![CDATA[
       SELECT SubjectID, Subjectname FROM Subjects WHERE Active>0
    ]]>.Value()
                Using objcommand As New SqlCommand
                    With objcommand
                        .Connection = objconn
                        .Connection.Open()
                        .CommandText = strSQL
                    End With
                    Using objReader As SqlDataReader = objcommand.ExecuteReader()
                        Dim rblClasses As New RadioButtonList
                        rblClasses.ID = "ListofSubjects"
                        Dim liClass As ListItem
                        Dim bTeachingClasses As Boolean = False
                        Dim strDiv As String = ""
                        While objReader.Read()
                            liClass = New ListItem
                            bTeachingClasses = True
                            With liClass
                                .Value = objReader("SubjectID").ToString
                                .Text = objReader("Subjectname").ToString
                            End With
                            'AddHandler lnkClass.Click, AddressOf ListStudents
                            rblClasses.Items.Add(liClass)
                            lblStatus.Text = ""
                        End While
                        ClassesHolderRight.Controls.Clear()
                        If bTeachingClasses = False Then
                            ClassesHolderRight.Controls.Add(New LiteralControl("No ACTIVE subjects are in the system currently"))
                        Else
                            ClassesHolderRight.Controls.Add(rblClasses)
                        End If
                    End Using
                End Using
            End Using
            'Dim objConn As New SqlConnection(strConn)
            'objConn.Open()
            'Dim strSQL As String = "SELECT SubjectID, Subjectname FROM Subjects WHERE Active>0"
            'Dim objCommand As New SqlCommand(strSQL, objConn)
            'Dim objReader As SqlDataReader = objCommand.ExecuteReader
            'Dim rblClasses As New RadioButtonList
            'rblClasses.ID = "ListofSubjects"
            'Dim liClass As ListItem
            'Dim bTeachingClasses As Boolean = False
            'Dim strDiv As String = ""
            'While objReader.Read()
            '    liClass = New ListItem
            '    bTeachingClasses = True
            '    With liClass
            '        .Value = objReader("SubjectID").ToString
            '        .Text = objReader("Subjectname").ToString
            '    End With
            '    'AddHandler lnkClass.Click, AddressOf ListStudents
            '    rblClasses.Items.Add(liClass)
            '    lblStatus.Text = ""
            'End While
            'SubjectsHolder.Controls.Clear()
            'If bTeachingClasses = False Then
            '    SubjectsHolder.Controls.Add(New LiteralControl("No ACTIVE subjects are in the system currently"))
            'Else
            '    SubjectsHolder.Controls.Add(rblClasses)
            'End If

            'objReader.Close()
            'objReader = Nothing
            'objCommand.Dispose()
            'objCommand = Nothing
            'objConn.Dispose()
            'objConn.Close()
            'objConn = Nothing
            'SqlConnection.ClearAllPools()
        Catch ex As Exception
            lblStatus.Text = "Problem area - LoadSubjects" + vbCrLf + ex.Message
        End Try

    End Sub

    Private Sub LoadTeachersSubjectsandCurrentlyActive(ByVal iTeacherID As Integer)
        Dim strConn As String = GetConnectStringFromWebConfig()
        Try
            Using objconn As New SqlConnection(strConn)
                'SELECT DISTINCT ClassesSubjects.SubjectID, Subjectname FROM ClassesSubjects 
                'INNER JOIN Classes ON ClassesSubjects.ClassID=Classes.ClassID
                'INNER JOIN Subjects ON ClassesSubjects.subjectid=Subjects.subjectid
                'AND ClassesSubjects.SubjectID <> (select CurrentSubjectID from StaffMembers where StaffMembers.StaffID=@TeacherID)
                'WHERE TeacherID=@TeacherID
                Dim strSQL As String = <![CDATA[
                     SELECT TeachersSubjects.SubjectID, Subjectname FROM TeachersSubjects 
        INNER JOIN Subjects ON TeachersSubjects.SubjectID=Subjects.SubjectID WHERE TeachersSubjects.TeacherID=@TeacherID
        AND TeachersSubjects.SubjectID <> (select CurrentSubjectID FROM StaffMembers WHERE StaffMembers.StaffID=@TeacherID)
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
                        Dim rblSubjects As New RadioButtonList
                        rblSubjects.ID = "ListofSubjects"
                        Dim liSubject As ListItem

                        Dim strDiv As String = ""
                        While objReader.Read()
                            bTeachingSubjects = True
                            liSubject = New ListItem
                            With liSubject
                                .Value = objReader("SubjectID").ToString
                                .Text = objReader("Subjectname").ToString
                            End With
                            'AddHandler lnkClass.Click, AddressOf ListStudents
                            ddlSubjects.Items.Add(liSubject)
                            lblStatus.Text = ""
                        End While
                        If bTeachingSubjects = False Then
                            ' lblStatus.Text = "This teacher currently does not teach any subjects"
                            divActivateSubject.Visible = False
                            divOnlysubject.Visible = True
                        Else
                            'ClassesHolder.Controls.Add(rblSubjects)
                            divActivateSubject.Visible = True
                            divOnlysubject.Visible = False
                        End If
                    End Using 'objReader
                    'ClassesHolder.Controls.Clear()

                End Using 'objcommand   

            End Using 'objConnection

        Catch ex As Exception
            lblStatus.Text = "Problem area - LoadTeachersSubjectsandCurrentlyActive" + vbCrLf + ex.Message
        End Try
    End Sub

    'Private Sub LoadTeachers()
    '    Dim strConn As String = GetConnectStringFromWebConfig()
    '    Try
    '        Using objconn As New SqlConnection(strConn)
    '            Dim strSQL As String = <![CDATA[
    '   SELECT StaffID, Firstname, Lastname FROM StaffMembers WHERE 
    '        Active>0 and ISAdmin=0
    ']]>.Value()
    '            Using objcommand As New SqlCommand
    '                With objcommand
    '                    .Connection = objconn
    '                    .Connection.Open()
    '                    .CommandText = strSQL
    '                End With
    '                Using objReader As SqlDataReader = objcommand.ExecuteReader()
    '                    Dim rblClasses As New RadioButtonList
    '                    rblClasses.ID = "ListofTeachers"
    '                    Dim liClass As ListItem
    '                    Dim strDiv As String = ""
    '                    Dim bTeacherFound As Boolean = False
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
    '                        TeachersHolder.Controls.Add(New LiteralControl("No ACTIVE teachers are in the system currently"))
    '                    Else
    '                        TeachersHolder.Controls.Add(rblClasses)
    '                    End If
    '                End Using
    '            End Using
    '        End Using
    '        'Dim objConn As New SqlConnection(strConn)
    '        'objConn.Open()
    '        'Dim strSQL As String = "SELECT StaffID, Firstname, Lastname FROM StaffMembers WHERE 
    '        'Active>0 and ISAdmin=0"
    '        'Dim objCommand As New SqlCommand(strSQL, objConn)
    '        'Dim objReader As SqlDataReader = objCommand.ExecuteReader
    '        'Dim rblClasses As New RadioButtonList
    '        'rblClasses.ID = "ListofTeachers"
    '        'Dim liClass As ListItem
    '        'Dim strDiv As String = ""
    '        'Dim bTeacherFound As Boolean = False
    '        'While objReader.Read()
    '        '    bTeacherFound = True
    '        '    liClass = New ListItem
    '        '    With liClass
    '        '        .Value = objReader("StaffID").ToString
    '        '        .Text = objReader("Firstname").ToString + " " + objReader("Lastname").ToString()
    '        '    End With
    '        '    'AddHandler lnkClass.Click, AddressOf ListStudents
    '        '    rblClasses.Items.Add(liClass)
    '        '    lblStatus.Text = ""
    '        'End While
    '        'TeachersHolder.Controls.Clear()
    '        'If bTeacherFound = False Then
    '        '    TeachersHolder.Controls.Add(New LiteralControl("No ACTIVE teachers are in the system currently"))
    '        'Else
    '        '    TeachersHolder.Controls.Add(rblClasses)
    '        'End If

    '        'objReader.Close()
    '        'objReader = Nothing
    '        'objCommand.Dispose()
    '        'objCommand = Nothing
    '        'objConn.Dispose()
    '        'objConn.Close()
    '        'objConn = Nothing
    '        'SqlConnection.ClearAllPools()
    '    Catch ex As Exception
    '        lblStatus.Text = "Problem area - LoadTeachers" + vbCrLf + ex.Message
    '    End Try
    'End Sub


    Protected Sub lnkReturn_Click(sender As Object, e As EventArgs) Handles lnkReturn.Click
        Response.Redirect("~/default.aspx")
    End Sub

    Protected Sub lnkAddSubjects_Click(sender As Object, e As EventArgs) Handles lnkAddSubjects.Click
        Dim ctrlControl As Control
        Dim iClassID As Integer = 0
        Dim iSubjectID As Integer = 0
        ctrlControl = ClassesHolderLeft.FindControl("ListofClassesNOTIn")
        Try
            If ctrlControl IsNot Nothing Then
                Dim rblList As RadioButtonList = CType(ctrlControl, RadioButtonList)
                If rblList.SelectedItem IsNot Nothing Then
                    iClassID = rblList.SelectedItem.Value
                    'ctrlControl = SubjectsHolder.FindControl("ListOfSubjects")
                    'If ctrlControl IsNot Nothing Then
                    '    rblList = CType(ctrlControl, RadioButtonList)
                    '    If rblList.SelectedItem IsNot Nothing Then
                    '        iSubjectID = rblList.SelectedItem.Value
                    '    Else
                    '        lblStatus.Text = "Please select the subject to be added the class"
                    '        Exit Sub
                    '    End If

                    'Else
                    '    iSubjectID = 
                    'End If
                Else
                    lblStatus.Text = "From the list on the right, please select which of your other classes you want to add to list on left"
                    Exit Sub
                End If

            Else
                iClassID = 0
            End If

            If iClassID = 0 Then
                lblStatus.Text = String.Format("This teacher already teaches {0} in all his/her classes.  No new class available to add.", GetSubjectname(Session("Activesubject")))
            Else
                'If Not SubjectTaughtinClass(iClassID, iSubjectID) Then
                If Session("IsAdmin") IsNot Nothing AndAlso Session("IsAdmin") = True Then
                    'ADMIN has logged in
                    StartTeachingActiveSubjectInClass(iClassID, GetActiveSubjectFor(Integer.Parse(Session("AdminPickedTeacher"))), Integer.Parse(Session("AdminPickedTeacher")))
                    'LoadSubjectsTaughtbyTeacher(Integer.Parse(Session("AdminPickedTeacher")))
                    LoadTeachersClassesTeachingActiveSubject(Integer.Parse(Session("AdminPickedTeacher")))
                    ' LoadSubjectsTaughtbyTeacher(Session("LoggedinTeacherID"))
                    LoadTeachersClassesNOTTeachingActiveSubject(Integer.Parse(Session("AdminPickedTeacher")))
                Else
                    StartTeachingActiveSubjectInClass(iClassID, Integer.Parse(Session("Activesubject")), Integer.Parse(Session("LoggedinTeacherID")))
                    'LoadSubjectsTaughtbyTeacher(Integer.Parse(Session("LoggedinTeacherID"))) 'Refresh that list
                    'LoadSubjectsTaughtbyTeacher(Integer.Parse(Session("AdminPickedTeacher")))
                    LoadTeachersClassesTeachingActiveSubject(Integer.Parse(Session("LoggedinTeacherID")))
                    ' LoadSubjectsTaughtbyTeacher(Session("LoggedinTeacherID"))
                    LoadTeachersClassesNOTTeachingActiveSubject(Integer.Parse(Session("LoggedinTeacherID")))
                End If
                'Else
                '    lblStatus.Text = "Selected Subject already taught in selected class"
                'End If

            End If
        Catch ex As Exception
            lblStatus.Text = "Problem area - lnkAddSubjects_Click" + vbCrLf + ex.Message
        End Try

    End Sub

    Private Sub StartTeachingActiveSubjectInClass(ByVal iClassID As Integer, iSubjectID As Integer, iTeacherID As Integer)
        Dim strConn As String = GetConnectStringFromWebConfig()
        ' Dim objConn As New SqlConnection(strConn)
        Dim bValid As Boolean = False
        Try
            Using objconn As New SqlConnection(strConn)
                Dim strSQL As String = <![CDATA[
       INSERT INTO ClassesSubjects (ClassID,SubjectID,TeacherID) VALUES(
            @ClassID,@SubjectID,@TeacherID)
    ]]>.Value()
                Using objcommand As New SqlCommand
                    With objcommand
                        .Connection = objconn
                        .Connection.Open()
                        .CommandText = strSQL
                        .Parameters.AddWithValue("@ClassID", iClassID)
                        .Parameters.AddWithValue("@SubjectID", iSubjectID)
                        .Parameters.AddWithValue("@TeacherID", iTeacherID)
                    End With
                    If objcommand.ExecuteNonQuery() <= 0 Then
                        lblStatus.Text = "Problem adding subject to class"
                    Else
                        lblStatus.Text = String.Format("{0} will be taught in {1} in future", GetSubjectname(iSubjectID), GetClassname(iClassID))
                    End If
                End Using 'objCommand
            End Using 'objconnection
            'objConn.Open()
            'Dim strSQL As String = "INSERT INTO ClassesSubjects (ClassID,SubjectID,TeacherID) VALUES(
            '@ClassID,@SubjectID,@TeacherID)"
            'Dim objCommand As New SqlCommand(strSQL, objConn)
            'objCommand.Parameters.AddWithValue("@ClassID", iClassID)
            'objCommand.Parameters.AddWithValue("@SubjectID", iSubjectID)
            'objCommand.Parameters.AddWithValue("@TeacherID", iTeacherID)
            'If objCommand.ExecuteNonQuery() <= 0 Then
            '    lblStatus.Text = "Problem adding subject to class"
            'Else
            '    lblStatus.Text = "Successfully added subject to class.  Use RETURN button to go back to HOME - MAIN SCREEN"
            'End If
            'objCommand.Dispose()
            'objCommand = Nothing
            'objConn.Dispose()
            'objConn.Close()
            'objConn = Nothing
            'SqlConnection.ClearAllPools()
        Catch ex As Exception
            lblStatus.Text = "Problem area - StartTeachingActiveSubjectInClass" + vbCrLf + ex.Message
        End Try
    End Sub

    Private Sub StopTeachingActiveSubjectInClass(ByVal iClassID As Integer, iSubjectID As Integer)
        Dim strConn As String = GetConnectStringFromWebConfig()
        ' Dim objConn As New SqlConnection(strConn)
        Dim bValid As Boolean = False
        Try
            Using objconn As New SqlConnection(strConn)
                Dim strSQL As String = <![CDATA[
    DELETE FROM ClassesSubjects WHERE ClassID=@ClassID AND
            SubjectID=@SubjectID
    ]]>.Value()
                Using objcommand As New SqlCommand
                    With objcommand
                        .Connection = objconn
                        .Connection.Open()
                        .CommandText = strSQL
                        .Parameters.AddWithValue("@ClassID", iClassID)
                        .Parameters.AddWithValue("@SubjectID", iSubjectID)
                    End With
                    If objcommand.ExecuteNonQuery() <= 0 Then
                        lblStatus.Text = "Problem removing subject from class"
                    Else
                        lblStatus.Text = String.Format("{0} will no longer be taught in {1}", GetSubjectname(iSubjectID), GetClassname(iClassID))
                    End If
                End Using 'objCommand
            End Using 'objconnection
            'objConn.Open()
            'Dim strSQL As String = "DELETE FROM ClassesSubjects WHERE ClassID=@ClassID AND
            'SubjectID=@SubjectID"
            'Dim objCommand As New SqlCommand(strSQL, objConn)
            'objCommand.Parameters.AddWithValue("@ClassID", iClassID)
            'objCommand.Parameters.AddWithValue("@SubjectID", iSubjectID)
            'If objCommand.ExecuteNonQuery() <= 0 Then
            '    lblStatus.Text = "Problem removing subject from class"
            'Else
            '    lblStatus.Text = "Successfully removed subject from class.  Use RETURN button to go back to HOME - MAIN SCREEN"
            'End If
            'objCommand.Dispose()
            'objCommand = Nothing
            'objConn.Dispose()
            'objConn.Close()
            'objConn = Nothing
            'SqlConnection.ClearAllPools()
        Catch ex As Exception
            lblStatus.Text = "Problem area - StopTeachingActiveSubjectInClass" + vbCrLf + ex.Message
        End Try
    End Sub

    'Private Function SubjectTaughtinClass(ByVal iClassID As Integer, iSubjectID As Integer) As Boolean
    '    Dim bInClass As Boolean = False
    '    Dim iCnt As Integer = 0
    '    Dim strConn As String = GetConnectStringFromWebConfig()
    '    'Dim objConn As New SqlConnection(strConn)
    '    Dim bValid As Boolean = False
    '    Try
    '        Using objconn As New SqlConnection(strConn)
    '            Dim strSQL As String = <![CDATA[
    'SELECT COUNT(*) FROM ClassesSubjects WHERE ClassID=@ClassID AND
    '        SubjectID=@SubjectID
    ']]>.Value()
    '            Using objcommand As New SqlCommand
    '                With objcommand
    '                    .Connection = objconn
    '                    .Connection.Open()
    '                    .CommandText = strSQL
    '                    objcommand.Parameters.AddWithValue("@ClassID", iClassID)
    '                    objcommand.Parameters.AddWithValue("@SubjectID", iSubjectID)
    '                End With

    '                iCnt = objcommand.ExecuteScalar()
    '                bInClass = iCnt > 0

    '            End Using 'objcommand
    '        End Using 'objConnection
    '        'objConn.Open()
    '        'Dim strSQL As String = "SELECT COUNT(*) FROM ClassesSubjects WHERE ClassID=@ClassID AND
    '        'SubjectID=@SubjectID"
    '        'Dim objCommand As New SqlCommand(strSQL, objConn)
    '        'objCommand.Parameters.AddWithValue("@ClassID", iClassID)
    '        ''objCommand.Parameters.AddWithValue("@SubjectID", iSubjectID)
    '        'iCnt = objCommand.ExecuteScalar()
    '        'bInClass = iCnt > 0
    '        'objCommand.Dispose()
    '        'objCommand = Nothing
    '        'objConn.Dispose()
    '        'objConn.Close()
    '        'objConn = Nothing
    '        'SqlConnection.ClearAllPools()
    '    Catch ex As Exception
    '        lblStatus.Text = "Problem with system.  Please let administrator know.  And please retry at a later time."
    '        bInClass = False
    '    End Try
    '    Return bInClass
    'End Function

    Protected Sub lnkRemoveSubjects_Click(sender As Object, e As EventArgs) Handles lnkRemoveSubject.Click
        Dim ctrlControl As Control
        Dim iClassID As Integer = 0
        Dim iSubjectID As Integer = 0
        ctrlControl = ClassesHolderLeft.FindControl("ListofClassesIn")
        Try
            If ctrlControl IsNot Nothing Then
                Dim rblList As RadioButtonList = CType(ctrlControl, RadioButtonList)
                If rblList.SelectedItem IsNot Nothing Then
                    iClassID = rblList.SelectedItem.Value
                    'ctrlControl = SubjectsHolder.FindControl("ListOfSubjects")
                    'If ctrlControl IsNot Nothing Then
                    '    rblList = CType(ctrlControl, RadioButtonList)
                    '    If rblList.SelectedItem IsNot Nothing Then
                    '        iSubjectID = rblList.SelectedItem.Value
                    '    Else
                    '        lblStatus.Text = "Please select the subject you want to remove from this class"
                    '        Exit Sub
                    '    End If
                    'Else
                    '    iSubjectID = 0
                    'End If
                Else
                    lblStatus.Text = "From the list on the left, please select which of your other classes you want to add to list on right"
                    Exit Sub
                End If
            Else
                iClassID = 0
            End If
            'If either selected class or selected student could not be determined
            If iClassID = 0 Then
                lblStatus.Text = String.Format("This teacher does not teach {0} in any of his/her classes.  NONE available to remove.", GetSubjectname(Session("Activesubject")))
            Else
                ' If SubjectTaughtinClass(iClassID, iSubjectID) Then
                StopTeachingActiveSubjectInClass(iClassID, Integer.Parse(Session("Activesubject")))
                If Session("IsAdmin") IsNot Nothing AndAlso Session("IsAdmin") = True Then
                        'ADMIN has logged in
                        'LoadSubjectsTaughtbyTeacher(Integer.Parse(Session("AdminPickedTeacher"))) 'Refresh that list
                        ' LoadClasses(Integer.Parse(Session("AdminPickedTeacher")))
                        LoadTeachersClassesTeachingActiveSubject(Integer.Parse(Session("AdminPickedTeacher")))
                        'LoadSubjectsTaughtbyTeacher(Session("AdminPickedTeacher"))
                        LoadTeachersClassesNOTTeachingActiveSubject(Integer.Parse(Session("AdminPickedTeacher")))
                    Else
                        LoadTeachersClassesTeachingActiveSubject(Integer.Parse(Session("LoggedinTeacherID")))
                        'LoadSubjectsTaughtbyTeacher(Session("AdminPickedTeacher"))
                        LoadTeachersClassesNOTTeachingActiveSubject(Integer.Parse(Session("LoggedinTeacherID")))
                    End If
                    'Else
                    '    lblStatus.Text = "Selected subject NOT taught in Selected class"
                End If
            'End If
        Catch ex As Exception
            lblStatus.Text = "Problem area - lnkRemoveSubjects_Click" + vbCrLf + ex.Message
        End Try

    End Sub

    'Protected Sub lnkLoadTeachers_Click(sender As Object, e As EventArgs) Handles lnkLoadTeachers.Click
    '    Dim ctrlControl As Control
    '    Dim iTeacherID As Integer = 0
    '    ctrlControl = ClassesHolderLeft.FindControl("ListOfTeachers")
    '    If ctrlControl IsNot Nothing Then
    '        Dim rblList As RadioButtonList = CType(ctrlControl, RadioButtonList)
    '        iTeacherID = rblList.SelectedItem.Value
    '    Else
    '        iTeacherID = 0
    '    End If
    '    'If a teacher is not (or could not be) selected, then there is problem
    '    If iTeacherID = 0 Then
    '        lblStatus.Text = "Problem area - lnkLoadTeachers_Click"
    '        Session("AdminPickedTeacher") = 0
    '    Else
    '        Session("AdminPickedTeacher") = iTeacherID
    '        LoadClasses(iTeacherID)
    '        lblTeacherName.Text = GetTeacherName(iTeacherID)
    '        'lblTeachername1.Text = GetTeacherName(iTeacherID)
    '        'LoadSubjectsTaughtbyTeacher(iTeacherID)
    '    End If
    'End Sub

    Private Sub lnkActivateSubject_Click(sender As Object, e As EventArgs) Handles lnkActivateSubject.Click
        ActivateSubject(Integer.Parse(ddlSubjects.SelectedValue()), Integer.Parse(Session("LoggedinTeacherID")))
        Session("Activesubject") = Integer.Parse(ddlSubjects.SelectedValue())
        ddlSubjects.Items.Clear()
        LoadTeachersClassesTeachingActiveSubject(Integer.Parse(Session("LoggedinTeacherID")))
        'LoadSubjectsTaughtbyTeacher(Session("AdminPickedTeacher"))
        LoadTeachersClassesNOTTeachingActiveSubject(Integer.Parse(Session("LoggedinTeacherID")))
        LoadTeachersSubjectsandCurrentlyActive(Integer.Parse(Session("LoggedinTeacherID")))
        lblActiveSubject.Text = GetTeacherCurrentlyActiveSubject(Convert.ToInt32(Session("LoggedinTeacherID")))
        lblActiveSubject1.Text = lblActiveSubject.Text
        lblActiveSubject2.Text = lblActiveSubject.Text
    End Sub

    Private Sub ActivateSubject(ByVal iSubjectID As Integer, ByVal iTeacherID As Integer)
        Dim strConn As String = GetConnectStringFromWebConfig()
        'Dim objConn As New SqlConnection(strConn)
        Dim bValid As Boolean = False
        Try
            Using objconn As New SqlConnection(strConn)
                Dim strSQL As String = <![CDATA[
                UPDATE StaffMembers SET CurrentSubjectID=@CurrentSubjectID WHERE StaffID=@TeacherID
    ]]>.Value()
                Using objcommand As New SqlCommand
                    With objcommand
                        .Connection = objconn
                        .Connection.Open()
                        .CommandText = strSQL
                        .Parameters.AddWithValue("@CurrentSubjectID", iSubjectID)
                        .Parameters.AddWithValue("@TeacherID", iTeacherID)
                    End With
                    If objcommand.ExecuteNonQuery() <= 0 Then
                        lblStatus.Text = "Problem activating the subject"
                    Else
                        lblStatus.Text = "Successfully activated the subject"
                    End If
                End Using 'objCommand
            End Using 'objconnection
        Catch ex As Exception
            lblStatus.Text = "Problem area - ActivateSubject" + vbCrLf + ex.Message
        End Try
    End Sub

    Private Sub LoadTeachersClassesTeachingActiveSubject(ByVal iTeacherID As Integer)
        Dim strConn As String = GetConnectStringFromWebConfig()
        Try
            Using objconn As New SqlConnection(strConn)
                Dim strSQL As String = <![CDATA[
             select ClassesSubjects.ClassID,Classname,subjectname from classessubjects 
inner join Classes on ClassesSubjects.ClassID=Classes.ClassID
inner join Subjects on ClassesSubjects.SubjectID=Subjects.SubjectID
where TeacherID=@TeacherID and ClassesSubjects.SubjectID=@SubjectID
    ]]>.Value()
                Using objcommand As New SqlCommand
                    With objcommand
                        .Connection = objconn
                        .Connection.Open()
                        .CommandText = strSQL
                        .Parameters.AddWithValue("@TeacherID", iTeacherID)
                        .Parameters.AddWithValue("@SubjectID", Integer.Parse(Session("Activesubject")))
                    End With
                    Using objReader As SqlDataReader = objcommand.ExecuteReader()
                        Dim rblStudents As New RadioButtonList
                        rblStudents.ID = "ListofClassesIn"
                        Dim liClass As ListItem
                        Dim bTeachingClasses As Boolean = False
                        Dim strDiv As String = ""
                        While objReader.Read()
                            liClass = New ListItem
                            bTeachingClasses = True
                            With liClass
                                .Value = objReader("ClassID").ToString
                                .Text = objReader("Classname").ToString
                            End With
                            'AddHandler lnkClass.Click, AddressOf ListStudents
                            rblStudents.Items.Add(liClass)
                        End While
                        ClassesHolderLeft.Controls.Clear()
                        If bTeachingClasses = False Then
                            ClassesHolderLeft.Controls.Add(New LiteralControl("This teacher has no classes yet teaching the active subject"))
                        Else
                            ClassesHolderLeft.Controls.Add(rblStudents)
                        End If
                    End Using 'objReader
                End Using 'objcommand
            End Using 'objConnection
        Catch ex As Exception
            lblStatus.Text = "Problem area - LoadTeachersClassesTeachingActiveSubject<br/>" + ex.Message
        End Try
    End Sub

    Private Sub LoadTeachersClassesNOTTeachingActiveSubject(ByVal iTeacherID As Integer)
        Dim strConn As String = GetConnectStringFromWebConfig()
        Try
            Using objconn As New SqlConnection(strConn)
                Dim strSQL As String = <![CDATA[
        select ClassID,classname from Classes 
where ClassID  not in (select ClassID from ClassesSubjects where SubjectID=@SubjectID and TeacherID=@TeacherID)
and ClassID in (select ClassID from TeachersClasses where TeacherID=@TeacherID)
    ]]>.Value()
                Using objcommand As New SqlCommand
                    With objcommand
                        .Connection = objconn
                        .Connection.Open()
                        .CommandText = strSQL
                        .Parameters.AddWithValue("@TeacherID", iTeacherID)
                        .Parameters.AddWithValue("@SubjectID", Integer.Parse(Session("Activesubject")))
                    End With
                    Using objReader As SqlDataReader = objcommand.ExecuteReader()
                        Dim rblStudents As New RadioButtonList
                        rblStudents.ID = "ListofClassesNOTIn"
                        Dim liClass As ListItem
                        Dim bTeachingClasses As Boolean = False
                        Dim strDiv As String = ""
                        While objReader.Read()
                            liClass = New ListItem
                            bTeachingClasses = True
                            With liClass
                                .Value = objReader("ClassID").ToString
                                .Text = objReader("Classname").ToString
                            End With
                            'AddHandler lnkClass.Click, AddressOf ListStudents
                            rblStudents.Items.Add(liClass)
                        End While
                        ClassesHolderRight.Controls.Clear()
                        If bTeachingClasses = False Then
                            ClassesHolderRight.Controls.Add(New LiteralControl("All of this teacher's classes already are teaching the active subject"))
                            'lnkAddSubjects.Enabled = False
                        Else
                            ClassesHolderRight.Controls.Add(rblStudents)
                        End If
                    End Using 'objReader
                End Using 'objcommand
            End Using 'objConnection
        Catch ex As Exception
            lblStatus.Text = "Problem area - LoadTeachersClassesNOTTeachingActiveSubject" + vbCrLf + ex.Message
        End Try
    End Sub

End Class