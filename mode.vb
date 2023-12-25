Public Class mode
    Private Sub PictureBox1_Click(sender As Object, e As EventArgs) Handles PictureBox1.Click
        IconButton31.PerformClick()
    End Sub

    Private Sub PictureBox4_Click(sender As Object, e As EventArgs) Handles PictureBox4.Click
        IconButton33.PerformClick()
    End Sub

    Private Sub PictureBox3_Click(sender As Object, e As EventArgs) Handles PictureBox3.Click
        IconButton32.PerformClick()
    End Sub

    Private Sub IconButton31_Click(sender As Object, e As EventArgs) Handles IconButton31.Click
        IconButton31.BackColor = Color.DarkOrange
        IconButton31.ForeColor = Color.White
        dashboard.ComboBox1.Text = "Espèce"

        IconButton32.BackColor = Color.White
        IconButton32.ForeColor = Color.Black

        IconButton33.BackColor = Color.White
        IconButton33.ForeColor = Color.Black

        dashboard.Panel3.Visible = False
    End Sub

    Private Sub IconButton32_Click(sender As Object, e As EventArgs) Handles IconButton32.Click
        IconButton32.BackColor = Color.DarkOrange
        IconButton32.ForeColor = Color.White
        dashboard.ComboBox1.Text = "Crédit"

        IconButton31.BackColor = Color.White
        IconButton31.ForeColor = Color.Black

        IconButton33.BackColor = Color.White
        IconButton33.ForeColor = Color.Black

        dashboard.Panel3.Visible = False
    End Sub
    Dim tpevar As Boolean = False
    Private Sub IconButton33_Click(sender As Object, e As EventArgs) Handles IconButton33.Click
        IconButton33.BackColor = Color.DarkOrange
        IconButton33.ForeColor = Color.White
        dashboard.ComboBox1.Text = "TPE"

        IconButton32.BackColor = Color.White
        IconButton32.ForeColor = Color.Black

        IconButton31.BackColor = Color.White
        IconButton31.ForeColor = Color.Black

        dashboard.Panel3.Visible = True
    End Sub

    Private Sub OK_Click(sender As Object, e As EventArgs) Handles OK.Click
        Me.DialogResult = DialogResult.OK
        Me.Close()
    End Sub
End Class