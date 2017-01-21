Module AssessmentTableLengths

    ''' <summary>
    ''' For now, only varchar fields are included as they seem to be the only one for which length needs to be double-checked before 
    ''' saving anything into it
    ''' </summary>
    Public Enum StaffMembers
        Firstname = 25
        Lastname = 25
        Description = 150
        Username = 25
        Password = 25
    End Enum
    Public Enum Questions
        QuestionsText = 250
        Standard = 10
        Description = 250
    End Enum

    Public Enum Answers
        AnswerText = 150
    End Enum
End Module
