Imports System.ComponentModel

Public Class clavier


    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        calcul.TextBox1.Text = calcul.TextBox1.Text + Button2.Text
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        calcul.TextBox1.Text = calcul.TextBox1.Text + Button3.Text
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        calcul.TextBox1.Text = calcul.TextBox1.Text + Button4.Text
    End Sub

    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        calcul.TextBox1.Text = calcul.TextBox1.Text + Button5.Text
    End Sub

    Private Sub Button6_Click(sender As Object, e As EventArgs) Handles Button6.Click
        calcul.TextBox1.Text = calcul.TextBox1.Text + Button6.Text
    End Sub

    Private Sub Button11_Click(sender As Object, e As EventArgs) Handles Button11.Click
        calcul.TextBox1.Text = calcul.TextBox1.Text + Button11.Text
    End Sub

    Private Sub Button10_Click(sender As Object, e As EventArgs) Handles Button10.Click
        calcul.TextBox1.Text = calcul.TextBox1.Text + Button10.Text
    End Sub

    Private Sub Button9_Click(sender As Object, e As EventArgs) Handles Button9.Click
        calcul.TextBox1.Text = calcul.TextBox1.Text + Button9.Text
    End Sub

    Private Sub Button8_Click(sender As Object, e As EventArgs) Handles Button8.Click
        calcul.TextBox1.Text = calcul.TextBox1.Text + Button8.Text
    End Sub

    Private Sub Button7_Click(sender As Object, e As EventArgs) Handles Button7.Click
        calcul.TextBox1.Text = calcul.TextBox1.Text + Button7.Text
    End Sub

    Private Sub Button12_Click(sender As Object, e As EventArgs) Handles Button12.Click
        If calcul.TextBox1.Text = "" Then
        Else
            calcul.TextBox1.Text = calcul.TextBox1.Text.Remove(calcul.TextBox1.Text.Length - 1)
        End If

    End Sub

    Private Sub Panel3_Paint(sender As Object, e As PaintEventArgs) Handles Panel3.Paint

    End Sub

    Private Sub Panel3_DoubleClick(sender As Object, e As EventArgs) Handles Panel3.DoubleClick
        Me.Close()
    End Sub

    Private Sub clavier_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim x As Integer
        Dim y As Integer
        x = Screen.PrimaryScreen.WorkingArea.Width / 2 - Me.Width / 2
        y = My.Computer.Screen.WorkingArea.Height - 225
        Me.Location = New Point(x, y)
    End Sub

    Private Sub clavier_Closing(sender As Object, e As CancelEventArgs) Handles Me.Closing

    End Sub

    Private Sub IconButton9_Click(sender As Object, e As EventArgs) Handles IconButton9.Click
        Me.Close()
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        calcul.TextBox1.Text = calcul.TextBox1.Text + Button1.Text
    End Sub

    Private Sub Button13_Click(sender As Object, e As EventArgs) Handles Button13.Click
        calcul.TextBox1.Text = calcul.TextBox1.Text + Button13.Text
    End Sub
End Class