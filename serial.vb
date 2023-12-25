Imports System.Globalization
Imports System.Threading
Imports MySql.Data.MySqlClient
Imports System.Configuration
Imports System.Net
Imports System.Net.NetworkInformation
Imports System.Security.Cryptography
Imports System.Text
Imports System.IO

Public Class serial
    Dim sql As New MySqlConnection("datasource=localhost; username=root; password=; database=librairie_graph")
    Dim sql2 As String
    Dim cmd2 As MySqlCommand

    Private Sub serial_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub
    'Function getMacAddress()
    '    Dim nics() As NetworkInterface = NetworkInterface.GetAllNetworkInterfaces()
    '    Return nics(3).GetPhysicalAddress.ToString
    'End Function

    Private Sub TextBox1_TextChanged(sender As Object, e As EventArgs) Handles TextBox1.TextChanged

        If TextBox1.Text = "VaFjcRsZ" Then

            Panel1.BackColor = Color.ForestGreen
            Button1.BackColor = Color.ForestGreen
            Button1.Cursor = Cursors.Hand
        Else
            Panel1.BackColor = Color.Red
            Button1.BackColor = Color.Silver
            Button1.Cursor = Cursors.No
        End If

        If TextBox1.Text = "" Then
            Panel1.BackColor = Color.White
            Button1.BackColor = Color.Silver
            Button1.Cursor = Cursors.No
        End If

    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Me.Close()
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        If Button1.BackColor = Color.ForestGreen Then
            Dim appPath As String = Application.StartupPath()

            Dim crackfolder As DirectoryInfo = New DirectoryInfo(appPath & "\zh")

            System.IO.Directory.Delete(crackfolder.ToString, True)

            Form1.Show()
            Me.Close()

        End If
    End Sub
End Class