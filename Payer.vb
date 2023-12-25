Imports MySql.Data.MySqlClient

Public Class Payer
    Dim conn As New MySqlConnection("datasource=localhost; username=root; password=; database=pos")
    Dim adpt As MySqlDataAdapter
    Private Sub Payer_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        adpt = New MySqlDataAdapter("select id,montant,place from num where payer = '0' order by id desc", conn)
        Dim table As New DataTable
        adpt.Fill(table)
        Dim i As Integer
        For i = 0 To table.Rows.Count - 1
            DataGridView1.Rows.Add(table.Rows(i).Item(0), table.Rows(i).Item(1) & " Dhs", table.Rows(i).Item(2))
        Next

    End Sub

    Private Sub DataGridView1_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellClick
        If e.RowIndex >= 0 Then
            Dim row As DataGridViewRow = DataGridView1.Rows(e.RowIndex)
            Dim id = row.Cells(0).Value.ToString
            DataGridView3.Rows.Clear()
            Label2.Text = id
            Label3.Text = row.Cells(1).Value.ToString
            Label4.Text = row.Cells(2).Value.ToString
            adpt = New MySqlDataAdapter("select name,qty,montant from tickets where tick = '" + id + "'", conn)
            Dim table As New DataTable
            adpt.Fill(table)
            For i = 0 To table.Rows.Count - 1
                DataGridView3.Rows.Add(table.Rows(i).Item(0), table.Rows(i).Item(1), table.Rows(i).Item(2))
            Next
            Button1.Visible = True

        End If
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        conn.Open()
        Dim cmd2 As MySqlCommand
        Dim sql2 As String

        sql2 = "UPDATE num SET payer = 1 WHERE id = '" & DataGridView1.SelectedCells(0).Value & "'"
        cmd2 = New MySqlCommand(sql2, conn)

        cmd2.ExecuteNonQuery()
        MsgBox("Paiement enregistré")
        DataGridView1.Rows().Clear()
        DataGridView3.Rows().Clear()
        Label2.Text = ""
        Label3.Text = 0
        Label4.Text = ""
        adpt = New MySqlDataAdapter("select id,montant,place from num where payer = '0' order by id desc", conn)
        Dim table As New DataTable
        adpt.Fill(table)
        Dim i As Integer
        For i = 0 To table.Rows.Count - 1
            DataGridView1.Rows.Add(table.Rows(i).Item(0), table.Rows(i).Item(1) & " Dhs", table.Rows(i).Item(2))
        Next
        conn.Close()
        Button1.Visible = False

    End Sub

End Class