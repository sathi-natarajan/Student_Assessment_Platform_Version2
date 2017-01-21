Public Class MessageFromAdmin
    Inherits System.Web.UI.UserControl

    Public Event MessageFromAdmin_DonotshowAgain_CheckChanged(ByVal iMessageID As Integer, ByVal bChecked As Boolean)

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub

    Private Sub chkDonotShowAgain_CheckedChanged(sender As Object, e As EventArgs) Handles chkDonotShowAgain.CheckedChanged
        Dim iMessageID As Integer = MessageID
        RaiseEvent MessageFromAdmin_DonotshowAgain_CheckChanged(iMessageID, chkDonotShowAgain.Checked)
    End Sub

    Public Property MessageID As Integer

    Public WriteOnly Property MessageText As String
        Set(value As String)
            divMessageText.InnerHtml = value
        End Set
    End Property

    Public WriteOnly Property MessageTypeText As String
        Set(value As String)
            lblTypeText.Text = value
        End Set
    End Property

    Public WriteOnly Property DateofActivity As String
        Set(value As String)
            lblDateActivity.Text = value
        End Set
    End Property
End Class