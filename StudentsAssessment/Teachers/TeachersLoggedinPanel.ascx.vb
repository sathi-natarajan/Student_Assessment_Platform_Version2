﻿Public Class TeachersLoggedinPanel
    Inherits System.Web.UI.UserControl

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session("IsAdmin") IsNot Nothing AndAlso Session("IsAdmin") = True Then
            lblAdminLoginMessage.Visible = True
        Else
            lblAdminLoginMessage.Visible = False
        End If
    End Sub

    Public Property Name() As String
        Get
            Return spnTeacherName.InnerHtml
        End Get
        Set(value As String)
            spnTeacherName.InnerHtml = value
        End Set
    End Property

    Protected Sub lnkLogout_Click(sender As Object, e As EventArgs) Handles lnkLogout.Click
        Session.Abandon()
        Session.Clear()
        Response.Redirect("~/default.aspx")
    End Sub

    Protected Sub lnkEditProfile_Click(sender As Object, e As EventArgs) Handles lnkEditProfile.Click
        If Session("IsAdmin") IsNot Nothing AndAlso Session("IsAdmin") = True Then
            Response.Redirect("~/Admins/TeacherProfile.aspx")
        Else
            Response.Redirect("~/Teachers/TeacherProfile.aspx")
        End If
    End Sub

    Protected Sub lnkClass_Click(sender As Object, e As EventArgs) Handles lnkClass.Click
        If Session("IsAdmin") IsNot Nothing AndAlso Session("IsAdmin") = True Then
            Response.Redirect("~/Admins/TeacherClass.aspx")
        Else
            Response.Redirect("~/Teachers/TeacherClass.aspx")
        End If
    End Sub

    Protected Sub lnkSubject_Click(sender As Object, e As EventArgs) Handles lnkSubject.Click
        If Session("IsAdmin") IsNot Nothing AndAlso Session("IsAdmin") = True Then
            Response.Redirect("~/Admins/TeacherSubject.aspx")
        Else
            Response.Redirect("~/Teachers/TeacherSubject.aspx")
        End If
    End Sub
End Class