Public Class QuestionFilter
    Inherits System.Web.UI.UserControl

    Public Enum Filters
        Grade
        Subject
        Standard
        QID
    End Enum
    Dim enumFilter As Filters

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
    End Sub
End Class