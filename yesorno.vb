Public Class yesorno

    Private Sub IconButton4_Click(sender As Object, e As EventArgs) Handles IconButton4.Click
        Me.Close()
    End Sub

    Private Sub IconButton1_Click(sender As Object, e As EventArgs) Handles IconButton1.Click
        Msgboxresult = Nothing
        Me.Close()
    End Sub

    Private Sub IconButton3_Click(sender As Object, e As EventArgs) Handles IconButton3.Click
        Msgboxresult = True
        Me.Close()
    End Sub

    Private Sub IconButton2_Click(sender As Object, e As EventArgs) Handles IconButton2.Click
        Msgboxresult = False
        Me.Close()
    End Sub
End Class