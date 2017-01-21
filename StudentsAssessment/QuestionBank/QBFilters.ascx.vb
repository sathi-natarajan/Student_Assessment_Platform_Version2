Public Class QBFilters
    Inherits System.Web.UI.UserControl
    Public Enum Filters
        Grade
        Subject
        Standard
        QID
        Questiontype
        Search
        None
    End Enum
    Dim enumFilter As Filters

    Public Event QBFilters_FilterClicked(enumFilter As Filters)
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub

    Protected Sub lnkFilter_Click(sender As Object, e As EventArgs) Handles lnkGradefilter.Click, lnkQIDfilter.Click, lnkQuestiontype.Click, lnkSearch.Click, lnkStandardfilter.Click, lnkSubjectfilter.Click, lnkShowall.Click
        Dim lnkButton As LinkButton = CType(sender, LinkButton)
        Select Case lnkButton.ID
            Case "lnkStandardfilter"
                RaiseEvent QBFilters_FilterClicked(Filters.Standard)
            Case "lnkSubjectfilter"
                RaiseEvent QBFilters_FilterClicked(Filters.Subject)
            Case "lnkGradefilter"
                RaiseEvent QBFilters_FilterClicked(Filters.Grade)
            Case "lnkQIDfilter"
                RaiseEvent QBFilters_FilterClicked(Filters.QID)
            Case "lnkQuestiontype"
                RaiseEvent QBFilters_FilterClicked(Filters.Questiontype)
            Case "lnkSearch"
                RaiseEvent QBFilters_FilterClicked(Filters.Search)
            Case "lnkShowall"
                RaiseEvent QBFilters_FilterClicked(Filters.None)
        End Select
    End Sub

End Class