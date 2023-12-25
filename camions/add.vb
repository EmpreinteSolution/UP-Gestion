Imports MySql.Data.MySqlClient

Public Class add
    Dim adpt As MySqlDataAdapter
    Private Sub IconButton2_Click(sender As Object, e As EventArgs) Handles IconButton2.Click
        Dim cmd As New MySqlCommand("INSERT INTO `camions`(`matricule`, `password`, `active`, `prix`, `role`) VALUES (@mt,@pwd,0,0,0)", conn2)
        cmd.Parameters.AddWithValue("@mt", TextBox1.Text)
        cmd.Parameters.AddWithValue("@pwd", TextBox2.Text)
        conn2.Open()
        cmd.ExecuteNonQuery()
        conn2.Close()
        MsgBox("added")
    End Sub

    Private Sub DataGridView1_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellClick
        If e.RowIndex >= 0 Then
            If e.ColumnIndex = 3 Then
                Dim Rep As Integer
                Rep = MsgBox("Voulez-vous vraiment supprimer ce camion ?", vbYesNo)
                If Rep = vbYes Then

                    Dim row As DataGridViewRow = DataGridView1.Rows(e.RowIndex)
                    Dim id As Integer = row.Cells(0).Value.ToString
                    conn2.Open()
                    Dim sql2 As String
                    Dim cmd2 As MySqlCommand
                    sql2 = "delete from camions where id = " + id.ToString

                    cmd2 = New MySqlCommand(sql2, conn2)

                    cmd2.ExecuteNonQuery()
                    conn2.Close()
                    DataGridView1.Rows.Clear()

                    adpt = New MySqlDataAdapter("select * from camions", conn2)
                    Dim table As New DataTable
                    adpt.Fill(table)
                    For i = 0 To table.Rows.Count - 1

                        DataGridView1.Rows.Add(table.Rows(i).Item(0), table.Rows(i).Item(1), table.Rows(i).Item(2), Nothing)

                    Next


                End If

            End If
        End If
    End Sub


    Private Sub add_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        adpt = New MySqlDataAdapter("select * from camions", conn2)
        Dim table As New DataTable
        adpt.Fill(table)
        For i = 0 To table.Rows.Count - 1

            DataGridView1.Rows.Add(table.Rows(i).Item(0), table.Rows(i).Item(1), table.Rows(i).Item(2), Nothing)

        Next
    End Sub
End Class