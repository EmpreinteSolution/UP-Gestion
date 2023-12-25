Imports MySql.Data.MySqlClient

Public Class frais
    Private Sub Button1_Click(sender As Object, e As EventArgs)

    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        If TextBox2.Text = "" Or TextBox2.Text = 0 Then
            Casef = True
            tfraisN = "Coût Logistique"
            tfraisM = 0
            Livr = "-"
            TextBox2.Clear()
            Me.Close()
        Else
            Casef = True
            tfraisN = "Coût Logistique"
            tfraisM = TextBox2.Text
            If ComboBox2.Text <> "" Then
                Livr = ComboBox2.Text
            Else
                Livr = "Livreur"
            End If
            TextBox2.Clear()
            Me.Close()
            End If
    End Sub
    Dim adpt As MySqlDataAdapter
    Private Sub Panel1_Paint(sender As Object, e As PaintEventArgs) Handles Panel1.Paint

    End Sub

    Private Sub frais_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
        If e.KeyCode = Keys.Enter Then
            If TextBox2.Text = "" Or TextBox2.Text = 0 Then
                Casef = True
                tfraisN = "Coût Logistique"
                tfraisM = 0
                Livr = "-"
                TextBox2.Text = 0
                Me.Close()
            Else
                Casef = True
                tfraisN = "Coût Logistique"
                tfraisM = TextBox2.Text
                TextBox2.Text = 0
                Livr = ComboBox2.Text
                Me.Close()
            End If
        End If

        If e.KeyCode = Keys.Escape Then
            TextBox2.Text = 0
            Me.Close()
        End If

    End Sub

    Private Sub frais_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.KeyPreview = True
        Casef = False

        TextBox2.Text = 0
        adpt = New MySqlDataAdapter("select name from livreurs", conn2)
        Dim table As New DataTable
        adpt.Fill(table)
        ComboBox2.Items.Clear()
        If table.Rows.Count <> 0 Then
            ComboBox2.Items.Add("Sans livraison")
            For i = 0 To table.Rows.Count - 1
                ComboBox2.Items.Add(table.Rows(i).Item(0))
            Next
        End If
        ComboBox2.Text = dashboard.Label32.Text
        TextBox2.Text = dashboard.Label30.Text
    End Sub

    Private Sub ComboBox2_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox2.SelectedIndexChanged
        If ComboBox2.Text = "Sans livraison" Then
            TextBox2.Text = 0
        Else
            If dashboard.Label23.Text <= 500 Then
                TextBox2.Text = 10
            End If
            If dashboard.Label23.Text > 500 AndAlso dashboard.Label23.Text <= 1000 Then
                TextBox2.Text = 20
            End If
            If dashboard.Label23.Text > 1000 Then
                TextBox2.Text = 30
            End If
        End If
    End Sub
End Class