Imports System.Data.SqlClient

Module general

    'Public Function GetConnectString() As String
    '    ' Find the database Name
    '    'It SHOULD BE the same way you specify when logging to SQL Server
    '    'using the Management studio.
    '    'ADDITIONALLY, must make sure that the below server is started by looking
    '    'at the "Services" control panel applet
    '    'Dim strSvr As String = ".\SQLSERVER08_2016" ' LAPTOP
    '    Dim strSvr As String = "(local)" ' DESKTOP
    '    Dim strSvrTimeOut As String = "30"
    '    Dim strCat As String = "AssessmentDB"
    '    Dim strUser As String = "sathi"
    '    Dim strPass As String = "sairam"
    '    ' strSvr = "(local)"
    '    'strPass = "sairam"
    '    '   strUser = "test"
    '    Dim strConn As String = "Data Source=" & strSvr & ";initial catalog=" + strCat + ";"
    '    'strConn += "Integrated Security=SSPI;"
    '    strConn += "uid=" & strUser & ";pwd=" & strPass
    '    Return strConn
    'End Function

    ''' <summary>
    ''' This returns the connection string it reads from web.config file
    ''' </summary>
    ''' <returns></returns>
    Public Function GetConnectStringFromWebConfig() As String
        Dim strConn As String = ""
        strConn = ConfigurationManager.ConnectionStrings("myConnectionString").ConnectionString
        Return strConn
    End Function

    Public Function GetTeacherCurrentlyActiveClass(ByVal iTeacherID As Integer) As String
        Dim strClass As String = ""
        Dim strConn As String = GetConnectStringFromWebConfig()
        Dim objConn As New SqlConnection(strConn)
        Dim bValid As Boolean = False
        Try
            objConn.Open()
            Dim strSQL As String = "SELECT Classname FROM StaffMembers INNER JOIN Classes ON Classes.ClassID=CurrentClassID  WHERE staffid=@TeacherID"
            Dim objCommand As New SqlCommand(strSQL, objConn)
            objCommand.Parameters.AddWithValue("@TeacherID", iTeacherID)
            Dim objReader As SqlDataReader = objCommand.ExecuteReader
            If objReader.Read() Then
                strClass = objReader("Classname").ToString
            Else
                strClass = ""
            End If
            objReader.Close()
            objReader = Nothing
            objCommand.Dispose()
            objCommand = Nothing
            objConn.Dispose()
            objConn.Close()
            objConn = Nothing
            SqlConnection.ClearAllPools()
        Catch ex As Exception
            strClass = "Problem area - GetTeacherCurrentlyActiveClass<br/>" + ex.Message
        End Try
        Return strClass
    End Function

    Public Function GetActiveClassFor(ByVal iTeacherId As Integer) As Integer
        Dim iclassID As Integer = 0
        Dim strConn As String = GetConnectStringFromWebConfig()
        Try
            Using objconn As New SqlConnection(strConn)
                Dim strSQL As String = <![CDATA[
       SELECT CurrentClassID FROM StaffMembers WHERE StaffID=@TeacherID
    ]]>.Value()
                Using objcommand As New SqlCommand
                    With objcommand
                        .Connection = objconn
                        .Connection.Open()
                        .CommandText = strSQL
                        .Parameters.AddWithValue("@TeacherID", iTeacherId)
                    End With
                    Using objReader As SqlDataReader = objcommand.ExecuteReader()
                        If objReader.Read() Then
                            iclassID = Integer.Parse(objReader("CurrentClassID"))
                        Else
                            iclassID = 0
                        End If
                    End Using 'objReader
                End Using 'objcommand
            End Using 'objConnection
        Catch ex As Exception
            iclassID = 0
        End Try
        Return iclassID
    End Function

    Public Function GetActiveSubjectFor(ByVal iTeacherId As Integer) As Integer
        Dim iSubjectID As Integer = 0
        Dim strConn As String = GetConnectStringFromWebConfig()
        Try
            Using objconn As New SqlConnection(strConn)
                Dim strSQL As String = <![CDATA[
       SELECT CurrentSubjectID FROM StaffMembers WHERE StaffID=@TeacherID
    ]]>.Value()
                Using objcommand As New SqlCommand
                    With objcommand
                        .Connection = objconn
                        .Connection.Open()
                        .CommandText = strSQL
                        .Parameters.AddWithValue("@TeacherID", iTeacherId)
                    End With
                    Using objReader As SqlDataReader = objcommand.ExecuteReader()
                        If objReader.Read() Then
                            iSubjectID = Integer.Parse(objReader("CurrentSubjectID"))
                        Else
                            iSubjectID = 0
                        End If
                    End Using 'objReader
                End Using 'objcommand
            End Using 'objConnection
        Catch ex As Exception
            iSubjectID = 0
        End Try
        Return iSubjectID
    End Function

    Public Function GetSubjectname(ByVal iSubjectID As Integer) As String
        Dim strSubjectname As String = ""
        Dim strConn As String = GetConnectStringFromWebConfig()
        Try
            Using objconn As New SqlConnection(strConn)
                Dim strSQL As String = <![CDATA[
       SELECT Subjectname FROM Subjects WHERE SubjectID=@SubjectID
    ]]>.Value()
                Using objcommand As New SqlCommand
                    With objcommand
                        .Connection = objconn
                        .Connection.Open()
                        .CommandText = strSQL
                        .Parameters.AddWithValue("@SubjectID", iSubjectID)
                    End With
                    Using objReader As SqlDataReader = objcommand.ExecuteReader()
                        If objReader.Read() Then
                            strSubjectname = objReader("Subjectname").ToString
                        Else
                            strSubjectname = ""
                        End If
                    End Using 'objReader
                End Using 'objcommand
            End Using 'objConnection
        Catch ex As Exception
            strSubjectname = ""
        End Try
        Return strSubjectname
    End Function

    Public Function GetClassname(ByVal iClassID As Integer) As String
        Dim strClassname As String = ""
        Dim strConn As String = GetConnectStringFromWebConfig()
        Try
            Using objconn As New SqlConnection(strConn)
                Dim strSQL As String = <![CDATA[
       SELECT Classname FROM Classes WHERE ClassID=@ClassID
    ]]>.Value()
                Using objcommand As New SqlCommand
                    With objcommand
                        .Connection = objconn
                        .Connection.Open()
                        .CommandText = strSQL
                        .Parameters.AddWithValue("@ClassID", iClassID)
                    End With
                    Using objReader As SqlDataReader = objcommand.ExecuteReader()
                        If objReader.Read() Then
                            strClassname = objReader("Classname").ToString
                        Else
                            strClassname = ""
                        End If
                    End Using 'objReader
                End Using 'objcommand
            End Using 'objConnection
        Catch ex As Exception
            strClassname = ""
        End Try
        Return strClassname
    End Function

    Public Function GetStudentname(ByVal iStudentID As Integer) As String
        Dim strClassname As String = ""
        Dim strConn As String = GetConnectStringFromWebConfig()
        Try
            Using objconn As New SqlConnection(strConn)
                Dim strSQL As String = <![CDATA[
       SELECT Firstname+ ' ' +Lastname as Fullname FROM Students WHERE StudentID=@StudentID
    ]]>.Value()
                Using objcommand As New SqlCommand
                    With objcommand
                        .Connection = objconn
                        .Connection.Open()
                        .CommandText = strSQL
                        .Parameters.AddWithValue("@StudentID", iStudentID)
                    End With
                    Using objReader As SqlDataReader = objcommand.ExecuteReader()
                        If objReader.Read() Then
                            strClassname = objReader("Fullname").ToString
                        Else
                            strClassname = ""
                        End If
                    End Using 'objReader
                End Using 'objcommand
            End Using 'objConnection
        Catch ex As Exception
            strClassname = ""
        End Try
        Return strClassname
    End Function

    Public Function GetTeacherCurrentlyActiveSubject(ByVal iTeacherID As Integer) As String
        Dim strSubject As String = ""
        Dim strConn As String = GetConnectStringFromWebConfig()
        Dim objConn As New SqlConnection(strConn)
        Dim bValid As Boolean = False
        Try
            objConn.Open()
            Dim strSQL As String = "SELECT Subjectname FROM StaffMembers INNER JOIN Subjects ON Subjects.SubjectID=CurrentSubjectID  WHERE StaffID=@TeacherID"
            Dim objCommand As New SqlCommand(strSQL, objConn)
            objCommand.Parameters.AddWithValue("@TeacherID", iTeacherID)
            Dim objReader As SqlDataReader = objCommand.ExecuteReader
            If objReader.Read() Then
                strSubject = objReader("Subjectname").ToString
            Else
                strSubject = ""
            End If
            objReader.Close()
            objReader = Nothing
            objCommand.Dispose()
            objCommand = Nothing
            objConn.Dispose()
            objConn.Close()
            objConn = Nothing
            SqlConnection.ClearAllPools()
        Catch ex As Exception
            strSubject = "Problem area - GetTeacherCurrentlyActiveClass<br/>" + ex.Message
        End Try
        Return strSubject
    End Function

    ''' <summary>
    ''' Returns a list of subjects for this teacher with comma seperating them
    ''' </summary>
    ''' <param name="iTeacherID"></param>
    ''' <returns></returns>
    Public Function GetTeacherSubjects(ByVal iTeacherID As Integer) As String
        Dim strSubjectsBuilder As New StringBuilder
        Dim strConn As String = GetConnectStringFromWebConfig()
        Dim objConn As New SqlConnection(strConn)
        Dim bValid As Boolean = False
        Try
            objConn.Open()
            'Dim strSQL As String = "SELECT StaffID, Username, Password FROM StaffMembers WHERE Username='" + txtUsername.Text + "' AND Password='" + txtPassword.Text + "'"
            Dim strSQL As String = "SELECT Subjectname FROM TeachersSubjects
                INNER JOIN Subjects on TeachersSubjects.SubjectID=Subjects.SubjectID 
                WHERE TeachersSubjects.TeacherID=@TeacherID"
            Dim objCommand As New SqlCommand(strSQL, objConn)
            objCommand.Parameters.AddWithValue("@TeacherID", iTeacherID)
            Dim objReader As SqlDataReader = objCommand.ExecuteReader
            While objReader.Read()
                strSubjectsBuilder.Append(objReader("Subjectname") + ",")
            End While
            objReader.Close()
            objReader = Nothing
            objCommand.Dispose()
            objCommand = Nothing
            objConn.Dispose()
            objConn.Close()
            objConn = Nothing
            SqlConnection.ClearAllPools()
        Catch ex As Exception
            strSubjectsBuilder.Append("Problem area - GetTeacherCurrentlyActiveClass<br/>" + ex.Message)
        End Try
        Return strSubjectsBuilder.ToString()
    End Function


    ''' <summary>
    ''' Given the teacher ID, this function returns the teacher's name.  This is the
    ''' combination of teacher's first and last names
    ''' </summary>
    ''' <param name="iTeacherID"></param>
    ''' <returns></returns>
    Public Function GetTeacherName(ByVal iTeacherID As Integer) As String
        Dim strFullname As String = ""
        Dim strConn As String = GetConnectStringFromWebConfig()
        Dim objConn As New SqlConnection(strConn)
        Dim bValid As Boolean = False
        Try
            objConn.Open()
            'Dim strSQL As String = "Select StaffID, Username, Password FROM StaffMembers WHERE Username='" + txtUsername.Text + "' AND Password='" + txtPassword.Text + "'"
            Dim strSQL As String = "SELECT Firstname, Lastname FROM StaffMembers
                WHERE StaffMembers.StaffID=@TeacherID"
            Dim objCommand As New SqlCommand(strSQL, objConn)
            objCommand.Parameters.AddWithValue("@TeacherID", iTeacherID)
            Dim objReader As SqlDataReader = objCommand.ExecuteReader
            If objReader.Read() Then
                strFullname = objReader("Firstname").ToString.ToUpper + " " + objReader("Lastname").ToString.ToUpper
            End If
            objReader.Close()
            objReader = Nothing
            objCommand.Dispose()
            objCommand = Nothing
            objConn.Dispose()
            objConn.Close()
            objConn = Nothing
            SqlConnection.ClearAllPools()
        Catch ex As Exception
            strFullname = "Problem area - GetTeacherCurrentlyActiveClass<br/>" + ex.Message
        End Try
        Return strFullname
    End Function

    ''' <summary>
    ''' Returns a list of classes for this teacher with comma seperating them
    ''' </summary>
    ''' <param name="iTeacherID"></param>
    ''' <returns></returns>
    Public Function GetTeacherClasses(ByVal iTeacherID As Integer) As String
        Dim strClassesBuilder As New StringBuilder
        Dim strConn As String = GetConnectStringFromWebConfig()
        Dim objConn As New SqlConnection(strConn)
        Dim bValid As Boolean = False
        Try
            objConn.Open()
            'Dim strSQL As String = "SELECT StaffID, Username, Password FROM StaffMembers WHERE Username='" + txtUsername.Text + "' AND Password='" + txtPassword.Text + "'"
            Dim strSQL As String = "SELECT Classname FROM TeachersClasses
                INNER JOIN Classes on TeachersClasses.ClassID=Classes.ClassID 
                WHERE TeachersClasses.TeacherID=@TeacherID"
            Dim objCommand As New SqlCommand(strSQL, objConn)
            objCommand.Parameters.AddWithValue("@TeacherID", iTeacherID)
            Dim objReader As SqlDataReader = objCommand.ExecuteReader
            While objReader.Read()
                strClassesBuilder.Append(objReader("Classname") + ",")
            End While
            objReader.Close()
            objReader = Nothing
            objCommand.Dispose()
            objCommand = Nothing
            objConn.Dispose()
            objConn.Close()
            objConn = Nothing
            SqlConnection.ClearAllPools()
        Catch ex As Exception
            strClassesBuilder.Append("Problem area - GetTeacherCurrentlyActiveClass<br/>" + ex.Message)
        End Try
        Return strClassesBuilder.ToString()
    End Function
End Module
