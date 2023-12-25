Imports System.IO
Imports MySql.Data.MySqlClient

Public Class gift
    Dim adpt, adpt2, adpt3 As MySqlDataAdapter
    Dim sql2, sql3 As String

    Private Sub IconButton2_Click(sender As Object, e As EventArgs) Handles IconButton2.Click
        conn2.Open()
        sql3 = "INSERT INTO banques (`organisme`, `agence`, `compte`) 
                    VALUES ('" + TextBox1.Text + "','" + TextBox2.Text + "','" + TextBox3.Text + "')"
        cmd3 = New MySqlCommand(sql3, conn2)
        cmd3.Parameters.Clear()
        cmd3.ExecuteNonQuery()
        conn2.Close()
        DataGridView2.Rows.Clear()
        adpt = New MySqlDataAdapter("select * from banques", conn2)
        Dim table As New DataTable
        adpt.Fill(table)

        For i = 0 To table.Rows.Count() - 1
            DataGridView2.Rows.Add(table.Rows(i).Item(1), table.Rows(i).Item(2), table.Rows(i).Item(3), "", table.Rows(i).Item(0))
        Next
    End Sub

    Private Sub IconButton12_Click(sender As Object, e As EventArgs) Handles IconButton12.Click
        Me.Close()
    End Sub

    Private Sub Panel5_Paint(sender As Object, e As PaintEventArgs) Handles Panel5.Paint

    End Sub

    Private Sub IconButton1_Click(sender As Object, e As EventArgs) Handles IconButton1.Click
        conn2.Open()
        Dim sql2, sql3 As String
        Dim cmd2, cmd3 As MySqlCommand

        cmd2 = New MySqlCommand("UPDATE `gifts` SET `qty` =  `qty` + 1 WHERE `id` = '" + Label3.Text + "' ", conn2)
        cmd2.ExecuteNonQuery()

        sql3 = "INSERT INTO `giftsoffer`(`gift_name`, `price`) VALUES (@name,@op) "
        cmd3 = New MySqlCommand(sql3, conn2)
        cmd3.Parameters.Clear()
        cmd3.Parameters.AddWithValue("@name", IconButton1.Text)
        cmd3.Parameters.AddWithValue("@op", Convert.ToDouble(Label4.Text).ToString("# ##0.00").Replace(".", ","))
        cmd3.ExecuteNonQuery()

        conn2.Close()
        Me.Close()
    End Sub

    Dim cmd2, cmd3 As MySqlCommand

    Private Sub gift_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        adpt = New MySqlDataAdapter("select * from gifts ORDER BY rand()", conn2)
        Dim table As New DataTable
        adpt.Fill(table)

        IconButton1.Text = table.Rows(0).Item(1)
        Label3.Text = table.Rows(0).Item(0)
        Label4.Text = table.Rows(0).Item(2)
    End Sub

    Private Sub DataGridView2_CellMouseClick(sender As Object, e As DataGridViewCellMouseEventArgs) Handles DataGridView2.CellMouseClick
        If e.RowIndex >= 0 Then
            If e.ColumnIndex = 3 Then
                Dim Rep As Integer
                Dim row As DataGridViewRow = DataGridView2.Rows(e.RowIndex)
                Rep = MsgBox("Voulez-vous vraiment supprimer cette banque ?", vbYesNo)
                If Rep = vbYes Then

                    conn2.Open()
                    Dim sql2, sql3 As String
                    Dim cmd2, cmd3 As MySqlCommand
                    sql2 = "delete from banques where id = '" + row.Cells(4).Value.ToString + "' "
                    cmd2 = New MySqlCommand(sql2, conn2)
                    cmd2.ExecuteNonQuery()

                    sql3 = "INSERT INTO `traces`(`name`, `operation`) VALUES (@name,@op) "
                    cmd3 = New MySqlCommand(sql3, conn2)
                    cmd3.Parameters.Clear()
                    cmd3.Parameters.AddWithValue("@name", Home.Label2.Text)
                    cmd3.Parameters.AddWithValue("@op", "Suppression de banque " & row.Cells(0).Value.ToString)
                    cmd3.ExecuteNonQuery()


                    conn2.Close()

                    DataGridView2.Rows.Clear()
                    adpt = New MySqlDataAdapter("select * from banques", conn2)
                    Dim table As New DataTable
                    adpt.Fill(table)

                    For i = 0 To table.Rows.Count() - 1
                        DataGridView2.Rows.Add(table.Rows(i).Item(1), table.Rows(i).Item(2), table.Rows(i).Item(3), "", table.Rows(i).Item(0))
                    Next
                End If
            End If
        End If
    End Sub
End Class