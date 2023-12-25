Public Class jjj
    Private Sub jjj_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Timer1.Start()


    End Sub

    Private Sub Timer1_Tick(hsender As Object, e As EventArgs) Handles Timer1.Tick
        Timer1.Stop()
        Dim jawab As Integer = MsgBox("jhsgdjhqgsjhqgdjq", vbYesNo)

        If jawab = vbYes Then
            MsgBox("yes")
            Timer1.Start()
        Else
            MsgBox("no")

        End If

    End Sub
End Class