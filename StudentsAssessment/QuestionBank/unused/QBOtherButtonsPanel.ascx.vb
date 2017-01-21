Public Class QBOtherButtonsPanel
    Inherits System.Web.UI.UserControl

    Public Enum ForScreens
        AddExplanation
        AddImage
    End Enum
    Public Event QbOtherButtonsPanel_OKClick(ByVal enumForscreen As ForScreens)
    Public Enum Filters
        Grade
        Subject
        Standard
        QID
    End Enum
    Dim enumFilter As Filters
    Dim enumForScreen As ForScreens

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
    End Sub

    Public Property ButtonsforScreen() As ForScreens
        Set(value As ForScreens)
            enumForScreen = value
        End Set
        Get
            Return enumForScreen
        End Get
    End Property
    Protected Sub lnkOK_Click(sender As Object, e As EventArgs) Handles lnkOK.Click
        RaiseEvent QbOtherButtonsPanel_OKClick(enumForScreen)
    End Sub
End Class