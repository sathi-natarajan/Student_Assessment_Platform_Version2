Imports System.Data.SqlClient

Public Class TeacherClass
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        SiteHeader1.Pagetitle = "TEACHER INTERFACE"
        SiteHeader1.PageDesc = "A place to add/remove students to/from class taught by teacher"
        TeachersMenu1.HiglightOption = TeachersMenu.OptiontoHighlight.Home
        'Make sure they are logged in.  Otherwise, forece them to log in
        If Session("LoggedinTeacherID") IsNot Nothing AndAlso IsNumeric(Session("LoggedinTeacherID")) Then
            If Session("LoggedinTeacherID") IsNot Nothing AndAlso IsNumeric(Session("LoggedinTeacherID")) Then
                If Session("IsAdmin") IsNot Nothing AndAlso Session("IsAdmin") = True Then
                    'ADMIN has logged in
                    ' divAdminPanel.Visible = True
                    'LoadTeachers()
                    divCurrentlyActiveClass.Visible = False
                Else
                    'divAdminPanel.Visible = False
                    divCurrentlyActiveClass.Visible = True
                End If
                divNotLoggedIn1.Visible = False
                TeachersPanel1.Visible = True
                TeachersPanel1.Name = GetTeacherName(Convert.ToInt32(Session("LoggedinTeacherID")))
            Else
                TeachersPanel1.Visible = False
                divNotLoggedIn1.Visible = True
            End If
            If Not IsPostBack Then
                'LoadClasses(Integer.Parse(Session("LoggedinTeacherID")))
                LoadStudentsOfTeacherAlreadyinActiveClass(Session("LoggedinTeacherID"))
                'LoadStudentsTaughtbyTeacher(Session("LoggedinTeacherID"))
                'LoadStudentsNOTAlreadyinActiveClass(Session("LoggedinTeacherID"))
                ' lblTeacherName.Text = GetTeacherName(Convert.ToInt32(Session("LoggedinTeacherID")))
                'lblTeacherName1.Text = lblTeacherName.Text
                'lblTeachername2.Text = GetTeacherName(Convert.ToInt32(Session("LoggedinTeacherID")))
                ddlClasses.Items.Clear()
                LoadTeachersClassesandCurrentlyActive(Convert.ToInt32(Session("LoggedinTeacherID")))
                lblActiveClass.Text = GetTeacherCurrentlyActiveClass(Convert.ToInt32(Session("LoggedinTeacherID")))
                lblActiveClass1.Text = lblActiveClass.Text
                lblActiveClass2.Text = lblActiveClass.Text
            Else
                'Admin has logged in and picked a teacher.
                If Session("AdminPickedTeacher") IsNot Nothing AndAlso IsNumeric(Session("AdminPickedTeacher")) Then
                    'LoadClasses(Integer.Parse(Session("AdminPickedTeacher")))
                    LoadStudentsOfTeacherAlreadyinActiveClass(Session("AdminPickedTeacher"))
                    ' LoadStudentsTaughtbyTeacher(Session("AdminPickedTeacher"))
                    'LoadStudentsNOTAlreadyinActiveClass(Session("AdminPickedTeacher"))
                    'lblTeacherName.Text = GetTeacherName(Convert.ToInt32(Session("AdminPickedTeacher")))
                    'lblTeachername2.Text = GetTeacherName(Convert.ToInt32(Session("AdminPickedTeacher")))
                Else
                    ' LoadClasses(Integer.Parse(Session("LoggedinTeacherID")))
                    LoadStudentsOfTeacherAlreadyinActiveClass(Session("LoggedinTeacherID"))
                    'LoadStudentsTaughtbyTeacher(Session("LoggedinTeacherID"))
                    LoadStudentsNOTAlreadyinActiveClass(Session("LoggedinTeacherID"))
                    ' lblTeacherName.Text = GetTeacherName(Convert.ToInt32(Session("LoggedinTeacherID")))
                    'lblTeachername2.Text = GetTeacherName(Convert.ToInt32(Session("LoggedinTeacherID")))

                End If
            End If
            'LoadStudents()
            LoadStudentsNOTAlreadyinActiveClass(Convert.ToInt32(Session("LoggedinTeacherID")))

        Else
                Response.Redirect("~/TeachersLogin.aspx")
        End If
    End Sub

    Private Sub LoadTeachersClassesandCurrentlyActive(ByVal iTeacherID As Integer)
        Dim strConn As String = GetConnectStringFromWebConfig()
        Try
            Using objconn As New SqlConnection(strConn)
                Dim strSQL As String = <![CDATA[
                            SELECT TeachersClasses.ClassID, Classname FROM TeachersClasses 
        INNER JOIN Classes ON TeachersClasses.ClassID=Classes.ClassID WHERE TeachersClasses.TeacherID=@TeacherID
        AND TeachersClasses.ClassID <> (select CurrentClassId FROM StaffMembers WHERE StaffMembers.StaffID=@TeacherID)
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
                        Dim rblClasses As New RadioButtonList
                        rblClasses.ID = "ListofClasses"
                        Dim liClass As ListItem
                        Dim strDiv As String = ""
                        While objReader.Read()
                            liClass = New ListItem
                            bTeachingClasses = True
                            With liClass
                                .Value = objReader("ClassID").ToString
                                .Text = objReader("Classname").ToString
                            End With
                            'AddHandler lnkClass.Click, AddressOf ListStudents
                            ddlClasses.Items.Add(liClass)
                            lblStatus.Text = ""
                        End While
                    End Using 'objReader
                    ' ClassesHolder.Controls.Clear()
                    If bTeachingClasses = False Then
                        'lblStatus.Text = "This teacher currently does not teach any classes"
                        divActivateClass.Visible = False
                        divOnlyclass.Visible = True
                    Else
                        '  ClassesHolder.Controls.Add(rblClasses)
                        divActivateClass.Visible = True
                        divOnlyclass.Visible = False
                    End If
                End Using 'objcommand
            End Using 'objConnection
        Catch ex As Exception
            lblStatus.Text = "Problem area - LoadTeachersClassesandCurrentlyActive<br/>" + ex.Message
        End Try
    End Sub

    'Private Sub LoadClasses(ByVal iTeacherID As Integer)
    '    Dim strConn As String = GetConnectStringFromWebConfig()
    '    Try
    '        Using objconn As New SqlConnection(strConn)
    '            Dim strSQL As String = <![CDATA[
    '    SELECT [EntryID],TeachersClasses.ClassID,[Classes].Classname FROM [AssessmentDB].[dbo].[TeachersClasses] 
    '        INNER JOIN Classes on TeachersClasses.ClassID=Classes.ClassID WHERE TeachersClasses.TeacherID=@TeacherID
    ']]>.Value()
    '            Using objcommand As New SqlCommand
    '                With objcommand
    '                    .Connection = objconn
    '                    .Connection.Open()
    '                    .CommandText = strSQL
    '                    .Parameters.AddWithValue("@TeacherID", iTeacherID)
    '                End With
    '                Using objReader As SqlDataReader = objcommand.ExecuteReader()
    '                    Dim rblClasses As New RadioButtonList
    '                    rblClasses.ID = "ListofClasses"
    '                    Dim liClass As ListItem
    '                    Dim bTeachingClasses As Boolean = False
    '                    Dim strDiv As String = ""
    '                    While objReader.Read()
    '                        liClass = New ListItem
    '                        bTeachingClasses = True
    '                        With liClass
    '                            .Value = objReader("ClassID").ToString
    '                            .Text = objReader("Classname").ToString
    '                        End With
    '                        'AddHandler lnkClass.Click, AddressOf ListStudents
    '                        rblClasses.Items.Add(liClass)
    '                        lblStatus.Text = ""
    '                    End While
    '                    ClassesHolder.Controls.Clear()
    '                    If bTeachingClasses = False Then
    '                        ClassesHolder.Controls.Add(New LiteralControl("This teacher currently does not teach any classes"))
    '                    Else
    '                        ClassesHolder.Controls.Add(rblClasses)
    '                    End If
    '                End Using 'objReader
    '            End Using 'objcommand
    '        End Using 'objConnection
    '    Catch ex As Exception
    '        lblStatus.Text = "Problem area - LoadClasses<br/>" + ex.Message
    '    End Try
    'End Sub

    Private Sub LoadStudentsOfTeacherAlreadyinActiveClass(ByVal iTeacherID As Integer)
        Dim strConn As String = GetConnectStringFromWebConfig()
        Try
            Using objconn As New SqlConnection(strConn)
                Dim strSQL As String = <![CDATA[
       SELECT ClassesStudents.StudentID,Firstname, Lastname FROM ClassesStudents 
INNER JOIN Classes ON ClassesStudents.ClassID=Classes.ClassID
INNER JOIN Students ON ClassesStudents.StudentID=Students.StudentID 
WHERE TeacherID=@TeacherID AND ClassesStudents.ClassID=@ClassID
ORDER BY Firstname, lastname
    ]]>.Value()
                Using objcommand As New SqlCommand
                    With objcommand
                        .Connection = objconn
                        .Connection.Open()
                        .CommandText = strSQL
                        .Parameters.AddWithValue("@TeacherID", iTeacherID)
                        .Parameters.AddWithValue("@ClassID", GetActiveClassFor(Integer.Parse(Session("LoggedinTeacherID"))))
                    End With
                    Using objReader As SqlDataReader = objcommand.ExecuteReader()
                        Dim rblStudents As New RadioButtonList
                        rblStudents.ID = "ListofStudentsIn"
                        Dim liClass As ListItem
                        Dim bTeachingClasses As Boolean = False
                        Dim strDiv As String = ""
                        While objReader.Read()
                            liClass = New ListItem
                            bTeachingClasses = True
                            With liClass
                                .Value = objReader("StudentID").ToString
                                .Text = objReader("Firstname").ToString + " " + objReader("Lastname").ToString()
                            End With
                            'AddHandler lnkClass.Click, AddressOf ListStudents
                            rblStudents.Items.Add(liClass)
                        End While
                        StudentsAlreadyInHolder.Controls.Clear()
                        If bTeachingClasses = False Then
                            StudentsAlreadyInHolder.Controls.Add(New LiteralControl("This teacher has no students yet in the active class"))
                        Else
                            StudentsAlreadyInHolder.Controls.Add(rblStudents)
                        End If
                    End Using 'objReader
                End Using 'objcommand
            End Using 'objConnection
        Catch ex As Exception
            lblStatus.Text = "Problem area - LoadStudentsOfTeacherAlreadyinActiveClass<br/>" + ex.Message
        End Try
    End Sub

    Private Sub LoadStudentsNOTAlreadyinActiveClass(ByVal iTeacherID As Integer)
        Dim strConn As String = GetConnectStringFromWebConfig()
        Try
            Using objconn As New SqlConnection(strConn)
                Dim strSQL As String = <![CDATA[
     SELECT StudentID, Firstname, lastname FROM Students WHERE Studentid NOT IN (SELECT StudentID FROM ClassesStudents WHERE
ClassesStudents.TeacherID=@TeacherID AND ClassId=@ClassID)
ORDER BY Firstname, lastname
    ]]>.Value()
                Using objcommand As New SqlCommand
                    With objcommand
                        .Connection = objconn
                        .Connection.Open()
                        .CommandText = strSQL
                        .Parameters.AddWithValue("@TeacherID", iTeacherID)
                        .Parameters.AddWithValue("@ClassID", GetActiveClassFor(Integer.Parse(Session("LoggedinTeacherID"))))
                    End With
                    Using objReader As SqlDataReader = objcommand.ExecuteReader()
                        Dim rblStudents As New RadioButtonList
                        rblStudents.ID = "ListofStudentsNOTIn"
                        Dim liClass As ListItem
                        Dim bTeachingClasses As Boolean = False
                        Dim strDiv As String = ""
                        While objReader.Read()
                            liClass = New ListItem
                            bTeachingClasses = True
                            With liClass
                                .Value = objReader("StudentID").ToString
                                .Text = objReader("Firstname").ToString + " " + objReader("Lastname").ToString()
                            End With
                            'AddHandler lnkClass.Click, AddressOf ListStudents
                            rblStudents.Items.Add(liClass)
                        End While
                        StudentsNOTInHolder.Controls.Clear()
                        If bTeachingClasses = False Then
                            StudentsNOTInHolder.Controls.Add(New LiteralControl("All students are already in the active class taught by teacher"))
                        Else
                            StudentsNOTInHolder.Controls.Add(rblStudents)
                        End If
                    End Using 'objReader
                End Using 'objcommand
            End Using 'objConnection
        Catch ex As Exception
            lblStatus.Text = "Problem area - LoadStudentsNOTAlreadyinActiveClass<br/>" + ex.Message
        End Try
    End Sub

    'Private Sub LoadStudents()
    '    Dim strConn As String = GetConnectStringFromWebConfig()
    '    Try
    '        Using objconn As New SqlConnection(strConn)
    '            Dim strSQL As String = <![CDATA[
    '   SELECT StudentID, Firstname, Lastname FROM Students WHERE Active>0
    ']]>.Value()
    '            Using objcommand As New SqlCommand
    '                With objcommand
    '                    .Connection = objconn
    '                    .Connection.Open()
    '                    .CommandText = strSQL
    '                End With
    '                Using objReader As SqlDataReader = objcommand.ExecuteReader()
    '                    Dim rblClasses As New RadioButtonList
    '                    rblClasses.ID = "ListofStudents"
    '                    Dim liClass As ListItem
    '                    Dim bTeachingClasses As Boolean = False
    '                    Dim strDiv As String = ""
    '                    While objReader.Read()
    '                        liClass = New ListItem
    '                        bTeachingClasses = True
    '                        With liClass
    '                            .Value = objReader("StudentID").ToString
    '                            .Text = objReader("Firstname").ToString + " " + objReader("Lastname").ToString()
    '                        End With
    '                        'AddHandler lnkClass.Click, AddressOf ListStudents
    '                        rblClasses.Items.Add(liClass)
    '                        lblStatus.Text = ""
    '                    End While
    '                    StudentsHolder.Controls.Clear()
    '                    If bTeachingClasses = False Then
    '                        StudentsHolder.Controls.Add(New LiteralControl("No students are in the system currently"))
    '                    Else
    '                        StudentsHolder.Controls.Add(rblClasses)
    '                    End If
    '                End Using 'objReader
    '            End Using 'objcommand
    '        End Using 'objConnection
    '    Catch ex As Exception
    '        lblStatus.Text = "Problem with system.  Please let administrator know.  And please retry at a later time"
    '    End Try
    'End Sub

    'Private Sub LoadTeachers()
    '    Dim strConn As String = GetConnectStringFromWebConfig()
    '    Try
    '        Using objconn As New SqlConnection(strConn)
    '            'This gets Admin too so he/she has a change to return to their own profile display
    '            Dim strSQL As String = <![CDATA[
    '     SELECT StaffID, Firstname, Lastname FROM StaffMembers WHERE 
    '        Active>0 AND IsAdmin=0
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

    'Private Sub LoadStudentsTaughtbyTeacher(ByVal iTeacherID As Integer)
    '    Dim strStudentsTaughtbyBuilder As New StringBuilder
    '    strStudentsTaughtbyBuilder.Append("<ul>")
    '    Dim strConn As String = GetConnectStringFromWebConfig()
    '    Try
    '        Using objconn As New SqlConnection(strConn)
    '            Dim strSQL As String = <![CDATA[
    '    Select EntryID,Classname,Firstname+' '+Lastname AS name,[TeacherID] FROM [AssessmentDB].[dbo].[ClassesStudents] 
    '        INNER JOIN Students on ClassesStudents.StudentID=Students.StudentID 
    '        INNER JOIN classes ON ClassesStudents.ClassID=Classes.ClassID WHERE teacherid=@TeacherID
    ']]>.Value()
    '            Using objcommand As New SqlCommand
    '                With objcommand
    '                    .Connection = objconn
    '                    .Connection.Open()
    '                    .CommandText = strSQL
    '                    .Parameters.AddWithValue("@TeacherID", iTeacherID)
    '                End With

    '                Using objReader As SqlDataReader = objcommand.ExecuteReader()
    '                    Dim bTeachingClasses As Boolean = False
    '                    While objReader.Read()
    '                        strStudentsTaughtbyBuilder.Append("<li>")
    '                        strStudentsTaughtbyBuilder.Append(objReader("name").ToString + " - " + objReader("classname").ToString)
    '                        strStudentsTaughtbyBuilder.Append("</li>")
    '                        bTeachingClasses = True
    '                        lblStatus.Text = ""
    '                    End While
    '                    strStudentsTaughtbyBuilder.Append("</ul>")
    '                    StudentsHolder1.Controls.Clear()
    '                    If bTeachingClasses = False Then
    '                        StudentsHolder1.Controls.Add(New LiteralControl("This teacher does not teach any student already"))
    '                    Else
    '                        StudentsHolder1.Controls.Add(New LiteralControl(strStudentsTaughtbyBuilder.ToString()))
    '                    End If
    '                End Using 'objReader
    '            End Using 'objcommand
    '        End Using 'objConnection
    '    Catch ex As Exception
    '        lblStatus.Text = "Problem with system.  Please let administrator know.  And please retry at a later time"
    '    End Try

    'End Sub

    Protected Sub lnkReturn_Click(sender As Object, e As EventArgs) Handles lnkReturn.Click
        Response.Redirect("~/default.aspx")
    End Sub

    Protected Sub lnkAddStudents_Click(sender As Object, e As EventArgs) Handles lnkAddStudents.Click
        Dim ctrlControl As Control
        Dim iClassID As Integer = 0
        Dim iStudentID As Integer = 0
        ctrlControl = StudentsNOTInHolder.FindControl("ListofStudentsNOTIn")
        Try
            If ctrlControl IsNot Nothing Then
                Dim rblList As RadioButtonList = CType(ctrlControl, RadioButtonList)
                If rblList.SelectedItem IsNot Nothing Then
                    iStudentID = rblList.SelectedItem.Value
                    'ctrlControl = ClassesHolder.FindControl("ListofStudentsIn")
                    'If ctrlControl IsNot Nothing Then
                    '    rblList = CType(ctrlControl, RadioButtonList)
                    '    If rblList.SelectedItem IsNot Nothing Then
                    '        iStudentID = rblList.SelectedItem.Value
                    '    Else
                    '        lblStatus.Text = "From the list on the right, please select which student to add to list on left"
                    '        Exit Sub
                    '    End If
                    'Else
                    '    iStudentID = 0
                    'End If
                Else
                    lblStatus.Text = "From the list on the right, please select which student to add to list on left"
                    Exit Sub
                End If
            Else
                iStudentID = 0
            End If

            If iStudentID = 0 Then
                lblStatus.Text = String.Format("All available students are already in {0}. No new students available to add", GetClassname(Session("Activeclass")))

            Else
                'If Not StudentinClass(iClassID, iStudentID) Then
                If Session("IsAdmin") IsNot Nothing AndAlso Session("IsAdmin") = True Then
                    'ADMIN has logged in
                    AddStudentToActiveClass(GetActiveClassFor(Integer.Parse(Session("AdminPickedTeacher"))), iStudentID, Integer.Parse(Session("AdminPickedTeacher")))
                    LoadStudentsOfTeacherAlreadyinActiveClass(Session("AdminPickedTeacher"))
                    LoadStudentsNOTAlreadyinActiveClass(Convert.ToInt32(Session("AdminPickedTeacher")))
                    ' LoadStudentsTaughtbyTeacher(Integer.Parse(Session("AdminPickedTeacher"))) 'Refresh that list
                Else
                    AddStudentToActiveClass(GetActiveClassFor(Integer.Parse(Session("LoggedinTeacherID"))), iStudentID, Integer.Parse(Session("LoggedinTeacherID")))
                    LoadStudentsOfTeacherAlreadyinActiveClass(Session("LoggedinTeacherID"))
                    LoadStudentsNOTAlreadyinActiveClass(Convert.ToInt32(Session("LoggedinTeacherID")))
                    'LoadStudentsTaughtbyTeacher(Integer.Parse(Session("LoggedinTeacherID"))) 'Refresh that list
                End If
                'Else
                '    lblStatus.Text = "Selected Student already in Selected class"
                'End If

            End If
        Catch ex As Exception
            lblStatus.Text = "Problem area - lnkAddStudents_Click<br/>" + ex.Message
        End Try

    End Sub

    Private Sub AddStudentToActiveClass(ByVal iClassID As Integer, iStudentID As Integer, iTeacherID As Integer)
        Dim strConn As String = GetConnectStringFromWebConfig()
        'Dim objConn As New SqlConnection(strConn)
        Dim bValid As Boolean = False
        Try
            Using objconn As New SqlConnection(strConn)
                Dim strSQL As String = <![CDATA[
       INSERT INTO ClassesStudents (ClassID,StudentID,TeacherID) VALUES(
            @ClassID,@StudentID,@TeacherID)
    ]]>.Value()
                Using objcommand As New SqlCommand
                    With objcommand
                        .Connection = objconn
                        .Connection.Open()
                        .CommandText = strSQL
                        .Parameters.AddWithValue("@ClassID", iClassID)
                        .Parameters.AddWithValue("@StudentID", iStudentID)
                        .Parameters.AddWithValue("@TeacherID", iTeacherID)
                    End With
                    If objcommand.ExecuteNonQuery() <= 0 Then
                        lblStatus.Text = "Problem adding student to active class"
                    Else
                        'lblStatus.Text = "Successfully added student to active class"
                        lblStatus.Text = String.Format("Student {0} has been added to {1}", GetStudentname(iStudentID), GetClassname(iClassID))
                    End If
                End Using 'objCommand
            End Using 'objconnection
        Catch ex As Exception
            lblStatus.Text = "Problem area - AddStudentToActiveClass<br/>" + ex.Message
        End Try
    End Sub

    Private Sub RemoveStudentFromClass(ByVal iClassID As Integer, iStudentID As Integer, ByVal iTeacherID As Integer)
        Dim strConn As String = GetConnectStringFromWebConfig()
        'Dim objConn As New SqlConnection(strConn)
        Dim bValid As Boolean = False
        Try
            Using objconn As New SqlConnection(strConn)
                Dim strSQL As String = <![CDATA[
    DELETE FROM ClassesStudents WHERE ClassID=@ClassID AND
            StudentID=@StudentID AND TeacherId=@TeacherID
    ]]>.Value()
                Using objcommand As New SqlCommand
                    With objcommand
                        .Connection = objconn
                        .Connection.Open()
                        .CommandText = strSQL
                        objcommand.Parameters.AddWithValue("@ClassID", iClassID)
                        objcommand.Parameters.AddWithValue("@StudentID", iStudentID)
                        objcommand.Parameters.AddWithValue("@TeacherID", iTeacherID)
                    End With
                    If objcommand.ExecuteNonQuery() <= 0 Then
                        lblStatus.Text = "Problem removing student from class"
                    Else
                        lblStatus.Text = "Successfully removed student from class"
                        lblStatus.Text = String.Format("Student {0} has been removed from {1}", GetStudentname(iStudentID), GetClassname(iClassID))
                    End If
                End Using 'objCommand
            End Using 'objconnection
            'objConn.Open()
            'Dim strSQL As String = "DELETE FROM ClassesStudents WHERE ClassID=@ClassID AND
            'StudentID=@StudentID AND TeacherId=@TeacherId"
            'Dim objCommand As New SqlCommand(strSQL, objConn)
            'objCommand.Parameters.AddWithValue("@ClassID", iClassID)
            'objCommand.Parameters.AddWithValue("@StudentID", iStudentID)
            'objCommand.Parameters.AddWithValue("@TeacherID", iTeacherID)
            'If objCommand.ExecuteNonQuery() <= 0 Then
            '    lblStatus.Text = "Problem removing student from class"
            'Else
            '    lblStatus.Text = "Successfully removed student from class"
            'End If
            'objCommand.Dispose()
            'objCommand = Nothing
            'objConn.Dispose()
            'objConn.Close()
            'objConn = Nothing
            'SqlConnection.ClearAllPools()
        Catch ex As Exception
            lblStatus.Text = "Problem area - RemoveStudentFromClass<br/>" + ex.Message
        End Try
    End Sub

    'Private Function StudentinClass(ByVal iClassID As Integer, iStudentID As Integer) As Boolean
    '    Dim bInClass As Boolean = False
    '    Dim iCnt As Integer = 0
    '    Dim strConn As String = GetConnectStringFromWebConfig()
    '    'Dim objConn As New SqlConnection(strConn)
    '    Dim bValid As Boolean = False
    '    Try
    '        Using objconn As New SqlConnection(strConn)
    '            Dim strSQL As String = <![CDATA[
    'SELECT COUNT(*) FROM ClassesStudents WHERE ClassID=@ClassID AND
    '        StudentID=@StudentID
    ']]>.Value()
    '            Using objcommand As New SqlCommand
    '                With objcommand
    '                    .Connection = objconn
    '                    .Connection.Open()
    '                    .CommandText = strSQL
    '                    objcommand.Parameters.AddWithValue("@ClassID", iClassID)
    '                    objcommand.Parameters.AddWithValue("@StudentID", iStudentID)
    '                End With
    '                iCnt = objcommand.ExecuteScalar()
    '                bInClass = iCnt > 0
    '            End Using 'objcommand
    '        End Using 'objConnection
    '    Catch ex As Exception
    '        lblStatus.Text = "Problem with system.  Please let administrator know.  And please retry at a later time."
    '        bInClass = False
    '    End Try
    '    Return bInClass
    'End Function

    Protected Sub lnkRemoveStudents_Click(sender As Object, e As EventArgs) Handles lnkRemoveStudents.Click
        Dim ctrlControl As Control
        Dim iClassID As Integer = 0
        Dim iStudentID As Integer = 0
        ctrlControl = StudentsAlreadyInHolder.FindControl("ListofStudentsIn")
        Try
            If ctrlControl IsNot Nothing Then
                Dim rblList As RadioButtonList = CType(ctrlControl, RadioButtonList)
                If rblList.SelectedItem IsNot Nothing Then
                    iStudentID = rblList.SelectedItem.Value
                    'ctrlControl = StudentsHolder.FindControl("ListOfStudents")
                    'If ctrlControl IsNot Nothing Then
                    '    rblList = CType(ctrlControl, RadioButtonList)
                    '    If rblList.SelectedItem IsNot Nothing Then
                    '        rblList = CType(ctrlControl, RadioButtonList)
                    '        iStudentID = rblList.SelectedItem.Value
                    '    Else
                    '        lblStatus.Text = "Please select the student who you want to remove from the class"
                    '        Exit Sub
                    '    End If
                    'Else
                    '    iStudentID = 0
                    'End If
                Else
                    lblStatus.Text = "From the list on the left, please select which student to remove from and move to list on left"
                    Exit Sub
                End If

            Else
                iStudentID = 0
            End If

            If iStudentID = 0 Then
                lblStatus.Text = String.Format("Currently NO students are already in {0}.None to remove.", GetClassname(Session("Activeclass")))
            Else
                'If StudentinClass(iClassID, iStudentID) Then
                If Session("IsAdmin") IsNot Nothing AndAlso Session("IsAdmin") = True Then
                    'ADMIN has logged in
                    RemoveStudentFromClass(GetActiveClassFor(Integer.Parse(Session("AdminPickedTeacher"))), iStudentID, Integer.Parse(Session("AdminPickedTeacher")))
                    LoadStudentsOfTeacherAlreadyinActiveClass(Session("AdminPickedTeacher"))
                    LoadStudentsNOTAlreadyinActiveClass(Convert.ToInt32(Session("AdminPickedTeacher")))
                    'LoadStudentsTaughtbyTeacher(Integer.Parse(Session("AdminPickedTeacher"))) 'Refresh that list
                Else
                    RemoveStudentFromClass(GetActiveClassFor(Integer.Parse(Session("LoggedinTeacherID"))), iStudentID, Integer.Parse(Session("LoggedinTeacherID")))
                    LoadStudentsOfTeacherAlreadyinActiveClass(Session("LoggedinTeacherID"))
                    LoadStudentsNOTAlreadyinActiveClass(Convert.ToInt32(Session("LoggedinTeacherID")))
                    'LoadStudentsTaughtbyTeacher(Integer.Parse(Session("LoggedinTeacherID"))) 'Refresh that list
                End If

                'Else
                '    lblStatus.Text = "Selected Student NOT in Selected class"
                'End If

            End If
        Catch ex As Exception
            lblStatus.Text = "Problem area - lnkRemoveStudents_Click<br/>" + ex.Message
        End Try

    End Sub

    'Protected Sub lnkLoadTeachers_Click(sender As Object, e As EventArgs) Handles lnkLoadTeachers.Click
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
    '        LoadClasses(iTeacherID)
    '        'LoadStudentsTaughtbyTeacher(iTeacherID)
    '        lblTeacherName.Text = GetTeacherName(iTeacherID)
    '        'lblTeachername2.Text = GetTeacherName(iTeacherID)
    '    End If
    'End Sub

    Private Sub lnkActivateClass_Click(sender As Object, e As EventArgs) Handles lnkActivateClass.Click
        Session("Activeclass") = ddlClasses.SelectedValue()
        ActivateClass(Integer.Parse(ddlClasses.SelectedValue()), Integer.Parse(Session("LoggedinTeacherID")))
        ddlClasses.Items.Clear()
        LoadTeachersClassesandCurrentlyActive(Integer.Parse(Session("LoggedinTeacherID")))
        lblActiveClass.Text = GetTeacherCurrentlyActiveClass(Convert.ToInt32(Session("LoggedinTeacherID")))
        LoadStudentsOfTeacherAlreadyinActiveClass(Session("LoggedinTeacherID"))
        LoadStudentsNOTAlreadyinActiveClass(Session("LoggedinTeacherID"))
    End Sub

    Private Sub ActivateClass(ByVal iClassID As Integer, ByVal iTeacherID As Integer)
        Dim strConn As String = GetConnectStringFromWebConfig()
        'Dim objConn As New SqlConnection(strConn)
        Dim bValid As Boolean = False
        Try
            Using objconn As New SqlConnection(strConn)
                Dim strSQL As String = <![CDATA[
                UPDATE StaffMembers SET CurrentClassID=@CurrentClassID WHERE StaffID=@TeacherID
    ]]>.Value()
                Using objcommand As New SqlCommand
                    With objcommand
                        .Connection = objconn
                        .Connection.Open()
                        .CommandText = strSQL
                        .Parameters.AddWithValue("@CurrentClassID", iClassID)
                        .Parameters.AddWithValue("@TeacherID", iTeacherID)
                    End With
                    If objcommand.ExecuteNonQuery() <= 0 Then
                        lblStatus.Text = "Problem activating the class"
                    Else
                        lblStatus.Text = "Successfully activated the class"
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
            lblStatus.Text = "Problem area - ActivateClass<br/>" + ex.Message
        End Try
    End Sub
End Class