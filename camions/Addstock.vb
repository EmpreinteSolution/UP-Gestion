Imports MySql.Data.MySqlClient

Public Class Addstock
    Dim adpt As New MySqlDataAdapter
    Dim tbl As DataTable
    Dim cmd As New MySqlCommand
    Private Sub Panel1_Paint(sender As Object, e As PaintEventArgs) Handles Panel1.Paint

    End Sub

    Private Sub IconButton2_Click(sender As Object, e As EventArgs) Handles IconButton2.Click
        DataGridView1.Rows.Add(TextBox1.Text, ComboBox1.Text, TextBox3.Text, TextBox6.Text, Nothing)
    End Sub

    Private Sub IconButton1_Click(sender As Object, e As EventArgs) Handles IconButton1.Click
        conn2.Open()
        For i = 0 To DataGridView1.Rows.Count - 1
            cmd = New MySqlCommand("INSERT INTO `camion_sous_stocks`(`id_camion`, `code_article`, `article`, `prix_ttc`, `qte`) VALUES (@v1,@v2,@v3,@v4,@v5)", conn2)
            cmd.Parameters.AddWithValue("@v1", comionid)
            cmd.Parameters.AddWithValue("@v2", DataGridView1.Rows(i).Cells(0).Value)
            cmd.Parameters.AddWithValue("@v3", DataGridView1.Rows(i).Cells(1).Value)
            cmd.Parameters.AddWithValue("@v4", DataGridView1.Rows(i).Cells(2).Value)
            cmd.Parameters.AddWithValue("@v5", DataGridView1.Rows(i).Cells(3).Value)
            cmd.ExecuteNonQuery()

            cmd = New MySqlCommand("UPDATE `article` SET `Stock` = `Stock` - @stck WHERE `Code` = @cod ", conn2)
            cmd.Parameters.AddWithValue("@stck", DataGridView1.Rows(i).Cells(3).Value)
            cmd.Parameters.AddWithValue("@cod", DataGridView1.Rows(i).Cells(0).Value)
            cmd.ExecuteNonQuery()

        Next
        MsgBox("Sous stock ajouté")

        conn2.Close()
    End Sub

    Private Sub DataGridView1_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellContentClick
        If e.RowIndex > -1 And e.ColumnIndex = 4 Then
            DataGridView1.Rows.RemoveAt(e.RowIndex)
        End If
    End Sub

    Private Sub TextBox1_KeyDown(sender As Object, e As KeyEventArgs) Handles TextBox1.KeyDown
        If e.KeyCode = Keys.Enter Then

            adpt = New MySqlDataAdapter("SELECT `Article` ,`PV_TTC`  FROM `article` WHERE `Code` LIKE '" + TextBox1.Text + "'", conn2)
            tbl = New DataTable
            adpt.Fill(tbl)
            If tbl.Rows.Count = 1 Then
                ComboBox1.Text = tbl.Rows(0).Item(0).ToString
                TextBox3.Text = tbl.Rows(0).Item(1).ToString
            End If
        End If
    End Sub

    Private Sub Addstock_Load(sender As Object, e As EventArgs) Handles MyBase.Load





    End Sub

    Private Sub ComboBox1_SelectedIndexChanged(sender As Object, e As EventArgs)

    End Sub

    Protected Sub txtVariat()



    End Sub

    Private Sub TextBox2_TextChanged(sender As Object, e As EventArgs) Handles txt.TextChanged
        ComboBox1.Items.Clear()

        adpt = New MySqlDataAdapter("SELECT `Article`  FROM `article` WHERE `Article` LIKE '%" + txt.Text + "%'", conn2)
        tbl = New DataTable

        adpt.Fill(tbl)
        For Each dt As DataRow In tbl.Rows
            ComboBox1.Items.Add(dt.Item(0))
        Next
    End Sub

    Private Sub ComboBox1_SelectedIndexChanged_1(sender As Object, e As EventArgs) Handles ComboBox1.SelectedIndexChanged
        adpt = New MySqlDataAdapter("SELECT `Code` , `Article` ,`PV_TTC`  FROM `article` WHERE `Article` = '" + ComboBox1.Text + "'", conn2)
        tbl = New DataTable
        adpt.Fill(tbl)
        If tbl.Rows.Count = 1 Then
            TextBox1.Text = tbl.Rows(0).Item(0).ToString
            TextBox3.Text = tbl.Rows(0).Item(2).ToString
        End If
    End Sub
End Class