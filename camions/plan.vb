Public Class plan
    Private currentChildForm As Form
    Private Sub OpenChildForm(childForm As Form)
        'Open only form'
        If currentChildForm IsNot Nothing Then
            currentChildForm.Close()
        End If
        currentChildForm = childForm
        'end'
        childForm.TopLevel = False
        childForm.FormBorderStyle = FormBorderStyle.None
        childForm.Dock = DockStyle.Fill
        Panel3.Controls.Add(childForm)
        Panel3.Tag = childForm
        childForm.BringToFront()
        childForm.Show()
        Me.Text = childForm.Text
    End Sub
    Private Sub plan_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Button2.PerformClick()
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        OpenChildForm(New add)
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        OpenChildForm(New listTrucks)
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        OpenChildForm(New venteCamion)
    End Sub

    Private Sub Panel2_Paint(sender As Object, e As PaintEventArgs) Handles Panel2.Paint

    End Sub
End Class