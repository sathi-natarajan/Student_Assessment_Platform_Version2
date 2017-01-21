Imports System.Data.SqlClient

Public Class TeachersLogin
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        SiteHeader1.Pagetitle = "TEACHER INTERFACE"
        SiteHeader1.PageDesc = "A place for teachers to log into system"
        txtUsername.Focus()
    End Sub

    Protected Sub lnkReturn_Click(sender As Object, e As EventArgs) Handles lnkReturn.Click
        Response.Redirect("default.aspx")
    End Sub

    Protected Sub lnkTeacherLogin_Click(sender As Object, e As EventArgs) Handles lnkTeacherLogin.Click
        Dim strConn As String = GetConnectStringFromWebConfig() 'GetConnectString()
        ' Dim objConn As New SqlConnection(strConn)
        Dim bValid As Boolean = False
        Dim iTeacherID As Integer = 0
        Try
            Using objconn As New SqlConnection(strConn)
                Dim strSQL As String = <![CDATA[
                SELECT StaffID, Username, Password,ISAdmin FROM StaffMembers WHERE Username=@Username AND Password=@Password AND Active>0
    ]]>.Value()
                Using objcommand As New SqlCommand
                    With objcommand
                        .Connection = objconn
                        .Connection.Open()
                        .CommandText = strSQL
                        .Parameters.AddWithValue("@Username", txtUsername.Text)
                        .Parameters.AddWithValue("@Password", txtPassword.Text)
                    End With

                    Using objReader As SqlDataReader = objcommand.ExecuteReader()
                        Dim strFullnameBuilder As New StringBuilder
                        If objReader.Read() Then
                            bValid = True
                            If objReader("IsAdmin") = True Then
                                Session("IsAdmin") = True
                            Else
                                Session("IsAdmin") = False
                            End If
                            iTeacherID = Convert.ToInt32(objReader("StaffID"))
                        Else
                            bValid = False
                            iTeacherID = 0
                            Session("IsAdmin") = False
                        End If
                    End Using 'objReader
                End Using 'objcommand
            End Using 'objConnection
            'objConn.Open()

            If bValid = False Then
                lblStatus.Text = "Invalid Username or Password.  Please retry."
                Session("LoggedinTeacherID") = Nothing
                Session("IsAdmin") = False
                Session.Clear()
            Else
                Session("LoggedinTeacherID") = iTeacherID
                Session("Activesubject") = GetActiveSubjectFor(iTeacherID)
                Session("Activeclass") = GetActiveClassFor(iTeacherID)
                'If admin logs in, take the person to admin section of site
                If Session("IsAdmin") Then
                    Response.Redirect("~/Admins/default.aspx")
                Else
                    'check also existence of page
                    If Session("TogoAFTERLogin") IsNot Nothing AndAlso Session("TogoAFTERLogin").ToString.Trim <> "" AndAlso Session("TogoAFTERLogin").ToString.EndsWith(".aspx") = True Then
                        Dim strTogoURL As String = Session("TogoAFTERLogin").ToString
                        Session("TogoAFTERLogin") = Nothing 'to prevent further going there without logging in
                        Response.Redirect(strTogoURL)
                    Else
                        Response.Redirect("~/default.aspx")
                    End If
                End If
            End If
            'Dim strSQL As String = "SELECT StaffID, Username, Password,ISAdmin FROM StaffMembers WHERE Username=@Username AND Password=@Password AND Active>0"
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
            lblStatus.Text = "System Problem.  Please try again later."
        End Try
    End Sub

End Class